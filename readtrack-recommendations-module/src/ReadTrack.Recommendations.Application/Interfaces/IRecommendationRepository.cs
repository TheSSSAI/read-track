using System;
using System.Threading;
using System.Threading.Tasks;
using ReadTrack.Recommendations.Domain.Entities;

namespace ReadTrack.Recommendations.Application.Interfaces
{
    /// <summary>
    /// Repository interface for managing recommendation-related entities in the persistence layer.
    /// </summary>
    public interface IRecommendationRepository
    {
        /// <summary>
        /// Adds a new recommendation job to the repository.
        /// </summary>
        Task AddJobAsync(RecommendationJob job, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a recommendation job by its unique identifier.
        /// </summary>
        Task<RecommendationJob?> GetJobByIdAsync(Guid jobId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing recommendation job.
        /// </summary>
        Task UpdateJobAsync(RecommendationJob job, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a user feedback record.
        /// </summary>
        Task AddFeedbackAsync(RecommendationFeedback feedback, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a recommendation entity by its ID.
        /// </summary>
        Task<Recommendation?> GetRecommendationByIdAsync(Guid recommendationId, CancellationToken cancellationToken);
    }
}