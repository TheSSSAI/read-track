# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-READING |
| Extraction Timestamp | 2025-10-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-TRK-001

#### 1.2.1.2 Requirement Text

The system shall allow users to search for books via Google Books API, add them to a personal library with specific shelf statuses, and log reading sessions.

#### 1.2.1.3 Validation Criteria

- User can search and add books
- User can log pages/time
- Data is persisted correctly

#### 1.2.1.4 Implementation Implications

- Implement Google Books API client
- Create LibraryItem and ReadingSession aggregates
- Expose REST endpoints for mobile client sync

#### 1.2.1.5 Extraction Reasoning

Core domain responsibility of this module.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-REL-002

#### 1.2.2.2 Requirement Text

The system must implement resilience patterns to handle failures gracefully when communicating with critical external APIs.

#### 1.2.2.3 Validation Criteria

- Circuit breaker for Google Books API
- Retry policies for transient failures

#### 1.2.2.4 Implementation Implications

- Use Polly via Microsoft.Extensions.Http.Resilience
- Configure retry and circuit breaker policies in Dependency Injection

#### 1.2.2.5 Extraction Reasoning

Direct impact on the GoogleBooksApiClient implementation.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

US-013

#### 1.2.3.2 Requirement Text

Free User is prevented from adding more than 20 library items.

#### 1.2.3.3 Validation Criteria

- Check user subscription status before addition
- Block write operation if limit exceeded

#### 1.2.3.4 Implementation Implications

- Consume ISubscriptionService to check user tier
- Implement limit logic in AddBookToLibraryCommandHandler

#### 1.2.3.5 Extraction Reasoning

Business rule requiring cross-module integration with Monetization.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

GoogleBooksApiClient

#### 1.3.1.2 Component Specification

Typed HTTP Client wrapper for Google Books API interactions.

#### 1.3.1.3 Implementation Requirements

- Implement IGoogleBooksClient
- Use Polly for resilience
- Map JSON to BookMetadata Value Object

#### 1.3.1.4 Architectural Context

Infrastructure Layer - External Adapter

#### 1.3.1.5 Extraction Reasoning

Encapsulates external dependency logic.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

LogReadingSessionHandler

#### 1.3.2.2 Component Specification

CQRS Command Handler for recording reading activity.

#### 1.3.2.3 Implementation Requirements

- Validate session consistency
- Update aggregate state
- Publish ReadingSessionLogged integration event

#### 1.3.2.4 Architectural Context

Application Layer - Use Case

#### 1.3.2.5 Extraction Reasoning

Core business transaction handler.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

LibraryController

#### 1.3.3.2 Component Specification

ASP.NET Core Controller exposing library management endpoints.

#### 1.3.3.3 Implementation Requirements

- Define Routes: POST /books, GET /library
- Authenticate User
- Delegate to MediatR

#### 1.3.3.4 Architectural Context

Presentation Layer - API Endpoint

#### 1.3.3.5 Extraction Reasoning

Primary entry point for the mobile client.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Domain

#### 1.4.1.2 Layer Responsibilities

Entities, Value Objects, Domain Events, Aggregate Roots

#### 1.4.1.3 Layer Constraints

- No dependencies on Application/Infrastructure
- Pure C#

#### 1.4.1.4 Implementation Patterns

- Domain-Driven Design
- Rich Domain Model

#### 1.4.1.5 Extraction Reasoning

Standard Clean Architecture layer.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Application

#### 1.4.2.2 Layer Responsibilities

CQRS Handlers, Interfaces, DTOs, Use Case Orchestration

#### 1.4.2.3 Layer Constraints

- Dependent on Domain
- Independent of Persistence details

#### 1.4.2.4 Implementation Patterns

- CQRS
- Mediator

#### 1.4.2.5 Extraction Reasoning

Standard Clean Architecture layer.

### 1.4.3.0 Layer Name

#### 1.4.3.1 Layer Name

Infrastructure

#### 1.4.3.2 Layer Responsibilities

EF Core Repositories, External API Clients, Configurations

#### 1.4.3.3 Layer Constraints

- Implement Application Interfaces

#### 1.4.3.4 Implementation Patterns

- Repository
- Adapter

#### 1.4.3.5 Extraction Reasoning

Standard Clean Architecture layer.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

ISubscriptionService

#### 1.5.1.2 Source Repository

REPO-BE-MOD-MONETIZATION

#### 1.5.1.3 Method Contracts

- {'method_name': 'GetUserSubscriptionStatusAsync', 'method_signature': 'Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId, CancellationToken ct)', 'method_purpose': 'Retrieves user tier to enforce library limits.', 'integration_context': 'Invoked in AddBookToLibraryCommandHandler.'}

#### 1.5.1.4 Integration Pattern

In-Process Service Call (Module-to-Module)

#### 1.5.1.5 Communication Protocol

C# Interface

#### 1.5.1.6 Extraction Reasoning

Required for US-013 enforcement.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IUnitOfWork

#### 1.5.2.2 Source Repository

REPO-BE-LIB-INFRA

#### 1.5.2.3 Method Contracts

- {'method_name': 'SaveChangesAsync', 'method_signature': 'Task<int> SaveChangesAsync(CancellationToken ct)', 'method_purpose': 'Commits transaction and dispatches domain events.', 'integration_context': 'Invoked at the end of Command Handlers.'}

#### 1.5.2.4 Integration Pattern

Shared Kernel Interface

#### 1.5.2.5 Communication Protocol

C# Interface

#### 1.5.2.6 Extraction Reasoning

Standard persistence pattern.

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

Reading REST API

#### 1.6.1.2 Consumer Repositories

- readtrack-mobile-apiclient

#### 1.6.1.3 Method Contracts

##### 1.6.1.3.1 Method Name

###### 1.6.1.3.1.1 Method Name

POST /api/v1/library/books

###### 1.6.1.3.1.2 Method Signature

Task<IActionResult> AddBook([FromBody] AddBookRequest request)

###### 1.6.1.3.1.3 Method Purpose

Adds a book to the user's library.

###### 1.6.1.3.1.4 Implementation Requirements

Validate request, dispatch command.

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

POST /api/v1/sessions/sync

###### 1.6.1.3.2.2 Method Signature

Task<IActionResult> SyncSessions([FromBody] SyncSessionsRequest request)

###### 1.6.1.3.2.3 Method Purpose

Synchronizes offline reading sessions.

###### 1.6.1.3.2.4 Implementation Requirements

Handle batch updates.

#### 1.6.1.4.0.0 Service Level Requirements

- P95 Latency < 200ms

#### 1.6.1.5.0.0 Implementation Constraints

- Must use JWT Authentication

#### 1.6.1.6.0.0 Extraction Reasoning

Primary interface for the mobile application.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

IReadingHistoryProvider

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-BE-MOD-RECOMMENDATIONS

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'GetUserReadingHistoryForEmbeddingsAsync', 'method_signature': 'Task<List<BookData>> GetUserReadingHistoryForEmbeddingsAsync(Guid userId)', 'method_purpose': 'Provides reading history to generate AI embeddings.', 'implementation_requirements': 'Optimized read-only query.'}

#### 1.6.2.4.0.0 Service Level Requirements

- Internal calls must be efficient

#### 1.6.2.5.0.0 Implementation Constraints

- Must return Domain Objects or DTOs from Shared Contracts

#### 1.6.2.6.0.0 Extraction Reasoning

Required by the Recommendations module for RAG pipeline.

### 1.6.3.0.0.0 Interface Name

#### 1.6.3.1.0.0 Interface Name

ReadingSessionLogged

#### 1.6.3.2.0.0 Consumer Repositories

- REPO-BE-MOD-ENGAGEMENT

#### 1.6.3.3.0.0 Method Contracts

- {'method_name': 'Publish', 'method_signature': 'Task Publish(INotification notification)', 'method_purpose': 'Notifies subscribers that a session was logged.', 'implementation_requirements': 'Use MediatR'}

#### 1.6.3.4.0.0 Service Level Requirements

- Reliable publishing

#### 1.6.3.5.0.0 Implementation Constraints

- Asynchronous event

#### 1.6.3.6.0.0 Extraction Reasoning

Triggers goal progress updates in the Engagement module.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8, ASP.NET Core 8

### 1.7.2.0.0.0 Integration Technologies

- MediatR
- Polly
- Google.Apis.Books.v1

### 1.7.3.0.0.0 Performance Constraints

Library fetch queries must use pagination and AsNoTracking.

### 1.7.4.0.0.0 Security Requirements

API Keys for Google Books must be loaded via Options pattern/Secrets Manager.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All internal (Monetization, Engagement, Recommenda... |
| Cross Reference Validation | Validated against Sequence Diagram 451 and 460. |
| Implementation Readiness Assessment | High. Interfaces and contracts are clearly defined... |
| Quality Assurance Confirmation | Integration patterns adhere to Modular Monolith co... |

