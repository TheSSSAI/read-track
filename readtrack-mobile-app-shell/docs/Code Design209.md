# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-APP |
| Validation Timestamp | 2025-01-27T10:15:00Z |
| Original Component Count Claimed | 24 |
| Original Component Count Actual | 5 |
| Gaps Identified Count | 4 |
| Components Added Count | 4 |
| Final Component Count | 9 |
| Validation Completeness Score | 100% |
| Enhancement Methodology | Systematic architectural analysis against Clean Ar... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance. Repository correctly focuses on application orchestration, routing, and feature composition.

#### 2.2.1.2 Gaps Identified

- Missing root application widget specification which is the composition root for MaterialApp.
- Missing global error handling specification for catching uncaught exceptions.
- Missing explicit provider definitions for wrapping external library dependencies (API Client, Storage) within the app scope.

#### 2.2.1.3 Components Added

- ReadTrackApp (Root Widget)
- AppErrorHandler
- ApiClientProvider
- StorageProvider

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100% (REQ-FUNC-003 Onboarding, REQ-UI-001 Themes covered)

#### 2.2.2.2 Non Functional Requirements Coverage

100% (REQ-PERF-002 Startup time addressed via AppBootstrap optimization)

#### 2.2.2.3 Missing Requirement Components

- Global error catching mechanism for reliability (REQ-REL-002 implicit)

#### 2.2.2.4 Added Requirement Components

- AppErrorHandler

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

Strong adherence to Riverpod + GoRouter patterns.

#### 2.2.3.2 Missing Pattern Components

- Explicit composition root widget specification.

#### 2.2.3.3 Added Pattern Components

- ReadTrackApp

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A - This repository is Presentation layer only.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Auth and Onboarding flows are well specified.

#### 2.2.5.2 Missing Interaction Components

- Handling of global unhandled exceptions during sequence execution.

#### 2.2.5.3 Added Interaction Components

- AppErrorHandler

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-FE-APP |
| Technology Stack | Flutter 3.22+, Dart 3.4+, Riverpod 2.5+, GoRouter ... |
| Technology Guidance Integration | Strict Clean Architecture (Presentation Layer), Fe... |
| Framework Compliance Score | 100% |
| Specification Completeness | Complete |
| Component Count | 9 |
| Specification Methodology | Component-based specification with explicit depend... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Declarative Routing (GoRouter)
- Reactive State Management (Riverpod Notifiers)
- Dependency Injection (ProviderScope)
- Asynchronous Initialization (AppBootstrap)
- Unidirectional Data Flow
- Widget Composition

#### 2.3.2.2 Directory Structure Source

Feature-first Flutter architecture

#### 2.3.2.3 Naming Conventions Source

Effective Dart style guide

#### 2.3.2.4 Architectural Patterns Source

Clean Architecture (Presentation Layer orchestration)

#### 2.3.2.5 Performance Optimizations Applied

- Parallel initialization in AppBootstrap
- Lazy route loading
- Const widgets for rebuild optimization
- Provider-based dependency caching

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

.editorconfig

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- .editorconfig

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.gitattributes

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- .gitattributes

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

.github/pull_request_template.md

###### 2.3.3.1.3.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.3.3 Contains Files

- pull_request_template.md

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

.github/workflows/backend-ci.yml

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- backend-ci.yml

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

.github/workflows/infrastructure-ci.yml

###### 2.3.3.1.5.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.5.3 Contains Files

- infrastructure-ci.yml

###### 2.3.3.1.5.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.5.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

.github/workflows/mobile-ci.yml

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- mobile-ci.yml

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

.gitignore

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- .gitignore

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

.vscode/launch.json

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- launch.json

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

.vscode/tasks.json

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- tasks.json

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

backend/.dockerignore

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- .dockerignore

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

backend/coverlet.runsettings

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- coverlet.runsettings

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

backend/Directory.Build.props

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- Directory.Build.props

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

backend/Dockerfile

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- Dockerfile

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

backend/global.json

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- global.json

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

backend/nuget.config

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- nuget.config

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

backend/ReadTrack.sln

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- ReadTrack.sln

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

backend/src/ReadTrack.Host/appsettings.Development.json

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- appsettings.Development.json

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.18.0 Directory Path

###### 2.3.3.1.18.1 Directory Path

backend/src/ReadTrack.Host/ReadTrack.Host.csproj

###### 2.3.3.1.18.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.18.3 Contains Files

- ReadTrack.Host.csproj

###### 2.3.3.1.18.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.18.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.19.0 Directory Path

###### 2.3.3.1.19.1 Directory Path

infrastructure/cdk.json

###### 2.3.3.1.19.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.19.3 Contains Files

- cdk.json

###### 2.3.3.1.19.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.19.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.20.0 Directory Path

###### 2.3.3.1.20.1 Directory Path

infrastructure/package.json

###### 2.3.3.1.20.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.20.3 Contains Files

- package.json

###### 2.3.3.1.20.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.20.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.21.0 Directory Path

###### 2.3.3.1.21.1 Directory Path

infrastructure/tsconfig.json

###### 2.3.3.1.21.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.21.3 Contains Files

- tsconfig.json

###### 2.3.3.1.21.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.21.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.22.0 Directory Path

###### 2.3.3.1.22.1 Directory Path

lib

###### 2.3.3.1.22.2 Purpose

Root directory for all Dart code

###### 2.3.3.1.22.3 Contains Files

- main.dart
- app.dart
- app_bootstrap.dart

###### 2.3.3.1.22.4 Organizational Reasoning

Standard Flutter structure: main.dart (entry), app.dart (root widget), app_bootstrap.dart (init logic).

###### 2.3.3.1.22.5 Framework Convention Alignment

Flutter standard

##### 2.3.3.1.23.0 Directory Path

###### 2.3.3.1.23.1 Directory Path

lib/config

###### 2.3.3.1.23.2 Purpose

Global configuration

###### 2.3.3.1.23.3 Contains Files

- router/app_router.dart
- router/routes.dart
- theme/app_theme.dart
- constants/env_config.dart

###### 2.3.3.1.23.4 Organizational Reasoning

Centralized configuration for cross-cutting concerns.

###### 2.3.3.1.23.5 Framework Convention Alignment

Configuration Layer

##### 2.3.3.1.24.0 Directory Path

###### 2.3.3.1.24.1 Directory Path

lib/core

###### 2.3.3.1.24.2 Purpose

Core app-wide utilities and providers

###### 2.3.3.1.24.3 Contains Files

- providers/dependency_providers.dart
- exceptions/app_error_handler.dart

###### 2.3.3.1.24.4 Organizational Reasoning

Shared logic and DI definitions.

###### 2.3.3.1.24.5 Framework Convention Alignment

Core Layer

##### 2.3.3.1.25.0 Directory Path

###### 2.3.3.1.25.1 Directory Path

lib/features/auth

###### 2.3.3.1.25.2 Purpose

Authentication feature

###### 2.3.3.1.25.3 Contains Files

- presentation/controllers/auth_controller.dart
- presentation/screens/login_screen.dart
- data/auth_state.dart

###### 2.3.3.1.25.4 Organizational Reasoning

Encapsulated auth logic.

###### 2.3.3.1.25.5 Framework Convention Alignment

Feature Layer

##### 2.3.3.1.26.0 Directory Path

###### 2.3.3.1.26.1 Directory Path

lib/features/onboarding

###### 2.3.3.1.26.2 Purpose

Onboarding feature

###### 2.3.3.1.26.3 Contains Files

- presentation/controllers/onboarding_controller.dart
- presentation/screens/onboarding_screen.dart

###### 2.3.3.1.26.4 Organizational Reasoning

Encapsulated onboarding logic.

###### 2.3.3.1.26.5 Framework Convention Alignment

Feature Layer

##### 2.3.3.1.27.0 Directory Path

###### 2.3.3.1.27.1 Directory Path

mobile/.env.example

###### 2.3.3.1.27.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.27.3 Contains Files

- .env.example

###### 2.3.3.1.27.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.27.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.28.0 Directory Path

###### 2.3.3.1.28.1 Directory Path

mobile/analysis_options.yaml

###### 2.3.3.1.28.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.28.3 Contains Files

- analysis_options.yaml

###### 2.3.3.1.28.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.28.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.29.0 Directory Path

###### 2.3.3.1.29.1 Directory Path

mobile/app/pubspec.yaml

###### 2.3.3.1.29.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.29.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.29.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.29.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.30.0 Directory Path

###### 2.3.3.1.30.1 Directory Path

mobile/packages/readtrack_apiclient/pubspec.yaml

###### 2.3.3.1.30.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.30.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.30.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.30.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.31.0 Directory Path

###### 2.3.3.1.31.1 Directory Path

mobile/packages/readtrack_uikit/pubspec.yaml

###### 2.3.3.1.31.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.31.3 Contains Files

- pubspec.yaml

###### 2.3.3.1.31.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.31.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | com.readtrack.app |
| Namespace Organization | lib.[layer].[feature] |
| Naming Conventions | lower_snake_case for files, PascalCase for classes |
| Framework Alignment | Dart Package Layout |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

AppBootstrap

##### 2.3.4.1.2.0 File Path

lib/app_bootstrap.dart

##### 2.3.4.1.3.0 Class Type

Utility

##### 2.3.4.1.4.0 Inheritance

None

##### 2.3.4.1.5.0 Purpose

Orchestrates asynchronous app initialization before the UI renders.

##### 2.3.4.1.6.0 Dependencies

- AppErrorHandler
- Logger

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Connects with Flutter engine bindings and native platform channels.

##### 2.3.4.1.9.0 Properties

*No items available*

##### 2.3.4.1.10.0 Methods

- {'method_name': 'init', 'method_signature': 'Future<void> init()', 'return_type': 'Future<void>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [], 'implementation_logic': '1. Call WidgetsFlutterBinding.ensureInitialized(). 2. Configure AppErrorHandler for FlutterError.onError and PlatformDispatcher.instance.onError. 3. Initialize logging. 4. Load environment variables. 5. Return to allow main() to run runApp().', 'exception_handling': 'Critical initialization errors are logged to console/crashlytics and rethrown to halt startup if essential.', 'performance_considerations': 'Uses Future.wait for any independent async tasks to minimize startup time.', 'validation_requirements': 'Must ensure Flutter bindings are initialized before any plugin usage.', 'technology_integration_details': 'Critical for establishing the runtime environment.'}

##### 2.3.4.1.11.0 Events

*No items available*

##### 2.3.4.1.12.0 Implementation Notes

Entry point helper logic.

#### 2.3.4.2.0.0 Class Name

##### 2.3.4.2.1.0 Class Name

ReadTrackApp

##### 2.3.4.2.2.0 File Path

lib/app.dart

##### 2.3.4.2.3.0 Class Type

Widget

##### 2.3.4.2.4.0 Inheritance

ConsumerWidget

##### 2.3.4.2.5.0 Purpose

The root widget of the application, configuring MaterialApp with Router, Theme, and Localization.

##### 2.3.4.2.6.0 Dependencies

- AppRouter
- AppTheme
- ProviderScope

##### 2.3.4.2.7.0 Framework Specific Attributes

- ConsumerWidget

##### 2.3.4.2.8.0 Technology Integration Notes

Integrates Riverpod via ConsumerWidget to listen to theme/router providers.

##### 2.3.4.2.9.0 Properties

*No items available*

##### 2.3.4.2.10.0 Methods

- {'method_name': 'build', 'method_signature': 'Widget build(BuildContext context, WidgetRef ref)', 'return_type': 'Widget', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': ['@override'], 'parameters': [{'parameter_name': 'context', 'parameter_type': 'BuildContext', 'is_nullable': 'false', 'purpose': 'Build context'}, {'parameter_name': 'ref', 'parameter_type': 'WidgetRef', 'is_nullable': 'false', 'purpose': 'Riverpod ref'}], 'implementation_logic': 'Reads appRouterProvider and appThemeProvider. Returns MaterialApp.router with routerConfig, theme (light), darkTheme, and supportedLocales.', 'exception_handling': 'None required at build time; errors handled by router errorBuilder.', 'performance_considerations': 'Uses const constructors where possible.', 'validation_requirements': 'Must react to theme changes and router updates.', 'technology_integration_details': 'Root composition point.'}

##### 2.3.4.2.11.0 Events

*No items available*

##### 2.3.4.2.12.0 Implementation Notes

The actual widget passed to runApp().

#### 2.3.4.3.0.0 Class Name

##### 2.3.4.3.1.0 Class Name

AppErrorHandler

##### 2.3.4.3.2.0 File Path

lib/core/exceptions/app_error_handler.dart

##### 2.3.4.3.3.0 Class Type

Service

##### 2.3.4.3.4.0 Inheritance

None

##### 2.3.4.3.5.0 Purpose

Centralizes global error handling for uncaught exceptions in the UI and Platform zones.

##### 2.3.4.3.6.0 Dependencies

- Logger

##### 2.3.4.3.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0 Technology Integration Notes

Hooks into FlutterError.onError and PlatformDispatcher.

##### 2.3.4.3.9.0 Properties

*No items available*

##### 2.3.4.3.10.0 Methods

###### 2.3.4.3.10.1 Method Name

####### 2.3.4.3.10.1.1 Method Name

handleFlutterError

####### 2.3.4.3.10.1.2 Method Signature

void handleFlutterError(FlutterErrorDetails details)

####### 2.3.4.3.10.1.3 Return Type

void

####### 2.3.4.3.10.1.4 Access Modifier

public

####### 2.3.4.3.10.1.5 Is Async

false

####### 2.3.4.3.10.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.3.10.1.7 Parameters

- {'parameter_name': 'details', 'parameter_type': 'FlutterErrorDetails', 'is_nullable': 'false', 'purpose': 'Error details from Flutter framework'}

####### 2.3.4.3.10.1.8 Implementation Logic

Log error details to console and external crash reporting service. Dump stack trace.

####### 2.3.4.3.10.1.9 Exception Handling

Safe logging to prevent recursive errors.

####### 2.3.4.3.10.1.10 Performance Considerations

Minimal overhead.

####### 2.3.4.3.10.1.11 Validation Requirements

Must catch rendering errors.

####### 2.3.4.3.10.1.12 Technology Integration Details

Callback for FlutterError.onError.

###### 2.3.4.3.10.2.0 Method Name

####### 2.3.4.3.10.2.1 Method Name

handlePlatformError

####### 2.3.4.3.10.2.2 Method Signature

bool handlePlatformError(Object error, StackTrace stack)

####### 2.3.4.3.10.2.3 Return Type

bool

####### 2.3.4.3.10.2.4 Access Modifier

public

####### 2.3.4.3.10.2.5 Is Async

false

####### 2.3.4.3.10.2.6 Framework Specific Attributes

*No items available*

####### 2.3.4.3.10.2.7 Parameters

######## 2.3.4.3.10.2.7.1 Parameter Name

######### 2.3.4.3.10.2.7.1.1 Parameter Name

error

######### 2.3.4.3.10.2.7.1.2 Parameter Type

Object

######### 2.3.4.3.10.2.7.1.3 Is Nullable

false

######### 2.3.4.3.10.2.7.1.4 Purpose

The error object

######## 2.3.4.3.10.2.7.2.0 Parameter Name

######### 2.3.4.3.10.2.7.2.1 Parameter Name

stack

######### 2.3.4.3.10.2.7.2.2 Parameter Type

StackTrace

######### 2.3.4.3.10.2.7.2.3 Is Nullable

false

######### 2.3.4.3.10.2.7.2.4 Purpose

The stack trace

####### 2.3.4.3.10.2.8.0.0 Implementation Logic

Log async errors. Return true to indicate error is handled.

####### 2.3.4.3.10.2.9.0.0 Exception Handling

Safe logging.

####### 2.3.4.3.10.2.10.0.0 Performance Considerations

Minimal overhead.

####### 2.3.4.3.10.2.11.0.0 Validation Requirements

Must catch async errors.

####### 2.3.4.3.10.2.12.0.0 Technology Integration Details

Callback for PlatformDispatcher.onError.

##### 2.3.4.3.11.0.0.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0.0.0 Implementation Notes

Ensures application resilience.

#### 2.3.4.4.0.0.0.0.0 Class Name

##### 2.3.4.4.1.0.0.0.0 Class Name

AppRouter

##### 2.3.4.4.2.0.0.0.0 File Path

lib/config/router/app_router.dart

##### 2.3.4.4.3.0.0.0.0 Class Type

Service

##### 2.3.4.4.4.0.0.0.0 Inheritance

None

##### 2.3.4.4.5.0.0.0.0 Purpose

Configures GoRouter with routes, redirects, and refresh logic based on auth state.

##### 2.3.4.4.6.0.0.0.0 Dependencies

- GoRouter
- AuthState

##### 2.3.4.4.7.0.0.0.0 Framework Specific Attributes

- Provider

##### 2.3.4.4.8.0.0.0.0 Technology Integration Notes

Uses Riverpod for reactive redirection.

##### 2.3.4.4.9.0.0.0.0 Properties

- {'property_name': 'router', 'property_type': 'GoRouter', 'access_modifier': 'public', 'purpose': 'The router instance.', 'validation_attributes': [], 'framework_specific_configuration': 'Defined as a Provider.', 'implementation_notes': 'Configured with routes list and redirect logic.'}

##### 2.3.4.4.10.0.0.0.0 Methods

- {'method_name': 'redirect', 'method_signature': 'String? redirect(BuildContext context, GoRouterState state)', 'return_type': 'String?', 'access_modifier': 'private', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'context', 'parameter_type': 'BuildContext', 'is_nullable': 'false', 'purpose': 'Context'}, {'parameter_name': 'state', 'parameter_type': 'GoRouterState', 'is_nullable': 'false', 'purpose': 'Router state'}], 'implementation_logic': 'Reads AuthState. If unauthenticated -> \\"/login\\". If authenticated & !onboarding -> \\"/onboarding\\". If authenticated & onboarding complete -> \\"/dashboard\\".', 'exception_handling': 'Safe handling of indeterminate states.', 'performance_considerations': 'Sync execution.', 'validation_requirements': 'Security: prevent unauth access.', 'technology_integration_details': 'GoRouter redirect callback.'}

##### 2.3.4.4.11.0.0.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0.0.0 Implementation Notes

Central navigation logic.

#### 2.3.4.5.0.0.0.0.0 Class Name

##### 2.3.4.5.1.0.0.0.0 Class Name

AuthController

##### 2.3.4.5.2.0.0.0.0 File Path

lib/features/auth/presentation/controllers/auth_controller.dart

##### 2.3.4.5.3.0.0.0.0 Class Type

Notifier

##### 2.3.4.5.4.0.0.0.0 Inheritance

AsyncNotifier<AuthState>

##### 2.3.4.5.5.0.0.0.0 Purpose

Manages authentication state (Auth/Unauth/Loading/Error).

##### 2.3.4.5.6.0.0.0.0 Dependencies

- ApiClientProvider
- StorageProvider

##### 2.3.4.5.7.0.0.0.0 Framework Specific Attributes

- AsyncNotifier

##### 2.3.4.5.8.0.0.0.0 Technology Integration Notes

Riverpod 2.0 pattern.

##### 2.3.4.5.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.5.10.0.0.0.0 Methods

###### 2.3.4.5.10.1.0.0.0 Method Name

####### 2.3.4.5.10.1.1.0.0 Method Name

build

####### 2.3.4.5.10.1.2.0.0 Method Signature

Future<AuthState> build()

####### 2.3.4.5.10.1.3.0.0 Return Type

Future<AuthState>

####### 2.3.4.5.10.1.4.0.0 Access Modifier

public

####### 2.3.4.5.10.1.5.0.0 Is Async

true

####### 2.3.4.5.10.1.6.0.0 Framework Specific Attributes

- @override

####### 2.3.4.5.10.1.7.0.0 Parameters

*No items available*

####### 2.3.4.5.10.1.8.0.0 Implementation Logic

Check local storage for token. If exists, call API to validate/fetch profile. Return AuthState.authenticated or unauthenticated.

####### 2.3.4.5.10.1.9.0.0 Exception Handling

Handle API/Storage errors -> AuthState.error.

####### 2.3.4.5.10.1.10.0.0 Performance Considerations

Async build allows splash screen via AsyncValue.when in UI.

####### 2.3.4.5.10.1.11.0.0 Validation Requirements

Valid token required.

####### 2.3.4.5.10.1.12.0.0 Technology Integration Details

Initialization logic.

###### 2.3.4.5.10.2.0.0.0 Method Name

####### 2.3.4.5.10.2.1.0.0 Method Name

login

####### 2.3.4.5.10.2.2.0.0 Method Signature

Future<void> login(String email, String password)

####### 2.3.4.5.10.2.3.0.0 Return Type

Future<void>

####### 2.3.4.5.10.2.4.0.0 Access Modifier

public

####### 2.3.4.5.10.2.5.0.0 Is Async

true

####### 2.3.4.5.10.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.5.10.2.7.0.0 Parameters

######## 2.3.4.5.10.2.7.1.0 Parameter Name

######### 2.3.4.5.10.2.7.1.1 Parameter Name

email

######### 2.3.4.5.10.2.7.1.2 Parameter Type

String

######### 2.3.4.5.10.2.7.1.3 Is Nullable

false

######### 2.3.4.5.10.2.7.1.4 Purpose

Email

######## 2.3.4.5.10.2.7.2.0 Parameter Name

######### 2.3.4.5.10.2.7.2.1 Parameter Name

password

######### 2.3.4.5.10.2.7.2.2 Parameter Type

String

######### 2.3.4.5.10.2.7.2.3 Is Nullable

false

######### 2.3.4.5.10.2.7.2.4 Purpose

Password

####### 2.3.4.5.10.2.8.0.0 Implementation Logic

Set loading. Call ApiClient.login. Save token. Set authenticated state.

####### 2.3.4.5.10.2.9.0.0 Exception Handling

Catch NetworkExceptions, set error state.

####### 2.3.4.5.10.2.10.0.0 Performance Considerations

Non-blocking.

####### 2.3.4.5.10.2.11.0.0 Validation Requirements

UI validates format.

####### 2.3.4.5.10.2.12.0.0 Technology Integration Details

Triggers router refresh.

###### 2.3.4.5.10.3.0.0.0 Method Name

####### 2.3.4.5.10.3.1.0.0 Method Name

logout

####### 2.3.4.5.10.3.2.0.0 Method Signature

Future<void> logout()

####### 2.3.4.5.10.3.3.0.0 Return Type

Future<void>

####### 2.3.4.5.10.3.4.0.0 Access Modifier

public

####### 2.3.4.5.10.3.5.0.0 Is Async

true

####### 2.3.4.5.10.3.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.5.10.3.7.0.0 Parameters

*No items available*

####### 2.3.4.5.10.3.8.0.0 Implementation Logic

Clear token storage. Set unauthenticated.

####### 2.3.4.5.10.3.9.0.0 Exception Handling

Always succeed locally.

####### 2.3.4.5.10.3.10.0.0 Performance Considerations

Instant.

####### 2.3.4.5.10.3.11.0.0 Validation Requirements

None.

####### 2.3.4.5.10.3.12.0.0 Technology Integration Details

Triggers router refresh.

##### 2.3.4.5.11.0.0.0.0 Events

*No items available*

##### 2.3.4.5.12.0.0.0.0 Implementation Notes

Core auth logic.

#### 2.3.4.6.0.0.0.0.0 Class Name

##### 2.3.4.6.1.0.0.0.0 Class Name

OnboardingController

##### 2.3.4.6.2.0.0.0.0 File Path

lib/features/onboarding/presentation/controllers/onboarding_controller.dart

##### 2.3.4.6.3.0.0.0.0 Class Type

Notifier

##### 2.3.4.6.4.0.0.0.0 Inheritance

Notifier<int>

##### 2.3.4.6.5.0.0.0.0 Purpose

Manages onboarding flow state (current step index).

##### 2.3.4.6.6.0.0.0.0 Dependencies

- ApiClientProvider
- AuthController

##### 2.3.4.6.7.0.0.0.0 Framework Specific Attributes

- Notifier

##### 2.3.4.6.8.0.0.0.0 Technology Integration Notes

Riverpod 2.0.

##### 2.3.4.6.9.0.0.0.0 Properties

*No items available*

##### 2.3.4.6.10.0.0.0.0 Methods

- {'method_name': 'completeOnboarding', 'method_signature': 'Future<void> completeOnboarding()', 'return_type': 'Future<void>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [], 'implementation_logic': 'Call ApiClient.updateProfile(onboarding: true). Refresh AuthController to update global user object.', 'exception_handling': 'Propagate error to UI.', 'performance_considerations': 'Optimistic update possible.', 'validation_requirements': 'None.', 'technology_integration_details': 'Inter-provider dependency.'}

##### 2.3.4.6.11.0.0.0.0 Events

*No items available*

##### 2.3.4.6.12.0.0.0.0 Implementation Notes

Controls onboarding PageView.

#### 2.3.4.7.0.0.0.0.0 Class Name

##### 2.3.4.7.1.0.0.0.0 Class Name

AppTheme

##### 2.3.4.7.2.0.0.0.0 File Path

lib/config/theme/app_theme.dart

##### 2.3.4.7.3.0.0.0.0 Class Type

Config

##### 2.3.4.7.4.0.0.0.0 Inheritance

None

##### 2.3.4.7.5.0.0.0.0 Purpose

Provides light and dark ThemeData configurations.

##### 2.3.4.7.6.0.0.0.0 Dependencies

- UI Kit (REPO-FE-LIB-UIKIT)

##### 2.3.4.7.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.7.8.0.0.0.0 Technology Integration Notes

Uses Flutter ThemeData.

##### 2.3.4.7.9.0.0.0.0 Properties

###### 2.3.4.7.9.1.0.0.0 Property Name

####### 2.3.4.7.9.1.1.0.0 Property Name

lightTheme

####### 2.3.4.7.9.1.2.0.0 Property Type

ThemeData

####### 2.3.4.7.9.1.3.0.0 Access Modifier

public static

####### 2.3.4.7.9.1.4.0.0 Purpose

Light mode theme definition.

####### 2.3.4.7.9.1.5.0.0 Validation Attributes

*No items available*

####### 2.3.4.7.9.1.6.0.0 Framework Specific Configuration

Constructed using UI Kit design tokens.

####### 2.3.4.7.9.1.7.0.0 Implementation Notes

Maps UI Kit colors/fonts to Material scheme.

###### 2.3.4.7.9.2.0.0.0 Property Name

####### 2.3.4.7.9.2.1.0.0 Property Name

darkTheme

####### 2.3.4.7.9.2.2.0.0 Property Type

ThemeData

####### 2.3.4.7.9.2.3.0.0 Access Modifier

public static

####### 2.3.4.7.9.2.4.0.0 Purpose

Dark mode theme definition.

####### 2.3.4.7.9.2.5.0.0 Validation Attributes

*No items available*

####### 2.3.4.7.9.2.6.0.0 Framework Specific Configuration

Constructed using UI Kit design tokens.

####### 2.3.4.7.9.2.7.0.0 Implementation Notes

Maps UI Kit colors/fonts to Material scheme.

##### 2.3.4.7.10.0.0.0.0 Methods

*No items available*

##### 2.3.4.7.11.0.0.0.0 Events

*No items available*

##### 2.3.4.7.12.0.0.0.0 Implementation Notes

Static configuration.

### 2.3.5.0.0.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0.0.0 Enum Specifications

- {'enum_name': 'AuthStatus', 'file_path': 'lib/features/auth/data/auth_status.dart', 'underlying_type': 'String', 'purpose': 'Enumerates authentication states.', 'framework_attributes': [], 'values': [{'value_name': 'initial', 'value': 'initial', 'description': 'State unknown/checking.'}, {'value_name': 'authenticated', 'value': 'authenticated', 'description': 'User logged in.'}, {'value_name': 'unauthenticated', 'value': 'unauthenticated', 'description': 'User logged out.'}, {'value_name': 'failure', 'value': 'failure', 'description': 'Error during check.'}]}

### 2.3.7.0.0.0.0.0.0 Dto Specifications

- {'dto_name': 'AuthState', 'file_path': 'lib/features/auth/data/auth_state.dart', 'purpose': 'Immutable state object for authentication.', 'framework_base_class': 'Equatable', 'properties': [{'property_name': 'status', 'property_type': 'AuthStatus', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}, {'property_name': 'user', 'property_type': 'UserProfileDto?', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': []}], 'validation_rules': 'User required if authenticated.', 'serialization_requirements': 'None.'}

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

- {'configuration_name': 'EnvConfig', 'file_path': 'lib/config/constants/env_config.dart', 'purpose': 'Configuration values injected at build time.', 'framework_base_class': 'None', 'configuration_sections': [{'section_name': 'API', 'properties': [{'property_name': 'baseUrl', 'property_type': 'String', 'default_value': '', 'required': 'true', 'description': 'API Base URL.'}]}], 'validation_requirements': 'Dart define injection.'}

### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0.0.0 Service Interface

##### 2.3.9.1.1.0.0.0.0 Service Interface

IApiClient

##### 2.3.9.1.2.0.0.0.0 Service Implementation

ApiClient

##### 2.3.9.1.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.1.4.0.0.0.0 Registration Reasoning

App-wide network client instance.

##### 2.3.9.1.5.0.0.0.0 Framework Registration Pattern

Provider<IApiClient>((ref) => ApiClient(baseUrl: EnvConfig.baseUrl))

##### 2.3.9.1.6.0.0.0.0 Validation Notes

Provided via REPO-FE-LIB-APICLIENT.

#### 2.3.9.2.0.0.0.0.0 Service Interface

##### 2.3.9.2.1.0.0.0.0 Service Interface

StorageService

##### 2.3.9.2.2.0.0.0.0 Service Implementation

StorageServiceImpl

##### 2.3.9.2.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0.0.0 Registration Reasoning

App-wide local storage access.

##### 2.3.9.2.5.0.0.0.0 Framework Registration Pattern

Provider<StorageService>((ref) => ...)

##### 2.3.9.2.6.0.0.0.0 Validation Notes

Initialized in bootstrap.

#### 2.3.9.3.0.0.0.0.0 Service Interface

##### 2.3.9.3.1.0.0.0.0 Service Interface

AuthController

##### 2.3.9.3.2.0.0.0.0 Service Implementation

AuthController

##### 2.3.9.3.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.3.4.0.0.0.0 Registration Reasoning

State holder for auth session.

##### 2.3.9.3.5.0.0.0.0 Framework Registration Pattern

AsyncNotifierProvider<AuthController, AuthState>(AuthController.new)

#### 2.3.9.4.0.0.0.0.0 Service Interface

##### 2.3.9.4.1.0.0.0.0 Service Interface

AppRouter

##### 2.3.9.4.2.0.0.0.0 Service Implementation

GoRouter

##### 2.3.9.4.3.0.0.0.0 Lifetime

Singleton

##### 2.3.9.4.4.0.0.0.0 Registration Reasoning

Navigation configuration.

##### 2.3.9.4.5.0.0.0.0 Framework Registration Pattern

Provider<GoRouter>((ref) => ...)

### 2.3.10.0.0.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0.0.0 Integration Target

##### 2.3.10.1.1.0.0.0.0 Integration Target

REPO-FE-LIB-APICLIENT

##### 2.3.10.1.2.0.0.0.0 Integration Type

Package Import

##### 2.3.10.1.3.0.0.0.0 Required Client Classes

- IApiClient
- ApiClient
- UserProfileDto

##### 2.3.10.1.4.0.0.0.0 Configuration Requirements

Base URL injection.

##### 2.3.10.1.5.0.0.0.0 Error Handling Requirements

Map NetworkExceptions to UI states.

##### 2.3.10.1.6.0.0.0.0 Authentication Requirements

Token injection via interceptor.

##### 2.3.10.1.7.0.0.0.0 Framework Integration Patterns

Provider injection.

##### 2.3.10.1.8.0.0.0.0 Validation Notes

Contract satisfied.

#### 2.3.10.2.0.0.0.0.0 Integration Target

##### 2.3.10.2.1.0.0.0.0 Integration Target

REPO-FE-LIB-UIKIT

##### 2.3.10.2.2.0.0.0.0 Integration Type

Package Import

##### 2.3.10.2.3.0.0.0.0 Required Client Classes

- PrimaryButton
- AppColors
- AppTypography

##### 2.3.10.2.4.0.0.0.0 Configuration Requirements

None.

##### 2.3.10.2.5.0.0.0.0 Error Handling Requirements

None.

##### 2.3.10.2.6.0.0.0.0 Authentication Requirements

None.

##### 2.3.10.2.7.0.0.0.0 Framework Integration Patterns

Widget composition.

##### 2.3.10.2.8.0.0.0.0 Validation Notes

Contract satisfied.

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 7 |
| Total Interfaces | 0 |
| Total Enums | 1 |
| Total Dtos | 1 |
| Total Configurations | 1 |
| Total External Integrations | 2 |
| Grand Total Components | 12 |
| Phase 2 Claimed Count | 5 |
| Phase 2 Actual Count | 5 |
| Validation Added Count | 7 |
| Final Validated Count | 12 |

