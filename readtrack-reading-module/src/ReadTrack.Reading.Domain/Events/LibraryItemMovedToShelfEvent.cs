using System;
using ReadTrack.Reading.Domain.Aggregates.Library;

namespace ReadTrack.Reading.Domain.Events;

/// <summary>
/// Domain Event raised when a library item is moved from one shelf to another.
/// Triggers updates for "Books Read" goals or cleanup when moving to "DidNotFinish".
/// </summary>
/// <param name="LibraryItemId">The identifier of the library item.</param>
/// <param name="UserId">The owner of the library item.</param>
/// <param name="OldShelf">The previous shelf status.</param>
/// <param name="NewShelf">The new shelf status.</param>
/// <param name="OccurredOn">The timestamp when the move occurred.</param>
public sealed record LibraryItemMovedToShelfEvent(
    Guid LibraryItemId,
    Guid UserId,
    ShelfStatus OldShelf,
    ShelfStatus NewShelf,
    DateTimeOffset OccurredOn
);