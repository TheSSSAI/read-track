# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-MONETIZATION |
| Validation Timestamp | 2025-10-27T12:00:00Z |
| Original Component Count Claimed | 29 |
| Original Component Count Actual | 29 |
| Gaps Identified Count | 0 |
| Components Added Count | 0 |
| Final Component Count | 29 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Strict alignment with Integration Design contracts... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant. Isolates monetization logic, payment provider integration, and subscription state management.

#### 2.2.1.2 Gaps Identified

*No items available*

#### 2.2.1.3 Components Added

*No items available*

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100% (REQ-FRE-001, REQ-ADS-001, REQ-FUNC-002)

#### 2.2.2.2 Non Functional Requirements Coverage

100% (Security via Signature Validation, Reliability via Idempotency Checks)

#### 2.2.2.3 Missing Requirement Components

*No items available*

#### 2.2.2.4 Added Requirement Components

*No items available*

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Complete. CQRS, Strategy Pattern (Providers), and Options Pattern utilized.

#### 2.2.3.2 Missing Pattern Components

*No items available*

#### 2.2.3.3 Added Pattern Components

*No items available*

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Complete. Subscription and PaymentTransaction entities defined with EF Core configuration.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Complete. Webhook flows and Service interactions mapped.

#### 2.2.5.2 Missing Interaction Components

*No items available*

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-MONETIZATION |
| Technology Stack | .NET 8, ASP.NET Core 8, Entity Framework Core 8, M... |
| Technology Guidance Integration | Implementation uses C# 12 Primary Constructors, Re... |
| Framework Compliance Score | 100% |
| Specification Completeness | 100% |
| Component Count | 29 |
| Specification Methodology | Domain-Driven Design with Clean Architecture |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- CQRS (MediatR)
- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- Options Pattern (IOptionsSnapshot)
- Resilient HTTP Clients (Microsoft.Extensions.Http.Resilience)
- Background Services (Hangfire)
- EF Core Code-First Migrations

#### 2.3.2.2 Directory Structure Source

Modular Monolith Vertical Slice

#### 2.3.2.3 Naming Conventions Source

Microsoft C# Coding Standards

#### 2.3.2.4 Architectural Patterns Source

Clean Architecture / DDD

#### 2.3.2.5 Performance Optimizations Applied

- AsNoTracking for Read Queries
- Database Indexing on External Transaction IDs for O(1) Idempotency Checks
- Async/Await I/O

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

.github/workflows/infrastructure-cd.yml

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- infrastructure-cd.yml

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

backend/nuget.config

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- nuget.config

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/ReadTrack.Backend.sln

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- ReadTrack.Backend.sln

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

backend/src/ReadTrack.Api/appsettings.Testing.json

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- appsettings.Testing.json

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/src/ReadTrack.Api/Dockerfile

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

backend/src/ReadTrack.Api/Properties/launchSettings.json

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

backend/src/ReadTrack.Api/ReadTrack.Api.csproj

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- ReadTrack.Api.csproj

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

backend/src/ReadTrack.Shared.Contracts/ReadTrack.Shared.Contracts.csproj

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- ReadTrack.Shared.Contracts.csproj

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

codecov.yml

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- codecov.yml

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

docker-compose.yml

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- docker-compose.yml

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- cdk.json

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- package.json

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

infrastructure/tsconfig.json

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- tsconfig.json

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- analysis_options.yaml

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

src/ReadTrack.Monetization.Application

###### 2.3.3.1.23.2 Purpose

Orchestrates use cases via CQRS commands and queries.

###### 2.3.3.1.23.3 Contains Files

- Commands/ProcessWebhook/ProcessWebhookCommand.cs
- Commands/ProcessWebhook/ProcessWebhookCommandHandler.cs
- Queries/GetSubscriptionStatus/GetSubscriptionStatusQuery.cs
- Queries/GetSubscriptionStatus/GetSubscriptionStatusQueryHandler.cs
- Interfaces/IPaymentProviderService.cs
- Interfaces/IPaymentProviderFactory.cs
- Services/ISubscriptionService.cs
- DTOs/SubscriptionStatusDto.cs

###### 2.3.3.1.23.4 Organizational Reasoning

Defines application boundaries and usage patterns.

###### 2.3.3.1.23.5 Framework Convention Alignment

Clean Architecture Application Layer

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

src/ReadTrack.Monetization.Domain

###### 2.3.3.1.24.2 Purpose

Contains pure business logic, entities, value objects, and domain events.

###### 2.3.3.1.24.3 Contains Files

- Entities/Subscription.cs
- Entities/PaymentTransaction.cs
- Entities/WebhookAuditLog.cs
- ValueObjects/SubscriptionTier.cs
- ValueObjects/SubscriptionStatus.cs
- Events/SubscriptionRenewedEvent.cs
- Events/SubscriptionTerminatedEvent.cs
- Exceptions/PaymentProviderException.cs
- Interfaces/ISubscriptionRepository.cs

###### 2.3.3.1.24.4 Organizational Reasoning

Isolates domain rules from infrastructure concerns.

###### 2.3.3.1.24.5 Framework Convention Alignment

Clean Architecture Domain Layer

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

src/ReadTrack.Monetization.Infrastructure

###### 2.3.3.1.25.2 Purpose

Implements external integrations and data persistence.

###### 2.3.3.1.25.3 Contains Files

- Persistence/MonetizationDbContext.cs
- Persistence/Configurations/SubscriptionConfiguration.cs
- Persistence/Repositories/SubscriptionRepository.cs
- Services/PaymentProviderFactory.cs
- Services/AppleStoreClient.cs
- Services/GooglePlayClient.cs
- Jobs/SubscriptionExpirationJob.cs
- Configuration/AppleStoreSettings.cs
- Configuration/GooglePlaySettings.cs

###### 2.3.3.1.25.4 Organizational Reasoning

Encapsulates technology-specific implementations.

###### 2.3.3.1.25.5 Framework Convention Alignment

Clean Architecture Infrastructure Layer

##### 2.3.3.1.26.0 Directory Path

###### 2.3.3.1.26.1 Directory Path

src/ReadTrack.Monetization.Presentation

###### 2.3.3.1.26.2 Purpose

Exposes functionality via HTTP APIs.

###### 2.3.3.1.26.3 Contains Files

- Controllers/WebhooksController.cs
- DTOs/AppleWebhookRequest.cs
- DTOs/GoogleWebhookRequest.cs

###### 2.3.3.1.26.4 Organizational Reasoning

Handles HTTP transport concerns.

###### 2.3.3.1.26.5 Framework Convention Alignment

ASP.NET Core Web API

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | ReadTrack.Monetization |
| Namespace Organization | ReadTrack.Monetization.{Layer}.{Component} |
| Naming Conventions | PascalCase |
| Framework Alignment | .NET 8 |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

Subscription

##### 2.3.4.1.2.0 File Path

src/ReadTrack.Monetization.Domain/Entities/Subscription.cs

##### 2.3.4.1.3.0 Class Type

Entity

##### 2.3.4.1.4.0 Inheritance

AggregateRoot<Guid>

##### 2.3.4.1.5.0 Purpose

Represents the user's subscription state and history.

##### 2.3.4.1.6.0 Dependencies

*No items available*

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses C# properties with private setters. Manages state transitions.

##### 2.3.4.1.9.0 Properties

###### 2.3.4.1.9.1 Property Name

####### 2.3.4.1.9.1.1 Property Name

UserId

####### 2.3.4.1.9.1.2 Property Type

Guid

####### 2.3.4.1.9.1.3 Access Modifier

public

####### 2.3.4.1.9.1.4 Purpose

Foreign key to User module.

####### 2.3.4.1.9.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.1.6 Framework Specific Configuration

Indexed

####### 2.3.4.1.9.1.7 Implementation Notes

Links to the Identity user.

###### 2.3.4.1.9.2.0 Property Name

####### 2.3.4.1.9.2.1 Property Name

Tier

####### 2.3.4.1.9.2.2 Property Type

SubscriptionTier

####### 2.3.4.1.9.2.3 Access Modifier

public

####### 2.3.4.1.9.2.4 Purpose

Current access level.

####### 2.3.4.1.9.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.2.6 Framework Specific Configuration

Enum conversion

####### 2.3.4.1.9.2.7 Implementation Notes

Free or Premium.

###### 2.3.4.1.9.3.0 Property Name

####### 2.3.4.1.9.3.1 Property Name

ValidUntil

####### 2.3.4.1.9.3.2 Property Type

DateTimeOffset

####### 2.3.4.1.9.3.3 Access Modifier

public

####### 2.3.4.1.9.3.4 Purpose

Expiration timestamp.

####### 2.3.4.1.9.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.3.6 Framework Specific Configuration

UTC

####### 2.3.4.1.9.3.7 Implementation Notes

Used to calculate active status.

###### 2.3.4.1.9.4.0 Property Name

####### 2.3.4.1.9.4.1 Property Name

ExternalSubscriptionId

####### 2.3.4.1.9.4.2 Property Type

string

####### 2.3.4.1.9.4.3 Access Modifier

public

####### 2.3.4.1.9.4.4 Purpose

Provider-specific ID for webhook correlation.

####### 2.3.4.1.9.4.5 Validation Attributes

*No items available*

####### 2.3.4.1.9.4.6 Framework Specific Configuration

Indexed

####### 2.3.4.1.9.4.7 Implementation Notes

Maps to Apple's original_transaction_id or Google's purchaseToken.

##### 2.3.4.1.10.0.0 Methods

- {'method_name': 'Renew', 'method_signature': 'public void Renew(DateTimeOffset newExpiry, string providerTransactionId)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'newExpiry', 'parameter_type': 'DateTimeOffset', 'is_nullable': 'false', 'purpose': 'New expiration date.'}, {'parameter_name': 'providerTransactionId', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'Unique ID of the renewal transaction.'}], 'implementation_logic': 'Updates ValidUntil. Adds SubscriptionRenewedEvent to domain events. Updates internal transaction history.', 'exception_handling': 'Throws DomainException if newExpiry < Current Expiry.', 'performance_considerations': 'In-memory operation.', 'validation_requirements': 'Input validation.', 'technology_integration_details': 'Domain Events pattern.'}

##### 2.3.4.1.11.0.0 Events

- {'event_name': 'SubscriptionRenewedEvent', 'event_type': 'DomainEvent', 'trigger_conditions': 'When Renew() is called successfully.', 'event_data': 'UserId, NewExpiryDate'}

##### 2.3.4.1.12.0.0 Implementation Notes

Core aggregate ensuring invariants.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

ProcessWebhookCommandHandler

##### 2.3.4.2.2.0.0 File Path

src/ReadTrack.Monetization.Application/Commands/ProcessWebhook/ProcessWebhookCommandHandler.cs

##### 2.3.4.2.3.0.0 Class Type

CommandHandler

##### 2.3.4.2.4.0.0 Inheritance

IRequestHandler<ProcessWebhookCommand, Result>

##### 2.3.4.2.5.0.0 Purpose

Handles incoming webhook requests idempotently.

##### 2.3.4.2.6.0.0 Dependencies

- ISubscriptionRepository
- IPaymentProviderFactory
- IUserService
- IUnitOfWork
- ILogger<ProcessWebhookCommandHandler>

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Uses C# 12 Primary Constructor for DI.

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'Handle', 'method_signature': 'public async Task<Result> Handle(ProcessWebhookCommand request, CancellationToken cancellationToken)', 'return_type': 'Task<Result>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'ProcessWebhookCommand', 'is_nullable': 'false', 'purpose': 'Command payload.'}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': 'false', 'purpose': 'Cancellation token.'}], 'implementation_logic': '1. Resolve provider service via Factory. 2. Verify signature. 3. Parse payload to get Transaction ID. 4. Check if Transaction ID already processed (Idempotency). 5. If new, update/create Subscription entity. 6. Call IUserService.UpdateUserTierAsync. 7. Commit UnitOfWork. 8. Log audit trail.', 'exception_handling': 'Catches PaymentProviderException, returns Result.Failure. Logs security failures.', 'performance_considerations': 'Transactional consistency.', 'validation_requirements': 'Signature verification must pass.', 'technology_integration_details': 'MediatR pipeline.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

Critical path for revenue.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

AppleStoreClient

##### 2.3.4.3.2.0.0 File Path

src/ReadTrack.Monetization.Infrastructure/Services/AppleStoreClient.cs

##### 2.3.4.3.3.0.0 Class Type

Service

##### 2.3.4.3.4.0.0 Inheritance

IPaymentProviderService

##### 2.3.4.3.5.0.0 Purpose

Handles communication and validation with Apple App Store Server API.

##### 2.3.4.3.6.0.0 Dependencies

- HttpClient
- IOptionsSnapshot<AppleStoreSettings>
- ILogger<AppleStoreClient>

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Uses System.IdentityModel.Tokens.Jwt for JWS verification.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': 'ValidateSignature', 'method_signature': 'public bool ValidateSignature(string payload, string signature)', 'return_type': 'bool', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'payload', 'parameter_type': 'string', 'is_nullable': 'false', 'purpose': 'The JWS payload.'}, {'parameter_name': 'signature', 'parameter_type': 'string', 'is_nullable': 'true', 'purpose': 'Unused for Apple V2 (embedded).'}], 'implementation_logic': 'Parses JWS. Verifies certificate chain against Apple Root CA. Validates signature.', 'exception_handling': 'Returns false on validation failure. Logs specific crypto error.', 'performance_considerations': 'Certificate validation is heavy; standard caching applies.', 'validation_requirements': 'Strict chain of trust.', 'technology_integration_details': 'BouncyCastle or System.Security.Cryptography.'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Implements Apple V2 Notifications logic.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

WebhooksController

##### 2.3.4.4.2.0.0 File Path

src/ReadTrack.Monetization.Presentation/Controllers/WebhooksController.cs

##### 2.3.4.4.3.0.0 Class Type

Controller

##### 2.3.4.4.4.0.0 Inheritance

BaseApiController

##### 2.3.4.4.5.0.0 Purpose

Receives external webhook requests.

##### 2.3.4.4.6.0.0 Dependencies

- ISender (MediatR)

##### 2.3.4.4.7.0.0 Framework Specific Attributes

- [ApiController]
- [Route(\"api/v1/webhooks\")]

##### 2.3.4.4.8.0.0 Technology Integration Notes

Minimal logic, delegates to MediatR.

##### 2.3.4.4.9.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0 Methods

- {'method_name': 'AppleWebhook', 'method_signature': 'public async Task<IActionResult> AppleWebhook([FromBody] AppleWebhookRequest request)', 'return_type': 'Task<IActionResult>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': ['[HttpPost(\\"apple\\")]'], 'parameters': [{'parameter_name': 'request', 'parameter_type': 'AppleWebhookRequest', 'is_nullable': 'false', 'purpose': 'JSON payload.'}], 'implementation_logic': 'Creates ProcessWebhookCommand(Provider.Apple, request.SignedPayload). Sends to MediatR. Returns 200 OK.', 'exception_handling': 'Global Exception Handler manages 500s.', 'performance_considerations': 'Fast return expected by Apple.', 'validation_requirements': 'ModelState validation.', 'technology_integration_details': 'ASP.NET Core Binding.'}

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes

Endpoints must be publicly accessible.

### 2.3.5.0.0.0.0 Interface Specifications

- {'interface_name': 'ISubscriptionService', 'file_path': 'src/ReadTrack.Monetization.Application/Services/ISubscriptionService.cs', 'purpose': 'Public interface for other modules to query subscription state.', 'generic_constraints': 'None', 'framework_specific_inheritance': 'None', 'method_contracts': [{'method_name': 'GetUserSubscriptionStatusAsync', 'method_signature': 'Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId)', 'return_type': 'Task<SubscriptionStatusDto>', 'framework_attributes': [], 'parameters': [{'parameter_name': 'userId', 'parameter_type': 'Guid', 'purpose': 'User ID.'}], 'contract_description': 'Returns current tier and expiration.', 'exception_contracts': 'None (Returns Free tier on error).'}], 'property_contracts': [], 'implementation_guidance': 'Should use caching.', 'validation_notes': 'Exposed to Reading/Recommendations modules.'}

### 2.3.6.0.0.0.0 Enum Specifications

- {'enum_name': 'SubscriptionTier', 'file_path': 'src/ReadTrack.Monetization.Domain/ValueObjects/SubscriptionTier.cs', 'underlying_type': 'int', 'purpose': 'Defines access levels.', 'framework_attributes': [], 'values': [{'value_name': 'Free', 'value': '0', 'description': 'Default.'}, {'value_name': 'Premium', 'value': '1', 'description': 'Paid.'}], 'validation_notes': 'Matched to User module definitions.'}

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'SubscriptionStatusDto', 'file_path': 'src/ReadTrack.Monetization.Application/DTOs/SubscriptionStatusDto.cs', 'purpose': 'Transport object for subscription status.', 'framework_base_class': 'record', 'properties': [{'property_name': 'Tier', 'property_type': 'SubscriptionTier', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'ShowAds', 'property_type': 'bool', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'None', 'serialization_requirements': 'JSON', 'validation_notes': 'Immutable record.'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'AppleStoreSettings', 'file_path': 'src/ReadTrack.Monetization.Infrastructure/Configuration/AppleStoreSettings.cs', 'purpose': 'Configuration for Apple API.', 'framework_base_class': 'class', 'configuration_sections': [{'section_name': 'Monetization:Apple', 'properties': [{'property_name': 'BundleId', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'App Bundle ID.'}, {'property_name': 'AppleRootCertificate', 'property_type': 'string', 'default_value': '', 'required': 'true', 'description': 'Cert content or path.'}]}], 'validation_requirements': 'Validated on startup.', 'validation_notes': 'Secrets managed via Secrets Manager.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IPaymentProviderService

##### 2.3.9.1.2.0.0 Service Implementation

AppleStoreClient

##### 2.3.9.1.3.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0 Registration Reasoning

Standard service lifetime.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddScoped<IPaymentProviderService, AppleStoreClient>() (Keyed registration recommended)

##### 2.3.9.1.6.0.0 Validation Notes

Factory resolves based on provider type.

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

ISubscriptionService

##### 2.3.9.2.2.0.0 Service Implementation

SubscriptionService

##### 2.3.9.2.3.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0 Registration Reasoning

Used by other modules.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddScoped<ISubscriptionService, SubscriptionService>()

##### 2.3.9.2.6.0.0 Validation Notes

Public API.

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

Apple App Store Server API

##### 2.3.10.1.2.0.0 Integration Type

Webhook

##### 2.3.10.1.3.0.0 Required Client Classes

- AppleStoreClient
- HttpClient

##### 2.3.10.1.4.0.0 Configuration Requirements

AppleStoreSettings

##### 2.3.10.1.5.0.0 Error Handling Requirements

Signature validation failure must log security event.

##### 2.3.10.1.6.0.0 Authentication Requirements

JWS Signature Verification.

##### 2.3.10.1.7.0.0 Framework Integration Patterns

Strategy Pattern

##### 2.3.10.1.8.0.0 Validation Notes

Critical security integration.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Hangfire

##### 2.3.10.2.2.0.0 Integration Type

Background Job

##### 2.3.10.2.3.0.0 Required Client Classes

- SubscriptionExpirationJob

##### 2.3.10.2.4.0.0 Configuration Requirements

Hangfire DB connection.

##### 2.3.10.2.5.0.0 Error Handling Requirements

Retry logic built-in.

##### 2.3.10.2.6.0.0 Authentication Requirements

None.

##### 2.3.10.2.7.0.0 Framework Integration Patterns

BackgroundService

##### 2.3.10.2.8.0.0 Validation Notes

Runs daily cleanup.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 12 |
| Total Interfaces | 4 |
| Total Enums | 2 |
| Total Dtos | 3 |
| Total Configurations | 2 |
| Total External Integrations | 2 |
| Grand Total Components | 29 |
| Phase 2 Claimed Count | 29 |
| Phase 2 Actual Count | 29 |
| Validation Added Count | 0 |
| Final Validated Count | 29 |

