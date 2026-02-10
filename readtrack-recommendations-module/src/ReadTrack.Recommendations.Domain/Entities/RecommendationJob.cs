using System;
using System.Collections.Generic;
using ReadTrack.Recommendations.Domain.Enums;

namespace ReadTrack.Recommendations.Domain.Entities
{
    /// <summary>
    /// Represents the lifecycle and state of an asynchronous recommendation generation request.
    /// </summary>
    public class RecommendationJob
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public RecommendationJobStatus Status { get; private set; }
        public string? FailureReason { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        private readonly List<Recommendation> _generatedRecommendations = new();
        public IReadOnlyCollection<Recommendation> GeneratedRecommendations => _generatedRecommendations.AsReadOnly();

        private RecommendationJob() { } // For EF Core

        public RecommendationJob(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            Id = Guid.NewGuid();
            UserId = userId;
            Status = RecommendationJobStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsProcessing()
        {
            if (Status != RecommendationJobStatus.Pending)
            {
                throw new InvalidOperationException($"Cannot transition to Processing from state {Status}.");
            }

            Status = RecommendationJobStatus.Processing;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Complete(List<Recommendation> recommendations)
        {
            if (Status != RecommendationJobStatus.Processing)
            {
                throw new InvalidOperationException($"Cannot transition to Completed from state {Status}.");
            }

            if (recommendations == null) throw new ArgumentNullException(nameof(recommendations));

            _generatedRecommendations.AddRange(recommendations);
            Status = RecommendationJobStatus.Completed;
            CompletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Fail(string reason)
        {
            if (Status == RecommendationJobStatus.Completed)
            {
                throw new InvalidOperationException("Cannot fail a job that is already completed.");
            }

            Status = RecommendationJobStatus.Failed;
            FailureReason = reason;
            UpdatedAt = DateTime.UtcNow;
            CompletedAt = DateTime.UtcNow; // Mark as finished even if failed
        }
    }
}