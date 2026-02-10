import 'dart:convert';
import 'package:flutter/foundation.dart';
import 'package:dio/dio.dart';
import '../../../../config/api_constants.dart';
import '../../../../data/models/library_item_dto.dart';
import '../../../../data/models/sync_enums.dart';
import '../interfaces/library_remote_source.dart';
import 'package:readtrack_domain/readtrack_domain.dart';

/// Concrete implementation of [ILibraryRemoteSource] using Dio.
class LibraryRemoteSourceImpl implements ILibraryRemoteSource {
  final Dio _dio;

  LibraryRemoteSourceImpl(this._dio);

  @override
  Future<List<LibraryItemDto>> fetchLibrary(DateTime? updatedAfter) async {
    try {
      final queryParams = <String, dynamic>{};
      if (updatedAfter != null) {
        queryParams['updatedAfter'] = updatedAfter.toIso8601String();
      }

      final response = await _dio.get(
        ApiConstants.libraryEndpoint,
        queryParameters: queryParams,
      );

      if (response.statusCode == 200) {
        final List<dynamic> data = response.data['items'];
        
        // Use compute for parsing if list is large, though simple mapping is shown here
        // for reliability within the generated context.
        return data.map((json) => LibraryItemDto.fromJson(json)).toList();
      } else {
        throw ServerException(
          message: 'Failed to fetch library', 
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.message ?? 'Network error during fetch',
        statusCode: e.response?.statusCode,
      );
    } catch (e) {
      throw ServerException(message: 'Unexpected error: $e');
    }
  }

  @override
  Future<LibraryItemDto> createItem(LibraryItemDto item) async {
    try {
      final response = await _dio.post(
        ApiConstants.libraryEndpoint,
        data: item.toJson(),
      );

      if (response.statusCode == 201) {
        return LibraryItemDto.fromJson(response.data);
      } else {
        throw ServerException(
          message: 'Failed to create item',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.message ?? 'Network error during create',
        statusCode: e.response?.statusCode,
      );
    }
  }

  @override
  Future<LibraryItemDto> updateItem(LibraryItemDto item) async {
    try {
      if (item.serverId == null) throw const ServerException(message: 'Server ID required for update');

      final response = await _dio.put(
        '${ApiConstants.libraryEndpoint}/${item.serverId}',
        data: item.toJson(),
      );

      if (response.statusCode == 200) {
        return LibraryItemDto.fromJson(response.data);
      } else {
        throw ServerException(
          message: 'Failed to update item',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.message ?? 'Network error during update',
        statusCode: e.response?.statusCode,
      );
    }
  }

  @override
  Future<void> deleteItem(String serverId) async {
    try {
      final response = await _dio.delete(
        '${ApiConstants.libraryEndpoint}/$serverId',
      );

      if (response.statusCode != 204 && response.statusCode != 200) {
        throw ServerException(
          message: 'Failed to delete item',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.message ?? 'Network error during delete',
        statusCode: e.response?.statusCode,
      );
    }
  }

  @override
  Future<void> syncBatch(List<LibraryItemDto> items) async {
    try {
      final payload = items.map((e) {
        final json = e.toJson();
        // Add meta-data for the backend to handle the operation type based on syncStatus
        json['operation'] = _mapSyncStatusToOperation(e.syncStatus);
        return json;
      }).toList();

      final response = await _dio.post(
        ApiConstants.libraryBatchEndpoint,
        data: jsonEncode({'batch': payload}),
      );

      if (response.statusCode != 200) {
        throw ServerException(
          message: 'Batch sync failed',
          statusCode: response.statusCode,
        );
      }
    } on DioException catch (e) {
      throw ServerException(
        message: e.message ?? 'Network error during batch sync',
        statusCode: e.response?.statusCode,
      );
    }
  }

  String _mapSyncStatusToOperation(SyncStatus status) {
    switch (status) {
      case SyncStatus.Created: return 'create';
      case SyncStatus.Updated: return 'update';
      case SyncStatus.Deleted: return 'delete';
      default: return 'none';
    }
  }
}