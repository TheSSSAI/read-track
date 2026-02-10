using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Extensions.Logging;
using ReadTrack.Shared.Infrastructure.BackgroundJobs; // References Level 1 abstraction

namespace ReadTrack.Shared.Infrastructure.BackgroundJobs
{
    /// <summary>
    /// Implementation of IBackgroundJobService using Hangfire.
    /// Abstracts the static Hangfire BackgroundJob class into an injectable service
    /// to support testing and architectural decoupling.
    /// </summary>
    public class HangfireJobService : IBackgroundJobService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ILogger<HangfireJobService> _logger;

        public HangfireJobService(
            IBackgroundJobClient backgroundJobClient,
            ILogger<HangfireJobService> logger)
        {
            _backgroundJobClient = backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public string Enqueue(Expression<Action> methodCall)
        {
            try
            {
                _logger.LogDebug("Enqueuing background job.");
                var jobId = _backgroundJobClient.Enqueue(methodCall);
                _logger.LogInformation("Successfully enqueued background job with ID: {JobId}", jobId);
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enqueue background job.");
                throw new InvalidOperationException("Could not enqueue background job.", ex);
            }
        }

        /// <inheritdoc />
        public string Enqueue<T>(Expression<Action<T>> methodCall)
        {
            try
            {
                _logger.LogDebug("Enqueuing background job for type {Type}.", typeof(T).Name);
                var jobId = _backgroundJobClient.Enqueue<T>(methodCall);
                _logger.LogInformation("Successfully enqueued background job for {Type} with ID: {JobId}", typeof(T).Name, jobId);
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enqueue background job for type {Type}.", typeof(T).Name);
                throw new InvalidOperationException($"Could not enqueue background job for type {typeof(T).Name}.", ex);
            }
        }

        /// <inheritdoc />
        public string Schedule(Expression<Action> methodCall, TimeSpan delay)
        {
            try
            {
                _logger.LogDebug("Scheduling background job with delay {Delay}.", delay);
                var jobId = _backgroundJobClient.Schedule(methodCall, delay);
                _logger.LogInformation("Successfully scheduled background job with ID: {JobId}", jobId);
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to schedule background job.");
                throw new InvalidOperationException("Could not schedule background job.", ex);
            }
        }

        /// <inheritdoc />
        public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            try
            {
                _logger.LogDebug("Scheduling background job for type {Type} with delay {Delay}.", typeof(T).Name, delay);
                var jobId = _backgroundJobClient.Schedule<T>(methodCall, delay);
                _logger.LogInformation("Successfully scheduled background job for {Type} with ID: {JobId}", typeof(T).Name, jobId);
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to schedule background job for type {Type}.", typeof(T).Name);
                throw new InvalidOperationException($"Could not schedule background job for type {typeof(T).Name}.", ex);
            }
        }

        /// <inheritdoc />
        public bool Delete(string jobId)
        {
            try
            {
                _logger.LogDebug("Deleting background job with ID: {JobId}", jobId);
                var result = _backgroundJobClient.Delete(jobId);
                
                if (result)
                {
                    _logger.LogInformation("Successfully deleted background job with ID: {JobId}", jobId);
                }
                else
                {
                    _logger.LogWarning("Failed to delete background job with ID: {JobId} (Job might not exist or state prevents deletion).", jobId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting background job with ID: {JobId}", jobId);
                throw new InvalidOperationException($"Could not delete background job {jobId}.", ex);
            }
        }

        /// <inheritdoc />
        public string ContinueJobWith(string parentJobId, Expression<Action> methodCall)
        {
            try
            {
                _logger.LogDebug("Creating continuation job for parent ID: {ParentJobId}", parentJobId);
                var jobId = _backgroundJobClient.ContinueJobWith(parentJobId, methodCall);
                _logger.LogInformation("Successfully created continuation job with ID: {JobId} for parent {ParentJobId}", jobId, parentJobId);
                return jobId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create continuation job for parent ID: {ParentJobId}", parentJobId);
                throw new InvalidOperationException($"Could not create continuation job for parent {parentJobId}.", ex);
            }
        }
    }
}