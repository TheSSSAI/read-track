using System.Net.Http;
using Polly;

namespace ReadTrack.Shared.Infrastructure.Resilience
{
    /// <summary>
    /// Provides configured Polly resilience policies for HTTP clients.
    /// Used to centralize configuration of circuit breakers and retries.
    /// </summary>
    public interface IResiliencePolicyProvider
    {
        /// <summary>
        /// Retrieves a configured async retry policy.
        /// Handles HttpRequestException, TimeoutException, and HTTP 5xx/408 status codes.
        /// </summary>
        /// <param name="policyKey">A unique key identifying the policy (e.g., "ExternalApi").</param>
        /// <returns>An async policy for handling HttpResponseMessages.</returns>
        IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(string policyKey);

        /// <summary>
        /// Retrieves a configured async circuit breaker policy.
        /// </summary>
        /// <param name="policyKey">A unique key identifying the policy context.</param>
        /// <returns>An async circuit breaker policy for handling HttpResponseMessages.</returns>
        IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(string policyKey);

        /// <summary>
        /// Retrieves a combined policy wrapping retry and circuit breaker logic.
        /// </summary>
        /// <param name="policyKey">A unique key identifying the policy context.</param>
        /// <returns>An async policy wrap.</returns>
        IAsyncPolicy<HttpResponseMessage> GetResiliencePipeline(string policyKey);
    }
}