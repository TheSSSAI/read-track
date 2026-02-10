using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Shared.Infrastructure.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a read-only repository used to query entities.
    /// This interface separates read operations to support CQRS patterns and optimization strategies (e.g., AsNoTracking).
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IReadRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of entities based on the provided specification.
        /// </summary>
        /// <param name="specification">The specification criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of entities matching the specification.</returns>
        Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single entity based on the provided specification.
        /// </summary>
        /// <param name="specification">The specification criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the count of entities based on the provided specification.
        /// </summary>
        /// <param name="specification">The specification criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The count of entities matching the specification.</returns>
        Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if any entities exist based on the provided specification.
        /// </summary>
        /// <param name="specification">The specification criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>True if any entities match; otherwise, false.</returns>
        Task<bool> AnyAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    }
}