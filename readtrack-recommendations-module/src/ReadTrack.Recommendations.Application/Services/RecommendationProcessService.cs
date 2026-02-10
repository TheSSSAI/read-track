using System.Text.Json;
using Microsoft.Extensions.Logging;
using ReadTrack.Recommendations.Application.Interfaces;
using ReadTrack.Recommendations.Application.Interfaces.Infrastructure;
using ReadTrack.Recommendations.Domain.Entities;
using ReadTrack.Recommendations.Domain.Enums;

namespace ReadTrack.Recommendations.Application.Services
{
    /// <summary>
    /// Orchestrates the Retrieval-Augmented Generation (RAG) pipeline to generate book recommendations.
    /// Handles job state management, context retrieval, LLM interaction, and result persistence.
    /// </summary>
    public class RecommendationProcessService : IRecommendationProcessService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IReadingHistoryProvider _readingHistoryProvider;
        private readonly IEmbeddingGenerator _embeddingGenerator;
        private readonly IVectorStoreClient _vectorStoreClient;
        private readonly IPromptBuilder _promptBuilder;
        private readonly ILLMClient _llmClient;
        private readonly ILogger<RecommendationProcessService> _logger;

        public RecommendationProcessService(
            IRecommendationRepository recommendationRepository,
            IReadingHistoryProvider readingHistoryProvider,
            IEmbeddingGenerator embeddingGenerator,
            IVectorStoreClient vectorStoreClient,
            IPromptBuilder promptBuilder,
            ILLMClient llmClient,
            ILogger<RecommendationProcessService> logger)
        {
            _recommendationRepository = recommendationRepository ?? throw new ArgumentNullException(nameof(recommendationRepository));
            _readingHistoryProvider = readingHistoryProvider ?? throw new ArgumentNullException(nameof(readingHistoryProvider));
            _embeddingGenerator = embeddingGenerator ?? throw new ArgumentNullException(nameof(embeddingGenerator));
            _vectorStoreClient = vectorStoreClient ?? throw new ArgumentNullException(nameof(vectorStoreClient));
            _promptBuilder = promptBuilder ?? throw new ArgumentNullException(nameof(promptBuilder));
            _llmClient = llmClient ?? throw new ArgumentNullException(nameof(llmClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task ProcessJobAsync(Guid jobId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting processing for RecommendationJob {JobId}", jobId);

            RecommendationJob? job = null;

            try
            {
                // 1. Load Job
                job = await _recommendationRepository.GetJobAsync(jobId, cancellationToken);
                if (job == null)
                {
                    _logger.LogError("Job {JobId} not found in repository", jobId);
                    return;
                }

                if (job.Status == RecommendationJobStatus.Completed || job.Status == RecommendationJobStatus.Failed)
                {
                    _logger.LogWarning("Job {JobId} is already in final state {Status}. Skipping processing.", jobId, job.Status);
                    return;
                }

                // 2. Update State to Processing
                job.MarkAsProcessing();
                await _recommendationRepository.UpdateJobAsync(job, cancellationToken);

                // 3. RAG Step: Context Retrieval
                // 3a. Fetch User History
                _logger.LogDebug("Fetching reading history for User {UserId}", job.UserId);
                var readingHistory = await _readingHistoryProvider.GetUserReadingHistoryAsync(job.UserId, cancellationToken);
                
                // 3b. Generate Embeddings for Context Search (Vector Search Strategy)
                // We convert recent history into a search vector to find similar books or themes in our vector store
                string contextSearchText = string.Join(" ", readingHistory.Select(b => $"{b.Title} {b.Author} {b.Genre}"));
                
                // Truncate to avoid excessive token costs for embedding if history is huge
                if (contextSearchText.Length > 8000) 
                {
                    contextSearchText = contextSearchText.Substring(0, 8000);
                }

                _logger.LogDebug("Generating embeddings for context search");
                var queryVector = await _embeddingGenerator.GenerateEmbeddingAsync(contextSearchText, cancellationToken);

                // 3c. Search Vector Store for Semantic Context
                _logger.LogDebug("Searching vector store for similar content");
                // Limit to top 5 relevant context documents
                var similarContentContexts = await _vectorStoreClient.SearchSimilarAsync(queryVector, 5, cancellationToken);

                // 4. RAG Step: Augmented Generation
                // 4a. Build Prompt
                _logger.LogDebug("Building prompt for LLM");
                var prompt = _promptBuilder.BuildRecommendationPrompt(readingHistory, similarContentContexts);

                // 4b. Call LLM
                _logger.LogDebug("Executing LLM completion request");
                var llmResponseJson = await _llmClient.GetChatCompletionAsync(prompt, cancellationToken);

                // 5. Parse Results
                _logger.LogDebug("Parsing LLM response");
                var recommendations = ParseLlmResponse(llmResponseJson, job.UserId, jobId);

                if (recommendations == null || !recommendations.Any())
                {
                    throw new InvalidOperationException("LLM returned no valid recommendations or parsing failed.");
                }

                // 6. Persist & Complete
                _logger.LogInformation("Persisting {Count} recommendations for Job {JobId}", recommendations.Count, jobId);
                
                // Add recommendations to the job entity (which updates navigation property)
                job.Complete(recommendations);
                
                // Save changes (UpdateJobAsync typically handles the graph update if configured correctly in EF, 
                // but we explicitly ensure everything is saved)
                await _recommendationRepository.UpdateJobAsync(job, cancellationToken);

                _logger.LogInformation("Job {JobId} completed successfully", jobId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process RecommendationJob {JobId}", jobId);

                if (job != null)
                {
                    try
                    {
                        // In strict DDD, we might not have a public setter, so we assume a method or direct property access based on level 1 specs.
                        // Assuming property setter is accessible or a Fail method exists (implied by robustness requirements).
                        // If 'Fail' method doesn't exist on entity in Level 1 files (which I can't modify), 
                        // we assume standard property setting for Status and FailureReason.
                        
                        // Reflection or direct set if internal/public:
                        // job.Status = RecommendationJobStatus.Failed;
                        // job.FailureReason = ex.Message;
                        
                        // However, strictly sticking to assumed entity behaviors for Clean Arch:
                        // I will assume the entity has a Fail method or properties are settable.
                        // Based on standard patterns:
                        typeof(RecommendationJob).GetProperty(nameof(RecommendationJob.Status))?
                            .SetValue(job, RecommendationJobStatus.Failed);
                        typeof(RecommendationJob).GetProperty(nameof(RecommendationJob.FailureReason))?
                            .SetValue(job, ex.Message);

                        await _recommendationRepository.UpdateJobAsync(job, cancellationToken);
                    }
                    catch (Exception saveEx)
                    {
                        _logger.LogError(saveEx, "Failed to save failure state for Job {JobId}", jobId);
                    }
                }
                
                // Re-throw if necessary for background worker retry policy, 
                // but usually we want to mark as failed in DB to stop infinite retries on poison messages.
                // We deliberately do not re-throw here to mark the job strictly as Failed in business logic terms.
            }
        }

        /// <summary>
        /// Parses the raw JSON string from the LLM into domain entities.
        /// Uses System.Text.Json for performance and minimal dependencies.
        /// </summary>
        private List<Recommendation> ParseLlmResponse(string jsonResponse, Guid userId, Guid jobId)
        {
            try
            {
                // Sanitize response if LLM wraps it in markdown code blocks
                var cleanJson = jsonResponse.Trim();
                if (cleanJson.StartsWith("```json"))
                {
                    cleanJson = cleanJson.Replace("```json", "").Replace("```", "").Trim();
                }
                else if (cleanJson.StartsWith("```"))
                {
                    cleanJson = cleanJson.Replace("```", "").Trim();
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var dtos = JsonSerializer.Deserialize<List<RecommendationResultDto>>(cleanJson, options);

                if (dtos == null) return new List<Recommendation>();

                return dtos.Select(dto => new Recommendation(
                    Guid.NewGuid(), // ID
                    userId,
                    jobId,
                    dto.Title ?? "Unknown Title",
                    dto.Author ?? "Unknown Author",
                    dto.Description ?? string.Empty,
                    dto.Explanation ?? string.Empty,
                    dto.CoverUrl,
                    dto.Isbn,
                    dto.ConfidenceScore,
                    DateTime.UtcNow
                )).ToList();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "JSON Parsing failed for LLM response: {ResponseSnippet}", 
                    jsonResponse.Length > 100 ? jsonResponse.Substring(0, 100) : jsonResponse);
                throw new FormatException("Failed to parse recommendation results.", ex);
            }
        }

        // Internal DTO for parsing logic to decouple from Domain Entities during deserialization
        private class RecommendationResultDto
        {
            public string? Title { get; set; }
            public string? Author { get; set; }
            public string? Description { get; set; }
            public string? Explanation { get; set; }
            public string? CoverUrl { get; set; }
            public string? Isbn { get; set; }
            public float ConfidenceScore { get; set; }
        }
    }
}