using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.DTOs;

/// <summary>
/// Data Transfer Object representing the metadata of a book.
/// Used for search results, library item details, and recommendations.
/// </summary>
public record BookDto
{
    /// <summary>
    /// The unique identifier of the book (e.g., Google Books Volume ID) if sourced externally.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// The main title of the book.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// The subtitle of the book, if available.
    /// </summary>
    [JsonPropertyName("subtitle")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Subtitle { get; init; }

    /// <summary>
    /// List of authors associated with the book.
    /// </summary>
    [JsonPropertyName("authors")]
    public List<string> Authors { get; init; } = new();

    /// <summary>
    /// The ISBN-13 identifier for the book.
    /// </summary>
    [JsonPropertyName("isbn13")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Isbn13 { get; init; }

    /// <summary>
    /// The ISBN-10 identifier for the book.
    /// </summary>
    [JsonPropertyName("isbn10")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Isbn10 { get; init; }

    /// <summary>
    /// The total number of pages in the book.
    /// </summary>
    [JsonPropertyName("pageCount")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PageCount { get; init; }

    /// <summary>
    /// The publisher of the book.
    /// </summary>
    [JsonPropertyName("publisher")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Publisher { get; init; }

    /// <summary>
    /// The publication date of the book.
    /// </summary>
    [JsonPropertyName("publishedDate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PublishedDate { get; init; }

    /// <summary>
    /// A description or synopsis of the book content.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; init; }

    /// <summary>
    /// URL to the thumbnail image of the book cover.
    /// </summary>
    [JsonPropertyName("thumbnailUrl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ThumbnailUrl { get; init; }

    /// <summary>
    /// Categories or genres associated with the book (e.g., "Fiction", "Science").
    /// </summary>
    [JsonPropertyName("categories")]
    public List<string> Categories { get; init; } = new();

    /// <summary>
    /// Language code (e.g., "en", "es").
    /// </summary>
    [JsonPropertyName("language")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Language { get; init; }

    /// <summary>
    /// Constructor for initializing required properties.
    /// </summary>
    public BookDto() { }

    /// <summary>
    /// Computed property to return the best available identifier (ISBN13, then ISBN10, then Id).
    /// </summary>
    [JsonIgnore]
    public string PrimaryIdentifier => Isbn13 ?? Isbn10 ?? Id ?? "Unknown";
}