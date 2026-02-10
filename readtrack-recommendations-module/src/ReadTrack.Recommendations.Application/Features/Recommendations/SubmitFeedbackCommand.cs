using System;
using MediatR;

namespace ReadTrack.Recommendations.Application.Features.Recommendations
{
    /// <summary>
    /// CQRS Command to submit user feedback on a specific recommendation.
    /// This feedback is used to refine future recommendations via the RAG pipeline.
    /// </summary>
    /// <remarks>
    /// Handler Logic:
    /// 1. Verifies the recommendation belongs to the user.
    /// 2. Creates/Updates a Feedback entity.
    /// 3. Asynchronously updates the user's preference embedding.
    /// </remarks>
    public record SubmitFeedbackCommand : IRequest<bool>
    {
        /// <summary>
        /// The ID of the specific recommendation being rated.
        /// </summary>
        public Guid RecommendationId { get; }

        /// <summary>
        /// The ID of the user providing feedback.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// The type of feedback provided (e.g., "Positive", "Negative", "Dismissed").
        /// Using string to allow flexibility, validated against allowed values in handler.
        /// </summary>
        public string FeedbackType { get; }

        /// <summary>
        /// Optional comments provided by the user.
        /// </summary>
        public string? Comments { get; }

        /// <summary>
        /// Creates a new instance of SubmitFeedbackCommand.
        /// </summary>
        /// <param name="recommendationId">The ID of the recommendation.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="feedbackType">The type of feedback ("Positive", "Negative", "Dismissed").</param>
        /// <param name="comments">Optional comments.</param>
        public SubmitFeedbackCommand(Guid recommendationId, Guid userId, string feedbackType, string? comments = null)
        {
            if (recommendationId == Guid.Empty)
                throw new ArgumentException("RecommendationId cannot be empty.", nameof(recommendationId));
            
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            if (string.IsNullOrWhiteSpace(feedbackType))
                throw new ArgumentException("FeedbackType cannot be empty.", nameof(feedbackType));

            RecommendationId = recommendationId;
            UserId = userId;
            FeedbackType = feedbackType;
            Comments = comments;
        }
    }
}