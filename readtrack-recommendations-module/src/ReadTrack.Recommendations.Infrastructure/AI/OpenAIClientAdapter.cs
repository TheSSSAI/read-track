using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Recommendations.Application.Models; // Assumed location for ChatPrompt based on interface usage
using ReadTrack.Recommendations.Infrastructure.Configuration;

namespace ReadTrack.Recommendations.Infrastructure.AI
{
    /// <summary>
    /// Adapter implementation for the OpenAI API using the OpenAI-DotNet library.
    /// Implements both LLM interaction and Embedding generation interfaces.
    /// Includes Polly resilience policies for transient failure handling.
    /// </summary>
    public class OpenAIClientAdapter : ILLMClient, IEmbeddingGenerator
    {
        private readonly OpenAIClient _openAiClient;
        private readonly OpenAIOptions _options;
        private readonly ILogger<OpenAIClientAdapter> _logger;
        private readonly AsyncRetryPolicy _retryPolicy;

        public OpenAIClientAdapter(
            IOptions<OpenAIOptions> options,
            ILogger<OpenAIClientAdapter> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new ArgumentException("OpenAI API Key is not configured.");
            }

            // Initialize the OpenAI Client
            _openAiClient = new OpenAIClient(_options.ApiKey);

            // Define a resilience policy for HTTP request errors (429 Too Many Requests, 5xx Server Errors)
            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>(ex => !ex.CancellationToken.IsCancellationRequested) // Timeout
                .Or<Exception>(ex => ex.Message.Contains("429") || ex.Message.Contains("500") || ex.Message.Contains("503")) // SDK specific exceptions if not wrapped in HttpRequestException
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // Exponential backoff: 2s, 4s, 8s
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            exception,
                            "OpenAI API request failed. Waiting {TimeSpan} before retry {RetryCount}. Error: {Message}",
                            timeSpan,
                            retryCount,
                            exception.Message);
                    });
        }

        /// <summary>
        /// Generates a chat completion response from the LLM based on the provided prompt.
        /// </summary>
        public async Task<string> GetChatCompletionAsync(ChatPrompt prompt, CancellationToken cancellationToken)
        {
            if (prompt == null) throw new ArgumentNullException(nameof(prompt));

            _logger.LogInformation("Sending chat completion request to OpenAI model: {ModelId}", _options.ModelId);

            var messages = new List<Message>
            {
                new Message(Role.System, prompt.SystemMessage),
                new Message(Role.User, prompt.UserMessage)
            };

            // Assuming ChatPrompt has optional history
            if (prompt.History != null && prompt.History.Count > 0)
            {
                // Insert history before the final user message if architecture dictates, 
                // typically history is folded into the prompt builder logic, but if passed here:
                foreach (var hist in prompt.History)
                {
                    messages.Insert(messages.Count - 1, new Message(hist.Role == "User" ? Role.User : Role.Assistant, hist.Content));
                }
            }

            var chatRequest = new ChatRequest(
                messages,
                model: _options.ModelId,
                temperature: 0.7,
                maxTokens: 2000 // Adjust based on requirement or configuration
            );

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    var response = await _openAiClient.ChatEndpoint.GetCompletionAsync(chatRequest, cancellationToken);
                    
                    if (response == null || response.Choices == null || response.Choices.Count == 0)
                    {
                        _logger.LogError("OpenAI returned an empty response.");
                        throw new InvalidOperationException("Received empty response from OpenAI.");
                    }

                    var content = response.FirstChoice.Message.Content;
                    
                    // Log usage for cost monitoring
                    if (response.Usage != null)
                    {
                        _logger.LogInformation("OpenAI Token Usage - Prompt: {PromptTokens}, Completion: {CompletionTokens}, Total: {TotalTokens}",
                            response.Usage.PromptTokens,
                            response.Usage.CompletionTokens,
                            response.Usage.TotalTokens);
                    }

                    return content;
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    _logger.LogError(ex, "Failed to get chat completion from OpenAI.");
                    throw;
                }
            });
        }

        /// <summary>
        /// Generates a vector embedding for the provided text.
        /// </summary>
        public async Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Text cannot be empty for embedding generation.", nameof(text));

            _logger.LogDebug("Generating embedding for text length: {Length}", text.Length);

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    var response = await _openAiClient.EmbeddingsEndpoint.CreateEmbeddingAsync(
                        text, 
                        model: _options.EmbeddingModelId, 
                        cancellationToken: cancellationToken);

                    if (response == null || response.Data == null || response.Data.Count == 0)
                    {
                        _logger.LogError("OpenAI returned an empty embedding response.");
                        throw new InvalidOperationException("Received empty embedding response from OpenAI.");
                    }

                    // Convert double[] to float[] as generally used in vector stores
                    var embeddingDoubles = response.Data[0].Embedding;
                    var embeddingFloats = new float[embeddingDoubles.Count];
                    
                    for (int i = 0; i < embeddingDoubles.Count; i++)
                    {
                        embeddingFloats[i] = (float)embeddingDoubles[i];
                    }

                    return embeddingFloats;
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    _logger.LogError(ex, "Failed to generate embedding from OpenAI.");
                    throw;
                }
            });
        }
    }
}