import 'package:isar/isar.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:readtrack_client/src/data/models/sync_enums.dart';

part 'library_item_dto.g.dart';

/// Data Transfer Object for Books and Articles in the library.
///
/// Supports offline caching via Isar and network transport via JsonSerializable.
@JsonSerializable()
@Collection()
class LibraryItemDto {
  /// Local Isar ID, generated via fastHash of the string [id].
  @Id()
  int get isarId => _fastHash(id);

  /// The unique server-side identifier (UUID).
  @Index(unique: true, replace: true)
  final String id;

  /// External ID from Google Books or other sources (optional).
  @JsonKey(name: 'google_book_id')
  final String? googleBookId;

  final String title;

  /// List of authors. Isar supports List<String>.
  final List<String> authors;

  @JsonKey(name: 'cover_url')
  final String? coverUrl;

  /// Total page count. Null for articles or unknown books.
  @JsonKey(name: 'page_count')
  final int? pageCount;

  /// Current shelf status (e.g., 'WantToRead', 'CurrentlyReading', 'Read', 'DNF').
  /// Stored as String for API compatibility.
  @JsonKey(name: 'shelf_status')
  final String shelfStatus;

  /// Date when the item was finished or abandoned.
  @JsonKey(name: 'completion_date')
  final DateTime? completionDate;

  /// Current progress as a percentage (0-100), useful for non-paged items.
  @JsonKey(name: 'current_progress_percent')
  final double? currentProgressPercent;

  /// Current progress as a page number.
  @JsonKey(name: 'current_page')
  final int? currentPage;

  /// Timestamp for Conflict Resolution (Last Write Wins).
  @JsonKey(name: 'updated_at')
  final DateTime updatedAt;

  /// Local synchronization state.
  @JsonKey(includeToJson: false, includeFromJson: false)
  @Enumerated(EnumType.ordinal)
  final SyncStatus syncStatus;

  LibraryItemDto({
    required this.id,
    this.googleBookId,
    required this.title,
    this.authors = const [],
    this.coverUrl,
    this.pageCount,
    required this.shelfStatus,
    this.completionDate,
    this.currentProgressPercent,
    this.currentPage,
    required this.updatedAt,
    this.syncStatus = SyncStatus.synced,
  });

  /// Creates a [LibraryItemDto] from a JSON map.
  factory LibraryItemDto.fromJson(Map<String, dynamic> json) => _$LibraryItemDtoFromJson(json);

  /// Converts this [LibraryItemDto] to a JSON map.
  Map<String, dynamic> toJson() => _$LibraryItemDtoToJson(this);

  /// Creates a copy of this DTO with updated fields.
  LibraryItemDto copyWith({
    String? id,
    String? googleBookId,
    String? title,
    List<String>? authors,
    String? coverUrl,
    int? pageCount,
    String? shelfStatus,
    DateTime? completionDate,
    double? currentProgressPercent,
    int? currentPage,
    DateTime? updatedAt,
    SyncStatus? syncStatus,
  }) {
    return LibraryItemDto(
      id: id ?? this.id,
      googleBookId: googleBookId ?? this.googleBookId,
      title: title ?? this.title,
      authors: authors ?? this.authors,
      coverUrl: coverUrl ?? this.coverUrl,
      pageCount: pageCount ?? this.pageCount,
      shelfStatus: shelfStatus ?? this.shelfStatus,
      completionDate: completionDate ?? this.completionDate,
      currentProgressPercent: currentProgressPercent ?? this.currentProgressPercent,
      currentPage: currentPage ?? this.currentPage,
      updatedAt: updatedAt ?? this.updatedAt,
      syncStatus: syncStatus ?? this.syncStatus,
    );
  }
}

/// Helper function to generate a consistent integer ID from a String for Isar.
int _fastHash(String string) {
  var hash = 0xcbf29ce484222325;
  var i = 0;
  while (i < string.length) {
    final codeUnit = string.codeUnitAt(i++);
    hash ^= codeUnit >> 8;
    hash *= 0x100000001b3;
    hash ^= codeUnit & 0xFF;
    hash *= 0x100000001b3;
  }
  return hash;
}