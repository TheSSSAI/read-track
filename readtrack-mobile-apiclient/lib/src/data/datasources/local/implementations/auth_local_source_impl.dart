import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:isar/isar.dart';
import '../../../../data/models/user_dto.dart';
import '../interfaces/auth_local_source.dart';
import 'package:readtrack_domain/readtrack_domain.dart';

/// Concrete implementation of [IAuthLocalSource].
/// 
/// Uses [FlutterSecureStorage] for sensitive token data.
/// Uses [Isar] for caching the user profile data.
class AuthLocalSourceImpl implements IAuthLocalSource {
  final FlutterSecureStorage _secureStorage;
  final Isar _isar;

  static const String _accessTokenKey = 'access_token';
  static const String _refreshTokenKey = 'refresh_token';

  AuthLocalSourceImpl(this._secureStorage, this._isar);

  @override
  Future<String?> getAccessToken() async {
    try {
      return await _secureStorage.read(key: _accessTokenKey);
    } catch (e) {
      throw CacheException(message: 'Failed to read access token: $e');
    }
  }

  @override
  Future<String?> getRefreshToken() async {
    try {
      return await _secureStorage.read(key: _refreshTokenKey);
    } catch (e) {
      throw CacheException(message: 'Failed to read refresh token: $e');
    }
  }

  @override
  Future<void> saveTokens({required String accessToken, required String refreshToken}) async {
    try {
      await Future.wait([
        _secureStorage.write(key: _accessTokenKey, value: accessToken),
        _secureStorage.write(key: _refreshTokenKey, value: refreshToken),
      ]);
    } catch (e) {
      throw CacheException(message: 'Failed to save tokens: $e');
    }
  }

  @override
  Future<void> clearTokens() async {
    try {
      await Future.wait([
        _secureStorage.delete(key: _accessTokenKey),
        _secureStorage.delete(key: _refreshTokenKey),
      ]);
    } catch (e) {
      throw CacheException(message: 'Failed to clear tokens: $e');
    }
  }

  @override
  Future<UserDto?> getUser() async {
    try {
      // Assuming single user per device/session paradigm for Isar
      return await _isar.userDtos.where().findFirst();
    } catch (e) {
      throw CacheException(message: 'Failed to get cached user profile: $e');
    }
  }

  @override
  Future<void> saveUser(UserDto user) async {
    try {
      await _isar.writeTxn(() async {
        // Clear previous user data to ensure single source of truth
        await _isar.userDtos.clear();
        await _isar.userDtos.put(user);
      });
    } catch (e) {
      throw CacheException(message: 'Failed to save user profile: $e');
    }
  }

  @override
  Future<void> clearUser() async {
    try {
      await _isar.writeTxn(() async {
        await _isar.userDtos.clear();
      });
    } catch (e) {
      throw CacheException(message: 'Failed to clear user profile: $e');
    }
  }
}