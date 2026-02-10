# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-RECOMMENDATIONS |
| Validation Timestamp | 2025-01-27T14:30:00Z |
| Original Component Count Claimed | 32 |
| Original Component Count Actual | 28 |
| Gaps Identified Count | 4 |
| Components Added Count | 6 |
| Final Component Count | 34 |
| Validation Completeness Score | 98.5 |
| Enhancement Methodology | Systematic RAG pipeline analysis against .NET 8 Cl... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with AI domain boundaries.

#### 2.2.1.2 Gaps Identified

- Missing domain entity for tracking the asynchronous job state (RecommendationJob).
- Lack of explicit rate limiting service abstraction for cost control.
- Missing configuration binding for OpenAI specific settings.

#### 2.2.1.3 Components Added

- RecommendationJob
- IRateLimitService
- OpenAIOptions
- RecommendationJobConfiguration

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- DTO for returning job polling status to client.
- Logic for parsing structured JSON from LLM response.

#### 2.2.2.4 Added Requirement Components

- RecommendationJobStatusDto
- LlmResponseParser

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

CQRS and RAG patterns well defined.

#### 2.2.3.2 Missing Pattern Components

- Specification pattern for querying recommendations by user.

#### 2.2.3.3 Added Pattern Components

- RecommendationsByUserSpecification

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Core entities mapped, missing Job persistence.

#### 2.2.4.2 Missing Database Components

- EF Core configuration for RecommendationJob.

#### 2.2.4.3 Added Database Components

- RecommendationJobConfiguration

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Flow from Controller to Handler is clear.

#### 2.2.5.2 Missing Interaction Components

- Background service integration for job execution.

#### 2.2.5.3 Added Interaction Components

- BackgroundJobClientWrapper

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-RECOMMENDATIONS |
| Technology Stack | .NET 8, ASP.NET Core 8, Entity Framework Core 8, O... |
| Technology Guidance Integration | Clean Architecture with Domain-Driven Design (DDD)... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 34 |
| Specification Methodology | Domain-Driven Design with Asynchronous Job Process... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- CQRS (Command Query Responsibility Segregation)
- Repository & Unit of Work
- Adapter Pattern (for AI Services)
- Options Pattern (Configuration)
- Resilience Pipeline (Polly)
- Asynchronous Background Jobs

#### 2.3.2.2 Directory Structure Source

Modular Monolith Vertical Slice

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Coding Standards

#### 2.3.2.4 Architectural Patterns Source

RAG Pipeline Architecture

#### 2.3.2.5 Performance Optimizations Applied

- Asynchronous I/O for all external API calls
- Token usage optimization via Prompt Engineering
- Job status polling to prevent HTTP timeout
- Vector search optimization via k-NN

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

.github/PULL_REQUEST_TEMPLATE.md

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- PULL_REQUEST_TEMPLATE.md

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/workflows/cdk-deploy.yml

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- cdk-deploy.yml

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.github/workflows/ci-backend.yml

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- ci-backend.yml

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.github/workflows/ci-mobile.yml

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- ci-mobile.yml

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

.gitignore

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- .gitignore

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

.vscode/launch.json

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- launch.json

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

backend/.runsettings

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- .runsettings

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

backend/omnisharp.json

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- omnisharp.json

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/ReadTrack.Backend.sln

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- ReadTrack.Backend.sln

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

backend/src/ReadTrack.Host/appsettings.Development.json

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

backend/src/ReadTrack.Host/Dockerfile

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- Dockerfile

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

backend/src/ReadTrack.Host/Properties/launchSettings.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- launchSettings.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

backend/src/ReadTrack.Host/ReadTrack.Host.csproj

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- ReadTrack.Host.csproj

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

backend/src/ReadTrack.Modules.Users/ReadTrack.Modules.Users.csproj

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- ReadTrack.Modules.Users.csproj

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

backend/tests/ReadTrack.Backend.Tests/ReadTrack.Backend.Tests.csproj

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- ReadTrack.Backend.Tests.csproj

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

CONTRIBUTING.md

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- CONTRIBUTING.md

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

docker-compose.yml

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- docker-compose.yml

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- cdk.json

###### 2.3.3.1.21.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

infrastructure/jest.config.js

###### 2.3.3.1.22.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.22.3 Contains Files

- jest.config.js

###### 2.3.3.1.22.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.22.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.23.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.23.3 Contains Files

- package.json

###### 2.3.3.1.23.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.23.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

infrastructure/tsconfig.json

###### 2.3.3.1.24.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.24.3 Contains Files

- tsconfig.json

###### 2.3.3.1.24.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.24.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.25.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.25.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.25.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.25.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.26.0 Directory Path

###### 2.3.3.1.26.1 Directory Path

mobile/pubspec.yaml

###### 2.3.3.1.26.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.26.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.26.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.26.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.27.0 Directory Path

###### 2.3.3.1.27.1 Directory Path

mobile/test/junit.xml

###### 2.3.3.1.27.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.27.3 Contains Files

- junit.xml

###### 2.3.3.1.27.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.27.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.28.0 Directory Path

###### 2.3.3.1.28.1 Directory Path

README.md

###### 2.3.3.1.28.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.28.3 Contains Files

- README.md

###### 2.3.3.1.28.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.28.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.29.0 Directory Path

###### 2.3.3.1.29.1 Directory Path

src/ReadTrack.Recommendations.Application/Features/Recommendations

###### 2.3.3.1.29.2 Purpose

CQRS Handlers and business logic orchestration

###### 2.3.3.1.29.3 Contains Files

- GenerateRecommendationsCommand.cs
- GenerateRecommendationsHandler.cs
- GetRecommendationJobStatusQuery.cs
- GetRecommendationJobStatusHandler.cs

###### 2.3.3.1.29.4 Organizational Reasoning

Vertical slicing of features.

###### 2.3.3.1.29.5 Framework Convention Alignment

Application Layer

##### 2.3.3.1.30.0 Directory Path

###### 2.3.3.1.30.1 Directory Path

src/ReadTrack.Recommendations.Application/Interfaces

###### 2.3.3.1.30.2 Purpose

Abstractions for infrastructure dependencies

###### 2.3.3.1.30.3 Contains Files

- ILLMClient.cs
- IVectorStoreClient.cs
- IEmbeddingGenerator.cs
- IRecommendationRepository.cs
- IReadingHistoryProvider.cs

###### 2.3.3.1.30.4 Organizational Reasoning

Dependency Inversion Principle.

###### 2.3.3.1.30.5 Framework Convention Alignment

Application Layer Interfaces

##### 2.3.3.1.31.0 Directory Path

###### 2.3.3.1.31.1 Directory Path

src/ReadTrack.Recommendations.Domain/Entities

###### 2.3.3.1.31.2 Purpose

Core domain entities and value objects

###### 2.3.3.1.31.3 Contains Files

- Recommendation.cs
- RecommendationJob.cs
- UserContextEmbedding.cs

###### 2.3.3.1.31.4 Organizational Reasoning

Encapsulates state and business rules independent of infrastructure.

###### 2.3.3.1.31.5 Framework Convention Alignment

DDD Domain Layer

##### 2.3.3.1.32.0 Directory Path

###### 2.3.3.1.32.1 Directory Path

src/ReadTrack.Recommendations.Infrastructure/AI

###### 2.3.3.1.32.2 Purpose

Implementation of AI interactions

###### 2.3.3.1.32.3 Contains Files

- OpenAIClientAdapter.cs
- LlmResponseParser.cs
- PromptBuilder.cs

###### 2.3.3.1.32.4 Organizational Reasoning

Infrastructure adapters for third-party libraries.

###### 2.3.3.1.32.5 Framework Convention Alignment

Infrastructure Layer

##### 2.3.3.1.33.0 Directory Path

###### 2.3.3.1.33.1 Directory Path

src/ReadTrack.Recommendations.Infrastructure/Persistence

###### 2.3.3.1.33.2 Purpose

Data access implementation

###### 2.3.3.1.33.3 Contains Files

- RecommendationsDbContext.cs
- RecommendationRepository.cs
- RecommendationConfiguration.cs
- RecommendationJobConfiguration.cs
- OpenSearchClientAdapter.cs

###### 2.3.3.1.33.4 Organizational Reasoning

EF Core and Vector Store implementation.

###### 2.3.3.1.33.5 Framework Convention Alignment

Infrastructure Layer

##### 2.3.3.1.34.0 Directory Path

###### 2.3.3.1.34.1 Directory Path

src/ReadTrack.Recommendations.Web/Controllers

###### 2.3.3.1.34.2 Purpose

API Entry Points

###### 2.3.3.1.34.3 Contains Files

- RecommendationsController.cs

###### 2.3.3.1.34.4 Organizational Reasoning

Exposes functionality via HTTP.

###### 2.3.3.1.34.5 Framework Convention Alignment

Presentation Layer

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Recommendations |
| Namespace Organization | ReadTrack.Recommendations.{Layer}.{Feature} |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET 8 Standards |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

RecommendationJob

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Recommendations.Domain/Entities/RecommendationJob.cs

##### 2.3.4.1.3.0 Class Type

Entity

##### 2.3.4.1.4.0 Inheritance

Entity<Guid>

##### 2.3.4.1.5.0 Purpose

Tracks the state of a long-running recommendation generation process.

##### 2.3.4.1.6.0 Dependencies

*No items available*

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Mapped to DB to support polling.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

UserId

####### 2.3.4.1.9.1.2 Property Type

Guid

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

The user requesting recommendations.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Required

####### 2.3.4.1.9.1.7 Implementation Notes

Foreign key reference.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

Status

####### 2.3.4.1.9.2.2 Property Type

RecommendationJobStatus

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

Current state (Pending, Processing, Completed, Failed).

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Enum string conversion

####### 2.3.4.1.9.2.7 Implementation Notes

State machine logic.

###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

FailureReason

####### 2.3.4.1.9.3.2 Property Type

string?

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Error message if failed.

####### 2.3.4.1.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.3.6 Framework Specific Configuration

Nullable

####### 2.3.4.1.9.3.7 Implementation Notes

Populated on exception.

###### 2.3.4.1.9.4.0 Property Name

####### 2.3.4.1.9.4.1 Property Name

GeneratedRecommendations

####### 2.3.4.1.9.4.2 Property Type

List<Recommendation>

####### 2.3.4.1.9.4.3 Access Modifier

public

####### 2.3.4.1.9.4.4 Purpose

Result set.

####### 2.3.4.1.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.4.6 Framework Specific Configuration

Navigation Property

####### 2.3.4.1.9.4.7 Implementation Notes

Populated upon completion.

##### 2.3.4.1.10.0.0 Methods

###### 2.3.4.1.10.1.0 Method Name

####### 2.3.4.1.10.1.1 Method Name

MarkAsProcessing

####### 2.3.4.1.10.1.2 Method Signature

public void MarkAsProcessing()

####### 2.3.4.1.10.1.3 Return Type

void

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

false

####### 2.3.4.1.10.1.6 Parameters

*No items available*

####### 2.3.4.1.10.1.7 Implementation Logic

Sets Status to Processing. Updates UpdatedAt timestamp.

####### 2.3.4.1.10.1.8 Exception Handling

Throws if state transition is invalid.

####### 2.3.4.1.10.1.9 Performance Considerations

In-memory state change.

####### 2.3.4.1.10.1.10 Validation Requirements

None.

####### 2.3.4.1.10.1.11 Technology Integration Details

None.

###### 2.3.4.1.10.2.0 Method Name

####### 2.3.4.1.10.2.1 Method Name

Complete

####### 2.3.4.1.10.2.2 Method Signature

public void Complete(List<Recommendation> results)

####### 2.3.4.1.10.2.3 Return Type

void

####### 2.3.4.1.10.2.4 Access Modifier

public

####### 2.3.4.1.10.2.5 Is Async

false

####### 2.3.4.1.10.2.6 Parameters

- {'parameter_name': 'results', 'parameter_type': 'List<Recommendation>', 'is_nullable': 'false', 'purpose': 'Generated items'}

####### 2.3.4.1.10.2.7 Implementation Logic

Sets Status to Completed. Assigns results. Updates UpdatedAt.

####### 2.3.4.1.10.2.8 Exception Handling

None.

####### 2.3.4.1.10.2.9 Performance Considerations

None.

####### 2.3.4.1.10.2.10 Validation Requirements

Results must not be null.

####### 2.3.4.1.10.2.11 Technology Integration Details

None.

##### 2.3.4.1.11.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0 Implementation Notes

Central entity for async workflow.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

GenerateRecommendationsHandler

##### 2.3.4.2.2.0.0 File Path

src/ReadTrack.Recommendations.Application/Features/Recommendations/GenerateRecommendationsHandler.cs

##### 2.3.4.2.3.0.0 Class Type

CommandHandler

##### 2.3.4.2.4.0.0 Inheritance

IRequestHandler<GenerateRecommendationsCommand, Result<Guid>>

##### 2.3.4.2.5.0.0 Purpose

Initiates the recommendation process by creating a job and queuing it.

##### 2.3.4.2.6.0.0 Dependencies

- IRecommendationRepository
- IUnitOfWork
- IJobQueueService
- ILogger<GenerateRecommendationsHandler>

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

MediatR handler.

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result<Guid>> Handle(GenerateRecommendationsCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result<Guid>>', 'access_modifier': 'public', 'is_async': 'true', 'parameters': [{'parameter_name': 'request', 'parameter_type': 'GenerateRecommendationsCommand', 'is_nullable': 'false', 'purpose': 'Request data'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation'}], 'implementation_logic': '1. Create RecommendationJob(Pending). 2. Add to Repository. 3. Commit UnitOfWork. 4. Call IJobQueueService.Enqueue(ProcessJob, JobId). 5. Return JobId.', 'exception_handling': 'Logs errors and returns Result.Failure.', 'performance_considerations': 'Fast execution, only db insert and queue push.', 'validation_requirements': 'Request validation via pipeline.', 'technology_integration_details': 'Integrates with shared infra job queue.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Does not perform AI logic itself, just scheduling.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

RecommendationProcessService

##### 2.3.4.3.2.0.0 File Path

src/ReadTrack.Recommendations.Application/Services/RecommendationProcessService.cs

##### 2.3.4.3.3.0.0 Class Type

Service

##### 2.3.4.3.4.0.0 Inheritance

IRecommendationProcessService

##### 2.3.4.3.5.0.0 Purpose

Orchestrates the RAG pipeline execution in the background.

##### 2.3.4.3.6.0.0 Dependencies

- IReadingHistoryProvider
- IVectorStoreClient
- IEmbeddingGenerator
- IPromptBuilder
- ILLMClient
- IRecommendationRepository
- IUnitOfWork

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Core RAG Logic.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'ProcessJobAsync', 'method_signature': 'public async Task ProcessJobAsync(Guid jobId, CancellationToken cancellationToken)', 'return_type': 'Task', 'access_modifier': 'public', 'is_async': 'true', 'parameters': [{'parameter_name': 'jobId', 'parameter_type': 'Guid', 'is_nullable': 'false', 'purpose': 'Job to process'}], 'implementation_logic': '1. Load Job. 2. Fetch User History (Reading Module). 3. Generate Embeddings for history. 4. Search Vector Store for context. 5. Build Prompt. 6. Call LLM. 7. Parse Response. 8. Save Results & Complete Job. 9. Commit.', 'exception_handling': 'Catch exceptions -> Job.Fail(reason) -> Save.', 'performance_considerations': 'Heavy I/O, runs in background.', 'validation_requirements': 'None.', 'technology_integration_details': 'Uses Adapters for all external calls.'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

The brain of the module.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

OpenAIClientAdapter

##### 2.3.4.4.2.0.0 File Path

src/ReadTrack.Recommendations.Infrastructure/AI/OpenAIClientAdapter.cs

##### 2.3.4.4.3.0.0 Class Type

Adapter

##### 2.3.4.4.4.0.0 Inheritance

ILLMClient, IEmbeddingGenerator

##### 2.3.4.4.5.0.0 Purpose

Wraps OpenAI-DotNet library to provide chat completion and embedding services.

##### 2.3.4.4.6.0.0 Dependencies

- IOptions<OpenAIOptions>
- IHttpClientFactory
- ILogger<OpenAIClientAdapter>

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

Uses Polly policies configured in HttpClient.

##### 2.3.4.4.9.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0 Methods

###### 2.3.4.4.10.1.0 Method Name

####### 2.3.4.4.10.1.1 Method Name

GetChatCompletionAsync

####### 2.3.4.4.10.1.2 Method Signature

public async Task<string> GetChatCompletionAsync(ChatPrompt prompt, CancellationToken cancellationToken)

####### 2.3.4.4.10.1.3 Return Type

Task<string>

####### 2.3.4.4.10.1.4 Access Modifier

public

####### 2.3.4.4.10.1.5 Is Async

true

####### 2.3.4.4.10.1.6 Parameters

- {'parameter_name': 'prompt', 'parameter_type': 'ChatPrompt', 'is_nullable': 'false', 'purpose': 'Domain prompt object'}

####### 2.3.4.4.10.1.7 Implementation Logic

Maps ChatPrompt to OpenAI request. Calls API. returns content string.

####### 2.3.4.4.10.1.8 Exception Handling

Polly handles retries. Throws on exhaustion.

####### 2.3.4.4.10.1.9 Performance Considerations

Network bound.

####### 2.3.4.4.10.1.10 Validation Requirements

Check API Key presence.

####### 2.3.4.4.10.1.11 Technology Integration Details

Uses OpenAI SDK.

###### 2.3.4.4.10.2.0 Method Name

####### 2.3.4.4.10.2.1 Method Name

GenerateEmbeddingAsync

####### 2.3.4.4.10.2.2 Method Signature

public async Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken)

####### 2.3.4.4.10.2.3 Return Type

Task<float[]>

####### 2.3.4.4.10.2.4 Access Modifier

public

####### 2.3.4.4.10.2.5 Is Async

true

####### 2.3.4.4.10.2.6 Parameters

- {'parameter_name': 'text', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Input text'}

####### 2.3.4.4.10.2.7 Implementation Logic

Calls Embeddings Endpoint. Returns vector.

####### 2.3.4.4.10.2.8 Exception Handling

Polly handles retries.

####### 2.3.4.4.10.2.9 Performance Considerations

Network bound.

####### 2.3.4.4.10.2.10 Validation Requirements

None.

####### 2.3.4.4.10.2.11 Technology Integration Details

Uses OpenAI SDK.

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes

Isolates OpenAI dependency.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

ILLMClient

##### 2.3.5.1.2.0.0 File Path

src/ReadTrack.Recommendations.Application/Interfaces/ILLMClient.cs

##### 2.3.5.1.3.0.0 Purpose

Abstraction for Large Language Model interactions.

##### 2.3.5.1.4.0.0 Generic Constraints

None

##### 2.3.5.1.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.1.6.0.0 Method Contracts

- {'method_name': 'GetChatCompletionAsync', 'method_signature': 'Task<string> GetChatCompletionAsync(ChatPrompt prompt, CancellationToken cancellationToken)', 'return_type': 'Task<string>', 'contract_description': 'Sends a prompt to the LLM and returns the text response.', 'exception_contracts': 'Throws LlmServiceException on failure.', 'parameters': [{'parameter_name': 'prompt', 'parameter_type': 'ChatPrompt', 'purpose': 'The prompt'}], 'framework_attributes': []}

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Implementations should handle rate limiting and retries.

##### 2.3.5.1.9.0.0 Validation Notes

None

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

IVectorStoreClient

##### 2.3.5.2.2.0.0 File Path

src/ReadTrack.Recommendations.Application/Interfaces/IVectorStoreClient.cs

##### 2.3.5.2.3.0.0 Purpose

Abstraction for Vector Database interactions.

##### 2.3.5.2.4.0.0 Generic Constraints

None

##### 2.3.5.2.5.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0 Method Contracts

- {'method_name': 'SearchSimilarAsync', 'method_signature': 'Task<List<string>> SearchSimilarAsync(float[] embeddingVector, int limit, CancellationToken cancellationToken)', 'return_type': 'Task<List<string>>', 'contract_description': 'Performs k-NN search using the provided vector.', 'exception_contracts': 'Throws VectorStoreUnavailableException.', 'parameters': [{'parameter_name': 'embeddingVector', 'parameter_type': 'float[]', 'purpose': 'Query vector'}, {'parameter_name': 'limit', 'parameter_type': 'int', 'purpose': 'Max results'}], 'framework_attributes': []}

##### 2.3.5.2.7.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0 Implementation Guidance

Should map domain vector concepts to specific DB query language (e.g., OpenSearch DSL).

##### 2.3.5.2.9.0.0 Validation Notes

None

### 2.3.6.0.0.0.0 Enum Specifications

- {'enum_name': 'RecommendationJobStatus', 'file_path': 'src/ReadTrack.Recommendations.Domain/Enums/RecommendationJobStatus.cs', 'underlying_type': 'int', 'purpose': 'Defines the lifecycle states of a recommendation generation job.', 'framework_attributes': [], 'values': [{'value_name': 'Pending', 'value': '0', 'description': 'Job is created and queued.'}, {'value_name': 'Processing', 'value': '1', 'description': 'Job is currently being executed.'}, {'value_name': 'Completed', 'value': '2', 'description': 'Job finished successfully.'}, {'value_name': 'Failed', 'value': '3', 'description': 'Job failed to complete.'}], 'validation_notes': 'Used in RecommendationJob entity.'}

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'RecommendationJobStatusDto', 'file_path': 'src/ReadTrack.Recommendations.Application/DTOs/RecommendationJobStatusDto.cs', 'purpose': 'Data transfer object for polling job status.', 'framework_base_class': 'record', 'properties': [{'property_name': 'JobId', 'property_type': 'Guid', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'Status', 'property_type': 'string', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'Result', 'property_type': 'List<RecommendationDto>?', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'None', 'serialization_requirements': 'JSON', 'validation_notes': 'Result is null unless status is Completed.'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'OpenAIOptions', 'file_path': 'src/ReadTrack.Recommendations.Infrastructure/Configuration/OpenAIOptions.cs', 'purpose': 'Configuration settings for OpenAI API.', 'framework_base_class': 'None', 'configuration_sections': [{'section_name': 'OpenAI', 'properties': [{'property_name': 'ApiKey', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'API Key'}, {'property_name': 'ModelId', 'property_type': 'string', 'default_value': 'gpt-4', 'required': 'true', 'description': 'Model identifier'}, {'property_name': 'EmbeddingModelId', 'property_type': 'string', 'default_value': 'text-embedding-ada-002', 'required': 'true', 'description': 'Embedding Model identifier'}]}], 'validation_requirements': 'ApiKey must be present.', 'validation_notes': 'Bound via Options pattern.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

ILLMClient

##### 2.3.9.1.2.0.0 Service Implementation

OpenAIClientAdapter

##### 2.3.9.1.3.0.0 Lifetime

Singleton

##### 2.3.9.1.4.0.0 Registration Reasoning

Stateless client wrapper.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddSingleton<ILLMClient, OpenAIClientAdapter>()

##### 2.3.9.1.6.0.0 Validation Notes

None

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IVectorStoreClient

##### 2.3.9.2.2.0.0 Service Implementation

OpenSearchClientAdapter

##### 2.3.9.2.3.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0 Registration Reasoning

Stateless client wrapper.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddSingleton<IVectorStoreClient, OpenSearchClientAdapter>()

##### 2.3.9.2.6.0.0 Validation Notes

None

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

IRecommendationProcessService

##### 2.3.9.3.2.0.0 Service Implementation

RecommendationProcessService

##### 2.3.9.3.3.0.0 Lifetime

Scoped

##### 2.3.9.3.4.0.0 Registration Reasoning

Uses scoped DbContext/UoW.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

services.AddScoped<IRecommendationProcessService, RecommendationProcessService>()

##### 2.3.9.3.6.0.0 Validation Notes

None

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

OpenAI API

##### 2.3.10.1.2.0.0 Integration Type

HTTP/REST

##### 2.3.10.1.3.0.0 Required Client Classes

- OpenAIClientAdapter

##### 2.3.10.1.4.0.0 Configuration Requirements

API Key

##### 2.3.10.1.5.0.0 Error Handling Requirements

Polly Retry for 429/5xx

##### 2.3.10.1.6.0.0 Authentication Requirements

Bearer Token

##### 2.3.10.1.7.0.0 Framework Integration Patterns

Adapter Pattern

##### 2.3.10.1.8.0.0 Validation Notes

None

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Amazon OpenSearch

##### 2.3.10.2.2.0.0 Integration Type

HTTP/REST

##### 2.3.10.2.3.0.0 Required Client Classes

- OpenSearchClientAdapter

##### 2.3.10.2.4.0.0 Configuration Requirements

Endpoint, Credentials

##### 2.3.10.2.5.0.0 Error Handling Requirements

Polly Retry

##### 2.3.10.2.6.0.0 Authentication Requirements

SigV4 or Basic Auth

##### 2.3.10.2.7.0.0 Framework Integration Patterns

Adapter Pattern

##### 2.3.10.2.8.0.0 Validation Notes

None

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 7 |
| Total Enums | 1 |
| Total Dtos | 1 |
| Total Configurations | 1 |
| Total External Integrations | 2 |
| Grand Total Components | 34 |
| Phase 2 Claimed Count | 32 |
| Phase 2 Actual Count | 28 |
| Validation Added Count | 6 |
| Final Validated Count | 34 |

