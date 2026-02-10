using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadTrack.Monetization.Application.DTOs;
using ReadTrack.Monetization.Application.Queries.GetSubscriptionStatus;

namespace ReadTrack.Monetization.Presentation.Controllers;

/// <summary>
/// Exposes endpoints for managing and querying user subscription status.
/// </summary>
[ApiController]
[Route("api/v1/subscriptions")]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<SubscriptionController> _logger;

    public SubscriptionController(ISender sender, ILogger<SubscriptionController> logger)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Retrieves the current subscription status for the authenticated user.
    /// </summary>
    /// <returns>The subscription details including tier and expiration.</returns>
    /// <response code="200">Returns the subscription status.</response>
    /// <response code="401">If the user is not authenticated.</response>
    /// <response code="500">If an internal error occurs.</response>
    [HttpGet("me")]
    [ProducesResponseType(typeof(SubscriptionStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMySubscription(CancellationToken cancellationToken)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        _logger.LogDebug("Fetching subscription status for user {UserId}", userId);

        var query = new GetSubscriptionStatusQuery(userId);
        var result = await _sender.Send(query, cancellationToken);

        // The query is designed to always return a DTO (defaulting to Free if no sub found),
        // so we typically won't see a failure here unless database connectivity issues occur.
        // If the Result pattern handles not found internally by returning default, IsSuccess is true.
        if (result.IsFailure)
        {
            _logger.LogError("Failed to fetch subscription for user {UserId}: {Error}", userId, result.Error);
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving subscription status.");
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Helper method to extract the User ID from the ClaimsPrincipal.
    /// This assumes the standard 'sub' or 'nameidentifier' claim is used for the User ID Guid.
    /// </summary>
    private bool TryGetUserId(out Guid userId)
    {
        userId = Guid.Empty;
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");

        if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
        {
            _logger.LogWarning("User ID claim not found in the current principal.");
            return false;
        }

        if (!Guid.TryParse(userIdClaim.Value, out userId))
        {
            _logger.LogWarning("User ID claim value '{ClaimValue}' is not a valid Guid.", userIdClaim.Value);
            return false;
        }

        return true;
    }
}