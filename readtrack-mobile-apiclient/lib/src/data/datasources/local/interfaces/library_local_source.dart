import '../../../models/library_item_dto.dart';
import '../../../models/reading_session_dto.dart';

/// Contract for the local persistence of library data (Isar).
/// Supports Offline-First architecture by acting as the primary cache.
abstract class ILibraryLocalSource {
  /// Retrieves all library items currently stored locally.
  Future<List<LibraryItemDto>> getAllLibraryItems();

  /// Retrieves a specific library item by its server ID.
  Future<LibraryItemDto?> getLibraryItemByServerId(String serverId);

  /// Persists a list of library items, replacing conflicts or updating existing ones.
  /// Used primarily during synchronization pull.
  Future<void> putLibraryItems(List<LibraryItemDto> items);

  /// Persists a single library item.
  Future<void> putLibraryItem(LibraryItemDto item);

  /// Marks an item as deleted locally (soft delete) or removes it if not synced.
  Future<void> deleteLibraryItem(int isarId);

  /// Retrieves all library items marked as 'Dirty' (Created/Updated locally).
  /// Used by the synchronization service to push changes.
  Future<List<LibraryItemDto>> getDirtyLibraryItems();

  /// Retrieves all library items marked as 'Deleted' locally.
  /// Used by the synchronization service to push deletes.
  Future<List<LibraryItemDto>> getDeletedLibraryItems();

  /// Saves a reading session locally.
  Future<void> saveReadingSession(ReadingSessionDto session);

  /// Retrieves reading sessions for a specific library item.
  Future<List<ReadingSessionDto>> getSessionsForBook(int libraryItemId);

  /// Retrieves dirty reading sessions for synchronization.
  Future<List<ReadingSessionDto>> getDirtyReadingSessions();

  /// Clears all library data.
  Future<void> clearAll();
}