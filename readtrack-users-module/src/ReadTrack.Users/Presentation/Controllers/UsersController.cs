using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadTrack.Users.Application.DTOs;
using ReadTrack.Users.Application.Features.Users.Commands;
using ReadTrack.Users.Application.Features.Users.Queries;

namespace ReadTrack.Users.Presentation.Controllers
{
    /// <summary>
    /// API Controller for managing user profiles, account settings, and data privacy compliance.
    /// Requires authentication for all endpoints.
    /// </summary>
    [ApiController]
    [Route("api/v1/users")]
    [Authorize]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="sender">The MediatR sender.</param>
        /// <param name="logger">The logger instance.</param>
        public UsersController(ISender sender, ILogger<UsersController> logger)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves the profile of the currently authenticated user.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The user profile details.</returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserProfileDto>> GetProfile(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User identifier missing from token.");
            }

            _logger.LogDebug("Retrieving profile for user {UserId}", userId);

            try
            {
                var query = new GetUserProfileQuery(userId);
                var profile = await _sender.Send(query, cancellationToken);

                if (profile == null)
                {
                    _logger.LogWarning("Profile not found for user {UserId}", userId);
                    return NotFound();
                }

                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving profile for user {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the profile.");
            }
        }

        /// <summary>
        /// Updates the profile information of the currently authenticated user.
        /// </summary>
        /// <param name="request">The update request containing new profile data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>No content on success.</returns>
        [HttpPut("me")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateUserProfileDto request,
            CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }

            if (request == null)
            {
                return BadRequest("Invalid request body.");
            }

            _logger.LogInformation("Updating profile for user {UserId}", userId);

            try
            {
                // Map the DTO to the Command, ensuring the UserId comes from the token
                var command = new UpdateUserProfileCommand(
                    userId,
                    request.DisplayName,
                    request.ProfilePictureUrl);

                await _sender.Send(command, cancellationToken);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation failed for profile update for user {UserId}", userId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for user {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the profile.");
            }
        }

        /// <summary>
        /// Initiates a permanent deletion of the user's account and associated data (GDPR Right to Erasure).
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Accepted if the deletion process has started.</returns>
        [HttpDelete("me")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }

            _logger.LogWarning("User {UserId} has requested account deletion.", userId);

            try
            {
                var command = new DeleteAccountCommand(userId);
                await _sender.Send(command, cancellationToken);

                return Accepted(new { message = "Account deletion request accepted and processing." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing account deletion for user {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing account deletion.");
            }
        }

        /// <summary>
        /// Requests a machine-readable export of the user's personal data (GDPR Data Portability).
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Accepted if the export job has been queued.</returns>
        [HttpPost("me/export")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RequestDataExport(CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized();
            }

            _logger.LogInformation("User {UserId} requested data export.", userId);

            try
            {
                var command = new RequestDataExportCommand(userId);
                var jobId = await _sender.Send(command, cancellationToken);

                return Accepted(new { JobId = jobId, Message = "Data export request queued." });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Data export request failed validation for user {UserId}", userId);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error queueing data export for user {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while queueing data export.");
            }
        }

        /// <summary>
        /// Helper method to extract the User ID from the current claims principal.
        /// </summary>
        /// <returns>The Guid User ID, or Guid.Empty if not found/invalid.</returns>
        private Guid GetCurrentUserId()
        {
            var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            // Check for custom claim if the standard sub isn't the Guid
            if (string.IsNullOrEmpty(claimValue))
            {
                claimValue = User.FindFirstValue("readtrack_user_id");
            }

            if (string.IsNullOrEmpty(claimValue))
            {
                return Guid.Empty;
            }

            // Attempt to parse Guid
            if (Guid.TryParse(claimValue, out var guidId))
            {
                return guidId;
            }

            // If the claim is an Auth0 ID (e.g., "auth0|123"), we might need to handle this differently
            // In a strict clean architecture, the token should contain the internal Guid.
            // For now, logging a warning if parsing fails.
            _logger.LogWarning("Failed to parse User ID Guid from claim value: {ClaimValue}", claimValue);
            return Guid.Empty;
        }
    }

    /// <summary>
    /// DTO for updating user profile information.
    /// Internal definition to bind request body.
    /// </summary>
    public record UpdateUserProfileDto(string DisplayName, string? ProfilePictureUrl);
}