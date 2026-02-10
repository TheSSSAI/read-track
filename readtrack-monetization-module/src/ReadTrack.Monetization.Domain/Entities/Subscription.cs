using System;
using System.Collections.Generic;
using System.Linq;
using ReadTrack.Monetization.Domain.Entities;
using ReadTrack.Monetization.Domain.Events;
using ReadTrack.Monetization.Domain.Exceptions;
using ReadTrack.Monetization.Domain.ValueObjects;

namespace ReadTrack.Monetization.Domain.Entities
{
    /// <summary>
    /// Represents the Aggregate Root for a user's subscription.
    /// Manages the lifecycle of the subscription, including renewals, cancellations, and tier changes.
    /// </summary>
    public class Subscription
    {
        private readonly List<PaymentTransaction> _transactions = new();
        private readonly List<object> _domainEvents = new();

        // EF Core Constructor
        protected Subscription() { }

        /// <summary>
        /// Creates a new subscription.
        /// </summary>
        public Subscription(Guid id, Guid userId, string provider, string externalSubscriptionId, SubscriptionTier tier)
        {
            if (id == Guid.Empty) throw new ArgumentException("Subscription ID cannot be empty.", nameof(id));
            if (userId == Guid.Empty) throw new ArgumentException("User ID cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(provider)) throw new ArgumentNullException(nameof(provider));
            if (string.IsNullOrWhiteSpace(externalSubscriptionId)) throw new ArgumentNullException(nameof(externalSubscriptionId));

            Id = id;
            UserId = userId;
            Provider = provider;
            ExternalSubscriptionId = externalSubscriptionId;
            Tier = tier;
            Status = SubscriptionStatus.Active; // Default to active upon creation, assuming immediate payment processing
            CreatedAt = DateTimeOffset.UtcNow;
            CurrentPeriodStart = DateTimeOffset.UtcNow;
            // Default period end to be updated by initial transaction or explicit logic
            CurrentPeriodEnd = DateTimeOffset.UtcNow; 
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Provider { get; private set; } = string.Empty;
        public string ExternalSubscriptionId { get; private set; } = string.Empty;
        
        public SubscriptionStatus Status { get; private set; }
        public SubscriptionTier Tier { get; private set; }
        
        public DateTimeOffset CurrentPeriodStart { get; private set; }
        public DateTimeOffset CurrentPeriodEnd { get; private set; }
        public DateTimeOffset? CancelledAt { get; private set; }
        
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? LastModifiedAt { get; private set; }

        // Navigation Property
        public IReadOnlyCollection<PaymentTransaction> Transactions => _transactions.AsReadOnly();
        
        // Domain Events Accessor
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Renews the subscription for a new period.
        /// </summary>
        /// <param name="newPeriodEnd">The expiration date of the new period.</param>
        /// <param name="transactionId">The external transaction ID associated with the renewal.</param>
        public void Renew(DateTimeOffset newPeriodEnd, string transactionId)
        {
            if (newPeriodEnd <= CurrentPeriodEnd)
            {
                // In some cases (corrections), this might happen, but generally renewal implies extension.
                // We will allow it but log/warn in logic layers if needed. 
                // For domain purity, we ensure we aren't regressing significantly without reason.
            }

            CurrentPeriodStart = CurrentPeriodEnd; // Logic assumes contiguous renewal
            CurrentPeriodEnd = newPeriodEnd;
            Status = SubscriptionStatus.Active;
            LastModifiedAt = DateTimeOffset.UtcNow;

            // Add domain event
            _domainEvents.Add(new SubscriptionRenewedEvent(UserId, newPeriodEnd, Tier));
        }

        /// <summary>
        /// Terminates the subscription immediately or schedules it for expiration.
        /// </summary>
        /// <param name="cancellationDate">When the user cancelled.</param>
        public void Cancel(DateTimeOffset cancellationDate)
        {
            CancelledAt = cancellationDate;
            LastModifiedAt = DateTimeOffset.UtcNow;
            
            // Logic: If cancelled, it might still be active until CurrentPeriodEnd.
            // We do not change Status to Expired yet; that happens when time passes CurrentPeriodEnd.
            // However, we might mark it as 'Cancelled' status if the business rule treats "Cancelled" as "Active but will not renew".
            // Assuming SubscriptionStatus has a Cancelled state for "Non-renewing".
            if (Status == SubscriptionStatus.Active)
            {
                Status = SubscriptionStatus.Cancelled; 
            }
        }

        /// <summary>
        /// Marks the subscription as expired.
        /// </summary>
        public void Expire()
        {
            Status = SubscriptionStatus.Expired;
            Tier = SubscriptionTier.Free; // Revert to free tier
            LastModifiedAt = DateTimeOffset.UtcNow;

            _domainEvents.Add(new SubscriptionTerminatedEvent(UserId, DateTimeOffset.UtcNow));
        }

        /// <summary>
        /// Records a payment transaction for this subscription.
        /// </summary>
        public void AddTransaction(PaymentTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            _transactions.Add(transaction);
        }

        /// <summary>
        /// Updates the subscription tier (Upgrade/Downgrade).
        /// </summary>
        public void UpdateTier(SubscriptionTier newTier)
        {
            if (Tier == newTier) return;
            
            Tier = newTier;
            LastModifiedAt = DateTimeOffset.UtcNow;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}