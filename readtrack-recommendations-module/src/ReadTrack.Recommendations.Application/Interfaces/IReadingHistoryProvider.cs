using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Recommendations.Application.Interfaces
{
    /// <summary>
    /// Represents a simplified data structure for reading history used in recommendation context.
    /// </summary>
    public record ReadingHistoryItem(Guid BookId, string Title, string Author, string Genre, int Rating, DateTime DateFinished);

    /// <summary>
    /// Interface for retrieving user reading history from the core Reading Module.
    /// Acts as an anti-corruption layer or port to the external module/service.
    /// </summary>
    public interface IReadingHistoryProvider
    {
        /// <summary>
        /// Fetches the recent reading history for a user to build context for recommendations.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="limit">The maximum number of history items to retrieve.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of reading history items.</returns>
        Task<IEnumerable<ReadingHistoryItem>> GetUserReadingHistoryAsync(Guid userId, int limit, CancellationToken cancellationToken);
    }
}