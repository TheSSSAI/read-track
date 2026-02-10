using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;
using ReadTrack.Shared; // Assumed Shared Kernel for Result pattern

namespace ReadTrack.Reading.Application.Features.Library.Commands.AddBookToLibrary;

/// <summary>
/// Handles the business logic for adding a book to a user's library.
/// Enforces subscription limits for Free Tier users.
/// </summary>
public class AddBookToLibraryCommandHandler : IRequestHandler<AddBookToLibraryCommand, Result<Guid>>
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly IGoogleBooksClient _googleBooksClient;
    private readonly ISubscriptionService _subscriptionService;
    private readonly ILogger<AddBookToLibraryCommandHandler> _logger;

    public AddBookToLibraryCommandHandler(
        ILibraryRepository libraryRepository,
        IGoogleBooksClient googleBooksClient,
        ISubscriptionService subscriptionService,
        ILogger<AddBookToLibraryCommandHandler> logger)
    {
        _libraryRepository = libraryRepository ?? throw new ArgumentNullException(nameof(libraryRepository));
        _googleBooksClient = googleBooksClient ?? throw new ArgumentNullException(nameof(googleBooksClient));
        _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<Guid>> Handle(AddBookToLibraryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Attempting to add book {GoogleBookId} for user {UserId}", request.GoogleBookId, request.UserId);

            // 1. Enforce Subscription Limits
            var subscriptionStatus = await _subscriptionService.GetUserSubscriptionStatusAsync(request.UserId, cancellationToken);
            
            if (subscriptionStatus == null)
            {
                _logger.LogError("Failed to retrieve subscription status for user {UserId}", request.UserId);
                return Result<Guid>.Failure("Could not verify subscription status.");
            }

            if (subscriptionStatus.IsFreeTier)
            {
                var currentBookCount = await _libraryRepository.CountByUserIdAsync(request.UserId, cancellationToken);
                const int FreeTierLimit = 20;

                if (currentBookCount >= FreeTierLimit)
                {
                    _logger.LogWarning("User {UserId} reached free tier limit of {Limit}", request.UserId, FreeTierLimit);
                    return Result<Guid>.Failure("Library limit reached. Please upgrade to Premium to add more books.");
                }
            }

            // 2. Fetch Book Metadata
            // We assume the command provides the GoogleBookId, but we need the full metadata to create the domain entity.
            // If the command already included metadata, we could skip this, but typically we fetch authoritatively or pass it in.
            // Assuming the client fetches the BookMetadata via Search, passing it entirely in command is better, 
            // but relying on ID ensures data consistency. Let's fetch using the client.
            var bookMetadata = await _googleBooksClient.GetBookDetailsAsync(request.GoogleBookId, cancellationToken);

            if (bookMetadata == null)
            {
                return Result<Guid>.Failure($"Book with ID {request.GoogleBookId} not found.");
            }

            // 3. Check for Duplicates
            // Domain rule: A user shouldn't add the same book twice? 
            // Often handled by unique index on UserID + GoogleBookId, but good to check here.
            var existingBook = await _libraryRepository.GetByGoogleBookIdAsync(request.UserId, request.GoogleBookId, cancellationToken);
            if (existingBook != null)
            {
                return Result<Guid>.Failure("This book is already in your library.");
            }

            // 4. Create Domain Entity
            var libraryItem = LibraryItem.Create(
                request.UserId,
                bookMetadata,
                request.InitialShelf
            );

            // 5. Persist
            await _libraryRepository.AddAsync(libraryItem, cancellationToken);
            await _libraryRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfully added book {BookId} to library for user {UserId}", libraryItem.Id, request.UserId);

            return Result<Guid>.Success(libraryItem.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding book to library for user {UserId}", request.UserId);
            return Result<Guid>.Failure("An unexpected error occurred while adding the book.");
        }
    }
}