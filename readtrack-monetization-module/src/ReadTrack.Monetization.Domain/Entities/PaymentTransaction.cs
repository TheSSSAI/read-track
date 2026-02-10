using System;

namespace ReadTrack.Monetization.Domain.Entities
{
    /// <summary>
    /// Represents an immutable record of a financial transaction related to a subscription.
    /// </summary>
    public class PaymentTransaction
    {
        // EF Core Constructor
        protected PaymentTransaction() { }

        public PaymentTransaction(
            Guid id,
            Guid subscriptionId,
            string externalTransactionId,
            decimal amount,
            string currency,
            string type,
            DateTimeOffset processedAt)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID cannot be empty.", nameof(id));
            if (subscriptionId == Guid.Empty) throw new ArgumentException("Subscription ID cannot be empty.", nameof(subscriptionId));
            if (string.IsNullOrWhiteSpace(externalTransactionId)) throw new ArgumentNullException(nameof(externalTransactionId));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentNullException(nameof(currency));
            if (string.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type));

            Id = id;
            SubscriptionId = subscriptionId;
            ExternalTransactionId = externalTransactionId;
            Amount = amount;
            Currency = currency;
            Type = type; // e.g., "Purchase", "Renewal", "Refund"
            ProcessedAt = processedAt;
        }

        public Guid Id { get; private set; }
        public Guid SubscriptionId { get; private set; }
        public string ExternalTransactionId { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        public string Type { get; private set; } = string.Empty;
        public DateTimeOffset ProcessedAt { get; private set; }

        // Navigation Property linkage would be configured in Infrastructure layer (Level 2)
    }
}