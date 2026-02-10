using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadTrack.Reading.Domain.Aggregates.Library;

/// <summary>
/// Value Object representing the immutable metadata of a book.
/// Encapsulates details retrieved from external sources like Google Books.
/// </summary>
public sealed record BookMetadata
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookMetadata"/> class.
    /// </summary>
    /// <param name="googleBookId">The unique identifier from Google Books API.</param>
    /// <param name="title">The title of the book.</param>
    /// <param name="authors">The list of authors.</param>
    /// <param name="isbn">The ISBN-13 or ISBN-10 identifier.</param>
    /// <param name="coverUrl">The URL to the book cover image.</param>
    /// <param name="pageCount">The total number of pages, if available.</param>
    /// <param name="description">A short description or synopsis.</param>
    /// <param name="categories">The genres or categories associated with the book.</param>
    /// <param name="language">The language code (e.g., 'en').</param>
    /// <exception cref="ArgumentException">Thrown when required fields are missing.</exception>
    public BookMetadata(
        string googleBookId,
        string title,
        IEnumerable<string> authors,
        string? isbn = null,
        string? coverUrl = null,
        int? pageCount = null,
        string? description = null,
        IEnumerable<string>? categories = null,
        string? language = null)
    {
        if (string.IsNullOrWhiteSpace(googleBookId))
            throw new ArgumentException("Google Book ID cannot be empty.", nameof(googleBookId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Book title cannot be empty.", nameof(title));

        if (authors == null || !authors.Any())
            throw new ArgumentException("Book must have at least one author.", nameof(authors));

        if (pageCount.HasValue && pageCount.Value < 0)
            throw new ArgumentException("Page count cannot be negative.", nameof(pageCount));

        GoogleBookId = googleBookId;
        Title = title;
        Authors = authors.ToList().AsReadOnly();
        Isbn = isbn;
        CoverUrl = coverUrl;
        PageCount = pageCount;
        Description = description;
        Categories = categories?.ToList().AsReadOnly() ?? new List<string>().AsReadOnly();
        Language = language;
    }

    /// <summary>
    /// Gets the unique identifier from the Google Books API.
    /// </summary>
    public string GoogleBookId { get; }

    /// <summary>
    /// Gets the title of the book.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the collection of authors.
    /// </summary>
    public IReadOnlyList<string> Authors { get; }

    /// <summary>
    /// Gets the International Standard Book Number (ISBN).
    /// </summary>
    public string? Isbn { get; }

    /// <summary>
    /// Gets the URL for the book's cover image.
    /// </summary>
    public string? CoverUrl { get; }

    /// <summary>
    /// Gets the total page count.
    /// </summary>
    public int? PageCount { get; }

    /// <summary>
    /// Gets the description or synopsis of the book.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the categories or genres.
    /// </summary>
    public IReadOnlyList<string> Categories { get; }

    /// <summary>
    /// Gets the language code (e.g. "en").
    /// </summary>
    public string? Language { get; }

    /// <summary>
    /// Returns a string representation of the book metadata.
    /// </summary>
    public override string ToString()
    {
        var authors = string.Join(", ", Authors);
        return $"{Title} by {authors} [ID: {GoogleBookId}]";
    }
}