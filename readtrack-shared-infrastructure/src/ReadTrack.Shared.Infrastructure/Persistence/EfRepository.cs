using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReadTrack.Shared.Infrastructure.Abstractions.Persistence;

namespace ReadTrack.Shared.Infrastructure.Persistence
{
    /// <summary>
    /// Generic implementation of the repository pattern using Entity Framework Core.
    /// Acts as both a read and write repository for Domain Entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity>, IReadRepository<TEntity> 
        where TEntity : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // FindAsync relies on the ChangeTracker if the entity is already loaded, 
            // which is efficient for Write scenarios where we fetch then update.
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            if (specification == null) throw new ArgumentNullException(nameof(specification));

            // For list operations via IReadRepository, we default to AsNoTracking for performance
            // as these are typically read-only query scenarios.
            var queryable = ApplySpecification(specification);
            
            return await queryable.ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity, cancellationToken);
            
            return entity;
        }

        /// <inheritdoc />
        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // If the entity is detached, we attach it and mark it as modified.
            // If it's already tracked, this might be redundant but safe.
            _dbContext.Entry(entity).State = EntityState.Modified;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Applies the specification to the default queryable set.
        /// </summary>
        /// <param name="specification">The specification to apply.</param>
        /// <returns>An IQueryable with the specification criteria applied.</returns>
        protected virtual IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            // We start with AsNoTracking for List/Read operations to adhere to REQ-PERF-001 optimizations.
            // If a specific use case requires tracking, it should likely use GetById or a specialized method.
            var query = _dbSet.AsNoTracking();
            
            return SpecificationEvaluator.GetQuery(query, specification);
        }
    }
}