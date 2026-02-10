using System;

namespace ReadTrack.Reading.Domain.Aggregates.Library
{
    /// <summary>
    /// Represents a single period of time spent reading a specific library item.
    /// Tracks duration, pages read, and the timeframe of the activity.
    /// </summary>
    public class ReadingSession
    {
        public Guid Id { get; private set; }
        public Guid LibraryItemId { get; private set; }
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset? EndTime { get; private set; }
        public TimeSpan? Duration { get; private set; }
        public int PagesRead { get; private set; }
        public string? Notes { get; private set; }
        public bool IsManualEntry { get; private set; }

        // Private constructor for ORM
        private ReadingSession() { }

        private ReadingSession(
            Guid libraryItemId,
            DateTimeOffset startTime,
            TimeSpan? duration,
            int pagesRead,
            string? notes,
            bool isManualEntry,
            DateTimeOffset? endTime)
        {
            Id = Guid.NewGuid();
            LibraryItemId = libraryItemId;
            StartTime = startTime;
            Duration = duration;
            PagesRead = pagesRead;
            Notes = notes;
            IsManualEntry = isManualEntry;
            EndTime = endTime;

            Validate();
        }

        /// <summary>
        /// Creates a new reading session manually entered by the user (e.g., "I read 20 pages yesterday").
        /// </summary>
        public static ReadingSession CreateManual(
            Guid libraryItemId,
            DateTimeOffset date,
            TimeSpan duration,
            int pagesRead,
            string? notes = null)
        {
            return new ReadingSession(
                libraryItemId,
                date,
                duration,
                pagesRead,
                notes,
                isManualEntry: true,
                endTime: date.Add(duration));
        }

        /// <summary>
        /// Creates a reading session tracked via a timer (Start/Stop).
        /// </summary>
        public static ReadingSession CreateTimed(
            Guid libraryItemId,
            DateTimeOffset startTime,
            DateTimeOffset endTime,
            int pagesRead,
            string? notes = null)
        {
            var duration = endTime - startTime;
            return new ReadingSession(
                libraryItemId,
                startTime,
                duration,
                pagesRead,
                notes,
                isManualEntry: false,
                endTime: endTime);
        }

        private void Validate()
        {
            if (LibraryItemId == Guid.Empty)
                throw new ArgumentException("Library Item ID is required.", nameof(LibraryItemId));

            if (PagesRead < 0)
                throw new ArgumentException("Pages read cannot be negative.", nameof(PagesRead));

            if (Duration.HasValue && Duration.Value.TotalMinutes < 0)
                throw new ArgumentException("Duration cannot be negative.", nameof(Duration));

            if (EndTime.HasValue && EndTime < StartTime)
                throw new ArgumentException("End time cannot be before start time.", nameof(EndTime));
        }
    }
}