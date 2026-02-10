using System;
using System.Collections.Generic;
using MediatR;

namespace ReadTrack.Reading.Application.Features.Sessions.Commands.SyncReadingSessions
{
    /// <summary>
    /// Command to batch sync reading sessions recorded offline on the mobile client.
    /// </summary>
    public record SyncReadingSessionsCommand(
        Guid UserId,
        List<SyncSessionDto> Sessions) : IRequest<SyncResultDto>;

    public record SyncSessionDto(
        Guid LibraryItemId,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime,
        int PagesRead,
        string? Notes,
        Guid ClientSessionId // Used for idempotency/deduplication
    );

    public record SyncResultDto(
        int SyncedCount,
        int FailedCount,
        List<Guid> FailedSessionIds
    );
}