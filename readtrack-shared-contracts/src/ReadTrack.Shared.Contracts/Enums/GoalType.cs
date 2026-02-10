using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.Enums;

/// <summary>
/// Defines the specific metric tracked by a reading goal.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GoalType
{
    /// <summary>
    /// The goal tracks the quantity of distinct books completed (e.g., "Read 52 books this year").
    /// Progress increments when a library item status changes to 'Read'.
    /// </summary>
    Books = 0,

    /// <summary>
    /// The goal tracks the number of pages read (e.g., "Read 50 pages per day").
    /// Progress increments based on 'pages_read' in logged reading sessions.
    /// </summary>
    Pages = 1,

    /// <summary>
    /// The goal tracks the duration of time spent reading (e.g., "Read 30 minutes per day").
    /// Progress increments based on 'duration' in logged reading sessions.
    /// </summary>
    Time = 2
}