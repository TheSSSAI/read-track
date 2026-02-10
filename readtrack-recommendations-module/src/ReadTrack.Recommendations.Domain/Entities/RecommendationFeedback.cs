using System;

namespace ReadTrack.Recommendations.Domain.Entities
{
    /// <summary>
    /// Represents user feedback on a specific recommendation to improve future suggestions.
    /// </summary>
    public class RecommendationFeedback
    {
        public Guid Id { get; private set; }
        public Guid RecommendationId { get; private set; }
        public Guid UserId { get; private set; }
        public string FeedbackType { get; private set; } // "Like", "Dislike", "Dismiss"
        public string? Comments { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private RecommendationFeedback() { } // For EF Core

        public RecommendationFeedback(Guid recommendationId, Guid userId, string feedbackType, string? comments = null)
        {
            if (recommendationId == Guid.Empty) throw new ArgumentException("RecommendationId cannot be empty.", nameof(recommendationId));
            if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(feedbackType)) throw new ArgumentNullException(nameof(feedbackType));

            Id = Guid.NewGuid();
            RecommendationId = recommendationId;
            UserId = userId;
            FeedbackType = feedbackType;
            Comments = comments;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateComments(string newComments)
        {
            Comments = newComments;
        }
    }
}