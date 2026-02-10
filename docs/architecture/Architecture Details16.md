# 1 Style

ModularMonolith

# 2 Patterns

## 2.1 Clean Architecture (Client)

### 2.1.1 Name

Clean Architecture (Client)

### 2.1.2 Description

The Flutter mobile client will be structured using Clean Architecture principles to separate UI (Presentation), business logic (Domain), and data handling (Data). This ensures testability, maintainability, and independence from external frameworks.

### 2.1.3 Benefits

- Separation of Concerns
- Testability of Business Logic
- Framework Independence

### 2.1.4 Tradeoffs

- Increased initial setup complexity due to boilerplate code.

### 2.1.5 Applicability

#### 2.1.5.1 Scenarios

- Complex mobile applications requiring offline support and clear boundaries between logic layers.

#### 2.1.5.2 Constraints

- May be overkill for very simple, single-screen applications.

## 2.2.0.0 Layered Architecture (Backend)

### 2.2.1.0 Name

Layered Architecture (Backend)

### 2.2.2.0 Description

The .NET backend API will use a Layered (Onion) Architecture, ensuring that business logic (Domain/Application layers) is independent of external concerns like databases, APIs, and UI (Infrastructure/Presentation layers).

### 2.2.3.0 Benefits

- High Maintainability
- Loose Coupling
- Enhanced Testability

### 2.2.4.0 Tradeoffs

- Requires discipline to maintain layer dependencies correctly.

### 2.2.5.0 Applicability

#### 2.2.5.1 Scenarios

- Enterprise applications where business logic is complex and needs to be isolated from infrastructure details.

#### 2.2.5.2 Constraints

*No items available*

## 2.3.0.0 Repository Pattern

### 2.3.1.0 Name

Repository Pattern

### 2.3.2.0 Description

Used in both client and backend to abstract data access. In the client, the repository will choose between the local Isar database (offline) and the remote API (online), handling data synchronization. In the backend, it abstracts EF Core details from the application layer.

### 2.3.3.0 Benefits

- Decouples business logic from data storage technology
- Centralizes data access logic
- Facilitates offline support and data synchronization on the client

### 2.3.4.0 Tradeoffs

*No items available*

### 2.3.5.0 Applicability

#### 2.3.5.1 Scenarios

- Applications requiring offline capabilities (REQ-FUNC-011)
- Systems where data access logic is complex and needs to be tested independently

#### 2.3.5.2 Constraints

*No items available*

## 2.4.0.0 CQRS (Command Query Responsibility Segregation)

### 2.4.1.0 Name

CQRS (Command Query Responsibility Segregation)

### 2.4.2.0 Description

The backend Application Layer will use a lightweight CQRS pattern to separate write operations (Commands) from read operations (Queries). This helps optimize read performance for dashboards and lists, directly supporting REQ-PERF-001 and REQ-PERF-002.

### 2.4.3.0 Benefits

- Optimized data models for reads vs. writes
- Improved performance for query operations
- Clearer separation of concerns

### 2.4.4.0 Tradeoffs

- Can introduce code duplication between command and query models.

### 2.4.5.0 Applicability

#### 2.4.5.1 Scenarios

- Systems with different performance requirements for reading and writing data.

#### 2.4.5.2 Constraints

*No items available*

# 3.0.0.0 Layers

## 3.1.0.0 client_presentation

### 3.1.1.0 Id

client_presentation

### 3.1.2.0 Name

Client.Presentation

### 3.1.3.0 Description

Handles all UI and user interaction for the Flutter mobile application. Contains widgets, screens, and state management components (BLoCs) that respond to user input and lifecycle events.

### 3.1.4.0 Technologystack

Flutter 3.22+, Material Design, flutter_bloc 8.1+

### 3.1.5.0 Language

Dart 3.4+

### 3.1.6.0 Type

🔹 Presentation

### 3.1.7.0 Responsibilities

- Render the user interface, including light and dark themes (REQ-UI-001).
- Ensure UI adheres to WCAG 2.1 Level AA (REQ-UI-002).
- Handle user input and delegate actions to the Domain layer via BLoCs.
- Display advertisements using the Google AdMob SDK for 'Free User' tier (REQ-FUNC-010).
- Manage UI state and data binding.

### 3.1.8.0 Components

- Screens (Dashboard, Goals, Onboarding)
- Widgets (Buttons, Forms, Lists)
- State Management (BLoCs)
- AdMobIntegrationComponent

### 3.1.9.0 Interfaces

*No items available*

### 3.1.10.0 Dependencies

- {'layerId': 'client_domain', 'type': 'Required'}

### 3.1.11.0 Constraints

*No items available*

## 3.2.0.0 client_domain

### 3.2.1.0 Id

client_domain

### 3.2.2.0 Name

Client.Domain

### 3.2.3.0 Description

The core of the mobile application's business logic. Contains entities, use cases (interactors), and abstract repository interfaces. This layer is independent of any UI or data frameworks.

### 3.2.4.0 Technologystack

Plain Dart objects and interfaces.

### 3.2.5.0 Language

Dart 3.4+

### 3.2.6.0 Type

🔹 Domain

### 3.2.7.0 Responsibilities

- Define core business entities (e.g., Goal, UserBook).
- Encapsulate application-specific business rules and logic in Use Cases.
- Define the contracts (interfaces) for data repositories that the Data layer will implement.

### 3.2.8.0 Components

- Entities (User, Goal)
- UseCases (CreateGoal, GetReadingStats, SynchronizeData)
- Repository Interfaces (IGoalRepository, IUserRepository)

### 3.2.9.0 Interfaces

*No items available*

### 3.2.10.0 Dependencies

*No items available*

### 3.2.11.0 Constraints

- {'type': 'DependencyRule', 'description': 'This layer must not depend on any other layer in the client architecture.'}

## 3.3.0.0 client_data

### 3.3.1.0 Id

client_data

### 3.3.2.0 Name

Client.Data

### 3.3.3.0 Description

Implements the repository interfaces defined in the Domain layer. It is responsible for fetching data from either the local database (Isar) or the remote backend API and managing data synchronization.

### 3.3.4.0 Technologystack

Isar 3.1+, Dio 5.4+

### 3.3.5.0 Language

Dart 3.4+

### 3.3.6.0 Type

🔹 DataAccess

### 3.3.7.0 Responsibilities

- Implement repository interfaces from the Domain layer.
- Manage local data persistence using the Isar database (REQ-FUNC-011).
- Communicate with the backend API for remote data.
- Implement the data synchronization logic for offline support (REQ-FUNC-011).
- Map data between remote DTOs, local database models, and domain entities.

### 3.3.8.0 Components

- Repository Implementations (GoalRepository, UserRepository)
- Data Sources (LocalDataSource using Isar, RemoteDataSource using Dio)
- Data Models (DTOs for API, Isar Schemas)
- SynchronizationService

### 3.3.9.0 Interfaces

*No items available*

### 3.3.10.0 Dependencies

- {'layerId': 'client_domain', 'type': 'Required'}

### 3.3.11.0 Constraints

*No items available*

## 3.4.0.0 backend_presentation

### 3.4.1.0 Id

backend_presentation

### 3.4.2.0 Name

Backend.Presentation

### 3.4.3.0 Description

The entry point to the backend system. Exposes functionality via a RESTful API built with ASP.NET Core. It handles HTTP requests, authentication, authorization, and routes them to the Application layer.

### 3.4.4.0 Technologystack

ASP.NET Core 8

### 3.4.5.0 Language

C# 12

### 3.4.6.0 Type

🔹 APIGateway

### 3.4.7.0 Responsibilities

- Define and expose RESTful API endpoints.
- Handle user authentication (JWT validation).
- Implement authorization policies (e.g., Premium vs. Free user access).
- Perform request model validation.
- Serialize responses into JSON format.

### 3.4.8.0 Components

- API Controllers (GoalsController, UsersController, SubscriptionsController)
- Middleware (Authentication, ExceptionHandling, RateLimiting)
- DTOs (Data Transfer Objects)

### 3.4.9.0 Interfaces

*No items available*

### 3.4.10.0 Dependencies

- {'layerId': 'backend_application', 'type': 'Required'}

### 3.4.11.0 Constraints

*No items available*

## 3.5.0.0 backend_application

### 3.5.1.0 Id

backend_application

### 3.5.2.0 Name

Backend.Application

### 3.5.3.0 Description

Orchestrates the business logic. Contains application services, commands, queries, and interfaces for infrastructure dependencies (e.g., repositories, email services). It is the direct consumer of the Domain layer.

### 3.5.4.0 Technologystack

MediatR 12.2+, FluentValidation 11.9+

### 3.5.5.0 Language

C# 12

### 3.5.6.0 Type

🔹 ApplicationServices

### 3.5.7.0 Responsibilities

- Implement application-specific use cases (e.g., creating a goal, generating recommendations).
- Coordinate domain entities and services to perform operations.
- Define contracts (interfaces) for repositories, external API clients, and other infrastructure concerns.
- Handle transaction management.

### 3.5.8.0 Components

- Commands (CreateGoalCommand, InitiateDataExportCommand)
- Queries (GetUserGoalsQuery, GetDashboardStatsQuery)
- Handlers (for Commands and Queries)
- Application Services (SubscriptionService, RecommendationService)
- Interfaces (IUserRepository, IOpenAIClient, IDataExportJobRepository)

### 3.5.9.0 Interfaces

*No items available*

### 3.5.10.0 Dependencies

- {'layerId': 'backend_domain', 'type': 'Required'}

### 3.5.11.0 Constraints

*No items available*

## 3.6.0.0 backend_domain

### 3.6.1.0 Id

backend_domain

### 3.6.2.0 Name

Backend.Domain

### 3.6.3.0 Description

The core of the backend, containing business entities, aggregates, and domain-specific logic. It has no dependencies on any other layer.

### 3.6.4.0 Technologystack

Plain C# objects.

### 3.6.5.0 Language

C# 12

### 3.6.6.0 Type

🔹 Domain

### 3.6.7.0 Responsibilities

- Define core domain entities and aggregates (User, Subscription, Goal).
- Encapsulate business rules and invariants within the entities.
- Define domain events.

### 3.6.8.0 Components

- Entities (User, Subscription, Goal, Book, Recommendation)
- Aggregates (User as an aggregate root)
- Enums (SubscriptionTier, GoalType)
- Domain Services (if any complex logic spans multiple aggregates)

### 3.6.9.0 Interfaces

*No items available*

### 3.6.10.0 Dependencies

*No items available*

### 3.6.11.0 Constraints

- {'type': 'DependencyRule', 'description': 'This layer must not have any dependencies on other layers.'}

## 3.7.0.0 backend_infrastructure

### 3.7.1.0 Id

backend_infrastructure

### 3.7.2.0 Name

Backend.Infrastructure

### 3.7.3.0 Description

Provides concrete implementations for the interfaces defined in the Application and Domain layers. It handles all external concerns like database access, calling third-party APIs, and background job processing.

### 3.7.4.0 Technologystack

Entity Framework Core 8, Polly 8.4+, Hangfire 1.8+, Npgsql 8.0+

### 3.7.5.0 Language

C# 12

### 3.7.6.0 Type

🔹 Infrastructure

### 3.7.7.0 Responsibilities

- Implement data repositories using Entity Framework Core and PostgreSQL.
- Provide a client for the OpenAI GPT-4 API (REQ-FUNC-007).
- Implement resilience patterns (Retry, Circuit Breaker) using Polly for external API calls (REQ-REL-002).
- Manage background jobs for data exports using Hangfire (REQ-FUNC-009).
- Handle other external concerns like logging (Serilog) and caching (Redis).

### 3.7.8.0 Components

- Persistence (DbContext, EF Core Repositories, Migrations)
- External Clients (OpenAIClient)
- Background Jobs (DataExportJobHandler)
- Services (DateTimeProvider, CachingService)

### 3.7.9.0 Interfaces

*No items available*

### 3.7.10.0 Dependencies

- {'layerId': 'backend_application', 'type': 'Required'}

### 3.7.11.0 Constraints

*No items available*

# 4.0.0.0 Quality Attributes

## 4.1.0.0 Performance

### 4.1.1.0 Tactics

- Use of pre-aggregated data in 'UserReadingStats' table for fast dashboard loading (REQ-PERF-002).
- Backend CQRS pattern to optimize read queries.
- Distributed caching with Redis for frequently accessed data.
- Asynchronous processing for all I/O-bound operations in the backend.
- Efficient indexing on the PostgreSQL database as defined in the schema.
- Use of Isar, a high-performance local database, on the mobile client.

### 4.1.2.0 Metrics

- 95th percentile latency for all core backend API endpoints < 200ms (REQ-PERF-001).
- Dashboard screen load time < 1.5 seconds on 4G (REQ-PERF-002).

## 4.2.0.0 Scalability

### 4.2.1.0 Tactics

- Stateless backend API design to allow horizontal scaling behind a load balancer.

### 4.2.2.0 Approach

Horizontal

## 4.3.0.0 Security

| Property | Value |
|----------|-------|
| Authentication | JWT (JSON Web Token) based authentication for the ... |
| Authorization | Policy-based authorization in ASP.NET Core to diff... |
| Data Protection | Password hashing (bcrypt), HTTPS for all communica... |

## 4.4.0.0 Reliability

### 4.4.1.0 Tactics

- Implementation of Retry and Circuit Breaker patterns using Polly for calls to external APIs like OpenAI (REQ-REL-002).
- Graceful degradation on the client application by using locally cached data from Isar when the backend is unavailable.
- Asynchronous, persistent background jobs for long-running tasks like data export to prevent request timeouts.

## 4.5.0.0 Maintainability

### 4.5.1.0 Tactics

- Strict separation of concerns via Clean/Layered Architecture.
- Use of Dependency Injection throughout both client and backend to promote loose coupling.
- Modular monolith design for the backend to isolate feature domains.

## 4.6.0.0 Extensibility

### 4.6.1.0 Tactics

- Use of interfaces for all infrastructure dependencies, allowing for alternative implementations.
- Modular design allows for new features to be added with minimal impact on existing code.

# 5.0.0.0 Technology Stack

## 5.1.0.0 Primary Language

Dart 3.4+ (Client), C# 12 (Backend)

## 5.2.0.0 Frameworks

- Flutter 3.22+ (Client)
- .NET 8 (Backend)
- ASP.NET Core 8 (Backend API)
- Entity Framework Core 8 (Backend Data Access)

## 5.3.0.0 Database

| Property | Value |
|----------|-------|
| Type | PostgreSQL (Backend), Isar (Client Local) |
| Version | PostgreSQL 16, Isar 3.1+ |
| Orm | Entity Framework Core 8 |

## 5.4.0.0 Domain Specific Libraries

### 5.4.1.0 google_mobile_ads

#### 5.4.1.1 Name

google_mobile_ads

#### 5.4.1.2 Version

5.1.0+

#### 5.4.1.3 Purpose

To display advertisements to 'Free User' tier users as per REQ-FUNC-010.

#### 5.4.1.4 Domain

Advertisements

### 5.4.2.0 OpenAI-DotNet

#### 5.4.2.1 Name

OpenAI-DotNet

#### 5.4.2.2 Version

7.8.0+

#### 5.4.2.3 Purpose

To integrate with the OpenAI GPT-4 API for generating book recommendations as per REQ-FUNC-007.

#### 5.4.2.4 Domain

AI Suggestions

## 5.5.0.0 Infrastructure

| Property | Value |
|----------|-------|
| Logging | Serilog (Backend), logger (Client) |
| Caching | Redis 7.2+ |
| Testing | NUnit/xUnit (Backend), flutter_test/test (Client) |

# 6.0.0.0 Backend Services

## 6.1.0.0 Background Job Processing

### 6.1.1.0 Name

DataExportJobService

### 6.1.2.0 Purpose

Handles the asynchronous, user-initiated export of personal data in a machine-readable format to comply with GDPR (REQ-FUNC-009).

### 6.1.3.0 Type

🔹 Background Job Processing

### 6.1.4.0 Communication

Triggered via an API call which enqueues a job in Hangfire. The job runs in the background, updating the 'DataExportJob' table status.

## 6.2.0.0 Scheduled Task / Webhook Processor

### 6.2.1.0 Name

SubscriptionStatusService

### 6.2.2.0 Purpose

Manages subscription lifecycle events, such as reverting a 'Premium User' to 'Free User' upon termination (REQ-FUNC-002).

### 6.2.3.0 Type

🔹 Scheduled Task / Webhook Processor

### 6.2.4.0 Communication

Can be implemented as a scheduled job that checks for expired subscriptions or as a webhook endpoint for a payment provider.

## 6.3.0.0 Data Processing

### 6.3.1.0 Name

RecommendationGenerationService

### 6.3.2.0 Purpose

Processes user data and feedback to generate personalized book recommendations via the OpenAI GPT-4 API, including rate limiting (REQ-FUNC-007).

### 6.3.3.0 Type

🔹 Data Processing

### 6.3.4.0 Communication

Invoked by the Application layer, makes external API calls using a resilient HTTP client (with Polly).

# 7.0.0.0 Cross Cutting Concerns

| Property | Value |
|----------|-------|
| Logging | Implemented via Dependency Injection in both clien... |
| Exception Handling | Global middleware in ASP.NET Core for the backend.... |
| Configuration | .NET Configuration framework (appsettings.json, en... |
| Validation | FluentValidation integrated into a MediatR pipelin... |
| Security | Authentication middleware and authorization attrib... |

