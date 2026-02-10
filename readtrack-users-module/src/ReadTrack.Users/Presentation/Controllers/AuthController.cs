using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadTrack.Users.Application.DTOs;
using ReadTrack.Users.Application.Features.Auth.Commands;

namespace ReadTrack.Users.Presentation.Controllers
{
    /// <summary>
    /// Handles authentication operations including social login and token management.
    /// </summary>
    [ApiController]
    [Route("api/v1/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="sender">The MediatR sender for dispatching commands.</param>
        /// <param name="logger">The logger instance.</param>
        public AuthController(ISender sender, ILogger<AuthController> logger)
        {
            _sender = sender ?? throw new System.ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Authenticates a user via a social identity provider (Google/Apple) and returns an access token.
        /// </summary>
        /// <param name="request">The social login request containing provider details and ID token.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An authentication response containing access and refresh tokens.</returns>
        [HttpPost("social")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthResponseDto>> SocialLogin(
            [FromBody] SocialLoginRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogWarning("Social login request body is null.");
                return BadRequest("Invalid request payload.");
            }

            _logger.LogInformation("Processing social login request for provider: {Provider}", request.Provider);

            try
            {
                var command = new SocialLoginCommand(request.Provider, request.IdToken);
                var result = await _sender.Send(command, cancellationToken);

                if (result == null)
                {
                    _logger.LogWarning("Social login failed. No result returned.");
                    return Unauthorized("Authentication failed.");
                }

                _logger.LogInformation("User authenticated successfully via {Provider}", request.Provider);
                return Ok(result);
            }
            catch (System.ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation failed for social login request.");
                return BadRequest(ex.Message);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt during social login.");
                return Unauthorized(ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during social login.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Refreshes an expired access token using a valid refresh token.
        /// </summary>
        /// <param name="command">The refresh token command containing the existing tokens.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A new set of access and refresh tokens.</returns>
        [HttpPost("refresh")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken(
            [FromBody] RefreshTokenCommand command,
            CancellationToken cancellationToken)
        {
            if (command == null)
            {
                return BadRequest("Invalid request payload.");
            }

            _logger.LogInformation("Processing token refresh request.");

            try
            {
                var result = await _sender.Send(command, cancellationToken);
                _logger.LogInformation("Token refreshed successfully.");
                return Ok(result);
            }
            catch (System.Security.SecurityException ex)
            {
                _logger.LogWarning(ex, "Security exception during token refresh: {Message}", ex.Message);
                return Unauthorized("Invalid refresh token.");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error processing token refresh request.");
                return BadRequest("Could not refresh token.");
            }
        }
    }
}