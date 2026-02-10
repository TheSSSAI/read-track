using System;
using MediatR;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Features.Library.Commands.UpdateLibraryItemShelf
{
    /// <summary>
    /// Command to move a library item to a different shelf.
    /// </summary>
    /// <param name="LibraryItemId">The ID of the library item to update.</param>
    /// <param name="UserId">The owner of the library item (for authorization).</param>
    /// <param name="NewShelf">The target shelf status.</param>
    public record UpdateLibraryItemShelfCommand(
        Guid LibraryItemId,
        Guid UserId,
        ShelfStatus NewShelf) : IRequest<bool>;
}