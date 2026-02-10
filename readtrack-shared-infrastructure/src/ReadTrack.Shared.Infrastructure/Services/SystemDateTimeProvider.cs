using System;
using ReadTrack.Shared.Infrastructure.Services; // References Level 1 abstraction

namespace ReadTrack.Shared.Infrastructure.Services
{
    /// <summary>
    /// Provides the actual system date and time.
    /// Used in production to wrap DateTime static access for testability.
    /// </summary>
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Gets the current local date and time.
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets the current date component (time set to midnight).
        /// </summary>
        public DateTime Today => DateTime.Today;
    }
}