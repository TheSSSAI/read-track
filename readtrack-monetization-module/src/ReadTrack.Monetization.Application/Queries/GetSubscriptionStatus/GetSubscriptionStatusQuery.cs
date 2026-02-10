using System;
using MediatR;
using ReadTrack.Monetization.Application.DTOs;

namespace ReadTrack.Monetization.Application.Queries.GetSubscriptionStatus
{
    /// <summary>
    /// CQRS Query to retrieve the subscription status for a specific user.
    /// </summary>
    /// <param name="UserId">The ID of the user to check.</param>
    public record GetSubscriptionStatusQuery(Guid UserId) : IRequest<SubscriptionStatusDto>;
}