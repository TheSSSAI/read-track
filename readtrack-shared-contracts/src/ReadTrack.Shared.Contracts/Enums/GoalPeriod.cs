using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.Enums;

/// <summary>
/// Defines the recurrence period for a reading goal.
/// Determines how often the goal progress resets or is evaluated.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GoalPeriod
{
    /// <summary>
    /// A goal that must be achieved every single day (e.g., Read 30 minutes daily).
    /// Resets at 00:00 local time.
    /// </summary>
    Daily = 0,

    /// <summary>
    /// A goal that must be achieved within a calendar week (e.g., Read 100 pages per week).
    /// Typically resets on Monday or Sunday depending on user locale settings.
    /// </summary>
    Weekly = 1,

    /// <summary>
    /// A goal that must be achieved within a calendar month (e.g., Read 2 books per month).
    /// Resets on the 1st of the month.
    /// </summary>
    Monthly = 2,

    /// <summary>
    /// A long-term goal for the entire calendar year (e.g., Read 50 books in 2024).
    /// Resets on January 1st.
    /// </summary>
    Yearly = 3
}