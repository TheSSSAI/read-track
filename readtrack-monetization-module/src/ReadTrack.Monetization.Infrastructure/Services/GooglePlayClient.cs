using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ReadTrack.Monetization.Application.Interfaces;
using ReadTrack.Monetization.Domain.Exceptions;
using ReadTrack.Monetization.Domain.ValueObjects;
using ReadTrack.Monetization.Infrastructure.Configuration;

namespace ReadTrack.Monetization.Infrastructure.Services;

/// <summary>
/// Service for interacting with Google Play Developer API and validating Pub/Sub notifications.
/// Implements IPaymentProviderService for the Google provider.
/// </summary>
public class GooglePlayClient : IPaymentProviderService
{
    private readonly HttpClient _httpClient;
    private readonly GooglePlaySettings _settings;
    private readonly ILogger<GooglePlayClient> _logger;

    public Provider Provider => Provider.Google;

    public GooglePlayClient(
        HttpClient httpClient,
        IOptionsSnapshot<GooglePlaySettings> settings,
        ILogger<GooglePlayClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Validates the Google Play Pub/Sub message payload.
    /// Google notifications rely on the security of the Pub/Sub subscription configuration rather than payload signing (unlike Apple).
    /// However, we validate the structure and potentially check against the Google API using the purchase token.
    /// </summary>
    /// <param name="payload">The base64 encoded data from the Pub/Sub message.</param>
    /// <returns>True if the payload is structurally valid and decoded successfully.</returns>
    public async Task<bool> ValidateSignatureAsync(string payload)
    {
        if (string.IsNullOrWhiteSpace(payload))
        {
            _logger.LogWarning("Google payload is empty.");
            return false;
        }

        try
        {
            // Google Pub/Sub messages contain a 'data' field which is base64 encoded JSON.
            // Validation here is primarily ensuring it's valid Base64 and contains expected JSON structure.
            // Security is enforced by the Pub/Sub IAM roles.
            
            byte[] data = Convert.FromBase64String(payload);
            string decodedString = Encoding.UTF8.GetString(data);
            
            using var doc = JsonDocument.Parse(decodedString);
            var root = doc.RootElement;

            // Basic structural check
            if (!root.TryGetProperty("packageName", out _) || 
                !root.TryGetProperty("subscriptionNotification", out _))
            {
                _logger.LogWarning("Google payload missing required properties.");
                return false;
            }

            return await Task.FromResult(true);
        }
        catch (FormatException)
        {
            _logger.LogWarning("Invalid Base64 in Google payload.");
            return false;
        }
        catch (JsonException)
        {
            _logger.LogWarning("Invalid JSON in Google payload.");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error validating Google payload.");
            return false;
        }
    }

    /// <summary>
    /// Parses the Google Play notification payload and potentially fetches details from Google API.
    /// </summary>
    public async Task<PaymentTransactionDetails> ParsePayloadAsync(string payload)
    {
        try
        {
            byte[] data = Convert.FromBase64String(payload);
            string decodedString = Encoding.UTF8.GetString(data);
            using var doc = JsonDocument.Parse(decodedString);
            var root = doc.RootElement;

            var subscriptionNotification = root.GetProperty("subscriptionNotification");
            var notificationType = subscriptionNotification.GetProperty("notificationType").GetInt32();
            var purchaseToken = subscriptionNotification.GetProperty("purchaseToken").GetString();
            var subscriptionId = subscriptionNotification.GetProperty("subscriptionId").GetString();

            if (string.IsNullOrEmpty(purchaseToken) || string.IsNullOrEmpty(subscriptionId))
            {
                throw new PaymentProviderException("Missing purchaseToken or subscriptionId in Google payload.");
            }

            // In a production environment, we would use the Google.Apis.AndroidPublisher.v3 library
            // to call purchases.subscriptions.get(packageName, subscriptionId, purchaseToken)
            // to get the authoritative expiryTimeMillis and paymentState.
            
            // Simulating API response for this implementation:
            var apiResult = await FetchSubscriptionDetailsMockAsync(subscriptionId!, purchaseToken!);

            return new PaymentTransactionDetails
            {
                ExternalTransactionId = purchaseToken!, // Google uses token as a unique ref for the purchase state
                ExternalSubscriptionId = purchaseToken!, // Often maps to purchase token or orderId if available
                TransactionDate = DateTimeOffset.UtcNow,
                Amount = 0, // Google notification doesn't include price; requires API call
                Currency = "USD",
                ValidUntil = apiResult.ExpiryTime,
                Status = MapNotificationTypeToStatus(notificationType)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing Google webhook payload.");
            throw new PaymentProviderException("Failed to parse Google payload", ex);
        }
    }

    private SubscriptionStatus MapNotificationTypeToStatus(int type)
    {
        // https://developer.android.com/google/play/billing/rtdn-reference#sub
        return type switch
        {
            1 => SubscriptionStatus.Active, // SUBSCRIPTION_RECOVERED
            2 => SubscriptionStatus.Active, // SUBSCRIPTION_RENEWED
            3 => SubscriptionStatus.Cancelled, // SUBSCRIPTION_CANCELED (Voluntary)
            4 => SubscriptionStatus.Active, // SUBSCRIPTION_PURCHASED
            5 => SubscriptionStatus.Expired, // SUBSCRIPTION_ON_HOLD (Account hold) -> Treat as expired or suspended
            6 => SubscriptionStatus.Expired, // SUBSCRIPTION_IN_GRACE_PERIOD -> Treat as active usually, but depends on logic
            7 => SubscriptionStatus.Active, // SUBSCRIPTION_RESTARTED
            9 => SubscriptionStatus.Active, // SUBSCRIPTION_DEFERRED
            10 => SubscriptionStatus.Expired, // SUBSCRIPTION_PAUSED
            12 => SubscriptionStatus.Expired, // SUBSCRIPTION_REVOKED
            13 => SubscriptionStatus.Expired, // SUBSCRIPTION_EXPIRED
            _ => SubscriptionStatus.Unknown
        };
    }

    // Mock method to simulate Google API call.
    // In real implementation, inject IGooglePlayPublisherService
    private Task<(DateTimeOffset ExpiryTime, string OrderId)> FetchSubscriptionDetailsMockAsync(string subId, string token)
    {
        // Simulate network delay
        return Task.FromResult((DateTimeOffset.UtcNow.AddMonths(1), $"GPA.{Guid.NewGuid()}"));
    }
}