import 'dart:io';
import 'package:dio/dio.dart';
import 'package:readtrack_client/src/services/connectivity_service.dart';

/// An interceptor that automatically retries failed network requests.
///
/// It specifically handles transient errors like network timeouts, socket exceptions,
/// and server-side 503 Service Unavailable responses. It integrates with
/// [ConnectivityService] to ensure a network connection exists before attempting
/// a retry.
class RetryInterceptor extends Interceptor {
  final Dio dio;
  final ConnectivityService connectivityService;
  final int maxRetries;
  final List<int> retryDelays;

  RetryInterceptor({
    required this.dio,
    required this.connectivityService,
    this.maxRetries = 3,
    this.retryDelays = const [1000, 2000, 4000], // Exponential backoff in ms
  });

  @override
  Future<void> onError(DioException err, ErrorInterceptorHandler handler) async {
    // Check if the error is a candidate for retry
    if (_shouldRetry(err)) {
      try {
        final attempt = (err.requestOptions.extra['retry_attempt'] as int?) ?? 0;

        if (attempt < maxRetries) {
          // Check for network connectivity before retrying
          final hasConnection = await connectivityService.hasConnection();
          if (!hasConnection) {
            // If no connection, we can't retry successfully. Pass the error.
            return super.onError(err, handler);
          }

          // Calculate delay
          final delayMs = attempt < retryDelays.length 
              ? retryDelays[attempt] 
              : retryDelays.last;
          
          await Future.delayed(Duration(milliseconds: delayMs));

          // Increment attempt count
          err.requestOptions.extra['retry_attempt'] = attempt + 1;

          // Retry the request
          final response = await dio.fetch(err.requestOptions);
          return handler.resolve(response);
        }
      } catch (e) {
        // If the retry itself fails unexpectedly, pass the original error
        // or wrap the new one. Usually, we propagate the original or the last failure.
        return super.onError(err, handler);
      }
    }

    return super.onError(err, handler);
  }

  /// Determines if a [DioException] represents a transient failure that warrants a retry.
  bool _shouldRetry(DioException err) {
    // Retry on connection timeouts, send timeouts, or receive timeouts
    if (err.type == DioExceptionType.connectionTimeout ||
        err.type == DioExceptionType.sendTimeout ||
        err.type == DioExceptionType.receiveTimeout) {
      return true;
    }

    // Retry on SocketExceptions (often network drops)
    if (err.type == DioExceptionType.unknown &&
        err.error != null &&
        err.error is SocketException) {
      return true;
    }

    // Retry on server-side 503 (Service Unavailable)
    if (err.type == DioExceptionType.badResponse &&
        err.response?.statusCode == 503) {
      return true;
    }

    // Do not retry on 4xx (client errors) or other 5xx errors unless specified
    return false;
  }
}