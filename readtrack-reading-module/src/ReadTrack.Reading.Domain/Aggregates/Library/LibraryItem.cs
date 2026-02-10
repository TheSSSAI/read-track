using System;
using System.Collections.Generic;
using System.Linq;
using ReadTrack.Reading.Domain.Events;

namespace ReadTrack.Reading.Domain.Aggregates.Library
{
    /// <summary>
    /// Aggregate Root representing a book in a user's library.
    /// Manages the lifecycle, state, and reading history of a book.
    /// </summary>
    public class LibraryItem
    {
        private readonly List<ReadingSession> _readingSessions = new();
        private readonly List<object> _domainEvents = new();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string GoogleBookId { get; private set; }
        public BookMetadata Metadata { get; private set; }
        public ShelfStatus Shelf { get; private set; }
        public int TotalPagesRead { get; private set; }
        public DateTimeOffset DateAdded { get; private set; }
        public DateTimeOffset? DateFinished { get; private set; }
        
        public IReadOnlyCollection<ReadingSession> ReadingSessions => _readingSessions.AsReadOnly();
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        // Private constructor for EF Core
        private LibraryItem() { }

        private LibraryItem(Guid userId, string googleBookId, BookMetadata metadata, ShelfStatus initialShelf)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            GoogleBookId = googleBookId;
            Metadata = metadata;
            Shelf = initialShelf;
            DateAdded = DateTimeOffset.UtcNow;
            TotalPagesRead = 0;

            if (initialShelf == ShelfStatus.Read)
            {
                DateFinished = DateTimeOffset.UtcNow;
                TotalPagesRead = metadata.PageCount ?? 0;
            }
        }

        /// <summary>
        /// Factory method to create a new Library Item.
        /// </summary>
        public static LibraryItem Create(Guid userId, string googleBookId, BookMetadata metadata, ShelfStatus initialShelf)
        {
            if (userId == Guid.Empty) throw new ArgumentException("User ID required", nameof(userId));
            if (string.IsNullOrWhiteSpace(googleBookId)) throw new ArgumentException("Google Book ID required", nameof(googleBookId));
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));

            return new LibraryItem(userId, googleBookId, metadata, initialShelf);
        }

        /// <summary>
        /// Moves the book to a different shelf (e.g., Want to Read -> Currently Reading).
        /// </summary>
        public void MoveToShelf(ShelfStatus newShelf)
        {
            if (Shelf == newShelf) return;

            var oldShelf = Shelf;
            Shelf = newShelf;

            if (newShelf == ShelfStatus.Read)
            {
                DateFinished = DateTimeOffset.UtcNow;
                // If moving to read, assume pages are completed if not tracked
                if (Metadata.PageCount.HasValue && TotalPagesRead < Metadata.PageCount.Value)
                {
                    TotalPagesRead = Metadata.PageCount.Value;
                }
            }
            else if (oldShelf == ShelfStatus.Read && newShelf != ShelfStatus.Read)
            {
                DateFinished = null;
            }

            _domainEvents.Add(new LibraryItemMovedToShelfEvent(Id, UserId, oldShelf, newShelf, DateTimeOffset.UtcNow));
        }

        /// <summary>
        /// Logs a reading session for this book.
        /// </summary>
        public void LogSession(ReadingSession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (session.LibraryItemId != Id) throw new ArgumentException("Session does not belong to this library item.");

            _readingSessions.Add(session);
            TotalPagesRead += session.PagesRead;

            // Cap at total pages if known
            if (Metadata.PageCount.HasValue && TotalPagesRead > Metadata.PageCount.Value)
            {
                TotalPagesRead = Metadata.PageCount.Value;
            }

            // Automatically move to "Currently Reading" if on "Want To Read"
            if (Shelf == ShelfStatus.WantToRead)
            {
                MoveToShelf(ShelfStatus.CurrentlyReading);
            }

            // Check if finished
            if (Metadata.PageCount.HasValue && TotalPagesRead >= Metadata.PageCount.Value && Shelf != ShelfStatus.Read)
            {
                MoveToShelf(ShelfStatus.Read);
            }

            _domainEvents.Add(new ReadingSessionLoggedEvent(
                Id, 
                UserId, 
                session.Duration ?? TimeSpan.Zero, 
                session.PagesRead, 
                session.StartTime));
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}