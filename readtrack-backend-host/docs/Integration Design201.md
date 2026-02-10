# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-HOST |
| Extraction Timestamp | 2025-05-15T14:30:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-SYS-001

#### 1.2.1.2 Requirement Text

Application Startup and Module Loading

#### 1.2.1.3 Validation Criteria

- All modules must be loaded deterministically at startup
- Configuration must be validated before the server starts accepting requests

#### 1.2.1.4 Implementation Implications

- Implement a 'ModuleLoader' or extension method pattern in Program.cs to register services from all referenced projects
- Aggregated 'appsettings.json' must contain sections for Users, Reading, and Monetization modules

#### 1.2.1.5 Extraction Reasoning

The Host acts as the composition root, responsible for wiring together the modular monolith components defined in other repositories.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-SEC-001

#### 1.2.2.2 Requirement Text

The system must use JWT (JSON Web Token) based authentication for the backend API.

#### 1.2.2.3 Validation Criteria

- Authentication middleware must validate Auth0-issued tokens
- Authorization policies (Free vs Premium) must be globally configured

#### 1.2.2.4 Implementation Implications

- Register 'JwtBearer' authentication in the DI container
- Configure CORS policies to allow traffic from the Flutter mobile application

#### 1.2.2.5 Extraction Reasoning

Security is a cross-cutting concern managed at the entry point (Host) to protect all underlying business modules.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-MON-001

#### 1.2.3.2 Requirement Text

The system must log security-sensitive events and performance metrics for monitoring.

#### 1.2.3.3 Validation Criteria

- Structured logging (Serilog) must be initialized before the host starts
- Correlation IDs must be injected into the request context

#### 1.2.3.4 Implementation Implications

- Configure Serilog request logging middleware
- Expose Health Check endpoints for infrastructure monitoring

#### 1.2.3.5 Extraction Reasoning

The Host handles the HTTP request lifecycle and is the primary point for observability instrumentation.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

CompositionRoot

#### 1.3.1.2 Component Specification

The central bootstrapping logic that aggregates service registrations from all business modules.

#### 1.3.1.3 Implementation Requirements

- Reference all module assemblies
- Invoke 'Add[Module]Module' extension methods
- Register MediatR and scan all module assemblies for handlers

#### 1.3.1.4 Architectural Context

Application Infrastructure - Entry Point

#### 1.3.1.5 Extraction Reasoning

Essential for the Modular Monolith architecture to function as a single unit.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

MiddlewarePipeline

#### 1.3.2.2 Component Specification

Defines the sequence of HTTP message handlers (Security -> Logging -> Routing).

#### 1.3.2.3 Implementation Requirements

- Configure GlobalExceptionHandler for RFC 7807 responses
- Configure Authentication and Authorization middleware order
- Configure SwaggerUI for API documentation aggregation

#### 1.3.2.4 Architectural Context

Presentation Layer - Request Pipeline

#### 1.3.2.5 Extraction Reasoning

Ensures consistent processing of requests across all domains.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Host / API Gateway', 'layer_responsibilities': 'Bootstrapping, Configuration, Process Management, HTTP Listener (Kestrel).', 'layer_constraints': ['Must NOT contain business logic', 'Must depend on all Module projects (Users, Reading, Monetization, etc.)', "Must implement the 'Options Pattern' for configuration binding"], 'implementation_patterns': ['Composition Root', 'Middleware Chain', 'Hosting (IHost)'], 'extraction_reasoning': 'The repository serves as the shell for the application.'}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IModuleDependency

#### 1.5.1.2 Source Repository

REPO-BE-MOD-USERS

#### 1.5.1.3 Method Contracts

- {'method_name': 'AddUsersModule', 'method_signature': 'IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration config)', 'method_purpose': 'Registers User domain services and DB context into the host container.', 'integration_context': 'Program.cs startup sequence'}

#### 1.5.1.4 Integration Pattern

In-Process Dependency Injection

#### 1.5.1.5 Communication Protocol

C# Assembly Reference

#### 1.5.1.6 Extraction Reasoning

Host requires the Users module to function.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IModuleDependency

#### 1.5.2.2 Source Repository

REPO-BE-MOD-READING

#### 1.5.2.3 Method Contracts

- {'method_name': 'AddReadingModule', 'method_signature': 'IServiceCollection AddReadingModule(this IServiceCollection services, IConfiguration config)', 'method_purpose': 'Registers Reading domain services and DB context into the host container.', 'integration_context': 'Program.cs startup sequence'}

#### 1.5.2.4 Integration Pattern

In-Process Dependency Injection

#### 1.5.2.5 Communication Protocol

C# Assembly Reference

#### 1.5.2.6 Extraction Reasoning

Host requires the Reading module to function.

### 1.5.3.0 Interface Name

#### 1.5.3.1 Interface Name

IModuleDependency

#### 1.5.3.2 Source Repository

REPO-BE-MOD-MONETIZATION

#### 1.5.3.3 Method Contracts

- {'method_name': 'AddMonetizationModule', 'method_signature': 'IServiceCollection AddMonetizationModule(this IServiceCollection services, IConfiguration config)', 'method_purpose': 'Registers Monetization domain services and DB context into the host container.', 'integration_context': 'Program.cs startup sequence'}

#### 1.5.3.4 Integration Pattern

In-Process Dependency Injection

#### 1.5.3.5 Communication Protocol

C# Assembly Reference

#### 1.5.3.6 Extraction Reasoning

Host requires the Monetization module to function.

### 1.5.4.0 Interface Name

#### 1.5.4.1 Interface Name

SharedInfrastructure

#### 1.5.4.2 Source Repository

REPO-BE-LIB-INFRA

#### 1.5.4.3 Method Contracts

- {'method_name': 'AddSharedInfrastructure', 'method_signature': 'IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)', 'method_purpose': 'Registers cross-cutting concerns like Redis, Hangfire, and Polly policies.', 'integration_context': 'Program.cs startup sequence'}

#### 1.5.4.4 Integration Pattern

In-Process Dependency Injection

#### 1.5.4.5 Communication Protocol

C# Assembly Reference

#### 1.5.4.6 Extraction Reasoning

Host requires shared infrastructure libraries.

## 1.6.0.0 Exposed Interfaces

### 1.6.1.0 Interface Name

#### 1.6.1.1 Interface Name

Composite REST API

#### 1.6.1.2 Consumer Repositories

- REPO-FE-LIB-APICLIENT

#### 1.6.1.3 Method Contracts

- {'method_name': 'Controller Endpoints', 'method_signature': 'HTTPS GET/POST/PUT/DELETE /api/*', 'method_purpose': 'Exposes the aggregated functionality of all underlying business modules to the mobile client.', 'implementation_requirements': 'Controllers must be automatically discovered via assembly scanning.'}

#### 1.6.1.4 Service Level Requirements

- P95 Latency < 200ms
- 99.9% Uptime

#### 1.6.1.5 Implementation Constraints

- Must support TLS 1.3
- Must provide Swagger/OpenAPI documentation

#### 1.6.1.6 Extraction Reasoning

The Host acts as the physical API Gateway for the client.

### 1.6.2.0 Interface Name

#### 1.6.2.1 Interface Name

Health Checks

#### 1.6.2.2 Consumer Repositories

- readtrack-infrastructure

#### 1.6.2.3 Method Contracts

##### 1.6.2.3.1 Method Name

###### 1.6.2.3.1.1 Method Name

Liveness Probe

###### 1.6.2.3.1.2 Method Signature

GET /health/live

###### 1.6.2.3.1.3 Method Purpose

Indicates if the process is running.

###### 1.6.2.3.1.4 Implementation Requirements

Return 200 OK

##### 1.6.2.3.2.0 Method Name

###### 1.6.2.3.2.1 Method Name

Readiness Probe

###### 1.6.2.3.2.2 Method Signature

GET /health/ready

###### 1.6.2.3.2.3 Method Purpose

Indicates if DB connections and dependent services are responsive.

###### 1.6.2.3.2.4 Implementation Requirements

Check DB connectivity and Auth0 reachability

#### 1.6.2.4.0.0 Service Level Requirements

- Response time < 1s

#### 1.6.2.5.0.0 Implementation Constraints

- No authentication required for health endpoints (protected by network layer)

#### 1.6.2.6.0.0 Extraction Reasoning

Required for the load balancer (AWS ALB) defined in the Infrastructure repository.

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

.NET 8 SDK, ASP.NET Core 8 Runtime

### 1.7.2.0.0.0 Integration Technologies

- MediatR (Internal Messaging)
- Auth0 (Identity)
- Serilog (Logging)
- Swashbuckle (OpenAPI)

### 1.7.3.0.0.0 Performance Constraints

Startup time must be < 5s. Low memory footprint overhead for the orchestration layer.

### 1.7.4.0.0.0 Security Requirements

Enforce HTTPS. Validate JWT signatures against Auth0 JWKS. Secure HTTP Headers (HSTS).

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | 100% mapped to Program.cs responsibilities and mod... |
| Cross Reference Validation | Verified dependencies on all defined business modu... |
| Implementation Readiness Assessment | High. Standard .NET 8 hosting patterns apply. |
| Quality Assurance Confirmation | Integration specs align with Modular Monolith arch... |

