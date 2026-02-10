using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;
using ReadTrack.Shared;

namespace ReadTrack.Reading.Application.Features.Library.Queries.GetUserLibrary;

/// <summary>
/// Handles retrieving the full library for a specific user.
/// </summary>
public class GetUserLibraryQueryHandler : IRequestHandler<GetUserLibraryQuery, Result<List<LibraryItem>>>
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly ILogger<GetUserLibraryQueryHandler> _logger;

    public GetUserLibraryQueryHandler(
        ILibraryRepository libraryRepository,
        ILogger<GetUserLibraryQueryHandler> logger)
    {
        _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<List<LibraryItem>>> Handle(GetUserLibraryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Performance Note: For very large libraries, pagination should be considered.
            // Given the constraints (e.g. 20 for free users), fetching all is acceptable for MVP but should be monitored.
            var items = await _libraryRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            return Result<List<LibraryItem>>.Success(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving library for user {UserId}", request.UserId);
            return Result<List<LibraryItem>>.Failure("Failed to retrieve user library.");
        }
    }
}