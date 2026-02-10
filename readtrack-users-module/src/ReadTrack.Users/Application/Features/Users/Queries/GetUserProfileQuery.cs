using System;
using MediatR;
using ReadTrack.Users.Application.DTOs;

namespace ReadTrack.Users.Application.Features.Users.Queries
{
    /// <summary>
    /// Query to retrieve the current profile information for a user.
    /// </summary>
    /// <remarks>
    /// Used to populate the User Profile UI and validate user status.
    /// Returns a readonly projection of the user entity as <see cref="UserProfileDto"/>.
    /// </remarks>
    /// <param name="UserId">The unique identifier of the user to retrieve.</param>
    public record GetUserProfileQuery(Guid UserId) : IRequest<UserProfileDto>
    {
        /// <summary>
        /// Validates that the query parameters are sufficient for execution.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when UserId is empty.</exception>
        public void Validate()
        {
            if (UserId == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty when fetching profile.", nameof(UserId));
            }
        }
    }
}