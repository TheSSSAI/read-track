# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-LIB-APICLIENT |
| Extraction Timestamp | 2023-10-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-OFF-001

#### 1.2.1.2 Requirement Text

Support core application functions while offline and automatically synchronize data upon reconnection.

#### 1.2.1.3 Validation Criteria

- Local Isar database acts as the single source of truth for the UI
- Write operations are persisted locally immediately and queued for sync
- Synchronization service processes the queue when connectivity is restored using 'last write wins'

#### 1.2.1.4 Implementation Implications

- Implement 'LocalDataSource' with Isar schema definitions
- Implement 'SyncService' observing 'connectivity_plus' streams
- Define 'SyncStatus' enum (Synced, Dirty, Deleted) on all DTOs

#### 1.2.1.5 Extraction Reasoning

This is the primary architectural driver for this repository, mandating the Offline-First implementation strategy.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-SEC-001

#### 1.2.2.2 Requirement Text

Secure authentication and data handling using JWT.

#### 1.2.2.3 Validation Criteria

- JWT Access Tokens must be included in the Authorization header of all API calls
- Refresh tokens must be stored securely using platform-specific secure storage
- Automatic token refresh must occur transparently on 401 Unauthorized responses

#### 1.2.2.4 Implementation Implications

- Implement 'AuthInterceptor' for Dio to inject tokens
- Use 'flutter_secure_storage' to persist credentials
- Implement a locking mechanism in Dio to queue requests while refreshing tokens

#### 1.2.2.5 Extraction Reasoning

Determines the security architecture of the network layer.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-PERF-002

#### 1.2.3.2 Requirement Text

The application's main Dashboard screen must load all necessary data and become fully interactive within 1.5 seconds.

#### 1.2.3.3 Validation Criteria

- Data fetching must not block the UI thread
- Dashboard data must be served from local cache initially (stale-while-revalidate)

#### 1.2.3.4 Implementation Implications

- Use Dart Isolates (via 'compute') for parsing large JSON payloads to avoid frame drops
- Repositories must expose 'Stream<T>' from Isar for reactive UI updates

#### 1.2.3.5 Extraction Reasoning

Constraints the data access pattern to be Cache-First/Reactive.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

DioNetworkClient

#### 1.3.1.2 Component Specification

A configured HTTP client wrapper utilizing Dio.

#### 1.3.1.3 Implementation Requirements

- Base URL configuration from environment variables
- Timeouts configured to 10s connect / 15s receive
- Registration of AuthInterceptor, RetryInterceptor, and LoggerInterceptor

#### 1.3.1.4 Architectural Context

Remote Data Source Infrastructure

#### 1.3.1.5 Extraction Reasoning

Centralizes HTTP configuration and resilience policies.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

IsarLocalDatabase

#### 1.3.2.2 Component Specification

The local NoSQL database instance managing persistence.

#### 1.3.2.3 Implementation Requirements

- Schema definitions for User, LibraryItem, ReadingSession, and Goal
- Indices on 'syncStatus' and 'updatedAt' fields for efficient sync queries
- Migration logic for schema updates

#### 1.3.2.4 Architectural Context

Local Data Source Infrastructure

#### 1.3.2.5 Extraction Reasoning

The persistence engine required for Offline-First functionality.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

RepositoryImpls

#### 1.3.3.2 Component Specification

Implementations of domain repository interfaces that orchestrate data flow between Isar and Dio.

#### 1.3.3.3 Implementation Requirements

- Write to Isar first, then attempt network call (or queue)
- Read from Isar Stream, trigger background fetch
- Map DTOs to Domain Entities

#### 1.3.3.4 Architectural Context

Data Repository Layer

#### 1.3.3.5 Extraction Reasoning

The bridge between the domain logic and the data sources.

### 1.3.4.0 Component Name

#### 1.3.4.1 Component Name

SynchronizationEngine

#### 1.3.4.2 Component Specification

Background service that reconciles local and remote data states.

#### 1.3.4.3 Implementation Requirements

- Push 'Dirty' items to backend
- Pull updated items from backend using 'lastSyncTimestamp'
- Resolve conflicts using server timestamps (Last Write Wins)

#### 1.3.4.4 Architectural Context

Data Service Layer

#### 1.3.4.5 Extraction Reasoning

Critical component for REQ-OFF-001.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Client.Data', 'layer_responsibilities': 'Data retrieval, persistence, synchronization, and transformation.', 'layer_constraints': ['Must depend only on Client.Domain', 'Must NOT depend on Flutter UI widgets', 'Must handle all I/O exceptions and map them to Domain Failures'], 'implementation_patterns': ['Repository Pattern', 'Offline-First / Cache-Aside', 'Adapter Pattern (DTO Mappers)', 'Interceptor Pattern'], 'extraction_reasoning': 'Standard Clean Architecture Data Layer definition.'}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

ReadTrack Backend API

#### 1.5.1.2 Source Repository

REPO-BE-HOST

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

POST /api/v1/auth/refresh

###### 1.5.1.3.1.2 Method Signature

AuthResponseDto refresh(RefreshTokenRequest request)

###### 1.5.1.3.1.3 Method Purpose

Obtain new access token using refresh token.

###### 1.5.1.3.1.4 Integration Context

Called by AuthInterceptor on 401 error.

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

GET /api/v1/library/sync

###### 1.5.1.3.2.2 Method Signature

LibrarySyncResponseDto syncLibrary(DateTime? since)

###### 1.5.1.3.2.3 Method Purpose

Fetch library changes occurred after the given timestamp.

###### 1.5.1.3.2.4 Integration Context

Called by SynchronizationEngine on network reconnect.

##### 1.5.1.3.3.0 Method Name

###### 1.5.1.3.3.1 Method Name

POST /api/v1/sessions/batch

###### 1.5.1.3.3.2 Method Signature

void batchLogSessions(List<ReadingSessionDto> sessions)

###### 1.5.1.3.3.3 Method Purpose

Upload multiple offline reading sessions in a single request.

###### 1.5.1.3.3.4 Integration Context

Called by SynchronizationEngine to flush the queue.

#### 1.5.1.4.0.0 Integration Pattern

RESTful API

#### 1.5.1.5.0.0 Communication Protocol

HTTPS / JSON

#### 1.5.1.6.0.0 Extraction Reasoning

The backend host exposes the API that this client must consume.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

Secure Storage

#### 1.5.2.2.0.0 Source Repository

flutter_secure_storage

#### 1.5.2.3.0.0 Method Contracts

- {'method_name': 'read', 'method_signature': 'Future<String?> read({required String key})', 'method_purpose': 'Retrieve sensitive tokens.', 'integration_context': 'Called by AuthInterceptor for every request.'}

#### 1.5.2.4.0.0 Integration Pattern

Platform Channel / Keychain

#### 1.5.2.5.0.0 Communication Protocol

Async Method Channel

#### 1.5.2.6.0.0 Extraction Reasoning

Platform dependency for security.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

ILibraryRepository

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-FE-APP

#### 1.6.1.3.0.0 Method Contracts

##### 1.6.1.3.1.0 Method Name

###### 1.6.1.3.1.1 Method Name

watchLibrary

###### 1.6.1.3.1.2 Method Signature

Stream<List<Book>> watchLibrary()

###### 1.6.1.3.1.3 Method Purpose

Provides a reactive stream of the user's library from the local database.

###### 1.6.1.3.1.4 Implementation Requirements

Must emit new values whenever the local DB changes (sync or user action).

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

addBook

###### 1.6.1.3.2.2 Method Signature

Future<Either<Failure, void>> addBook(String googleBookId)

###### 1.6.1.3.2.3 Method Purpose

Adds a book to the library.

###### 1.6.1.3.2.4 Implementation Requirements

Handles offline queuing implicitly.

#### 1.6.1.4.0.0 Service Level Requirements

- Stream must emit initial value within 50ms
- Methods must never throw exceptions; return Either<Failure, T>

#### 1.6.1.5.0.0 Implementation Constraints

- Must return Domain Entities, not DTOs

#### 1.6.1.6.0.0 Extraction Reasoning

Primary interface used by the App Shell to interact with library data.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

ISyncService

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-FE-APP

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'initialize', 'method_signature': 'void initialize()', 'method_purpose': 'Starts the connectivity listener and background sync worker.', 'implementation_requirements': 'Called at app startup.'}

#### 1.6.2.4.0.0 Service Level Requirements

- Must not block main thread

#### 1.6.2.5.0.0 Implementation Constraints

*No items available*

#### 1.6.2.6.0.0 Extraction Reasoning

Allows the app shell to bootstrap the synchronization logic.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

Flutter 3.22+, Dart 3.4+

### 1.7.2.0.0.0 Integration Technologies

- Dio (Network)
- Isar (Persistence)
- Freezed (Immutability)
- JsonSerializable (Serialization)

### 1.7.3.0.0.0 Performance Constraints

JSON parsing > 10KB must run in Isolate. P95 Database Read < 10ms.

### 1.7.4.0.0.0 Security Requirements

Tokens stored in Keychain/Keystore. TLS 1.2+ enforced. No PII in logs.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped all data access requirements including Offl... |
| Cross Reference Validation | Verified endpoints match REPO-BE-HOST capabilities... |
| Implementation Readiness Assessment | High. Detailed patterns for Dio/Isar integration a... |
| Quality Assurance Confirmation | Integration patterns ensure resilience and decoupl... |

