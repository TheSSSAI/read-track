using System;
using MediatR;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Features.Library.Commands.AddBookToLibrary
{
    /// <summary>
    /// Command to add a new book to the user's library.
    /// This triggers limits checks for Free users.
    /// </summary>
    /// <param name="UserId">The ID of the user adding the book.</param>
    /// <param name="GoogleBookId">The unique identifier from Google Books API.</param>
    /// <param name="InitialShelf">The shelf to place the book on initially.</param>
    public record AddBookToLibraryCommand(
        Guid UserId,
        string GoogleBookId,
        ShelfStatus InitialShelf) : IRequest<Guid>; // Returns the Guid of the created LibraryItem (or Result wrapper in Handler)
}