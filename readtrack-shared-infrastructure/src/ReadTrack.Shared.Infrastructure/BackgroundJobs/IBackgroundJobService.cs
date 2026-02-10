using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReadTrack.Shared.Infrastructure.BackgroundJobs
{
    /// <summary>
    /// Abstracts the background job provider (e.g., Hangfire) to decouple application logic from specific implementations.
    /// </summary>
    public interface IBackgroundJobService
    {
        /// <summary>
        /// Queues a fire-and-forget background job.
        /// </summary>
        /// <param name="methodCall">The method call expression to execute.</param>
        /// <returns>The unique identifier of the created job.</returns>
        string Enqueue(Expression<Action> methodCall);

        /// <summary>
        /// Queues a fire-and-forget background job using a specific service type.
        /// This is the preferred method for resolving dependencies from the IoC container.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <param name="methodCall">The method call expression using the resolved service.</param>
        /// <returns>The unique identifier of the created job.</returns>
        string Enqueue<T>(Expression<Action<T>> methodCall);

        /// <summary>
        /// Schedules a background job to run after a specified delay.
        /// </summary>
        /// <param name="methodCall">The method call expression to execute.</param>
        /// <param name="delay">The delay to wait before executing the job.</param>
        /// <returns>The unique identifier of the created job.</returns>
        string Schedule(Expression<Action> methodCall, TimeSpan delay);

        /// <summary>
        /// Schedules a background job to run after a specified delay using a specific service type.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <param name="methodCall">The method call expression using the resolved service.</param>
        /// <param name="delay">The delay to wait before executing the job.</param>
        /// <returns>The unique identifier of the created job.</returns>
        string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);

        /// <summary>
        /// Schedules a background job to run at a specific point in time.
        /// </summary>
        /// <param name="methodCall">The method call expression to execute.</param>
        /// <param name="enqueueAt">The exact time to execute the job.</param>
        /// <returns>The unique identifier of the created job.</returns>
        string Schedule(Expression<Action> methodCall, DateTimeOffset enqueueAt);

        /// <summary>
        /// Schedules a background job to run at a specific point in time using a specific service type.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <param name="methodCall">The method call expression using the resolved service.</param>
        /// <param name="enqueueAt">The exact time to execute the job.</param>
        /// <returns>The unique identifier of the created job.</returns>
        string Schedule<T>(Expression<Action<T>> methodCall, DateTimeOffset enqueueAt);
    }
}