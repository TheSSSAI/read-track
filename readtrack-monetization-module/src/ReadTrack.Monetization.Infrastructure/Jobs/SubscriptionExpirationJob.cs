using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using ReadTrack.Monetization.Domain.Interfaces;
using ReadTrack.Monetization.Application.Interfaces;
using ReadTrack.Monetization.Domain.ValueObjects;

namespace ReadTrack.Monetization.Infrastructure.Jobs;

/// <summary>
/// Background job responsible for identifying expired subscriptions and downgrading users.
/// Acts as a safety net for missed webhooks to ensure revenue assurance and correct feature gating.
/// Designed to be run via a scheduler (e.g., Hangfire), but implemented here as a runnable service method.
/// </summary>
public class SubscriptionExpirationJob
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SubscriptionExpirationJob> _logger;

    public SubscriptionExpirationJob(
        IServiceProvider serviceProvider,
        ILogger<SubscriptionExpirationJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <summary>
    /// Executes the expiration check logic. 
    /// This method should be invoked by the scheduling infrastructure (e.g., recurring Hangfire job).
    /// </summary>
    public async Task ProcessExpiredSubscriptionsAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var subscriptionRepository = scope.ServiceProvider.GetRequiredService<ISubscriptionRepository>();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<SubscriptionExpirationJob>>();

        logger.LogInformation("Starting Subscription Expiration Job at {Time}", DateTimeOffset.UtcNow);

        try
        {
            // 1. Fetch all subscriptions that are marked 'Active' but have passed their expiration date
            // Using a batch size to prevent memory overload if many expire at once
            int batchSize = 100;
            int pageNumber = 0;
            bool hasMore = true;
            int processedCount = 0;

            while (hasMore && !cancellationToken.IsCancellationRequested)
            {
                var expiredSubscriptions = await subscriptionRepository.GetExpiredActiveSubscriptionsAsync(batchSize, cancellationToken);
                
                if (!expiredSubscriptions.Any())
                {
                    hasMore = false;
                    continue;
                }

                foreach (var subscription in expiredSubscriptions)
                {
                    try
                    {
                        // 2. Update Domain Entity
                        logger.LogInformation("Downgrading expired subscription {SubscriptionId} for User {UserId}", subscription.SubscriptionId, subscription.UserId);
                        
                        subscription.Terminate(); // Sets Status to Expired/Cancelled based on domain rules
                        
                        await subscriptionRepository.UpdateAsync(subscription, cancellationToken);

                        // 3. Sync with User Module
                        // Downgrade the user to Free tier
                        await userService.UpdateUserTierAsync(subscription.UserId, SubscriptionTier.Free, cancellationToken);
                        
                        processedCount++;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to process expiration for subscription {SubscriptionId}", subscription.SubscriptionId);
                        // Continue processing others even if one fails
                    }
                }

                // 4. Commit batch
                await subscriptionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                
                // Safety break if we process fewer than batch size, meaning we are done
                if (expiredSubscriptions.Count < batchSize)
                {
                    hasMore = false;
                }
            }

            logger.LogInformation("Subscription Expiration Job completed. Processed {Count} subscriptions.", processedCount);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Fatal error in Subscription Expiration Job");
            throw; // Re-throw to ensure the job runner (Hangfire) records the failure and retries
        }
    }
}