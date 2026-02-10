using System;
using MediatR;
using ReadTrack.Users.Application.DTOs;

namespace ReadTrack.Users.Application.Features.Users.Commands
{
    /// <summary>
    /// Command to update the mutable profile information of a registered user.
    /// </summary>
    /// <remarks>
    /// Handles updates to the user's display name and potentially other profile attributes.
    /// Adheres to Sequence 472 for user profile updates.
    /// </remarks>
    public record UpdateUserProfileCommand : IRequest<UserProfileDto>
    {
        /// <summary>
        /// The unique identifier of the user to update.
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// The new display name for the user.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// The optional URL for the user's profile image.
        /// </summary>
        public string? ProfileImageUrl { get; }

        /// <summary>
        /// Initializes a new instance of the UpdateUserProfileCommand.
        /// </summary>
        /// <param name="userId">The ID of the user executing the update.</param>
        /// <param name="displayName">The new display name (must not be empty).</param>
        /// <param name="profileImageUrl">Optional profile image URL.</param>
        /// <exception cref="ArgumentException">Thrown when userId is empty or displayName is null/whitespace.</exception>
        public UpdateUserProfileCommand(Guid userId, string displayName, string? profileImageUrl = null)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Display name cannot be empty.", nameof(displayName));

            UserId = userId;
            DisplayName = displayName.Trim();
            ProfileImageUrl = profileImageUrl;
        }
    }
}