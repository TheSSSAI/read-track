using System.Threading.Tasks;

namespace ReadTrack.Monetization.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for interacting with external payment providers (Apple, Google).
    /// </summary>
    public interface IPaymentProviderService
    {
        /// <summary>
        /// Gets the provider identifier (e.g., "Apple", "Google").
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Validates the cryptographic signature of the webhook payload.
        /// </summary>
        /// <param name="payload">The raw payload string.</param>
        /// <param name="signature">The signature string (if applicable/separate).</param>
        /// <returns>True if the signature is valid; otherwise, false.</returns>
        bool ValidateSignature(string payload, string? signature = null);

        /// <summary>
        /// Parses a raw webhook payload into a normalized domain-relevant structure.
        /// </summary>
        /// <param name="payload">The raw payload.</param>
        /// <returns>A normalized object containing transaction ID, subscription ID, expiry, etc.</returns>
        /// <remarks>
        /// In a real implementation, this might return a specific Result<T> or DTO. 
        /// For this level, we define the contract abstractly.
        /// </remarks>
        Task<ParsedWebhookData> ParseNotificationAsync(string payload);
    }

    /// <summary>
    /// Simple DTO to carry normalized webhook data across the boundary.
    /// </summary>
    public record ParsedWebhookData(
        string ExternalSubscriptionId,
        string ExternalTransactionId,
        string TransactionType,
        System.DateTimeOffset? ExpiresDate,
        string OriginalTransactionId
    );
}