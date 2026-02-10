using System.Collections.Generic;
using MediatR;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Application.Features.Library.Queries.SearchBooks
{
    /// <summary>
    /// Query to search for books via the external Google Books API.
    /// </summary>
    /// <param name="Query">The search string (title, author, or ISBN).</param>
    public record SearchBooksQuery(string Query) : IRequest<List<BookMetadata>>;
}