import 'package:readtrack_domain/readtrack_domain.dart';
import '../../data/models/library_item_dto.dart';
import '../../data/models/reading_session_dto.dart';
import '../../data/models/sync_enums.dart';

/// Mapper extensions for converting between [LibraryItem] domain entities and [LibraryItemDto].
extension LibraryItemMapper on LibraryItemDto {
  /// Converts a DTO to a Domain Entity.
  LibraryItem toDomain() {
    return LibraryItem(
      id: serverId ?? '', // Domain ID is the server ID
      title: title,
      author: author,
      coverUrl: coverUrl,
      pageCount: pageCount ?? 0,
      currentPage: currentPage ?? 0,
      status: _mapDtoStatusToDomain(status),
      sessions: sessions.map((s) => s.toDomain()).toList(),
      addedAt: addedAt,
      updatedAt: updatedAt,
    );
  }

  BookStatus _mapDtoStatusToDomain(int statusIndex) {
    // Assuming Enum index mapping: 0: WantToRead, 1: CurrentlyReading, 2: Read, 3: DNF
    // This should match the definition in LibraryItemDto
    const statuses = BookStatus.values;
    if (statusIndex >= 0 && statusIndex < statuses.length) {
      return statuses[statusIndex];
    }
    return BookStatus.wantToRead; // Default fallback
  }
}

extension LibraryItemEntityMapper on LibraryItem {
  /// Converts a Domain Entity to a DTO for persistence/network.
  /// 
  /// [syncStatus] defaults to [SyncStatus.Synced] but should be overridden 
  /// by the repository based on the operation context.
  LibraryItemDto toDto({
    int? isarId, 
    SyncStatus syncStatus = SyncStatus.Synced
  }) {
    return LibraryItemDto()
      ..id = isarId // Persist local ID if updating
      ..serverId = id
      ..title = title
      ..author = author
      ..coverUrl = coverUrl
      ..pageCount = pageCount
      ..currentPage = currentPage
      ..status = status.index
      ..syncStatus = syncStatus
      ..addedAt = addedAt
      ..updatedAt = updatedAt
      ..sessions.addAll(sessions.map((s) => s.toDto()));
  }
}

/// Mapper extensions for [ReadingSession].
extension ReadingSessionMapper on ReadingSessionDto {
  ReadingSession toDomain() {
    return ReadingSession(
      id: id.toString(), // Local ID to string or UUID if available
      bookId: libraryItemId.value.toString(),
      startTime: startTime,
      endTime: endTime,
      durationSeconds: durationSeconds,
      startPage: startPage,
      endPage: endPage,
      notes: notes,
    );
  }
}

extension ReadingSessionEntityMapper on ReadingSession {
  ReadingSessionDto toDto() {
    return ReadingSessionDto()
      ..startTime = startTime
      ..endTime = endTime
      ..durationSeconds = durationSeconds
      ..startPage = startPage
      ..endPage = endPage
      ..notes = notes;
      // libraryItemId is managed by the Isar link mechanism in the repository
  }
}