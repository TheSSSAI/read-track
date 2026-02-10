# 1 Analysis Metadata

| Property | Value |
|----------|-------|
| Analysis Timestamp | 2025-01-24T12:00:00Z |
| Repository Component Id | readtrack-monetization-module |
| Analysis Completeness Score | 100 |
| Critical Findings Count | 4 |
| Analysis Methodology | Systematic Context-Driven Decomposition with .NET ... |

# 2 Repository Analysis

## 2.1 Repository Definition

### 2.1.1 Scope Boundaries

- Subscription Lifecycle Management (Purchase, Renewal, Cancellation, Expiration)
- Third-Party Payment Provider Integration (Apple App Store, Google Play Store Webhooks)
- Freemium Business Logic Enforcement (Feature Gating Rules)
- Advertisement Policy Management (Ad serving criteria for Free Users)

### 2.1.2 Technology Stack

- .NET 8 (ASP.NET Core)
- Entity Framework Core 8
- MediatR (CQRS)
- FluentValidation
- PostgreSQL (via Npgsql)

### 2.1.3 Architectural Constraints

- Must utilize Clean Architecture with strict separation of Domain, Application, and Infrastructure layers
- Must process external webhooks idempotently to ensure data consistency
- Must operate within a Modular Monolith, interacting with other modules via in-process interfaces
- Data persistence must be isolated to the monetization schema

### 2.1.4 Dependency Relationships

#### 2.1.4.1 In-Process Module Integration: REPO-BE-MOD-USERS

##### 2.1.4.1.1 Dependency Type

In-Process Module Integration

##### 2.1.4.1.2 Target Component

REPO-BE-MOD-USERS

##### 2.1.4.1.3 Integration Pattern

Direct Service Call (via DI)

##### 2.1.4.1.4 Reasoning

Monetization module must update User entities (SubscriptionTier) when subscription status changes via IUserService.

#### 2.1.4.2.0 External API Dependency: Apple App Store Server API

##### 2.1.4.2.1 Dependency Type

External API Dependency

##### 2.1.4.2.2 Target Component

Apple App Store Server API

##### 2.1.4.2.3 Integration Pattern

Webhook & API Polling

##### 2.1.4.2.4 Reasoning

Required to validate receipts and receive server-to-server notifications for iOS subscriptions.

#### 2.1.4.3.0 External API Dependency: Google Play Developer API

##### 2.1.4.3.1 Dependency Type

External API Dependency

##### 2.1.4.3.2 Target Component

Google Play Developer API

##### 2.1.4.3.3 Integration Pattern

Webhook (Pub/Sub) & API Polling

##### 2.1.4.3.4 Reasoning

Required to validate purchase tokens and receive real-time developer notifications for Android subscriptions.

### 2.1.5.0.0 Analysis Insights

This repository acts as the financial engine of the application. It decouples the complex, volatile logic of third-party payment providers from the core application domain. The use of .NET 8 features like 'record' types for immutable webhook payloads and 'async/await' for IO-bound verification is critical for performance and maintainability.

# 3.0.0.0.0 Requirements Mapping

## 3.1.0.0.0 Functional Requirements

### 3.1.1.0.0 Requirement Id

#### 3.1.1.1.0 Requirement Id

REQ-FRE-001

#### 3.1.1.2.0 Requirement Description

Enforce freemium model limits and premium tier upgrades.

#### 3.1.1.3.0 Implementation Implications

- Domain Logic: Define SubscriptionTier enum (Free, Premium) and validation rules.
- Application Layer: Implement commands to upgrade/downgrade user tier based on payment events.

#### 3.1.1.4.0 Required Components

- SubscriptionService
- TierUpgradeCommandHandler

#### 3.1.1.5.0 Analysis Reasoning

This is the core responsibility of the module, requiring a robust state machine to transition users between tiers based on subscription validity.

### 3.1.2.0.0 Requirement Id

#### 3.1.2.1.0 Requirement Id

REQ-ADS-001

#### 3.1.2.2.0 Requirement Description

Serve advertisements to Free Users based on usage rules.

#### 3.1.2.3.0 Implementation Implications

- Domain Logic: Define AdPolicy (e.g., interstitial frequency).
- Application Layer: Provide query endpoints for clients to check ad eligibility.

#### 3.1.2.4.0 Required Components

- AdPolicyEvaluator
- GetAdConfigQueryHandler

#### 3.1.2.5.0 Analysis Reasoning

The module must provide the 'rules' for ads, even if the client handles the display. It centralizes the monetization strategy.

### 3.1.3.0.0 Requirement Id

#### 3.1.3.1.0 Requirement Id

REQ-FUNC-002

#### 3.1.3.2.0 Requirement Description

Automatically revert Premium account to Free upon expiration.

#### 3.1.3.3.0 Implementation Implications

- Background Job: Scheduled task to identify and downgrade expired subscriptions.
- Event Publishing: Emit SubscriptionExpiredEvent for other modules.

#### 3.1.3.4.0 Required Components

- SubscriptionStatusService
- ProcessExpiredSubscriptionsJob

#### 3.1.3.5.0 Analysis Reasoning

Critical for revenue assurance and enforcing the business model. Requires a resilient background process (Hangfire) as a fallback to webhooks.

## 3.2.0.0.0 Non Functional Requirements

### 3.2.1.0.0 Requirement Type

#### 3.2.1.1.0 Requirement Type

Reliability

#### 3.2.1.2.0 Requirement Specification

Handle webhook failures gracefully.

#### 3.2.1.3.0 Implementation Impact

Must implement idempotency checks and retries for webhook processing.

#### 3.2.1.4.0 Design Constraints

- Store all raw webhook payloads before processing
- Use transactional updates

#### 3.2.1.5.0 Analysis Reasoning

Payment providers (Apple/Google) retry webhooks; the system must not double-credit or incorrectly cancel subscriptions due to duplicate events.

### 3.2.2.0.0 Requirement Type

#### 3.2.2.1.0 Requirement Type

Security

#### 3.2.2.2.0 Requirement Specification

Secure validation of purchase receipts.

#### 3.2.2.3.0 Implementation Impact

Integration with Apple/Google public keys/APIs to verify signatures.

#### 3.2.2.4.0 Design Constraints

- No client-side trust for payment status
- Strict validation of JWTs from Apple

#### 3.2.2.5.0 Analysis Reasoning

Prevents fraud where clients might spoof receipt data.

## 3.3.0.0.0 Requirements Analysis Summary

The module is heavily driven by event-based requirements (webhooks) and state transitions (tier changes). High reliability and security NFRs dictate a design that prioritizes data integrity over raw throughput.

# 4.0.0.0.0 Architecture Analysis

## 4.1.0.0.0 Architectural Patterns

### 4.1.1.0.0 Pattern Name

#### 4.1.1.1.0 Pattern Name

CQRS (Command Query Responsibility Segregation)

#### 4.1.1.2.0 Pattern Application

Separating subscription state mutations (Webhooks/Purchases) from status checks.

#### 4.1.1.3.0 Required Components

- MediatR
- Commands/ProcessWebhook
- Queries/GetSubscriptionStatus

#### 4.1.1.4.0 Implementation Strategy

Use MediatR IRequest/IRequestHandler interfaces. Commands handle complex logic and side effects; Queries return DTOs directly.

#### 4.1.1.5.0 Analysis Reasoning

.NET 8 optimization: Allows independent scaling of read/write logic and cleaner separation of complex webhook processing code.

### 4.1.2.0.0 Pattern Name

#### 4.1.2.1.0 Pattern Name

Domain-Driven Design (DDD)

#### 4.1.2.2.0 Pattern Application

Encapsulating subscription logic in the Domain layer.

#### 4.1.2.3.0 Required Components

- Subscription Aggregate
- PaymentTransaction Entity
- SubscriptionService

#### 4.1.2.4.0 Implementation Strategy

Rich domain models that enforce invariants (e.g., 'Cannot cancel an already expired subscription').

#### 4.1.2.5.0 Analysis Reasoning

Essential for managing the complexity of subscription lifecycles without bleeding business rules into controllers.

### 4.1.3.0.0 Pattern Name

#### 4.1.3.1.0 Pattern Name

Outbox Pattern / Domain Events

#### 4.1.3.2.0 Pattern Application

Reliable messaging to other modules (e.g., Users Module).

#### 4.1.3.3.0 Required Components

- IDomainEvent
- SubscriptionTerminatedEvent

#### 4.1.3.4.0 Implementation Strategy

Publish events via MediatR notifications after successful transaction commit.

#### 4.1.3.5.0 Analysis Reasoning

Ensures the User module is eventually consistent with the Monetization module without tight coupling.

## 4.2.0.0.0 Integration Points

### 4.2.1.0.0 Integration Type

#### 4.2.1.1.0 Integration Type

Inbound Webhook

#### 4.2.1.2.0 Target Components

- Apple App Store
- Google Play Store

#### 4.2.1.3.0 Communication Pattern

Asynchronous Push

#### 4.2.1.4.0 Interface Requirements

- JSON Payload Parsing
- Signature Verification

#### 4.2.1.5.0 Analysis Reasoning

The primary source of truth for subscription state changes.

### 4.2.2.0.0 Integration Type

#### 4.2.2.1.0 Integration Type

Internal Module Call

#### 4.2.2.2.0 Target Components

- REPO-BE-MOD-USERS

#### 4.2.2.3.0 Communication Pattern

Synchronous In-Process (Method Call)

#### 4.2.2.4.0 Interface Requirements

- IUserService Interface

#### 4.2.2.5.0 Analysis Reasoning

Needed to propagate the 'Premium' status to the User entity for access control checks across the app.

## 4.3.0.0.0 Layering Strategy

| Property | Value |
|----------|-------|
| Layer Organization | Clean Architecture: Domain -> Application -> Infra... |
| Component Placement | Entities in Domain, Use Cases in Application, EF C... |
| Analysis Reasoning | Standard .NET 8 enterprise pattern ensuring testab... |

# 5.0.0.0.0 Database Analysis

## 5.1.0.0.0 Entity Mappings

### 5.1.1.0.0 Entity Name

#### 5.1.1.1.0 Entity Name

Subscription

#### 5.1.1.2.0 Database Table

Subscriptions

#### 5.1.1.3.0 Required Properties

- SubscriptionId (PK)
- UserId (FK)
- Provider (Enum: Apple/Google)
- ExternalSubscriptionId
- Status
- CurrentPeriodEnd
- Tier

#### 5.1.1.4.0 Relationship Mappings

- One-to-Many with PaymentTransactions

#### 5.1.1.5.0 Access Patterns

- Query by ExternalSubscriptionId (Webhook lookup)
- Query by UserId (Status check)
- Query by ExpirationDate (Background job)

#### 5.1.1.6.0 Analysis Reasoning

Central entity tracking the user's current standing. Indexed by ExternalId for fast webhook processing.

### 5.1.2.0.0 Entity Name

#### 5.1.2.1.0 Entity Name

PaymentTransaction

#### 5.1.2.2.0 Database Table

PaymentTransactions

#### 5.1.2.3.0 Required Properties

- TransactionId (PK)
- SubscriptionId (FK)
- ExternalTransactionId
- Amount
- Currency
- Type (Purchase/Renewal/Refund)
- ProcessedAt

#### 5.1.2.4.0 Relationship Mappings

- Many-to-One with Subscription

#### 5.1.2.5.0 Access Patterns

- Append-only log of all financial events

#### 5.1.2.6.0 Analysis Reasoning

Audit trail for financial reporting and support debugging. Immutable record.

## 5.2.0.0.0 Data Access Requirements

### 5.2.1.0.0 Operation Type

#### 5.2.1.1.0 Operation Type

Transactional Write

#### 5.2.1.2.0 Required Methods

- UpdateSubscriptionAndLogTransaction

#### 5.2.1.3.0 Performance Constraints

High integrity required; isolation level ReadCommitted or higher.

#### 5.2.1.4.0 Analysis Reasoning

Updating a subscription status and logging the transaction must happen atomically to prevent data discrepancies.

### 5.2.2.0.0 Operation Type

#### 5.2.2.1.0 Operation Type

Batch Read

#### 5.2.2.2.0 Required Methods

- GetExpiredSubscriptions

#### 5.2.2.3.0 Performance Constraints

Optimized for large datasets; index on CurrentPeriodEnd.

#### 5.2.2.4.0 Analysis Reasoning

The background job must efficiently find thousands of expired subscriptions without table scanning.

## 5.3.0.0.0 Persistence Strategy

| Property | Value |
|----------|-------|
| Orm Configuration | Entity Framework Core 8 with Code-First Migrations... |
| Migration Requirements | Separate migration history for the Monetization mo... |
| Analysis Reasoning | Allows the module to evolve its schema independent... |

# 6.0.0.0.0 Sequence Analysis

## 6.1.0.0.0 Interaction Patterns

### 6.1.1.0.0 Sequence Name

#### 6.1.1.1.0 Sequence Name

Process Subscription Webhook

#### 6.1.1.2.0 Repository Role

Primary Processor

#### 6.1.1.3.0 Required Interfaces

- IWebhookValidator
- IUserService

#### 6.1.1.4.0 Method Specifications

##### 6.1.1.4.1 Method Name

###### 6.1.1.4.1.1 Method Name

Handle(ProcessWebhookCommand command)

###### 6.1.1.4.1.2 Interaction Context

Invoked by MediatR when controller receives webhook.

###### 6.1.1.4.1.3 Parameter Analysis

Raw JSON payload, Provider type.

###### 6.1.1.4.1.4 Return Type Analysis

Result<Unit> (Success/Failure).

###### 6.1.1.4.1.5 Analysis Reasoning

Orchestrates validation, parsing, database update, and user tier update.

##### 6.1.1.4.2.0 Method Name

###### 6.1.1.4.2.1 Method Name

UpdateUserTierAsync(Guid userId, SubscriptionTier tier)

###### 6.1.1.4.2.2 Interaction Context

Called after successful subscription update.

###### 6.1.1.4.2.3 Parameter Analysis

UserId, New Tier.

###### 6.1.1.4.2.4 Return Type Analysis

Task.

###### 6.1.1.4.2.5 Analysis Reasoning

Propagates the change to the Users module.

#### 6.1.1.5.0.0 Analysis Reasoning

Implements Sequence 453 and 454. Decouples the HTTP request from the business logic.

### 6.1.2.0.0.0 Sequence Name

#### 6.1.2.1.0.0 Sequence Name

Get User Ad Config

#### 6.1.2.2.0.0 Repository Role

Policy Enforcer

#### 6.1.2.3.0.0 Required Interfaces

- IReadRepository<Subscription>

#### 6.1.2.4.0.0 Method Specifications

- {'method_name': 'Handle(GetAdConfigQuery query)', 'interaction_context': 'Invoked when client requests ad rules.', 'parameter_analysis': 'UserId.', 'return_type_analysis': 'AdConfigDto (ShowAds: bool, InterstitialInterval: int).', 'analysis_reasoning': "Centralizes ad logic (REQ-ADS-001) so clients don't hardcode rules."}

#### 6.1.2.5.0.0 Analysis Reasoning

Supports US-026 and US-023. Determines if ads should be shown based on subscription status.

## 6.2.0.0.0.0 Communication Protocols

- {'protocol_type': 'In-Process Direct Method Call', 'implementation_requirements': 'Direct injection of IUserService interface.', 'analysis_reasoning': 'Low latency requirement for updating user status during webhook processing.'}

# 7.0.0.0.0.0 Critical Analysis Findings

## 7.1.0.0.0.0 Finding Category

### 7.1.1.0.0.0 Finding Category

Architectural Risk

### 7.1.2.0.0.0 Finding Description

Webhook Idempotency is Critical.

### 7.1.3.0.0.0 Implementation Impact

Must implement a mechanism to track processed ExternalTransactionIds to prevent duplicate processing if Apple/Google retries requests.

### 7.1.4.0.0.0 Priority Level

High

### 7.1.5.0.0.0 Analysis Reasoning

Duplicate processing could extend subscriptions incorrectly or trigger duplicate user notifications.

## 7.2.0.0.0.0 Finding Category

### 7.2.1.0.0.0 Finding Category

Data Consistency

### 7.2.2.0.0.0 Finding Description

Synchronization between Subscription and User Tier.

### 7.2.3.0.0.0 Implementation Impact

If the call to IUserService fails after the subscription updates, the data will be out of sync.

### 7.2.4.0.0.0 Priority Level

High

### 7.2.5.0.0.0 Analysis Reasoning

Requires a transactional approach or an Outbox pattern/Eventual Consistency mechanism to ensure the User module *always* receives the update.

## 7.3.0.0.0.0 Finding Category

### 7.3.1.0.0.0 Finding Category

Security Vulnerability

### 7.3.2.0.0.0 Finding Description

Webhook Signature Verification.

### 7.3.3.0.0.0 Implementation Impact

Must implement strict verification of JWT signatures (Apple) and Pub/Sub tokens (Google).

### 7.3.4.0.0.0 Priority Level

High

### 7.3.5.0.0.0 Analysis Reasoning

Failure to verify allows attackers to spoof purchases and gain premium access for free.

## 7.4.0.0.0.0 Finding Category

### 7.4.1.0.0.0 Finding Category

Dependency Management

### 7.4.2.0.0.0 Finding Description

External API Changes.

### 7.4.3.0.0.0 Implementation Impact

Apple and Google APIs change frequently. The repository must use abstraction layers for these providers.

### 7.4.4.0.0.0 Priority Level

Medium

### 7.4.5.0.0.0 Analysis Reasoning

Isolating vendor-specific logic protects the core domain from breaking changes.

# 8.0.0.0.0.0 Analysis Traceability

## 8.1.0.0.0.0 Cached Context Utilization

Utilized Sequences 453, 454, 466; Requirements REQ-FRE-001, REQ-ADS-001; Database Schema for Subscription/Transaction.

## 8.2.0.0.0.0 Analysis Decision Trail

- Identified as Business Logic repo based on description.
- Mapped to .NET 8 Clean Architecture structure.
- Derived entities from Payment Flows.
- Determined integration patterns from Architecture Map.

## 8.3.0.0.0.0 Assumption Validations

- Assumed 'subscription-job-processor-006' is part of this repository's logical scope (Application layer background service).
- Assumed EF Core 8 is the ORM based on tech stack definition.

## 8.4.0.0.0.0 Cross Reference Checks

- Verified against Sequence 454 for expiration logic.
- Checked REQ-ADS-001 for ad serving rule requirements.

