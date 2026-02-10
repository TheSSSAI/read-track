using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.DTOs;

/// <summary>
/// Data Transfer Object representing a reading tip or educational article.
/// Sourced from the Headless CMS (Contentful) and served to the client.
/// </summary>
public record TipArticleDto
{
    /// <summary>
    /// Unique identifier for the article (from CMS).
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// The headline title of the article.
    /// </summary>
    [JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// A short summary or teaser text for list views.
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; init; }

    /// <summary>
    /// The full body content of the article. 
    /// May contain Markdown or HTML depending on CMS configuration.
    /// </summary>
    [JsonPropertyName("content")]
    public required string Content { get; init; }

    /// <summary>
    /// The category or topic this tip belongs to (e.g., "Focus", "Speed", "Comprehension").
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; init; }

    /// <summary>
    /// URL to the cover image for the article.
    /// </summary>
    [JsonPropertyName("imageUrl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ImageUrl { get; init; }

    /// <summary>
    /// The date the article was published.
    /// </summary>
    [JsonPropertyName("publishDate")]
    public DateTimeOffset PublishDate { get; init; }

    /// <summary>
    /// Estimated time in minutes required to read the article.
    /// </summary>
    [JsonPropertyName("estimatedReadTimeMinutes")]
    public int EstimatedReadTimeMinutes { get; init; }

    /// <summary>
    /// List of tags associated with the article for searching/filtering.
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; init; } = new();

    /// <summary>
    /// Indicates if this content is premium-only.
    /// </summary>
    [JsonPropertyName("isPremium")]
    public bool IsPremium { get; init; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public TipArticleDto() { }
}