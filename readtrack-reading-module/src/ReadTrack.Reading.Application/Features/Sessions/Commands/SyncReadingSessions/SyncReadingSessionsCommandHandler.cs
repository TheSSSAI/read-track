using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;
using ReadTrack.Shared;

namespace ReadTrack.Reading.Application.Features.Sessions.Commands.SyncReadingSessions;

/// <summary>
/// Handles batch synchronization of reading sessions created offline.
/// </summary>
public class SyncReadingSessionsCommandHandler : IRequestHandler<SyncReadingSessionsCommand, Result<int>>
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ILogger<SyncReadingSessionsCommandHandler> _logger;

    public SyncReadingSessionsCommandHandler(
        ILibraryRepository libraryRepository,
        ILogger<SyncReadingSessionsCommandHandler> logger)
    {
        _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<int>> Handle(SyncReadingSessionsCommand request, CancellationToken cancellationToken)
    {
        int successCount = 0;
        
        try
        {
            _logger.LogInformation("Syncing {Count} sessions for user {UserId}", request.Sessions.Count, request.UserId);

            // Optimization: We could fetch all relevant library items in one go if the repository supports specific bulk fetches,
            // but loop processing is safer for ensuring domain invariants per item.
            foreach (var sessionDto in request.Sessions)
            {
                try
                {
                    var libraryItem = await _libraryRepository.GetByIdAsync(sessionDto.LibraryItemId, cancellationToken);

                    if (libraryItem == null || libraryItem.UserId != request.UserId)
                    {
                        _logger.LogWarning("Skipping sync for session {SessionId}: Item not found or unauthorized", sessionDto.Id);
                        continue;
                    }

                    // Check idempotency: Assuming the ReadingSession entity has a client-generated ID or we check timestamps.
                    // If the session ID already exists in the item's history, skip.
                    if (libraryItem.Sessions.Any(s => s.Id == sessionDto.Id))
                    {
                        _logger.LogDebug("Session {SessionId} already exists, skipping", sessionDto.Id);
                        continue;
                    }

                    var session = ReadingSession.Create(
                        sessionDto.StartTime,
                        sessionDto.EndTime,
                        sessionDto.PagesRead,
                        sessionDto.IsReread,
                        sessionDto.Id // Using client-provided ID for consistency
                    );

                    libraryItem.LogSession(session);
                    _libraryRepository.Update(libraryItem);
                    successCount++;
                }
                catch (Exception itemEx)
                {
                    // We don't want one failure to fail the whole batch
                    _logger.LogError(itemEx, "Failed to sync session {SessionId}", sessionDto.Id);
                }
            }

            if (successCount > 0)
            {
                await _libraryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            _logger.LogInformation("Successfully synced {Count} sessions", successCount);
            return Result<int>.Success(successCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Critical error during session sync");
            return Result<int>.Failure("Synchronization failed.");
        }
    }
}