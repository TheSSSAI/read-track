using System;
using ReadTrack.Users.Domain.Enums;

namespace ReadTrack.Users.Domain.Entities
{
    /// <summary>
    /// Represents a long-running background job for exporting user data.
    /// Manages the state lifecycle of the export request as per GDPR compliance.
    /// </summary>
    public class DataExportJob
    {
        public Guid Id { get; private set; }
        
        // Foreign Key
        public Guid UserId { get; private set; }
        
        // State
        public DataExportStatus Status { get; private set; }
        
        // Output location
        public string? S3Key { get; private set; }
        
        // Metadata
        public string? FailureReason { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public DateTime? ExpirationDate { get; private set; }

        // Navigation property
        public virtual User User { get; private set; } = null!;

        /// <summary>
        /// EF Core constructor
        /// </summary>
        protected DataExportJob() { }

        /// <summary>
        /// Private constructor for factory method.
        /// </summary>
        private DataExportJob(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Status = DataExportStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Creates a new data export job for a user.
        /// </summary>
        public static DataExportJob Initiate(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            return new DataExportJob(userId);
        }

        /// <summary>
        /// Transitions the job to the Processing state.
        /// </summary>
        public void MarkProcessing()
        {
            if (Status != DataExportStatus.Pending)
            {
                throw new InvalidOperationException($"Cannot transition to Processing from state {Status}.");
            }

            Status = DataExportStatus.Processing;
        }

        /// <summary>
        /// Completes the job successfully with the location of the exported file.
        /// </summary>
        /// <param name="s3Key">The storage key where the export file is located.</param>
        /// <param name="expiration">The date when this export file should be expired/deleted.</param>
        public void Complete(string s3Key, DateTime expiration)
        {
            if (Status != DataExportStatus.Processing)
            {
                throw new InvalidOperationException($"Cannot complete a job in state {Status}. Job must be Processing.");
            }

            if (string.IsNullOrWhiteSpace(s3Key)) throw new ArgumentNullException(nameof(s3Key));

            S3Key = s3Key;
            ExpirationDate = expiration;
            Status = DataExportStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks the job as failed with a reason.
        /// </summary>
        /// <param name="reason">The reason for the failure.</param>
        public void Fail(string reason)
        {
            // We allow failing from Pending or Processing
            if (Status == DataExportStatus.Completed || Status == DataExportStatus.Failed)
            {
                throw new InvalidOperationException($"Cannot fail a job that is already {Status}.");
            }

            FailureReason = reason ?? "Unknown error occurred.";
            Status = DataExportStatus.Failed;
            CompletedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Checks if the export file associated with this job has expired.
        /// </summary>
        public bool IsExpired()
        {
            return ExpirationDate.HasValue && DateTime.UtcNow > ExpirationDate.Value;
        }
    }
}