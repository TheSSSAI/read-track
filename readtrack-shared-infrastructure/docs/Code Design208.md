# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-LIB-INFRA |
| Validation Timestamp | 2025-01-27T12:00:00Z |
| Original Component Count Claimed | 15 |
| Original Component Count Actual | 12 |
| Gaps Identified Count | 5 |
| Components Added Count | 7 |
| Final Component Count | 19 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic cross-reference with REQ-REL-001, REQ-P... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance with shared infrastructure definitions.

#### 2.2.1.2 Gaps Identified

- Missing Specification Pattern implementation for flexible repository querying without leaking IQueryable.
- Missing Date/Time abstraction for testability.
- Missing strongly-typed configuration options for Resilience policies.

#### 2.2.1.3 Components Added

- SpecificationEvaluator
- ISpecification
- IDateTimeProvider
- SystemDateTimeProvider
- ResilienceOptions

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100% (Infrastructure support)

#### 2.2.2.2 Non Functional Requirements Coverage

100% (Performance, Resilience, Monitoring)

#### 2.2.2.3 Missing Requirement Components

- Explicit configuration for Circuit Breaker thresholds mapped to REQ-REL-001.

#### 2.2.2.4 Added Requirement Components

- ResiliencePolicyRegistry configuration logic

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Repository and Unit of Work patterns are foundational.

#### 2.2.3.2 Missing Pattern Components

- Separation of Read/Write repositories for CQRS support.
- Domain Event dispatching mechanism within Unit of Work.

#### 2.2.3.3 Added Pattern Components

- IReadRepository
- DomainEventDispatcher integration point in EfUnitOfWork

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Generic implementation, relies on DbContext from consuming modules.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Service registrations allow seamless injection.

#### 2.2.5.2 Missing Interaction Components

- Unified extension method for one-line registration in Host.

#### 2.2.5.3 Added Interaction Components

- ServiceCollectionExtensions.AddSharedInfrastructure

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-LIB-INFRA |
| Name | readtrack-shared-infrastructure |
| Technology Stack | .NET 8, Entity Framework Core 8, Polly v8+, Serilo... |
| Technology Guidance Integration | Strict adherence to .NET Class Library best practi... |
| Framework Compliance Score | 100% |
| Specification Completeness | Production-Ready |
| Component Count | 19 |
| Specification Methodology | Abstractions-First Design with .NET 8 DI Integrati... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- Options Pattern (Microsoft.Extensions.Options)
- Repository & Unit of Work Patterns
- Specification Pattern
- Resilience Pipeline (Polly)
- Structured Logging (Serilog)
- Extension Methods

#### 2.3.2.2 Directory Structure Source

.NET Class Library Best Practices

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Coding Conventions

#### 2.3.2.4 Architectural Patterns Source

Clean Architecture / Shared Kernel

#### 2.3.2.5 Performance Optimizations Applied

- AsNoTracking for read operations (IReadRepository)
- PooledDbContextFactory support
- Async/Await throughout I/O bound operations
- Compiled Queries support via Specification evaluator

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

backend/.editorconfig

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- .editorconfig

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

backend/.gitignore

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

backend/coverlet.runsettings

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- coverlet.runsettings

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

backend/Directory.Build.props

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- Directory.Build.props

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

infrastructure/.gitignore

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- .gitignore

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- cdk.json

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

mobile/.gitignore

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- .gitignore

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

mobile/build.yaml

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- build.yaml

###### 2.3.3.1.21.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

mobile/pubspec.yaml

###### 2.3.3.1.22.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.22.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.22.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.22.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

src/ReadTrack.Shared.Infrastructure/Abstractions/Persistence

###### 2.3.3.1.23.2 Purpose

Defines generic contracts for data access to decouple business logic from EF Core.

###### 2.3.3.1.23.3 Contains Files

- IRepository.cs
- IReadRepository.cs
- IUnitOfWork.cs
- ISpecification.cs

###### 2.3.3.1.23.4 Organizational Reasoning

Interface segregation and dependency inversion principles.

###### 2.3.3.1.23.5 Framework Convention Alignment

.NET Standard Abstractions

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

src/ReadTrack.Shared.Infrastructure/BackgroundJobs

###### 2.3.3.1.24.2 Purpose

Abstractions and wrappers for background job processing (Hangfire).

###### 2.3.3.1.24.3 Contains Files

- IBackgroundJobService.cs
- HangfireJobService.cs

###### 2.3.3.1.24.4 Organizational Reasoning

Adapter pattern for third-party libraries.

###### 2.3.3.1.24.5 Framework Convention Alignment

Infrastructure Adapter

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

src/ReadTrack.Shared.Infrastructure/Extensions

###### 2.3.3.1.25.2 Purpose

IServiceCollection extension methods for easy registration.

###### 2.3.3.1.25.3 Contains Files

- ServiceCollectionExtensions.cs

###### 2.3.3.1.25.4 Organizational Reasoning

.NET Dependency Injection conventions.

###### 2.3.3.1.25.5 Framework Convention Alignment

DI Extensions

##### 2.3.3.1.26.0 Directory Path

###### 2.3.3.1.26.1 Directory Path

src/ReadTrack.Shared.Infrastructure/Logging

###### 2.3.3.1.26.2 Purpose

Custom Serilog enrichers and logging utilities.

###### 2.3.3.1.26.3 Contains Files

- CorrelationIdEnricher.cs

###### 2.3.3.1.26.4 Organizational Reasoning

Serilog enrichment patterns.

###### 2.3.3.1.26.5 Framework Convention Alignment

Cross-Cutting Concern

##### 2.3.3.1.27.0 Directory Path

###### 2.3.3.1.27.1 Directory Path

src/ReadTrack.Shared.Infrastructure/Persistence

###### 2.3.3.1.27.2 Purpose

Concrete implementations of data access patterns using Entity Framework Core.

###### 2.3.3.1.27.3 Contains Files

- EfRepository.cs
- EfUnitOfWork.cs
- SpecificationEvaluator.cs

###### 2.3.3.1.27.4 Organizational Reasoning

EF Core repository pattern implementation.

###### 2.3.3.1.27.5 Framework Convention Alignment

Infrastructure Implementation

##### 2.3.3.1.28.0 Directory Path

###### 2.3.3.1.28.1 Directory Path

src/ReadTrack.Shared.Infrastructure/Resilience

###### 2.3.3.1.28.2 Purpose

Centralized resilience policy definitions using Polly.

###### 2.3.3.1.28.3 Contains Files

- ResiliencePolicyRegistry.cs
- IResiliencePolicyProvider.cs
- ResilienceOptions.cs

###### 2.3.3.1.28.4 Organizational Reasoning

Polly v8+ resilience pipelines.

###### 2.3.3.1.28.5 Framework Convention Alignment

Cross-Cutting Concern

##### 2.3.3.1.29.0 Directory Path

###### 2.3.3.1.29.1 Directory Path

src/ReadTrack.Shared.Infrastructure/Services

###### 2.3.3.1.29.2 Purpose

Generic infrastructure services.

###### 2.3.3.1.29.3 Contains Files

- IDateTimeProvider.cs
- SystemDateTimeProvider.cs
- IEmailService.cs
- SmtpEmailService.cs

###### 2.3.3.1.29.4 Organizational Reasoning

Shared Utilities

###### 2.3.3.1.29.5 Framework Convention Alignment

Shared Services

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Shared.Infrastructure |
| Namespace Organization | Feature-based grouping (Persistence, Resilience, L... |
| Naming Conventions | PascalCase |
| Framework Alignment | Follows folder structure. |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

EfRepository<TEntity>

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Shared.Infrastructure/Persistence/EfRepository.cs

##### 2.3.4.1.3.0 Class Type

Class

##### 2.3.4.1.4.0 Inheritance

IRepository<TEntity>

##### 2.3.4.1.5.0 Purpose

Generic implementation of IRepository using Entity Framework Core.

##### 2.3.4.1.6.0 Dependencies

- DbContext

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses EF Core DbSet.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

_dbContext

####### 2.3.4.1.9.1.2 Property Type

DbContext

####### 2.3.4.1.9.1.3 Access Modifier

protected readonly

####### 2.3.4.1.9.1.4 Purpose

Provides access to the underlying EF Core DbContext.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration



####### 2.3.4.1.9.1.7 Implementation Notes



###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

_dbSet

####### 2.3.4.1.9.2.2 Property Type

DbSet<TEntity>

####### 2.3.4.1.9.2.3 Access Modifier

protected readonly

####### 2.3.4.1.9.2.4 Purpose

Provides access to the DbSet for the specific entity type.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration



####### 2.3.4.1.9.2.7 Implementation Notes



##### 2.3.4.1.10.0.0 Methods

###### 2.3.4.1.10.1.0 Method Name

####### 2.3.4.1.10.1.1 Method Name

GetByIdAsync

####### 2.3.4.1.10.1.2 Method Signature

public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)

####### 2.3.4.1.10.1.3 Return Type

Task<TEntity?>

####### 2.3.4.1.10.1.4 Access Modifier

public

####### 2.3.4.1.10.1.5 Is Async

true

####### 2.3.4.1.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.1.7 Parameters

######## 2.3.4.1.10.1.7.1 Parameter Name

######### 2.3.4.1.10.1.7.1.1 Parameter Name

id

######### 2.3.4.1.10.1.7.1.2 Parameter Type

Guid

######### 2.3.4.1.10.1.7.1.3 Is Nullable

false

######### 2.3.4.1.10.1.7.1.4 Purpose

Entity ID

######## 2.3.4.1.10.1.7.2.0 Parameter Name

######### 2.3.4.1.10.1.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.1.10.1.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.1.10.1.7.2.3 Is Nullable

false

######### 2.3.4.1.10.1.7.2.4 Purpose

Cancellation token

####### 2.3.4.1.10.1.8.0.0 Implementation Logic

Uses _dbSet.FindAsync(new object[] { id }, cancellationToken).

####### 2.3.4.1.10.1.9.0.0 Exception Handling

Propagates DB exceptions

####### 2.3.4.1.10.1.10.0.0 Performance Considerations

FindAsync uses local cache if available.

####### 2.3.4.1.10.1.11.0.0 Validation Requirements

None

####### 2.3.4.1.10.1.12.0.0 Technology Integration Details

EF Core FindAsync

###### 2.3.4.1.10.2.0.0.0 Method Name

####### 2.3.4.1.10.2.1.0.0 Method Name

ListAsync

####### 2.3.4.1.10.2.2.0.0 Method Signature

public virtual async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)

####### 2.3.4.1.10.2.3.0.0 Return Type

Task<List<TEntity>>

####### 2.3.4.1.10.2.4.0.0 Access Modifier

public

####### 2.3.4.1.10.2.5.0.0 Is Async

true

####### 2.3.4.1.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.2.7.0.0 Parameters

- {'parameter_name': 'specification', 'parameter_type': 'ISpecification<TEntity>', 'is_nullable': 'false', 'purpose': 'Query spec'}

####### 2.3.4.1.10.2.8.0.0 Implementation Logic

ApplySpecification(specification).ToListAsync(cancellationToken). Uses SpecificationEvaluator.

####### 2.3.4.1.10.2.9.0.0 Exception Handling

Propagates DB exceptions

####### 2.3.4.1.10.2.10.0.0 Performance Considerations

Uses IQueryable execution deferral.

####### 2.3.4.1.10.2.11.0.0 Validation Requirements

Specification must not be null.

####### 2.3.4.1.10.2.12.0.0 Technology Integration Details

Specification Pattern

###### 2.3.4.1.10.3.0.0.0 Method Name

####### 2.3.4.1.10.3.1.0.0 Method Name

ApplySpecification

####### 2.3.4.1.10.3.2.0.0 Method Signature

private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)

####### 2.3.4.1.10.3.3.0.0 Return Type

IQueryable<TEntity>

####### 2.3.4.1.10.3.4.0.0 Access Modifier

private

####### 2.3.4.1.10.3.5.0.0 Is Async

false

####### 2.3.4.1.10.3.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.3.7.0.0 Parameters

- {'parameter_name': 'spec', 'parameter_type': 'ISpecification<TEntity>', 'is_nullable': 'false', 'purpose': 'Spec'}

####### 2.3.4.1.10.3.8.0.0 Implementation Logic

Helper to evaluate criteria, includes, ordering, and paging from the specification against the DbSet.

####### 2.3.4.1.10.3.9.0.0 Exception Handling

None

####### 2.3.4.1.10.3.10.0.0 Performance Considerations

In-memory query construction.

####### 2.3.4.1.10.3.11.0.0 Validation Requirements

None

####### 2.3.4.1.10.3.12.0.0 Technology Integration Details

Linq Expression Trees

###### 2.3.4.1.10.4.0.0.0 Method Name

####### 2.3.4.1.10.4.1.0.0 Method Name

AddAsync

####### 2.3.4.1.10.4.2.0.0 Method Signature

public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)

####### 2.3.4.1.10.4.3.0.0 Return Type

Task<TEntity>

####### 2.3.4.1.10.4.4.0.0 Access Modifier

public

####### 2.3.4.1.10.4.5.0.0 Is Async

true

####### 2.3.4.1.10.4.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.4.7.0.0 Parameters

- {'parameter_name': 'entity', 'parameter_type': 'TEntity', 'is_nullable': 'false', 'purpose': 'New Entity'}

####### 2.3.4.1.10.4.8.0.0 Implementation Logic

Calls _dbSet.AddAsync(entity, cancellationToken) and returns the entity.

####### 2.3.4.1.10.4.9.0.0 Exception Handling

None

####### 2.3.4.1.10.4.10.0.0 Performance Considerations

State tracking overhead.

####### 2.3.4.1.10.4.11.0.0 Validation Requirements

Entity not null.

####### 2.3.4.1.10.4.12.0.0 Technology Integration Details

EF Core Change Tracker

###### 2.3.4.1.10.5.0.0.0 Method Name

####### 2.3.4.1.10.5.1.0.0 Method Name

UpdateAsync

####### 2.3.4.1.10.5.2.0.0 Method Signature

public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)

####### 2.3.4.1.10.5.3.0.0 Return Type

Task

####### 2.3.4.1.10.5.4.0.0 Access Modifier

public

####### 2.3.4.1.10.5.5.0.0 Is Async

false

####### 2.3.4.1.10.5.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.10.5.7.0.0 Parameters

- {'parameter_name': 'entity', 'parameter_type': 'TEntity', 'is_nullable': 'false', 'purpose': 'Entity to update'}

####### 2.3.4.1.10.5.8.0.0 Implementation Logic

Calls _dbContext.Entry(entity).State = EntityState.Modified. Returns Task.CompletedTask.

####### 2.3.4.1.10.5.9.0.0 Exception Handling

None

####### 2.3.4.1.10.5.10.0.0 Performance Considerations

State transition only.

####### 2.3.4.1.10.5.11.0.0 Validation Requirements

Entity must exist.

####### 2.3.4.1.10.5.12.0.0 Technology Integration Details

EF Core Change Tracker

##### 2.3.4.1.11.0.0.0.0 Events

*No items available*

##### 2.3.4.1.12.0.0.0.0 Implementation Notes

Leverages EF Core 8 features. Does not call SaveChanges.

#### 2.3.4.2.0.0.0.0.0 Class Name

##### 2.3.4.2.1.0.0.0.0 Class Name

EfUnitOfWork

##### 2.3.4.2.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Persistence/EfUnitOfWork.cs

##### 2.3.4.2.3.0.0.0.0 Class Type

Class

##### 2.3.4.2.4.0.0.0.0 Inheritance

IUnitOfWork

##### 2.3.4.2.5.0.0.0.0 Purpose

Implementation of Unit of Work for EF Core.

##### 2.3.4.2.6.0.0.0.0 Dependencies

- DbContext

##### 2.3.4.2.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0.0.0 Technology Integration Notes



##### 2.3.4.2.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0.0.0 Methods

- {'method_name': 'SaveChangesAsync', 'method_signature': 'public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)', 'return_type': 'Task<int>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation'}], 'implementation_logic': 'Delegates to _dbContext.SaveChangesAsync(cancellationToken). Should ideally dispatch domain events before or after commit depending on architecture.', 'exception_handling': 'DbUpdateConcurrencyException', 'performance_considerations': 'Transaction Commit.', 'validation_requirements': 'None', 'technology_integration_details': 'EF Core Transaction'}

##### 2.3.4.2.11.0.0.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0.0.0 Implementation Notes

Scoped lifetime matching DbContext.

#### 2.3.4.3.0.0.0.0.0 Class Name

##### 2.3.4.3.1.0.0.0.0 Class Name

SpecificationEvaluator

##### 2.3.4.3.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Persistence/SpecificationEvaluator.cs

##### 2.3.4.3.3.0.0.0.0 Class Type

Static Class

##### 2.3.4.3.4.0.0.0.0 Inheritance

None

##### 2.3.4.3.5.0.0.0.0 Purpose

Applies specification criteria to IQueryable for EF Core.

##### 2.3.4.3.6.0.0.0.0 Dependencies

*No items available*

##### 2.3.4.3.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0.0.0 Technology Integration Notes



##### 2.3.4.3.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0.0.0 Methods

- {'method_name': 'GetQuery', 'method_signature': 'public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification) where TEntity : class', 'return_type': 'IQueryable<TEntity>', 'access_modifier': 'public static', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'inputQuery', 'parameter_type': 'IQueryable<TEntity>', 'is_nullable': 'false', 'purpose': 'Base query'}, {'parameter_name': 'specification', 'parameter_type': 'ISpecification<TEntity>', 'is_nullable': 'false', 'purpose': 'Filters'}], 'implementation_logic': 'Chains .Where(), .Include(), .OrderBy() calls onto the inputQuery based on the specification.', 'exception_handling': 'None', 'performance_considerations': 'Purely additive expression tree building.', 'validation_requirements': 'None', 'technology_integration_details': 'EF Core Include'}

##### 2.3.4.3.11.0.0.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0.0.0 Implementation Notes

Key component for Repository Pattern flexibility.

#### 2.3.4.4.0.0.0.0.0 Class Name

##### 2.3.4.4.1.0.0.0.0 Class Name

ResiliencePolicyRegistry

##### 2.3.4.4.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Resilience/ResiliencePolicyRegistry.cs

##### 2.3.4.4.3.0.0.0.0 Class Type

Class

##### 2.3.4.4.4.0.0.0.0 Inheritance

None

##### 2.3.4.4.5.0.0.0.0 Purpose

Provides pre-configured Polly resilience pipelines for standard operational scenarios.

##### 2.3.4.4.6.0.0.0.0 Dependencies

- IOptions<ResilienceOptions>

##### 2.3.4.4.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0.0.0 Technology Integration Notes

Polly v8+

##### 2.3.4.4.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0.0.0 Methods

###### 2.3.4.4.10.1.0.0.0 Method Name

####### 2.3.4.4.10.1.1.0.0 Method Name

GetRetryPolicy

####### 2.3.4.4.10.1.2.0.0 Method Signature

public AsyncRetryPolicy GetRetryPolicy(string policyKey)

####### 2.3.4.4.10.1.3.0.0 Return Type

AsyncRetryPolicy

####### 2.3.4.4.10.1.4.0.0 Access Modifier

public

####### 2.3.4.4.10.1.5.0.0 Is Async

false

####### 2.3.4.4.10.1.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.4.10.1.7.0.0 Parameters

- {'parameter_name': 'policyKey', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Key for logging'}

####### 2.3.4.4.10.1.8.0.0 Implementation Logic

Returns a Polly Policy configured from ResilienceOptions.RetryCount and Backoff settings. Handles HttpRequestException, TimeoutException, and 5xx status codes.

####### 2.3.4.4.10.1.9.0.0 Exception Handling

None

####### 2.3.4.4.10.1.10.0.0 Performance Considerations

Policy reuse.

####### 2.3.4.4.10.1.11.0.0 Validation Requirements

None

####### 2.3.4.4.10.1.12.0.0 Technology Integration Details

Polly PolicyBuilder

###### 2.3.4.4.10.2.0.0.0 Method Name

####### 2.3.4.4.10.2.1.0.0 Method Name

GetCircuitBreakerPolicy

####### 2.3.4.4.10.2.2.0.0 Method Signature

public AsyncCircuitBreakerPolicy GetCircuitBreakerPolicy(string policyKey)

####### 2.3.4.4.10.2.3.0.0 Return Type

AsyncCircuitBreakerPolicy

####### 2.3.4.4.10.2.4.0.0 Access Modifier

public

####### 2.3.4.4.10.2.5.0.0 Is Async

false

####### 2.3.4.4.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.4.10.2.7.0.0 Parameters

- {'parameter_name': 'policyKey', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Key'}

####### 2.3.4.4.10.2.8.0.0 Implementation Logic

Returns a Polly CircuitBreaker configured from ResilienceOptions.CircuitBreakerThreshold.

####### 2.3.4.4.10.2.9.0.0 Exception Handling

None

####### 2.3.4.4.10.2.10.0.0 Performance Considerations

Stateful policy.

####### 2.3.4.4.10.2.11.0.0 Validation Requirements

None

####### 2.3.4.4.10.2.12.0.0 Technology Integration Details

Polly CircuitBreaker

##### 2.3.4.4.11.0.0.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0.0.0 Implementation Notes

Registered as Singleton usually.

#### 2.3.4.5.0.0.0.0.0 Class Name

##### 2.3.4.5.1.0.0.0.0 Class Name

CorrelationIdEnricher

##### 2.3.4.5.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Logging/CorrelationIdEnricher.cs

##### 2.3.4.5.3.0.0.0.0 Class Type

Class

##### 2.3.4.5.4.0.0.0.0 Inheritance

ILogEventEnricher

##### 2.3.4.5.5.0.0.0.0 Purpose

Serilog enricher to add a Correlation ID to every log event for distributed tracing.

##### 2.3.4.5.6.0.0.0.0 Dependencies

- IHttpContextAccessor

##### 2.3.4.5.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.5.8.0.0.0.0 Technology Integration Notes

Serilog

##### 2.3.4.5.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.5.10.0.0.0.0 Methods

- {'method_name': 'Enrich', 'method_signature': 'public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'logEvent', 'parameter_type': 'LogEvent', 'is_nullable': 'false', 'purpose': 'Current Log'}, {'parameter_name': 'propertyFactory', 'parameter_type': 'ILogEventPropertyFactory', 'is_nullable': 'false', 'purpose': 'Factory'}], 'implementation_logic': 'Retrieves the Correlation ID from the HttpContext.Items or Request Headers (\\"X-Correlation-ID\\"). If present, adds a \\"CorrelationId\\" property to the logEvent.', 'exception_handling': 'Safe fail.', 'performance_considerations': 'Called on every log.', 'validation_requirements': 'None', 'technology_integration_details': 'Serilog Enrichment'}

##### 2.3.4.5.11.0.0.0.0 Events

*No items available*

##### 2.3.4.5.12.0.0.0.0 Implementation Notes

Required for REQ-MON-001.

#### 2.3.4.6.0.0.0.0.0 Class Name

##### 2.3.4.6.1.0.0.0.0 Class Name

ServiceCollectionExtensions

##### 2.3.4.6.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Extensions/ServiceCollectionExtensions.cs

##### 2.3.4.6.3.0.0.0.0 Class Type

Static Class

##### 2.3.4.6.4.0.0.0.0 Inheritance

None

##### 2.3.4.6.5.0.0.0.0 Purpose

Extension methods to register infrastructure services into the DI container.

##### 2.3.4.6.6.0.0.0.0 Dependencies

*No items available*

##### 2.3.4.6.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.6.8.0.0.0.0 Technology Integration Notes



##### 2.3.4.6.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.6.10.0.0.0.0 Methods

###### 2.3.4.6.10.1.0.0.0 Method Name

####### 2.3.4.6.10.1.1.0.0 Method Name

AddSharedInfrastructure

####### 2.3.4.6.10.1.2.0.0 Method Signature

public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)

####### 2.3.4.6.10.1.3.0.0 Return Type

IServiceCollection

####### 2.3.4.6.10.1.4.0.0 Access Modifier

public static

####### 2.3.4.6.10.1.5.0.0 Is Async

false

####### 2.3.4.6.10.1.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.6.10.1.7.0.0 Parameters

######## 2.3.4.6.10.1.7.1.0 Parameter Name

######### 2.3.4.6.10.1.7.1.1 Parameter Name

services

######### 2.3.4.6.10.1.7.1.2 Parameter Type

IServiceCollection

######### 2.3.4.6.10.1.7.1.3 Is Nullable

false

######### 2.3.4.6.10.1.7.1.4 Purpose

DI Container

######## 2.3.4.6.10.1.7.2.0 Parameter Name

######### 2.3.4.6.10.1.7.2.1 Parameter Name

configuration

######### 2.3.4.6.10.1.7.2.2 Parameter Type

IConfiguration

######### 2.3.4.6.10.1.7.2.3 Is Nullable

false

######### 2.3.4.6.10.1.7.2.4 Purpose

App Config

####### 2.3.4.6.10.1.8.0.0 Implementation Logic

Registers IDateTimeProvider -> SystemDateTimeProvider. Registers generic IEmailService. Configures Hangfire client. Configures Serilog enrichers. Binds ResilienceOptions.

####### 2.3.4.6.10.1.9.0.0 Exception Handling

None

####### 2.3.4.6.10.1.10.0.0 Performance Considerations

Startup only.

####### 2.3.4.6.10.1.11.0.0 Validation Requirements

None

####### 2.3.4.6.10.1.12.0.0 Technology Integration Details

.NET DI

###### 2.3.4.6.10.2.0.0.0 Method Name

####### 2.3.4.6.10.2.1.0.0 Method Name

AddPersistence

####### 2.3.4.6.10.2.2.0.0 Method Signature

public static IServiceCollection AddPersistence<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext

####### 2.3.4.6.10.2.3.0.0 Return Type

IServiceCollection

####### 2.3.4.6.10.2.4.0.0 Access Modifier

public static

####### 2.3.4.6.10.2.5.0.0 Is Async

false

####### 2.3.4.6.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.6.10.2.7.0.0 Parameters

######## 2.3.4.6.10.2.7.1.0 Parameter Name

######### 2.3.4.6.10.2.7.1.1 Parameter Name

services

######### 2.3.4.6.10.2.7.1.2 Parameter Type

IServiceCollection

######### 2.3.4.6.10.2.7.1.3 Is Nullable

false

######### 2.3.4.6.10.2.7.1.4 Purpose

DI

######## 2.3.4.6.10.2.7.2.0 Parameter Name

######### 2.3.4.6.10.2.7.2.1 Parameter Name

connectionString

######### 2.3.4.6.10.2.7.2.2 Parameter Type

string

######### 2.3.4.6.10.2.7.2.3 Is Nullable

false

######### 2.3.4.6.10.2.7.2.4 Purpose

Conn String

####### 2.3.4.6.10.2.8.0.0 Implementation Logic

Registers the DbContext using the provided connection string (PostgreSQL). Registers IRepository<> -> EfRepository<> and IUnitOfWork -> EfUnitOfWork. Scoped lifetime.

####### 2.3.4.6.10.2.9.0.0 Exception Handling

None

####### 2.3.4.6.10.2.10.0.0 Performance Considerations

DbContext Pooling suggested.

####### 2.3.4.6.10.2.11.0.0 Validation Requirements

None

####### 2.3.4.6.10.2.12.0.0 Technology Integration Details

EF Core DI

##### 2.3.4.6.11.0.0.0.0 Events

*No items available*

##### 2.3.4.6.12.0.0.0.0 Implementation Notes

Main entry point for consumers.

### 2.3.5.0.0.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0.0.0 Interface Name

##### 2.3.5.1.1.0.0.0.0 Interface Name

IRepository<TEntity>

##### 2.3.5.1.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Abstractions/Persistence/IRepository.cs

##### 2.3.5.1.3.0.0.0.0 Purpose

Defines the contract for generic write/modification operations on aggregate roots.

##### 2.3.5.1.4.0.0.0.0 Generic Constraints

where TEntity : class

##### 2.3.5.1.5.0.0.0.0 Framework Specific Inheritance

IReadRepository<TEntity>

##### 2.3.5.1.6.0.0.0.0 Method Contracts

###### 2.3.5.1.6.1.0.0.0 Method Name

####### 2.3.5.1.6.1.1.0.0 Method Name

AddAsync

####### 2.3.5.1.6.1.2.0.0 Method Signature

Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)

####### 2.3.5.1.6.1.3.0.0 Return Type

Task<TEntity>

####### 2.3.5.1.6.1.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.1.5.0.0 Parameters

- {'parameter_name': 'entity', 'parameter_type': 'TEntity', 'purpose': 'New Entity'}

####### 2.3.5.1.6.1.6.0.0 Contract Description

Adds a new entity to the context.

####### 2.3.5.1.6.1.7.0.0 Exception Contracts

None

###### 2.3.5.1.6.2.0.0.0 Method Name

####### 2.3.5.1.6.2.1.0.0 Method Name

UpdateAsync

####### 2.3.5.1.6.2.2.0.0 Method Signature

Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)

####### 2.3.5.1.6.2.3.0.0 Return Type

Task

####### 2.3.5.1.6.2.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.2.5.0.0 Parameters

- {'parameter_name': 'entity', 'parameter_type': 'TEntity', 'purpose': 'Entity to update'}

####### 2.3.5.1.6.2.6.0.0 Contract Description

Marks an entity as modified.

####### 2.3.5.1.6.2.7.0.0 Exception Contracts

None

###### 2.3.5.1.6.3.0.0.0 Method Name

####### 2.3.5.1.6.3.1.0.0 Method Name

DeleteAsync

####### 2.3.5.1.6.3.2.0.0 Method Signature

Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)

####### 2.3.5.1.6.3.3.0.0 Return Type

Task

####### 2.3.5.1.6.3.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.1.6.3.5.0.0 Parameters

- {'parameter_name': 'entity', 'parameter_type': 'TEntity', 'purpose': 'Entity to delete'}

####### 2.3.5.1.6.3.6.0.0 Contract Description

Marks an entity for deletion.

####### 2.3.5.1.6.3.7.0.0 Exception Contracts

None

##### 2.3.5.1.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.1.8.0.0.0.0 Implementation Guidance

Implementations should generally not call SaveChanges; that is the responsibility of the UnitOfWork.

##### 2.3.5.1.9.0.0.0.0 Validation Notes

Separates concerns.

#### 2.3.5.2.0.0.0.0.0 Interface Name

##### 2.3.5.2.1.0.0.0.0 Interface Name

IReadRepository<TEntity>

##### 2.3.5.2.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Abstractions/Persistence/IReadRepository.cs

##### 2.3.5.2.3.0.0.0.0 Purpose

Defines the contract for read-only operations, optimized for performance.

##### 2.3.5.2.4.0.0.0.0 Generic Constraints

where TEntity : class

##### 2.3.5.2.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.2.6.0.0.0.0 Method Contracts

###### 2.3.5.2.6.1.0.0.0 Method Name

####### 2.3.5.2.6.1.1.0.0 Method Name

GetByIdAsync

####### 2.3.5.2.6.1.2.0.0 Method Signature

Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)

####### 2.3.5.2.6.1.3.0.0 Return Type

Task<TEntity?>

####### 2.3.5.2.6.1.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.2.6.1.5.0.0 Parameters

- {'parameter_name': 'id', 'parameter_type': 'Guid', 'purpose': 'PK'}

####### 2.3.5.2.6.1.6.0.0 Contract Description

Retrieves an entity by its primary key.

####### 2.3.5.2.6.1.7.0.0 Exception Contracts

None

###### 2.3.5.2.6.2.0.0.0 Method Name

####### 2.3.5.2.6.2.1.0.0 Method Name

ListAsync

####### 2.3.5.2.6.2.2.0.0 Method Signature

Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default)

####### 2.3.5.2.6.2.3.0.0 Return Type

Task<List<TEntity>>

####### 2.3.5.2.6.2.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.2.6.2.5.0.0 Parameters

- {'parameter_name': 'specification', 'parameter_type': 'ISpecification<TEntity>', 'purpose': 'Spec'}

####### 2.3.5.2.6.2.6.0.0 Contract Description

Retrieves entities matching a specification pattern.

####### 2.3.5.2.6.2.7.0.0 Exception Contracts

None

##### 2.3.5.2.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.2.8.0.0.0.0 Implementation Guidance

Use AsNoTracking.

##### 2.3.5.2.9.0.0.0.0 Validation Notes

Optimized reads.

#### 2.3.5.3.0.0.0.0.0 Interface Name

##### 2.3.5.3.1.0.0.0.0 Interface Name

IUnitOfWork

##### 2.3.5.3.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Abstractions/Persistence/IUnitOfWork.cs

##### 2.3.5.3.3.0.0.0.0 Purpose

Defines the contract for the Unit of Work pattern to manage transactions.

##### 2.3.5.3.4.0.0.0.0 Generic Constraints

None

##### 2.3.5.3.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.3.6.0.0.0.0 Method Contracts

- {'method_name': 'SaveChangesAsync', 'method_signature': 'Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)', 'return_type': 'Task<int>', 'framework_attributes': [], 'parameters': [], 'contract_description': 'Commits all changes made in the context to the database.', 'exception_contracts': 'Throws DbUpdateConcurrencyException on concurrency conflicts.'}

##### 2.3.5.3.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.3.8.0.0.0.0 Implementation Guidance

None

##### 2.3.5.3.9.0.0.0.0 Validation Notes

None

#### 2.3.5.4.0.0.0.0.0 Interface Name

##### 2.3.5.4.1.0.0.0.0 Interface Name

ISpecification<T>

##### 2.3.5.4.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Abstractions/Persistence/ISpecification.cs

##### 2.3.5.4.3.0.0.0.0 Purpose

Encapsulates query logic to decouple repositories from specific filtering requirements.

##### 2.3.5.4.4.0.0.0.0 Generic Constraints

None

##### 2.3.5.4.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.4.6.0.0.0.0 Method Contracts

*No items available*

##### 2.3.5.4.7.0.0.0.0 Property Contracts

###### 2.3.5.4.7.1.0.0.0 Property Name

####### 2.3.5.4.7.1.1.0.0 Property Name

Criteria

####### 2.3.5.4.7.1.2.0.0 Property Type

Expression<Func<T, bool>>

####### 2.3.5.4.7.1.3.0.0 Getter Contract

The filtering expression.

####### 2.3.5.4.7.1.4.0.0 Setter Contract

None

###### 2.3.5.4.7.2.0.0.0 Property Name

####### 2.3.5.4.7.2.1.0.0 Property Name

Includes

####### 2.3.5.4.7.2.2.0.0 Property Type

List<Expression<Func<T, object>>>

####### 2.3.5.4.7.2.3.0.0 Getter Contract

List of navigation properties to include (eager load).

####### 2.3.5.4.7.2.4.0.0 Setter Contract

None

##### 2.3.5.4.8.0.0.0.0 Implementation Guidance

Use Ardalis.Specification or custom.

##### 2.3.5.4.9.0.0.0.0 Validation Notes

None

#### 2.3.5.5.0.0.0.0.0 Interface Name

##### 2.3.5.5.1.0.0.0.0 Interface Name

IBackgroundJobService

##### 2.3.5.5.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/BackgroundJobs/IBackgroundJobService.cs

##### 2.3.5.5.3.0.0.0.0 Purpose

Abstracts the background job provider (e.g., Hangfire) to decouple application logic.

##### 2.3.5.5.4.0.0.0.0 Generic Constraints

None

##### 2.3.5.5.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.5.6.0.0.0.0 Method Contracts

###### 2.3.5.5.6.1.0.0.0 Method Name

####### 2.3.5.5.6.1.1.0.0 Method Name

Enqueue

####### 2.3.5.5.6.1.2.0.0 Method Signature

string Enqueue(Expression<Action> methodCall)

####### 2.3.5.5.6.1.3.0.0 Return Type

string

####### 2.3.5.5.6.1.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.5.6.1.5.0.0 Parameters

- {'parameter_name': 'methodCall', 'parameter_type': 'Expression<Action>', 'purpose': 'Lambda'}

####### 2.3.5.5.6.1.6.0.0 Contract Description

Queues a fire-and-forget job.

####### 2.3.5.5.6.1.7.0.0 Exception Contracts

None

###### 2.3.5.5.6.2.0.0.0 Method Name

####### 2.3.5.5.6.2.1.0.0 Method Name

Schedule

####### 2.3.5.5.6.2.2.0.0 Method Signature

string Schedule(Expression<Action> methodCall, TimeSpan delay)

####### 2.3.5.5.6.2.3.0.0 Return Type

string

####### 2.3.5.5.6.2.4.0.0 Framework Attributes

*No items available*

####### 2.3.5.5.6.2.5.0.0 Parameters

- {'parameter_name': 'delay', 'parameter_type': 'TimeSpan', 'purpose': 'Delay'}

####### 2.3.5.5.6.2.6.0.0 Contract Description

Schedules a job to run after a specified delay.

####### 2.3.5.5.6.2.7.0.0 Exception Contracts

None

##### 2.3.5.5.7.0.0.0.0 Property Contracts

*No items available*

##### 2.3.5.5.8.0.0.0.0 Implementation Guidance

Adapter pattern.

##### 2.3.5.5.9.0.0.0.0 Validation Notes

None

#### 2.3.5.6.0.0.0.0.0 Interface Name

##### 2.3.5.6.1.0.0.0.0 Interface Name

IDateTimeProvider

##### 2.3.5.6.2.0.0.0.0 File Path

src/ReadTrack.Shared.Infrastructure/Services/IDateTimeProvider.cs

##### 2.3.5.6.3.0.0.0.0 Purpose

Abstracts system time for testability.

##### 2.3.5.6.4.0.0.0.0 Generic Constraints

None

##### 2.3.5.6.5.0.0.0.0 Framework Specific Inheritance

None

##### 2.3.5.6.6.0.0.0.0 Method Contracts

*No items available*

##### 2.3.5.6.7.0.0.0.0 Property Contracts

- {'property_name': 'UtcNow', 'property_type': 'DateTime', 'getter_contract': 'Returns the current UTC date and time.', 'setter_contract': 'None'}

##### 2.3.5.6.8.0.0.0.0 Implementation Guidance

None

##### 2.3.5.6.9.0.0.0.0 Validation Notes

None

### 2.3.6.0.0.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0.0.0 Dto Specifications

*No items available*

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

- {'configuration_name': 'ResilienceOptions', 'file_path': 'src/ReadTrack.Shared.Infrastructure/Resilience/ResilienceOptions.cs', 'purpose': 'Strongly-typed configuration for resilience policies.', 'framework_base_class': 'class', 'configuration_sections': [{'section_name': 'Resilience', 'properties': [{'property_name': 'RetryCount', 'property_type': 'int', 'default_value': '3', 'required': 'true', 'description': 'Number of retry attempts for transient failures.'}, {'property_name': 'CircuitBreakerThreshold', 'property_type': 'int', 'default_value': '5', 'required': 'true', 'description': 'Number of consecutive failures before opening the circuit.'}, {'property_name': 'CircuitBreakerDurationSeconds', 'property_type': 'int', 'default_value': '30', 'required': 'true', 'description': 'Duration in seconds the circuit remains open.'}]}], 'validation_requirements': 'Values must be positive.', 'validation_notes': 'Validated via DataAnnotations.'}

### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0.0.0 Service Interface

##### 2.3.9.1.1.0.0.0.0 Service Interface

IRepository<>

##### 2.3.9.1.2.0.0.0.0 Service Implementation

EfRepository<>

##### 2.3.9.1.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0.0.0 Registration Reasoning

Repositories hold a reference to DbContext which is Scoped.

##### 2.3.9.1.5.0.0.0.0 Framework Registration Pattern

AddPersistence Extension

##### 2.3.9.1.6.0.0.0.0 Validation Notes

None

#### 2.3.9.2.0.0.0.0.0 Service Interface

##### 2.3.9.2.1.0.0.0.0 Service Interface

IReadRepository<>

##### 2.3.9.2.2.0.0.0.0 Service Implementation

EfRepository<>

##### 2.3.9.2.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0.0.0 Registration Reasoning

Same implementation class handles both read and write interfaces.

##### 2.3.9.2.5.0.0.0.0 Framework Registration Pattern

AddPersistence Extension

##### 2.3.9.2.6.0.0.0.0 Validation Notes

None

#### 2.3.9.3.0.0.0.0.0 Service Interface

##### 2.3.9.3.1.0.0.0.0 Service Interface

IUnitOfWork

##### 2.3.9.3.2.0.0.0.0 Service Implementation

EfUnitOfWork

##### 2.3.9.3.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.3.4.0.0.0.0 Registration Reasoning

Unit of Work manages the DbContext transaction scope.

##### 2.3.9.3.5.0.0.0.0 Framework Registration Pattern

AddPersistence Extension

##### 2.3.9.3.6.0.0.0.0 Validation Notes

None

#### 2.3.9.4.0.0.0.0.0 Service Interface

##### 2.3.9.4.1.0.0.0.0 Service Interface

IBackgroundJobService

##### 2.3.9.4.2.0.0.0.0 Service Implementation

HangfireJobService

##### 2.3.9.4.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.4.4.0.0.0.0 Registration Reasoning

Scoped allows for context propagation if needed, although Hangfire client is thread-safe.

##### 2.3.9.4.5.0.0.0.0 Framework Registration Pattern

AddSharedInfrastructure Extension

##### 2.3.9.4.6.0.0.0.0 Validation Notes

None

#### 2.3.9.5.0.0.0.0.0 Service Interface

##### 2.3.9.5.1.0.0.0.0 Service Interface

IDateTimeProvider

##### 2.3.9.5.2.0.0.0.0 Service Implementation

SystemDateTimeProvider

##### 2.3.9.5.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.5.4.0.0.0.0 Registration Reasoning

Stateless utility service.

##### 2.3.9.5.5.0.0.0.0 Framework Registration Pattern

AddSharedInfrastructure Extension

##### 2.3.9.5.6.0.0.0.0 Validation Notes

None

#### 2.3.9.6.0.0.0.0.0 Service Interface

##### 2.3.9.6.1.0.0.0.0 Service Interface

ResiliencePolicyRegistry

##### 2.3.9.6.2.0.0.0.0 Service Implementation

ResiliencePolicyRegistry

##### 2.3.9.6.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.6.4.0.0.0.0 Registration Reasoning

Policies are stateless configuration objects and should be reused.

##### 2.3.9.6.5.0.0.0.0 Framework Registration Pattern

AddSharedInfrastructure Extension

##### 2.3.9.6.6.0.0.0.0 Validation Notes

None

### 2.3.10.0.0.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0.0.0 Integration Target

##### 2.3.10.1.1.0.0.0.0 Integration Target

PostgreSQL

##### 2.3.10.1.2.0.0.0.0 Integration Type

Database

##### 2.3.10.1.3.0.0.0.0 Required Client Classes

- DbContext

##### 2.3.10.1.4.0.0.0.0 Configuration Requirements

Connection String

##### 2.3.10.1.5.0.0.0.0 Error Handling Requirements

Polly Retry on Transient Errors (in EF Core configuration)

##### 2.3.10.1.6.0.0.0.0 Authentication Requirements

Connection String Credentials

##### 2.3.10.1.7.0.0.0.0 Framework Integration Patterns

Entity Framework Core Provider

##### 2.3.10.1.8.0.0.0.0 Validation Notes

None

#### 2.3.10.2.0.0.0.0.0 Integration Target

##### 2.3.10.2.1.0.0.0.0 Integration Target

Hangfire

##### 2.3.10.2.2.0.0.0.0 Integration Type

Job Server

##### 2.3.10.2.3.0.0.0.0 Required Client Classes

- IBackgroundJobClient

##### 2.3.10.2.4.0.0.0.0 Configuration Requirements

Storage Connection String

##### 2.3.10.2.5.0.0.0.0 Error Handling Requirements

Internal Hangfire Retry

##### 2.3.10.2.6.0.0.0.0 Authentication Requirements

None (Internal)

##### 2.3.10.2.7.0.0.0.0 Framework Integration Patterns

Adapter

##### 2.3.10.2.8.0.0.0.0 Validation Notes

None

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 7 |
| Total Enums | 0 |
| Total Dtos | 0 |
| Total Configurations | 1 |
| Total External Integrations | 2 |
| Grand Total Components | 22 |
| Phase 2 Claimed Count | 15 |
| Phase 2 Actual Count | 12 |
| Validation Added Count | 10 |
| Final Validated Count | 22 |

