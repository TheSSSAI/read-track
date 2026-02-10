using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReadTrack.Shared.Infrastructure.Abstractions.Persistence
{
    /// <summary>
    /// Encapsulates query logic to decouple repositories from specific filtering requirements.
    /// Implements the Specification Pattern.
    /// </summary>
    /// <typeparam name="T">The entity type this specification applies to.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the filtering criteria expression (Where clause).
        /// </summary>
        Expression<Func<T, bool>>? Criteria { get; }

        /// <summary>
        /// Gets the list of navigation properties to include (eager load).
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Gets the list of navigation properties to include using string names (for ThenInclude scenarios).
        /// </summary>
        List<string> IncludeStrings { get; }

        /// <summary>
        /// Gets the primary ordering expression (OrderBy).
        /// </summary>
        Expression<Func<T, object>>? OrderBy { get; }

        /// <summary>
        /// Gets the primary descending ordering expression (OrderByDescending).
        /// </summary>
        Expression<Func<T, object>>? OrderByDescending { get; }

        /// <summary>
        /// Gets the secondary ordering expression (ThenBy).
        /// </summary>
        Expression<Func<T, object>>? GroupBy { get; }

        /// <summary>
        /// Gets the number of records to take (paging).
        /// </summary>
        int Take { get; }

        /// <summary>
        /// Gets the number of records to skip (paging).
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// Gets a value indicating whether paging is enabled for this specification.
        /// </summary>
        bool IsPagingEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether to apply distinct filtering.
        /// </summary>
        bool IsDistinct { get; }

        /// <summary>
        /// Gets a value indicating whether query results should be cached in memory (AsNoTracking).
        /// </summary>
        bool AsNoTracking { get; }
    }
}