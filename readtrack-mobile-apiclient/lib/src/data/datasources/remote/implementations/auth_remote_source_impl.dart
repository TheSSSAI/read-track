import 'package:dio/dio.dart';
import '../../../../config/api_constants.dart';
import '../../../../data/models/user_dto.dart';
import '../interfaces/auth_remote_source.dart';
import 'package:readtrack_domain/readtrack_domain.dart';

/// Concrete implementation of [IAuthRemoteSource].
class AuthRemoteSourceImpl implements IAuthRemoteSource {
  final Dio _dio;

  AuthRemoteSourceImpl(this._dio);

  @override
  Future<Map<String, dynamic>> login(String email, String password) async {
    try {
      final response = await _dio.post(
        ApiConstants.loginEndpoint,
        data: {
          'email': email,
          'password': password,
        },
      );

      if (response.statusCode == 200) {
        // Expected structure: { 'token': '...', 'refreshToken': '...', 'user': {...} }
        return response.data;
      } else {
        throw ServerException(
          message: 'Login failed',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.response?.data['message'] ?? e.message ?? 'Login connection failed',
        statusCode: e.response?.statusCode,
      );
    }
  }

  @override
  Future<Map<String, dynamic>> register(String username, String email, String password) async {
    try {
      final response = await _dio.post(
        ApiConstants.registerEndpoint,
        data: {
          'username': username,
          'email': email,
          'password': password,
        },
      );

      if (response.statusCode == 201) {
        return response.data;
      } else {
        throw ServerException(
          message: 'Registration failed',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.response?.data['message'] ?? e.message ?? 'Registration connection failed',
        statusCode: e.response?.statusCode,
      );
    }
  }

  @override
  Future<Map<String, String>> refreshToken(String refreshToken) async {
    try {
      // Create a separate Dio instance or ensure this request ignores the AuthInterceptor 
      // to avoid circular dependency loop if the interceptor uses this method.
      // Usually, the interceptor handles the refresh logic internally, but if this method 
      // is exposed for manual refresh, we must be careful.
      
      final response = await _dio.post(
        ApiConstants.refreshEndpoint,
        data: {
          'refreshToken': refreshToken,
        },
        options: Options(
          headers: {'requiresToken': false}, // Flag for interceptors to ignore
        ),
      );

      if (response.statusCode == 200) {
        return {
          'accessToken': response.data['accessToken'],
          'refreshToken': response.data['refreshToken'],
        };
      } else {
        throw ServerException(
          message: 'Token refresh failed',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: 'Token refresh connection failed',
        statusCode: e.response?.statusCode,
      );
    }
  }

  @override
  Future<UserDto> fetchUserProfile() async {
    try {
      final response = await _dio.get(ApiConstants.userProfileEndpoint);

      if (response.statusCode == 200) {
        return UserDto.fromJson(response.data);
      } else {
        throw ServerException(
          message: 'Failed to fetch user profile',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.message ?? 'Profile fetch failed',
        statusCode: e.response?.statusCode,
      );
    }
  }
}