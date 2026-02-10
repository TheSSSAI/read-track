import 'package:isar/isar.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:readtrack_client/src/data/models/sync_enums.dart';

part 'goal_dto.g.dart';

/// Data Transfer Object for Reading Goals.
///
/// Allows users to set targets (e.g., 50 books per year, 30 mins per day).
@JsonSerializable()
@Collection()
class GoalDto {
  /// Local Isar ID, generated via fastHash of the string [id].
  @Id()
  int get isarId => _fastHash(id);

  /// The unique server-side identifier (UUID).
  @Index(unique: true, replace: true)
  final String id;

  /// The type of goal (e.g., 'Books', 'Pages', 'Time').
  final String type;

  /// The period for the goal (e.g., 'Daily', 'Weekly', 'Monthly', 'Yearly').
  final String period;

  /// The target value to achieve (e.g., 50 for books, 30 for minutes).
  @JsonKey(name: 'target_value')
  final int targetValue;

  /// The current progress towards the target.
  @JsonKey(name: 'current_progress')
  final int currentProgress;

  /// The date when this goal becomes active.
  @JsonKey(name: 'start_date')
  final DateTime startDate;

  /// The date when this goal expires (optional for recurring/indefinite goals).
  @JsonKey(name: 'end_date')
  final DateTime? endDate;

  /// Timestamp for Conflict Resolution.
  @JsonKey(name: 'updated_at')
  final DateTime updatedAt;

  /// Local synchronization state.
  @JsonKey(includeToJson: false, includeFromJson: false)
  @Enumerated(EnumType.ordinal)
  final SyncStatus syncStatus;

  GoalDto({
    required this.id,
    required this.type,
    required this.period,
    required this.targetValue,
    this.currentProgress = 0,
    required this.startDate,
    this.endDate,
    required this.updatedAt,
    this.syncStatus = SyncStatus.synced,
  });

  /// Creates a [GoalDto] from a JSON map.
  factory GoalDto.fromJson(Map<String, dynamic> json) => _$GoalDtoFromJson(json);

  /// Converts this [GoalDto] to a JSON map.
  Map<String, dynamic> toJson() => _$GoalDtoToJson(this);

  /// Creates a copy of this DTO with updated fields.
  GoalDto copyWith({
    String? id,
    String? type,
    String? period,
    int? targetValue,
    int? currentProgress,
    DateTime? startDate,
    DateTime? endDate,
    DateTime? updatedAt,
    SyncStatus? syncStatus,
  }) {
    return GoalDto(
      id: id ?? this.id,
      type: type ?? this.type,
      period: period ?? this.period,
      targetValue: targetValue ?? this.targetValue,
      currentProgress: currentProgress ?? this.currentProgress,
      startDate: startDate ?? this.startDate,
      endDate: endDate ?? this.endDate,
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