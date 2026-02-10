# 1 Components

## 1.1 Components

### 1.1.1 flutter-client-app-001

#### 1.1.1.1 Id

flutter-client-app-001

#### 1.1.1.2 Name

Flutter Mobile Client

#### 1.1.1.3 Description

The main entry point and container for the Flutter mobile application, responsible for initializing services, routing, and managing the overall application lifecycle.

#### 1.1.1.4 Type

🔹 Application

#### 1.1.1.5 Dependencies

- api-gateway-001

#### 1.1.1.6 Properties

| Property | Value |
|----------|-------|
| Version | 1.0.0 |
| Platform | iOS, Android |

#### 1.1.1.7 Interfaces

*No items available*

#### 1.1.1.8 Technology

Flutter

#### 1.1.1.9 Resources

| Property | Value |
|----------|-------|
| Cpu | N/A (Client-Side) |
| Memory | N/A (Client-Side) |
| Storage | N/A (Client-Side) |

#### 1.1.1.10 Configuration

##### 1.1.1.10.1 Api Base Url

🔗 [https://api.example.com/v1](https://api.example.com/v1)

##### 1.1.1.10.2 Theme Default

light

#### 1.1.1.11.0 Health Check

*Not specified*

#### 1.1.1.12.0 Responsible Features

- Onboarding
- Goal Management
- Offline Support
- Advertisements
- User Interface

#### 1.1.1.13.0 Security

##### 1.1.1.13.1 Requires Authentication

❌ No

##### 1.1.1.13.2 Requires Authorization

❌ No

##### 1.1.1.13.3 Allowed Roles

*No items available*

### 1.1.2.0.0 dashboard-bloc-002

#### 1.1.2.1.0 Id

dashboard-bloc-002

#### 1.1.2.2.0 Name

DashboardBloc

#### 1.1.2.3.0 Description

A state management component in the Client.Presentation layer that orchestrates the fetching of all data required for the main dashboard screen, ensuring it loads within the performance budget.

#### 1.1.2.4.0 Type

🔹 State Management (BLoC)

#### 1.1.2.5.0 Dependencies

- goal-repository-client-003
- user-stats-repository-client-004
- recommendation-repository-client-005

#### 1.1.2.6.0 Properties

*No data available*

#### 1.1.2.7.0 Interfaces

*No items available*

#### 1.1.2.8.0 Technology

flutter_bloc

#### 1.1.2.9.0 Resources

*No data available*

#### 1.1.2.10.0 Configuration

*No data available*

#### 1.1.2.11.0 Health Check

*Not specified*

#### 1.1.2.12.0 Responsible Features

- REQ-PERF-002: Dashboard Performance

#### 1.1.2.13.0 Security

##### 1.1.2.13.1 Requires Authentication

✅ Yes

##### 1.1.2.13.2 Requires Authorization

❌ No

##### 1.1.2.13.3 Allowed Roles

*No items available*

### 1.1.3.0.0 sync-service-client-006

#### 1.1.3.1.0 Id

sync-service-client-006

#### 1.1.3.2.0 Name

SynchronizationService

#### 1.1.3.3.0 Description

A client-side service in the Client.Data layer responsible for automatically synchronizing local data from the Isar database with the remote backend upon reconnection.

#### 1.1.3.4.0 Type

🔹 Service

#### 1.1.3.5.0 Dependencies

- local-data-source-007
- remote-data-source-008

#### 1.1.3.6.0 Properties

*No data available*

#### 1.1.3.7.0 Interfaces

*No items available*

#### 1.1.3.8.0 Technology

Dart

#### 1.1.3.9.0 Resources

*No data available*

#### 1.1.3.10.0 Configuration

##### 1.1.3.10.1 Sync Interval

OnConnectionRestored

##### 1.1.3.10.2 Batch Size

50

#### 1.1.3.11.0 Health Check

*Not specified*

#### 1.1.3.12.0 Responsible Features

- REQ-FUNC-011: Offline Support

#### 1.1.3.13.0 Security

##### 1.1.3.13.1 Requires Authentication

✅ Yes

##### 1.1.3.13.2 Requires Authorization

❌ No

##### 1.1.3.13.3 Allowed Roles

*No items available*

### 1.1.4.0.0 local-data-source-007

#### 1.1.4.1.0 Id

local-data-source-007

#### 1.1.4.2.0 Name

LocalDataSource

#### 1.1.4.3.0 Description

A data source component in the Client.Data layer that directly interacts with the Isar database for all local data storage and retrieval operations.

#### 1.1.4.4.0 Type

🔹 Data Source

#### 1.1.4.5.0 Dependencies

*No items available*

#### 1.1.4.6.0 Properties

*No data available*

#### 1.1.4.7.0 Interfaces

- ILocalDataSource

#### 1.1.4.8.0 Technology

Isar

#### 1.1.4.9.0 Resources

*No data available*

#### 1.1.4.10.0 Configuration

*No data available*

#### 1.1.4.11.0 Health Check

*Not specified*

#### 1.1.4.12.0 Responsible Features

- REQ-FUNC-011: Offline Support

#### 1.1.4.13.0 Security

##### 1.1.4.13.1 Requires Authentication

❌ No

##### 1.1.4.13.2 Requires Authorization

❌ No

##### 1.1.4.13.3 Allowed Roles

*No items available*

### 1.1.5.0.0 admob-component-009

#### 1.1.5.1.0 Id

admob-component-009

#### 1.1.5.2.0 Name

AdMobComponent

#### 1.1.5.3.0 Description

A presentation layer component that integrates the Google AdMob SDK to display advertisements to users on the 'Free User' tier.

#### 1.1.5.4.0 Type

🔹 UI Component

#### 1.1.5.5.0 Dependencies

*No items available*

#### 1.1.5.6.0 Properties

| Property | Value |
|----------|-------|
| Sdk Version | google_mobile_ads:^5.1.0 |

#### 1.1.5.7.0 Interfaces

*No items available*

#### 1.1.5.8.0 Technology

Flutter

#### 1.1.5.9.0 Resources

*No data available*

#### 1.1.5.10.0 Configuration

##### 1.1.5.10.1 Ad Unit Id

ca-app-pub-...

#### 1.1.5.11.0 Health Check

*Not specified*

#### 1.1.5.12.0 Responsible Features

- REQ-FUNC-010: Advertisements

#### 1.1.5.13.0 Security

##### 1.1.5.13.1 Requires Authentication

❌ No

##### 1.1.5.13.2 Requires Authorization

❌ No

##### 1.1.5.13.3 Allowed Roles

*No items available*

### 1.1.6.0.0 api-gateway-001

#### 1.1.6.1.0 Id

api-gateway-001

#### 1.1.6.2.0 Name

API Gateway / Presentation Layer

#### 1.1.6.3.0 Description

The public-facing entry point of the backend monolith, responsible for handling all incoming HTTP requests, authentication, authorization, and routing to the application layer. Comprises all API controllers.

#### 1.1.6.4.0 Type

🔹 API Gateway

#### 1.1.6.5.0 Dependencies

- application-layer-010

#### 1.1.6.6.0 Properties

| Property | Value |
|----------|-------|
| Version | 1.0.0 |

#### 1.1.6.7.0 Interfaces

- RESTful API

#### 1.1.6.8.0 Technology

ASP.NET Core

#### 1.1.6.9.0 Resources

##### 1.1.6.9.1 Cpu

1 core

##### 1.1.6.9.2 Memory

2GB

#### 1.1.6.10.0 Configuration

##### 1.1.6.10.1 Jwt Issuer

🔗 [https://auth.example.com](https://auth.example.com)

##### 1.1.6.10.2 Cors Policy

AllowClientOrigin

#### 1.1.6.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health |
| Interval | 30 |
| Timeout | 5 |

#### 1.1.6.12.0 Responsible Features

- User Management
- Goal Management
- AI Suggestions
- Subscription Management
- Data Export

#### 1.1.6.13.0 Security

##### 1.1.6.13.1 Requires Authentication

✅ Yes

##### 1.1.6.13.2 Requires Authorization

✅ Yes

##### 1.1.6.13.3 Allowed Roles

- Free User
- Premium User

### 1.1.7.0.0 application-layer-010

#### 1.1.7.1.0 Id

application-layer-010

#### 1.1.7.2.0 Name

Application Layer

#### 1.1.7.3.0 Description

The core logic orchestrator of the backend. Implements CQRS by containing command and query handlers that process requests from the presentation layer and coordinate with the domain and infrastructure layers.

#### 1.1.7.4.0 Type

🔹 Application Logic

#### 1.1.7.5.0 Dependencies

- domain-layer-011
- infrastructure-layer-012

#### 1.1.7.6.0 Properties

*No data available*

#### 1.1.7.7.0 Interfaces

*No items available*

#### 1.1.7.8.0 Technology

MediatR

#### 1.1.7.9.0 Resources

##### 1.1.7.9.1 Cpu

2 cores

##### 1.1.7.9.2 Memory

4GB

#### 1.1.7.10.0 Configuration

*No data available*

#### 1.1.7.11.0 Health Check

*Not specified*

#### 1.1.7.12.0 Responsible Features

- User Management
- Goal Management
- AI Suggestions
- Subscription Management
- Data Export

#### 1.1.7.13.0 Security

##### 1.1.7.13.1 Requires Authentication

❌ No

##### 1.1.7.13.2 Requires Authorization

❌ No

##### 1.1.7.13.3 Allowed Roles

*No items available*

### 1.1.8.0.0 recommendation-generation-service-013

#### 1.1.8.1.0 Id

recommendation-generation-service-013

#### 1.1.8.2.0 Name

RecommendationGenerationService

#### 1.1.8.3.0 Description

An application service that generates personalized book recommendations by processing user data and interacting with the OpenAI GPT-4 API client. Includes logic for incorporating user feedback into future prompts.

#### 1.1.8.4.0 Type

🔹 Service

#### 1.1.8.5.0 Dependencies

- openai-client-014
- user-repository-backend-015
- recommendation-repository-backend-016

#### 1.1.8.6.0 Properties

*No data available*

#### 1.1.8.7.0 Interfaces

*No items available*

#### 1.1.8.8.0 Technology

.NET

#### 1.1.8.9.0 Resources

*No data available*

#### 1.1.8.10.0 Configuration

##### 1.1.8.10.1 Prompt Template Version

v2.1

##### 1.1.8.10.2 Max Tokens

1,024

#### 1.1.8.11.0 Health Check

*Not specified*

#### 1.1.8.12.0 Responsible Features

- REQ-FUNC-007: AI Suggestions

#### 1.1.8.13.0 Security

##### 1.1.8.13.1 Requires Authentication

✅ Yes

##### 1.1.8.13.2 Requires Authorization

✅ Yes

##### 1.1.8.13.3 Allowed Roles

- Premium User

### 1.1.9.0.0 openai-client-014

#### 1.1.9.1.0 Id

openai-client-014

#### 1.1.9.2.0 Name

OpenAIClient

#### 1.1.9.3.0 Description

An infrastructure component that encapsulates all communication with the external OpenAI GPT-4 API. It implements resilience patterns like Retry and Circuit Breaker to handle transient failures gracefully.

#### 1.1.9.4.0 Type

🔹 API Client

#### 1.1.9.5.0 Dependencies

- resilience-policy-provider-017

#### 1.1.9.6.0 Properties

*No data available*

#### 1.1.9.7.0 Interfaces

- IOpenAIClient

#### 1.1.9.8.0 Technology

HttpClient, Polly

#### 1.1.9.9.0 Resources

*No data available*

#### 1.1.9.10.0 Configuration

##### 1.1.9.10.1 Api Key

env_var:OPENAI_API_KEY

##### 1.1.9.10.2 Rate Limit

60 RPM

#### 1.1.9.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/openai |
| Interval | 120 |
| Timeout | 10 |

#### 1.1.9.12.0 Responsible Features

- REQ-FUNC-007: AI Suggestions
- REQ-REL-002: API Resilience

#### 1.1.9.13.0 Security

##### 1.1.9.13.1 Requires Authentication

❌ No

##### 1.1.9.13.2 Requires Authorization

❌ No

##### 1.1.9.13.3 Allowed Roles

*No items available*

### 1.1.10.0.0 subscription-status-service-018

#### 1.1.10.1.0 Id

subscription-status-service-018

#### 1.1.10.2.0 Name

SubscriptionStatusService

#### 1.1.10.3.0 Description

A background service that periodically checks for terminated or expired subscriptions and reverts the associated user accounts from 'Premium User' to 'Free User' tier.

#### 1.1.10.4.0 Type

🔹 Background Job

#### 1.1.10.5.0 Dependencies

- subscription-repository-backend-019
- user-repository-backend-015

#### 1.1.10.6.0 Properties

*No data available*

#### 1.1.10.7.0 Interfaces

*No items available*

#### 1.1.10.8.0 Technology

Hangfire

#### 1.1.10.9.0 Resources

*No data available*

#### 1.1.10.10.0 Configuration

##### 1.1.10.10.1 Cron Schedule

0 1 * * *

#### 1.1.10.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/jobs/subscription |
| Interval | 3600 |
| Timeout | 60 |

#### 1.1.10.12.0 Responsible Features

- REQ-FUNC-002: Subscription Termination

#### 1.1.10.13.0 Security

##### 1.1.10.13.1 Requires Authentication

❌ No

##### 1.1.10.13.2 Requires Authorization

❌ No

##### 1.1.10.13.3 Allowed Roles

*No items available*

### 1.1.11.0.0 data-export-job-handler-020

#### 1.1.11.1.0 Id

data-export-job-handler-020

#### 1.1.11.2.0 Name

DataExportJobHandler

#### 1.1.11.3.0 Description

A background job handler that processes user-initiated data export requests. It queries all relevant user data, compiles it into a machine-readable format (JSON), and makes it available for download.

#### 1.1.11.4.0 Type

🔹 Background Job

#### 1.1.11.5.0 Dependencies

- user-repository-backend-015
- goal-repository-backend-021
- data-export-job-repository-022

#### 1.1.11.6.0 Properties

*No data available*

#### 1.1.11.7.0 Interfaces

*No items available*

#### 1.1.11.8.0 Technology

Hangfire

#### 1.1.11.9.0 Resources

*No data available*

#### 1.1.11.10.0 Configuration

##### 1.1.11.10.1 Output Format

JSON

##### 1.1.11.10.2 Storage Location

Private S3 Bucket

#### 1.1.11.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/jobs/dataexport |
| Interval | 3600 |
| Timeout | 60 |

#### 1.1.11.12.0 Responsible Features

- REQ-FUNC-009: Data Export

#### 1.1.11.13.0 Security

##### 1.1.11.13.1 Requires Authentication

❌ No

##### 1.1.11.13.2 Requires Authorization

❌ No

##### 1.1.11.13.3 Allowed Roles

*No items available*

### 1.1.12.0.0 persistence-layer-023

#### 1.1.12.1.0 Id

persistence-layer-023

#### 1.1.12.2.0 Name

Persistence Layer

#### 1.1.12.3.0 Description

An infrastructure component containing the concrete implementations of all repository interfaces. It uses Entity Framework Core to interact with the PostgreSQL database and includes pre-aggregated views for performance.

#### 1.1.12.4.0 Type

🔹 Repository

#### 1.1.12.5.0 Dependencies

*No items available*

#### 1.1.12.6.0 Properties

| Property | Value |
|----------|-------|
| Orm | Entity Framework Core 8 |

#### 1.1.12.7.0 Interfaces

- IUserRepository
- IGoalRepository
- ISubscriptionRepository

#### 1.1.12.8.0 Technology

Entity Framework Core, Npgsql

#### 1.1.12.9.0 Resources

##### 1.1.12.9.1 Cpu

1 core

##### 1.1.12.9.2 Memory

2GB

#### 1.1.12.10.0 Configuration

##### 1.1.12.10.1 Connection String

env_var:DB_CONNECTION_STRING

##### 1.1.12.10.2 Command Timeout

30

#### 1.1.12.11.0 Health Check

| Property | Value |
|----------|-------|
| Path | /health/db |
| Interval | 30 |
| Timeout | 5 |

#### 1.1.12.12.0 Responsible Features

- Data Persistence
- REQ-PERF-001: API Performance
- REQ-PERF-002: Dashboard Performance

#### 1.1.12.13.0 Security

##### 1.1.12.13.1 Requires Authentication

❌ No

##### 1.1.12.13.2 Requires Authorization

❌ No

##### 1.1.12.13.3 Allowed Roles

*No items available*

## 1.2.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Environment | Production |
| Logging Level | Information |
| Database Url | env_var:DB_CONNECTION_STRING |
| Cache Ttl | 3600 |
| Max Threads | 200 |

# 2.0.0.0.0 Component Relations

## 2.1.0.0.0 Architecture

### 2.1.1.0.0 Components

#### 2.1.1.1.0 backend-users-controller-001

##### 2.1.1.1.1 Id

backend-users-controller-001

##### 2.1.1.1.2 Name

UsersController

##### 2.1.1.1.3 Description

Handles all HTTP requests related to user management, including profile updates and confirming the completion of the onboarding process.

##### 2.1.1.1.4 Type

🔹 APIController

##### 2.1.1.1.5 Dependencies

- mediator-service-001

##### 2.1.1.1.6 Interfaces

*No items available*

##### 2.1.1.1.7 Technology

ASP.NET Core 8

##### 2.1.1.1.8 Resources

###### 2.1.1.1.8.1 Cpu

0.25 cores

###### 2.1.1.1.8.2 Memory

256MB

##### 2.1.1.1.9.0 Configuration

###### 2.1.1.1.9.1 Route

/api/users

##### 2.1.1.1.10.0 Health Check

*Not specified*

##### 2.1.1.1.11.0 Responsible Features

- Onboarding
- User Management

##### 2.1.1.1.12.0 Security

###### 2.1.1.1.12.1 Requires Authentication

✅ Yes

###### 2.1.1.1.12.2 Requires Authorization

✅ Yes

###### 2.1.1.1.12.3 Allowed Roles

- Free User
- Premium User

#### 2.1.1.2.0.0 backend-goals-controller-002

##### 2.1.1.2.1.0 Id

backend-goals-controller-002

##### 2.1.1.2.2.0 Name

GoalsController

##### 2.1.1.2.3.0 Description

Provides RESTful endpoints for creating, reading, updating, and deleting user-defined reading goals.

##### 2.1.1.2.4.0 Type

🔹 APIController

##### 2.1.1.2.5.0 Dependencies

- mediator-service-001

##### 2.1.1.2.6.0 Interfaces

*No items available*

##### 2.1.1.2.7.0 Technology

ASP.NET Core 8

##### 2.1.1.2.8.0 Resources

###### 2.1.1.2.8.1 Cpu

0.25 cores

###### 2.1.1.2.8.2 Memory

256MB

##### 2.1.1.2.9.0 Configuration

###### 2.1.1.2.9.1 Route

/api/goals

##### 2.1.1.2.10.0 Health Check

*Not specified*

##### 2.1.1.2.11.0 Responsible Features

- Goal Management

##### 2.1.1.2.12.0 Security

###### 2.1.1.2.12.1 Requires Authentication

✅ Yes

###### 2.1.1.2.12.2 Requires Authorization

✅ Yes

###### 2.1.1.2.12.3 Allowed Roles

- Free User
- Premium User

#### 2.1.1.3.0.0 backend-recommendations-controller-003

##### 2.1.1.3.1.0 Id

backend-recommendations-controller-003

##### 2.1.1.3.2.0 Name

RecommendationsController

##### 2.1.1.3.3.0 Description

Exposes an endpoint to generate and retrieve personalized book recommendations for the authenticated user.

##### 2.1.1.3.4.0 Type

🔹 APIController

##### 2.1.1.3.5.0 Dependencies

- mediator-service-001
- rate-limiting-middleware-009

##### 2.1.1.3.6.0 Interfaces

*No items available*

##### 2.1.1.3.7.0 Technology

ASP.NET Core 8

##### 2.1.1.3.8.0 Resources

###### 2.1.1.3.8.1 Cpu

0.25 cores

###### 2.1.1.3.8.2 Memory

256MB

##### 2.1.1.3.9.0 Configuration

###### 2.1.1.3.9.1 Route

/api/recommendations

##### 2.1.1.3.10.0 Health Check

*Not specified*

##### 2.1.1.3.11.0 Responsible Features

- AI Suggestions

##### 2.1.1.3.12.0 Security

###### 2.1.1.3.12.1 Requires Authentication

✅ Yes

###### 2.1.1.3.12.2 Requires Authorization

✅ Yes

###### 2.1.1.3.12.3 Allowed Roles

- Premium User

#### 2.1.1.4.0.0 backend-data-export-controller-004

##### 2.1.1.4.1.0 Id

backend-data-export-controller-004

##### 2.1.1.4.2.0 Name

DataExportController

##### 2.1.1.4.3.0 Description

Provides endpoints for initiating and checking the status of a user's personal data export job, in compliance with GDPR.

##### 2.1.1.4.4.0 Type

🔹 APIController

##### 2.1.1.4.5.0 Dependencies

- mediator-service-001

##### 2.1.1.4.6.0 Interfaces

*No items available*

##### 2.1.1.4.7.0 Technology

ASP.NET Core 8

##### 2.1.1.4.8.0 Resources

###### 2.1.1.4.8.1 Cpu

0.25 cores

###### 2.1.1.4.8.2 Memory

256MB

##### 2.1.1.4.9.0 Configuration

###### 2.1.1.4.9.1 Route

/api/export

##### 2.1.1.4.10.0 Health Check

*Not specified*

##### 2.1.1.4.11.0 Responsible Features

- User Management

##### 2.1.1.4.12.0 Security

###### 2.1.1.4.12.1 Requires Authentication

✅ Yes

###### 2.1.1.4.12.2 Requires Authorization

✅ Yes

###### 2.1.1.4.12.3 Allowed Roles

- Free User
- Premium User

#### 2.1.1.5.0.0 mediator-service-001

##### 2.1.1.5.1.0 Id

mediator-service-001

##### 2.1.1.5.2.0 Name

Mediator (CQRS Handlers)

##### 2.1.1.5.3.0 Description

A set of command and query handlers that implement the application's use cases. It decouples the presentation layer from the application logic.

##### 2.1.1.5.4.0 Type

🔹 ApplicationService

##### 2.1.1.5.5.0 Dependencies

- user-repository-010
- goal-repository-011
- recommendation-repository-012
- openai-client-014

##### 2.1.1.5.6.0 Interfaces

*No items available*

##### 2.1.1.5.7.0 Technology

MediatR

##### 2.1.1.5.8.0 Resources

###### 2.1.1.5.8.1 Cpu

1 core

###### 2.1.1.5.8.2 Memory

1GB

##### 2.1.1.5.9.0 Configuration

*No data available*

##### 2.1.1.5.10.0 Health Check

*Not specified*

##### 2.1.1.5.11.0 Responsible Features

- Goal Management
- AI Suggestions
- User Management

##### 2.1.1.5.12.0 Security

###### 2.1.1.5.12.1 Requires Authentication

❌ No

###### 2.1.1.5.12.2 Requires Authorization

❌ No

#### 2.1.1.6.0.0 backend-subscription-service-005

##### 2.1.1.6.1.0 Id

backend-subscription-service-005

##### 2.1.1.6.2.0 Name

SubscriptionStatusService

##### 2.1.1.6.3.0 Description

Contains the business logic for managing subscription lifecycles. It is invoked by background jobs or webhooks to handle expirations and terminations.

##### 2.1.1.6.4.0 Type

🔹 ApplicationService

##### 2.1.1.6.5.0 Dependencies

- user-repository-010
- subscription-repository-013

##### 2.1.1.6.6.0 Interfaces

- ISubscriptionService

##### 2.1.1.6.7.0 Technology

.NET 8

##### 2.1.1.6.8.0 Resources

###### 2.1.1.6.8.1 Cpu

0.5 cores

###### 2.1.1.6.8.2 Memory

512MB

##### 2.1.1.6.9.0 Configuration

*No data available*

##### 2.1.1.6.10.0 Health Check

*Not specified*

##### 2.1.1.6.11.0 Responsible Features

- Subscription Management

##### 2.1.1.6.12.0 Security

###### 2.1.1.6.12.1 Requires Authentication

❌ No

###### 2.1.1.6.12.2 Requires Authorization

❌ No

#### 2.1.1.7.0.0 subscription-job-processor-006

##### 2.1.1.7.1.0 Id

subscription-job-processor-006

##### 2.1.1.7.2.0 Name

SubscriptionJobProcessor

##### 2.1.1.7.3.0 Description

A scheduled background job that periodically scans for expired subscriptions and triggers the SubscriptionStatusService to revert user tiers to 'Free'.

##### 2.1.1.7.4.0 Type

🔹 BackgroundJob

##### 2.1.1.7.5.0 Dependencies

- backend-subscription-service-005

##### 2.1.1.7.6.0 Interfaces

*No items available*

##### 2.1.1.7.7.0 Technology

Hangfire

##### 2.1.1.7.8.0 Resources

###### 2.1.1.7.8.1 Cpu

0.5 cores

###### 2.1.1.7.8.2 Memory

512MB

##### 2.1.1.7.9.0 Configuration

###### 2.1.1.7.9.1 Cron Schedule

0 2 * * *

##### 2.1.1.7.10.0 Health Check

*Not specified*

##### 2.1.1.7.11.0 Responsible Features

- Subscription Management

##### 2.1.1.7.12.0 Security

###### 2.1.1.7.12.1 Requires Authentication

❌ No

###### 2.1.1.7.12.2 Requires Authorization

❌ No

#### 2.1.1.8.0.0 data-export-job-processor-007

##### 2.1.1.8.1.0 Id

data-export-job-processor-007

##### 2.1.1.8.2.0 Name

DataExportJobProcessor

##### 2.1.1.8.3.0 Description

A background job processor that handles asynchronous data export requests. It queries all user data, compiles it into a machine-readable file, and updates the job status.

##### 2.1.1.8.4.0 Type

🔹 BackgroundJob

##### 2.1.1.8.5.0 Dependencies

- user-repository-010
- goal-repository-011
- data-export-job-repository-014

##### 2.1.1.8.6.0 Interfaces

*No items available*

##### 2.1.1.8.7.0 Technology

Hangfire

##### 2.1.1.8.8.0 Resources

###### 2.1.1.8.8.1 Cpu

1 core

###### 2.1.1.8.8.2 Memory

1GB

##### 2.1.1.8.9.0 Configuration

###### 2.1.1.8.9.1 Max Retries

3

##### 2.1.1.8.10.0 Health Check

*Not specified*

##### 2.1.1.8.11.0 Responsible Features

- User Management

##### 2.1.1.8.12.0 Security

###### 2.1.1.8.12.1 Requires Authentication

❌ No

###### 2.1.1.8.12.2 Requires Authorization

❌ No

#### 2.1.1.9.0.0 authentication-middleware-008

##### 2.1.1.9.1.0 Id

authentication-middleware-008

##### 2.1.1.9.2.0 Name

AuthenticationMiddleware

##### 2.1.1.9.3.0 Description

ASP.NET Core middleware responsible for validating JWT tokens on incoming requests and establishing the user's identity.

##### 2.1.1.9.4.0 Type

🔹 Middleware

##### 2.1.1.9.5.0 Dependencies

*No items available*

##### 2.1.1.9.6.0 Interfaces

*No items available*

##### 2.1.1.9.7.0 Technology

ASP.NET Core 8 JWT Bearer

##### 2.1.1.9.8.0 Resources

*No data available*

##### 2.1.1.9.9.0 Configuration

###### 2.1.1.9.9.1 Issuer

thesss-api

###### 2.1.1.9.9.2 Audience

thesss-client

##### 2.1.1.9.10.0 Health Check

*Not specified*

##### 2.1.1.9.11.0 Responsible Features

- Security

##### 2.1.1.9.12.0 Security

###### 2.1.1.9.12.1 Requires Authentication

❌ No

###### 2.1.1.9.12.2 Requires Authorization

❌ No

#### 2.1.1.10.0.0 rate-limiting-middleware-009

##### 2.1.1.10.1.0 Id

rate-limiting-middleware-009

##### 2.1.1.10.2.0 Name

RateLimitingMiddleware

##### 2.1.1.10.3.0 Description

ASP.NET Core middleware that applies rate limiting policies to specific endpoints, primarily to control the cost of calls to the OpenAI API.

##### 2.1.1.10.4.0 Type

🔹 Middleware

##### 2.1.1.10.5.0 Dependencies

*No items available*

##### 2.1.1.10.6.0 Interfaces

*No items available*

##### 2.1.1.10.7.0 Technology

ASP.NET Core 8 Rate Limiting

##### 2.1.1.10.8.0 Resources

*No data available*

##### 2.1.1.10.9.0 Configuration

| Property | Value |
|----------|-------|
| Policy | FixedWindow |
| Permit Limit | 5 |
| Window | 1h |

##### 2.1.1.10.10.0 Health Check

*Not specified*

##### 2.1.1.10.11.0 Responsible Features

- AI Suggestions

##### 2.1.1.10.12.0 Security

###### 2.1.1.10.12.1 Requires Authentication

❌ No

###### 2.1.1.10.12.2 Requires Authorization

❌ No

#### 2.1.1.11.0.0 user-repository-010

##### 2.1.1.11.1.0 Id

user-repository-010

##### 2.1.1.11.2.0 Name

UserRepository

##### 2.1.1.11.3.0 Description

Implements data access logic for the User entity, interacting with the PostgreSQL database. Part of the Infrastructure layer.

##### 2.1.1.11.4.0 Type

🔹 Repository

##### 2.1.1.11.5.0 Dependencies

- database-context-015

##### 2.1.1.11.6.0 Interfaces

- IUserRepository

##### 2.1.1.11.7.0 Technology

Entity Framework Core 8

##### 2.1.1.11.8.0 Resources

*No data available*

##### 2.1.1.11.9.0 Configuration

*No data available*

##### 2.1.1.11.10.0 Health Check

*Not specified*

##### 2.1.1.11.11.0 Responsible Features

- User Management
- Onboarding
- Subscription Management

##### 2.1.1.11.12.0 Security

###### 2.1.1.11.12.1 Requires Authentication

❌ No

###### 2.1.1.11.12.2 Requires Authorization

❌ No

#### 2.1.1.12.0.0 goal-repository-011

##### 2.1.1.12.1.0 Id

goal-repository-011

##### 2.1.1.12.2.0 Name

GoalRepository

##### 2.1.1.12.3.0 Description

Implements data access logic for the Goal entity using Entity Framework Core. Part of the Infrastructure layer.

##### 2.1.1.12.4.0 Type

🔹 Repository

##### 2.1.1.12.5.0 Dependencies

- database-context-015

##### 2.1.1.12.6.0 Interfaces

- IGoalRepository

##### 2.1.1.12.7.0 Technology

Entity Framework Core 8

##### 2.1.1.12.8.0 Resources

*No data available*

##### 2.1.1.12.9.0 Configuration

*No data available*

##### 2.1.1.12.10.0 Health Check

*Not specified*

##### 2.1.1.12.11.0 Responsible Features

- Goal Management

##### 2.1.1.12.12.0 Security

###### 2.1.1.12.12.1 Requires Authentication

❌ No

###### 2.1.1.12.12.2 Requires Authorization

❌ No

#### 2.1.1.13.0.0 openai-client-014

##### 2.1.1.13.1.0 Id

openai-client-014

##### 2.1.1.13.2.0 Name

OpenAIClient

##### 2.1.1.13.3.0 Description

A client component responsible for all communication with the OpenAI GPT-4 API. It includes resilience patterns like Retry and Circuit Breaker.

##### 2.1.1.13.4.0 Type

🔹 ExternalServiceClient

##### 2.1.1.13.5.0 Dependencies

*No items available*

##### 2.1.1.13.6.0 Interfaces

- IOpenAIClient

##### 2.1.1.13.7.0 Technology

HttpClient, Polly

##### 2.1.1.13.8.0 Resources

*No data available*

##### 2.1.1.13.9.0 Configuration

###### 2.1.1.13.9.1 Retry Attempts

3

###### 2.1.1.13.9.2 Circuit Breaker Threshold

5

##### 2.1.1.13.10.0 Health Check

*Not specified*

##### 2.1.1.13.11.0 Responsible Features

- AI Suggestions

##### 2.1.1.13.12.0 Security

###### 2.1.1.13.12.1 Requires Authentication

❌ No

###### 2.1.1.13.12.2 Requires Authorization

❌ No

#### 2.1.1.14.0.0 client-onboarding-component-101

##### 2.1.1.14.1.0 Id

client-onboarding-component-101

##### 2.1.1.14.2.0 Name

OnboardingComponent

##### 2.1.1.14.3.0 Description

A client-side component in the Presentation layer that manages the mandatory, multi-step onboarding UI for new users.

##### 2.1.1.14.4.0 Type

🔹 ClientUIComponent

##### 2.1.1.14.5.0 Dependencies

- client-user-repository-105

##### 2.1.1.14.6.0 Interfaces

*No items available*

##### 2.1.1.14.7.0 Technology

Flutter, flutter_bloc

##### 2.1.1.14.8.0 Resources

*No data available*

##### 2.1.1.14.9.0 Configuration

*No data available*

##### 2.1.1.14.10.0 Health Check

*Not specified*

##### 2.1.1.14.11.0 Responsible Features

- Onboarding

##### 2.1.1.14.12.0 Security

###### 2.1.1.14.12.1 Requires Authentication

❌ No

###### 2.1.1.14.12.2 Requires Authorization

❌ No

#### 2.1.1.15.0.0 client-ad-integration-component-102

##### 2.1.1.15.1.0 Id

client-ad-integration-component-102

##### 2.1.1.15.2.0 Name

AdIntegrationComponent

##### 2.1.1.15.3.0 Description

Client-side component responsible for initializing the Google AdMob SDK and displaying advertisements to users on the 'Free User' tier.

##### 2.1.1.15.4.0 Type

🔹 ClientUIComponent

##### 2.1.1.15.5.0 Dependencies

- client-user-repository-105

##### 2.1.1.15.6.0 Interfaces

*No items available*

##### 2.1.1.15.7.0 Technology

Flutter, google_mobile_ads

##### 2.1.1.15.8.0 Resources

*No data available*

##### 2.1.1.15.9.0 Configuration

###### 2.1.1.15.9.1 Ad Unit Id

ca-app-pub-...

##### 2.1.1.15.10.0 Health Check

*Not specified*

##### 2.1.1.15.11.0 Responsible Features

- Advertisements

##### 2.1.1.15.12.0 Security

###### 2.1.1.15.12.1 Requires Authentication

❌ No

###### 2.1.1.15.12.2 Requires Authorization

❌ No

#### 2.1.1.16.0.0 client-sync-service-103

##### 2.1.1.16.1.0 Id

client-sync-service-103

##### 2.1.1.16.2.0 Name

SynchronizationService

##### 2.1.1.16.3.0 Description

A core service in the client's Data layer that manages two-way data synchronization between the local Isar database and the backend API, enabling offline support.

##### 2.1.1.16.4.0 Type

🔹 ClientService

##### 2.1.1.16.5.0 Dependencies

- client-local-data-source-107
- client-remote-data-source-108

##### 2.1.1.16.6.0 Interfaces

*No items available*

##### 2.1.1.16.7.0 Technology

Dart

##### 2.1.1.16.8.0 Resources

*No data available*

##### 2.1.1.16.9.0 Configuration

###### 2.1.1.16.9.1 Sync Trigger

OnReconnect, OnAppLaunch, Manual

##### 2.1.1.16.10.0 Health Check

*Not specified*

##### 2.1.1.16.11.0 Responsible Features

- Offline Support

##### 2.1.1.16.12.0 Security

###### 2.1.1.16.12.1 Requires Authentication

❌ No

###### 2.1.1.16.12.2 Requires Authorization

❌ No

#### 2.1.1.17.0.0 client-goal-usecases-104

##### 2.1.1.17.1.0 Id

client-goal-usecases-104

##### 2.1.1.17.2.0 Name

GoalUseCases

##### 2.1.1.17.3.0 Description

A component in the client's Domain layer that encapsulates the business logic for creating, editing, and deleting goals, independent of UI or data sources.

##### 2.1.1.17.4.0 Type

🔹 ClientDomainComponent

##### 2.1.1.17.5.0 Dependencies

- client-goal-repository-106

##### 2.1.1.17.6.0 Interfaces

*No items available*

##### 2.1.1.17.7.0 Technology

Dart

##### 2.1.1.17.8.0 Resources

*No data available*

##### 2.1.1.17.9.0 Configuration

*No data available*

##### 2.1.1.17.10.0 Health Check

*Not specified*

##### 2.1.1.17.11.0 Responsible Features

- Goal Management

##### 2.1.1.17.12.0 Security

###### 2.1.1.17.12.1 Requires Authentication

❌ No

###### 2.1.1.17.12.2 Requires Authorization

❌ No

#### 2.1.1.18.0.0 client-goal-repository-106

##### 2.1.1.18.1.0 Id

client-goal-repository-106

##### 2.1.1.18.2.0 Name

GoalRepository (Client)

##### 2.1.1.18.3.0 Description

Implements the IGoalRepository interface from the Domain layer. It orchestrates data fetching from the LocalDataSource or RemoteDataSource based on network connectivity.

##### 2.1.1.18.4.0 Type

🔹 ClientRepository

##### 2.1.1.18.5.0 Dependencies

- client-local-data-source-107
- client-remote-data-source-108
- client-sync-service-103

##### 2.1.1.18.6.0 Interfaces

- IGoalRepository

##### 2.1.1.18.7.0 Technology

Dart

##### 2.1.1.18.8.0 Resources

*No data available*

##### 2.1.1.18.9.0 Configuration

*No data available*

##### 2.1.1.18.10.0 Health Check

*Not specified*

##### 2.1.1.18.11.0 Responsible Features

- Goal Management
- Offline Support

##### 2.1.1.18.12.0 Security

###### 2.1.1.18.12.1 Requires Authentication

❌ No

###### 2.1.1.18.12.2 Requires Authorization

❌ No

#### 2.1.1.19.0.0 client-local-data-source-107

##### 2.1.1.19.1.0 Id

client-local-data-source-107

##### 2.1.1.19.2.0 Name

LocalDataSource

##### 2.1.1.19.3.0 Description

A data source component that provides an abstraction over the Isar database for all local data persistence operations on the client device.

##### 2.1.1.19.4.0 Type

🔹 ClientDataSource

##### 2.1.1.19.5.0 Dependencies

*No items available*

##### 2.1.1.19.6.0 Interfaces

- ILocalDataSource

##### 2.1.1.19.7.0 Technology

Isar

##### 2.1.1.19.8.0 Resources

*No data available*

##### 2.1.1.19.9.0 Configuration

*No data available*

##### 2.1.1.19.10.0 Health Check

*Not specified*

##### 2.1.1.19.11.0 Responsible Features

- Offline Support

##### 2.1.1.19.12.0 Security

###### 2.1.1.19.12.1 Requires Authentication

❌ No

###### 2.1.1.19.12.2 Requires Authorization

❌ No

#### 2.1.1.20.0.0 client-remote-data-source-108

##### 2.1.1.20.1.0 Id

client-remote-data-source-108

##### 2.1.1.20.2.0 Name

RemoteDataSource

##### 2.1.1.20.3.0 Description

A data source component that handles all HTTP communication with the backend REST API using the Dio library.

##### 2.1.1.20.4.0 Type

🔹 ClientDataSource

##### 2.1.1.20.5.0 Dependencies

*No items available*

##### 2.1.1.20.6.0 Interfaces

- IRemoteDataSource

##### 2.1.1.20.7.0 Technology

Dio

##### 2.1.1.20.8.0 Resources

*No data available*

##### 2.1.1.20.9.0 Configuration

###### 2.1.1.20.9.1 Base Url

🔗 [https://api.thesss.com](https://api.thesss.com)

##### 2.1.1.20.10.0 Health Check

*Not specified*

##### 2.1.1.20.11.0 Responsible Features

- Offline Support

##### 2.1.1.20.12.0 Security

###### 2.1.1.20.12.1 Requires Authentication

❌ No

###### 2.1.1.20.12.2 Requires Authorization

❌ No

### 2.1.2.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Environment | production |
| Logging Level | INFO |
| Database Url | jdbc:postgresql://prod-db:5432/thesss_db |
| Cache Url | redis://prod-cache:6379 |
| Open Ai Api Key | env_var:OPENAI_API_KEY |
| Jwt Secret | env_var:JWT_SECRET_KEY |

