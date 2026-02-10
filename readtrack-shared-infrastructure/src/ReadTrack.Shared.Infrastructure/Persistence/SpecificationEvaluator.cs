using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReadTrack.Shared.Infrastructure.Abstractions.Persistence;

namespace ReadTrack.Shared.Infrastructure.Persistence
{
    /// <summary>
    /// Evaluates specifications and applies them to an IQueryable to generate the final EF Core query.
    /// This implementation supports filtering, eager loading (includes), ordering, and paging.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public static class SpecificationEvaluator<TEntity> where TEntity : class
    {
        /// <summary>
        /// Applies the specification's criteria, includes, sorting, and paging to the input query.
        /// </summary>
        /// <param name="inputQuery">The base queryable.</param>
        /// <param name="specification">The specification to apply.</param>
        /// <returns>The modified queryable with all rules applied.</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // Modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            // Includes all string-based includes
            query = specification.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));

            // Apply ordering if specified
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            return query;
        }
    }
}