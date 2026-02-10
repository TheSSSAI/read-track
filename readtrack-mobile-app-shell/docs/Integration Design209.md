# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-APP |
| Extraction Timestamp | 2023-10-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-UI-001

#### 1.2.1.2 Requirement Text

The application's user interface must support both a light theme and a dark theme.

#### 1.2.1.3 Validation Criteria

- Application adheres to system-wide theme settings by default
- Manual override available in settings

#### 1.2.1.4 Implementation Implications

- Initialize MaterialApp with theme and darkTheme properties fetched from REPO-FE-LIB-UIKIT
- Listen to platform brightness changes via WidgetsBindingObserver

#### 1.2.1.5 Extraction Reasoning

The application shell is the root composition point where the Theme is injected into the Flutter widget tree.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-FUNC-003

#### 1.2.2.2 Requirement Text

The system shall display a mandatory, multi-step onboarding sequence for all users immediately following their first successful authentication.

#### 1.2.2.3 Validation Criteria

- Redirection to /onboarding if profile.hasCompletedOnboarding is false
- Blocking navigation to dashboard until complete

#### 1.2.2.4 Implementation Implications

- Implement Route Guards in GoRouter
- Consume User Profile state from REPO-FE-LIB-APICLIENT to determine routing logic

#### 1.2.2.5 Extraction Reasoning

Routing orchestration and access control are primary responsibilities of the App Shell.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-PERF-002

#### 1.2.3.2 Requirement Text

The application's main Dashboard screen must load all necessary data and become fully interactive within 1.5 seconds from launch.

#### 1.2.3.3 Validation Criteria

- AppStartup time < 1.5s (warm start)
- Deferred loading of non-critical features

#### 1.2.3.4 Implementation Implications

- Parallelize initialization of Isar and Firebase in AppBootstrap
- Use Riverpod's `lazy: true` for feature providers

#### 1.2.3.5 Extraction Reasoning

The shell controls the startup sequence and dependency initialization graph.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

AppRouter

#### 1.3.1.2 Component Specification

Central navigation manager using GoRouter. Handles deep linking, auth guards, and redirection logic.

#### 1.3.1.3 Implementation Requirements

- Define route hierarchy (Login -> Onboarding -> Dashboard)
- Implement `redirect` callback to check Authentication State
- Configure Deep Link handling for email verification/password reset

#### 1.3.1.4 Architectural Context

Presentation Layer - Navigation Orchestrator

#### 1.3.1.5 Extraction Reasoning

Centralizes navigation logic required by REQ-FUNC-003 and global app structure.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

AppBootstrap

#### 1.3.2.2 Component Specification

Initializes global dependencies, configures error reporting, and prepares the runtime environment.

#### 1.3.2.3 Implementation Requirements

- Initialize Flutter bindings
- Configure Riverpod `ProviderContainer` with overrides
- Initialize local storage (Isar) via API Client
- Setup global error handling (FlutterError.onError)

#### 1.3.2.4 Architectural Context

Presentation Layer - Composition Root

#### 1.3.2.5 Extraction Reasoning

Ensures the environment is ready before mounting the UI, critical for REQ-PERF-002.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

GlobalProviders

#### 1.3.3.2 Component Specification

Top-level Riverpod providers for cross-cutting concerns (Auth, Theme, Localization).

#### 1.3.3.3 Implementation Requirements

- Provide `authNotifierProvider` listening to API Client stream
- Provide `themeModeProvider` for dynamic switching

#### 1.3.3.4 Architectural Context

Presentation Layer - State Injection

#### 1.3.3.5 Extraction Reasoning

Binds the UI Kit and API Client libraries to the application lifecycle.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Application Shell (Presentation)', 'layer_responsibilities': 'Orchestration, Navigation, Dependency Injection, and Feature Composition.', 'layer_constraints': ['Must NOT implement business logic (delegate to Domain packages)', 'Must NOT implement raw data access (delegate to API Client package)', 'Must NOT implement reusable atomic widgets (delegate to UI Kit package)'], 'implementation_patterns': ['Composition Root', 'Router Pattern', 'Dependency Injection (Riverpod)'], 'extraction_reasoning': 'The shell acts as the glue code connecting specialized libraries into a runnable app.'}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IAuthRepository

#### 1.5.1.2 Source Repository

REPO-FE-LIB-APICLIENT

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

authStateChanges

###### 1.5.1.3.1.2 Method Signature

Stream<User?> get authStateChanges

###### 1.5.1.3.1.3 Method Purpose

Emits events when login/logout occurs to trigger router redirection.

###### 1.5.1.3.1.4 Integration Context

Consumed by `AppRouter` via Riverpod provider.

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

getUserProfile

###### 1.5.1.3.2.2 Method Signature

Future<UserProfile?> getUserProfile()

###### 1.5.1.3.2.3 Method Purpose

Fetches profile to check `hasCompletedOnboarding` flag.

###### 1.5.1.3.2.4 Integration Context

Called during AppBootstrap to determine initial route.

#### 1.5.1.4.0.0 Integration Pattern

Dart Package Import / DI

#### 1.5.1.5.0.0 Communication Protocol

In-Process Asynchronous Stream

#### 1.5.1.6.0.0 Extraction Reasoning

Routing logic depends entirely on the authentication state managed by the API Client.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

AppThemeFactory

#### 1.5.2.2.0.0 Source Repository

REPO-FE-LIB-UIKIT

#### 1.5.2.3.0.0 Method Contracts

##### 1.5.2.3.1.0 Method Name

###### 1.5.2.3.1.1 Method Name

lightTheme

###### 1.5.2.3.1.2 Method Signature

ThemeData get lightTheme

###### 1.5.2.3.1.3 Method Purpose

Provides the standardized light theme definition.

###### 1.5.2.3.1.4 Integration Context

Injected into `MaterialApp.theme`.

##### 1.5.2.3.2.0 Method Name

###### 1.5.2.3.2.1 Method Name

darkTheme

###### 1.5.2.3.2.2 Method Signature

ThemeData get darkTheme

###### 1.5.2.3.2.3 Method Purpose

Provides the standardized dark theme definition.

###### 1.5.2.3.2.4 Integration Context

Injected into `MaterialApp.darkTheme`.

#### 1.5.2.4.0.0 Integration Pattern

Dart Package Import

#### 1.5.2.5.0.0 Communication Protocol

In-Process Static Access

#### 1.5.2.6.0.0 Extraction Reasoning

Global styling consistency requires consuming the UI Kit's theme definitions.

## 1.6.0.0.0.0 Exposed Interfaces

- {'interface_name': 'Flutter Application Entry', 'consumer_repositories': ['Android OS', 'iOS'], 'method_contracts': [{'method_name': 'main', 'method_signature': 'void main()', 'method_purpose': 'Application entry point.', 'implementation_requirements': 'Must call AppBootstrap and runApp(ProviderScope(...)).'}], 'service_level_requirements': ['Cold start time < 2s'], 'implementation_constraints': ['Must catch top-level exceptions'], 'extraction_reasoning': 'Standard Flutter embedding interface.'}

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

Flutter 3.22+, Dart 3.4+

### 1.7.2.0.0.0 Integration Technologies

- Riverpod (DI/State)
- GoRouter (Navigation)
- FlutterSecureStorage (Persisted State)

### 1.7.3.0.0.0 Performance Constraints

Router parsing and redirect logic must be synchronous or highly optimized (<16ms) to prevent frame drops during navigation.

### 1.7.4.0.0.0 Security Requirements

Environment variables (API Keys, Base URLs) must be injected at build time using `--dart-define`.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Verified routing, bootstrapping, and dependency in... |
| Cross Reference Validation | Confirmed consumption of `REPO-FE-LIB-APICLIENT` f... |
| Implementation Readiness Assessment | High. Clear separation of concerns and well-define... |
| Quality Assurance Confirmation | Integration architecture ensures modularity and te... |

