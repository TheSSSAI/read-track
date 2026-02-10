using System;
using MediatR;
using ReadTrack.Recommendations.Application.DTOs;

namespace ReadTrack.Recommendations.Application.Features.Recommendations
{
    /// <summary>
    /// CQRS Query to poll the status of a recommendation generation job.
    /// </summary>
    /// <remarks>
    /// Handler Logic:
    /// 1. Retrieves the RecommendationJob by Id.
    /// 2. Validates that the requesting user owns the job.
    /// 3. Maps the entity to RecommendationJobStatusDto.
    /// </remarks>
    public record GetRecommendationJobStatusQuery : IRequest<RecommendationJobStatusDto>
    {
        /// <summary>
        /// The unique identifier of the job to check.
        /// </summary>
        public Guid JobId { get; }

        /// <summary>
        /// The ID of the user requesting the status. Used for security authorization.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Creates a new instance of GetRecommendationJobStatusQuery.
        /// </summary>
        /// <param name="jobId">The Job ID returned by the generation command.</param>
        /// <param name="userId">The ID of the requesting user.</param>
        /// <exception cref="ArgumentException">Thrown if IDs are empty.</exception>
        public GetRecommendationJobStatusQuery(Guid jobId, Guid userId)
        {
            if (jobId == Guid.Empty)
            {
                throw new ArgumentException("JobId cannot be empty.", nameof(jobId));
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            }

            JobId = jobId;
            UserId = userId;
        }
    }
}