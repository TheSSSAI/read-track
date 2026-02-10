import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';
import '../../local/secure_storage_service.dart';
import '../../../config/api_constants.dart';

/// Interceptor to inject JWT tokens into requests and handle automatic token refresh.
/// 
/// Extends [QueuedInterceptor] to process requests sequentially. If a 401 occurs,
/// subsequent requests are paused until the token refresh logic completes.
class AuthInterceptor extends QueuedInterceptor {
  final SecureStorageService _secureStorageService;
  final Dio _tokenDio;

  AuthInterceptor(this._secureStorageService) 
      : _tokenDio = Dio(BaseOptions(baseUrl: ApiConstants.baseUrl));

  @override
  Future<void> onRequest(
    RequestOptions options,
    RequestInterceptorHandler handler,
  ) async {
    // Skip token injection for public endpoints if tagged
    if (options.extra['isPublic'] == true) {
      return handler.next(options);
    }

    try {
      final accessToken = await _secureStorageService.getAccessToken();
      if (accessToken != null) {
        options.headers['Authorization'] = 'Bearer $accessToken';
      }
      return handler.next(options);
    } catch (e) {
      // If we can't read the token, proceed without it; 
      // the server will return 401 if it's required.
      debugPrint('AuthInterceptor: Failed to read access token: $e');
      return handler.next(options);
    }
  }

  @override
  Future<void> onError(
    DioException err,
    ErrorInterceptorHandler handler,
  ) async {
    // Only handle 401 Unauthorized errors
    if (err.response?.statusCode != 401) {
      return handler.next(err);
    }

    // Check if we have a refresh token
    final refreshToken = await _secureStorageService.getRefreshToken();
    if (refreshToken == null) {
      // No refresh token available, cannot refresh. Force logout flow via error.
      await _performLocalLogout();
      return handler.next(err);
    }

    // Attempt to refresh the token
    try {
      debugPrint('AuthInterceptor: 401 detected, attempting token refresh...');
      
      // Use a separate Dio instance to avoid interceptor loops
      final response = await _tokenDio.post(
        ApiConstants.refreshEndpoint,
        data: {'refreshToken': refreshToken},
      );

      if (response.statusCode == 200 && response.data != null) {
        final newAccessToken = response.data['accessToken'];
        final newRefreshToken = response.data['refreshToken'];

        if (newAccessToken != null && newRefreshToken != null) {
          // Save new tokens
          await _secureStorageService.saveTokens(
            accessToken: newAccessToken,
            refreshToken: newRefreshToken,
          );

          // Retry the original request with the new token
          final options = err.requestOptions;
          options.headers['Authorization'] = 'Bearer $newAccessToken';
          
          // Create a new Dio instance for the retry to ensure clean state
          // or use the original dio instance if accessible. 
          // Since we are inside an interceptor, we should avoid using the original Dio
          // if it might trigger the queue logic again in a complex way, 
          // but resolve() usually handles this.
          // Using a basic fetch here is safer.
          final retryDio = Dio(BaseOptions(
            baseUrl: options.baseUrl,
            contentType: options.contentType,
            responseType: options.responseType,
            headers: options.headers, // Includes new auth header
          ));

          final retryResponse = await retryDio.request(
            options.path,
            data: options.data,
            queryParameters: options.queryParameters,
            options: Options(
              method: options.method,
              headers: options.headers,
            ),
          );

          return handler.resolve(retryResponse);
        }
      }
      
      // If refresh failed (e.g. invalid response format), logout
      throw DioException(requestOptions: err.requestOptions, error: 'Token refresh failed');

    } catch (refreshError) {
      debugPrint('AuthInterceptor: Token refresh failed: $refreshError');
      // If refresh fails (e.g. refresh token expired), clear local storage
      await _performLocalLogout();
      // Reject with the ORIGINAL error (401) or the refresh error depending on requirement.
      // Usually rejecting with the refresh error or the original 401 signals the app to logout.
      return handler.next(err);
    }
  }

  Future<void> _performLocalLogout() async {
    await _secureStorageService.clearTokens();
  }
}