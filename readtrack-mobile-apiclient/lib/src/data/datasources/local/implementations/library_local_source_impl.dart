import 'package:isar/isar.dart';
import '../../../../data/models/library_item_dto.dart';
import '../../../../data/models/sync_enums.dart';
import '../interfaces/library_local_source.dart';
import 'package:readtrack_domain/readtrack_domain.dart'; // For Failure/Exception definitions

/// Concrete implementation of [ILibraryLocalSource] using Isar.
class LibraryLocalSourceImpl implements ILibraryLocalSource {
  final Isar _isar;

  LibraryLocalSourceImpl(this._isar);

  @override
  Future<List<LibraryItemDto>> getAll() async {
    try {
      // Exclude items marked as deleted locally but not yet synced
      return await _isar.libraryItemDtos
          .filter()
          .not()
          .syncStatusEqualTo(SyncStatus.Deleted)
          .findAll();
    } catch (e) {
      throw CacheException(message: 'Failed to fetch local library items: $e');
    }
  }

  @override
  Future<LibraryItemDto?> getById(String serverId) async {
    try {
      return await _isar.libraryItemDtos
          .filter()
          .serverIdEqualTo(serverId)
          .findFirst();
    } catch (e) {
      throw CacheException(message: 'Failed to fetch item by ID: $e');
    }
  }

  @override
  Future<void> put(LibraryItemDto item) async {
    try {
      await _isar.writeTxn(() async {
        await _isar.libraryItemDtos.put(item);
        // Also save sessions if they are embedded or linked
        if (item.sessions.isNotEmpty) {
          await item.sessions.save(); 
        }
      });
    } catch (e) {
      throw CacheException(message: 'Failed to save library item: $e');
    }
  }

  @override
  Future<void> putAll(List<LibraryItemDto> items) async {
    try {
      await _isar.writeTxn(() async {
        await _isar.libraryItemDtos.putAll(items);
        for (var item in items) {
          if (item.sessions.isNotEmpty) {
            await item.sessions.save();
          }
        }
      });
    } catch (e) {
      throw CacheException(message: 'Failed to batch save library items: $e');
    }
  }

  @override
  Future<List<LibraryItemDto>> getDirty() async {
    try {
      return await _isar.libraryItemDtos
          .filter()
          .syncStatusEqualTo(SyncStatus.Created)
          .or()
          .syncStatusEqualTo(SyncStatus.Updated)
          .or()
          .syncStatusEqualTo(SyncStatus.Deleted)
          .findAll();
    } catch (e) {
      throw CacheException(message: 'Failed to fetch dirty items: $e');
    }
  }

  @override
  Future<void> markSynced(List<int> isarIds) async {
    try {
      await _isar.writeTxn(() async {
        final items = await _isar.libraryItemDtos
            .getAll(isarIds);
        
        final validItems = items.whereType<LibraryItemDto>().toList();
        
        for (var item in validItems) {
          // If it was marked deleted and is now synced, actually delete it
          if (item.syncStatus == SyncStatus.Deleted) {
            await _isar.libraryItemDtos.delete(item.id);
          } else {
            item.syncStatus = SyncStatus.Synced;
            await _isar.libraryItemDtos.put(item);
          }
        }
      });
    } catch (e) {
      throw CacheException(message: 'Failed to mark items as synced: $e');
    }
  }

  @override
  Future<void> delete(String serverId) async {
    try {
      final item = await _isar.libraryItemDtos
          .filter()
          .serverIdEqualTo(serverId)
          .findFirst();

      if (item != null) {
        await _isar.writeTxn(() async {
          // Soft delete for sync purposes
          item.syncStatus = SyncStatus.Deleted;
          await _isar.libraryItemDtos.put(item);
        });
      }
    } catch (e) {
      throw CacheException(message: 'Failed to delete local item: $e');
    }
  }
}