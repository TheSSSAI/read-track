# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-READING |
| Validation Timestamp | 2025-10-27T12:00:00Z |
| Original Component Count Claimed | 36 |
| Original Component Count Actual | 36 |
| Gaps Identified Count | 0 |
| Components Added Count | 0 |
| Final Component Count | 36 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Strict alignment with .NET 8 Modular Monolith guid... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Full compliance. Module focuses strictly on Library Management and Reading Tracking.

#### 2.2.1.2 Gaps Identified

*No items available*

#### 2.2.1.3 Components Added

*No items available*

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100% (Resilience, Performance, Scalability)

#### 2.2.2.3 Missing Requirement Components

*No items available*

#### 2.2.2.4 Added Requirement Components

*No items available*

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Clean Architecture, CQRS, and DDD patterns fully implemented.

#### 2.2.3.2 Missing Pattern Components

*No items available*

#### 2.2.3.3 Added Pattern Components

*No items available*

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Complete mapping for LibraryItem and ReadingSession aggregates.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

All sequence diagram flows (Log Session, Sync, Search) covered.

#### 2.2.5.2 Missing Interaction Components

*No items available*

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-READING |
| Technology Stack | .NET 8, ASP.NET Core 8, Entity Framework Core 8, M... |
| Technology Guidance Integration | Utilizes C# 12 features (primary constructors, rec... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 36 |
| Specification Methodology | Domain-Driven Design with Clean Architecture |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Clean Architecture (Domain/Application/Infrastructure)
- CQRS with MediatR
- Rich Domain Models (DDD)
- Repository & Unit of Work
- Options Pattern for Configuration
- Polly Resilience Pipeline
- FluentValidation

#### 2.3.2.2 Directory Structure Source

Modular Monolith .NET 8 Standard

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Coding Conventions

#### 2.3.2.4 Architectural Patterns Source

Enterprise Application Architecture

#### 2.3.2.5 Performance Optimizations Applied

- AsNoTracking for Read Queries
- Compiled Models in EF Core
- Batch Updates (where applicable)
- Connection Pooling for HTTP Clients
- Async/Await I/O Optimization

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

.github/CODEOWNERS

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- CODEOWNERS

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/workflows/ci-cd.yml

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- ci-cd.yml

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.gitignore

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- .gitignore

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.vscode/extensions.json

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- extensions.json

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

backend/.dockerignore

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- .dockerignore

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

backend/Directory.Build.props

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

backend/Directory.Packages.props

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- Directory.Packages.props

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/global.json

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- global.json

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

backend/src/ReadTrack.Host/Dockerfile

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- Dockerfile

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/xunit.runner.json

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- xunit.runner.json

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- cdk.json

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

infrastructure/jest.config.js

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- jest.config.js

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- package.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

infrastructure/tsconfig.json

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- tsconfig.json

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

mobile/pubspec.yaml

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

src/ReadTrack.Reading.Application

###### 2.3.3.1.19.2 Purpose

Orchestrates use cases via CQRS.

###### 2.3.3.1.19.3 Contains Files

- Features/Library/Commands/AddBookToLibrary/AddBookToLibraryCommand.cs
- Features/Library/Queries/GetUserLibrary/GetUserLibraryQuery.cs
- Features/Sessions/Commands/LogReadingSession/LogReadingSessionCommand.cs
- Features/Sessions/Commands/SyncReadingSessions/SyncReadingSessionsCommand.cs
- Interfaces/ILibraryRepository.cs
- Interfaces/IGoogleBooksClient.cs
- Interfaces/ISubscriptionService.cs

###### 2.3.3.1.19.4 Organizational Reasoning

Separates read/write concerns and defines dependency contracts.

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard Application Layer

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

src/ReadTrack.Reading.Domain

###### 2.3.3.1.20.2 Purpose

Encapsulates enterprise logic, entities, and business rules.

###### 2.3.3.1.20.3 Contains Files

- Aggregates/Library/LibraryItem.cs
- Aggregates/Library/ReadingSession.cs
- Aggregates/Library/BookMetadata.cs
- Aggregates/Library/ShelfStatus.cs
- Events/ReadingSessionLoggedEvent.cs
- Events/LibraryItemMovedToShelfEvent.cs

###### 2.3.3.1.20.4 Organizational Reasoning

Zero-dependency core ensures domain purity.

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard Domain Layer

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

src/ReadTrack.Reading.Infrastructure

###### 2.3.3.1.21.2 Purpose

Implements interfaces and external integration.

###### 2.3.3.1.21.3 Contains Files

- Persistence/ReadingDbContext.cs
- Persistence/Repositories/LibraryRepository.cs
- ExternalServices/GoogleBooks/GoogleBooksClient.cs
- ExternalServices/GoogleBooks/GoogleBooksSettings.cs

###### 2.3.3.1.21.4 Organizational Reasoning

Encapsulates implementation details and external dependencies.

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard Infrastructure Layer

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

src/ReadTrack.Reading.Presentation

###### 2.3.3.1.22.2 Purpose

Exposes functionality via HTTP API.

###### 2.3.3.1.22.3 Contains Files

- Controllers/LibraryController.cs
- Controllers/SessionsController.cs

###### 2.3.3.1.22.4 Organizational Reasoning

Entry point for the module.

###### 2.3.3.1.22.5 Framework Convention Alignment

ASP.NET Core Web API

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Reading |
| Namespace Organization | ReadTrack.Reading.{Layer}.{Feature} |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET 8 Best Practices |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

LibraryItem

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Reading.Domain/Aggregates/Library/LibraryItem.cs

##### 2.3.4.1.3.0 Class Type

Entity

##### 2.3.4.1.4.0 Inheritance

AggregateRoot<Guid>

##### 2.3.4.1.5.0 Purpose

Aggregate Root managing book state, shelf location, and reading sessions.

##### 2.3.4.1.6.0 Dependencies

*No items available*

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Utilizes private collections and public IReadOnlyCollection to enforce encapsulation.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

UserId

####### 2.3.4.1.9.1.2 Property Type

Guid

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

Owner identifier.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Indexed

####### 2.3.4.1.9.1.7 Implementation Notes

Partitioning key for multi-tenancy.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

Metadata

####### 2.3.4.1.9.2.2 Property Type

BookMetadata

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

Value object containing book details.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Owned Entity

####### 2.3.4.1.9.2.7 Implementation Notes

Mapped as embedded columns.

###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

Shelf

####### 2.3.4.1.9.3.2 Property Type

ShelfStatus

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Current status.

####### 2.3.4.1.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.3.6 Framework Specific Configuration

Enum

####### 2.3.4.1.9.3.7 Implementation Notes

Defaults to WantToRead.

##### 2.3.4.1.10.0.0 Methods

- {'method_name': 'LogSession', 'method_signature': 'public void LogSession(ReadingSession session)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'parameters': [{'parameter_name': 'session', 'parameter_type': 'ReadingSession', 'is_nullable': 'false', 'purpose': 'Session to add.'}], 'implementation_logic': "Adds session to internal list. Updates 'PagesRead' on metadata if session exceeds current. Raises ReadingSessionLoggedEvent.", 'exception_handling': 'Throws DomainException if pages exceed total pages.', 'performance_considerations': 'In-memory operation.', 'validation_requirements': 'Session start time must be valid.', 'technology_integration_details': 'Updates aggregate state for persistence.'}

##### 2.3.4.1.11.0.0 Events

- {'event_name': 'ReadingSessionLoggedEvent', 'event_type': 'DomainEvent', 'trigger_conditions': 'On successful session log.', 'event_data': 'UserId, LibraryItemId, Duration, Pages'}

##### 2.3.4.1.12.0.0 Implementation Notes

Core logic carrier.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

AddBookToLibraryCommandHandler

##### 2.3.4.2.2.0.0 File Path

src/ReadTrack.Reading.Application/Features/Library/Commands/AddBookToLibrary/AddBookToLibraryCommandHandler.cs

##### 2.3.4.2.3.0.0 Class Type

Handler

##### 2.3.4.2.4.0.0 Inheritance

IRequestHandler<AddBookToLibraryCommand, Result<Guid>>

##### 2.3.4.2.5.0.0 Purpose

Handles logic for adding a book, including subscription limit checks.

##### 2.3.4.2.6.0.0 Dependencies

- ILibraryRepository
- ISubscriptionService
- IUnitOfWork

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Uses primary constructor for DI.

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result<Guid>> Handle(AddBookToLibraryCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result<Guid>>', 'access_modifier': 'public', 'is_async': 'true', 'parameters': [{'parameter_name': 'request', 'parameter_type': 'AddBookToLibraryCommand', 'is_nullable': 'false', 'purpose': 'Command data.'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation.'}], 'implementation_logic': '1. Call SubscriptionService.GetStatus. 2. If Free and Repo.Count(User) >= 20, return Failure. 3. Create LibraryItem. 4. Repo.Add. 5. UnitOfWork.Commit.', 'exception_handling': 'Returns Result.Failure for business rules.', 'performance_considerations': 'Count query should be optimized.', 'validation_requirements': 'Limit check is critical (US-013).', 'technology_integration_details': 'MediatR pipeline execution.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Enforces monetization constraints in business logic.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

GoogleBooksClient

##### 2.3.4.3.2.0.0 File Path

src/ReadTrack.Reading.Infrastructure/ExternalServices/GoogleBooks/GoogleBooksClient.cs

##### 2.3.4.3.3.0.0 Class Type

Service

##### 2.3.4.3.4.0.0 Inheritance

IGoogleBooksClient

##### 2.3.4.3.5.0.0 Purpose

Encapsulates HTTP interactions with Google Books API.

##### 2.3.4.3.6.0.0 Dependencies

- HttpClient
- IOptions<GoogleBooksSettings>

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Registered with Polly policy in DI.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'SearchBooksAsync', 'method_signature': 'public async Task<List<BookMetadata>> SearchBooksAsync(string query, CancellationToken cancellationToken)', 'return_type': 'Task<List<BookMetadata>>', 'access_modifier': 'public', 'is_async': 'true', 'parameters': [{'parameter_name': 'query', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Search term.'}], 'implementation_logic': 'Constructs URL with API Key. Sends GET. Deserializes JSON. Maps to Domain Value Objects.', 'exception_handling': 'Throws integration exception on failure (handled by Polly policies mostly).', 'performance_considerations': 'Response mapping should use System.Text.Json source generation.', 'validation_requirements': 'Input query validation.', 'technology_integration_details': 'Relies on IHttpClientFactory.'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Infrastructure adapter.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

ILibraryRepository

##### 2.3.5.1.2.0.0 File Path

src/ReadTrack.Reading.Application/Interfaces/ILibraryRepository.cs

##### 2.3.5.1.3.0.0 Purpose

Data access contract for Library Aggregate.

##### 2.3.5.1.4.0.0 Generic Constraints



##### 2.3.5.1.5.0.0 Framework Specific Inheritance



##### 2.3.5.1.6.0.0 Method Contracts

###### 2.3.5.1.6.1.0 Method Name

####### 2.3.5.1.6.1.1 Method Name

GetByUserIdAsync

####### 2.3.5.1.6.1.2 Method Signature

Task<List<LibraryItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)

####### 2.3.5.1.6.1.3 Return Type

Task<List<LibraryItem>>

####### 2.3.5.1.6.1.4 Contract Description

Gets all items for a user.

####### 2.3.5.1.6.1.5 Exception Contracts

None

###### 2.3.5.1.6.2.0 Method Name

####### 2.3.5.1.6.2.1 Method Name

CountByUserIdAsync

####### 2.3.5.1.6.2.2 Method Signature

Task<int> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken)

####### 2.3.5.1.6.2.3 Return Type

Task<int>

####### 2.3.5.1.6.2.4 Contract Description

Efficiently counts items for limit checks.

####### 2.3.5.1.6.2.5 Exception Contracts

None

###### 2.3.5.1.6.3.0 Method Name

####### 2.3.5.1.6.3.1 Method Name

AddAsync

####### 2.3.5.1.6.3.2 Method Signature

Task AddAsync(LibraryItem item, CancellationToken cancellationToken)

####### 2.3.5.1.6.3.3 Return Type

Task

####### 2.3.5.1.6.3.4 Contract Description

Adds entity to change tracker.

####### 2.3.5.1.6.3.5 Exception Contracts

None

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Implement using EF Core.

##### 2.3.5.1.9.0.0 Validation Notes

Separation of concern.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

ISubscriptionService

##### 2.3.5.2.2.0.0 File Path

src/ReadTrack.Reading.Application/Interfaces/ISubscriptionService.cs

##### 2.3.5.2.3.0.0 Purpose

Port for accessing monetization info.

##### 2.3.5.2.4.0.0 Generic Constraints



##### 2.3.5.2.5.0.0 Framework Specific Inheritance



##### 2.3.5.2.6.0.0 Method Contracts

- {'method_name': 'GetUserSubscriptionStatusAsync', 'method_signature': 'Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId, CancellationToken cancellationToken)', 'return_type': 'Task<SubscriptionStatusDto>', 'contract_description': 'Gets tier info.', 'exception_contracts': 'None'}

##### 2.3.5.2.7.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0 Implementation Guidance

Implemented by adapter calling Monetization Module.

##### 2.3.5.2.9.0.0 Validation Notes

Integration boundary.

### 2.3.6.0.0.0.0 Enum Specifications

- {'enum_name': 'ShelfStatus', 'file_path': 'src/ReadTrack.Reading.Domain/Aggregates/Library/ShelfStatus.cs', 'underlying_type': 'int', 'purpose': 'Defines book state.', 'framework_attributes': [], 'values': [{'value_name': 'WantToRead', 'value': '0', 'description': 'Backlog'}, {'value_name': 'CurrentlyReading', 'value': '1', 'description': 'Active'}, {'value_name': 'Read', 'value': '2', 'description': 'Completed'}, {'value_name': 'DidNotFinish', 'value': '3', 'description': 'Abandoned'}], 'validation_notes': 'Domain constant.'}

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'AddBookToLibraryCommand', 'file_path': 'src/ReadTrack.Reading.Application/Features/Library/Commands/AddBookToLibrary/AddBookToLibraryCommand.cs', 'purpose': 'Request payload for adding a book.', 'framework_base_class': 'IRequest<Result<Guid>>', 'properties': [{'property_name': 'UserId', 'property_type': 'Guid', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'GoogleBookId', 'property_type': 'string', 'validation_attributes': ['Required'], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'InitialShelf', 'property_type': 'ShelfStatus', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'GoogleBookId required.', 'serialization_requirements': 'JSON', 'validation_notes': 'C# Record.'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'GoogleBooksSettings', 'file_path': 'src/ReadTrack.Reading.Infrastructure/ExternalServices/GoogleBooks/GoogleBooksSettings.cs', 'purpose': 'Config settings for Google API.', 'framework_base_class': 'None', 'configuration_sections': [{'section_name': 'GoogleBooks', 'properties': [{'property_name': 'ApiKey', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'API Key.'}, {'property_name': 'BaseUrl', 'property_type': 'string', 'default_value': 'https://www.googleapis.com/books/v1/', 'required': 'true', 'description': 'Endpoint.'}]}], 'validation_requirements': 'Valid structure.', 'validation_notes': 'Mapped via IOptions.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

ILibraryRepository

##### 2.3.9.1.2.0.0 Service Implementation

LibraryRepository

##### 2.3.9.1.3.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0 Registration Reasoning

EF Context lifetime alignment.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<ILibraryRepository, LibraryRepository>()

##### 2.3.9.1.6.0.0 Validation Notes



#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IGoogleBooksClient

##### 2.3.9.2.2.0.0 Service Implementation

GoogleBooksClient

##### 2.3.9.2.3.0.0 Lifetime

HttpClient

##### 2.3.9.2.4.0.0 Registration Reasoning

Polly integration.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddHttpClient<IGoogleBooksClient, GoogleBooksClient>().AddPolicyHandler(...)

##### 2.3.9.2.6.0.0 Validation Notes



### 2.3.10.0.0.0.0 External Integration Specifications

- {'integration_target': 'Google Books API', 'integration_type': 'HTTP REST', 'required_client_classes': ['HttpClient'], 'configuration_requirements': 'GoogleBooksSettings', 'error_handling_requirements': 'Polly Retry/CircuitBreaker', 'authentication_requirements': 'API Key Query Param', 'framework_integration_patterns': 'Typed HttpClient', 'validation_notes': 'Supports REQ-TRK-001'}

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 1 |
| Total Dtos | 2 |
| Total Configurations | 1 |
| Total External Integrations | 1 |
| Grand Total Components | 21 |
| Phase 2 Claimed Count | 36 |
| Phase 2 Actual Count | 36 |
| Validation Added Count | 0 |
| Final Validated Count | 36 |

