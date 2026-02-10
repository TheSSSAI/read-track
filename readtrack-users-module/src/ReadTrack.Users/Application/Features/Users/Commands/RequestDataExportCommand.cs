using System;
using MediatR;

namespace ReadTrack.Users.Application.Features.Users.Commands
{
    /// <summary>
    /// Command to request a comprehensive export of all user data.
    /// </summary>
    /// <remarks>
    /// This implements the "Right to Data Portability" requirement (REQ-FUNC-009 / Sequence 459).
    /// The command initiates an asynchronous background job and returns the Job ID for tracking.
    /// </remarks>
    /// <param name="UserId">The ID of the user requesting the export.</param>
    public record RequestDataExportCommand(Guid UserId) : IRequest<Guid>
    {
        /// <summary>
        /// Ensures the request is valid before processing.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if UserId is empty.</exception>
        public void EnsureValid()
        {
            if (UserId == Guid.Empty)
            {
                throw new ArgumentException("Valid User ID is required to initiate data export.", nameof(UserId));
            }
        }
    }
}