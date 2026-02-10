import 'package:isar/isar.dart';
import 'package:path_provider/path_provider.dart';
import '../data/models/user_dto.dart';
import '../data/models/library_item_dto.dart';
import '../data/models/reading_session_dto.dart';
import '../data/models/goal_dto.dart';

/// Factory responsible for initializing and managing the Isar database instance.
/// This ensures a singleton-like access pattern and proper schema registration.
class IsarFactory {
  static Isar? _instance;

  /// Opens the Isar database if not already open.
  /// 
  /// This method performs the following:
  /// 1. Retrieves the application documents directory for storage.
  /// 2. Checks if an instance is already active.
  /// 3. Opens the database with all required DTO schemas.
  /// 4. Enables the inspector for debug builds.
  Future<Isar> openIsar() async {
    if (_instance != null) {
      return _instance!;
    }

    final dir = await getApplicationDocumentsDirectory();
    
    _instance = await Isar.open(
      [
        UserDtoSchema,
        LibraryItemDtoSchema,
        ReadingSessionDtoSchema,
        GoalDtoSchema,
      ],
      directory: dir.path,
      inspector: true, // Enable Isar Inspector for debugging
    );

    return _instance!;
  }

  /// Closes the Isar instance if it exists.
  Future<void> closeIsar() async {
    await _instance?.close();
    _instance = null;
  }

  /// Clears all data from the database. 
  /// Useful for logout sequences or debugging.
  Future<void> clearDatabase() async {
    final isar = await openIsar();
    await isar.writeTxn(() async {
      await isar.clear();
    });
  }
}