import 'package:readtrack_domain/readtrack_domain.dart';
import '../../data/models/user_dto.dart';

/// Mapper extensions for converting between [User] domain entities and [UserDto].
extension UserMapper on UserDto {
  /// Converts the DTO to a clean Domain Entity.
  User toDomain() {
    return User(
      id: id,
      email: email,
      username: username,
      avatarUrl: avatarUrl,
      subscriptionTier: _mapTier(subscriptionTier),
      joinedAt: joinedAt,
      lastLoginAt: lastLoginAt,
    );
  }

  SubscriptionTier _mapTier(String? tier) {
    switch (tier?.toLowerCase()) {
      case 'premium':
        return SubscriptionTier.premium;
      case 'free':
      default:
        return SubscriptionTier.free;
    }
  }
}

extension UserEntityMapper on User {
  /// Converts the Domain Entity to a DTO for local storage.
  UserDto toDto({int? isarId}) {
    return UserDto()
      ..isarId = isarId
      ..id = id
      ..email = email
      ..username = username
      ..avatarUrl = avatarUrl
      ..subscriptionTier = subscriptionTier == SubscriptionTier.premium ? 'premium' : 'free'
      ..joinedAt = joinedAt
      ..lastLoginAt = lastLoginAt;
  }
}