import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

import 'app.dart';
import 'app_bootstrap.dart';

/// The entry point of the ReadTrack application.
///
/// This file acts as the Composition Root for the Flutter application.
/// Its responsibilities are strictly limited to:
/// 1. Establishing an error-handling zone for top-level exceptions.
/// 2. Invoking the [AppBootstrap] to perform asynchronous initialization.
/// 3. Injecting the global [ProviderScope] for Riverpod state management.
/// 4. Mounting the root [ReadTrackApp] widget.
void main() {
  // We use runZonedGuarded to catch any errors that might occur during the 
  // asynchronous bootstrapping phase or that escape the Flutter framework's 
  // standard error handling mechanisms.
  runZonedGuarded<Future<void>>(
    () async {
      // Create an instance of the bootstrap orchestrator.
      final appBootstrap = AppBootstrap();

      // Initialize core dependencies, configuration, and environment variables.
      // This method handles:
      // - WidgetsFlutterBinding.ensureInitialized()
      // - AppErrorHandler configuration (FlutterError.onError, PlatformDispatcher)
      // - Logger initialization
      // - Environment configuration loading (EnvConfig)
      // - Local storage initialization
      await appBootstrap.init();

      // Mount the application UI.
      // The ProviderScope is the root of the Riverpod state management system.
      // It creates the container where all providers' states are stored.
      runApp(
        const ProviderScope(
          child: ReadTrackApp(),
        ),
      );
    },
    (error, stackTrace) {
      // This callback catches uncaught asynchronous errors within the Zone.
      // In a production environment, this is the last line of defense for 
      // reporting crashes that occur before the error reporting service is 
      // fully initialized or for errors that bypass the PlatformDispatcher.
      
      // We use debugPrint here as a fallback because the Logger might not be
      // initialized if the bootstrap failed early.
      debugPrint('CRITICAL: Uncaught exception in main application zone.');
      debugPrint('Error: $error');
      debugPrint('StackTrace: $stackTrace');
    },
  );
}