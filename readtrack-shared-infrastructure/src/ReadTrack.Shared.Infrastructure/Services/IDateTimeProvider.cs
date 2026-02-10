using System;

namespace ReadTrack.Shared.Infrastructure.Services
{
    /// <summary>
    /// Abstracts system time operations to facilitate unit testing and time-travel debugging.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the current date and time in Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Gets the current date and time in the local time zone.
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// Gets the current date (without time component) in UTC.
        /// </summary>
        DateTime Today { get; }
    }
}