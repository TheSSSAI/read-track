# 1 Id

REPO-BE-HOST

# 2 Name

readtrack-backend-host

# 3 Description

This repository serves as the composition root and host for the .NET 8 modular monolith. It does not contain any business logic itself. Instead, its primary responsibility is to orchestrate and integrate all the decomposed business domain modules (e.g., Users, Monetization, Reading) and shared libraries into a single, deployable application. It manages application startup, configuration, dependency injection wiring, middleware pipeline setup (authentication, logging, CORS), and hosting concerns (Kestrel, ASP.NET Core). By centralizing these concerns, it allows the individual business modules to remain focused purely on their domain logic, free from boilerplate hosting code. This repository effectively replaces the shell of the original monolithic `readtrack-backend-api`.

# 4 Type

🔹 Application Services

# 5 Namespace

ReadTrack.Api

# 6 Output Path

solution/backend/host

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

ASP.NET Core 8

# 10 Thirdparty Libraries

- Microsoft.AspNetCore
- Serilog.AspNetCore
- Swashbuckle.AspNetCore

# 11 Layer Ids

- presentation

# 12 Dependencies

- REPO-BE-MOD-USERS
- REPO-BE-MOD-MONETIZATION
- REPO-BE-MOD-READING
- REPO-BE-MOD-ENGAGEMENT
- REPO-BE-MOD-RECOMMENDATIONS
- REPO-BE-LIB-INFRA

# 13 Requirements

*No items available*

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Modular Monolith

# 17 Architecture Map

- api-gateway-001

# 18 Components Map

*No items available*

# 19 Requirements Map

*No items available*

# 20 Decomposition Rationale

## 20.1 Operation Type

RESTRUCTURED_HOST

## 20.2 Source Repository

REPO-BE-API

## 20.3 Decomposition Reasoning

The original monolith's responsibilities were overloaded. This host was created to serve the single responsibility of application composition and hosting, separating it from the business logic which was decomposed into dedicated module repositories. This clean separation improves maintainability and clarifies the application's entry point and overall structure.

## 20.4 Extracted Responsibilities

- Application Startup & Configuration
- Middleware Pipeline Configuration
- Dependency Injection Composition Root

## 20.5 Reusability Scope

- Not applicable, this is a top-level host.

## 20.6 Development Benefits

- Simplifies the main application entry point.
- Decouples business logic from hosting concerns.
- Provides a single place to manage application-wide configuration.

# 21.0 Dependency Contracts

*No data available*

# 22.0 Exposed Contracts

*No data available*

# 23.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Acts as the central DI container configuration poi... |
| Event Communication | Does not initiate events but ensures the event bus... |
| Data Flow | Manages the incoming HTTP request pipeline, routin... |
| Error Handling | Configures global exception handling middleware. |
| Async Patterns | Manages the hosting environment for async ASP.NET ... |

# 24.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Focus on configuring services in Program.cs and ap... |
| Performance Considerations | Optimize application startup time and middleware p... |
| Security Considerations | Ensure security headers, CORS policies, and authen... |
| Testing Approach | Focus on integration tests that verify the applica... |

# 25.0 Scope Boundaries

## 25.1 Must Implement

- Application entry point (Program.cs)
- Configuration loading (appsettings)
- Service registration for all modules
- Middleware pipeline definition

## 25.2 Must Not Implement

- Business logic (Controllers, Services, Use Cases)
- Database entities or DbContexts
- Direct calls to external services

## 25.3 Extension Points

- Adding new business modules by referencing their projects.
- Adding new cross-cutting middleware.

## 25.4 Validation Rules

*No items available*

