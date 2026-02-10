using System;
using MediatR;

namespace ReadTrack.Reading.Application.Features.Sessions.Commands.LogReadingSession
{
    /// <summary>
    /// Command to log a reading session for a specific book.
    /// </summary>
    /// <param name="UserId">The user ID logging the session.</param>
    /// <param name="LibraryItemId">The book being read.</param>
    /// <param name="Date">When the reading happened.</param>
    /// <param name="Duration">How long the reading session lasted.</param>
    /// <param name="PagesRead">Number of pages read.</param>
    /// <param name="Notes">Optional notes about the session.</param>
    public record LogReadingSessionCommand(
        Guid UserId,
        Guid LibraryItemId,
        DateTimeOffset Date,
        TimeSpan Duration,
        int PagesRead,
        string? Notes = null) : IRequest<Guid>;
}