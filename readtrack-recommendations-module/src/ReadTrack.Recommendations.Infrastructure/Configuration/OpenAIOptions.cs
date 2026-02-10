using System.ComponentModel.DataAnnotations;

namespace ReadTrack.Recommendations.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration options for the OpenAI API integration.
    /// Maps to the "OpenAI" section in appsettings.json.
    /// </summary>
    public sealed class OpenAIOptions
    {
        public const string SectionName = "OpenAI";

        /// <summary>
        /// The API Key for authenticating with OpenAI.
        /// This should be stored securely (e.g., User Secrets, Azure Key Vault, AWS Secrets Manager).
        /// </summary>
        [Required(ErrorMessage = "OpenAI API Key is required.")]
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        /// The Organization ID (optional), used for billing attribution if multiple orgs exist.
        /// </summary>
        public string? OrganizationId { get; set; }

        /// <summary>
        /// The specific LLM model identifier to use for chat completions (e.g., "gpt-4", "gpt-3.5-turbo").
        /// Defaults to "gpt-4".
        /// </summary>
        [Required]
        public string ChatModelId { get; set; } = "gpt-4";

        /// <summary>
        /// The specific model identifier to use for generating vector embeddings (e.g., "text-embedding-ada-002").
        /// </summary>
        [Required]
        public string EmbeddingModelId { get; set; } = "text-embedding-ada-002";

        /// <summary>
        /// The maximum number of tokens to generate in the completion.
        /// Controls cost and response length.
        /// </summary>
        [Range(1, 32000)]
        public int MaxTokens { get; set; } = 1000;

        /// <summary>
        /// The sampling temperature to use, between 0 and 2.
        /// Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.
        /// </summary>
        [Range(0.0, 2.0)]
        public double Temperature { get; set; } = 0.7;

        /// <summary>
        /// Timeout in seconds for API requests.
        /// </summary>
        public int RequestTimeoutSeconds { get; set; } = 30;

        /// <summary>
        /// Retry count for transient failures (HTTP 429, 5xx).
        /// </summary>
        public int MaxRetries { get; set; } = 3;
    }
}