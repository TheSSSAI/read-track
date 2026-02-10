using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;
using ReadTrack.Shared;

namespace ReadTrack.Reading.Application.Features.Sessions.Commands.LogReadingSession;

/// <summary>
/// Handles logging a single reading session for a library item.
/// </summary>
public class LogReadingSessionCommandHandler : IRequestHandler<LogReadingSessionCommand, Result<Guid>>
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ILogger<LogReadingSessionCommandHandler> _logger;

    public LogReadingSessionCommandHandler(
        ILibraryRepository libraryRepository,
        ILogger<LogReadingSessionCommandHandler> logger)
    {
        _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Guid>> Handle(LogReadingSessionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Logging session for item {ItemId} user {UserId}", request.LibraryItemId, request.UserId);

            // 1. Fetch Aggregate
            // We need to fetch the LibraryItem to ensure the session is valid within the context of the book 
            // (e.g., pages read don't exceed total pages).
            var libraryItem = await _libraryRepository.GetByIdAsync(request.LibraryItemId, cancellationToken);

            if (libraryItem == null)
            {
                return Result<Guid>.Failure($"Library item {request.LibraryItemId} not found.");
            }

            if (libraryItem.UserId != request.UserId)
            {
                return Result<Guid>.Failure("Unauthorized access to library item.");
            }

            // 2. Create Value Object / Entity
            // Assuming ReadingSession is an entity or value object created via a factory method
            var session = ReadingSession.Create(
                request.StartTime,
                request.EndTime,
                request.PagesRead,
                request.IsReread
            );

            // 3. Execute Domain Logic
            // The aggregate handles adding the session and updating the total progress of the book.
            libraryItem.LogSession(session);

            // 4. Persist
            // Even though session is added to the aggregate's collection, we might need to explicitly update
            // if using certain EF configurations, but typically tracking handles it.
            _libraryRepository.Update(libraryItem);
            await _libraryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Session {SessionId} logged successfully", session.Id);

            return Result<Guid>.Success(session.Id);
        }
        catch (DomainException dex)
        {
            _logger.LogWarning(dex, "Domain validation failed logging session");
            return Result<Guid>.Failure(dex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error logging session");
            return Result<Guid>.Failure("An error occurred while logging the reading session.");
        }
    }
}