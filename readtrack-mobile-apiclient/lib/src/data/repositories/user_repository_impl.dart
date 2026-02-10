import 'dart:async';

import 'package:dartz/dartz.dart';
import 'package:dio/dio.dart';

import '../../services/connectivity_service.dart';
import '../datasources/local/interfaces/auth_local_source.dart';
import '../datasources/remote/interfaces/auth_remote_source.dart';
import '../models/user_dto.dart';

/// Implementation of the UserRepository.
/// 
/// Handles authentication, user profile management, and session persistence.
/// Unlike library data, authentication actions (Login/Register) typically require
/// immediate network connectivity and cannot be performed purely offline 
/// (except for session restoration).
class UserRepositoryImpl {
  final IAuthLocalSource _localSource;
  final IAuthRemoteSource _remoteSource;
  final ConnectivityService _connectivityService;

  UserRepositoryImpl({
    required IAuthLocalSource localSource,
    required IAuthRemoteSource remoteSource,
    required ConnectivityService connectivityService,
  })  : _localSource = localSource,
        _remoteSource = remoteSource,
        _connectivityService = connectivityService;

  /// Authenticates a user using credentials.
  /// 
  /// 1. Validates connectivity.
  /// 2. Calls remote API.
  /// 3. Persists Access/Refresh tokens and User Profile locally.
  Future<Either<Exception, UserDto>> login(String email, String password) async {
    if (!await _connectivityService.isConnected) {
      return Left(Exception('No Internet Connection'));
    }

    try {
      final response = await _remoteSource.login(email, password);
      
      // Persist sensitive tokens securely
      await _localSource.saveTokens(
        accessToken: response.accessToken, 
        refreshToken: response.refreshToken
      );
      
      // Persist user profile for offline access
      await _localSource.saveUserProfile(response.user);
      
      return Right(response.user);
    } on DioException catch (e) {
      return Left(Exception(_mapDioError(e)));
    } catch (e) {
      return Left(Exception('Login failed: ${e.toString()}'));
    }
  }

  /// Registers a new user.
  Future<Either<Exception, UserDto>> register({
    required String email, 
    required String password, 
    required String displayName
  }) async {
    if (!await _connectivityService.isConnected) {
      return Left(Exception('No Internet Connection'));
    }

    try {
      final response = await _remoteSource.register(email, password, displayName);
      
      await _localSource.saveTokens(
        accessToken: response.accessToken, 
        refreshToken: response.refreshToken
      );
      
      await _localSource.saveUserProfile(response.user);
      
      return Right(response.user);
    } on DioException catch (e) {
      return Left(Exception(_mapDioError(e)));
    } catch (e) {
      return Left(Exception('Registration failed: ${e.toString()}'));
    }
  }

  /// Retrieves the current user profile.
  /// 
  /// Strategy: Cache-First.
  /// Returns the local profile immediately.
  /// If connected, attempts to refresh the profile from the server in the background (optional).
  Future<Either<Exception, UserDto?>> getCurrentUser() async {
    try {
      final localUser = await _localSource.getUserProfile();
      
      if (localUser != null && await _connectivityService.isConnected) {
        try {
          // Background refresh
          final remoteUser = await _remoteSource.getUserProfile();
          if (remoteUser != localUser) {
            await _localSource.saveUserProfile(remoteUser);
            return Right(remoteUser);
          }
        } catch (_) {
          // If remote fails, just return local
        }
      }
      
      return Right(localUser);
    } catch (e) {
      return Left(Exception('Failed to get user profile: ${e.toString()}'));
    }
  }

  /// Logs out the user.
  /// 
  /// 1. Clears local storage (Tokens & Profile).
  /// 2. Attempts best-effort remote logout (to revoke refresh token).
  Future<Either<Exception, void>> logout() async {
    try {
      await _localSource.clearSession();
      
      if (await _connectivityService.isConnected) {
        try {
          await _remoteSource.logout();
        } catch (_) {
          // Ignored, local session is already cleared
        }
      }
      return const Right(null);
    } catch (e) {
      return Left(Exception('Logout failed: ${e.toString()}'));
    }
  }

  /// Authenticates using a third-party provider (Google/Apple).
  Future<Either<Exception, UserDto>> socialLogin(String provider, String token) async {
    if (!await _connectivityService.isConnected) {
      return Left(Exception('No Internet Connection'));
    }

    try {
      final response = await _remoteSource.socialLogin(provider, token);
      
      await _localSource.saveTokens(
        accessToken: response.accessToken, 
        refreshToken: response.refreshToken
      );
      await _localSource.saveUserProfile(response.user);
      
      return Right(response.user);
    } on DioException catch (e) {
      return Left(Exception(_mapDioError(e)));
    } catch (e) {
      return Left(Exception('Social login failed: ${e.toString()}'));
    }
  }

  String _mapDioError(DioException e) {
    if (e.response != null) {
      // Return server message if available
      final data = e.response?.data;
      if (data is Map<String, dynamic> && data.containsKey('message')) {
        return data['message'].toString();
      }
      return 'Server Error: ${e.response?.statusCode}';
    }
    return 'Network Error';
  }
}