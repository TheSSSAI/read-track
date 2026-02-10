import 'dart:async';

import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:logger/logger.dart';

import 'config/constants/env_config.dart';
import 'core/exceptions/app_error_handler.dart';
import 'core/providers/dependency_providers.dart';

/// A utility class responsible for orchestrating the asynchronous initialization
/// of the application before the UI is rendered.
///
/// This includes:
/// - Initializing Flutter bindings
/// - Setting up global error handling
/// - Initializing the Dependency Injection container (Riverpod)
/// - Warming up critical infrastructure services (Database, Storage)
class AppBootstrap {
  static final Logger _logger = Logger(
    printer: PrettyPrinter(
      methodCount: 0,
      errorMethodCount: 8,
      lineLength: 120,
      colors: true,
      printEmojis: true,
      printTime: true,
    ),
  );

  /// Initializes the application and returns the root [ProviderContainer].
  ///
  /// This method should be called from `main.dart` inside `runZonedGuarded` or
  /// directly in `main` if platform error handling is setup here.
  ///
  /// Returns a [Future] that completes with the initialized [ProviderContainer].
  static Future<ProviderContainer> init() async {
    final stopwatch = Stopwatch()..start();
    
    // 1. Initialize Flutter Bindings
    // Required before any platform channels or plugins are accessed.
    WidgetsFlutterBinding.ensureInitialized();

    // 2. Configure Global Error Handling
    // We instantiate the error handler early to catch initialization errors.
    final errorHandler = AppErrorHandler();

    // Catch Flutter Errors (Layout, Rendering, Widget lifecycle)
    FlutterError.onError = (FlutterErrorDetails details) {
      if (kDebugMode) {
        FlutterError.dumpErrorToConsole(details);
      } else {
        Zone.current.handleUncaughtError(details.exception, details.stack ?? StackTrace.empty);
      }
      errorHandler.handleFlutterError(details);
    };

    // Catch Platform/Async Errors
    PlatformDispatcher.instance.onError = (Object error, StackTrace stack) {
      errorHandler.handlePlatformError(error, stack);
      return true; // Prevents the app from crashing on some platforms
    };

    _logger.i('AppBootstrap: Bindings and Error Handling initialized.');

    // 3. Create the ProviderContainer
    // This is the root of the application state. We create it here to pre-warm
    // providers before passing it to the UI.
    final container = ProviderContainer(
      observers: [
        _AppProviderObserver(_logger),
      ],
    );

    try {
      // 4. Validate Environment Configuration
      _validateEnvironment();

      // 5. Warm-up Critical Infrastructure
      // We explicitly await the initialization of core services to ensure
      // they are ready when the UI mounts.
      await _initializeCoreServices(container);

      stopwatch.stop();
      _logger.i('AppBootstrap: Initialization completed successfully in ${stopwatch.elapsedMilliseconds}ms.');
      
      return container;
    } catch (e, stackTrace) {
      _logger.f('AppBootstrap: Critical initialization failure.', error: e, stackTrace: stackTrace);
      
      // Handle critical failure (rethrow to crash app or show fatal error screen in main)
      // For "military-grade" resilience, we ensure the error is logged and then
      // allow the main function to decide how to present the fatal state.
      rethrow;
    }
  }

  /// Validates that critical environment variables are present.
  static void _validateEnvironment() {
    if (EnvConfig.baseUrl.isEmpty) {
      throw const FormatException('Critical Configuration Error: API Base URL is missing in EnvConfig.');
    }
    _logger.d('AppBootstrap: Environment validated. API Target: ${EnvConfig.baseUrl}');
  }

  /// Initializes core services that must be ready before the app starts.
  static Future<void> _initializeCoreServices(ProviderContainer container) async {
    _logger.d('AppBootstrap: Warming up core services...');

    // Await Storage Service Initialization
    // Assuming storageServiceProvider returns the service instance and might need init.
    // If the provider is synchronous, this is fast. If async, we await it.
    // Here we simply read it to ensure the singleton is constructed.
    final storageService = container.read(storageServiceProvider);
    _logger.v('AppBootstrap: Storage Service initialized: ${storageService.runtimeType}');

    // Await Local Database (Isar) Initialization
    // isarDatabaseProvider is typically a FutureProvider, so we await .future.
    // This ensures schema migration and file opening is complete.
    await container.read(isarDatabaseProvider.future);
    _logger.v('AppBootstrap: Isar Database initialized.');

    // Initialize API Client
    // We read the provider to instantiate the client with the validated EnvConfig.
    final apiClient = container.read(apiClientProvider);
    _logger.v('AppBootstrap: API Client initialized: ${apiClient.runtimeType}');
  }
}

/// A simple observer to log Riverpod state changes for debugging purposes.
class _AppProviderObserver extends ProviderObserver {
  final Logger _logger;

  _AppProviderObserver(this._logger);

  @override
  void didUpdateProvider(
    ProviderBase<Object?> provider,
    Object? previousValue,
    Object? newValue,
    ProviderContainer container,
  ) {
    if (kDebugMode) {
      // reducing verbosity, only log explicit errors or critical state changes if needed
      if (newValue is AsyncError) {
        _logger.w('Provider ${provider.name ?? provider.runtimeType} emitted error: ${newValue.error}');
      }
    }
  }

  @override
  void didAddProvider(
    ProviderBase<Object?> provider,
    Object? value,
    ProviderContainer container,
  ) {
    if (kDebugMode) {
      // _logger.v('Provider initialized: ${provider.name ?? provider.runtimeType}');
    }
  }
  
  @override
  void didDisposeProvider(
    ProviderBase<Object?> provider, 
    ProviderContainer container,
  ) {
    if (kDebugMode) {
      // _logger.v('Provider disposed: ${provider.name ?? provider.runtimeType}');
    }
  }
}