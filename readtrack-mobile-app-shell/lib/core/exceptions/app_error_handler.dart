import 'package:flutter/foundation.dart';
import 'package:flutter/widgets.dart';
import 'package:logger/logger.dart';

/// A centralized service for handling global application errors.
///
/// This service is responsible for catching, logging, and reporting unhandled exceptions
/// from both the Flutter framework (UI rendering errors) and the underlying platform
/// (asynchronous Dart errors). It serves as the primary integration point for
/// logging services and crash reporting tools.
class AppErrorHandler {
  /// Internal logger instance for console output.
  final Logger _logger;

  /// Creates an instance of [AppErrorHandler].
  ///
  /// Typically registered as a singleton or provider within the dependency injection system.
  AppErrorHandler()
      : _logger = Logger(
          printer: PrettyPrinter(
            methodCount: 2,
            errorMethodCount: 8,
            lineLength: 120,
            colors: true,
            printEmojis: true,
            printTime: true,
          ),
        );

  /// Handles errors caught by the Flutter framework.
  ///
  /// This method should be assigned to `FlutterError.onError` in the app bootstrap phase.
  /// It logs the error details to the console and can be extended to report to
  /// external crash tracking services like Firebase Crashlytics or Sentry.
  ///
  /// @param details The structured error details provided by Flutter.
  void handleFlutterError(FlutterErrorDetails details) {
    _logger.e(
      'Flutter Framework Error Captured',
      error: details.exception,
      stackTrace: details.stack,
    );

    // In production, this would also send to Crashlytics:
    // FirebaseCrashlytics.instance.recordFlutterError(details);

    // Ensure the error is also dumped to the console in debug mode for visibility
    if (kDebugMode) {
      FlutterError.dumpErrorToConsole(details);
    }
  }

  /// Handles asynchronous errors caught by the platform dispatcher.
  ///
  /// This method should be assigned to `PlatformDispatcher.instance.onError` in the
  /// app bootstrap phase. It handles errors that occur outside the widget lifecycle,
  /// such as in Futures or microtasks.
  ///
  /// @param error The exception object.
  /// @param stack The stack trace associated with the error.
  /// @return `true` to indicate the error has been handled and the app should not crash;
  ///         `false` to propagate the error (which usually crashes the app).
  bool handlePlatformError(Object error, StackTrace stack) {
    _logger.f(
      'Uncaught Platform Error Captured',
      error: error,
      stackTrace: stack,
    );

    // In production, this would also send to Crashlytics:
    // FirebaseCrashlytics.instance.recordError(error, stack, fatal: true);

    // We return true to prevent the app from crashing immediately on the native side,
    // allowing for a potential graceful degradation or error screen display.
    return true;
  }

  /// Logs a non-fatal error or warning that was caught manually within the app logic.
  ///
  /// @param message A description of the error context.
  /// @param error The exception object (optional).
  /// @param stackTrace The stack trace (optional).
  void logWarning(String message, [Object? error, StackTrace? stackTrace]) {
    _logger.w(message, error: error, stackTrace: stackTrace);
  }

  /// Logs a standard info message for tracking application flow.
  ///
  /// @param message The information to log.
  void logInfo(String message) {
    _logger.i(message);
  }
}