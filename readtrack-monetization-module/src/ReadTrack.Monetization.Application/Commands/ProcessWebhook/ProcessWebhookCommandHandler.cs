using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Monetization.Application.Interfaces;
using ReadTrack.Monetization.Domain.Entities;
using ReadTrack.Monetization.Domain.Interfaces;
using ReadTrack.Monetization.Domain.ValueObjects;
using ReadTrack.Shared.Contracts;

namespace ReadTrack.Monetization.Application.Commands.ProcessWebhook;

/// <summary>
/// Handles the processing of payment provider webhooks (Apple/Google) to update subscription state.
/// Implements strict idempotency and ensures user tier synchronization.
/// </summary>
public class ProcessWebhookCommandHandler : IRequestHandler<ProcessWebhookCommand, Result>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IPaymentProviderFactory _paymentProviderFactory;
    private readonly IUserService _userService; // Integration with Users Module
    private readonly ILogger<ProcessWebhookCommandHandler> _logger;

    public ProcessWebhookCommandHandler(
        ISubscriptionRepository subscriptionRepository,
        IPaymentProviderFactory paymentProviderFactory,
        IUserService userService,
        ILogger<ProcessWebhookCommandHandler> logger)
    {
        _subscriptionRepository = subscriptionRepository;
        _paymentProviderFactory = paymentProviderFactory;
        _userService = userService;
        _logger = logger;
    }

    public async Task<Result> Handle(ProcessWebhookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Processing webhook for provider {Provider}", request.Provider);

        try
        {
            // 1. Resolve the correct provider strategy
            var paymentProvider = _paymentProviderFactory.GetPaymentProvider(request.Provider);

            // 2. Parse and Validate the webhook payload
            // The provider implementation handles signature verification and payload extraction
            var webhookEvent = await paymentProvider.ParseAndValidateWebhookAsync(request.Payload, cancellationToken);

            if (webhookEvent == null)
            {
                _logger.LogWarning("Webhook validation failed or payload was unparseable for provider {Provider}", request.Provider);
                return Result.Failure("Invalid webhook signature or payload.");
            }

            _logger.LogInformation(
                "Webhook validated. ExternalTransactionId: {TransactionId}, ExternalSubscriptionId: {SubscriptionId}, Type: {Type}",
                webhookEvent.ExternalTransactionId,
                webhookEvent.ExternalSubscriptionId,
                webhookEvent.TransactionType);

            // 3. Idempotency Check
            // Check if we have already processed this specific transaction ID
            var existingTransaction = await _subscriptionRepository.GetTransactionByExternalIdAsync(webhookEvent.ExternalTransactionId, cancellationToken);
            if (existingTransaction != null)
            {
                _logger.LogInformation("Transaction {TransactionId} already processed. Skipping.", webhookEvent.ExternalTransactionId);
                return Result.Success(); // Idempotent success
            }

            // 4. Retrieve or Create Subscription
            var subscription = await _subscriptionRepository.GetByExternalIdAsync(webhookEvent.ExternalSubscriptionId, cancellationToken);

            if (subscription == null)
            {
                // Scenario: New Subscription via Webhook (e.g., if initial purchase receipt wasn't synced yet)
                // We need a UserId to create a subscription. The webhook might contain it in metadata, 
                // or we might fail if we can't link it.
                if (webhookEvent.UserId == Guid.Empty)
                {
                    _logger.LogError("Received webhook for unknown subscription {SubscriptionId} and no UserID found in payload.", webhookEvent.ExternalSubscriptionId);
                    return Result.Failure("Cannot link webhook to user.");
                }

                _logger.LogInformation("Creating new subscription for User {UserId}", webhookEvent.UserId);
                
                subscription = new Subscription(
                    Guid.NewGuid(),
                    webhookEvent.UserId,
                    request.Provider,
                    webhookEvent.ExternalSubscriptionId,
                    SubscriptionTier.Premium, // Assuming purchase implies premium
                    webhookEvent.ExpiryDate
                );

                await _subscriptionRepository.AddAsync(subscription, cancellationToken);
            }
            else
            {
                // Scenario: Existing Subscription (Renewal, Cancellation, etc.)
                _logger.LogInformation("Updating existing subscription {SubscriptionId}", subscription.SubscriptionId);
                
                // Update based on event type
                switch (webhookEvent.TransactionType)
                {
                    case "RENEWAL":
                    case "PURCHASE":
                    case "INTERACTIVE_RENEWAL":
                        subscription.Renew(webhookEvent.ExpiryDate);
                        break;
                        
                    case "CANCEL":
                    case "DID_FAIL_TO_RENEW":
                    case "EXPIRED":
                        // Usually webhooks tell us it *will* expire or *has* expired.
                        // If the expiry date in the webhook is past, we terminate.
                        // If it's a "cancel auto-renew", we typically just update the expiry but keep status active until then.
                        // For simplicity in this command, we trust the expiry date provided.
                        if (webhookEvent.ExpiryDate <= DateTimeOffset.UtcNow)
                        {
                            subscription.Terminate();
                        }
                        else
                        {
                            // Cancellation requested but period not over
                            subscription.UpdateExpiry(webhookEvent.ExpiryDate);
                        }
                        break;
                        
                    case "REFUND":
                    case "REVOKED":
                        subscription.Revoke();
                        break;
                }

                await _subscriptionRepository.UpdateAsync(subscription, cancellationToken);
            }

            // 5. Record the Transaction (Audit Trail)
            var transaction = new PaymentTransaction(
                Guid.NewGuid(),
                subscription.SubscriptionId,
                webhookEvent.ExternalTransactionId,
                webhookEvent.Amount,
                webhookEvent.Currency,
                webhookEvent.TransactionType,
                DateTimeOffset.UtcNow
            );

            await _subscriptionRepository.AddTransactionAsync(transaction, cancellationToken);

            // 6. Commit Database Changes
            await _subscriptionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // 7. Sync User Module
            // We do this after DB commit to ensure consistency. 
            // If this fails, the system is eventually consistent via background jobs or subsequent webhooks.
            try
            {
                await _userService.UpdateUserTierAsync(subscription.UserId, subscription.Tier, cancellationToken);
                _logger.LogInformation("Updated User {UserId} tier to {Tier}", subscription.UserId, subscription.Tier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to sync user tier for User {UserId}. Data may be inconsistent.", subscription.UserId);
                // We do not fail the request here as the financial record is secured.
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing webhook for provider {Provider}", request.Provider);
            return Result.Failure($"Internal error processing webhook: {ex.Message}");
        }
    }
}