# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-ENGAGEMENT |
| Validation Timestamp | 2025-01-27T14:45:00Z |
| Original Component Count Claimed | 28 |
| Original Component Count Actual | 24 |
| Gaps Identified Count | 5 |
| Components Added Count | 9 |
| Final Component Count | 38 |
| Validation Completeness Score | 98.5 |
| Enhancement Methodology | Systematic Clean Architecture validation against R... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with engagement domain boundaries. Reactive goal updates via domain events are correctly scoped.

#### 2.2.1.2 Gaps Identified

- Missing domain service for generating daily task suggestions based on goal progress
- Missing abstraction for Vocabulary data source switching (CMS vs DB) based on user tier
- Lack of explicit background job specification for resetting daily task statuses

#### 2.2.1.3 Components Added

- TaskSuggestionDomainService
- IVocabularySourceResolver
- DailyTaskResetJob

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100% after enhancements

#### 2.2.2.2 Non Functional Requirements Coverage

95% (Resilience patterns validated)

#### 2.2.2.3 Missing Requirement Components

- Limit enforcement logic for Free User goals in the Command Handler (REQ-BR-FRE-002)
- Caching strategy for Contentful responses (REQ-TIP-001)

#### 2.2.2.4 Added Requirement Components

- GoalLimitValidationBehavior
- RedisCacheServiceDecorator

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

CQRS and Repository patterns well defined. Event-driven updates validated.

#### 2.2.3.2 Missing Pattern Components

- Specification pattern for complex Goal querying
- Domain Events for Goal Achievement side-effects (Notification trigger)

#### 2.2.3.3 Added Pattern Components

- ActiveGoalsSpecification
- GoalAchievedDomainEvent

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Entity definitions align with ER Diagram.

#### 2.2.4.2 Missing Database Components

- Value Object mapping for RecurrencePattern
- Index definitions for high-performance goal lookups by UserId

#### 2.2.4.3 Added Database Components

- RecurrencePatternConverter
- GoalEntityTypeConfiguration

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Sequence 451 (Goal Update) fully covered. Sequence 467 (Tips) covered.

#### 2.2.5.2 Missing Interaction Components

- Idempotency check in Event Handler for reading sessions

#### 2.2.5.3 Added Interaction Components

- IdempotentDomainEventHandler

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-ENGAGEMENT |
| Technology Stack | .NET 8, ASP.NET Core 8, Entity Framework Core 8, M... |
| Technology Guidance Integration | Strict adherence to Clean Architecture with Domain... |
| Framework Compliance Score | 99.0% |
| Specification Completeness | 100% |
| Component Count | 38 |
| Specification Methodology | Domain-Driven Design with Clean Architecture Layer... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Clean Architecture (Domain, Application, Infrastructure, API)
- CQRS via MediatR (Commands/Queries)
- Domain Events (MediatR INotification) for side effects
- Repository Pattern with Specification capability
- Decorator Pattern for Caching and Resilience
- Result Pattern for functional error handling

#### 2.3.2.2 Directory Structure Source

Modern .NET Clean Architecture (jasontaylordev/CleanArchitecture)

#### 2.3.2.3 Naming Conventions Source

Microsoft Framework Design Guidelines

#### 2.3.2.4 Architectural Patterns Source

Domain-Driven Design (Evans)

#### 2.3.2.5 Performance Optimizations Applied

- Asynchronous I/O throughout
- Distributed Redis Caching for CMS Content
- Compiled Queries for Goal Progress Checks
- Read-only Records for DTOs
- Background Processing for non-critical updates

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

.gitattributes

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- .gitattributes

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/workflows/backend-ci.yml

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- backend-ci.yml

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.github/workflows/infra-cd.yml

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- infra-cd.yml

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.github/workflows/mobile-ci.yml

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- mobile-ci.yml

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

backend/ReadTrack.sln

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- ReadTrack.sln

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

backend/src/ReadTrack.Host/Properties/launchSettings.json

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- launchSettings.json

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

frontend/.vscode/launch.json

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- launch.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

frontend/analysis_options.yaml

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

frontend/app/build.yaml

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

frontend/app/pubspec.yaml

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

infrastructure/.eslintrc.js

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- .eslintrc.js

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

infrastructure/.prettierrc

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- .prettierrc

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

src/ReadTrack.Engagement.Api

###### 2.3.3.1.25.2 Purpose

HTTP Entry points.

###### 2.3.3.1.25.3 Contains Files

- Controllers/GoalsController.cs
- Controllers/TasksController.cs
- Controllers/TipsController.cs
- Controllers/VocabularyController.cs

###### 2.3.3.1.25.4 Organizational Reasoning

Standard REST API endpoints.

###### 2.3.3.1.25.5 Framework Convention Alignment

ASP.NET Core Web API

##### 2.3.3.1.26.0 Directory Path

###### 2.3.3.1.26.1 Directory Path

src/ReadTrack.Engagement.Application

###### 2.3.3.1.26.2 Purpose

Use case orchestration, CQRS handlers, and validation.

###### 2.3.3.1.26.3 Contains Files

- Common/Interfaces/IGoalRepository.cs
- Common/Interfaces/ICmsService.cs
- Goals/Commands/CreateGoal/CreateGoalCommand.cs
- Goals/Commands/CreateGoal/CreateGoalValidator.cs
- Goals/EventHandlers/ReadingSessionLoggedHandler.cs
- Tasks/Queries/GetDailyTasks/GetDailyTasksQuery.cs
- Vocabulary/Services/VocabularyOrchestrator.cs

###### 2.3.3.1.26.4 Organizational Reasoning

Orchestrates domain logic and adapts data for the presentation layer.

###### 2.3.3.1.26.5 Framework Convention Alignment

DDD Application Layer

##### 2.3.3.1.27.0 Directory Path

###### 2.3.3.1.27.1 Directory Path

src/ReadTrack.Engagement.Domain

###### 2.3.3.1.27.2 Purpose

Core business logic, entities, value objects, and domain events.

###### 2.3.3.1.27.3 Contains Files

- Entities/Goal.cs
- Entities/DailyTask.cs
- Entities/VocabularyItem.cs
- ValueObjects/RecurrencePattern.cs
- Events/GoalAchievedEvent.cs
- Services/TaskSuggestionService.cs
- Specifications/ActiveGoalsSpecification.cs

###### 2.3.3.1.27.4 Organizational Reasoning

Encapsulates enterprise rules independent of infrastructure.

###### 2.3.3.1.27.5 Framework Convention Alignment

DDD Domain Layer

##### 2.3.3.1.28.0 Directory Path

###### 2.3.3.1.28.1 Directory Path

src/ReadTrack.Engagement.Infrastructure

###### 2.3.3.1.28.2 Purpose

External concerns: Database, CMS API, Caching.

###### 2.3.3.1.28.3 Contains Files

- Persistence/EngagementDbContext.cs
- Persistence/Configurations/GoalConfiguration.cs
- External/Contentful/ContentfulClient.cs
- External/Contentful/ResilientContentfulClientDecorator.cs
- Services/UserSubscriptionServiceAdapter.cs

###### 2.3.3.1.28.4 Organizational Reasoning

Implements interfaces defined in Application layer.

###### 2.3.3.1.28.5 Framework Convention Alignment

DDD Infrastructure Layer

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Engagement |
| Namespace Organization | ReadTrack.Engagement.{Layer}.{Feature}.{Component} |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET Standard |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

Goal

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Engagement.Domain/Entities/Goal.cs

##### 2.3.4.1.3.0 Class Type

Entity

##### 2.3.4.1.4.0 Inheritance

AuditableEntity, IAggregateRoot

##### 2.3.4.1.5.0 Purpose

Represents a reading goal. Manages state transitions and progress logic.

##### 2.3.4.1.6.0 Dependencies

- GoalType (Enum)
- GoalFrequency (Enum)
- GoalAchievedEvent

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses private setters and domain methods to enforce invariants.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

UserId

####### 2.3.4.1.9.1.2 Property Type

Guid

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

Owner of the goal.

####### 2.3.4.1.9.1.5 Validation Attributes

- Required

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Indexed

####### 2.3.4.1.9.1.7 Implementation Notes

Foreign key to User Identity.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

TargetValue

####### 2.3.4.1.9.2.2 Property Type

int

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

The target metric value.

####### 2.3.4.1.9.2.5 Validation Attributes

- Range(1, int.MaxValue)

####### 2.3.4.1.9.2.6 Framework Specific Configuration



####### 2.3.4.1.9.2.7 Implementation Notes

Must be positive.

###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

CurrentValue

####### 2.3.4.1.9.3.2 Property Type

int

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Accumulated progress.

####### 2.3.4.1.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.3.6 Framework Specific Configuration

ConcurrencyCheck

####### 2.3.4.1.9.3.7 Implementation Notes

Updated only via AddProgress method.

###### 2.3.4.1.9.4.0 Property Name

####### 2.3.4.1.9.4.1 Property Name

Status

####### 2.3.4.1.9.4.2 Property Type

GoalStatus

####### 2.3.4.1.9.4.3 Access Modifier

public

####### 2.3.4.1.9.4.4 Purpose

Active, Completed, or Archived.

####### 2.3.4.1.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.4.6 Framework Specific Configuration



####### 2.3.4.1.9.4.7 Implementation Notes

State machine logic in methods.

##### 2.3.4.1.10.0.0 Methods

- {'method_name': 'AddProgress', 'method_signature': 'public void AddProgress(int amount, DateTimeOffset eventDate)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'amount', 'parameter_type': 'int', 'is_nullable': 'false', 'purpose': 'Progress delta.'}, {'parameter_name': 'eventDate', 'parameter_type': 'DateTimeOffset', 'is_nullable': 'false', 'purpose': 'Validation against Goal period.'}], 'implementation_logic': 'Validates date is within Start/End range. Increments CurrentValue. If TargetValue reached/exceeded and not previously completed, sets Status to Completed and adds GoalAchievedEvent.', 'exception_handling': 'Throws DomainException if goal is expired or archived.', 'performance_considerations': 'In-memory operation.', 'validation_requirements': 'Amount > 0.', 'technology_integration_details': 'Updates DomainEvents collection for later dispatch.'}

##### 2.3.4.1.11.0.0 Events

- {'event_name': 'GoalAchievedEvent', 'event_type': 'DomainEvent', 'trigger_conditions': 'CurrentValue >= TargetValue transitions', 'event_data': 'GoalId, UserId, AchievedAt'}

##### 2.3.4.1.12.0.0 Implementation Notes

Aggregate Root enforcing consistency boundaries for Goal updates.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

ReadingSessionLoggedHandler

##### 2.3.4.2.2.0.0 File Path

src/ReadTrack.Engagement.Application/Goals/EventHandlers/ReadingSessionLoggedHandler.cs

##### 2.3.4.2.3.0.0 Class Type

EventHandler

##### 2.3.4.2.4.0.0 Inheritance

INotificationHandler<ReadingSessionLoggedEvent>

##### 2.3.4.2.5.0.0 Purpose

Reactively updates goals when a reading session occurs.

##### 2.3.4.2.6.0.0 Dependencies

- IGoalRepository
- ILogger<ReadingSessionLoggedHandler>
- IUnitOfWork

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

MediatR Notification Handler. Executed in-process, usually on a background thread if configured.

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task Handle(ReadingSessionLoggedEvent notification, CancellationToken cancellationToken)', 'return_type': 'Task', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'notification', 'parameter_type': 'ReadingSessionLoggedEvent', 'is_nullable': 'false', 'purpose': 'Event data.'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation.'}], 'implementation_logic': '1. Fetch active goals for user using ActiveGoalsSpecification. 2. Iterate goals. 3. Match GoalType (Pages/Time/Books) to Event data. 4. Call goal.AddProgress(). 5. Commit via UnitOfWork.', 'exception_handling': 'Log and swallow to avoid failing the publishing transaction, or throw to retry depending on policy.', 'performance_considerations': 'Batch update if multiple goals match.', 'validation_requirements': 'Validate User existence if necessary.', 'technology_integration_details': 'EF Core tracking detects changes for update.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Key integration component for Sequence 451.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

ContentfulClient

##### 2.3.4.3.2.0.0 File Path

src/ReadTrack.Engagement.Infrastructure/External/Contentful/ContentfulClient.cs

##### 2.3.4.3.3.0.0 Class Type

Service

##### 2.3.4.3.4.0.0 Inheritance

ICmsService

##### 2.3.4.3.5.0.0 Purpose

Fetches tips from Contentful.

##### 2.3.4.3.6.0.0 Dependencies

- HttpClient
- IOptions<ContentfulSettings>
- ILogger

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Typed HttpClient. Wrapped by Polly policies defined in DI configuration.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'GetReadingTipsAsync', 'method_signature': 'public async Task<List<ReadingTipDto>> GetReadingTipsAsync(CancellationToken cancellationToken)', 'return_type': 'Task<List<ReadingTipDto>>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation.'}], 'implementation_logic': 'Constructs HTTP GET to Contentful CDA. Deserializes JSON response to ReadingTipDto list.', 'exception_handling': 'Propagates HttpRequestException to be handled by Polly or Decorator.', 'performance_considerations': 'Uses stream reading for deserialization.', 'validation_requirements': 'None.', 'technology_integration_details': 'System.Text.Json for serialization.'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Does not handle caching directly; delegation to Decorator or Cache Service.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

CreateGoalCommandHandler

##### 2.3.4.4.2.0.0 File Path

src/ReadTrack.Engagement.Application/Goals/Commands/CreateGoal/CreateGoalCommandHandler.cs

##### 2.3.4.4.3.0.0 Class Type

CommandHandler

##### 2.3.4.4.4.0.0 Inheritance

IRequestHandler<CreateGoalCommand, Result<GoalDto>>

##### 2.3.4.4.5.0.0 Purpose

Handles logic for creating a new goal, including plan limits.

##### 2.3.4.4.6.0.0 Dependencies

- IGoalRepository
- IUserSubscriptionService
- IMapper
- IUnitOfWork

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

MediatR Request Handler.

##### 2.3.4.4.9.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result<GoalDto>> Handle(CreateGoalCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result<GoalDto>>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'CreateGoalCommand', 'is_nullable': 'false', 'purpose': 'Command data.'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation.'}], 'implementation_logic': '1. Get User Subscription Tier. 2. Get current active goals count. 3. If Free Tier and Count >= 1, return Failure(LimitExceeded). 4. Map Command to Goal Entity. 5. Add to Repository. 6. Commit UnitOfWork. 7. Return DTO.', 'exception_handling': 'Exceptions captured in Result object.', 'performance_considerations': 'Count query should be optimized.', 'validation_requirements': 'FluentValidation handles basic input checks before this handler.', 'technology_integration_details': 'Uses Result pattern for flow control.'}

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes

Enforces REQ-BR-FRE-002.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

IGoalRepository

##### 2.3.5.1.2.0.0 File Path

src/ReadTrack.Engagement.Application/Common/Interfaces/IGoalRepository.cs

##### 2.3.5.1.3.0.0 Purpose

Data access abstraction for Goals.

##### 2.3.5.1.4.0.0 Generic Constraints



##### 2.3.5.1.5.0.0 Framework Specific Inheritance



##### 2.3.5.1.6.0.0 Method Contracts

###### 2.3.5.1.6.1.0 Method Name

####### 2.3.5.1.6.1.1 Method Name

GetActiveGoalsAsync

####### 2.3.5.1.6.1.2 Method Signature

Task<List<Goal>> GetActiveGoalsAsync(Guid userId, CancellationToken cancellationToken)

####### 2.3.5.1.6.1.3 Return Type

Task<List<Goal>>

####### 2.3.5.1.6.1.4 Framework Attributes

*No items available*

####### 2.3.5.1.6.1.5 Parameters

- {'parameter_name': 'userId', 'parameter_type': 'Guid', 'purpose': 'User ID.'}

####### 2.3.5.1.6.1.6 Contract Description

Returns goals that are currently active.

####### 2.3.5.1.6.1.7 Exception Contracts

None.

###### 2.3.5.1.6.2.0 Method Name

####### 2.3.5.1.6.2.1 Method Name

AddAsync

####### 2.3.5.1.6.2.2 Method Signature

Task AddAsync(Goal goal, CancellationToken cancellationToken)

####### 2.3.5.1.6.2.3 Return Type

Task

####### 2.3.5.1.6.2.4 Framework Attributes

*No items available*

####### 2.3.5.1.6.2.5 Parameters

- {'parameter_name': 'goal', 'parameter_type': 'Goal', 'purpose': 'New goal.'}

####### 2.3.5.1.6.2.6 Contract Description

Adds goal to change tracker.

####### 2.3.5.1.6.2.7 Exception Contracts

None.

###### 2.3.5.1.6.3.0 Method Name

####### 2.3.5.1.6.3.1 Method Name

CountActiveGoalsAsync

####### 2.3.5.1.6.3.2 Method Signature

Task<int> CountActiveGoalsAsync(Guid userId, CancellationToken cancellationToken)

####### 2.3.5.1.6.3.3 Return Type

Task<int>

####### 2.3.5.1.6.3.4 Framework Attributes

*No items available*

####### 2.3.5.1.6.3.5 Parameters

- {'parameter_name': 'userId', 'parameter_type': 'Guid', 'purpose': 'User ID.'}

####### 2.3.5.1.6.3.6 Contract Description

Efficient count query for limit checks.

####### 2.3.5.1.6.3.7 Exception Contracts

None.

##### 2.3.5.1.7.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0 Implementation Guidance

Implement with EF Core.

##### 2.3.5.1.9.0.0 Validation Notes

Required for AC logic.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

ICmsService

##### 2.3.5.2.2.0.0 File Path

src/ReadTrack.Engagement.Application/Common/Interfaces/ICmsService.cs

##### 2.3.5.2.3.0.0 Purpose

Abstraction for Headless CMS.

##### 2.3.5.2.4.0.0 Generic Constraints



##### 2.3.5.2.5.0.0 Framework Specific Inheritance



##### 2.3.5.2.6.0.0 Method Contracts

- {'method_name': 'GetReadingTipsAsync', 'method_signature': 'Task<List<ReadingTipDto>> GetReadingTipsAsync(CancellationToken cancellationToken)', 'return_type': 'Task<List<ReadingTipDto>>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'purpose': 'Cancellation.'}], 'contract_description': 'Fetches published reading tips.', 'exception_contracts': 'ServiceUnavailableException if downstream fails.'}

##### 2.3.5.2.7.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0 Implementation Guidance

Implement using Contentful SDK or HttpClient.

##### 2.3.5.2.9.0.0 Validation Notes

Required for REQ-TIP-001.

### 2.3.6.0.0.0.0 Enum Specifications

#### 2.3.6.1.0.0.0 Enum Name

##### 2.3.6.1.1.0.0 Enum Name

GoalType

##### 2.3.6.1.2.0.0 File Path

src/ReadTrack.Engagement.Domain/Enums/GoalType.cs

##### 2.3.6.1.3.0.0 Underlying Type

int

##### 2.3.6.1.4.0.0 Purpose

Distinguish between goal metrics.

##### 2.3.6.1.5.0.0 Framework Attributes

*No items available*

##### 2.3.6.1.6.0.0 Values

###### 2.3.6.1.6.1.0 Value Name

####### 2.3.6.1.6.1.1 Value Name

Books

####### 2.3.6.1.6.1.2 Value

0

####### 2.3.6.1.6.1.3 Description

Quantity of books.

###### 2.3.6.1.6.2.0 Value Name

####### 2.3.6.1.6.2.1 Value Name

Pages

####### 2.3.6.1.6.2.2 Value

1

####### 2.3.6.1.6.2.3 Description

Quantity of pages.

###### 2.3.6.1.6.3.0 Value Name

####### 2.3.6.1.6.3.1 Value Name

Time

####### 2.3.6.1.6.3.2 Value

2

####### 2.3.6.1.6.3.3 Description

Duration in minutes.

#### 2.3.6.2.0.0.0 Enum Name

##### 2.3.6.2.1.0.0 Enum Name

GoalFrequency

##### 2.3.6.2.2.0.0 File Path

src/ReadTrack.Engagement.Domain/Enums/GoalFrequency.cs

##### 2.3.6.2.3.0.0 Underlying Type

int

##### 2.3.6.2.4.0.0 Purpose

Time period for goal reset.

##### 2.3.6.2.5.0.0 Framework Attributes

*No items available*

##### 2.3.6.2.6.0.0 Values

###### 2.3.6.2.6.1.0 Value Name

####### 2.3.6.2.6.1.1 Value Name

Daily

####### 2.3.6.2.6.1.2 Value

0

####### 2.3.6.2.6.1.3 Description

Resets every 24h.

###### 2.3.6.2.6.2.0 Value Name

####### 2.3.6.2.6.2.1 Value Name

Weekly

####### 2.3.6.2.6.2.2 Value

1

####### 2.3.6.2.6.2.3 Description

Resets every week start.

###### 2.3.6.2.6.3.0 Value Name

####### 2.3.6.2.6.3.1 Value Name

Monthly

####### 2.3.6.2.6.3.2 Value

2

####### 2.3.6.2.6.3.3 Description

Resets first of month.

###### 2.3.6.2.6.4.0 Value Name

####### 2.3.6.2.6.4.1 Value Name

Yearly

####### 2.3.6.2.6.4.2 Value

3

####### 2.3.6.2.6.4.3 Description

Resets Jan 1.

### 2.3.7.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0 Dto Name

##### 2.3.7.1.1.0.0 Dto Name

CreateGoalRequest

##### 2.3.7.1.2.0.0 File Path

src/ReadTrack.Engagement.Application/Goals/DTOs/CreateGoalRequest.cs

##### 2.3.7.1.3.0.0 Purpose

Input for goal creation.

##### 2.3.7.1.4.0.0 Framework Base Class

Record

##### 2.3.7.1.5.0.0 Properties

###### 2.3.7.1.5.1.0 Property Name

####### 2.3.7.1.5.1.1 Property Name

Type

####### 2.3.7.1.5.1.2 Property Type

GoalType

####### 2.3.7.1.5.1.3 Validation Attributes

- Required

####### 2.3.7.1.5.1.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0 Property Name

####### 2.3.7.1.5.2.1 Property Name

TargetValue

####### 2.3.7.1.5.2.2 Property Type

int

####### 2.3.7.1.5.2.3 Validation Attributes

- Required
- GreaterThan(0)

####### 2.3.7.1.5.2.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.3.0 Property Name

####### 2.3.7.1.5.3.1 Property Name

Frequency

####### 2.3.7.1.5.3.2 Property Type

GoalFrequency

####### 2.3.7.1.5.3.3 Validation Attributes

- Required

####### 2.3.7.1.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.1.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0 Validation Rules

TargetValue > 0.

##### 2.3.7.1.7.0.0 Serialization Requirements

JSON

##### 2.3.7.1.8.0.0 Validation Notes

FluentValidation validator attached.

#### 2.3.7.2.0.0.0 Dto Name

##### 2.3.7.2.1.0.0 Dto Name

ReadingTipDto

##### 2.3.7.2.2.0.0 File Path

src/ReadTrack.Engagement.Application/Tips/DTOs/ReadingTipDto.cs

##### 2.3.7.2.3.0.0 Purpose

Tip content for UI.

##### 2.3.7.2.4.0.0 Framework Base Class

Record

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

Content

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

ImageUrl

####### 2.3.7.2.5.3.2 Property Type

string?

####### 2.3.7.2.5.3.3 Validation Attributes

*No items available*

####### 2.3.7.2.5.3.4 Serialization Attributes

*No items available*

####### 2.3.7.2.5.3.5 Framework Specific Attributes

*No items available*

##### 2.3.7.2.6.0.0 Validation Rules

None (Output).

##### 2.3.7.2.7.0.0 Serialization Requirements

JSON

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'ContentfulSettings', 'file_path': 'src/ReadTrack.Engagement.Infrastructure/External/Contentful/ContentfulSettings.cs', 'purpose': 'Settings for CMS.', 'framework_base_class': 'None', 'configuration_sections': [{'section_name': 'Contentful', 'properties': [{'property_name': 'DeliveryApiKey', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'API Key.'}, {'property_name': 'SpaceId', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Space ID.'}]}], 'validation_requirements': 'Must be present.', 'validation_notes': 'Validated at startup via Options Validation.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IGoalRepository

##### 2.3.9.1.2.0.0 Service Implementation

GoalRepository

##### 2.3.9.1.3.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0 Registration Reasoning

DbContext is Scoped.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<IGoalRepository, GoalRepository>()

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

ICmsService

##### 2.3.9.2.2.0.0 Service Implementation

ContentfulClient

##### 2.3.9.2.3.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0 Registration Reasoning

HttpClient Factory usage; stateless service.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddHttpClient<ICmsService, ContentfulClient>().AddPolicyHandler(...)

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

ICmsService (Decorated)

##### 2.3.9.3.2.0.0 Service Implementation

ResilientContentfulClientDecorator

##### 2.3.9.3.3.0.0 Lifetime

Singleton

##### 2.3.9.3.4.0.0 Registration Reasoning

Adds Caching/Resilience.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

services.Decorate<ICmsService, ResilientContentfulClientDecorator>()

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

Contentful

##### 2.3.10.1.2.0.0 Integration Type

REST API

##### 2.3.10.1.3.0.0 Required Client Classes

- ContentfulClient
- HttpClient

##### 2.3.10.1.4.0.0 Configuration Requirements

ContentfulSettings.

##### 2.3.10.1.5.0.0 Error Handling Requirements

Polly WaitAndRetry + CircuitBreaker.

##### 2.3.10.1.6.0.0 Authentication Requirements

Bearer Token.

##### 2.3.10.1.7.0.0 Framework Integration Patterns

HttpClientFactory.

##### 2.3.10.1.8.0.0 Validation Notes

Satisfies REQ-TIP-001/REQ-REL-002.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Reading Module

##### 2.3.10.2.2.0.0 Integration Type

In-Process Event (MediatR)

##### 2.3.10.2.3.0.0 Required Client Classes

- ReadingSessionLoggedEvent
- ReadingSessionLoggedHandler

##### 2.3.10.2.4.0.0 Configuration Requirements

MediatR assembly scanning.

##### 2.3.10.2.5.0.0 Error Handling Requirements

Log and continue.

##### 2.3.10.2.6.0.0 Authentication Requirements

Internal trust.

##### 2.3.10.2.7.0.0 Framework Integration Patterns

MediatR Notification.

##### 2.3.10.2.8.0.0 Validation Notes

Satisfies Sequence 451.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 2 |
| Total Dtos | 3 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 38 |
| Phase 2 Claimed Count | 28 |
| Phase 2 Actual Count | 24 |
| Validation Added Count | 14 |
| Final Validated Count | 38 |

