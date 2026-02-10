# 1 Id

REPO-BE-MOD-MONETIZATION

# 2 Name

readtrack-monetization-module

# 3 Description

This repository is a dedicated business module focused on all monetization aspects of the application. It was decomposed from the `readtrack-backend-api` to isolate the logic for the freemium subscription model (REQ-FRE-001), feature gating based on user tier (Free vs. Premium), and advertisement serving rules (REQ-ADS-001). It also contains the webhook endpoints for processing server-to-server notifications from the Apple App Store and Google Play Store for subscription lifecycle events (purchases, renewals, cancellations). This separation allows the monetization strategy to evolve independently of the core product features and centralizes the complex, provider-specific payment integration logic.

# 4 Type

🔹 Business Logic

# 5 Namespace

ReadTrack.Monetization

# 6 Output Path

solution/backend/modules/monetization

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

ASP.NET Core 8, Entity Framework Core 8

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- application
- domain
- infrastructure

# 12 Dependencies

- REPO-BE-LIB-CONTRACTS
- REPO-BE-LIB-INFRA
- REPO-BE-MOD-USERS

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-FRE-001

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-ADS-001

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Clean Architecture Slice

# 17.0.0 Architecture Map

- application-layer-010

# 18.0.0 Components Map

- backend-subscription-service-005
- subscription-job-processor-006

# 19.0.0 Requirements Map

- REQ-FRE-001

# 20.0.0 Decomposition Rationale

## 20.1.0 Operation Type

NEW_DECOMPOSED

## 20.2.0 Source Repository

REPO-BE-API

## 20.3.0 Decomposition Reasoning

Monetization logic is a distinct and complex domain with its own external dependencies (payment providers) and business rules. Isolating it ensures that changes to pricing, subscription plans, or ad logic do not risk destabilizing core application functionality. It also allows for specialized development focus on the critical revenue-generating aspects of the system.

## 20.4.0 Extracted Responsibilities

- Subscription Lifecycle Management
- Payment Provider Webhook Processing
- Feature Access Control (Gating)
- Advertisement Serving Logic

## 20.5.0 Reusability Scope

- The core subscription status management logic could be adapted for other SaaS products.

## 20.6.0 Development Benefits

- Enables focused development on revenue-critical features.
- Isolates payment provider integration complexity.
- Simplifies auditing of financial and subscription-related code.

# 21.0.0 Dependency Contracts

## 21.1.0 Repo-Be-Mod-Users

### 21.1.1 Required Interfaces

- {'interface': 'IUserService', 'methods': ['UpdateUserTierAsync(Guid userId, SubscriptionTier tier)'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Direct Service Call (via DI)

### 21.1.3 Communication Protocol

In-process

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'ISubscriptionService', 'methods': ['GetUserSubscriptionStatusAsync(Guid userId) : SubscriptionStatusDto'], 'events': ['SubscriptionTierChanged(Guid userId, SubscriptionTier oldTier, SubscriptionTier newTier)'], 'properties': [], 'consumers': ['REPO-BE-MOD-READING', 'REPO-BE-MOD-RECOMMENDATIONS']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Provides an `ISubscriptionService` that other modu... |
| Event Communication | Listens for webhook events from payment providers.... |
| Data Flow | Owns the `Subscription` table and manages its stat... |
| Error Handling | Robust error handling and logging for webhook proc... |
| Async Patterns | Webhook handlers and subscription status update jo... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Implement webhook endpoints as minimal API endpoin... |
| Performance Considerations | Webhook endpoints must be highly available and res... |
| Security Considerations | Webhook endpoints must be secured and validate the... |
| Testing Approach | Requires extensive integration testing using mock ... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- All logic related to subscriptions, payments, and feature gating.
- Handling of App Store and Google Play webhooks.

## 25.2.0 Must Not Implement

- User profile management.
- Core reading tracking functionality.
- Any UI-related logic.

## 25.3.0 Extension Points

- Adding new subscription plans or tiers.
- Integrating with new payment providers.

## 25.4.0 Validation Rules

- Validate incoming webhook payloads.
- Enforce feature limits based on the user's current subscription tier.

