using Microsoft.EntityFrameworkCore;
using ReadTrack.Shared.Infrastructure.Abstractions.Persistence;

namespace ReadTrack.Shared.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework Core implementation of the Unit of Work pattern.
    /// Manages the database context lifecycle and transaction commits.
    /// </summary>
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfUnitOfWork"/> class.
        /// </summary>
        /// <param name="dbContext">The Entity Framework Core database context.</param>
        /// <exception cref="ArgumentNullException">Thrown if dbContext is null.</exception>
        public EfUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Commits all changes made in the current context to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>The number of state entries written to the database.</returns>
        /// <exception cref="DbUpdateConcurrencyException">Thrown when a concurrency violation is encountered.</exception>
        /// <exception cref="DbUpdateException">Thrown when an error is encountered while saving to the database.</exception>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // In a more complex implementation, we might dispatch domain events here
            // either before or after the commit, depending on the consistency requirements.
            // For this baseline implementation, we delegate directly to the DbContext.
            
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}