import 'package:flutter_riverpod/flutter_riverpod.dart';
// Assuming the external package exists as per repository definition
import 'package:readtrack_apiclient/readtrack_apiclient.dart';

import '../../config/constants/env_config.dart';
import '../exceptions/app_error_handler.dart';

/// Provides the [AppErrorHandler] for global error handling logic.
///
/// This service is used to centralize error reporting and logging across
/// the application's UI and business logic layers.
final appErrorHandlerProvider = Provider<AppErrorHandler>((ref) {
  // Logger dependency would be injected here if it were a separate provider,
  // but AppErrorHandler instantiates it internally or via its own DI strategy
  // based on the Level 1 implementation.
  return AppErrorHandler();
});

/// Provides the configured [ApiClient] instance for network requests.
///
/// The [ApiClient] is initialized with the base URL defined in [EnvConfig].
/// This provider serves as the primary gateway for all remote data interactions.
///
/// Usage:
/// ```dart
/// final apiClient = ref.read(apiClientProvider);
/// final user = await apiClient.getUserProfile();
/// ```
final apiClientProvider = Provider<ApiClient>((ref) {
  // Inject the Base URL from the environment configuration (Level 0)
  final baseUrl = EnvConfig.baseUrl;
  
  // Initialize the API client with environment-specific configuration.
  // We explicitly create a new instance to ensure it uses the correct config.
  return ApiClient(
    baseUrl: baseUrl,
    // Additional configuration like timeouts or interceptors could be added here
    // or handled internally by the ApiClient constructor.
  );
});

/// Provides the [StorageService] instance for local data persistence.
///
/// This service abstracts the underlying local storage implementation (e.g., Isar,
/// SecureStorage). This provider is typically meant to be overridden in the
/// [ProviderScope] at the app root if the service requires asynchronous
/// initialization (e.g., `await Isar.open(...)`).
///
/// However, providing a default implementation allows for lazy initialization
/// or usage in tests where the mock is injected.
final storageServiceProvider = Provider<StorageService>((ref) {
  // Return the default implementation from the API client package.
  // If asynchronous initialization is required, this should be handled
  // via an AppBootstrap process that overrides this provider with the
  // ready instance.
  return StorageService.instance;
});

/// A convenience provider to access the current [AuthStatus] directly if needed,
/// though typical usage flows through the AuthController state.
/// This acts as a bridge between the data layer auth status and the UI.
final authStatusProvider = StreamProvider<AuthStatus>((ref) {
  final apiClient = ref.watch(apiClientProvider);
  // Assuming ApiClient exposes a stream of auth changes
  return apiClient.authStateChanges.map((user) {
    if (user != null) {
      return AuthStatus.authenticated;
    }
    return AuthStatus.unauthenticated;
  });
});