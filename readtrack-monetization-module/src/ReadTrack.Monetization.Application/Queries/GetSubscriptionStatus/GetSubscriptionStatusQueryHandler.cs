using MediatR;
using Microsoft.Extensions.Logging;
using ReadTrack.Monetization.Application.DTOs;
using ReadTrack.Monetization.Domain.Interfaces;
using ReadTrack.Monetization.Domain.ValueObjects;

namespace ReadTrack.Monetization.Application.Queries.GetSubscriptionStatus;

/// <summary>
/// Handles retrieval of the current subscription status for a user.
/// Used by clients to determine feature access and ad display policies.
/// </summary>
public class GetSubscriptionStatusQueryHandler : IRequestHandler<GetSubscriptionStatusQuery, SubscriptionStatusDto>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly ILogger<GetSubscriptionStatusQueryHandler> _logger;

    public GetSubscriptionStatusQueryHandler(
        ISubscriptionRepository subscriptionRepository, 
        ILogger<GetSubscriptionStatusQueryHandler> logger)
    {
        _subscriptionRepository = subscriptionRepository;
        _logger = logger;
    }

    public async Task<SubscriptionStatusDto> Handle(GetSubscriptionStatusQuery request, CancellationToken cancellationToken)
    {
        // 1. Fetch the user's most relevant subscription
        // A user might have multiple historical records, we want the active one or the most recent one.
        var subscription = await _subscriptionRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        // 2. Determine Tier and Rules
        if (subscription == null)
        {
            // Default to Free if no subscription record exists
            _logger.LogDebug("No subscription found for User {UserId}. Returning Free tier.", request.UserId);
            return new SubscriptionStatusDto(SubscriptionTier.Free, ShowAds: true, ExpiryDate: null);
        }

        // 3. Validate Status
        // Even if the DB says 'Active', we double check the date to be precise
        bool isActive = subscription.Status == SubscriptionStatus.Active 
                        && subscription.ValidUntil > DateTimeOffset.UtcNow;

        if (isActive)
        {
            // Premium Logic: Tier is Premium, Ads are hidden
            return new SubscriptionStatusDto(
                Tier: SubscriptionTier.Premium, 
                ShowAds: false, 
                ExpiryDate: subscription.ValidUntil
            );
        }
        else
        {
            // Expired/Cancelled Logic: Revert to Free, Show Ads
            return new SubscriptionStatusDto(
                Tier: SubscriptionTier.Free, 
                ShowAds: true, 
                ExpiryDate: subscription.ValidUntil
            );
        }
    }
}