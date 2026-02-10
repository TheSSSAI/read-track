import '../../../models/library_item_dto.dart';
import '../../../models/reading_session_dto.dart';

/// Contract for the remote Library API.
/// This source is responsible for communicating with the backend services.
abstract class ILibraryRemoteSource {
  /// Fetches the user's library items.
  /// [lastSync] is optional for delta synchronization support.
  Future<List<LibraryItemDto>> fetchLibrary({DateTime? lastSync});

  /// Adds a new book to the remote library.
  /// Returns the created DTO from the server (which includes the server-generated ID).
  Future<LibraryItemDto> addBook(LibraryItemDto item);

  /// Updates an existing book on the remote library.
  Future<LibraryItemDto> updateBook(LibraryItemDto item);

  /// Deletes a book from the remote library by its Server ID.
  Future<void> deleteBook(String serverId);

  /// Pushes a batch of reading sessions to the backend.
  Future<void> syncSessions(List<ReadingSessionDto> sessions);

  /// Performs a search for books via the backend proxy (e.g. Google Books wrapper).
  Future<List<LibraryItemDto>> searchBooks(String query);
}