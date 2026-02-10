# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-LIB-CONTRACTS |
| Extraction Timestamp | 2025-01-27T12:30:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-ARCH-001

#### 1.2.1.2 Requirement Text

The system shall implement a Modular Monolith architecture with strict decoupling between business modules.

#### 1.2.1.3 Validation Criteria

- Cross-module communication relies on shared contracts rather than project references
- Events and DTOs are defined centrally to avoid circular dependencies

#### 1.2.1.4 Implementation Implications

- Define all MediatR Integration Events (e.g., ReadingSessionLogged) in this assembly
- Define shared DTOs used in public API responses here
- Define Enums used across modules (e.g., SubscriptionTier) here

#### 1.2.1.5 Extraction Reasoning

This repository acts as the 'Shared Kernel' enabling the architectural constraint of decoupled modules.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-GEN-DTO

#### 1.2.2.2 Requirement Text

Define data structures for API communication.

#### 1.2.2.3 Validation Criteria

- API responses must use a standardized envelope
- DTOs must be immutable and serialization-friendly

#### 1.2.2.4 Implementation Implications

- Implement ApiResponse<T> generic wrapper
- Use C# records for all DTOs with System.Text.Json attributes

#### 1.2.2.5 Extraction Reasoning

Standardizes the API surface area for the mobile client across all disparate backend modules.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-FUNC-002

#### 1.2.3.2 Requirement Text

The system shall automatically revert a 'Premium User' account to 'Free User' tier upon subscription termination.

#### 1.2.3.3 Validation Criteria

- Subscription lifecycle events must be broadcast to all interested modules

#### 1.2.3.4 Implementation Implications

- Define SubscriptionTerminated event implementing INotification
- Define SubscriptionTier enum to ensure consistent logic across Users and Monetization modules

#### 1.2.3.5 Extraction Reasoning

Requires shared types to coordinate state changes between the Monetization and Users modules.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

IntegrationEvents

#### 1.3.1.2 Component Specification

Immutable record definitions for system-wide domain events used with MediatR.

#### 1.3.1.3 Implementation Requirements

- Must implement MediatR.INotification
- Must support JSON serialization for potential future out-of-process messaging

#### 1.3.1.4 Architectural Context

Shared Kernel / Event Bus Contract

#### 1.3.1.5 Extraction Reasoning

Essential for the event-driven communication pattern in the Modular Monolith.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

ApiContracts

#### 1.3.2.2 Component Specification

Data Transfer Objects (DTOs) and standardized response wrappers for the REST API.

#### 1.3.2.3 Implementation Requirements

- Implement ApiResponse<T> with Success, Data, and ErrorMessage fields
- Define request/response DTOs for all public endpoints

#### 1.3.2.4 Architectural Context

Shared Kernel / API Layer

#### 1.3.2.5 Extraction Reasoning

Ensures the mobile client receives a consistent data structure regardless of which backend module handles the request.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

SharedEnumerations

#### 1.3.3.2 Component Specification

Centralized enum definitions to prevent magic strings/numbers across modules.

#### 1.3.3.3 Implementation Requirements

- SubscriptionTier (Free, Premium)
- ShelfStatus (WantToRead, Reading, Read, DNF)
- GoalType (Books, Pages, Time)

#### 1.3.3.4 Architectural Context

Shared Kernel / Domain Language

#### 1.3.3.5 Extraction Reasoning

Required to maintain data integrity across module boundaries (e.g., Reading module needs to know ShelfStatus, Engagement module needs to know GoalType).

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Shared Kernel', 'layer_responsibilities': 'Defines the ubiquitous language and communication contracts for the system.', 'layer_constraints': ['Must have ZERO dependencies on feature modules (Users, Reading, etc.)', 'Must contain NO business logic', 'Must use primitive types or self-contained value objects'], 'implementation_patterns': ['POCO / Record Types', 'Marker Interfaces'], 'extraction_reasoning': 'Serves as the foundation dependency for all other backend projects.'}

## 1.5.0.0 Dependency Interfaces

*No items available*

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

ApiResponse<T>

#### 1.6.1.2 Consumer Repositories

- REPO-BE-MOD-USERS
- REPO-BE-MOD-READING
- REPO-BE-MOD-MONETIZATION
- REPO-BE-MOD-ENGAGEMENT
- REPO-BE-MOD-RECOMMENDATIONS

#### 1.6.1.3 Method Contracts

##### 1.6.1.3.1 Method Name

###### 1.6.1.3.1.1 Method Name

Ok

###### 1.6.1.3.1.2 Method Signature

public static ApiResponse<T> Ok(T data)

###### 1.6.1.3.1.3 Method Purpose

Creates a successful response envelope.

###### 1.6.1.3.1.4 Implementation Requirements

Sets Success=true.

##### 1.6.1.3.2.0 Method Name

###### 1.6.1.3.2.1 Method Name

Fail

###### 1.6.1.3.2.2 Method Signature

public static ApiResponse<T> Fail(string message)

###### 1.6.1.3.2.3 Method Purpose

Creates a failure response envelope.

###### 1.6.1.3.2.4 Implementation Requirements

Sets Success=false, ErrorMessage=message.

#### 1.6.1.4.0.0 Service Level Requirements

- Zero-allocation where possible

#### 1.6.1.5.0.0 Implementation Constraints

- Must be generic

#### 1.6.1.6.0.0 Extraction Reasoning

Standardizes API responses across all controllers.

### 1.6.2.0.0.0 Interface Name

#### 1.6.2.1.0.0 Interface Name

ReadingSessionLogged

#### 1.6.2.2.0.0 Consumer Repositories

- REPO-BE-MOD-READING
- REPO-BE-MOD-ENGAGEMENT
- REPO-BE-MOD-RECOMMENDATIONS

#### 1.6.2.3.0.0 Method Contracts

- {'method_name': 'ReadingSessionLogged', 'method_signature': 'public record ReadingSessionLogged(Guid SessionId, Guid UserId, int PagesRead, TimeSpan Duration, DateTimeOffset Timestamp) : INotification;', 'method_purpose': 'Notifies listeners that a reading session occurred.', 'implementation_requirements': 'Immutable record.'}

#### 1.6.2.4.0.0 Service Level Requirements

- Serializable

#### 1.6.2.5.0.0 Implementation Constraints

- No logic

#### 1.6.2.6.0.0 Extraction Reasoning

Primary event connecting the Reading domain to the Gamification/Engagement domain.

### 1.6.3.0.0.0 Interface Name

#### 1.6.3.1.0.0 Interface Name

SubscriptionTerminated

#### 1.6.3.2.0.0 Consumer Repositories

- REPO-BE-MOD-MONETIZATION
- REPO-BE-MOD-USERS
- REPO-BE-MOD-READING

#### 1.6.3.3.0.0 Method Contracts

- {'method_name': 'SubscriptionTerminated', 'method_signature': 'public record SubscriptionTerminated(Guid UserId, DateTimeOffset EffectiveDate) : INotification;', 'method_purpose': "Notifies listeners that a user's subscription has ended.", 'implementation_requirements': 'Immutable record.'}

#### 1.6.3.4.0.0 Service Level Requirements

- Serializable

#### 1.6.3.5.0.0 Implementation Constraints

- No logic

#### 1.6.3.6.0.0 Extraction Reasoning

Critical event for enforcing feature gating across the distributed monolith.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8 Class Library

### 1.7.2.0.0.0 Integration Technologies

- MediatR.Contracts (INotification)
- System.Text.Json (Serialization attributes)

### 1.7.3.0.0.0 Performance Constraints

Types must be lightweight and serialization-friendly to minimize overhead in high-throughput API endpoints.

### 1.7.4.0.0.0 Security Requirements

DTOs must not expose sensitive internal state (e.g., password hashes, internal DB IDs unless necessary).

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Mapped all necessary DTOs and Events for the ident... |
| Cross Reference Validation | Confirmed consistency with the 'Shared Kernel' def... |
| Implementation Readiness Assessment | High. The record syntax and structures are fully d... |
| Quality Assurance Confirmation | Adheres to strict 'Zero Logic' and 'Zero Dependenc... |

