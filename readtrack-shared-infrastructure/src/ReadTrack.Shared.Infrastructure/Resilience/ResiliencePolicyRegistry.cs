using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

namespace ReadTrack.Shared.Infrastructure.Resilience
{
    /// <summary>
    /// Provides a centralized registry for creating and retrieving configured Polly resilience policies.
    /// Implements <see cref="IResiliencePolicyProvider"/> to expose these policies to consumers.
    /// </summary>
    public class ResiliencePolicyRegistry : IResiliencePolicyProvider
    {
        private readonly ResilienceOptions _options;
        private readonly ILogger<ResiliencePolicyRegistry> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResiliencePolicyRegistry"/> class.
        /// </summary>
        /// <param name="options">The configuration options for resilience settings.</param>
        /// <param name="logger">The logger instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if options or logger are null.</exception>
        public ResiliencePolicyRegistry(IOptions<ResilienceOptions> options, ILogger<ResiliencePolicyRegistry> logger)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates or retrieves an asynchronous retry policy configured with exponential backoff and jitter.
        /// Handles HttpRequestException, TimeoutException, and HTTP 5xx status codes (when used with HttpResponseMessage).
        /// </summary>
        /// <param name="policyKey">A unique key identifying the policy usage context (e.g., "OpenAI", "Database").</param>
        /// <returns>A configured AsyncRetryPolicy.</returns>
        public AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy(string policyKey)
        {
            var delay = Backoff.DecorrelatedJitterBackoffV2(
                medianFirstRetryDelay: TimeSpan.FromSeconds(1),
                retryCount: _options.RetryCount
            );

            return Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .OrResult(msg => (int)msg.StatusCode >= 500 || msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                .WaitAndRetryAsync(
                    retryCount: _options.RetryCount,
                    sleepDurationProvider: (retryAttempt) => 
                    {
                        // Safe fallback if the jitter provider returns fewer enumerations than retry count
                        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                    },
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
                        if (outcome.Exception != null)
                        {
                            _logger.LogWarning(
                                outcome.Exception,
                                "Resilience Retry [{PolicyKey}]: Attempt {RetryAttempt} of {MaxRetries} failed. Waiting {Delay}ms. Error: {Message}",
                                policyKey, retryAttempt, _options.RetryCount, timespan.TotalMilliseconds, outcome.Exception.Message);
                        }
                        else
                        {
                            _logger.LogWarning(
                                "Resilience Retry [{PolicyKey}]: Attempt {RetryAttempt} of {MaxRetries} failed. Waiting {Delay}ms. Status Code: {StatusCode}",
                                policyKey, retryAttempt, _options.RetryCount, timespan.TotalMilliseconds, outcome.Result?.StatusCode);
                        }
                    });
        }

        /// <summary>
        /// Creates or retrieves an asynchronous circuit breaker policy.
        /// </summary>
        /// <param name="policyKey">A unique key identifying the policy usage context.</param>
        /// <returns>A configured AsyncCircuitBreakerPolicy.</returns>
        public AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(string policyKey)
        {
            return Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .OrResult(msg => (int)msg.StatusCode >= 500 || msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: _options.CircuitBreakerThreshold,
                    durationOfBreak: TimeSpan.FromSeconds(_options.CircuitBreakerDurationSeconds),
                    onBreak: (outcome, breakDuration) =>
                    {
                        _logger.LogError(
                            "Resilience Circuit [{PolicyKey}]: Circuit OPENED for {Duration}s due to failure. Last Error: {Message}",
                            policyKey, breakDuration.TotalSeconds, outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString());
                    },
                    onReset: () =>
                    {
                        _logger.LogInformation("Resilience Circuit [{PolicyKey}]: Circuit RESET. Connectivity restored.", policyKey);
                    },
                    onHalfOpen: () =>
                    {
                        _logger.LogInformation("Resilience Circuit [{PolicyKey}]: Circuit HALF-OPEN. Testing connectivity.", policyKey);
                    });
        }
    }
}