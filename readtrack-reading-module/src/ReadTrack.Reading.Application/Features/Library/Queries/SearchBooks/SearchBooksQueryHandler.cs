using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;
using ReadTrack.Shared;

namespace ReadTrack.Reading.Application.Features.Library.Queries.SearchBooks;

/// <summary>
/// Handles searching for books via the external Google Books provider.
/// </summary>
public class SearchBooksQueryHandler : IRequestHandler<SearchBooksQuery, Result<List<BookMetadata>>>
{
    private readonly IGoogleBooksClient _googleBooksClient;
    private readonly ILogger<SearchBooksQueryHandler> _logger;

    public SearchBooksQueryHandler(
        IGoogleBooksClient googleBooksClient,
        ILogger<SearchBooksQueryHandler> logger)
    {
        _googleBooksClient = googleBooksClient ?? throw new ArgumentNullException(nameof(googleBooksClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<List<BookMetadata>>> Handle(SearchBooksQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
        {
            return Result<List<BookMetadata>>.Failure("Search query cannot be empty.");
        }

        try
        {
            _logger.LogDebug("Searching external book provider for query: {Query}", request.Query);

            var results = await _googleBooksClient.SearchBooksAsync(request.Query, cancellationToken);

            return Result<List<BookMetadata>>.Success(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "External book search failed for query: {Query}", request.Query);
            // We return a failure but allow the UI to handle it gracefully (e.g. show "Service Unavailable")
            return Result<List<BookMetadata>>.Failure("External search service is currently unavailable.");
        }
    }
}