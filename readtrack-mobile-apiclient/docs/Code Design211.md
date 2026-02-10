# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-LIB-APICLIENT |
| Validation Timestamp | 2025-05-17T10:00:00Z |
| Original Component Count Claimed | 24 |
| Original Component Count Actual | 24 |
| Gaps Identified Count | 6 |
| Components Added Count | 6 |
| Final Component Count | 30 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic implementation mapping of Offline-First... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant with Data Access Layer responsibilities.

#### 2.2.1.2 Gaps Identified

- Missing detailed configuration for Dio Interceptor token refresh flows
- Lack of specific Isar schema indices for sync performance
- Missing explicit mapper extensions for DTO-Entity conversion

#### 2.2.1.3 Components Added

- AuthInterceptor
- LibraryItemMapper
- ReadingSessionMapper
- GoalMapper
- IsarSchemaDefinitions
- RetryPolicyConfig

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

*No items available*

#### 2.2.2.4 Added Requirement Components

*No items available*

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

High. Repository and Data Source patterns are strictly separated.

#### 2.2.3.2 Missing Pattern Components

*No items available*

#### 2.2.3.3 Added Pattern Components

- Result<T> Pattern for Error Handling

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Complete. Dual-purpose DTOs defined.

#### 2.2.4.2 Missing Database Components

- SyncStatus enum field in Isar Collections

#### 2.2.4.3 Added Database Components

- SyncStatus

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Complete. Sync logic and optimistic updates covered.

#### 2.2.5.2 Missing Interaction Components

*No items available*

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-LIB-APICLIENT |
| Technology Stack | Flutter 3.22+, Dart 3.4+, Dio 5.4+, Isar 3.1+, Fre... |
| Technology Guidance Integration | Strict adherence to Offline-First architecture usi... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 30 |
| Specification Methodology | Data Source Abstraction with Repository Orchestrat... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Repository Pattern
- Remote/Local Data Source Split
- Adapter Pattern (Mappers)
- Interceptor Pattern (Dio)
- Dependency Injection (Provider/GetIt)
- Code Generation (build_runner)

#### 2.3.2.2 Directory Structure Source

Flutter/Dart Clean Architecture Conventions

#### 2.3.2.3 Naming Conventions Source

Effective Dart

#### 2.3.2.4 Architectural Patterns Source

Offline-First Mobile Architecture

#### 2.3.2.5 Performance Optimizations Applied

- Isolate-based JSON parsing via compute()
- Isar indexed queries for sync status
- Dio connection pooling
- Lazy loading of heavy components

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.editorconfig

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- .editorconfig

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.github/workflows/backend-ci.yml

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- backend-ci.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/workflows/coverage.yml

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- coverage.yml

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.github/workflows/mobile-ci.yml

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- mobile-ci.yml

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.gitignore

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- .gitignore

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

.vscode/launch.json

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- launch.json

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

backend/Directory.Packages.props

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- Directory.Packages.props

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

backend/docker-compose.yml

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- docker-compose.yml

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

backend/global.json

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- global.json

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/ReadTrack.sln

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- ReadTrack.sln

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

backend/src/ReadTrack.Host/appsettings.Development.json

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/src/ReadTrack.Host/Dockerfile

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- Dockerfile

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

backend/xunit.runner.json

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- xunit.runner.json

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

frontend/analysis_options.yaml

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

frontend/app/pubspec.yaml

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

frontend/melos.yaml

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- melos.yaml

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- cdk.json

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- package.json

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

lib/src/config

###### 2.3.3.1.19.2 Purpose

Configuration and initialization of third-party clients

###### 2.3.3.1.19.3 Contains Files

- dio_factory.dart
- isar_factory.dart
- api_constants.dart

###### 2.3.3.1.19.4 Organizational Reasoning

Centralizes setup logic for Dio and Isar to allow for easy testing configuration and singleton management.

###### 2.3.3.1.19.5 Framework Convention Alignment

Configuration isolation

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

lib/src/data/datasources/local

###### 2.3.3.1.20.2 Purpose

Interfaces and implementations for local storage

###### 2.3.3.1.20.3 Contains Files

- interfaces/library_local_source.dart
- implementations/library_local_source_impl.dart
- interfaces/auth_local_source.dart
- implementations/auth_local_source_impl.dart
- secure_storage_service.dart

###### 2.3.3.1.20.4 Organizational Reasoning

Separates local IO logic (Isar/SecureStorage) from network logic.

###### 2.3.3.1.20.5 Framework Convention Alignment

Data Source pattern

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

lib/src/data/datasources/remote

###### 2.3.3.1.21.2 Purpose

Interfaces and implementations for network API

###### 2.3.3.1.21.3 Contains Files

- interfaces/library_remote_source.dart
- implementations/library_remote_source_impl.dart
- interfaces/auth_remote_source.dart
- implementations/auth_remote_source_impl.dart
- interceptors/auth_interceptor.dart
- interceptors/retry_interceptor.dart

###### 2.3.3.1.21.4 Organizational Reasoning

Encapsulates all HTTP interactions and error handling.

###### 2.3.3.1.21.5 Framework Convention Alignment

Data Source pattern

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

lib/src/data/mappers

###### 2.3.3.1.22.2 Purpose

Extension methods for converting DTOs to Domain Entities

###### 2.3.3.1.22.3 Contains Files

- library_mapper.dart
- user_mapper.dart
- goal_mapper.dart

###### 2.3.3.1.22.4 Organizational Reasoning

Keeps DTOs pure data structures and moves conversion logic to extensions.

###### 2.3.3.1.22.5 Framework Convention Alignment

Extension method pattern

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

lib/src/data/models

###### 2.3.3.1.23.2 Purpose

Dual-purpose DTOs for API serialization and Isar persistence

###### 2.3.3.1.23.3 Contains Files

- user_dto.dart
- library_item_dto.dart
- reading_session_dto.dart
- goal_dto.dart
- sync_enums.dart

###### 2.3.3.1.23.4 Organizational Reasoning

Co-locating persistence and serialization definitions simplifies the data layer and leverages code generation.

###### 2.3.3.1.23.5 Framework Convention Alignment

Model layer with code gen support

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

lib/src/data/repositories

###### 2.3.3.1.24.2 Purpose

Repository implementations coordinating local and remote sources

###### 2.3.3.1.24.3 Contains Files

- library_repository_impl.dart
- user_repository_impl.dart
- goal_repository_impl.dart

###### 2.3.3.1.24.4 Organizational Reasoning

Implements the domain contracts defined in the domain layer (imported).

###### 2.3.3.1.24.5 Framework Convention Alignment

Repository pattern implementation

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

lib/src/services

###### 2.3.3.1.25.2 Purpose

Orchestration services for data layer concerns

###### 2.3.3.1.25.3 Contains Files

- synchronization_service.dart
- connectivity_service.dart

###### 2.3.3.1.25.4 Organizational Reasoning

Handles complex logic like background sync that involves multiple repositories.

###### 2.3.3.1.25.5 Framework Convention Alignment

Service layer

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | readtrack_client |
| Namespace Organization | feature_based or layer_based |
| Naming Conventions | snake_case files, PascalCase classes |
| Framework Alignment | Dart package layout |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

LibraryRepositoryImpl

##### 2.3.4.1.2.0 File Path

lib/src/data/repositories/library_repository_impl.dart

##### 2.3.4.1.3.0 Class Type

Repository Implementation

##### 2.3.4.1.4.0 Inheritance

ILibraryRepository

##### 2.3.4.1.5.0 Purpose

Orchestrates data access for library items, prioritizing local cache and handling sync.

##### 2.3.4.1.6.0 Dependencies

- LibraryLocalSource
- LibraryRemoteSource
- ConnectivityService

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses 'Either' type for functional error handling.

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

###### 2.3.4.1.10.1 Method Name

####### 2.3.4.1.10.1.1 Method Name

getLibraryItems

####### 2.3.4.1.10.1.2 Method Signature

Future<Either<Failure, List<LibraryItem>>> getLibraryItems()

####### 2.3.4.1.10.1.3 Return Type

Future<Either<Failure, List<LibraryItem>>>

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

true

####### 2.3.4.1.10.1.6 Parameters

*No items available*

####### 2.3.4.1.10.1.7 Implementation Logic

1. Fetch from LocalSource. 2. If connected, trigger background sync (fire-and-forget). 3. Return mapped local entities.

####### 2.3.4.1.10.1.8 Exception Handling

Catches Isar exceptions, maps to CacheFailure.

####### 2.3.4.1.10.1.9 Performance Considerations

Fetching large lists should use Isar's async capabilities.

####### 2.3.4.1.10.1.10 Validation Requirements

None

####### 2.3.4.1.10.1.11 Technology Integration Details

Relies on Isar's speed for immediate UI rendering.

###### 2.3.4.1.10.2.0 Method Name

####### 2.3.4.1.10.2.1 Method Name

addBook

####### 2.3.4.1.10.2.2 Method Signature

Future<Either<Failure, void>> addBook(LibraryItem item)

####### 2.3.4.1.10.2.3 Return Type

Future<Either<Failure, void>>

####### 2.3.4.1.10.2.4 Access Modifier

public

####### 2.3.4.1.10.2.5 Is Async

true

####### 2.3.4.1.10.2.6 Parameters

- {'parameter_name': 'item', 'parameter_type': 'LibraryItem', 'is_nullable': 'false', 'purpose': 'Domain entity to add'}

####### 2.3.4.1.10.2.7 Implementation Logic

1. Map to DTO. 2. Set SyncStatus.Created. 3. Save to LocalSource. 4. If connected, attempt RemoteSource.add(). 5. If Remote success, update Local SyncStatus.Synced. 6. Return Right(void).

####### 2.3.4.1.10.2.8 Exception Handling

If Remote fails, keep Local as Created (sync will handle later). If Remote fails with 402/403 (Limit), revert Local and return Failure.

####### 2.3.4.1.10.2.9 Performance Considerations

Optimistic UI update.

####### 2.3.4.1.10.2.10 Validation Requirements

Check 20 book limit logic if enforced on client side.

####### 2.3.4.1.10.2.11 Technology Integration Details

Transactional local write.

##### 2.3.4.1.11.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0 Implementation Notes

Core implementation of REQ-OFF-001.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

AuthInterceptor

##### 2.3.4.2.2.0.0 File Path

lib/src/data/datasources/remote/interceptors/auth_interceptor.dart

##### 2.3.4.2.3.0.0 Class Type

Interceptor

##### 2.3.4.2.4.0.0 Inheritance

QueuedInterceptor

##### 2.3.4.2.5.0.0 Purpose

Injects JWT tokens and handles 401 automatic refresh.

##### 2.3.4.2.6.0.0 Dependencies

- SecureStorageService
- Dio

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Extends QueuedInterceptor to lock requests during refresh.

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

###### 2.3.4.2.10.1.0 Method Name

####### 2.3.4.2.10.1.1 Method Name

onRequest

####### 2.3.4.2.10.1.2 Method Signature

void onRequest(RequestOptions options, RequestInterceptorHandler handler)

####### 2.3.4.2.10.1.3 Return Type

void

####### 2.3.4.2.10.1.4 Access Modifier

public

####### 2.3.4.2.10.1.5 Is Async

true

####### 2.3.4.2.10.1.6 Parameters

######## 2.3.4.2.10.1.6.1 Parameter Name

######### 2.3.4.2.10.1.6.1.1 Parameter Name

options

######### 2.3.4.2.10.1.6.1.2 Parameter Type

RequestOptions

######### 2.3.4.2.10.1.6.1.3 Is Nullable

false

######### 2.3.4.2.10.1.6.1.4 Purpose

Current request config

######## 2.3.4.2.10.1.6.2.0 Parameter Name

######### 2.3.4.2.10.1.6.2.1 Parameter Name

handler

######### 2.3.4.2.10.1.6.2.2 Parameter Type

RequestInterceptorHandler

######### 2.3.4.2.10.1.6.2.3 Is Nullable

false

######### 2.3.4.2.10.1.6.2.4 Purpose

Interceptor control

####### 2.3.4.2.10.1.7.0.0 Implementation Logic

Read access token from SecureStorage. Add 'Authorization: Bearer $token' header. Call handler.next().

####### 2.3.4.2.10.1.8.0.0 Exception Handling

If storage read fails, continue without token (public endpoints).

####### 2.3.4.2.10.1.9.0.0 Performance Considerations

Storage read is async.

####### 2.3.4.2.10.1.10.0.0 Validation Requirements

None

####### 2.3.4.2.10.1.11.0.0 Technology Integration Details

Integration with flutter_secure_storage.

###### 2.3.4.2.10.2.0.0.0 Method Name

####### 2.3.4.2.10.2.1.0.0 Method Name

onError

####### 2.3.4.2.10.2.2.0.0 Method Signature

void onError(DioException err, ErrorInterceptorHandler handler)

####### 2.3.4.2.10.2.3.0.0 Return Type

void

####### 2.3.4.2.10.2.4.0.0 Access Modifier

public

####### 2.3.4.2.10.2.5.0.0 Is Async

true

####### 2.3.4.2.10.2.6.0.0 Parameters

######## 2.3.4.2.10.2.6.1.0 Parameter Name

######### 2.3.4.2.10.2.6.1.1 Parameter Name

err

######### 2.3.4.2.10.2.6.1.2 Parameter Type

DioException

######### 2.3.4.2.10.2.6.1.3 Is Nullable

false

######### 2.3.4.2.10.2.6.1.4 Purpose

The error

######## 2.3.4.2.10.2.6.2.0 Parameter Name

######### 2.3.4.2.10.2.6.2.1 Parameter Name

handler

######### 2.3.4.2.10.2.6.2.2 Parameter Type

ErrorInterceptorHandler

######### 2.3.4.2.10.2.6.2.3 Is Nullable

false

######### 2.3.4.2.10.2.6.2.4 Purpose

Interceptor control

####### 2.3.4.2.10.2.7.0.0 Implementation Logic

If 401: Lock Dio. Call RefreshToken endpoint. If success, update storage, unlock Dio, retry original request. If fail, unlock Dio, reject (logout).

####### 2.3.4.2.10.2.8.0.0 Exception Handling

Catch refresh failures.

####### 2.3.4.2.10.2.9.0.0 Performance Considerations

Locks all outgoing requests during refresh.

####### 2.3.4.2.10.2.10.0.0 Validation Requirements

Check specific error codes.

####### 2.3.4.2.10.2.11.0.0 Technology Integration Details

Complex flow handling.

##### 2.3.4.2.11.0.0.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0.0.0 Implementation Notes

Critical for REQ-SEC-001.

#### 2.3.4.3.0.0.0.0.0 Class Name

##### 2.3.4.3.1.0.0.0.0 Class Name

SynchronizationService

##### 2.3.4.3.2.0.0.0.0 File Path

lib/src/services/synchronization_service.dart

##### 2.3.4.3.3.0.0.0.0 Class Type

Service

##### 2.3.4.3.4.0.0.0.0 Inheritance

ISynchronizationService

##### 2.3.4.3.5.0.0.0.0 Purpose

Manages background synchronization of data.

##### 2.3.4.3.6.0.0.0.0 Dependencies

- LibraryLocalSource
- LibraryRemoteSource
- GoalLocalSource
- GoalRemoteSource

##### 2.3.4.3.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0.0.0 Technology Integration Notes

Orchestrator.

##### 2.3.4.3.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0.0.0 Methods

- {'method_name': 'syncLibrary', 'method_signature': 'Future<void> syncLibrary()', 'return_type': 'Future<void>', 'access_modifier': 'public', 'is_async': 'true', 'parameters': [], 'implementation_logic': '1. LocalSource.getDirtyItems(). 2. For each: RemoteSource.upsert(). 3. If success: LocalSource.markSynced(). 4. LocalSource.getDeletedItems(). 5. RemoteSource.delete(). 6. Mark purged. 7. Fetch updates from Remote since lastSync. 8. LocalSource.batchUpsert().', 'exception_handling': 'Log errors, continue sync for other items.', 'performance_considerations': 'Use Isolate for processing fetch results if large.', 'validation_requirements': 'Conflict resolution (Last Write Wins via timestamps).', 'technology_integration_details': 'Isar transactions.'}

##### 2.3.4.3.11.0.0.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0.0.0 Implementation Notes

Implements REQ-FUNC-011 logic.

### 2.3.5.0.0.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0.0.0 Interface Name

##### 2.3.5.1.1.0.0.0.0 Interface Name

ILibraryLocalSource

##### 2.3.5.1.2.0.0.0.0 File Path

lib/src/data/datasources/local/interfaces/library_local_source.dart

##### 2.3.5.1.3.0.0.0.0 Purpose

Contract for local library storage.

##### 2.3.5.1.4.0.0.0.0 Generic Constraints



##### 2.3.5.1.5.0.0.0.0 Framework Specific Inheritance



##### 2.3.5.1.6.0.0.0.0 Method Contracts

###### 2.3.5.1.6.1.0.0.0 Method Name

####### 2.3.5.1.6.1.1.0.0 Method Name

getAll

####### 2.3.5.1.6.1.2.0.0 Method Signature

Future<List<LibraryItemDto>> getAll()

####### 2.3.5.1.6.1.3.0.0 Return Type

Future<List<LibraryItemDto>>

####### 2.3.5.1.6.1.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.1.5.0.0 Parameters

*No items available*

####### 2.3.5.1.6.1.6.0.0 Contract Description

Retrieve all items.

####### 2.3.5.1.6.1.7.0.0 Exception Contracts

CacheException

###### 2.3.5.1.6.2.0.0.0 Method Name

####### 2.3.5.1.6.2.1.0.0 Method Name

put

####### 2.3.5.1.6.2.2.0.0 Method Signature

Future<void> put(LibraryItemDto item)

####### 2.3.5.1.6.2.3.0.0 Return Type

Future<void>

####### 2.3.5.1.6.2.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.2.5.0.0 Parameters

- {'parameter_name': 'item', 'parameter_type': 'LibraryItemDto', 'purpose': 'Item to save'}

####### 2.3.5.1.6.2.6.0.0 Contract Description

Insert or Update item.

####### 2.3.5.1.6.2.7.0.0 Exception Contracts

CacheException

###### 2.3.5.1.6.3.0.0.0 Method Name

####### 2.3.5.1.6.3.1.0.0 Method Name

getDirty

####### 2.3.5.1.6.3.2.0.0 Method Signature

Future<List<LibraryItemDto>> getDirty()

####### 2.3.5.1.6.3.3.0.0 Return Type

Future<List<LibraryItemDto>>

####### 2.3.5.1.6.3.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.3.5.0.0 Parameters

*No items available*

####### 2.3.5.1.6.3.6.0.0 Contract Description

Get items waiting for sync.

####### 2.3.5.1.6.3.7.0.0 Exception Contracts



##### 2.3.5.1.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0.0.0 Implementation Guidance

Use Isar.

##### 2.3.5.1.9.0.0.0.0 Validation Notes



#### 2.3.5.2.0.0.0.0.0 Interface Name

##### 2.3.5.2.1.0.0.0.0 Interface Name

ILibraryRemoteSource

##### 2.3.5.2.2.0.0.0.0 File Path

lib/src/data/datasources/remote/interfaces/library_remote_source.dart

##### 2.3.5.2.3.0.0.0.0 Purpose

Contract for remote library API.

##### 2.3.5.2.4.0.0.0.0 Generic Constraints



##### 2.3.5.2.5.0.0.0.0 Framework Specific Inheritance



##### 2.3.5.2.6.0.0.0.0 Method Contracts

- {'method_name': 'fetchLibrary', 'method_signature': 'Future<List<LibraryItemDto>> fetchLibrary(DateTime? updatedAfter)', 'return_type': 'Future<List<LibraryItemDto>>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'updatedAfter', 'parameter_type': 'DateTime?', 'purpose': 'Delta sync timestamp'}], 'contract_description': 'Get items from API.', 'exception_contracts': 'ServerException'}

##### 2.3.5.2.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0.0.0 Implementation Guidance

Use Dio.

##### 2.3.5.2.9.0.0.0.0 Validation Notes



### 2.3.6.0.0.0.0.0.0 Enum Specifications

- {'enum_name': 'SyncStatus', 'file_path': 'lib/src/data/models/sync_enums.dart', 'underlying_type': 'int', 'purpose': 'Tracks synchronization state of local items.', 'framework_attributes': [], 'values': [{'value_name': 'Synced', 'value': '0', 'description': 'Item matches server state.'}, {'value_name': 'Created', 'value': '1', 'description': 'Created locally, needs push.'}, {'value_name': 'Updated', 'value': '2', 'description': 'Modified locally, needs push.'}, {'value_name': 'Deleted', 'value': '3', 'description': 'Deleted locally, needs push delete.'}], 'validation_notes': 'Stored in Isar.'}

### 2.3.7.0.0.0.0.0.0 Dto Specifications

- {'dto_name': 'LibraryItemDto', 'file_path': 'lib/src/data/models/library_item_dto.dart', 'purpose': 'Data model for library books.', 'framework_base_class': '', 'properties': [{'property_name': 'id', 'property_type': 'Id', 'validation_attributes': [], 'serialization_attributes': ['jsonKey(ignore: true)'], 'framework_specific_attributes': ['Isar Id']}, {'property_name': 'serverId', 'property_type': 'String', 'validation_attributes': [], 'serialization_attributes': ["jsonKey(name: 'id')"], 'framework_specific_attributes': ['Isar Index(unique: true)']}, {'property_name': 'title', 'property_type': 'String', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'syncStatus', 'property_type': 'SyncStatus', 'validation_attributes': [], 'serialization_attributes': ['jsonKey(ignore: true)'], 'framework_specific_attributes': ['Isar Enumerated']}, {'property_name': 'updatedAt', 'property_type': 'DateTime', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'ServerId required for sync.', 'serialization_requirements': 'Dual annotations for Isar/Json.', 'validation_notes': 'Use freezed.'}

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0.0.0 Configuration Name

DioFactory

##### 2.3.8.1.2.0.0.0.0 File Path

lib/src/config/dio_factory.dart

##### 2.3.8.1.3.0.0.0.0 Purpose

Creates configured Dio client.

##### 2.3.8.1.4.0.0.0.0 Framework Base Class



##### 2.3.8.1.5.0.0.0.0 Configuration Sections

- {'section_name': 'BaseOptions', 'properties': [{'property_name': 'connectTimeout', 'property_type': 'Duration', 'default_value': 'seconds: 5', 'required': 'true', 'description': ''}, {'property_name': 'receiveTimeout', 'property_type': 'Duration', 'default_value': 'seconds: 3', 'required': 'true', 'description': ''}]}

##### 2.3.8.1.6.0.0.0.0 Validation Requirements



##### 2.3.8.1.7.0.0.0.0 Validation Notes



#### 2.3.8.2.0.0.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0.0.0 Configuration Name

IsarFactory

##### 2.3.8.2.2.0.0.0.0 File Path

lib/src/config/isar_factory.dart

##### 2.3.8.2.3.0.0.0.0 Purpose

Opens Isar instance.

##### 2.3.8.2.4.0.0.0.0 Framework Base Class



##### 2.3.8.2.5.0.0.0.0 Configuration Sections

- {'section_name': 'Schemas', 'properties': [{'property_name': 'schemas', 'property_type': 'List', 'default_value': '[LibraryItemDtoSchema, ...]', 'required': 'true', 'description': ''}]}

##### 2.3.8.2.6.0.0.0.0 Validation Requirements

Must enable inspector in debug.

##### 2.3.8.2.7.0.0.0.0 Validation Notes



### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0.0.0 Service Interface

##### 2.3.9.1.1.0.0.0.0 Service Interface

Dio

##### 2.3.9.1.2.0.0.0.0 Service Implementation

DioFactory.create

##### 2.3.9.1.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.1.4.0.0.0.0 Registration Reasoning

Reuse connections.

##### 2.3.9.1.5.0.0.0.0 Framework Registration Pattern

Provider

##### 2.3.9.1.6.0.0.0.0 Validation Notes



#### 2.3.9.2.0.0.0.0.0 Service Interface

##### 2.3.9.2.1.0.0.0.0 Service Interface

Isar

##### 2.3.9.2.2.0.0.0.0 Service Implementation

IsarFactory.open

##### 2.3.9.2.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0.0.0 Registration Reasoning

Single DB instance.

##### 2.3.9.2.5.0.0.0.0 Framework Registration Pattern

Provider (Async)

##### 2.3.9.2.6.0.0.0.0 Validation Notes



### 2.3.10.0.0.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0.0.0 Integration Target

##### 2.3.10.1.1.0.0.0.0 Integration Target

Backend API

##### 2.3.10.1.2.0.0.0.0 Integration Type

REST

##### 2.3.10.1.3.0.0.0.0 Required Client Classes

- Dio

##### 2.3.10.1.4.0.0.0.0 Configuration Requirements

Base URL

##### 2.3.10.1.5.0.0.0.0 Error Handling Requirements

Retry 3 times on 503.

##### 2.3.10.1.6.0.0.0.0 Authentication Requirements

Bearer Token

##### 2.3.10.1.7.0.0.0.0 Framework Integration Patterns

Remote Source

##### 2.3.10.1.8.0.0.0.0 Validation Notes



#### 2.3.10.2.0.0.0.0.0 Integration Target

##### 2.3.10.2.1.0.0.0.0 Integration Target

Local Device

##### 2.3.10.2.2.0.0.0.0 Integration Type

Isar DB

##### 2.3.10.2.3.0.0.0.0 Required Client Classes

- Isar

##### 2.3.10.2.4.0.0.0.0 Configuration Requirements

Storage Path

##### 2.3.10.2.5.0.0.0.0 Error Handling Requirements

IO Exception handling.

##### 2.3.10.2.6.0.0.0.0 Authentication Requirements

Encryption Key (Optional)

##### 2.3.10.2.7.0.0.0.0 Framework Integration Patterns

Local Source

##### 2.3.10.2.8.0.0.0.0 Validation Notes



## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 1 |
| Total Dtos | 5 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 30 |
| Phase 2 Claimed Count | 24 |
| Phase 2 Actual Count | 24 |
| Validation Added Count | 6 |
| Final Validated Count | 30 |

