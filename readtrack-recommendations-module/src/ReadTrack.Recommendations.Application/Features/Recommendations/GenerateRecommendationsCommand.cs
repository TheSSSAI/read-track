using System;
using MediatR;

namespace ReadTrack.Recommendations.Application.Features.Recommendations
{
    /// <summary>
    /// CQRS Command to initiate the personalized recommendation generation process.
    /// This is an asynchronous operation that returns a Job ID for polling.
    /// </summary>
    /// <remarks>
    /// Handler Logic:
    /// 1. Validates the user's subscription tier limits (5/month for free users).
    /// 2. Creates a RecommendationJob in 'Pending' state.
    /// 3. Queues the job for background processing via Hangfire/BackgroundService.
    /// 4. Returns the JobId.
    /// </remarks>
    public record GenerateRecommendationsCommand : IRequest<Guid>
    {
        /// <summary>
        /// The unique identifier of the user requesting recommendations.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Creates a new instance of GenerateRecommendationsCommand.
        /// </summary>
        /// <param name="userId">The ID of the requesting user.</param>
        /// <exception cref="ArgumentException">Thrown if userId is empty.</exception>
        public GenerateRecommendationsCommand(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            }

            UserId = userId;
        }
    }
}