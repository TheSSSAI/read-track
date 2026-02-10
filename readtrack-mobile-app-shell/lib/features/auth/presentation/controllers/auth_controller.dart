import 'dart:async';

import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:readtrack_apiclient/readtrack_apiclient.dart';

import '../../../../core/providers/dependency_providers.dart';
import '../../data/auth_state.dart';
import '../../data/auth_status.dart';

/// Global provider for accessing the authentication controller and state.
/// This provider is used by the Router to determine redirection logic
/// and by the UI to show loading/error/authenticated states.
final authControllerProvider = AsyncNotifierProvider<AuthController, AuthState>(AuthController.new);

/// Controller responsible for managing the global authentication state of the application.
/// It orchestrates interactions between the API Client, Local Storage, and the UI state.
class AuthController extends AsyncNotifier<AuthState> {
  late final IApiClient _apiClient;
  late final StorageService _storageService;

  /// Key used for storing the authentication token in secure storage.
  static const String _tokenKey = 'auth_token';

  @override
  Future<AuthState> build() async {
    _apiClient = ref.read(apiClientProvider);
    _storageService = ref.read(storageServiceProvider);

    return _restoreSession();
  }

  /// Attempts to restore the user session from local storage on app startup.
  Future<AuthState> _restoreSession() async {
    try {
      final token = await _storageService.read(key: _tokenKey);

      if (token == null || token.isEmpty) {
        return const AuthState.unauthenticated();
      }

      // Configure the API client with the restored token for subsequent requests
      // Assuming the ApiClient exposes a way to set the token or uses an interceptor
      // that reads from the same storage source.
      // Here we validate the token by fetching the profile.
      final userProfile = await _apiClient.getUserProfile();

      if (userProfile != null) {
        return AuthState.authenticated(userProfile);
      } else {
        // Token exists but profile fetch failed (likely invalid token)
        await _clearSession();
        return const AuthState.unauthenticated();
      }
    } catch (e, stackTrace) {
      // If network fails during startup, we might want to stay unauthenticated
      // or handle offline mode differently. For strict auth, we treat verification
      // failure as unauthenticated if we can't verify the token.
      
      // However, if we want to support offline usage, we would need to load
      // the user profile from local database (Isar) here.
      // Given the scope of this file, we default to unauthenticated on error
      // to ensure security, unless it's a specific network error and we have local data.
      // For this implementation, we will log out on error to be safe.
      await _clearSession();
      
      // We return failure state only if we want to show an error screen,
      // but for bootstrap, unauthenticated is usually safer to redirect to login.
      // We'll log the error internally (if a logger was available) and return unauth.
      return const AuthState.unauthenticated();
    }
  }

  /// Performs the login operation with email and password.
  Future<void> login(String email, String password) async {
    state = const AsyncValue.loading();

    state = await AsyncValue.guard(() async {
      try {
        final authResponse = await _apiClient.login(
          email: email,
          password: password,
        );

        if (authResponse.token.isNotEmpty) {
          await _storageService.write(key: _tokenKey, value: authResponse.token);
          
          // Fetch complete profile details after successful login
          final userProfile = await _apiClient.getUserProfile();
          
          if (userProfile != null) {
            return AuthState.authenticated(userProfile);
          } else {
            throw const AuthException('Failed to retrieve user profile after login.');
          }
        } else {
          return const AuthState.failure('Invalid response from server.');
        }
      } on NetworkException catch (e) {
        // Map specific API network exceptions to business state
        return AuthState.failure(e.message ?? 'Connection failed. Please check your internet.');
      } on AuthException catch (e) {
        // Handle specific auth errors (e.g. invalid credentials)
        return AuthState.failure(e.message);
      } catch (e) {
        return AuthState.failure('An unexpected error occurred: ${e.toString()}');
      }
    });
  }

  /// Logs the user out, clearing local storage and resetting state.
  Future<void> logout() async {
    state = const AsyncValue.loading();

    try {
      // Attempt server-side logout (fire and forget)
      // We don't block the local logout if the server fails
      await _apiClient.logout().catchError((_) {}); 
    } finally {
      await _clearSession();
      state = const AsyncValue.data(AuthState.unauthenticated());
    }
  }

  /// Clears local authentication data.
  Future<void> _clearSession() async {
    await _storageService.delete(key: _tokenKey);
  }
}

/// Custom exception for authentication related errors within the controller
class AuthException implements Exception {
  final String message;
  const AuthException(this.message);
  
  @override
  String toString() => message;
}