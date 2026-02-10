using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadTrack.Monetization.Application.Commands.ProcessWebhook;
using ReadTrack.Monetization.Domain.Enums;
using ReadTrack.Monetization.Presentation.DTOs;

namespace ReadTrack.Monetization.Presentation.Controllers;

/// <summary>
/// Handles external webhook notifications from payment providers (Apple App Store, Google Play Store).
/// These endpoints function as the entry point for subscription lifecycle events.
/// </summary>
[ApiController]
[Route("api/v1/webhooks")]
public class WebhooksController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<WebhooksController> _logger;

    public WebhooksController(ISender sender, ILogger<WebhooksController> logger)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Receives App Store Server Notifications V2 from Apple.
    /// </summary>
    /// <param name="request">The payload containing the signed transaction info.</param>
    /// <returns>200 OK to acknowledge receipt.</returns>
    [HttpPost("apple")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AppleWebhook([FromBody] AppleWebhookRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.SignedPayload))
        {
            _logger.LogWarning("Received empty or invalid Apple webhook payload.");
            return BadRequest("Invalid payload structure.");
        }

        _logger.LogInformation("Received Apple webhook notification.");

        try
        {
            // We dispatch the command to process the webhook.
            // The command handler takes care of signature verification, parsing, and idempotency.
            var command = new ProcessWebhookCommand(
                Provider: PaymentProvider.Apple,
                Payload: request.SignedPayload
            );

            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                // We log the failure but generally return 200 OK to Apple to prevent retries of malformed/invalid requests 
                // that failed validation, UNLESS it's a transient error that warrants a retry.
                // However, per standard webhook practices, if we fail to process logic (business rule), we might still ack.
                // If the error is internal system failure, the Global Exception Handler will likely return 500, causing Apple to retry.
                _logger.LogError("Failed to process Apple webhook: {Error}", result.Error);
                
                // If it's a validation error (e.g. bad signature), we return 200 to stop Apple from retrying junk.
                // If it's a system error, we might want to return 500 (handled by middleware).
                // Here we assume result.Failure indicates a handled logic failure.
                return Ok(); 
            }

            _logger.LogInformation("Successfully processed Apple webhook.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing Apple webhook.");
            // Return 500 to signal Apple to retry later
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal processing error.");
        }
    }

    /// <summary>
    /// Receives Real-time Developer Notifications from Google Play (via Cloud Pub/Sub).
    /// </summary>
    /// <param name="request">The Pub/Sub envelope containing the base64 encoded data.</param>
    /// <returns>200 OK to acknowledge receipt.</returns>
    [HttpPost("google")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GoogleWebhook([FromBody] GoogleWebhookRequest request)
    {
        // Google Pub/Sub sends a specific wrapper structure.
        if (request?.Message == null || string.IsNullOrWhiteSpace(request.Message.Data))
        {
            _logger.LogWarning("Received empty or invalid Google webhook payload.");
            // For Google Pub/Sub, returning 400 will cause it to NOT retry (usually), or it might dead-letter.
            return BadRequest("Invalid payload structure.");
        }

        _logger.LogInformation("Received Google webhook notification (MessageId: {MessageId}).", request.Message.MessageId);

        try
        {
            // The actual notification data is in the 'data' field, base64 encoded.
            // The Command Handler will decode and validate this.
            var command = new ProcessWebhookCommand(
                Provider: PaymentProvider.Google,
                Payload: request.Message.Data
            );

            var result = await _sender.Send(command);

            if (result.IsFailure)
            {
                _logger.LogError("Failed to process Google webhook: {Error}", result.Error);
                // Return 200 OK to acknowledge the Pub/Sub message so it isn't redelivered infinitely
                // unless it is a transient issue we want to retry.
                return Ok();
            }

            _logger.LogInformation("Successfully processed Google webhook.");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while processing Google webhook.");
            // Return 500 to trigger Pub/Sub retry policy
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal processing error.");
        }
    }
}