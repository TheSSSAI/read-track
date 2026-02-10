using ReadTrack.Shared.Infrastructure.Abstractions.Persistence;

namespace ReadTrack.Shared.Infrastructure.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a complete repository that handles both read and write operations 
    /// for a specific entity type. Inherits read capabilities from <see cref="IReadRepository{T}"/>.
    /// </summary>
    /// <typeparam name="T">The entity type, which must be a class.</typeparam>
    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        /// <summary>
        /// Adds a new entity to the repository context.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The added entity.</returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks an existing entity as updated in the repository context.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks an entity for deletion from the repository context.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}