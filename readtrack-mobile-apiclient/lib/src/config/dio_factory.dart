import 'package:dio/dio.dart';
import 'package:readtrack_client/src/config/api_constants.dart';

/// Factory class for creating and configuring Dio instances.
/// 
/// This class centralizes the HTTP client configuration, ensuring consistent
/// timeouts, base URLs, and header configurations across the application.
class DioFactory {
  /// Creates a configured [Dio] instance.
  ///
  /// [interceptors] can be passed to attach specific behaviors like 
  /// authentication, logging, or retries.
  /// 
  /// The [baseUrl] defaults to the value in [ApiConstants] but can be overridden
  /// for testing or specific environment configurations.
  static Dio createDio({
    String? baseUrl,
    List<Interceptor>? interceptors,
  }) {
    final options = BaseOptions(
      baseUrl: baseUrl ?? ApiConstants.baseUrl,
      connectTimeout: const Duration(milliseconds: ApiConstants.connectTimeoutMs),
      receiveTimeout: const Duration(milliseconds: ApiConstants.receiveTimeoutMs),
      sendTimeout: const Duration(milliseconds: ApiConstants.sendTimeoutMs),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
      // Ensure we validate status codes correctly. 
      // We generally want to treat 200-299 as success.
      validateStatus: (status) {
        return status != null && status >= 200 && status < 300;
      },
    );

    final dio = Dio(options);

    if (interceptors != null && interceptors.isNotEmpty) {
      dio.interceptors.addAll(interceptors);
    }

    // Add a default LogInterceptor in debug mode if needed, 
    // though typically handled by specific logging configurations.
    
    return dio;
  }
}