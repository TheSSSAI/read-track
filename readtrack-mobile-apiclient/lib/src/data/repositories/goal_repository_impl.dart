import 'dart:async';

import 'package:dartz/dartz.dart';
import 'package:dio/dio.dart';

import '../../services/connectivity_service.dart';
import '../models/goal_dto.dart';
import '../models/sync_enums.dart';

// Assuming interfaces exist as per architectural pattern, even if implied in file lists.
// Creating abstraction stubs here if they were strictly not present in previous levels
// to ensure compilation of this level 4 code. In a real scenario, these imports point to Level 2.
abstract class IGoalLocalSource {
  Future<List<GoalDto>> getGoals();
  Future<void> putGoal(GoalDto goal);
  Future<void> deleteGoal(int id);
}

abstract class IGoalRemoteSource {
  Future<List<GoalDto>> fetchGoals();
  Future<GoalDto> createGoal(GoalDto goal);
  Future<GoalDto> updateGoal(GoalDto goal);
  Future<void> deleteGoal(String serverId);
}

/// Implementation of the GoalRepository.
/// 
/// Manages reading goals with Offline-First capabilities.
class GoalRepositoryImpl {
  final IGoalLocalSource _localSource;
  final IGoalRemoteSource _remoteSource;
  final ConnectivityService _connectivityService;

  GoalRepositoryImpl({
    required IGoalLocalSource localSource,
    required IGoalRemoteSource remoteSource,
    required ConnectivityService connectivityService,
  })  : _localSource = localSource,
        _remoteSource = remoteSource,
        _connectivityService = connectivityService;

  /// Retrieves all goals for the current user.
  Future<Either<Exception, List<GoalDto>>> getGoals() async {
    try {
      // 1. Get from Local Cache
      final localGoals = await _localSource.getGoals();
      
      // 2. Trigger background refresh if connected and needed
      // (Implementation of background refresh is typically in SynchronizationService,
      // but simple fetch-on-read can optionally be done here for freshness)
      if (await _connectivityService.isConnected && localGoals.isEmpty) {
         try {
           final remoteGoals = await _remoteSource.fetchGoals();
           for (var goal in remoteGoals) {
             await _localSource.putGoal(goal.copyWith(syncStatus: SyncStatus.Synced));
           }
           return Right(remoteGoals);
         } catch (_) {
           // Fallback to local (empty)
         }
      }

      return Right(localGoals);
    } catch (e) {
      return Left(Exception('Failed to fetch goals: ${e.toString()}'));
    }
  }

  /// Creates a new reading goal.
  Future<Either<Exception, void>> createGoal(GoalDto goal) async {
    try {
      final optimisticGoal = goal.copyWith(
        syncStatus: SyncStatus.Created,
        updatedAt: DateTime.now().toUtc(),
      );

      // Save locally first
      await _localSource.putGoal(optimisticGoal);

      if (await _connectivityService.isConnected) {
        try {
          final serverGoal = await _remoteSource.createGoal(optimisticGoal);
          
          // Update local with server ID and synced status
          final syncedGoal = serverGoal.copyWith(
            id: optimisticGoal.id, // Maintain Isar ID
            syncStatus: SyncStatus.Synced,
          );
          await _localSource.putGoal(syncedGoal);
        } catch (e) {
          // Validate if it's a business logic error (e.g. Max Goals Reached)
          if (e is DioException && (e.response?.statusCode == 403 || e.response?.statusCode == 402)) {
            await _localSource.deleteGoal(optimisticGoal.id);
            return Left(Exception('Goal limit reached. Please upgrade to Premium.'));
          }
          // Otherwise leave as Created for retry
        }
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to create goal: ${e.toString()}'));
    }
  }

  /// Updates an existing goal.
  Future<Either<Exception, void>> updateGoal(GoalDto goal) async {
    try {
      final optimisticGoal = goal.copyWith(
        syncStatus: SyncStatus.Updated,
        updatedAt: DateTime.now().toUtc(),
      );

      await _localSource.putGoal(optimisticGoal);

      if (await _connectivityService.isConnected) {
        try {
          final serverGoal = await _remoteSource.updateGoal(optimisticGoal);
          
          final syncedGoal = serverGoal.copyWith(
            id: optimisticGoal.id,
            syncStatus: SyncStatus.Synced,
          );
          await _localSource.putGoal(syncedGoal);
        } catch (e) {
          // Leave as Updated
        }
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to update goal: ${e.toString()}'));
    }
  }

  /// Deletes a goal.
  Future<Either<Exception, void>> deleteGoal(int id, String? serverId) async {
    try {
      // Soft delete locally first
      final goals = await _localSource.getGoals();
      final target = goals.cast<GoalDto?>().firstWhere((g) => g?.id == id, orElse: () => null);
      
      if (target != null) {
        final deletedGoal = target.copyWith(
          syncStatus: SyncStatus.Deleted,
          updatedAt: DateTime.now().toUtc(),
        );
        await _localSource.putGoal(deletedGoal);
      }

      if (await _connectivityService.isConnected && serverId != null) {
        try {
          await _remoteSource.deleteGoal(serverId);
          // Hard delete locally on success
          await _localSource.deleteGoal(id);
        } catch (e) {
          // Leave as Deleted
        }
      } else if (serverId == null) {
        // Not on server, safe to hard delete
        await _localSource.deleteGoal(id);
      }

      return const Right(null);
    } catch (e) {
      return Left(Exception('Failed to delete goal: ${e.toString()}'));
    }
  }
}