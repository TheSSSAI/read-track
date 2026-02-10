using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Interfaces
{
    /// <summary>
    /// Abstraction for the external Google Books API service.
    /// Implementations handle HTTP calls, resilience (Polly), and mapping to Domain objects.
    /// </summary>
    public interface IGoogleBooksClient
    {
        /// <summary>
        /// Searches for books matching the query string.
        /// </summary>
        /// <param name="query">Search term.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of book metadata.</returns>
        Task<List<BookMetadata>> SearchBooksAsync(string query, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves specific book details by Google Book ID.
        /// </summary>
        /// <param name="googleBookId">The specific volume ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Book metadata or null if not found.</returns>
        Task<BookMetadata?> GetBookDetailsAsync(string googleBookId, CancellationToken cancellationToken = default);
    }
}