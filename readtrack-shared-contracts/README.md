# ReadTrack - Enterprise Reading Tracker

ReadTrack is a comprehensive, enterprise-grade reading tracking application built using a **Modular Monolith** architecture. This repository hosts the complete system, including the backend services, shared contracts, mobile client, and infrastructure-as-code.

## 🏗️ Repository Structure

The repository is organized into distinct workspaces to enforce architectural boundaries:

- **`src/ReadTrack.Shared.Contracts`**: The **Shared Kernel**. Contains strict DTOs, Enums, and Integration Events used for communication between modules and the client. Zero business logic.
- **`backend/`**: The Modular Monolith host and feature modules (User, Reading, Monetization, Engagement).
- **`mobile/`**: The Flutter mobile application.
- **`infrastructure/`**: AWS CDK (TypeScript) definitions for cloud deployment.

## 🚀 Architecture: Modular Monolith

The system follows **Domain-Driven Design (DDD)** and **Clean Architecture** principles.

### The Shared Kernel (`ReadTrack.Shared.Contracts`)
This library acts as the "nervous system" of the application. It defines the ubiquitous language used across boundaries.
- **DTOs**: Standardized shapes for API Requests and Responses (`ApiResponse<T>`, `BookDto`, etc.).
- **Messages**: `INotification` records for MediatR integration events (e.g., `ReadingSessionLogged`, `UserCreated`).
- **Enums**: Shared domain enumerations (e.g., `SubscriptionTier`, `GoalType`).

**Architectural Rules:**
1.  **Zero Dependency**: The Shared Contracts project must NOT depend on any feature module.
2.  **Immutability**: All contracts are defined as C# `record` types.
3.  **Serialization**: Contracts are optimized for `System.Text.Json` serialization.

## 🛠️ Getting Started

### Prerequisites
- .NET 8 SDK
- Flutter SDK (3.x+)
- Node.js (20.x+)
- Docker Desktop

### Backend Setup
1.  Navigate to the backend directory:
    ```bash
    cd backend
    ```
2.  Restore dependencies:
    ```bash
    dotnet restore ReadTrack.sln
    ```
3.  Run the host:
    ```bash
    dotnet run --project src/ReadTrack.Host
    ```

### Mobile Setup
1.  Navigate to the mobile directory:
    ```bash
    cd mobile
    ```
2.  Get dependencies:
    ```bash
    flutter pub get
    ```
3.  Run the app:
    ```bash
    flutter run
    ```

## 🔄 CI/CD Pipelines

- **Backend CI**: Builds .NET solution and runs unit/integration tests.
- **Mobile CI**: Analyzes Dart code and runs widget tests.
- **Infra Deploy**: Deploys AWS resources via CDK (requires AWS credentials).

## 🧪 Testing

We adhere to a high standard of code quality:
- **Unit Tests**: Business logic validation.
- **Integration Tests**: Module interaction verification.
- **Architecture Tests**: Enforcing dependency rules (e.g., Domain cannot depend on Infrastructure).

## 📄 License

Proprietary - ReadTrack Inc.