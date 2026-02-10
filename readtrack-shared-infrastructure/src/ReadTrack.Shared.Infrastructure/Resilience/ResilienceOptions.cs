using System.ComponentModel.DataAnnotations;

namespace ReadTrack.Shared.Infrastructure.Resilience
{
    /// <summary>
    /// Configuration options for Polly resilience policies.
    /// Maps to the "Resilience" section in appsettings.json.
    /// </summary>
    public class ResilienceOptions
    {
        public const string SectionName = "Resilience";

        /// <summary>
        /// The number of retry attempts for transient failures (HTTP 5xx, 408, Network Failures).
        /// Default is 3.
        /// </summary>
        [Range(1, 10, ErrorMessage = "Retry count must be between 1 and 10.")]
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// The number of consecutive failures allowed before opening the circuit breaker.
        /// Default is 5.
        /// </summary>
        [Range(1, 50, ErrorMessage = "Circuit breaker threshold must be between 1 and 50.")]
        public int CircuitBreakerThreshold { get; set; } = 5;

        /// <summary>
        /// The duration in seconds that the circuit remains open before transitioning to half-open.
        /// Default is 30 seconds.
        /// </summary>
        [Range(1, 300, ErrorMessage = "Circuit breaker duration must be between 1 and 300 seconds.")]
        public int CircuitBreakerDurationSeconds { get; set; } = 30;

        /// <summary>
        /// The base delay in seconds for exponential backoff during retries.
        /// Default is 2 seconds.
        /// </summary>
        [Range(1, 10, ErrorMessage = "Backoff delay must be between 1 and 10 seconds.")]
        public int ExponentialBackoffSeconds { get; set; } = 2;
    }
}