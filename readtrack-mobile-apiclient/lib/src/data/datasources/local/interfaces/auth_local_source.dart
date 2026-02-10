import '../../../models/user_dto.dart';

/// Contract for local persistence of user profile data.
/// Note: Authentication tokens are handled by SecureStorageService,
/// this source handles the User Entity data.
abstract class IAuthLocalSource {
  /// Saves the user profile data locally.
  Future<void> saveUser(UserDto user);

  /// Retrieves the currently logged-in user profile.
  Future<UserDto?> getUser();

  /// Deletes the local user profile data.
  Future<void> deleteUser();

  /// Updates specific fields of the user profile.
  Future<void> updateUser(UserDto user);
}