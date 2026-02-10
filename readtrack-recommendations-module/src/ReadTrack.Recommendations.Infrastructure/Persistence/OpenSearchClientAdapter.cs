using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenSearch.Client;
using Polly;
using Polly.Retry;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Recommendations.Infrastructure.Configuration;

namespace ReadTrack.Recommendations.Infrastructure.Persistence
{
    /// <summary>
    /// Adapter implementation for Amazon OpenSearch Service.
    /// Provides functionality to search for similar vectors using k-NN.
    /// </summary>
    public class OpenSearchClientAdapter : IVectorStoreClient
    {
        private readonly IOpenSearchClient _client;
        private readonly OpenSearchOptions _options;
        private readonly ILogger<OpenSearchClientAdapter> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public OpenSearchClientAdapter(
            IOptions<OpenSearchOptions> options,
            ILogger<OpenSearchClientAdapter> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrWhiteSpace(_options.Endpoint))
            {
                throw new ArgumentException("OpenSearch Endpoint is not configured.");
            }

            var pool = new SingleNodeConnectionPool(new Uri(_options.Endpoint));
            var settings = new ConnectionSettings(pool)
                .DefaultIndex(_options.IndexName)
                .ThrowExceptions(alwaysThrow: true) // Ensure exceptions are thrown for Polly to catch
                .EnableDebugMode(); // Helpful for dev, consider toggling based on env

            // Configure Basic Auth if provided, otherwise assume IAM/SigV4 context or VPC access
            if (!string.IsNullOrEmpty(_options.Username) && !string.IsNullOrEmpty(_options.Password))
            {
                settings.BasicAuthentication(_options.Username, _options.Password);
            }

            _client = new OpenSearchClient(settings);

            // Resilience Policy for DB connectivity issues
            _retryPolicy = Policy
                .Handle<OpenSearchClientException>()
                .Or<System.Net.Http.HttpRequestException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            exception,
                            "OpenSearch request failed. Waiting {TimeSpan} before retry {RetryCount}.",
                            timeSpan,
                            retryCount);
                    });
        }

        /// <summary>
        /// Performs a k-Nearest Neighbors (k-NN) search to find documents semantically similar to the provided vector.
        /// </summary>
        /// <param name="embeddingVector">The query vector.</param>
        /// <param name="limit">The maximum number of results to return (k).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of content strings from the matching documents.</returns>
        public async Task<List<string>> SearchSimilarAsync(float[] embeddingVector, int limit, CancellationToken cancellationToken)
        {
            if (embeddingVector == null || embeddingVector.Length == 0)
                throw new ArgumentException("Embedding vector cannot be empty.", nameof(embeddingVector));

            if (limit <= 0) limit = 5;

            _logger.LogInformation("Executing k-NN search on index {IndexName} with limit {Limit}", _options.IndexName, limit);

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    // Construct k-NN query
                    // Assuming the field mapping in OpenSearch for the vector is named "embedding"
                    // and the text content field is named "content".
                    var searchResponse = await _client.SearchAsync<dynamic>(s => s
                        .Index(_options.IndexName)
                        .Size(limit)
                        .Query(q => q
                            .Knn(k => k
                                .Field("embedding") // Must match index mapping
                                .Vector(embeddingVector)
                                .K(limit)
                            )
                        )
                        .Source(src => src.Includes(i => i.Field("content"))), // Only fetch content field
                        cancellationToken
                    );

                    if (!searchResponse.IsValid)
                    {
                        _logger.LogError("OpenSearch query failed: {ServerError}", searchResponse.ServerError?.Error?.Reason ?? searchResponse.DebugInformation);
                        throw new Exception($"OpenSearch query failed: {searchResponse.ServerError?.Error?.Reason}");
                    }

                    var results = new List<string>();

                    foreach (var hit in searchResponse.Hits)
                    {
                        // Dynamic parsing of the source document
                        // In a strictly typed system, we would map to a DTO, but for RAG context building, string extraction is often sufficient.
                        try 
                        {
                            // Using dynamic access via Newtonsoft.Json structure often used by OpenSearch client
                            // or simple dictionary access
                            var source = hit.Source as IDictionary<string, object>;
                            if (source != null && source.TryGetValue("content", out var contentObj))
                            {
                                results.Add(contentObj?.ToString() ?? string.Empty);
                            }
                            else
                            {
                                // Fallback for dynamic object property access
                                dynamic dynamicSource = hit.Source;
                                if (dynamicSource.content != null)
                                {
                                    results.Add(dynamicSource.content.ToString());
                                }
                            }
                        }
                        catch (Exception parseEx)
                        {
                            _logger.LogWarning(parseEx, "Failed to parse search hit content. Skipping document ID {Id}.", hit.Id);
                        }
                    }

                    _logger.LogDebug("k-NN search returned {Count} documents.", results.Count);
                    return results;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during OpenSearch execution.");
                    throw;
                }
            });
        }
    }
}