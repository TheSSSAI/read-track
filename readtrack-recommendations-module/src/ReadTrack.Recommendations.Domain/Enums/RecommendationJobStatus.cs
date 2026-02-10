using System;

namespace ReadTrack.Recommendations.Domain.Enums
{
    /// <summary>
    /// Represents the lifecycle states of an asynchronous recommendation generation job.
    /// This state machine drives the polling mechanism for the client UI.
    /// </summary>
    public enum RecommendationJobStatus
    {
        /// <summary>
        /// The job has been created and queued but processing has not started.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// The job is currently being processed by the RAG pipeline (Context retrieval, LLM inference).
        /// </summary>
        Processing = 1,

        /// <summary>
        /// The job completed successfully and recommendations are available.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// The job failed due to an internal error or external API failure.
        /// </summary>
        Failed = 3
    }
}