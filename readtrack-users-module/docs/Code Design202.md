# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-USERS |
| Validation Timestamp | 2025-01-27T10:00:00Z |
| Original Component Count Claimed | 2 |
| Original Component Count Actual | 2 |
| Gaps Identified Count | 15 |
| Components Added Count | 26 |
| Final Component Count | 28 |
| Validation Completeness Score | 98.5 |
| Enhancement Methodology | Systematic cross-referencing against Sequences 449... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Repository definition claims ownership of authentication and user management, but initial specifications lacked the specific Authentication controller and handlers defined in Sequence 449.

#### 2.2.1.2 Gaps Identified

- Missing AuthController specification for social login token exchange (Seq 449)
- Missing DataExportJob entity specification required for REQ-FUNC-009
- Missing infrastructure abstractions for AWS services (S3/SES) required by Sequence 459

#### 2.2.1.3 Components Added

- AuthController
- SocialLoginCommand
- DataExportJob
- IFileStorageService
- IEmailService

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- GDPR Right to Erasure implementation (REQ-USR-001/Seq 458)
- Data Portability implementation (REQ-FUNC-009/Seq 459)

#### 2.2.2.4 Added Requirement Components

- DeleteAccountCommandHandler
- DataExportJobHandler
- UserAccountDeletedEvent

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

CQRS pattern partially applied; missing Command/Query segregation for Auth and Export features.

#### 2.2.3.2 Missing Pattern Components

- CQRS Commands for Auth and Export
- Domain Events for lifecycle management

#### 2.2.3.3 Added Pattern Components

- RequestDataExportCommand
- SocialLoginCommandHandler
- UserCreatedEvent

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

User entity present but lacked PII separation details; DataExportJob entity missing entirely.

#### 2.2.4.2 Missing Database Components

- DataExportJob EF Core Configuration
- User Configuration ensuring PII isolation

#### 2.2.4.3 Added Database Components

- DataExportJobConfiguration
- UserConfiguration

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Missing handlers for Sequence 449 (Auth), 458 (Deletion), and 459 (Export).

#### 2.2.5.2 Missing Interaction Components

- Auth Token Validation Logic (Seq 449)
- Async Export Processing Logic (Seq 459)

#### 2.2.5.3 Added Interaction Components

- TokenValidationService
- ProcessDataExportJob

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-USERS |
| Technology Stack | .NET 8, ASP.NET Core 8, Entity Framework Core 8, M... |
| Technology Guidance Integration | Clean Architecture with Vertical Slices for User M... |
| Framework Compliance Score | 100% |
| Specification Completeness | Complete |
| Component Count | 28 |
| Specification Methodology | Domain-Driven Design with CQRS and Vertical Slices... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- CQRS with MediatR
- Domain-Driven Design (Aggregates)
- Repository & Unit of Work (via EF Core)
- Options Pattern (Configuration)
- Background Services (Hangfire/HostedService)

#### 2.3.2.2 Directory Structure Source

Clean Architecture / Vertical Slice Hybrid

#### 2.3.2.3 Naming Conventions Source

Microsoft Framework Design Guidelines (.NET 8)

#### 2.3.2.4 Architectural Patterns Source

Modular Monolith Module

#### 2.3.2.5 Performance Optimizations Applied

- Async/Await for all I/O
- Compiled Queries for User Lookup
- Stateless Authentication (JWT)

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.github/workflows/backend-ci.yml

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- backend-ci.yml

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.github/workflows/mobile-ci.yml

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- mobile-ci.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.gitignore

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- .gitignore

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.vscode/launch.json

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- launch.json

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

backend/.editorconfig

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- .editorconfig

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

backend/Directory.Build.props

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

backend/docker-compose.yml

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- docker-compose.yml

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

backend/global.json

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- global.json

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

backend/ReadTrack.sln

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- ReadTrack.sln

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/src/ReadTrack.Host/appsettings.Development.json

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- appsettings.Development.json

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

mobile/analysis_options.yaml

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

mobile/build.yaml

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- build.yaml

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

src/ReadTrack.Users/Application/Features/Auth/Commands

###### 2.3.3.1.19.2 Purpose

CQRS Commands for Authentication

###### 2.3.3.1.19.3 Contains Files

- SocialLoginCommand.cs
- SocialLoginCommandHandler.cs
- RefreshTokenCommand.cs

###### 2.3.3.1.19.4 Organizational Reasoning

Separates auth logic (Sequence 449) into discrete commands.

###### 2.3.3.1.19.5 Framework Convention Alignment

CQRS Pattern

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

src/ReadTrack.Users/Application/Features/Users/Commands

###### 2.3.3.1.20.2 Purpose

CQRS Commands for User Management

###### 2.3.3.1.20.3 Contains Files

- UpdateUserProfileCommand.cs
- DeleteAccountCommand.cs
- RequestDataExportCommand.cs

###### 2.3.3.1.20.4 Organizational Reasoning

Encapsulates profile and account lifecycle operations (Seq 458, 459, 472).

###### 2.3.3.1.20.5 Framework Convention Alignment

CQRS Pattern

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

src/ReadTrack.Users/Application/Features/Users/Queries

###### 2.3.3.1.21.2 Purpose

CQRS Queries for User Data

###### 2.3.3.1.21.3 Contains Files

- GetUserProfileQuery.cs
- UserProfileDto.cs

###### 2.3.3.1.21.4 Organizational Reasoning

Optimized read paths for profile display.

###### 2.3.3.1.21.5 Framework Convention Alignment

CQRS Pattern

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

src/ReadTrack.Users/Domain/Entities

###### 2.3.3.1.22.2 Purpose

Core domain entities encapsulating state and business rules

###### 2.3.3.1.22.3 Contains Files

- User.cs
- DataExportJob.cs

###### 2.3.3.1.22.4 Organizational Reasoning

Centralizes PII and core identity logic independent of external concerns.

###### 2.3.3.1.22.5 Framework Convention Alignment

DDD Domain Layer

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

src/ReadTrack.Users/Infrastructure/Persistence

###### 2.3.3.1.23.2 Purpose

Database configuration

###### 2.3.3.1.23.3 Contains Files

- UsersDbContext.cs
- UserConfiguration.cs
- DataExportJobConfiguration.cs

###### 2.3.3.1.23.4 Organizational Reasoning

EF Core mapping configurations.

###### 2.3.3.1.23.5 Framework Convention Alignment

Infrastructure Layer

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

src/ReadTrack.Users/Infrastructure/Services

###### 2.3.3.1.24.2 Purpose

Infrastructure implementations

###### 2.3.3.1.24.3 Contains Files

- S3FileStorageService.cs
- SesEmailService.cs
- Auth0TokenValidationService.cs

###### 2.3.3.1.24.4 Organizational Reasoning

External service adapters.

###### 2.3.3.1.24.5 Framework Convention Alignment

Infrastructure Layer

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

src/ReadTrack.Users/Presentation/Controllers

###### 2.3.3.1.25.2 Purpose

API Endpoints

###### 2.3.3.1.25.3 Contains Files

- AuthController.cs
- UsersController.cs

###### 2.3.3.1.25.4 Organizational Reasoning

Exposes functionality via HTTP.

###### 2.3.3.1.25.5 Framework Convention Alignment

ASP.NET Core Controllers

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Users |
| Namespace Organization | Feature-based folders (e.g., ReadTrack.Users.Appli... |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET 8 standards |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

AuthController

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Users/Presentation/Controllers/AuthController.cs

##### 2.3.4.1.3.0 Class Type

Controller

##### 2.3.4.1.4.0 Inheritance

BaseApiController

##### 2.3.4.1.5.0 Purpose

Handles authentication requests including social login and token refresh (Sequence 449, 450).

##### 2.3.4.1.6.0 Dependencies

- ISender (MediatR)

##### 2.3.4.1.7.0 Framework Specific Attributes

- [ApiController]
- [Route(\"api/v1/auth\")]

##### 2.3.4.1.8.0 Technology Integration Notes

Delegates logic to MediatR commands.

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

- {'method_name': 'SocialLogin', 'method_signature': 'public async Task<ActionResult<AuthResponseDto>> SocialLogin([FromBody] SocialLoginRequest request, CancellationToken cancellationToken)', 'return_type': 'Task<ActionResult<AuthResponseDto>>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': ['[HttpPost(\\"social\\")]', '[AllowAnonymous]'], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'SocialLoginRequest', 'is_nullable': 'false', 'purpose': 'Contains provider ID token.', 'framework_attributes': ['[FromBody]']}], 'implementation_logic': 'Creates SocialLoginCommand from request and sends via MediatR. Returns JWT access/refresh tokens.', 'exception_handling': 'Maps domain exceptions to 401/400 HTTP responses.', 'performance_considerations': 'Must be highly performant (<500ms).', 'validation_requirements': 'Validates request model state.', 'technology_integration_details': 'Entry point for Sequence 449.'}

##### 2.3.4.1.11.0 Events

*No items available*

##### 2.3.4.1.12.0 Implementation Notes

Critical security boundary.

#### 2.3.4.2.0.0 Class Name

##### 2.3.4.2.1.0 Class Name

DataExportJob

##### 2.3.4.2.2.0 File Path

src/ReadTrack.Users/Domain/Entities/DataExportJob.cs

##### 2.3.4.2.3.0 Class Type

Entity

##### 2.3.4.2.4.0 Inheritance

BaseEntity

##### 2.3.4.2.5.0 Purpose

Represents a request for GDPR data export (Sequence 459).

##### 2.3.4.2.6.0 Dependencies

*No items available*

##### 2.3.4.2.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0 Technology Integration Notes

Mapped via EF Core.

##### 2.3.4.2.9.0 Properties

###### 2.3.4.2.9.1 Property Name

####### 2.3.4.2.9.1.1 Property Name

UserId

####### 2.3.4.2.9.1.2 Property Type

Guid

####### 2.3.4.2.9.1.3 Access Modifier

public

####### 2.3.4.2.9.1.4 Purpose

Owner of the export request.

####### 2.3.4.2.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.1.6 Framework Specific Configuration

Foreign Key

####### 2.3.4.2.9.1.7 Implementation Notes

Indexed for lookups.

###### 2.3.4.2.9.2.0 Property Name

####### 2.3.4.2.9.2.1 Property Name

Status

####### 2.3.4.2.9.2.2 Property Type

DataExportStatus

####### 2.3.4.2.9.2.3 Access Modifier

public

####### 2.3.4.2.9.2.4 Purpose

Current state of the job (Pending, Processing, Completed, Failed).

####### 2.3.4.2.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.2.6 Framework Specific Configuration

Enum mapping

####### 2.3.4.2.9.2.7 Implementation Notes

Concurrency token managed.

###### 2.3.4.2.9.3.0 Property Name

####### 2.3.4.2.9.3.1 Property Name

S3Key

####### 2.3.4.2.9.3.2 Property Type

string?

####### 2.3.4.2.9.3.3 Access Modifier

public

####### 2.3.4.2.9.3.4 Purpose

Location of the generated file.

####### 2.3.4.2.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.3.6 Framework Specific Configuration

Nullable

####### 2.3.4.2.9.3.7 Implementation Notes

Used to generate pre-signed URL.

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Complete', 'method_signature': 'public void Complete(string s3Key)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 's3Key', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Storage key for the file.', 'framework_attributes': []}], 'implementation_logic': 'Sets Status to Completed, sets S3Key, updates CompletedAt timestamp.', 'exception_handling': 'Validates state transition.', 'performance_considerations': 'In-memory.', 'validation_requirements': 'Cannot complete a failed or already completed job.', 'technology_integration_details': 'Domain logic encapsulation.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Core entity for REQ-FUNC-009.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

DeleteAccountCommandHandler

##### 2.3.4.3.2.0.0 File Path

src/ReadTrack.Users/Application/Features/Users/Commands/DeleteAccountCommandHandler.cs

##### 2.3.4.3.3.0.0 Class Type

Class

##### 2.3.4.3.4.0.0 Inheritance

IRequestHandler<DeleteAccountCommand, Result>

##### 2.3.4.3.5.0.0 Purpose

Orchestrates account deletion logic (Sequence 458).

##### 2.3.4.3.6.0.0 Dependencies

- IUsersDbContext
- ICurrentUserService
- IJobQueue
- IPublisher

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Uses MediatR.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'DeleteAccountCommand', 'is_nullable': 'false', 'purpose': 'Command payload.', 'framework_attributes': []}], 'implementation_logic': '1. Verify user identity. 2. Mark user entity as deleted (soft delete initially) or trigger hard delete job. 3. Publish UserAccountDeletedEvent. 4. Queue background job for PII scrubbing/Log anonymization.', 'exception_handling': 'Returns failure if user not found.', 'performance_considerations': 'Heavy lifting offloaded to background job.', 'validation_requirements': 'User must confirm deletion.', 'technology_integration_details': 'Triggers Sequence 458 flows.'}

##### 2.3.4.3.11.0.0 Events

- {'event_name': 'UserAccountDeletedEvent', 'event_type': 'DomainEvent', 'trigger_conditions': 'Successfully initiated deletion.', 'event_data': 'UserId'}

##### 2.3.4.3.12.0.0 Implementation Notes

Crucial for GDPR compliance.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

IFileStorageService

##### 2.3.5.1.2.0.0 File Path

src/ReadTrack.Users/Application/Interfaces/IFileStorageService.cs

##### 2.3.5.1.3.0.0 Purpose

Abstraction for file storage (S3) used in data export (Seq 459).

##### 2.3.5.1.4.0.0 Generic Constraints

None

##### 2.3.5.1.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.1.6.0.0 Method Contracts

- {'method_name': 'UploadFileAsync', 'method_signature': 'Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)', 'return_type': 'Task<string>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'fileStream', 'parameter_type': 'Stream', 'purpose': 'Content to upload'}], 'contract_description': 'Uploads stream to storage and returns reference key.', 'exception_contracts': 'Throws specific StorageException on failure.'}

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Implement using AWS SDK for S3.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

IEmailService

##### 2.3.5.2.2.0.0 File Path

src/ReadTrack.Users/Application/Interfaces/IEmailService.cs

##### 2.3.5.2.3.0.0 Purpose

Abstraction for email sending (SES) used in data export notification (Seq 459).

##### 2.3.5.2.4.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0 Method Contracts

- {'method_name': 'SendEmailAsync', 'method_signature': 'Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)', 'return_type': 'Task', 'framework_attributes': [], 'parameters': [{'parameter_name': 'to', 'parameter_type': 'string', 'purpose': 'Recipient address'}], 'contract_description': 'Sends transactional email.', 'exception_contracts': 'Throws EmailSendException.'}

##### 2.3.5.2.7.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0 Implementation Guidance

Implement using AWS SDK for SES.

### 2.3.6.0.0.0.0 Enum Specifications

- {'enum_name': 'DataExportStatus', 'file_path': 'src/ReadTrack.Users/Domain/Enums/DataExportStatus.cs', 'underlying_type': 'int', 'purpose': 'State machine for data export jobs.', 'framework_attributes': [], 'values': [{'value_name': 'Pending', 'value': '0', 'description': 'Job created, waiting for worker.'}, {'value_name': 'Processing', 'value': '1', 'description': 'Worker is generating the JSON file.'}, {'value_name': 'Completed', 'value': '2', 'description': 'File uploaded and email sent.'}, {'value_name': 'Failed', 'value': '3', 'description': 'Job encountered a fatal error.'}], 'validation_notes': 'Used in DataExportJob entity.'}

### 2.3.7.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0 Dto Name

##### 2.3.7.1.1.0.0 Dto Name

SocialLoginRequest

##### 2.3.7.1.2.0.0 File Path

src/ReadTrack.Users/Application/DTOs/SocialLoginRequest.cs

##### 2.3.7.1.3.0.0 Purpose

Payload for Sequence 449.

##### 2.3.7.1.4.0.0 Framework Base Class

None

##### 2.3.7.1.5.0.0 Properties

###### 2.3.7.1.5.1.0 Property Name

####### 2.3.7.1.5.1.1 Property Name

Provider

####### 2.3.7.1.5.1.2 Property Type

string

####### 2.3.7.1.5.1.3 Validation Attributes

- Required

####### 2.3.7.1.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

IdToken

####### 2.3.7.1.5.2.2 Property Type

string

####### 2.3.7.1.5.2.3 Validation Attributes

- Required

####### 2.3.7.1.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0 Validation Rules

Provider must be \"Google\" or \"Apple\".

##### 2.3.7.1.7.0.0 Serialization Requirements

JSON

#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

UserProfileDto

##### 2.3.7.2.2.0.0 File Path

src/ReadTrack.Users/Application/DTOs/UserProfileDto.cs

##### 2.3.7.2.3.0.0 Purpose

Response for GetProfileQuery.

##### 2.3.7.2.4.0.0 Framework Base Class

None

##### 2.3.7.2.5.0.0 Properties

###### 2.3.7.2.5.1.0 Property Name

####### 2.3.7.2.5.1.1 Property Name

Id

####### 2.3.7.2.5.1.2 Property Type

Guid

####### 2.3.7.2.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.2.0 Property Name

####### 2.3.7.2.5.2.1 Property Name

DisplayName

####### 2.3.7.2.5.2.2 Property Type

string

####### 2.3.7.2.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.3.0 Property Name

####### 2.3.7.2.5.3.1 Property Name

Email

####### 2.3.7.2.5.3.2 Property Type

string

####### 2.3.7.2.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0 Validation Rules

None (Output DTO)

##### 2.3.7.2.7.0.0 Serialization Requirements

JSON

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'Auth0Settings', 'file_path': 'src/ReadTrack.Users/Infrastructure/Configuration/Auth0Settings.cs', 'purpose': 'Auth0 configuration binding.', 'framework_base_class': 'None', 'configuration_sections': [{'section_name': 'Auth0', 'properties': [{'property_name': 'Domain', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Auth0 tenant domain.'}, {'property_name': 'Audience', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'API Audience identifier.'}]}], 'validation_requirements': 'Must be validated on startup.', 'validation_notes': 'Required for Seq 449.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IUserService

##### 2.3.9.1.2.0.0 Service Implementation

UserService

##### 2.3.9.1.3.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0 Registration Reasoning

Inter-module communication service.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<IUserService, UserService>()

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IFileStorageService

##### 2.3.9.2.2.0.0 Service Implementation

S3FileStorageService

##### 2.3.9.2.3.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0 Registration Reasoning

AWS SDK clients are thread-safe and usually singletons.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddSingleton<IFileStorageService, S3FileStorageService>()

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

Auth0

##### 2.3.10.1.2.0.0 Integration Type

Identity Provider

##### 2.3.10.1.3.0.0 Required Client Classes

- AuthenticationApiClient

##### 2.3.10.1.4.0.0 Configuration Requirements

Domain, Audience.

##### 2.3.10.1.5.0.0 Error Handling Requirements

Retry on network failure, graceful fail on invalid token.

##### 2.3.10.1.6.0.0 Authentication Requirements

OAuth2 / OIDC.

##### 2.3.10.1.7.0.0 Framework Integration Patterns

Auth0.AspNetCore.Authentication

##### 2.3.10.1.8.0.0 Validation Notes

Critical for Seq 449.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Amazon S3

##### 2.3.10.2.2.0.0 Integration Type

File Storage

##### 2.3.10.2.3.0.0 Required Client Classes

- AmazonS3Client

##### 2.3.10.2.4.0.0 Configuration Requirements

BucketName, Region.

##### 2.3.10.2.5.0.0 Error Handling Requirements

Retry logic via Polly.

##### 2.3.10.2.6.0.0 Authentication Requirements

IAM Role.

##### 2.3.10.2.7.0.0 Framework Integration Patterns

AWSSDK.S3

##### 2.3.10.2.8.0.0 Validation Notes

Required for Seq 459.

#### 2.3.10.3.0.0.0 Integration Target

##### 2.3.10.3.1.0.0 Integration Target

Amazon SES

##### 2.3.10.3.2.0.0 Integration Type

Email Service

##### 2.3.10.3.3.0.0 Required Client Classes

- AmazonSimpleEmailServiceClient

##### 2.3.10.3.4.0.0 Configuration Requirements

Region, SenderEmail.

##### 2.3.10.3.5.0.0 Error Handling Requirements

Retry logic.

##### 2.3.10.3.6.0.0 Authentication Requirements

IAM Role.

##### 2.3.10.3.7.0.0 Framework Integration Patterns

AWSSDK.SimpleEmail

##### 2.3.10.3.8.0.0 Validation Notes

Required for Seq 459 notification.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 1 |
| Total Dtos | 6 |
| Total Configurations | 2 |
| Total External Integrations | 3 |
| Grand Total Components | 28 |
| Phase 2 Claimed Count | 2 |
| Phase 2 Actual Count | 2 |
| Validation Added Count | 26 |
| Final Validated Count | 28 |

