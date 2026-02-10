using System;
using MediatR;

namespace ReadTrack.Users.Application.Features.Users.Commands
{
    /// <summary>
    /// Command to initiate the permanent deletion of a user account.
    /// </summary>
    /// <remarks>
    /// This command triggers the GDPR "Right to Erasure" flow (Sequence 458).
    /// It initiates cascading soft-deletes and schedules background jobs for PII anonymization.
    /// </remarks>
    /// <param name="UserId">The unique identifier of the user requesting deletion.</param>
    public record DeleteAccountCommand(Guid UserId) : IRequest<bool>
    {
        /// <summary>
        /// Validates the command integrity.
        /// </summary>
        public void Validate()
        {
            if (UserId == Guid.Empty)
            {
                throw new ArgumentException("User ID must be provided for account deletion.", nameof(UserId));
            }
        }
    }
}