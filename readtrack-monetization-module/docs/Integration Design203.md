# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-MONETIZATION |
| Extraction Timestamp | 2025-10-27T10:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-FRE-001

#### 1.2.1.2 Requirement Text

The system shall support a Freemium business model with distinct 'Free' and 'Premium' user tiers, enforcing feature gates and limitations based on the active subscription status.

#### 1.2.1.3 Validation Criteria

- Verify distinct database states for Free and Premium users
- Validate that premium features are inaccessible without an active subscription
- Ensure subscription status is authoritatively determined by backend validation of payment provider receipts

#### 1.2.1.4 Implementation Implications

- Domain entity 'Subscription' must track status, tier, expiry date, and provider transaction IDs
- Application service must provide synchronous status checks for other modules
- Webhook handlers must be idempotent to handle duplicate events from Apple/Google

#### 1.2.1.5 Extraction Reasoning

Explicitly mapped in repository definition and critical for the module's core existence.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-ADS-001

#### 1.2.2.2 Requirement Text

The system shall manage advertisement serving rules, ensuring ads are displayed to Free users and suppressed for Premium users.

#### 1.2.2.3 Validation Criteria

- Verify ad serving logic returns true for Free tier
- Verify ad serving logic returns false for Premium tier
- Ensure ad suppression is immediate upon subscription activation

#### 1.2.2.4 Implementation Implications

- Logic to determine 'ShowAds' flag based on valid subscription
- Integration with user context to provide ad status to the client app

#### 1.2.2.5 Extraction Reasoning

Explicitly mapped in repository definition as a responsibility of the monetization module.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUNC-002

#### 1.2.3.2 Requirement Text

The system shall automatically revert a 'Premium User' account to the 'Free User' tier upon subscription termination.

#### 1.2.3.3 Validation Criteria

- Verify automatic downgrade upon receipt of expiration/cancellation webhook
- Verify scheduled job identifies and downgrades expired subscriptions not caught by webhooks

#### 1.2.3.4 Implementation Implications

- Implementation of background job processor (Hangfire) for expiration checks
- Event publication (SubscriptionTerminated) to notify Users module of role change

#### 1.2.3.5 Extraction Reasoning

Implied by the 'Subscription Lifecycle Management' responsibility and Sequence Diagram 454.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

SubscriptionService

#### 1.3.1.2 Component Specification

Core domain service managing subscription entities, status validation, and tier determination logic.

#### 1.3.1.3 Implementation Requirements

- Implement ISubscriptionService interface
- Encapsulate logic for determining active status based on dates and grace periods
- Manage persistence of subscription records via EF Core

#### 1.3.1.4 Architectural Context

Backend.Application / Backend.Domain

#### 1.3.1.5 Extraction Reasoning

Mapped from 'backend-subscription-service-005' and required to fulfill REQ-FRE-001.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

WebhookController

#### 1.3.2.2 Component Specification

API controller exposing public endpoints to receive and process server-to-server notifications from payment providers.

#### 1.3.2.3 Implementation Requirements

- Endpoints for POST /api/v1/webhooks/apple and /google
- Signature validation middleware to verify payload authenticity
- Idempotency checks using transaction IDs

#### 1.3.2.4 Architectural Context

Backend.Presentation (API Layer)

#### 1.3.2.5 Extraction Reasoning

Identified in Sequence Diagram 453 and repository description for processing external payment events.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

SubscriptionJobProcessor

#### 1.3.3.2 Component Specification

Background worker responsible for asynchronous tasks such as daily expiration checks and processing queued webhook events.

#### 1.3.3.3 Implementation Requirements

- Integration with Hangfire for job scheduling
- Logic to query expired subscriptions and trigger downgrade events
- Retry policies for failed webhook processing

#### 1.3.3.4 Architectural Context

Backend.Infrastructure (Background Jobs)

#### 1.3.3.5 Extraction Reasoning

Mapped from 'subscription-job-processor-006' and Sequence Diagram 454.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Monetization.Domain

#### 1.4.1.2 Layer Responsibilities

Defines Subscription entities, value objects (Tier, Provider), and domain events (SubscriptionRenewed, SubscriptionExpired).

#### 1.4.1.3 Layer Constraints

- No dependencies on Application or Infrastructure layers
- Pure C# logic

#### 1.4.1.4 Implementation Patterns

- Rich Domain Model
- Domain Events

#### 1.4.1.5 Extraction Reasoning

Standard Clean Architecture layer for core business logic.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Monetization.Application

#### 1.4.2.2 Layer Responsibilities

Orchestrates subscription flows, handles commands (ProcessWebhook), and queries (GetStatus).

#### 1.4.2.3 Layer Constraints

- Depends only on Domain
- Uses MediatR for CQRS

#### 1.4.2.4 Implementation Patterns

- CQRS (Commands/Queries)
- Dependency Injection

#### 1.4.2.5 Extraction Reasoning

Standard Clean Architecture layer for use case implementation.

### 1.4.3.0 Layer Name

#### 1.4.3.1 Layer Name

Monetization.Infrastructure

#### 1.4.3.2 Layer Responsibilities

Implements repositories, external payment provider clients, and background jobs.

#### 1.4.3.3 Layer Constraints

- Depends on Application and Domain
- Handles external I/O

#### 1.4.3.4 Implementation Patterns

- Repository Pattern (EF Core)
- Adapter Pattern (External APIs)

#### 1.4.3.5 Extraction Reasoning

Standard Clean Architecture layer for external concerns.

## 1.5.0.0 Dependency Interfaces

- {'interface_name': 'IUserService', 'source_repository': 'REPO-BE-MOD-USERS', 'method_contracts': [{'method_name': 'UpdateUserTierAsync', 'method_signature': 'Task UpdateUserTierAsync(Guid userId, SubscriptionTier tier)', 'method_purpose': "Updates the user's role/claims in the Identity system when subscription status changes.", 'integration_context': 'Called within the ProcessWebhook command handler or SubscriptionTerminated event handler.'}], 'integration_pattern': 'Direct Service Call (Module-to-Module)', 'communication_protocol': 'In-process Method Call', 'extraction_reasoning': "Explicitly defined in 'dependency_contracts' and required for Sequence 454."}

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

ISubscriptionService

#### 1.6.1.2 Consumer Repositories

- REPO-BE-MOD-READING
- REPO-BE-MOD-RECOMMENDATIONS

#### 1.6.1.3 Method Contracts

- {'method_name': 'GetUserSubscriptionStatusAsync', 'method_signature': 'Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId)', 'method_purpose': 'Returns the current subscription tier and expiry for a user. Used for feature gating.', 'implementation_requirements': 'Must be high-performance, potentially cached.'}

#### 1.6.1.4 Service Level Requirements

- Sub-10ms response time for cached checks
- High availability for feature gating

#### 1.6.1.5 Implementation Constraints

- Must assume 'Free' on error (fail-safe)

#### 1.6.1.6 Extraction Reasoning

Defined in 'exposed_contracts' to allow other modules to check feature access.

### 1.6.2.0 Interface Name

#### 1.6.2.1 Interface Name

Webhook Endpoints

#### 1.6.2.2 Consumer Repositories

- Apple App Store
- Google Play Store

#### 1.6.2.3 Method Contracts

##### 1.6.2.3.1 Method Name

###### 1.6.2.3.1.1 Method Name

ProcessAppleWebhook

###### 1.6.2.3.1.2 Method Signature

POST /api/v1/webhooks/apple

###### 1.6.2.3.1.3 Method Purpose

Receives subscription lifecycle events from Apple.

###### 1.6.2.3.1.4 Implementation Requirements

Validate JWS signature, parse notificationType.

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

ProcessGoogleWebhook

###### 1.6.2.3.2.2 Method Signature

POST /api/v1/webhooks/google

###### 1.6.2.3.2.3 Method Purpose

Receives subscription lifecycle events from Google.

###### 1.6.2.3.2.4 Implementation Requirements

Decode Base64 message, validate via Google Publisher API.

#### 1.6.2.4.0.0 Service Level Requirements

- 200 OK response within 200ms to provider

#### 1.6.2.5.0.0 Implementation Constraints

- Must handle duplicates (idempotency)
- Security: Validate signatures

#### 1.6.2.6.0.0 Extraction Reasoning

Implicit interface required for subscription integration (Sequence 453).

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8, ASP.NET Core 8, Entity Framework Core 8

### 1.7.2.0.0.0 Integration Technologies

- MediatR (In-process events)
- Hangfire (Background Jobs)
- Apple StoreKit API
- Google Play Developer API

### 1.7.3.0.0.0 Performance Constraints

Webhook endpoints must respond immediately. Complex processing should be offloaded to background queues.

### 1.7.4.0.0.0 Security Requirements

Strict validation of webhook signatures using platform-specific secrets. Secure storage of transaction receipts.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All requirements (FRE-001, ADS-001) and sequences ... |
| Cross Reference Validation | Validated dependencies against REPO-BE-MOD-USERS d... |
| Implementation Readiness Assessment | High. Clear interface definitions, database respon... |
| Quality Assurance Confirmation | Systematic review confirms all repository-specific... |

