# 1 Id

REPO-BE-LIB-CONTRACTS

# 2 Name

readtrack-shared-contracts

# 3 Description

A foundational, cross-cutting library repository that contains no logic. Its sole purpose is to define the shared data contracts (Data Transfer Objects - DTOs), enums, and event/notification message definitions used for communication between the different backend modules and for the public API exposed to the mobile client. Extracted from the original monolith, this repository ensures that all components speak the same language, preventing inconsistencies and tight coupling. By having all modules reference this central set of contracts, we enable decoupled communication (e.g., via MediatR events) without creating circular project dependencies. This is the 'shared kernel' of the backend architecture.

# 4 Type

🔹 Cross-Cutting Library

# 5 Namespace

ReadTrack.Shared.Contracts

# 6 Output Path

solution/shared/contracts

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

Plain Old C# Objects (POCOs)

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- shared

# 12 Dependencies

*No items available*

# 13 Requirements

*No items available*

# 14 Generate Tests

❌ No

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Shared Kernel

# 17 Architecture Map

*No items available*

# 18 Components Map

*No items available*

# 19 Requirements Map

*No items available*

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-BE-API

## 20.3 Decomposition Reasoning

In a modular monolith, direct references between business modules are undesirable. A shared contracts library is essential to break these dependencies. It allows one module (e.g., Reading) to publish an event that another module (e.g., Engagement) can consume without either module knowing about the other's implementation details, only the shared event contract.

## 20.4 Extracted Responsibilities

- API Data Transfer Objects (DTOs)
- Shared Enumerations (e.g., SubscriptionTier, Shelf)
- MediatR Notification/Event Definitions (e.g., ReadingSessionLogged)

## 20.5 Reusability Scope

- This library is a dependency for every single backend business module and the API host.

## 20.6 Development Benefits

- Enforces a consistent data language across the system.
- Prevents circular dependencies between modules.
- Facilitates decoupled, event-based communication.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

## 22.1 Public Interfaces

### 22.1.1 Interface

#### 22.1.1.1 Interface

UserProfileDto

#### 22.1.1.2 Methods

*No items available*

#### 22.1.1.3 Events

*No items available*

#### 22.1.1.4 Properties

- Guid Id
- string Email
- string DisplayName

#### 22.1.1.5 Consumers

- REPO-BE-MOD-USERS
- REPO-BE-MOD-READING

### 22.1.2.0 Interface

#### 22.1.2.1 Interface

ReadingSessionLogged : INotification

#### 22.1.2.2 Methods

*No items available*

#### 22.1.2.3 Events

*No items available*

#### 22.1.2.4 Properties

- Guid UserId
- int PagesRead

#### 22.1.2.5 Consumers

- REPO-BE-MOD-READING
- REPO-BE-MOD-ENGAGEMENT

# 23.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Not applicable. |
| Event Communication | Defines the message contracts used by MediatR. |
| Data Flow | Defines the structure of data as it crosses module... |
| Error Handling | May define common error response DTOs. |
| Async Patterns | Not applicable. |

# 24.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | This project should contain only C# classes, recor... |
| Performance Considerations | Not applicable. |
| Security Considerations | Ensure DTOs do not accidentally expose sensitive d... |
| Testing Approach | Generally does not require tests, as it contains n... |

# 25.0.0.0 Scope Boundaries

## 25.1.0.0 Must Implement

- Data structures for API requests/responses.
- Shared enums.
- Event message contracts.

## 25.2.0.0 Must Not Implement

- Any business logic, methods, or behavior.
- Database access logic.
- References to any other project in the solution.

## 25.3.0.0 Extension Points

- Adding new DTOs or event definitions as the system grows.

## 25.4.0.0 Validation Rules

*No items available*

