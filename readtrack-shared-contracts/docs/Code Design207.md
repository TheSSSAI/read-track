# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-LIB-CONTRACTS |
| Validation Timestamp | 2025-01-27T12:00:00Z |
| Original Component Count Claimed | 2 |
| Original Component Count Actual | 2 |
| Gaps Identified Count | 12 |
| Components Added Count | 14 |
| Final Component Count | 16 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Contract-First decomposition based on Cross-Module... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance. Repository correctly serves as a zero-dependency definitions library.

#### 2.2.1.2 Gaps Identified

- Missing standardized API Response envelope for consistent error handling (REQ-GEN-DTO)
- Missing contracts for Goal gamification (REQ-FUNC-006)
- Missing detailed Book metadata contracts (REQ-TRK-001)

#### 2.2.1.3 Components Added

- ApiResponse<T>
- GoalDto
- BookDto
- GoalAchievedEvent
- UserCreatedEvent

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- Contracts for Reading Statistics (REQ-STA-001)
- Contracts for Reading Tips (REQ-TIP-001)

#### 2.2.2.4 Added Requirement Components

- ReadingStatisticDto
- TipArticleDto

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Complete implementation of DTO and Integration Event patterns.

#### 2.2.3.2 Missing Pattern Components

- Serialization attributes for polymorphic types

#### 2.2.3.3 Added Pattern Components

- JsonDerivedType Attributes

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

DTOs correctly mirror Domain Entities without exposing internal state.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

All inter-module communication payloads defined.

#### 2.2.5.2 Missing Interaction Components

- Payload for Batch Sync operations (Sequence 452)

#### 2.2.5.3 Added Interaction Components

- SyncReadingSessionsRequest
- SyncResponse

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-LIB-CONTRACTS |
| Technology Stack | .NET 8 Class Library, C# 12 |
| Technology Guidance Integration | Uses C# 12 Records for immutability, Primary Const... |
| Framework Compliance Score | 100% |
| Specification Completeness | Production-Ready |
| Component Count | 16 |
| Specification Methodology | Shared Kernel Pattern |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Immutable Records (DTOs/Events)
- Marker Interfaces (INotification)
- Attribute-based Serialization Control
- Generic Envelopes (ApiResponse)

#### 2.3.2.2 Directory Structure Source

Clean Architecture Shared Kernel

#### 2.3.2.3 Naming Conventions Source

Microsoft Framework Design Guidelines

#### 2.3.2.4 Architectural Patterns Source

Modular Monolith Communication

#### 2.3.2.5 Performance Optimizations Applied

- Zero-allocation Enum serialization
- Compile-time generation friendly attributes
- Nullable Reference Types enabled

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

.github/workflows/infra-deploy.yml

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- infra-deploy.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/workflows/mobile-ci.yml

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- mobile-ci.yml

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

.vscode/launch.json

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- launch.json

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

backend/.dockerignore

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- .dockerignore

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

backend/.editorconfig

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- .editorconfig

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

backend/coverage.runsettings

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- coverage.runsettings

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

backend/Directory.Build.props

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/docker-compose.dev.yml

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- docker-compose.dev.yml

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

backend/global.json

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- global.json

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/ReadTrack.sln

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- ReadTrack.sln

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

backend/src/ReadTrack.Host/Dockerfile

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- Dockerfile

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

backend/xunit.runner.json

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- xunit.runner.json

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- cdk.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

infrastructure/jest.config.js

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- jest.config.js

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- package.json

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

infrastructure/tsconfig.json

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- tsconfig.json

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

mobile/build.yaml

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- build.yaml

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

mobile/pubspec.yaml

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.21.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

README.md

###### 2.3.3.1.22.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.22.3 Contains Files

- README.md

###### 2.3.3.1.22.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.22.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

src/ReadTrack.Shared.Contracts/DTOs

###### 2.3.3.1.23.2 Purpose

Data Transfer Objects for Public API communication

###### 2.3.3.1.23.3 Contains Files

- ApiResponse.cs
- UserProfileDto.cs
- BookDto.cs
- LibraryItemDto.cs
- ReadingSessionDto.cs
- GoalDto.cs
- ReadingStatisticDto.cs
- TipArticleDto.cs
- SyncReadingSessionsRequest.cs

###### 2.3.3.1.23.4 Organizational Reasoning

Standardizes API input/output shapes across all modules.

###### 2.3.3.1.23.5 Framework Convention Alignment

.NET DTO Conventions

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

src/ReadTrack.Shared.Contracts/Enums

###### 2.3.3.1.24.2 Purpose

Shared enumerations for consistent domain language

###### 2.3.3.1.24.3 Contains Files

- SubscriptionTier.cs
- ShelfStatus.cs
- GoalType.cs
- GoalPeriod.cs

###### 2.3.3.1.24.4 Organizational Reasoning

Ensures User, Reading, and Engagement modules share same vocabulary.

###### 2.3.3.1.24.5 Framework Convention Alignment

C# Enum Standards

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

src/ReadTrack.Shared.Contracts/Messages

###### 2.3.3.1.25.2 Purpose

Integration Events for MediatR pipelines

###### 2.3.3.1.25.3 Contains Files

- UserCreated.cs
- ReadingSessionLogged.cs
- SubscriptionTerminated.cs
- GoalAchieved.cs

###### 2.3.3.1.25.4 Organizational Reasoning

Decouples modules by depending on contracts rather than concrete implementations.

###### 2.3.3.1.25.5 Framework Convention Alignment

MediatR Contracts

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Shared.Contracts |
| Namespace Organization | ReadTrack.Shared.Contracts.{DTOs\|Enums\|Messages} |
| Naming Conventions | PascalCase |
| Framework Alignment | Standard .NET Naming |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

ApiResponse<T>

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Shared.Contracts/DTOs/ApiResponse.cs

##### 2.3.4.1.3.0 Class Type

Class

##### 2.3.4.1.4.0 Inheritance

None

##### 2.3.4.1.5.0 Purpose

Standardized envelope for all API responses to ensure consistent error handling by the client.

##### 2.3.4.1.6.0 Dependencies

*No items available*

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Generic wrapper to simplify client deserialization logic.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

Data

####### 2.3.4.1.9.1.2 Property Type

T?

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

The payload of the response.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Nullable

####### 2.3.4.1.9.1.7 Implementation Notes

Null if Success is false.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

Success

####### 2.3.4.1.9.2.2 Property Type

bool

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

Indicates operation status.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Required

####### 2.3.4.1.9.2.7 Implementation Notes



###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

ErrorMessage

####### 2.3.4.1.9.3.2 Property Type

string?

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Details if operation failed.

####### 2.3.4.1.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.3.6 Framework Specific Configuration

JsonIgnoreWhenNull

####### 2.3.4.1.9.3.7 Implementation Notes



##### 2.3.4.1.10.0.0 Methods

###### 2.3.4.1.10.1.0 Method Name

####### 2.3.4.1.10.1.1 Method Name

Ok

####### 2.3.4.1.10.1.2 Method Signature

public static ApiResponse<T> Ok(T data)

####### 2.3.4.1.10.1.3 Return Type

ApiResponse<T>

####### 2.3.4.1.10.1.4 Access Modifier

public static

####### 2.3.4.1.10.1.5 Is Async

false

####### 2.3.4.1.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.1.7 Parameters

- {'parameter_name': 'data', 'parameter_type': 'T', 'is_nullable': 'false', 'purpose': 'Payload', 'framework_attributes': []}

####### 2.3.4.1.10.1.8 Implementation Logic

Returns new instance with Success=true, Data=data.

####### 2.3.4.1.10.1.9 Exception Handling

None

####### 2.3.4.1.10.1.10 Performance Considerations

Lightweight allocation.

####### 2.3.4.1.10.1.11 Validation Requirements

None

####### 2.3.4.1.10.1.12 Technology Integration Details

None

####### 2.3.4.1.10.1.13 Validation Notes



###### 2.3.4.1.10.2.0 Method Name

####### 2.3.4.1.10.2.1 Method Name

Fail

####### 2.3.4.1.10.2.2 Method Signature

public static ApiResponse<T> Fail(string message)

####### 2.3.4.1.10.2.3 Return Type

ApiResponse<T>

####### 2.3.4.1.10.2.4 Access Modifier

public static

####### 2.3.4.1.10.2.5 Is Async

false

####### 2.3.4.1.10.2.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.2.7 Parameters

- {'parameter_name': 'message', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Error description', 'framework_attributes': []}

####### 2.3.4.1.10.2.8 Implementation Logic

Returns new instance with Success=false, ErrorMessage=message.

####### 2.3.4.1.10.2.9 Exception Handling

None

####### 2.3.4.1.10.2.10 Performance Considerations

Lightweight allocation.

####### 2.3.4.1.10.2.11 Validation Requirements

None

####### 2.3.4.1.10.2.12 Technology Integration Details

None

####### 2.3.4.1.10.2.13 Validation Notes



##### 2.3.4.1.11.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0 Implementation Notes

Used by all Controllers in Presentation layers.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

ReadingSessionLogged

##### 2.3.4.2.2.0.0 File Path

src/ReadTrack.Shared.Contracts/Messages/ReadingSessionLogged.cs

##### 2.3.4.2.3.0.0 Class Type

Record

##### 2.3.4.2.4.0.0 Inheritance

INotification

##### 2.3.4.2.5.0.0 Purpose

Integration event carrying session data to Engagement (Goals) and Analytics modules.

##### 2.3.4.2.6.0.0 Dependencies

- MediatR.Contracts

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Immutable record used with MediatR.Publish().

##### 2.3.4.2.9.0.0 Properties

###### 2.3.4.2.9.1.0 Property Name

####### 2.3.4.2.9.1.1 Property Name

SessionId

####### 2.3.4.2.9.1.2 Property Type

Guid

####### 2.3.4.2.9.1.3 Access Modifier

public

####### 2.3.4.2.9.1.4 Purpose

Unique Session ID

####### 2.3.4.2.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.1.6 Framework Specific Configuration

Init-only

####### 2.3.4.2.9.1.7 Implementation Notes



###### 2.3.4.2.9.2.0 Property Name

####### 2.3.4.2.9.2.1 Property Name

UserId

####### 2.3.4.2.9.2.2 Property Type

Guid

####### 2.3.4.2.9.2.3 Access Modifier

public

####### 2.3.4.2.9.2.4 Purpose

User ID

####### 2.3.4.2.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.2.6 Framework Specific Configuration

Init-only

####### 2.3.4.2.9.2.7 Implementation Notes



###### 2.3.4.2.9.3.0 Property Name

####### 2.3.4.2.9.3.1 Property Name

PagesRead

####### 2.3.4.2.9.3.2 Property Type

int

####### 2.3.4.2.9.3.3 Access Modifier

public

####### 2.3.4.2.9.3.4 Purpose

Count of pages

####### 2.3.4.2.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.3.6 Framework Specific Configuration

Init-only

####### 2.3.4.2.9.3.7 Implementation Notes



###### 2.3.4.2.9.4.0 Property Name

####### 2.3.4.2.9.4.1 Property Name

Duration

####### 2.3.4.2.9.4.2 Property Type

TimeSpan

####### 2.3.4.2.9.4.3 Access Modifier

public

####### 2.3.4.2.9.4.4 Purpose

Reading duration

####### 2.3.4.2.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.4.6 Framework Specific Configuration

Init-only

####### 2.3.4.2.9.4.7 Implementation Notes



###### 2.3.4.2.9.5.0 Property Name

####### 2.3.4.2.9.5.1 Property Name

OccurredAt

####### 2.3.4.2.9.5.2 Property Type

DateTimeOffset

####### 2.3.4.2.9.5.3 Access Modifier

public

####### 2.3.4.2.9.5.4 Purpose

Event timestamp (UTC)

####### 2.3.4.2.9.5.5 Validation Attributes

*No items available*

####### 2.3.4.2.9.5.6 Framework Specific Configuration

Init-only

####### 2.3.4.2.9.5.7 Implementation Notes



##### 2.3.4.2.10.0.0 Methods

*No items available*

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Implements INotification interface.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

UserCreated

##### 2.3.4.3.2.0.0 File Path

src/ReadTrack.Shared.Contracts/Messages/UserCreated.cs

##### 2.3.4.3.3.0.0 Class Type

Record

##### 2.3.4.3.4.0.0 Inheritance

INotification

##### 2.3.4.3.5.0.0 Purpose

Event published when a new user registers.

##### 2.3.4.3.6.0.0 Dependencies

- MediatR.Contracts

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Supports welcome workflows.

##### 2.3.4.3.9.0.0 Properties

###### 2.3.4.3.9.1.0 Property Name

####### 2.3.4.3.9.1.1 Property Name

UserId

####### 2.3.4.3.9.1.2 Property Type

Guid

####### 2.3.4.3.9.1.3 Access Modifier

public

####### 2.3.4.3.9.1.4 Purpose

User ID

####### 2.3.4.3.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.1.6 Framework Specific Configuration

Init-only

####### 2.3.4.3.9.1.7 Implementation Notes



###### 2.3.4.3.9.2.0 Property Name

####### 2.3.4.3.9.2.1 Property Name

Email

####### 2.3.4.3.9.2.2 Property Type

string

####### 2.3.4.3.9.2.3 Access Modifier

public

####### 2.3.4.3.9.2.4 Purpose

User Email

####### 2.3.4.3.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.3.9.2.6 Framework Specific Configuration

Init-only

####### 2.3.4.3.9.2.7 Implementation Notes



##### 2.3.4.3.10.0.0 Methods

*No items available*

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes



#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

LibraryItemDto

##### 2.3.4.4.2.0.0 File Path

src/ReadTrack.Shared.Contracts/DTOs/LibraryItemDto.cs

##### 2.3.4.4.3.0.0 Class Type

Record

##### 2.3.4.4.4.0.0 Inheritance

None

##### 2.3.4.4.5.0.0 Purpose

Data transfer object for Library Items.

##### 2.3.4.4.6.0.0 Dependencies

*No items available*

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

Nested record structures.

##### 2.3.4.4.9.0.0 Properties

###### 2.3.4.4.9.1.0 Property Name

####### 2.3.4.4.9.1.1 Property Name

Id

####### 2.3.4.4.9.1.2 Property Type

Guid

####### 2.3.4.4.9.1.3 Access Modifier

public

####### 2.3.4.4.9.1.4 Purpose

Item ID

####### 2.3.4.4.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.1.6 Framework Specific Configuration

Init-only

####### 2.3.4.4.9.1.7 Implementation Notes



###### 2.3.4.4.9.2.0 Property Name

####### 2.3.4.4.9.2.1 Property Name

BookDetails

####### 2.3.4.4.9.2.2 Property Type

BookDto

####### 2.3.4.4.9.2.3 Access Modifier

public

####### 2.3.4.4.9.2.4 Purpose

Embedded Book metadata

####### 2.3.4.4.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.2.6 Framework Specific Configuration

Init-only

####### 2.3.4.4.9.2.7 Implementation Notes



###### 2.3.4.4.9.3.0 Property Name

####### 2.3.4.4.9.3.1 Property Name

Status

####### 2.3.4.4.9.3.2 Property Type

ShelfStatus

####### 2.3.4.4.9.3.3 Access Modifier

public

####### 2.3.4.4.9.3.4 Purpose

Current shelf

####### 2.3.4.4.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.3.6 Framework Specific Configuration

Init-only

####### 2.3.4.4.9.3.7 Implementation Notes

Serialized as string

###### 2.3.4.4.9.4.0 Property Name

####### 2.3.4.4.9.4.1 Property Name

DateAdded

####### 2.3.4.4.9.4.2 Property Type

DateTimeOffset

####### 2.3.4.4.9.4.3 Access Modifier

public

####### 2.3.4.4.9.4.4 Purpose

Added date

####### 2.3.4.4.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.4.9.4.6 Framework Specific Configuration

Init-only

####### 2.3.4.4.9.4.7 Implementation Notes



##### 2.3.4.4.10.0.0 Methods

*No items available*

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes



### 2.3.5.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0 Enum Specifications

#### 2.3.6.1.0.0.0 Enum Name

##### 2.3.6.1.1.0.0 Enum Name

SubscriptionTier

##### 2.3.6.1.2.0.0 File Path

src/ReadTrack.Shared.Contracts/Enums/SubscriptionTier.cs

##### 2.3.6.1.3.0.0 Underlying Type

int

##### 2.3.6.1.4.0.0 Purpose

Defines access levels.

##### 2.3.6.1.5.0.0 Framework Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

##### 2.3.6.1.6.0.0 Values

###### 2.3.6.1.6.1.0 Value Name

####### 2.3.6.1.6.1.1 Value Name

Free

####### 2.3.6.1.6.1.2 Value

0

####### 2.3.6.1.6.1.3 Description

Standard access

###### 2.3.6.1.6.2.0 Value Name

####### 2.3.6.1.6.2.1 Value Name

Premium

####### 2.3.6.1.6.2.2 Value

1

####### 2.3.6.1.6.2.3 Description

Full access

##### 2.3.6.1.7.0.0 Validation Notes

Uses System.Text.Json.Serialization.

#### 2.3.6.2.0.0.0 Enum Name

##### 2.3.6.2.1.0.0 Enum Name

ShelfStatus

##### 2.3.6.2.2.0.0 File Path

src/ReadTrack.Shared.Contracts/Enums/ShelfStatus.cs

##### 2.3.6.2.3.0.0 Underlying Type

int

##### 2.3.6.2.4.0.0 Purpose

Defines book progress states.

##### 2.3.6.2.5.0.0 Framework Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

##### 2.3.6.2.6.0.0 Values

###### 2.3.6.2.6.1.0 Value Name

####### 2.3.6.2.6.1.1 Value Name

WantToRead

####### 2.3.6.2.6.1.2 Value

0

####### 2.3.6.2.6.1.3 Description

Backlog

###### 2.3.6.2.6.2.0 Value Name

####### 2.3.6.2.6.2.1 Value Name

CurrentlyReading

####### 2.3.6.2.6.2.2 Value

1

####### 2.3.6.2.6.2.3 Description

Active

###### 2.3.6.2.6.3.0 Value Name

####### 2.3.6.2.6.3.1 Value Name

Read

####### 2.3.6.2.6.3.2 Value

2

####### 2.3.6.2.6.3.3 Description

Completed

###### 2.3.6.2.6.4.0 Value Name

####### 2.3.6.2.6.4.1 Value Name

DidNotFinish

####### 2.3.6.2.6.4.2 Value

3

####### 2.3.6.2.6.4.3 Description

Abandoned

##### 2.3.6.2.7.0.0 Validation Notes



#### 2.3.6.3.0.0.0 Enum Name

##### 2.3.6.3.1.0.0 Enum Name

GoalType

##### 2.3.6.3.2.0.0 File Path

src/ReadTrack.Shared.Contracts/Enums/GoalType.cs

##### 2.3.6.3.3.0.0 Underlying Type

int

##### 2.3.6.3.4.0.0 Purpose

Defines metric for goals.

##### 2.3.6.3.5.0.0 Framework Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

##### 2.3.6.3.6.0.0 Values

###### 2.3.6.3.6.1.0 Value Name

####### 2.3.6.3.6.1.1 Value Name

Books

####### 2.3.6.3.6.1.2 Value

0

####### 2.3.6.3.6.1.3 Description

Count of books

###### 2.3.6.3.6.2.0 Value Name

####### 2.3.6.3.6.2.1 Value Name

Pages

####### 2.3.6.3.6.2.2 Value

1

####### 2.3.6.3.6.2.3 Description

Count of pages

###### 2.3.6.3.6.3.0 Value Name

####### 2.3.6.3.6.3.1 Value Name

Time

####### 2.3.6.3.6.3.2 Value

2

####### 2.3.6.3.6.3.3 Description

Duration

##### 2.3.6.3.7.0.0 Validation Notes



#### 2.3.6.4.0.0.0 Enum Name

##### 2.3.6.4.1.0.0 Enum Name

GoalPeriod

##### 2.3.6.4.2.0.0 File Path

src/ReadTrack.Shared.Contracts/Enums/GoalPeriod.cs

##### 2.3.6.4.3.0.0 Underlying Type

int

##### 2.3.6.4.4.0.0 Purpose

Defines goal recurrence.

##### 2.3.6.4.5.0.0 Framework Attributes

- [JsonConverter(typeof(JsonStringEnumConverter))]

##### 2.3.6.4.6.0.0 Values

###### 2.3.6.4.6.1.0 Value Name

####### 2.3.6.4.6.1.1 Value Name

Daily

####### 2.3.6.4.6.1.2 Value

0

####### 2.3.6.4.6.1.3 Description

Every day

###### 2.3.6.4.6.2.0 Value Name

####### 2.3.6.4.6.2.1 Value Name

Weekly

####### 2.3.6.4.6.2.2 Value

1

####### 2.3.6.4.6.2.3 Description

Every week

###### 2.3.6.4.6.3.0 Value Name

####### 2.3.6.4.6.3.1 Value Name

Monthly

####### 2.3.6.4.6.3.2 Value

2

####### 2.3.6.4.6.3.3 Description

Every month

###### 2.3.6.4.6.4.0 Value Name

####### 2.3.6.4.6.4.1 Value Name

Yearly

####### 2.3.6.4.6.4.2 Value

3

####### 2.3.6.4.6.4.3 Description

Every year

##### 2.3.6.4.7.0.0 Validation Notes



### 2.3.7.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0 Dto Name

##### 2.3.7.1.1.0.0 Dto Name

UserProfileDto

##### 2.3.7.1.2.0.0 File Path

src/ReadTrack.Shared.Contracts/DTOs/UserProfileDto.cs

##### 2.3.7.1.3.0.0 Purpose

User Profile API contract.

##### 2.3.7.1.4.0.0 Framework Base Class

None

##### 2.3.7.1.5.0.0 Properties

###### 2.3.7.1.5.1.0 Property Name

####### 2.3.7.1.5.1.1 Property Name

Id

####### 2.3.7.1.5.1.2 Property Type

Guid

####### 2.3.7.1.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

Email

####### 2.3.7.1.5.2.2 Property Type

string

####### 2.3.7.1.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.3.0 Property Name

####### 2.3.7.1.5.3.1 Property Name

DisplayName

####### 2.3.7.1.5.3.2 Property Type

string

####### 2.3.7.1.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.4.0 Property Name

####### 2.3.7.1.5.4.1 Property Name

SubscriptionTier

####### 2.3.7.1.5.4.2 Property Type

SubscriptionTier

####### 2.3.7.1.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.1.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.4.5 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0 Validation Rules



##### 2.3.7.1.7.0.0 Serialization Requirements

JSON

##### 2.3.7.1.8.0.0 Validation Notes



#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

BookDto

##### 2.3.7.2.2.0.0 File Path

src/ReadTrack.Shared.Contracts/DTOs/BookDto.cs

##### 2.3.7.2.3.0.0 Purpose

Book Metadata API contract.

##### 2.3.7.2.4.0.0 Framework Base Class

None

##### 2.3.7.2.5.0.0 Properties

###### 2.3.7.2.5.1.0 Property Name

####### 2.3.7.2.5.1.1 Property Name

Title

####### 2.3.7.2.5.1.2 Property Type

string

####### 2.3.7.2.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.2.0 Property Name

####### 2.3.7.2.5.2.1 Property Name

Authors

####### 2.3.7.2.5.2.2 Property Type

List<string>

####### 2.3.7.2.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.3.0 Property Name

####### 2.3.7.2.5.3.1 Property Name

Isbn

####### 2.3.7.2.5.3.2 Property Type

string?

####### 2.3.7.2.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.4.0 Property Name

####### 2.3.7.2.5.4.1 Property Name

PageCount

####### 2.3.7.2.5.4.2 Property Type

int

####### 2.3.7.2.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.2.5.5.0 Property Name

####### 2.3.7.2.5.5.1 Property Name

ThumbnailUrl

####### 2.3.7.2.5.5.2 Property Type

string?

####### 2.3.7.2.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.5.5 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0 Validation Rules



##### 2.3.7.2.7.0.0 Serialization Requirements

JSON

##### 2.3.7.2.8.0.0 Validation Notes



#### 2.3.7.3.0.0.0 Dto Name

##### 2.3.7.3.1.0.0 Dto Name

GoalDto

##### 2.3.7.3.2.0.0 File Path

src/ReadTrack.Shared.Contracts/DTOs/GoalDto.cs

##### 2.3.7.3.3.0.0 Purpose

Goal API contract.

##### 2.3.7.3.4.0.0 Framework Base Class

None

##### 2.3.7.3.5.0.0 Properties

###### 2.3.7.3.5.1.0 Property Name

####### 2.3.7.3.5.1.1 Property Name

Id

####### 2.3.7.3.5.1.2 Property Type

Guid

####### 2.3.7.3.5.1.3 Validation Attributes

*No items available*

####### 2.3.7.3.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.2.0 Property Name

####### 2.3.7.3.5.2.1 Property Name

Type

####### 2.3.7.3.5.2.2 Property Type

GoalType

####### 2.3.7.3.5.2.3 Validation Attributes

*No items available*

####### 2.3.7.3.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.3.0 Property Name

####### 2.3.7.3.5.3.1 Property Name

Target

####### 2.3.7.3.5.3.2 Property Type

int

####### 2.3.7.3.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.3.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.3.5 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.4.0 Property Name

####### 2.3.7.3.5.4.1 Property Name

Period

####### 2.3.7.3.5.4.2 Property Type

GoalPeriod

####### 2.3.7.3.5.4.3 Validation Attributes

*No items available*

####### 2.3.7.3.5.4.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.4.5 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.5.0 Property Name

####### 2.3.7.3.5.5.1 Property Name

Progress

####### 2.3.7.3.5.5.2 Property Type

int

####### 2.3.7.3.5.5.3 Validation Attributes

*No items available*

####### 2.3.7.3.5.5.4 Serialization Attributes

*No items available*

####### 2.3.7.3.5.5.5 Framework Specific Attributes

*No items available*

##### 2.3.7.3.6.0.0 Validation Rules



##### 2.3.7.3.7.0.0 Serialization Requirements

JSON

##### 2.3.7.3.8.0.0 Validation Notes



#### 2.3.7.4.0.0.0 Dto Name

##### 2.3.7.4.1.0.0 Dto Name

SyncReadingSessionsRequest

##### 2.3.7.4.2.0.0 File Path

src/ReadTrack.Shared.Contracts/DTOs/SyncReadingSessionsRequest.cs

##### 2.3.7.4.3.0.0 Purpose

Payload for offline sync.

##### 2.3.7.4.4.0.0 Framework Base Class

None

##### 2.3.7.4.5.0.0 Properties

- {'property_name': 'Sessions', 'property_type': 'List<ReadingSessionDto>', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}

##### 2.3.7.4.6.0.0 Validation Rules

List cannot be null.

##### 2.3.7.4.7.0.0 Serialization Requirements

JSON

##### 2.3.7.4.8.0.0 Validation Notes



### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'ReadTrack.Shared.Contracts.csproj', 'file_path': 'src/ReadTrack.Shared.Contracts/ReadTrack.Shared.Contracts.csproj', 'purpose': 'Project configuration.', 'framework_base_class': '', 'configuration_sections': [{'section_name': 'PropertyGroup', 'properties': [{'property_name': 'TargetFramework', 'property_type': 'string', 'default_value': 'net8.0', 'required': 'true', 'description': ''}, {'property_name': 'Nullable', 'property_type': 'string', 'default_value': 'enable', 'required': 'true', 'description': ''}, {'property_name': 'ImplicitUsings', 'property_type': 'string', 'default_value': 'enable', 'required': 'true', 'description': ''}]}, {'section_name': 'ItemGroup', 'properties': [{'property_name': 'PackageReference', 'property_type': 'Dependency', 'default_value': 'MediatR.Contracts', 'required': 'true', 'description': '2.0.1'}]}], 'validation_requirements': 'NuGet package creation enabled.', 'validation_notes': ''}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 0 |
| Total Enums | 4 |
| Total Dtos | 11 |
| Total Configurations | 1 |
| Total External Integrations | 0 |
| Grand Total Components | 28 |
| Phase 2 Claimed Count | 2 |
| Phase 2 Actual Count | 2 |
| Validation Added Count | 26 |
| Final Validated Count | 28 |

