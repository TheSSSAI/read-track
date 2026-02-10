using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Recommendations.Application.Services
{
    /// <summary>
    /// Application service interface that orchestrates the end-to-end recommendation generation process.
    /// This service is typically invoked by a background job processor.
    /// </summary>
    public interface IRecommendationProcessService
    {
        /// <summary>
        /// Executes the recommendation pipeline for a specific job.
        /// This includes context retrieval, embedding generation, LLM inference, and result storage.
        /// </summary>
        /// <param name="jobId">The ID of the recommendation job to process.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ProcessJobAsync(Guid jobId, CancellationToken cancellationToken);
    }
}