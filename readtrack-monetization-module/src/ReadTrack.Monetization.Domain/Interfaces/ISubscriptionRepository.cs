using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReadTrack.Monetization.Domain.Entities;

namespace ReadTrack.Monetization.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for subscription data persistence.
    /// </summary>
    public interface ISubscriptionRepository
    {
        /// <summary>
        /// Retrieves a subscription by its unique ID.
        /// </summary>
        Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a subscription associated with a specific user.
        /// </summary>
        Task<Subscription?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a subscription by the provider's external subscription ID.
        /// Used primarily for webhook correlation.
        /// </summary>
        Task<Subscription?> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if a transaction ID has already been processed to ensure idempotency.
        /// </summary>
        Task<bool> HasTransactionBeenProcessedAsync(string externalTransactionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a list of subscriptions that have expired before the given date and are still marked as active.
        /// </summary>
        Task<List<Subscription>> GetExpiredSubscriptionsAsync(DateTimeOffset referenceDate, int batchSize, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists a new subscription.
        /// </summary>
        Task AddAsync(Subscription subscription, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing subscription.
        /// </summary>
        Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken = default);

        /// <summary>
        /// Persists the changes made in the unit of work.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}