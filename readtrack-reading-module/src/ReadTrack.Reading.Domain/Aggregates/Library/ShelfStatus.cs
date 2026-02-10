namespace ReadTrack.Reading.Domain.Aggregates.Library;

/// <summary>
/// Represents the current status of a book within the user's personal library.
/// </summary>
public enum ShelfStatus
{
    /// <summary>
    /// The book is in the backlog, intended to be read in the future.
    /// </summary>
    WantToRead = 0,

    /// <summary>
    /// The book is currently being read by the user.
    /// </summary>
    CurrentlyReading = 1,

    /// <summary>
    /// The book has been completed.
    /// </summary>
    Read = 2,

    /// <summary>
    /// The user started the book but decided not to finish it.
    /// </summary>
    DidNotFinish = 3
}