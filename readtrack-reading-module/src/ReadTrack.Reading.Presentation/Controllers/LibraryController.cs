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
using ReadTrack.Reading.Application.Features.Library.Commands.AddBookToLibrary;
using ReadTrack.Reading.Application.Features.Library.Commands.UpdateLibraryItemShelf;
using ReadTrack.Reading.Application.Features.Library.Queries.GetUserLibrary;
using ReadTrack.Reading.Application.Features.Library.Queries.SearchBooks;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Presentation.Controllers;

/// <summary>
/// Manages library operations including searching, adding books, and updating shelf status.
/// </summary>
[ApiController]
[Route("api/v1/library")]
[Authorize]
[Produces(MediaTypeNames.Application.Json)]
public class LibraryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibraryController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    /// Searches for books using the external Google Books API provider.
    /// </summary>
    /// <param name="query">The search term (title, author, or ISBN).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of book metadata results.</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(List<BookMetadata>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<BookMetadata>>> SearchBooks(
        [FromQuery] string query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var searchBooksQuery = new SearchBooksQuery(query);
        var result = await _mediator.Send(searchBooksQuery, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Retrieves the authenticated user's personal library.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of library items belonging to the user.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<LibraryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<LibraryItem>>> GetUserLibrary(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var query = new GetUserLibraryQuery(userId);
        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Adds a new book to the user's library.
    /// </summary>
    /// <param name="request">The book details to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ID of the newly created library item.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> AddBookToLibrary(
        [FromBody] AddBookRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        
        var command = new AddBookToLibraryCommand(
            userId, 
            request.GoogleBookId, 
            request.InitialShelf);

        // Note: The Command Handler returns a Result<Guid>. 
        // We assume the Result type pattern exposes Success/Failure properties.
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            // Map specific domain/business errors to HTTP status codes
            if (result.Error.Contains("Limit", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(StatusCodes.Status403Forbidden, result.Error);
            }
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetUserLibrary), new { }, result.Value);
    }

    /// <summary>
    /// Updates the shelf status of a specific library item.
    /// </summary>
    /// <param name="id">The ID of the library item to update.</param>
    /// <param name="request">The new shelf status.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>No content on success.</returns>
    [HttpPatch("{id:guid}/shelf")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateLibraryItemShelf(
        Guid id,
        [FromBody] UpdateShelfRequest request,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        var command = new UpdateLibraryItemShelfCommand(
            userId, 
            id, 
            request.NewShelfStatus);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            if (result.Error.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result.Error);
            }
            return BadRequest(result.Error);
        }

        return NoContent();
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

    // DTOs specific to the Controller Request Body to avoid exposing UserId in the body
    public record AddBookRequest(string GoogleBookId, ShelfStatus InitialShelf);
    public record UpdateShelfRequest(ShelfStatus NewShelfStatus);
}