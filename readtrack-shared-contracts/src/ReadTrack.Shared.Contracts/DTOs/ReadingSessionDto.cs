using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.DTOs;

/// <summary>
/// Data Transfer Object representing a single logged reading session.
/// Transferred between the mobile client and the backend for synchronization and history display.
/// </summary>
public record ReadingSessionDto
{
    /// <summary>
    /// Unique identifier for the reading session.
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    /// <summary>
    /// The ID of the LibraryItem (UserBook) this session belongs to.
    /// </summary>
    [JsonPropertyName("libraryItemId")]
    public Guid LibraryItemId { get; init; }

    /// <summary>
    /// The timestamp when the session started or was logged for (UTC).
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTimeOffset StartTime { get; init; }

    /// <summary>
    /// The duration of the reading session in minutes.
    /// Used for time-based tracking and statistics.
    /// </summary>
    [JsonPropertyName("durationMinutes")]
    public int DurationMinutes { get; init; }

    /// <summary>
    /// The number of pages read during this specific session.
    /// Applicable for page-based tracking.
    /// </summary>
    [JsonPropertyName("pagesRead")]
    public int? PagesRead { get; init; }

    /// <summary>
    /// The page number where the user started this session.
    /// </summary>
    [JsonPropertyName("startPage")]
    public int? StartPage { get; init; }

    /// <summary>
    /// The page number where the user ended this session.
    /// </summary>
    [JsonPropertyName("endPage")]
    public int? EndPage { get; init; }

    /// <summary>
    /// The percentage progress recorded at the end of this session.
    /// Applicable for percentage-based tracking (e.g., e-books, articles).
    /// Value is between 0.0 and 100.0.
    /// </summary>
    [JsonPropertyName("progressPercentage")]
    public double? ProgressPercentage { get; init; }

    /// <summary>
    /// Optional user notes or thoughts recorded during this session.
    /// </summary>
    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Notes { get; init; }

    /// <summary>
    /// Indicates if this session was manually entered (true) or tracked via timer (false).
    /// </summary>
    [JsonPropertyName("isManualEntry")]
    public bool IsManualEntry { get; init; }

    /// <summary>
    /// Client-side timestamp representing when this record was last modified.
    /// Used for conflict resolution during offline synchronization ('last write wins').
    /// </summary>
    [JsonPropertyName("clientLastModified")]
    public DateTimeOffset ClientLastModified { get; init; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ReadingSessionDto() { }
}