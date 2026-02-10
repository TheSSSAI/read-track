using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadTrack.Reading.Application.Interfaces
{
    /// <summary>
    /// Interface for interacting with the Monetization Module to retrieve user subscription details.
    /// This creates a decoupling point between Reading and Monetization.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Retrieves the subscription status for a given user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Status details including tier and limits.</returns>
        Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Data Transfer Object representing the user's subscription state relevant to the Reading module.
    /// </summary>
    public record SubscriptionStatusDto(
        bool IsPremium,
        int MaxLibraryItems,
        bool IsActive
    );
}