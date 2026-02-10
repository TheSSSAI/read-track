import '../../../models/user_dto.dart';

/// Contract for the remote Authentication and User API.
abstract class IAuthRemoteSource {
  /// Authenticates a user with a Google ID Token.
  /// Returns the User profile and sets up the session via AuthInterceptor logic implicitly.
  Future<UserDto> loginWithGoogle(String idToken);

  /// Authenticates a user with an Apple ID Token.
  Future<UserDto> loginWithApple(String idToken, {String? nonce});

  /// Refreshes the access token using a refresh token.
  /// Note: This is typically used by the Interceptor, but defined here for architecture completeness
  /// or manual invocation if necessary.
  Future<Map<String, dynamic>> refreshToken(String refreshToken);

  /// Fetches the current user's profile from the backend.
  Future<UserDto> getUserProfile();

  /// Deletes the user's account permanently.
  Future<void> deleteAccount();
}