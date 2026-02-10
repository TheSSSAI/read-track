import 'package:isar/isar.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:readtrack_client/src/data/models/sync_enums.dart';

part 'reading_session_dto.g.dart';

/// Data Transfer Object for a discrete Reading Session.
///
/// Represents a period of time spent reading a specific library item.
@JsonSerializable()
@Collection()
class ReadingSessionDto {
  /// Local Isar ID, generated via fastHash of the string [id].
  @Id()
  int get isarId => _fastHash(id);

  /// The unique server-side identifier (UUID).
  @Index(unique: true, replace: true)
  final String id;

  /// The ID of the [LibraryItemDto] this session belongs to.
  /// Stored as a string ID to simplify serialization. Relationships can be
  /// enforced at the repository or service level.
  @JsonKey(name: 'library_item_id')
  @Index()
  final String libraryItemId;

  /// The start time of the reading session (UTC).
  @JsonKey(name: 'start_time')
  final DateTime startTime;

  /// Duration of the session in seconds.
  @JsonKey(name: 'duration_seconds')
  final int durationSeconds;

  /// The number of pages read during this session (if applicable).
  @JsonKey(name: 'pages_read')
  final int? pagesRead;

  /// The progress percentage gained during this session (if applicable).
  @JsonKey(name: 'progress_delta_percent')
  final double? progressDeltaPercent;

  /// Type of session entry (e.g., 'Timer', 'Manual').
  @JsonKey(name: 'session_type')
  final String sessionType;

  /// Timestamp for Conflict Resolution.
  @JsonKey(name: 'updated_at')
  final DateTime updatedAt;

  /// Local synchronization state.
  @JsonKey(includeToJson: false, includeFromJson: false)
  @Enumerated(EnumType.ordinal)
  final SyncStatus syncStatus;

  ReadingSessionDto({
    required this.id,
    required this.libraryItemId,
    required this.startTime,
    required this.durationSeconds,
    this.pagesRead,
    this.progressDeltaPercent,
    this.sessionType = 'Manual',
    required this.updatedAt,
    this.syncStatus = SyncStatus.synced,
  });

  /// Creates a [ReadingSessionDto] from a JSON map.
  factory ReadingSessionDto.fromJson(Map<String, dynamic> json) => _$ReadingSessionDtoFromJson(json);

  /// Converts this [ReadingSessionDto] to a JSON map.
  Map<String, dynamic> toJson() => _$ReadingSessionDtoToJson(this);

  /// Creates a copy of this DTO with updated fields.
  ReadingSessionDto copyWith({
    String? id,
    String? libraryItemId,
    DateTime? startTime,
    int? durationSeconds,
    int? pagesRead,
    double? progressDeltaPercent,
    String? sessionType,
    DateTime? updatedAt,
    SyncStatus? syncStatus,
  }) {
    return ReadingSessionDto(
      id: id ?? this.id,
      libraryItemId: libraryItemId ?? this.libraryItemId,
      startTime: startTime ?? this.startTime,
      durationSeconds: durationSeconds ?? this.durationSeconds,
      pagesRead: pagesRead ?? this.pagesRead,
      progressDeltaPercent: progressDeltaPercent ?? this.progressDeltaPercent,
      sessionType: sessionType ?? this.sessionType,
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