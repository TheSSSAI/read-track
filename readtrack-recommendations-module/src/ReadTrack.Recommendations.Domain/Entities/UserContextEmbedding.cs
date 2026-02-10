using System;

namespace ReadTrack.Recommendations.Domain.Entities
{
    /// <summary>
    /// Represents a vector embedding of user data (reading history, goals) used for semantic similarity search.
    /// </summary>
    public class UserContextEmbedding
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        
        /// <summary>
        /// The high-dimensional vector representation of the source text.
        /// </summary>
        public float[] EmbeddingVector { get; private set; }
        
        /// <summary>
        /// The raw text that was embedded.
        /// </summary>
        public string SourceText { get; private set; }
        
        /// <summary>
        /// The type of data source (e.g., "ReadingHistory", "Goal", "ExplicitInterest").
        /// </summary>
        public string SourceType { get; private set; }
        
        /// <summary>
        /// The ID of the source entity (e.g., the BookId or GoalId).
        /// </summary>
        public Guid SourceId { get; private set; }
        
        public DateTime CreatedAt { get; private set; }

        private UserContextEmbedding() { } // For EF Core

        public UserContextEmbedding(Guid userId, float[] embeddingVector, string sourceText, string sourceType, Guid sourceId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (embeddingVector == null || embeddingVector.Length == 0) throw new ArgumentException("EmbeddingVector cannot be empty.", nameof(embeddingVector));
            if (string.IsNullOrWhiteSpace(sourceText)) throw new ArgumentNullException(nameof(sourceText));
            if (string.IsNullOrWhiteSpace(sourceType)) throw new ArgumentNullException(nameof(sourceType));

            Id = Guid.NewGuid();
            UserId = userId;
            EmbeddingVector = embeddingVector;
            SourceText = sourceText;
            SourceType = sourceType;
            SourceId = sourceId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}