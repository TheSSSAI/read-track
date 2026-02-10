# 1 Id

REPO-FE-APP

# 2 Name

readtrack-mobile-app-shell

# 3 Description

This repository is the core of the Flutter mobile application, acting as the application shell. After decomposition of the original `readtrack-mobile-app`, this repository's focus is on application bootstrapping, navigation/routing, dependency injection setup, and composing the overall UI by assembling screens and features. It contains the main application entry point and platform-specific configurations (iOS/Android). It consumes the UI components from the `readtrack-mobile-uikit` library and uses the `readtrack-mobile-apiclient` for all backend communication. This separation ensures the main application repository is lean and focused on orchestration, while the more granular and reusable parts of the UI and data layers are managed independently.

# 4 Type

🔹 Application Services

# 5 Namespace

com.readtrack.app

# 6 Output Path

solution/mobile/app

# 7 Framework

Flutter 3.22+

# 8 Language

Dart

# 9 Technology

Flutter, Riverpod

# 10 Thirdparty Libraries

- flutter_riverpod
- go_router

# 11 Layer Ids

- presentation
- application

# 12 Dependencies

- REPO-FE-LIB-UIKIT
- REPO-FE-LIB-APICLIENT

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Clean Architecture

# 17 Architecture Map

- flutter-client-app-001

# 18 Components Map

*No items available*

# 19 Requirements Map

*No items available*

# 20 Decomposition Rationale

## 20.1 Operation Type

RESTRUCTURED_HOST

## 20.2 Source Repository

REPO-FE-MOB

## 20.3 Decomposition Reasoning

The original mobile app repository mixed application-level concerns (routing), UI components (buttons, cards), and data access logic (HTTP calls) together. Decomposing it separates these concerns, allowing the UI Kit and API client to be versioned and potentially reused. This repository becomes the clean integration point for all client-side components.

## 20.4 Extracted Responsibilities

- Application Initialization
- Screen Navigation and Routing
- State Management Provider Scoping
- Feature/Screen Composition

## 20.5 Reusability Scope

- Not applicable, this is the main application shell.

## 20.6 Development Benefits

- Clear separation between the app's structure and its building blocks.
- Simplifies management of top-level concerns like routing and theming.
- Makes the core UI and data logic easier to test in isolation.

# 21.0 Dependency Contracts

## 21.1 Repo-Fe-Lib-Uikit

### 21.1.1 Required Interfaces

- {'interface': 'PrimaryButton', 'methods': [], 'events': [], 'properties': ['String text', 'VoidCallback onPressed']}

### 21.1.2 Integration Pattern

Widget Composition

### 21.1.3 Communication Protocol

Dart package dependency

## 21.2.0 Repo-Fe-Lib-Apiclient

### 21.2.1 Required Interfaces

- {'interface': 'IApiClient', 'methods': ['Future<UserProfileDto> getUserProfile()'], 'events': [], 'properties': []}

### 21.2.2 Integration Pattern

Dependency Injection (via Riverpod)

### 21.2.3 Communication Protocol

Dart package dependency

# 22.0.0 Exposed Contracts

*No data available*

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Uses Riverpod to provide instances of repositories... |
| Event Communication | Uses state management streams (Riverpod) to commun... |
| Data Flow | Orchestrates the flow of data from repositories (v... |
| Error Handling | Implements app-wide error handling and user notifi... |
| Async Patterns | Manages UI state for asynchronous operations (load... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Focus on defining the navigation graph (e.g., with... |
| Performance Considerations | Optimize app startup time (as per REQ-PERF-003) an... |
| Security Considerations | Manage secure storage of authentication tokens. |
| Testing Approach | Focus on widget tests for individual screens and i... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- The `main()` function.
- The top-level MaterialApp/CupertinoApp widget.
- Routing logic.
- Dependency injection setup.

## 25.2.0 Must Not Implement

- Reusable, generic widgets (e.g., buttons, text fields) - these belong in the UI Kit.
- Direct HTTP calls or data model definitions - these belong in the API Client.

## 25.3.0 Extension Points

- Adding new screens and routes to the application.

## 25.4.0 Validation Rules

*No items available*

