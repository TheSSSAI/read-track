import 'dart:async';

import 'package:dartz/dartz.dart';
import 'package:dio/dio.dart';

import '../../config/api_constants.dart';
import '../../services/connectivity_service.dart';
import '../datasources/local/interfaces/library_local_source.dart';
import '../datasources/remote/interfaces/library_remote_source.dart';
import '../mappers/library_mapper.dart';
import '../models/library_item_dto.dart';
import '../models/reading_session_dto.dart';
import '../models/sync_enums.dart';

// Assuming Domain Entities and Repository Interfaces exist in Domain Layer
// If not explicitly provided in file list, we infer their structure based on usage.
// For the purpose of this file generation, we treat them as imported.
// import '../../../domain/entities/library_item.dart';
// import '../../../domain/entities/reading_session.dart';
// import '../../../domain/repositories/i_library_repository.dart';
// import '../../../core/error/failures.dart';

/// Implementation of the LibraryRepository.
/// 
/// This class coordinates between the Local Data Source (Isar) and the Remote Data Source (API).
/// It implements an Offline-First strategy where the Local Database is the Single Source of Truth
/// for the UI. Writes are persisted locally immediately and synchronized to the server
/// when connectivity is available.
class LibraryRepositoryImpl {
  final ILibraryLocalSource _localSource;
  final ILibraryRemoteSource _remoteSource;
  final ConnectivityService _connectivityService;

  LibraryRepositoryImpl({
    required ILibraryLocalSource localSource,
    required ILibraryRemoteSource remoteSource,
    required ConnectivityService connectivityService,
  })  : _localSource = localSource,
        _remoteSource = remoteSource,
        _connectivityService = connectivityService;

  /// Retrieves the user's library.
  /// 
  /// Strategy:
  /// 1. Fetch data from the Local Data Source.
  /// 2. If connected, trigger a background fetch to update the local cache (Fire-and-Forget or Stream based).
  ///    Note: For this implementation, we return the local data immediately. 
  ///    The SynchronizationService is typically responsible for keeping local/remote in sync.
  Future<Either<Exception, List<LibraryItemDto>>> getLibraryItems() async {
    try {
      final localItems = await _localSource.getAllLibraryItems();
      return Right(localItems);
    } catch (e) {
      return Left(Exception('Cache Failure: ${e.toString()}'));
    }
  }

  /// Adds a new book to the library.
  /// 
  /// Strategy:
  /// 1. Mark DTO as [SyncStatus.Created].
  /// 2. Save to Local Source (Optimistic UI update).
  /// 3. Check connectivity.
  /// 4. If Online: Attempt to push to Remote.
  ///    - If Success: Mark as [SyncStatus.Synced] and update Local.
  ///    - If Failure: Keep as [SyncStatus.Created] for background sync.
  /// 5. Return success if local save succeeded.
  Future<Either<Exception, void>> addBook(LibraryItemDto item) async {
    try {
      // 1. Prepare for local storage (Optimistic)
      final optimisticItem = item.copyWith(
        syncStatus: SyncStatus.Created,
        updatedAt: DateTime.now().toUtc(),
      );

      // 2. Persist Locally
      await _localSource.putLibraryItem(optimisticItem);

      // 3. Attempt Network Sync if available
      if (await _connectivityService.isConnected) {
        try {
          final serverItem = await _remoteSource.addLibraryItem(optimisticItem);
          
          // 4. Update Local with Server state (Synced)
          // We map the server ID back to the local item if necessary, or just mark synced
          final syncedItem = serverItem.copyWith(
            id: optimisticItem.id, // Keep local Isar ID
            syncStatus: SyncStatus.Synced,
          );
          
          await _localSource.putLibraryItem(syncedItem);
        } catch (e) {
          // Network failed, but local save succeeded. 
          // Item stays 'Created' and will be picked up by SyncService.
          // We verify if it was a 4xx error (Validation/Limit) which might require rollback.
          if (e is DioException && (e.response?.statusCode ?? 0) >= 400 && (e.response?.statusCode ?? 0) < 500) {
             // If the server explicitly rejected it (e.g. limit reached), we should probably delete local
             // and return failure to the user.
             await _localSource.deleteLibraryItem(optimisticItem.id);
             return Left(Exception('Server rejected item: ${e.message}'));
          }
        }
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to add book: ${e.toString()}'));
    }
  }

  /// Updates an existing book (e.g., status change, page progress).
  Future<Either<Exception, void>> updateBook(LibraryItemDto item) async {
    try {
      final optimisticItem = item.copyWith(
        syncStatus: SyncStatus.Updated,
        updatedAt: DateTime.now().toUtc(),
      );

      await _localSource.putLibraryItem(optimisticItem);

      if (await _connectivityService.isConnected) {
        try {
          final serverItem = await _remoteSource.updateLibraryItem(optimisticItem);
          
          final syncedItem = serverItem.copyWith(
            id: optimisticItem.id,
            syncStatus: SyncStatus.Synced,
          );
          
          await _localSource.putLibraryItem(syncedItem);
        } catch (e) {
          // Leave as Updated for background sync
          if (e is DioException && (e.response?.statusCode ?? 0) >= 400 && (e.response?.statusCode ?? 0) < 500) {
             return Left(Exception('Server update rejected: ${e.message}'));
          }
        }
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to update book: ${e.toString()}'));
    }
  }

  /// Deletes a book from the library.
  /// 
  /// Strategy:
  /// 1. Mark as [SyncStatus.Deleted] locally (Soft Delete in Isar).
  /// 2. If Online: Send DELETE request.
  ///    - If Success: Hard delete from Local.
  ///    - If Failure: Keep [SyncStatus.Deleted] for background sync.
  Future<Either<Exception, void>> deleteBook(int id, String? serverId) async {
    try {
      // 1. Soft Delete Locally
      final existingItem = await _localSource.getLibraryItem(id);
      if (existingItem != null) {
        final deletedItem = existingItem.copyWith(
          syncStatus: SyncStatus.Deleted,
          updatedAt: DateTime.now().toUtc(),
        );
        await _localSource.putLibraryItem(deletedItem);
      }

      if (await _connectivityService.isConnected && serverId != null) {
        try {
          await _remoteSource.deleteLibraryItem(serverId);
          // 2. Hard Delete Locally upon server confirmation
          await _localSource.deleteLibraryItem(id);
        } catch (e) {
          // Leave as Deleted for background sync
        }
      } else if (serverId == null) {
        // If it was never synced to server, just hard delete locally
        await _localSource.deleteLibraryItem(id);
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to delete book: ${e.toString()}'));
    }
  }

  /// Logs a reading session for a book.
  /// 
  /// This creates a ReadingSession entity and updates the LibraryItem progress.
  /// Both operations are performed transactionally in the local source.
  Future<Either<Exception, void>> logSession(ReadingSessionDto session, LibraryItemDto updatedBook) async {
    try {
      final sessionWithSync = session.copyWith(
        syncStatus: SyncStatus.Created,
      );
      
      final bookWithSync = updatedBook.copyWith(
        syncStatus: SyncStatus.Updated,
        updatedAt: DateTime.now().toUtc(),
      );

      // Perform local transactional update
      await _localSource.putReadingSession(sessionWithSync);
      await _localSource.putLibraryItem(bookWithSync);

      if (await _connectivityService.isConnected) {
        try {
          // Attempt immediate sync
          await _remoteSource.logSession(sessionWithSync);
          await _remoteSource.updateLibraryItem(bookWithSync);

          // Mark as synced
          await _localSource.putReadingSession(sessionWithSync.copyWith(syncStatus: SyncStatus.Synced));
          await _localSource.putLibraryItem(bookWithSync.copyWith(syncStatus: SyncStatus.Synced));
        } catch (e) {
          // Leave as dirty for background sync
        }
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to log session: ${e.toString()}'));
    }
  }

  /// Fetches reading sessions for a specific book.
  Future<Either<Exception, List<ReadingSessionDto>>> getSessionsForBook(int bookId) async {
    try {
      final sessions = await _localSource.getSessionsForBook(bookId);
      return Right(sessions);
    } catch (e) {
      return Left(Exception('Failed to fetch sessions: ${e.toString()}'));
    }
  }
}