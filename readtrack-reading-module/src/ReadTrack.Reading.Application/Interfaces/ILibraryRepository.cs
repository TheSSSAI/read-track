using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for data access operations related to the Library aggregate.
    /// This repository handles the persistence and retrieval of LibraryItems and their associated ReadingSessions.
    /// </summary>
    public interface ILibraryRepository
    {
        /// <summary>
        /// Adds a new library item to the repository.
        /// </summary>
        /// <param name="libraryItem">The library item to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task AddAsync(LibraryItem libraryItem, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing library item in the repository.
        /// </summary>
        /// <param name="libraryItem">The library item to update.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(LibraryItem libraryItem, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a library item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the library item.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The library item if found; otherwise, null.</returns>
        Task<LibraryItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves all library items belonging to a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of library items belonging to the user.</returns>
        Task<List<LibraryItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a specific library item for a user based on the Google Book ID.
        /// This is primarily used to check if a book already exists in the user's library.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="googleBookId">The external Google Book ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The library item if found; otherwise, null.</returns>
        Task<LibraryItem?> GetByUserIdAndGoogleIdAsync(Guid userId, string googleBookId, CancellationToken cancellationToken);

        /// <summary>
        /// Counts the total number of library items for a specific user.
        /// Used for enforcing subscription limits (e.g., Free tier limit of 20 books).
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The count of items in the user's library.</returns>
        Task<int> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Persists all tracked changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}