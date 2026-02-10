using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Shared.Infrastructure.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for the Unit of Work pattern to manage database transactions.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits all changes made in the current context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException">Thrown when a concurrency violation is encountered.</exception>
        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Thrown when an error is encountered while sending updates to the database.</exception>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}