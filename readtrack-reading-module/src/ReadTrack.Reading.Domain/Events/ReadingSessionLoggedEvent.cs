using System;

namespace ReadTrack.Reading.Domain.Events;

/// <summary>
/// Domain Event raised when a user successfully logs a reading session.
/// This event triggers side effects such as updating statistics, goal progress, and streaks.
/// </summary>
/// <param name="SessionId">The unique identifier of the created session.</param>
/// <param name="UserId">The user who logged the session.</param>
/// <param name="LibraryItemId">The book/item associated with the session.</param>
/// <param name="Duration">The time spent reading.</param>
/// <param name="PagesRead">The number of pages read in this session.</param>
/// <param name="OccurredOn">The timestamp when the session occurred.</param>
public sealed record ReadingSessionLoggedEvent(
    Guid SessionId,
    Guid UserId,
    Guid LibraryItemId,
    TimeSpan Duration,
    int PagesRead,
    DateTimeOffset OccurredOn
);