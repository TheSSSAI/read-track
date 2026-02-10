using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReadTrack.Recommendations.Application.DTOs;
using ReadTrack.Recommendations.Application.Features.Recommendations;

namespace ReadTrack.Recommendations.Web.Controllers
{
    /// <summary>
    /// API Controller for managing AI-powered book recommendations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class RecommendationsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<RecommendationsController> _logger;

        public RecommendationsController(ISender sender, ILogger<RecommendationsController> logger)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Initiates the background process to generate personalized book recommendations.
        /// </summary>
        /// <returns>A Job ID to poll for status.</returns>
        /// <response code="202">Request accepted and job queued.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="429">User has exceeded their monthly recommendation limit.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> GenerateRecommendations()
        {
            var userId = GetCurrentUserId();
            _logger.LogInformation("Received recommendation generation request for User {UserId}", userId);

            try
            {
                var command = new GenerateRecommendationsCommand(userId);
                var result = await _sender.Send(command);

                if (result.IsSuccess)
                {
                    // Return 202 Accepted with a Location header to poll the job status
                    return AcceptedAtAction(
                        nameof(GetRecommendationJobStatus),
                        new { jobId = result.Value },
                        new { JobId = result.Value });
                }

                // Map domain/application errors to HTTP status codes
                // Assuming result.Error contains information about rate limits or other issues
                if (result.Error.Contains("limit", StringComparison.OrdinalIgnoreCase))
                {
                    return StatusCode(StatusCodes.Status429TooManyRequests, new { error = result.Error });
                }

                return BadRequest(new { error = result.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error initiating recommendation generation for User {UserId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Polls the status of a recommendation generation job.
        /// </summary>
        /// <param name="jobId">The unique identifier of the job.</param>
        /// <returns>The current status and results if completed.</returns>
        /// <response code="200">Returns the job status.</response>
        /// <response code="404">Job not found.</response>
        [HttpGet("jobs/{jobId}")]
        [ProducesResponseType(typeof(RecommendationJobStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecommendationJobStatus(Guid jobId)
        {
            var userId = GetCurrentUserId();
            var query = new GetRecommendationJobStatusQuery(jobId, userId);

            try
            {
                var result = await _sender.Send(query);

                if (result == null)
                {
                    return NotFound(new { error = $"Job {jobId} not found or does not belong to the user." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving job status for Job {JobId} User {UserId}", jobId, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the job status.");
            }
        }

        /// <summary>
        /// Submits user feedback (Like/Dislike/Dismiss) for a specific recommendation.
        /// </summary>
        /// <param name="recommendationId">The ID of the recommendation.</param>
        /// <param name="request">The feedback details.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">Feedback submitted successfully.</response>
        /// <response code="400">Invalid feedback request.</response>
        [HttpPost("{recommendationId}/feedback")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitFeedback(Guid recommendationId, [FromBody] SubmitFeedbackRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid feedback data.");
            }

            var userId = GetCurrentUserId();
            var command = new SubmitFeedbackCommand(
                userId, 
                recommendationId, 
                request.FeedbackType, 
                request.Comments);

            try
            {
                var result = await _sender.Send(command);

                if (result.IsSuccess)
                {
                    return NoContent();
                }

                return BadRequest(new { error = result.Error });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting feedback for Recommendation {RecommendationId} User {UserId}", recommendationId, userId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while submitting feedback.");
            }
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }

            // In a real authenticated scenario, this might trigger a 401 via middleware, 
            // but for safety in the controller logic:
            _logger.LogWarning("User ID claim missing or invalid in authenticated request.");
            throw new UnauthorizedAccessException("User ID is missing or invalid.");
        }

        // DTO for binding the feedback request body
        public record SubmitFeedbackRequest(string FeedbackType, string? Comments);
    }
}