using System;
using System.Threading;
using System.Threading.Tasks;
using ReadTrack.Monetization.Application.DTOs;

namespace ReadTrack.Monetization.Application.Services
{
    /// <summary>
    /// Defines the application service contract for subscription management.
    /// exposed to other modules within the monolith for status checks.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Retrieves the current subscription status for a user.
        /// This method is optimized for frequent access (e.g., feature gating).
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A DTO containing tier, status, and expiration info.</returns>
        Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}