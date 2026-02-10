# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-LIB-INFRA |
| Extraction Timestamp | 2025-01-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-REL-002

#### 1.2.1.2 Requirement Text

The system must implement resilience patterns to handle failures gracefully when communicating with critical external APIs.

#### 1.2.1.3 Validation Criteria

- A circuit breaker pattern must be implemented
- Retry policies with exponential backoff must be defined

#### 1.2.1.4 Implementation Implications

- Implement ResiliencePolicyRegistry using Polly v8+
- Define AsyncRetryPolicy and AsyncCircuitBreakerPolicy configurations via ResilienceOptions
- Expose named policies for 'OpenAI', 'GoogleBooks', and 'Contentful'

#### 1.2.1.5 Extraction Reasoning

The library acts as the central definition point for resilience strategies used by feature modules.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-PERF-001

#### 1.2.2.2 Requirement Text

The 95th percentile latency for all core backend API endpoints must be less than 200 milliseconds under nominal load.

#### 1.2.2.3 Validation Criteria

- Database queries must be optimized (e.g., NoTracking)
- Connection pooling must be utilized

#### 1.2.2.4 Implementation Implications

- Implement IReadRepository<T> enforcing AsNoTracking() for read operations
- Implement SpecificationEvaluator to compile queries efficiently
- Configure DbContext pooling in ServiceCollectionExtensions

#### 1.2.2.5 Extraction Reasoning

Foundational data access patterns in this library directly dictate the performance baseline for all modules.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-MON-001

#### 1.2.3.2 Requirement Text

System auditing and monitoring via centralized logging.

#### 1.2.3.3 Validation Criteria

- Structured logging must be implemented
- Correlation IDs must be propagated

#### 1.2.3.4 Implementation Implications

- Implement CorrelationIdEnricher for Serilog
- Implement LoggingBehavior for MediatR pipelines to log request/response metrics automatically

#### 1.2.3.5 Extraction Reasoning

Centralized logging infrastructure is a core responsibility of this cross-cutting library.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

REQ-FUNC-009

#### 1.2.4.2 Requirement Text

Asynchronous backend job for data export.

#### 1.2.4.3 Validation Criteria

- Job queuing mechanism must be abstracted

#### 1.2.4.4 Implementation Implications

- Define IBackgroundJobService abstraction
- Implement HangfireJobService adapter to decouple domain logic from Hangfire dependencies

#### 1.2.4.5 Extraction Reasoning

The shared infrastructure must provide the mechanism for modules to offload long-running tasks.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

EfRepository<T>

#### 1.3.1.2 Component Specification

Generic repository implementation using EF Core 8.

#### 1.3.1.3 Implementation Requirements

- Implement IRepository<T> and IReadRepository<T>
- Utilize SpecificationEvaluator for dynamic query generation
- Support CancellationToken propagation for all async methods

#### 1.3.1.4 Architectural Context

Shared Infrastructure Layer - Data Access

#### 1.3.1.5 Extraction Reasoning

Standardizes database interactions and query optimization across the monolith.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

ResiliencePolicyRegistry

#### 1.3.2.2 Component Specification

Central registry for Polly resilience pipelines.

#### 1.3.2.3 Implementation Requirements

- Load configuration from ResilienceOptions (appsettings.json)
- Provide factory methods for HTTP client resilience strategies
- Integrate with Microsoft.Extensions.Http.Resilience

#### 1.3.2.4 Architectural Context

Shared Infrastructure Layer - Resilience

#### 1.3.2.5 Extraction Reasoning

Centralizes fault tolerance logic to ensure consistent behavior across external integrations.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

EfUnitOfWork

#### 1.3.3.2 Component Specification

Transaction management wrapper for EF Core.

#### 1.3.3.3 Implementation Requirements

- Manage DbContext lifetime and transaction scope
- Dispatch Domain Events before or after commit based on configuration
- Handle concurrency exceptions

#### 1.3.3.4 Architectural Context

Shared Infrastructure Layer - Persistence

#### 1.3.3.5 Extraction Reasoning

Required for atomic operations spanning multiple repositories within a module.

### 1.3.4.0 Component Name

#### 1.3.4.1 Component Name

HangfireJobService

#### 1.3.4.2 Component Specification

Adapter for Hangfire background job client.

#### 1.3.4.3 Implementation Requirements

- Implement IBackgroundJobService
- Abstract the static BackgroundJob.Enqueue calls into instance methods for testability

#### 1.3.4.4 Architectural Context

Shared Infrastructure Layer - Job Processing

#### 1.3.4.5 Extraction Reasoning

Decouples application logic from the specific job runner implementation.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Shared Infrastructure', 'layer_responsibilities': 'Provide cross-cutting technical implementations (Logging, Caching, Resilience, Data Access) used by all business modules.', 'layer_constraints': ['Must NOT contain business domain entities', 'Must NOT depend on feature modules', 'Must expose interfaces in a separate Abstractions namespace'], 'implementation_patterns': ['Repository Pattern', 'Adapter Pattern', 'Decorator Pattern'], 'extraction_reasoning': 'Serves as the technical foundation (Shared Kernel) for the Modular Monolith.'}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

DbContext

#### 1.5.1.2 Source Repository

Microsoft.EntityFrameworkCore

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

Set<T>

###### 1.5.1.3.1.2 Method Signature

DbSet<T> Set<T>() where T : class

###### 1.5.1.3.1.3 Method Purpose

Accesses the DbSet for generic entity operations

###### 1.5.1.3.1.4 Integration Context

Used within EfRepository

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

SaveChangesAsync

###### 1.5.1.3.2.2 Method Signature

Task<int> SaveChangesAsync(CancellationToken token)

###### 1.5.1.3.2.3 Method Purpose

Commits transaction

###### 1.5.1.3.2.4 Integration Context

Used within EfUnitOfWork

#### 1.5.1.4.0.0 Integration Pattern

Framework Class Inheritance

#### 1.5.1.5.0.0 Communication Protocol

In-Process

#### 1.5.1.6.0.0 Extraction Reasoning

Core dependency for persistence implementation.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

IBackgroundJobClient

#### 1.5.2.2.0.0 Source Repository

Hangfire.Core

#### 1.5.2.3.0.0 Method Contracts

- {'method_name': 'Enqueue', 'method_signature': 'string Enqueue(Expression<Action> methodCall)', 'method_purpose': 'Schedules a job', 'integration_context': 'Used within HangfireJobService'}

#### 1.5.2.4.0.0 Integration Pattern

Library Wrapper

#### 1.5.2.5.0.0 Communication Protocol

In-Process / Database

#### 1.5.2.6.0.0 Extraction Reasoning

Core dependency for async job implementation.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

IRepository<T>

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-BE-MOD-USERS
- REPO-BE-MOD-READING
- REPO-BE-MOD-MONETIZATION
- REPO-BE-MOD-ENGAGEMENT
- REPO-BE-MOD-RECOMMENDATIONS

#### 1.6.1.3.0.0 Method Contracts

- {'method_name': 'AddAsync', 'method_signature': 'Task<T> AddAsync(T entity, CancellationToken token)', 'method_purpose': 'Adds entity to change tracker', 'implementation_requirements': 'Generic implementation wrapping DbSet.AddAsync'}

#### 1.6.1.4.0.0 Service Level Requirements

- Zero overhead abstraction

#### 1.6.1.5.0.0 Implementation Constraints

- T must be a class

#### 1.6.1.6.0.0 Extraction Reasoning

Primary data access contract for all domain modules.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

IReadRepository<T>

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-BE-MOD-READING
- REPO-BE-MOD-ENGAGEMENT

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'ListAsync', 'method_signature': 'Task<List<T>> ListAsync(ISpecification<T> spec, CancellationToken token)', 'method_purpose': 'Retrieves filtered list using Specification pattern', 'implementation_requirements': 'Must use AsNoTracking() for performance'}

#### 1.6.2.4.0.0 Service Level Requirements

- Optimized for high-throughput reads

#### 1.6.2.5.0.0 Implementation Constraints

- Read-only operations

#### 1.6.2.6.0.0 Extraction Reasoning

Separates read-only concerns to support CQRS query handlers.

### 1.6.3.0.0.0 Interface Name

#### 1.6.3.1.0.0 Interface Name

IBackgroundJobService

#### 1.6.3.2.0.0 Consumer Repositories

- REPO-BE-MOD-USERS
- REPO-BE-MOD-MONETIZATION
- REPO-BE-MOD-RECOMMENDATIONS

#### 1.6.3.3.0.0 Method Contracts

- {'method_name': 'Enqueue', 'method_signature': 'string Enqueue(Expression<Action> methodCall)', 'method_purpose': 'Abstracts fire-and-forget job scheduling', 'implementation_requirements': 'Must handle serialization of arguments'}

#### 1.6.3.4.0.0 Service Level Requirements

- High availability

#### 1.6.3.5.0.0 Implementation Constraints

- Job arguments must be serializable

#### 1.6.3.6.0.0 Extraction Reasoning

Provides job scheduling capabilities without direct Hangfire dependency in modules.

### 1.6.4.0.0.0 Interface Name

#### 1.6.4.1.0.0 Interface Name

IResiliencePolicyProvider

#### 1.6.4.2.0.0 Consumer Repositories

- REPO-BE-MOD-READING
- REPO-BE-MOD-RECOMMENDATIONS
- REPO-BE-MOD-ENGAGEMENT

#### 1.6.4.3.0.0 Method Contracts

- {'method_name': 'GetRetryPolicy', 'method_signature': 'AsyncRetryPolicy GetRetryPolicy(string key)', 'method_purpose': 'Returns a configured Polly retry policy', 'implementation_requirements': 'Must load settings from configuration'}

#### 1.6.4.4.0.0 Service Level Requirements

- Thread-safe policy retrieval

#### 1.6.4.5.0.0 Implementation Constraints

- Policies should be cached/singleton

#### 1.6.4.6.0.0 Extraction Reasoning

Centralizes configuration of circuit breakers and retries.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8 Class Library

### 1.7.2.0.0.0 Integration Technologies

- Entity Framework Core 8
- Polly v8+
- Serilog
- Hangfire
- MediatR

### 1.7.3.0.0.0 Performance Constraints

Base repository methods must use AsNoTracking by default for IReadRepository to minimize GC pressure and ChangeTracker overhead.

### 1.7.4.0.0.0 Security Requirements

Infrastructure components must not log sensitive data (PII) in exceptions or traces. DbContext implementations must support connection string injection from secure sources.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All core cross-cutting concerns (Data Access, Resi... |
| Cross Reference Validation | Validated usage by User, Reading, and Recommendati... |
| Implementation Readiness Assessment | High. Clear interface definitions and adapter patt... |
| Quality Assurance Confirmation | Adheres to Dependency Inversion Principle, keeping... |

