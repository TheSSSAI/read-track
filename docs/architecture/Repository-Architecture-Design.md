# ReadTrack - Enterprise Architecture Documentation

## Executive Summary

This document outlines the comprehensive enterprise architecture for the ReadTrack application, a cross-platform mobile service for personal reading tracking. The architecture is designed to support a scalable, maintainable, and high-performance system, aligning with a mobile-first, cloud-native strategy and a freemium business model.

The key architectural decision was to evolve the system from an initial monolithic structure into a **Modular Monolith** for the backend and a **decomposed component-based architecture** for the frontend. This hybrid approach provides a pragmatic balance, offering the development velocity and isolated ownership benefits of microservices while avoiding their immediate operational complexity and cost. The backend is systematically partitioned into vertical slices based on business domains (Users, Monetization, Reading, Engagement, AI Recommendations), enabling parallel development by autonomous teams. The Flutter frontend is similarly decomposed into a core application shell, a reusable UI Kit, and a dedicated API client library, promoting consistency and reusability.

The chosen technology stack, centered on **.NET 8** for the backend and **Flutter** for the client, is optimized for performance, developer productivity, and cross-platform consistency. The entire system is deployed on **Amazon Web Services (AWS)** using an Infrastructure-as-Code (IaC) approach with the **AWS CDK**, ensuring repeatable, secure, and scalable environments.

This architecture directly addresses critical non-functional requirements, including API latency (P95 < 200ms), dashboard load times (< 1.5s), and resilience against external service failures, delivering significant business value through enhanced system stability, accelerated feature delivery, and a clear path for future growth.

## Solution Architecture Overview

The ReadTrack solution is a cloud-native system composed of a Flutter mobile client and a .NET 8 backend API, hosted on AWS. The architecture is built upon a foundation of proven design patterns to ensure robustness and scalability.

- **Technology Stack**:
  - **Frontend**: Flutter 3.22+ / Dart 3.4+ with Riverpod for state management and Isar for local offline storage.
  - **Backend**: .NET 8 / C# 12 Modular Monolith using ASP.NET Core for APIs, Entity Framework Core for data access, MediatR for in-process messaging, and Hangfire for background jobs.
  - **Database**: Amazon Aurora (PostgreSQL) for primary data, Amazon ElastiCache (Redis) for caching, and Amazon OpenSearch for vector storage.
  - **Infrastructure**: AWS (`eu-central-1`), provisioned via AWS CDK with TypeScript.

- **Architectural Patterns**:
  - **Modular Monolith**: The backend is structured as a single deployable unit composed of loosely coupled, independently maintainable modules that communicate via in-process messaging. This provides clear domain boundaries without the overhead of a distributed system.
  - **CQRS (Command Query Responsibility Segregation)**: MediatR is used to separate write operations (Commands) from read operations (Queries), simplifying logic within each module.
  - **Client-Side Data Synchronization**: The Flutter app uses an offline-first approach, persisting data locally in an Isar database and synchronizing with the backend when online.
  - **Job Queue**: Hangfire is used to offload long-running and resource-intensive tasks (e.g., AI recommendation generation, data exports) from the synchronous API request path, ensuring the UI remains responsive.
  - **Resilience Patterns**: External API integrations are wrapped with Retry and Circuit Breaker patterns (via Polly) to handle transient faults and prevent cascading failures.

- **Integration Approach**: The primary integration point is the versioned RESTful API exposed by the backend. Internal communication between backend modules is handled asynchronously via an in-process event bus (MediatR). External integrations with third-party services (Auth0, OpenAI, Google Books) are isolated behind dedicated adapter interfaces within their respective modules.

## Repository Architecture Strategy

The repository structure was strategically decomposed from an initial state of three monolithic repositories (`backend-api`, `mobile-app`, `infrastructure`) to a final, optimized set of ten repositories. This decomposition was driven by a hybrid strategy of Business Domain-Driven Decomposition and Cross-Cutting Concern Extraction.

- **Decomposition Rationale**: The monolithic backend was partitioned into vertical slices representing distinct business capabilities (Users, Monetization, Reading, Engagement, Recommendations). This aligns the codebase with the business domain, isolates complexity, and allows for autonomous team ownership. For example, the highly specialized and costly AI/LLM logic is now contained within the `readtrack-recommendations-module`, preventing its unique dependencies and performance profile from affecting the core application.

- **Optimization Benefits**:
  - **Improved Maintainability**: Smaller, focused repositories with a single responsibility are easier to understand, modify, and test.
  - **Enhanced Team Autonomy**: Teams can develop, test, and deploy their modules independently, reducing coordination overhead and accelerating delivery.
  - **Isolation of Complexity**: Volatile or complex dependencies, such as payment provider webhooks or LLM clients, are confined to their specific modules.
  - **Code Reusability**: Cross-cutting concerns like shared data contracts, infrastructure abstractions, and UI components are extracted into reusable library repositories, enforcing consistency and reducing duplication.

- **Development Workflow**: This structure enables a parallel development workflow where teams responsible for different modules (e.g., Monetization vs. Engagement) can work concurrently with minimal friction. Shared libraries are versioned, allowing modules to adopt updates at their own pace. The `readtrack-backend-host` and `readtrack-mobile-app-shell` repositories act as the final integration points where all components are assembled into deployable artifacts.

## System Architecture Diagrams

### Repository Dependency Architecture

This diagram illustrates the dependencies between the decomposed repositories, grouped by their architectural layer and platform. It shows how business modules depend on shared libraries and how the host applications compose the final system.

mermaid
graph TD
    subgraph "CI/CD Pipeline" as CICD
        CICD_Repo[REPO-CI-CD]
    end

    subgraph "Frontend Application" as FE
        subgraph "App Shell"
            FE_App[REPO-FE-APP]
        end
        subgraph "Shared Libraries"
            FE_Lib_UI[REPO-FE-LIB-UIKIT]
            FE_Lib_API[REPO-FE-LIB-APICLIENT]
        end
    end

    subgraph "Backend Application (Modular Monolith)" as BE
        subgraph "Host"
            BE_Host[REPO-BE-HOST]
        end
        subgraph "Business Modules"
            BE_Mod_Users[REPO-BE-MOD-USERS]
            BE_Mod_Monetization[REPO-BE-MOD-MONETIZATION]
            BE_Mod_Reading[REPO-BE-MOD-READING]
            BE_Mod_Engagement[REPO-BE-MOD-ENGAGEMENT]
            BE_Mod_Recs[REPO-BE-MOD-RECOMMENDATIONS]
        end
        subgraph "Shared Libraries"
            BE_Lib_Contracts[REPO-BE-LIB-CONTRACTS]
            BE_Lib_Infra[REPO-BE-LIB-INFRA]
        end
    end

    subgraph "Infrastructure as Code" as IA
        IA_IaC[REPO-IA-IAC]
    end

    %% Frontend Dependencies
    FE_App --> FE_Lib_UI
    FE_App --> FE_Lib_API

    %% Backend Dependencies
    BE_Host --> BE_Mod_Users
    BE_Host --> BE_Mod_Monetization
    BE_Host --> BE_Mod_Reading
    BE_Host --> BE_Mod_Engagement
    BE_Host --> BE_Mod_Recs
    BE_Host --> BE_Lib_Infra

    BE_Mod_Users --> BE_Lib_Contracts
    BE_Mod_Users --> BE_Lib_Infra
    
    BE_Mod_Monetization --> BE_Lib_Contracts
    BE_Mod_Monetization --> BE_Lib_Infra
    BE_Mod_Monetization --> BE_Mod_Users

    BE_Mod_Reading --> BE_Lib_Contracts
    BE_Mod_Reading --> BE_Lib_Infra
    BE_Mod_Reading --> BE_Mod_Monetization

    BE_Mod_Engagement --> BE_Lib_Contracts
    BE_Mod_Engagement --> BE_Lib_Infra
    BE_Mod_Engagement --> BE_Mod_Monetization
    BE_Mod_Engagement --> BE_Mod_Reading

    BE_Mod_Recs --> BE_Lib_Contracts
    BE_Mod_Recs --> BE_Lib_Infra
    BE_Mod_Recs --> BE_Mod_Monetization
    BE_Mod_Recs --> BE_Mod_Reading

    BE_Lib_Infra --> BE_Lib_Contracts

    %% Integration & Deployment Dependencies
    FE_Lib_API -.->|Consumes API| BE_Host
    CICD_Repo -.->|Deploys| BE_Host
    CICD_Repo -.->|Provisions| IA_IaC

    %% Styling
    classDef app fill:#e6f3ff,stroke:#007bff,stroke-width:2px
    classDef module fill:#e8f5e9,stroke:#4caf50,stroke-width:2px
    classDef library fill:#fff8e1,stroke:#ffc107,stroke-width:2px
    classDef infra fill:#f3e5f5,stroke:#9c27b0,stroke-width:2px
    
    class FE_App,BE_Host app
    class BE_Mod_Users,BE_Mod_Monetization,BE_Mod_Reading,BE_Mod_Engagement,BE_Mod_Recs module
    class FE_Lib_UI,FE_Lib_API,BE_Lib_Contracts,BE_Lib_Infra library
    class IA_IaC,CICD_Repo infra


### Component Integration Patterns

This sequence diagram illustrates the event-driven, in-process communication between backend modules for the core "Log a Reading Session" use case. It shows how a single API call triggers a chain of decoupled actions across multiple business domains.

mermaid
sequenceDiagram
    participant Client as Mobile Client
    participant Host as API Host
    participant Reading as Reading Module
    participant MediatR as Event Bus
    participant Engagement as Engagement Module
    participant Recs as Recommendations Module

    Client->>+Host: POST /api/v1/reading/sessions
    Note right of Host: Request authorized and routed
    Host->>+Reading: Handle(LogReadingSessionCommand)
    Reading->>Reading: Validate request & save ReadingSession to DB
    Note over Reading: Business logic for session logging
    Reading->>+MediatR: Publish(ReadingSessionLogged Event)
    MediatR-->>-Reading: Acknowledge
    Reading-->>-Host: Return Success Response
    Host-->>-Client: 200 OK

    par
        MediatR->>+Engagement: Handle(ReadingSessionLogged Event)
        Engagement->>Engagement: Update Goal progress in DB
        Engagement-->>-MediatR: Complete
    and
        MediatR->>+Recs: Handle(ReadingSessionLogged Event)
        Recs->>Recs: Enqueue background job to update user's AI vector embeddings
        Recs-->>-MediatR: Complete
    end


## Repository Catalog

This catalog provides a detailed specification for each repository in the final architecture.

#### `REPO-BE-HOST` (readtrack-backend-host)
- **Description**: The composition root and host for the .NET 8 modular monolith. Integrates all business modules and shared libraries into a single, deployable ASP.NET Core application. Manages startup, configuration, DI, and middleware.
- **Type**: Application Services
- **Technology**: ASP.NET Core 8, C# 12
- **Dependencies**: All `REPO-BE-MOD-*` modules, `REPO-BE-LIB-INFRA`.

#### `REPO-BE-MOD-USERS` (readtrack-users-module)
- **Description**: Encapsulates user and identity management. Responsible for social login (via Auth0), profile management, and data privacy features like account deletion and data export (REQ-USR-001).
- **Type**: Business Logic
- **Technology**: .NET 8, EF Core 8
- **Dependencies**: `REPO-BE-LIB-CONTRACTS`, `REPO-BE-LIB-INFRA`.

#### `REPO-BE-MOD-MONETIZATION` (readtrack-monetization-module)
- **Description**: Manages all monetization logic, including the freemium subscription model, feature gating, and processing payment provider webhooks (REQ-FRE-001).
- **Type**: Business Logic
- **Technology**: .NET 8, EF Core 8
- **Dependencies**: `REPO-BE-LIB-CONTRACTS`, `REPO-BE-LIB-INFRA`, `REPO-BE-MOD-USERS`.

#### `REPO-BE-MOD-READING` (readtrack-reading-module)
- **Description**: The core domain of the application, responsible for personal library management, virtual shelves, and logging reading sessions (REQ-TRK-001). Integrates with the Google Books API.
- **Type**: Business Logic
- **Technology**: .NET 8, EF Core 8
- **Dependencies**: `REPO-BE-LIB-CONTRACTS`, `REPO-BE-LIB-INFRA`, `REPO-BE-MOD-MONETIZATION`.

#### `REPO-BE-MOD-ENGAGEMENT` (readtrack-engagement-module)
- **Description**: Contains features to drive user engagement, such as goal setting (REQ-GOL-001), daily tasks, reading tips (from a CMS), and vocabulary games. Reacts to events from the Reading module.
- **Type**: Business Logic
- **Technology**: .NET 8, EF Core 8
- **Dependencies**: `REPO-BE-LIB-CONTRACTS`, `REPO-BE-LIB-INFRA`, `REPO-BE-MOD-MONETIZATION`, `REPO-BE-MOD-READING`.

#### `REPO-BE-MOD-RECOMMENDATIONS` (readtrack-recommendations-module)
- **Description**: A specialized module for generating AI-powered book recommendations (REQ-AIS-001). Encapsulates all logic for interacting with OpenAI and the Amazon OpenSearch vector store.
- **Type**: Business Logic
- **Technology**: .NET 8, OpenAI-DotNet, OpenSearch.Client
- **Dependencies**: `REPO-BE-LIB-CONTRACTS`, `REPO-BE-LIB-INFRA`, `REPO-BE-MOD-MONETIZATION`, `REPO-BE-MOD-READING`.

#### `REPO-BE-LIB-CONTRACTS` (readtrack-shared-contracts)
- **Description**: A shared kernel library defining data contracts (DTOs), enums, and event definitions used for communication between all backend modules and the public API.
- **Type**: Cross-Cutting Library
- **Technology**: .NET 8 (POCOs)
- **Dependencies**: None.

#### `REPO-BE-LIB-INFRA` (readtrack-shared-infrastructure)
- **Description**: A shared library providing reusable infrastructure components, such as a generic repository pattern, unit of work, resilience policies (Polly), and logging helpers.
- **Type**: Cross-Cutting Library
- **Technology**: .NET 8, EF Core 8, Polly, Serilog
- **Dependencies**: `REPO-BE-LIB-CONTRACTS`.

#### `REPO-FE-APP` (readtrack-mobile-app-shell)
- **Description**: The core Flutter application shell. Responsible for app bootstrapping, navigation, dependency injection (Riverpod), and composing screens from the UI Kit.
- **Type**: Application Services
- **Technology**: Flutter 3.22+, Dart 3.4+
- **Dependencies**: `REPO-FE-LIB-UIKIT`, `REPO-FE-LIB-APICLIENT`.

#### `REPO-FE-LIB-UIKIT` (readtrack-mobile-uikit)
- **Description**: A reusable Flutter package containing the application's design system, themes, and shared UI components (buttons, cards, etc.), ensuring visual consistency.
- **Type**: Cross-Cutting Library
- **Technology**: Flutter 3.22+
- **Dependencies**: None.

#### `REPO-FE-LIB-APICLIENT` (readtrack-mobile-apiclient)
- **Description**: The data layer library for the Flutter app. Manages all communication with the backend API, data models, and local offline persistence/synchronization using the Isar database (REQ-OFF-001).
- **Type**: Data Access
- **Technology**: Flutter, Dio, Isar
- **Dependencies**: None.

#### `REPO-IA-IAC` (readtrack-infrastructure)
- **Description**: Defines all cloud infrastructure as code using the AWS CDK and TypeScript. Provisions all necessary AWS resources in a repeatable and version-controlled manner.
- **Type**: Infrastructure
- **Technology**: AWS CDK, TypeScript
- **Dependencies**: None.

## Integration Architecture

Integration within the ReadTrack system occurs at two primary levels: between the client and backend, and between the modules within the backend.

- **Client-Backend Integration**: Communication is exclusively via a synchronous, versioned **RESTful API** over HTTPS. The `readtrack-mobile-apiclient` repository encapsulates all logic for calling this API. Authentication is handled via JWT Bearer tokens, which are included in the `Authorization` header of every request.

- **Inter-Module Integration (Backend)**: To maintain loose coupling, backend modules communicate primarily through an **asynchronous, in-process event bus** powered by MediatR. 
  - **Mechanism**: When a significant business event occurs (e.g., a reading session is logged in the Reading Module), it publishes a notification object (defined in `readtrack-shared-contracts`). Other modules (e.g., Engagement, Recommendations) subscribe to this notification type and execute their logic in response, without having a direct code dependency on the publishing module.
  - **Data Consistency**: For operations requiring transactional consistency, event handlers can be executed within the same database transaction as the original command.
  - **Direct Calls**: In rare cases where an immediate response is required, one module can invoke a public service interface from another module via standard dependency injection. This is used sparingly to avoid tight coupling.

## Technology Implementation Framework

This section provides high-level implementation guidance for developers working within the established architecture.

- **Backend Development**: Modules should be built following Clean Architecture principles, with a clear separation of Domain, Application, and Infrastructure layers. Use the CQRS pattern with MediatR for all use cases. All database access should be performed asynchronously (`async`/`await`) through repositories that leverage the generic patterns in `readtrack-shared-infrastructure`.

- **Frontend Development**: The application follows a feature-based structure. Each feature should contain its own views (widgets), state management (Riverpod Notifiers), and models. Views should be constructed using components from the `readtrack-mobile-uikit` library. All data access must be delegated to repository interfaces provided by the `readtrack-mobile-apiclient` library.

- **Resilience**: All external HTTP calls from the backend must be executed through HttpClient instances configured with the shared Polly resilience policies (Retry, Circuit Breaker) defined in `readtrack-shared-infrastructure`.

## Performance & Scalability Architecture

The architecture incorporates several key strategies to meet performance and scalability requirements.

- **API Performance (REQ-PERF-001)**: Achieved through a multi-layered approach:
  - **Caching**: A distributed Redis cache is used to store frequently accessed, read-heavy data (e.g., user stats for the dashboard), reducing load on the PostgreSQL database.
  - **Asynchronous Processing**: Long-running operations are offloaded to Hangfire, keeping API endpoints fast and responsive.
  - **Efficient Data Access**: The use of Entity Framework Core with optimized queries and proper indexing ensures the database is not a bottleneck.

- **Scalability (REQ-SCA-001)**: The backend is designed for horizontal scalability:
  - **Stateless Services**: The API modules are stateless, allowing new instances to be added behind a load balancer to handle increased traffic.
  - **Managed Services**: Leveraging auto-scaling AWS services like Aurora Serverless v2 and ECS Fargate allows the infrastructure to scale compute and database capacity automatically based on demand.

- **Client Performance (REQ-PERF-002)**: The mobile app's performance is optimized by:
  - **Offline-First**: Using a local Isar database allows the app to load core data instantly without waiting for network requests.
  - **Efficient State Management**: Riverpod is used to ensure that only the necessary widgets are rebuilt in response to state changes.

## Development & Deployment Strategy

- **Team Organization**: The repository structure naturally supports a feature-based or component-based team structure. A backend team could be further divided into squads owning one or more business modules (e.g., a 'Growth' squad owning Monetization and Engagement). A dedicated frontend team would own the three Flutter repositories.

- **Development Workflow**: Developers work within their respective repositories. Shared libraries (`contracts`, `infrastructure`, `uikit`, `apiclient`) are published as versioned packages (e.g., to a private NuGet/Pub feed). Business modules and application shells consume these packages, allowing for controlled and decoupled updates.

- **Deployment Architecture**: A CI/CD pipeline (defined in a dedicated `REPO-CI-CD` repository) orchestrates the entire process. 
  1. Changes to `readtrack-infrastructure` trigger an AWS CDK deployment to provision or update the cloud environment.
  2. Changes to backend modules or libraries trigger their individual build and test pipelines. 
  3. A successful build of `readtrack-backend-host` triggers the creation of a Docker container, which is then deployed to the AWS Fargate service provisioned by the IaC.
  4. Mobile app deployments are submitted to the Apple App Store and Google Play Store.

## Architecture Decision Records

### ADR-001: Adopt a Modular Monolith Architecture for the Backend
- **Decision**: The backend will be implemented as a single deployable artifact (a monolith) but will be internally structured into loosely coupled modules aligned with business domains. Inter-module communication will primarily use an in-process event bus (MediatR).
- **Rationale**: This approach provides a strong starting point that balances development speed with architectural scalability. It avoids the significant operational overhead and complexity of a distributed microservices architecture (e.g., service discovery, distributed transactions, network latency) while still achieving clear separation of concerns, enabling parallel development and independent module evolution. It offers a clear and incremental path to extracting modules into separate microservices in the future if required by scale or business needs.

### ADR-002: Decompose Frontend into Shell, UI Kit, and API Client Libraries
- **Decision**: The monolithic Flutter application will be decomposed into three distinct repositories: a core application shell, a reusable UI component library (`UI Kit`), and a dedicated data access library (`API Client`).
- **Rationale**: This separation enforces a clean architecture on the client. It decouples the application's structure (shell) from its visual appearance (UI Kit) and its data source (API Client). This improves testability (UI can be tested without data, data can be tested without UI), promotes code reuse (the UI Kit and API Client could be used in other projects), and accelerates development by providing a consistent, versioned set of building blocks.

### ADR-003: Isolate AI/LLM Functionality into a Dedicated Module
- **Decision**: All functionality related to AI-powered recommendations, including interaction with OpenAI's GPT-4 and the vector database, will be encapsulated within a dedicated `readtrack-recommendations-module`.
- **Rationale**: AI/LLM integration represents a fundamentally different architectural concern from the rest of the application's transactional workload. It is computationally expensive, has high latency, introduces unique and heavy external dependencies, and carries significant operational costs. Isolating this functionality is critical for stability (a failing AI service won't bring down the core app), cost control (rate limiting and specific scaling rules can be applied), and independent evolution of the AI features.