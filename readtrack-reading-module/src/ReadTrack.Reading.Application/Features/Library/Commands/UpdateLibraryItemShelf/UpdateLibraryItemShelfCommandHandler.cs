using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Shared;

namespace ReadTrack.Reading.Application.Features.Library.Commands.UpdateLibraryItemShelf;

/// <summary>
/// Handles moving a book between shelves (e.g., WantToRead -> CurrentlyReading).
/// </summary>
public class UpdateLibraryItemShelfCommandHandler : IRequestHandler<UpdateLibraryItemShelfCommand, Result<bool>>
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ILogger<UpdateLibraryItemShelfCommandHandler> _logger;

    public UpdateLibraryItemShelfCommandHandler(
        ILibraryRepository libraryRepository,
        ILogger<UpdateLibraryItemShelfCommandHandler> logger)
    {
        _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<bool>> Handle(UpdateLibraryItemShelfCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Updating shelf for item {LibraryItemId} to {NewShelf} for user {UserId}", request.LibraryItemId, request.NewShelf, request.UserId);

            // 1. Fetch Aggregate
            var libraryItem = await _libraryRepository.GetByIdAsync(request.LibraryItemId, cancellationToken);

            if (libraryItem == null)
            {
                return Result<bool>.Failure($"Library item {request.LibraryItemId} not found.");
            }

            // 2. Validate Ownership
            if (libraryItem.UserId != request.UserId)
            {
                _logger.LogWarning("User {UserId} attempted to modify item {ItemId} belonging to another user", request.UserId, request.LibraryItemId);
                return Result<bool>.Failure("Unauthorized access to library item.");
            }

            // 3. Execute Domain Logic
            // The MoveToShelf method on the aggregate handles domain events and state validation
            libraryItem.MoveToShelf(request.NewShelf);

            // 4. Persist
            _libraryRepository.Update(libraryItem);
            await _libraryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully updated shelf for item {LibraryItemId}", request.LibraryItemId);

            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update shelf for library item {ItemId}", request.LibraryItemId);
            return Result<bool>.Failure("An error occurred while updating the bookshelf.");
        }
    }
}