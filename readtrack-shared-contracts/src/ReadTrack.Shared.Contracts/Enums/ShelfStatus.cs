using System.Text.Json.Serialization;

namespace ReadTrack.Shared.Contracts.Enums;

/// <summary>
/// Represents the current lifecycle state of a book or article within the user's personal library.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ShelfStatus
{
    /// <summary>
    /// The item is in the backlog; the user intends to read it in the future.
    /// This is the default state when adding new items via search.
    /// </summary>
    WantToRead = 0,

    /// <summary>
    /// The user is actively reading this item.
    /// Progress updates and reading sessions can be logged against items in this state.
    /// </summary>
    CurrentlyReading = 1,

    /// <summary>
    /// The user has finished reading the item.
    /// A completion date is typically associated with this state.
    /// </summary>
    Read = 2,

    /// <summary>
    /// "Did Not Finish". The user started the item but decided to stop reading it permanently.
    /// These items are preserved in history but excluded from completion statistics.
    /// </summary>
    DidNotFinish = 3
}