using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadTrack.Reading.Application.Features.Sessions.Commands.LogReadingSession;
using ReadTrack.Reading.Application.Features.Sessions.Commands.SyncReadingSessions;

namespace ReadTrack.Reading.Presentation.Controllers;

/// <summary>
/// Manages reading session operations including logging active sessions and syncing offline data.
/// </summary>
[ApiController]
[Route("api/v1/sessions")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
public class SessionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SessionsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Logs a new reading session for a specific library item.
    /// </summary>
    /// <param name="request">The session details including duration and pages read.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the created session.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> LogSession(
        [FromBody] LogSessionRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var command = new LogReadingSessionCommand(
            userId,
            request.LibraryItemId,
            request.StartTime,
            request.Duration,
            request.PagesRead,
            request.Notes
        );

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            // Handle specific domain errors that map to HTTP status codes
            if (result.Error.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result.Error);
            }
            return BadRequest(result.Error);
        }

        // Ideally returns a location header, but simpler Created response fits generic use case
        return Created(string.Empty, result.Value);
    }

    /// <summary>
    /// Synchronizes a batch of reading sessions recorded while offline.
    /// </summary>
    /// <param name="request">The batch of sessions to sync.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A summary of the synchronization result.</returns>
    [HttpPost("sync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SyncSessions(
        [FromBody] SyncSessionsRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        // Convert the controller request DTOs to the command DTOs expected by the Application layer
        var sessionDtos = new List<SyncSessionDto>();
        if (request.Sessions != null)
        {
            foreach (var s in request.Sessions)
            {
                sessionDtos.Add(new SyncSessionDto(
                    s.LibraryItemId,
                    s.StartTime,
                    s.Duration,
                    s.PagesRead,
                    s.Notes
                ));
            }
        }

        var command = new SyncReadingSessionsCommand(userId, sessionDtos);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return Ok(new { SyncedCount = result.Value });
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                          ?? User.FindFirst("sub")
                          ?? User.FindFirst("uid");

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedAccessException("User identifier is missing or invalid in the token.");
        }

        return userId;
    }

    // DTOs for Controller Binding
    public record LogSessionRequest(
        Guid LibraryItemId,
        DateTime StartTime,
        TimeSpan Duration,
        int PagesRead,
        string? Notes
    );

    public record SyncSessionsRequest(List<SyncSessionItem> Sessions);

    public record SyncSessionItem(
        Guid LibraryItemId,
        DateTime StartTime,
        TimeSpan Duration,
        int PagesRead,
        string? Notes
    );
}