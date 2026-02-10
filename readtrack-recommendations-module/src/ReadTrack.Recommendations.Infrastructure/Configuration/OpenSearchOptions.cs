using System;
using System.ComponentModel.DataAnnotations;

namespace ReadTrack.Recommendations.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration options for the Amazon OpenSearch Serverless integration.
    /// Maps to the "OpenSearch" section in appsettings.json.
    /// </summary>
    public sealed class OpenSearchOptions
    {
        public const string SectionName = "OpenSearch";

        /// <summary>
        /// The fully qualified endpoint URL for the OpenSearch domain/collection.
        /// </summary>
        [Required(ErrorMessage = "OpenSearch Endpoint is required.")]
        [Url(ErrorMessage = "OpenSearch Endpoint must be a valid URL.")]
        public string Endpoint { get; set; } = string.Empty;

        /// <summary>
        /// The name of the index storing user context embeddings.
        /// </summary>
        [Required(ErrorMessage = "Default Index Name is required.")]
        public string DefaultIndex { get; set; } = "user-context-embeddings";

        /// <summary>
        /// The username for Basic Authentication (if not using IAM/SigV4).
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// The password for Basic Authentication (if not using IAM/SigV4).
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// AWS Region for SigV4 signing (e.g., "us-east-1"). Required if using IAM authentication.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// The dimension of the vectors being stored (e.g., 1536 for ada-002).
        /// Used for index validation.
        /// </summary>
        public int VectorDimension { get; set; } = 1536;

        /// <summary>
        /// The number of nearest neighbors to retrieve (k).
        /// </summary>
        [Range(1, 100)]
        public int DefaultK { get; set; } = 5;
    }
}