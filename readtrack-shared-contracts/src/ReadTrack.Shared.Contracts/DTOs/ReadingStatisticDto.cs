using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.DTOs;

/// <summary>
/// Data Transfer Object containing aggregated reading statistics for a user.
/// Used to populate the 'Statistics' and 'Insights' screens in the application.
/// </summary>
public record ReadingStatisticDto
{
    /// <summary>
    /// The ID of the user these statistics belong to.
    /// </summary>
    [JsonPropertyName("userId")]
    public Guid UserId { get; init; }

    /// <summary>
    /// Total number of books marked as 'Read'.
    /// </summary>
    [JsonPropertyName("totalBooksFinished")]
    public int TotalBooksFinished { get; init; }

    /// <summary>
    /// Cumulative count of pages read across all sessions.
    /// </summary>
    [JsonPropertyName("totalPagesRead")]
    public int TotalPagesRead { get; init; }

    /// <summary>
    /// Total time spent reading in minutes.
    /// </summary>
    [JsonPropertyName("totalReadingTimeMinutes")]
    public int TotalReadingTimeMinutes { get; init; }

    /// <summary>
    /// Calculated average reading speed in pages per hour.
    /// </summary>
    [JsonPropertyName("averageReadingSpeedPagesPerHour")]
    public double AverageReadingSpeedPagesPerHour { get; init; }

    /// <summary>
    /// Current consecutive days streak of reading activity.
    /// </summary>
    [JsonPropertyName("currentStreakDays")]
    public int CurrentStreakDays { get; init; }

    /// <summary>
    /// Longest consecutive days streak ever achieved by the user.
    /// </summary>
    [JsonPropertyName("longestStreakDays")]
    public int LongestStreakDays { get; init; }

    /// <summary>
    /// Breakdown of reading activity by day of the week (0=Sunday to 6=Saturday).
    /// Key is day index, Value is minutes read.
    /// </summary>
    [JsonPropertyName("weeklyActivity")]
    public Dictionary<int, int> WeeklyActivity { get; init; } = new();

    /// <summary>
    /// Top genres read by the user, ordered by frequency.
    /// </summary>
    [JsonPropertyName("topGenres")]
    public List<string> TopGenres { get; init; } = new();

    /// <summary>
    /// The date these statistics were last calculated/aggregated.
    /// </summary>
    [JsonPropertyName("lastUpdated")]
    public DateTimeOffset LastUpdated { get; init; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ReadingStatisticDto() { }
}