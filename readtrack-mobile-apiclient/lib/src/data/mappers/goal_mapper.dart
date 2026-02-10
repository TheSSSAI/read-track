import 'package:readtrack_domain/readtrack_domain.dart';
import '../../data/models/goal_dto.dart';
import '../../data/models/sync_enums.dart';

/// Mapper extensions for converting between [Goal] domain entities and [GoalDto].
extension GoalMapper on GoalDto {
  Goal toDomain() {
    return Goal(
      id: serverId ?? '',
      type: _mapType(type),
      targetAmount: targetAmount,
      period: _mapPeriod(period),
      currentProgress: currentProgress,
      startDate: startDate,
      endDate: endDate,
      isCompleted: isCompleted,
    );
  }

  GoalType _mapType(int typeIndex) {
    const types = GoalType.values;
    if (typeIndex >= 0 && typeIndex < types.length) {
      return types[typeIndex];
    }
    return GoalType.booksRead;
  }

  GoalPeriod _mapPeriod(int periodIndex) {
    const periods = GoalPeriod.values;
    if (periodIndex >= 0 && periodIndex < periods.length) {
      return periods[periodIndex];
    }
    return GoalPeriod.yearly;
  }
}

extension GoalEntityMapper on Goal {
  GoalDto toDto({
    int? isarId,
    SyncStatus syncStatus = SyncStatus.Synced,
  }) {
    return GoalDto()
      ..id = isarId
      ..serverId = id
      ..type = type.index
      ..targetAmount = targetAmount
      ..period = period.index
      ..currentProgress = currentProgress
      ..startDate = startDate
      ..endDate = endDate
      ..isCompleted = isCompleted
      ..syncStatus = syncStatus;
  }
}