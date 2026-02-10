using System;
using System.Collections.Generic;
using MediatR;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Features.Library.Queries.GetUserLibrary
{
    /// <summary>
    /// Query to retrieve the user's library, optionally filtered by shelf.
    /// </summary>
    /// <param name="UserId">The user ID.</param>
    /// <param name="ShelfFilter">Optional shelf filter.</param>
    public record GetUserLibraryQuery(
        Guid UserId,
        ShelfStatus? ShelfFilter = null) : IRequest<List<LibraryItem>>;
}