import 'package:isar/isar.dart';
import 'package:json_annotation/json_annotation.dart';
import 'package:readtrack_client/src/data/models/sync_enums.dart';

part 'user_dto.g.dart';

/// Data Transfer Object for User profiles.
///
/// Serves as both a JSON serialization model for the API and a persistent
/// entity for the local Isar database.
@JsonSerializable()
@Collection()
class UserDto {
  /// Local Isar ID, generated via fastHash of the string [id].
  @Id()
  int get isarId => _fastHash(id);

  /// The unique server-side identifier (UUID).
  @Index(unique: true, replace: true)
  final String id;

  final String email;

  @JsonKey(name: 'display_name')
  final String? displayName;

  @JsonKey(name: 'photo_url')
  final String? photoUrl;

  /// The user's subscription tier (e.g., 'Free', 'Premium').
  @JsonKey(name: 'subscription_tier')
  final String subscriptionTier;

  /// The last time this record was updated on the server or locally.
  @JsonKey(name: 'updated_at')
  final DateTime updatedAt;

  /// The synchronization status of this record.
  /// 
  /// This field is ignored during JSON serialization as it is strictly
  /// a local persistence concern.
  @JsonKey(includeToJson: false, includeFromJson: false)
  @Enumerated(EnumType.ordinal)
  final SyncStatus syncStatus;

  UserDto({
    required this.id,
    required this.email,
    this.displayName,
    this.photoUrl,
    this.subscriptionTier = 'Free',
    required this.updatedAt,
    this.syncStatus = SyncStatus.synced,
  });

  /// Creates a [UserDto] from a JSON map.
  factory UserDto.fromJson(Map<String, dynamic> json) => _$UserDtoFromJson(json);

  /// Converts this [UserDto] to a JSON map.
  Map<String, dynamic> toJson() => _$UserDtoToJson(this);

  /// Creates a copy of this DTO with updated fields.
  UserDto copyWith({
    String? id,
    String? email,
    String? displayName,
    String? photoUrl,
    String? subscriptionTier,
    DateTime? updatedAt,
    SyncStatus? syncStatus,
  }) {
    return UserDto(
      id: id ?? this.id,
      email: email ?? this.email,
      displayName: displayName ?? this.displayName,
      photoUrl: photoUrl ?? this.photoUrl,
      subscriptionTier: subscriptionTier ?? this.subscriptionTier,
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