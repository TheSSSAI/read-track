# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-USERS |
| Extraction Timestamp | 2025-01-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-USR-001

#### 1.2.1.2 Requirement Text

User Management and Authentication compliance

#### 1.2.1.3 Validation Criteria

- Secure user registration via social providers (Google/Apple)
- Profile management capabilities
- Compliance with GDPR Right to Erasure

#### 1.2.1.4 Implementation Implications

- Integrate with Auth0 for OIDC identity verification
- Implement account deletion logic that triggers cascading data removal via events
- Expose API endpoints for profile updates

#### 1.2.1.5 Extraction Reasoning

Core domain responsibility of this module.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-FUNC-009

#### 1.2.2.2 Requirement Text

The system shall provide a user-initiated mechanism to export their personal data in a machine-readable format.

#### 1.2.2.3 Validation Criteria

- Export process is asynchronous
- Data is aggregated from all modules
- Secure download link sent via email

#### 1.2.2.4 Implementation Implications

- Implement background job handler for data aggregation
- Integrate with S3 for secure storage
- Integrate with SES for email delivery

#### 1.2.2.5 Extraction Reasoning

Module specific requirement for data portability.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUNC-002

#### 1.2.3.2 Requirement Text

The system shall automatically revert a 'Premium User' account to the 'Free User' tier upon subscription termination.

#### 1.2.3.3 Validation Criteria

- User role in database updates immediately upon signal from Monetization module

#### 1.2.3.4 Implementation Implications

- Expose internal interface IUserService.UpdateTierAsync for Monetization module consumption

#### 1.2.3.5 Extraction Reasoning

Cross-module integration requirement.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

UsersController

#### 1.3.1.2 Component Specification

ASP.NET Core API Controller managing HTTP requests for user profile, settings, and compliance actions.

#### 1.3.1.3 Implementation Requirements

- Map HTTP requests to MediatR Commands/Queries
- Enforce [Authorize] attributes on all endpoints
- Return Standardized API Responses

#### 1.3.1.4 Architectural Context

Presentation Layer

#### 1.3.1.5 Extraction Reasoning

Entry point for client applications.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

Auth0IdentityService

#### 1.3.2.2 Component Specification

Infrastructure adapter wrapping Auth0 Management API interactions.

#### 1.3.2.3 Implementation Requirements

- Validate ID Tokens from social providers
- Sync Auth0 user profile updates with local User entity
- Handle token refresh requests

#### 1.3.2.4 Architectural Context

Infrastructure Layer / Identity Gateway

#### 1.3.2.5 Extraction Reasoning

Critical dependency isolation for Auth0 integration.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

DataExportJobHandler

#### 1.3.3.2 Component Specification

Background job processor creating JSON exports of user data.

#### 1.3.3.3 Implementation Requirements

- Aggregate data from local repository
- Request data from other modules via MediatR Requests
- Serialize to JSON and upload to IFileStorageService
- Trigger IEmailService to send download link

#### 1.3.3.4 Architectural Context

Application Layer / Background Processing

#### 1.3.3.5 Extraction Reasoning

Handles long-running compliance tasks.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Users.Application

#### 1.4.1.2 Layer Responsibilities

Orchestrates user-related use cases, defines interfaces for infrastructure, and manages event publishing.

#### 1.4.1.3 Layer Constraints

- Must not depend on EF Core directly
- Must not depend on HTTP Context

#### 1.4.1.4 Implementation Patterns

- CQRS
- Mediator Pattern

#### 1.4.1.5 Extraction Reasoning

Standard Clean Architecture application layer.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Users.Infrastructure

#### 1.4.2.2 Layer Responsibilities

Implements interfaces for persistence (EF Core), identity (Auth0), storage (S3), and email (SES).

#### 1.4.2.3 Layer Constraints

- Must implement Application interfaces
- Must handle external API resilience (Polly)

#### 1.4.2.4 Implementation Patterns

- Adapter Pattern
- Repository Pattern

#### 1.4.2.5 Extraction Reasoning

Standard Clean Architecture infrastructure layer.

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IBackgroundJobService

#### 1.5.1.2 Source Repository

REPO-BE-LIB-INFRA

#### 1.5.1.3 Method Contracts

- {'method_name': 'Enqueue', 'method_signature': 'string Enqueue<T>(Expression<Action<T>> methodCall)', 'method_purpose': 'Offloads data export and account deletion tasks to Hangfire.', 'integration_context': 'Called by CommandHandlers for heavy operations.'}

#### 1.5.1.4 Integration Pattern

Library Import / DI

#### 1.5.1.5 Communication Protocol

In-Process

#### 1.5.1.6 Extraction Reasoning

Uses shared infrastructure for job scheduling.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IFileStorageService

#### 1.5.2.2 Source Repository

REPO-BE-MOD-USERS (Internal Infra)

#### 1.5.2.3 Method Contracts

##### 1.5.2.3.1 Method Name

###### 1.5.2.3.1.1 Method Name

UploadAsync

###### 1.5.2.3.1.2 Method Signature

Task<string> UploadAsync(string containerName, string blobName, Stream content)

###### 1.5.2.3.1.3 Method Purpose

Uploads generated export files to secure storage.

###### 1.5.2.3.1.4 Integration Context

Called by DataExportJobHandler.

##### 1.5.2.3.2.0 Method Name

###### 1.5.2.3.2.1 Method Name

GetPresignedUrl

###### 1.5.2.3.2.2 Method Signature

string GetPresignedUrl(string blobUrl, TimeSpan expiry)

###### 1.5.2.3.2.3 Method Purpose

Generates secure temporary link for email.

###### 1.5.2.3.2.4 Integration Context

Called by DataExportJobHandler.

#### 1.5.2.4.0.0 Integration Pattern

Interface Abstraction (Adapter)

#### 1.5.2.5.0.0 Communication Protocol

AWS SDK (HTTPS)

#### 1.5.2.6.0.0 Extraction Reasoning

Abstraction over AWS S3.

## 1.6.0.0.0.0 Exposed Interfaces

### 1.6.1.0.0.0 Interface Name

#### 1.6.1.1.0.0 Interface Name

IUserService

#### 1.6.1.2.0.0 Consumer Repositories

- REPO-BE-MOD-MONETIZATION
- REPO-BE-MOD-READING

#### 1.6.1.3.0.0 Method Contracts

##### 1.6.1.3.1.0 Method Name

###### 1.6.1.3.1.1 Method Name

UpdateUserTierAsync

###### 1.6.1.3.1.2 Method Signature

Task UpdateUserTierAsync(Guid userId, SubscriptionTier tier, CancellationToken ct)

###### 1.6.1.3.1.3 Method Purpose

Allows the Monetization module to update user role upon subscription changes.

###### 1.6.1.3.1.4 Implementation Requirements

Must be idempotent and update the User entity directly.

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

GetUserProfileAsync

###### 1.6.1.3.2.2 Method Signature

Task<UserProfileDto?> GetUserProfileAsync(Guid userId, CancellationToken ct)

###### 1.6.1.3.2.3 Method Purpose

Provides user display name and avatar for UI composition in other modules.

###### 1.6.1.3.2.4 Implementation Requirements

Should use caching for performance.

#### 1.6.1.4.0.0 Service Level Requirements

- Tier updates must be strongly consistent
- Profile reads must be < 10ms (cached)

#### 1.6.1.5.0.0 Implementation Constraints

- In-process method calls via DI

#### 1.6.1.6.0.0 Extraction Reasoning

Primary contract for cross-module user state operations.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

UserAccountDeletedEvent

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-BE-MOD-READING
- REPO-BE-MOD-ENGAGEMENT
- REPO-BE-MOD-MONETIZATION

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'Publish', 'method_signature': 'public record UserAccountDeletedEvent(Guid UserId) : INotification;', 'method_purpose': 'Signals that a user has requested erasure, triggering cascading data cleanup.', 'implementation_requirements': 'Published via MediatR after transaction commit.'}

#### 1.6.2.4.0.0 Service Level Requirements

- Guaranteed delivery (via Outbox if possible)

#### 1.6.2.5.0.0 Implementation Constraints

- Asynchronous handling in consumers

#### 1.6.2.6.0.0 Extraction Reasoning

Critical for GDPR/CCPA compliance across the distributed system.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8, ASP.NET Core 8

### 1.7.2.0.0.0 Integration Technologies

- MediatR (In-Process Messaging)
- Hangfire (Background Jobs)
- Auth0 OIDC (Authentication)
- AWS SDK (S3, SES)

### 1.7.3.0.0.0 Performance Constraints

Login flow < 500ms. Export jobs must not block API threads.

### 1.7.4.0.0.0 Security Requirements

Strict PII isolation. All external storage encrypted at rest. Signed URLs for file access.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped all user management flows: Auth, Profile, E... |
| Cross Reference Validation | Verified IUserService matches usage in Monetizatio... |
| Implementation Readiness Assessment | High. Interfaces and patterns are clearly defined. |
| Quality Assurance Confirmation | Integration design adheres to modular monolith pri... |

