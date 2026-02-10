# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- Dart 3.4+
- Flutter 3.22+
- Isar 3.1+
- C# 12
- .NET 8
- ASP.NET Core 8
- Entity Framework Core 8
- PostgreSQL 16
- MediatR
- Hangfire

## 1.3 Service Interfaces

- RESTful API (Backend.Presentation)
- OpenAI GPT-4 API Client
- Google Books API Client
- Google AdMob SDK

## 1.4 Data Models

- User
- Subscription
- Book
- UserBook
- ReadingSession
- Goal
- Recommendation
- DataExportJob
- UserReadingStats

# 2.0 Data Mapping Strategy

## 2.1 Essential Mappings

### 2.1.1 Mapping Id

#### 2.1.1.1 Mapping Id

MAP-001

#### 2.1.1.2 Source

Backend Domain Entities (e.g., Goal, UserBook)

#### 2.1.1.3 Target

Backend API DTOs (e.g., GoalDto, UserBookDto)

#### 2.1.1.4 Transformation

flattened

#### 2.1.1.5 Configuration

*No data available*

#### 2.1.1.6 Mapping Technique

Object-to-object mapping

#### 2.1.1.7 Justification

Required to expose data via the RESTful API to the client, separating internal domain models from the public contract.

#### 2.1.1.8 Complexity

medium

### 2.1.2.0 Mapping Id

#### 2.1.2.1 Mapping Id

MAP-002

#### 2.1.2.2 Source

Backend API DTOs

#### 2.1.2.3 Target

Client Data Models (Isar Schemas)

#### 2.1.2.4 Transformation

direct

#### 2.1.2.5 Configuration

*No data available*

#### 2.1.2.6 Mapping Technique

Deserialization and object-to-object mapping

#### 2.1.2.7 Justification

Required for the client to persist data received from the backend into its local Isar database for offline support (REQ-FUNC-011).

#### 2.1.2.8 Complexity

simple

### 2.1.3.0 Mapping Id

#### 2.1.3.1 Mapping Id

MAP-003

#### 2.1.3.2 Source

User Reading Data (History, Goals, Vocabulary)

#### 2.1.3.3 Target

OpenAI GPT-4 API Prompt (Text)

#### 2.1.3.4 Transformation

aggregation

#### 2.1.3.5 Configuration

##### 2.1.3.5.1 Prompt Template

Based on the user's reading history (titles: {titles}), goals ({goals}), and vocabulary ({vocab}), recommend 3 books with reasons.

#### 2.1.3.6.0 Mapping Technique

Data aggregation and string formatting

#### 2.1.3.7.0 Justification

Required to construct a meaningful prompt for the LLM to generate book recommendations (REQ-FUNC-007).

#### 2.1.3.8.0 Complexity

medium

### 2.1.4.0.0 Mapping Id

#### 2.1.4.1.0 Mapping Id

MAP-004

#### 2.1.4.2.0 Source

OpenAI GPT-4 API Response (Text/JSON)

#### 2.1.4.3.0 Target

Recommendation Entity

#### 2.1.4.4.0 Transformation

splitting

#### 2.1.4.5.0 Configuration

*No data available*

#### 2.1.4.6.0 Mapping Technique

Text parsing and deserialization

#### 2.1.4.7.0 Justification

Required to parse the unstructured/semi-structured response from the LLM into structured, storable recommendation data (REQ-FUNC-007).

#### 2.1.4.8.0 Complexity

complex

### 2.1.5.0.0 Mapping Id

#### 2.1.5.1.0 Mapping Id

MAP-005

#### 2.1.5.2.0 Source

User Data (UserBook, ReadingSession)

#### 2.1.5.3.0 Target

Exported JSON File

#### 2.1.5.4.0 Transformation

merging

#### 2.1.5.5.0 Configuration

##### 2.1.5.5.1 Root Element

userData

##### 2.1.5.5.2 Collections

- library
- readingHistory

#### 2.1.5.6.0 Mapping Technique

Serialization into a hierarchical JSON structure

#### 2.1.5.7.0 Justification

Required to fulfill user data export requests in a machine-readable format as per REQ-FUNC-009.

#### 2.1.5.8.0 Complexity

medium

### 2.1.6.0.0 Mapping Id

#### 2.1.6.1.0 Mapping Id

MAP-006

#### 2.1.6.2.0 Source

ReadingSession Entity

#### 2.1.6.3.0 Target

UserReadingStats Entity

#### 2.1.6.4.0 Transformation

aggregation

#### 2.1.6.5.0 Configuration

##### 2.1.6.5.1 Group By

UserId, statDate

##### 2.1.6.5.2 Aggregates

- SUM(pagesRead)
- SUM(duration)

#### 2.1.6.6.0 Mapping Technique

Scheduled data aggregation job

#### 2.1.6.7.0 Justification

Required to pre-aggregate daily statistics to ensure fast dashboard loading times, supporting REQ-PERF-002.

#### 2.1.6.8.0 Complexity

medium

## 2.2.0.0.0 Object To Object Mappings

- {'sourceObject': 'Backend.Domain.Goal', 'targetObject': 'Backend.Presentation.GoalDto', 'fieldMappings': [{'sourceField': 'goalId', 'targetField': 'id', 'transformation': 'direct', 'dataTypeConversion': 'Guid to string'}, {'sourceField': 'goalType', 'targetField': 'type', 'transformation': 'direct', 'dataTypeConversion': 'Enum to string'}, {'sourceField': 'period', 'targetField': 'period', 'transformation': 'direct', 'dataTypeConversion': 'Enum to string'}, {'sourceField': 'targetValue', 'targetField': 'target', 'transformation': 'direct', 'dataTypeConversion': 'none'}, {'sourceField': 'currentValue', 'targetField': 'progress', 'transformation': 'direct', 'dataTypeConversion': 'none'}, {'sourceField': 'isActive', 'targetField': 'isActive', 'transformation': 'direct', 'dataTypeConversion': 'none'}]}

## 2.3.0.0.0 Data Type Conversions

### 2.3.1.0.0 From

#### 2.3.1.1.0 From

Guid (C#)

#### 2.3.1.2.0 To

String (JSON)

#### 2.3.1.3.0 Conversion Method

Standard string representation

#### 2.3.1.4.0 Validation Required

✅ Yes

### 2.3.2.0.0 From

#### 2.3.2.1.0 From

DateTime (C#)

#### 2.3.2.2.0 To

String (ISO 8601)

#### 2.3.2.3.0 Conversion Method

Standard ISO 8601 formatting

#### 2.3.2.4.0 Validation Required

✅ Yes

## 2.4.0.0.0 Bidirectional Mappings

- {'entity': 'Goal', 'forwardMapping': 'Domain.Goal to Presentation.GoalDto', 'reverseMapping': 'Presentation.CreateGoalDto to Application.CreateGoalCommand', 'consistencyStrategy': 'Source of truth is the Domain Entity. DTOs are stateless transfer objects.'}

# 3.0.0.0.0 Schema Validation Requirements

## 3.1.0.0.0 Field Level Validations

### 3.1.1.0.0 Field

#### 3.1.1.1.0 Field

User.email

#### 3.1.1.2.0 Rules

- notEmpty
- validEmailFormat

#### 3.1.1.3.0 Priority

🚨 critical

#### 3.1.1.4.0 Error Message

A valid email address is required.

### 3.1.2.0.0 Field

#### 3.1.2.1.0 Field

Goal.targetValue

#### 3.1.2.2.0 Rules

- greaterThan(0)

#### 3.1.2.3.0 Priority

🔴 high

#### 3.1.2.4.0 Error Message

Goal target must be a positive number.

## 3.2.0.0.0 Cross Field Validations

*No items available*

## 3.3.0.0.0 Business Rule Validations

### 3.3.1.0.0 Rule Id

#### 3.3.1.1.0 Rule Id

BR-FREE-USER-GOAL-LIMIT

#### 3.3.1.2.0 Description

A user on the 'Free User' tier cannot have more than one active goal.

#### 3.3.1.3.0 Fields

- User.subscriptionTier
- Goal.isActive

#### 3.3.1.4.0 Logic

IF User.subscriptionTier == 'Free' THEN COUNT(Goals WHERE isActive == true) MUST BE <= 1

#### 3.3.1.5.0 Priority

🚨 critical

### 3.3.2.0.0 Rule Id

#### 3.3.2.1.0 Rule Id

BR-FREE-USER-BOOK-LIMIT

#### 3.3.2.2.0 Description

A reverted user with more than 20 books must be prevented from adding new books.

#### 3.3.2.3.0 Fields

- User.subscriptionTier
- UserBook

#### 3.3.2.4.0 Logic

IF User.subscriptionTier == 'Free' AND COUNT(UserBooks) > 20 THEN prevent new UserBook creation.

#### 3.3.2.5.0 Priority

🚨 critical

## 3.4.0.0.0 Conditional Validations

*No items available*

## 3.5.0.0.0 Validation Groups

- {'groupName': 'UserRegistration', 'validations': ['User.email', 'User.passwordHash'], 'executionOrder': 1, 'stopOnFirstFailure': True}

# 4.0.0.0.0 Transformation Pattern Evaluation

## 4.1.0.0.0 Selected Patterns

### 4.1.1.0.0 Pattern

#### 4.1.1.1.0 Pattern

adapter

#### 4.1.1.2.0 Use Case

Integrating with the OpenAI API.

#### 4.1.1.3.0 Implementation

An `OpenAIClient` class in Backend.Infrastructure that transforms internal request models into HTTP requests for the external API and transforms the HTTP response back into an application-level result object.

#### 4.1.1.4.0 Justification

Decouples the application from the specific implementation details of the external LLM API, satisfying REQ-FUNC-007.

### 4.1.2.0.0 Pattern

#### 4.1.2.1.0 Pattern

converter

#### 4.1.2.2.0 Use Case

Mapping between Domain Entities and API DTOs.

#### 4.1.2.3.0 Implementation

Using a library like AutoMapper or writing explicit mapping methods to convert objects between layers (Domain, Application, Presentation).

#### 4.1.2.4.0 Justification

Essential for maintaining separation of concerns in a Layered/Clean Architecture.

## 4.2.0.0.0 Pipeline Processing

### 4.2.1.0.0 Required

✅ Yes

### 4.2.2.0.0 Stages

#### 4.2.2.1.0 Stage

##### 4.2.2.1.1 Stage

FetchUserData

##### 4.2.2.1.2 Transformation

Querying UserBook and ReadingSession tables.

##### 4.2.2.1.3 Dependencies

*No items available*

#### 4.2.2.2.0 Stage

##### 4.2.2.2.1 Stage

FormatData

##### 4.2.2.2.2 Transformation

MAP-005: Merging data into a hierarchical JSON structure.

##### 4.2.2.2.3 Dependencies

- FetchUserData

#### 4.2.2.3.0 Stage

##### 4.2.2.3.1 Stage

StoreFile

##### 4.2.2.3.2 Transformation

Saving the generated JSON to a secure storage location.

##### 4.2.2.3.3 Dependencies

- FormatData

#### 4.2.2.4.0 Stage

##### 4.2.2.4.1 Stage

NotifyUser

##### 4.2.2.4.2 Transformation

Sending an email with a download link.

##### 4.2.2.4.3 Dependencies

- StoreFile

### 4.2.3.0.0 Parallelization

❌ No

## 4.3.0.0.0 Processing Mode

### 4.3.1.0.0 Real Time

#### 4.3.1.1.0 Required

✅ Yes

#### 4.3.1.2.0 Scenarios

- API requests for fetching data
- Logging a reading session

#### 4.3.1.3.0 Latency Requirements

< 200ms P95 (REQ-PERF-001)

### 4.3.2.0.0 Batch

| Property | Value |
|----------|-------|
| Required | ✅ |
| Batch Size | 1 |
| Frequency | On-demand (Data Export), Daily (Subscription Statu... |

### 4.3.3.0.0 Streaming

| Property | Value |
|----------|-------|
| Required | ❌ |
| Streaming Framework | N/A |
| Windowing Strategy | N/A |

## 4.4.0.0.0 Canonical Data Model

### 4.4.1.0.0 Applicable

❌ No

### 4.4.2.0.0 Scope

*No items available*

### 4.4.3.0.0 Benefits

*No items available*

# 5.0.0.0.0 Version Handling Strategy

## 5.1.0.0.0 Schema Evolution

### 5.1.1.0.0 Strategy

Additive changes only

### 5.1.2.0.0 Versioning Scheme

API versioning (e.g., /api/v1/)

### 5.1.3.0.0 Compatibility

| Property | Value |
|----------|-------|
| Backward | ✅ |
| Forward | ❌ |
| Reasoning | The mobile client may not update immediately. The ... |

## 5.2.0.0.0 Transformation Versioning

| Property | Value |
|----------|-------|
| Mechanism | Code versioning via Git |
| Version Identification | N/A |
| Migration Strategy | Coordinated deployment of client and backend for b... |

## 5.3.0.0.0 Data Model Changes

| Property | Value |
|----------|-------|
| Migration Path | Entity Framework Core migrations |
| Rollback Strategy | EF Core CLI tools for reverting migrations. |
| Validation Strategy | Automated tests and manual QA on a staging environ... |

## 5.4.0.0.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology | N/A |
| Governance | API contract is documented (e.g., using OpenAPI/Sw... |

# 6.0.0.0.0 Performance Optimization

## 6.1.0.0.0 Critical Requirements

### 6.1.1.0.0 Operation

#### 6.1.1.1.0 Operation

Core API Endpoint Response

#### 6.1.1.2.0 Max Latency

200ms (P95)

#### 6.1.1.3.0 Throughput Target

10,000 concurrent users

#### 6.1.1.4.0 Justification

REQ-PERF-001

### 6.1.2.0.0 Operation

#### 6.1.2.1.0 Operation

Mobile Dashboard Screen Load

#### 6.1.2.2.0 Max Latency

1.5s

#### 6.1.2.3.0 Throughput Target

N/A

#### 6.1.2.4.0 Justification

REQ-PERF-002

## 6.2.0.0.0 Parallelization Opportunities

*No items available*

## 6.3.0.0.0 Caching Strategies

- {'cacheType': 'Distributed Cache (Redis)', 'cacheScope': 'Application-wide', 'evictionPolicy': 'LRU (Least Recently Used)', 'applicableTransformations': ['UserReadingStats queries for the dashboard']}

## 6.4.0.0.0 Memory Optimization

### 6.4.1.0.0 Techniques

- Using streaming APIs for large data exports to avoid loading the entire dataset into memory.

### 6.4.2.0.0 Thresholds

N/A

### 6.4.3.0.0 Monitoring Required

✅ Yes

## 6.5.0.0.0 Lazy Evaluation

### 6.5.1.0.0 Applicable

❌ No

### 6.5.2.0.0 Scenarios

*No items available*

### 6.5.3.0.0 Implementation

N/A

## 6.6.0.0.0 Bulk Processing

### 6.6.1.0.0 Required

✅ Yes

### 6.6.2.0.0 Batch Sizes

#### 6.6.2.1.0 Optimal

1,000

#### 6.6.2.2.0 Maximum

5,000

### 6.6.3.0.0 Parallelism

1

# 7.0.0.0.0 Error Handling And Recovery

## 7.1.0.0.0 Error Handling Strategies

### 7.1.1.0.0 Error Type

#### 7.1.1.1.0 Error Type

External API Failure (OpenAI, Google Books)

#### 7.1.1.2.0 Strategy

Circuit Breaker

#### 7.1.1.3.0 Fallback Action

Return a user-friendly error message or cached/default data (REQ-REL-002).

#### 7.1.1.4.0 Escalation Path

- Log error
- Alerting system

### 7.1.2.0.0 Error Type

#### 7.1.2.1.0 Error Type

Data Transformation Error (e.g., LLM response parsing)

#### 7.1.2.2.0 Strategy

Log and Fail

#### 7.1.2.3.0 Fallback Action

Return an error to the user indicating recommendations could not be generated.

#### 7.1.2.4.0 Escalation Path

- Log error with payload
- Alerting system

### 7.1.3.0.0 Error Type

#### 7.1.3.1.0 Error Type

Data Export Job Failure

#### 7.1.3.2.0 Strategy

Retry and Dead-letter

#### 7.1.3.3.0 Fallback Action

Update job status to 'Failed' and notify user.

#### 7.1.3.4.0 Escalation Path

- Hangfire Failed Jobs Dashboard

## 7.2.0.0.0 Logging Requirements

### 7.2.1.0.0 Log Level

warn

### 7.2.2.0.0 Included Data

- CorrelationId
- Timestamp
- ErrorMessage
- StackTrace

### 7.2.3.0.0 Retention Period

30 days

### 7.2.4.0.0 Alerting

✅ Yes

## 7.3.0.0.0 Partial Success Handling

### 7.3.1.0.0 Strategy

N/A

### 7.3.2.0.0 Reporting Mechanism

N/A

### 7.3.3.0.0 Recovery Actions

*No items available*

## 7.4.0.0.0 Circuit Breaking

- {'dependency': 'OpenAI GPT-4 API', 'threshold': '5 consecutive failures', 'timeout': '30s', 'fallbackStrategy': "Return cached recommendations or a 'service unavailable' message."}

## 7.5.0.0.0 Retry Strategies

- {'operation': 'External API calls', 'maxRetries': 3, 'backoffStrategy': 'exponential', 'retryConditions': ['HTTP 5xx status codes', 'Network timeout']}

## 7.6.0.0.0 Error Notifications

- {'condition': "Data Export Job enters 'Failed' state after all retries.", 'recipients': ['dev-alerts@readtrack.com'], 'severity': 'high', 'channel': 'Email'}

# 8.0.0.0.0 Project Specific Transformations

## 8.1.0.0.0 LLM Prompt Generation

### 8.1.1.0.0 Transformation Id

PST-001

### 8.1.2.0.0 Name

LLM Prompt Generation

### 8.1.3.0.0 Description

Transforms structured user data from multiple database tables into a single formatted text prompt for the OpenAI API.

### 8.1.4.0.0 Source

#### 8.1.4.1.0 Service

Backend.Application

#### 8.1.4.2.0 Model

UserBook, Goal, VocabularyItem

#### 8.1.4.3.0 Fields

- title
- author
- goalType
- targetValue
- word

### 8.1.5.0.0 Target

#### 8.1.5.1.0 Service

Backend.Infrastructure.OpenAIClient

#### 8.1.5.2.0 Model

OpenAI API Request

#### 8.1.5.3.0 Fields

- prompt

### 8.1.6.0.0 Transformation

#### 8.1.6.1.0 Type

🔹 aggregation

#### 8.1.6.2.0 Logic

Concatenate lists of book titles, goals, and vocabulary words into a pre-defined text template.

#### 8.1.6.3.0 Configuration

*No data available*

### 8.1.7.0.0 Frequency

on-demand

### 8.1.8.0.0 Criticality

high

### 8.1.9.0.0 Dependencies

- REQ-FUNC-007

### 8.1.10.0.0 Validation

#### 8.1.10.1.0 Pre Transformation

- Ensure user data is not empty.

#### 8.1.10.2.0 Post Transformation

- Ensure prompt string length is within API limits.

### 8.1.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Medium |
| Latency Requirement | < 100ms |
| Optimization Strategy | Efficient database queries. |

## 8.2.0.0.0 User Data Export to JSON

### 8.2.1.0.0 Transformation Id

PST-002

### 8.2.2.0.0 Name

User Data Export to JSON

### 8.2.3.0.0 Description

Aggregates a user's entire library and reading history into a single, structured JSON file for download.

### 8.2.4.0.0 Source

#### 8.2.4.1.0 Service

Backend.Infrastructure.DataExportJobHandler

#### 8.2.4.2.0 Model

UserBook, ReadingSession

#### 8.2.4.3.0 Fields

- *

### 8.2.5.0.0 Target

#### 8.2.5.1.0 Service

File Storage

#### 8.2.5.2.0 Model

JSON Document

#### 8.2.5.3.0 Fields

- library
- readingSessions

### 8.2.6.0.0 Transformation

#### 8.2.6.1.0 Type

🔹 merging

#### 8.2.6.2.0 Logic

Fetch all related entities for a user and serialize them into a nested JSON object.

#### 8.2.6.3.0 Configuration

*No data available*

### 8.2.7.0.0 Frequency

batch

### 8.2.8.0.0 Criticality

high

### 8.2.9.0.0 Dependencies

- REQ-FUNC-009
- REQ-DATA-001

### 8.2.10.0.0 Validation

#### 8.2.10.1.0 Pre Transformation

- Verify user exists.

#### 8.2.10.2.0 Post Transformation

- Validate JSON schema of the output file.

### 8.2.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Low |
| Latency Requirement | N/A (background job) |
| Optimization Strategy | Stream data from the database to the JSON serializ... |

## 8.3.0.0.0 Daily Reading Stats Aggregation

### 8.3.1.0.0 Transformation Id

PST-003

### 8.3.2.0.0 Name

Daily Reading Stats Aggregation

### 8.3.3.0.0 Description

Calculates total minutes and pages read per user per day from individual reading sessions.

### 8.3.4.0.0 Source

#### 8.3.4.1.0 Service

Backend Background Service

#### 8.3.4.2.0 Model

ReadingSession

#### 8.3.4.3.0 Fields

- startTime
- endTime
- pagesRead

### 8.3.5.0.0 Target

#### 8.3.5.1.0 Service

Database

#### 8.3.5.2.0 Model

UserReadingStats

#### 8.3.5.3.0 Fields

- totalMinutesRead
- totalPagesRead

### 8.3.6.0.0 Transformation

#### 8.3.6.1.0 Type

🔹 aggregation

#### 8.3.6.2.0 Logic

A scheduled job groups ReadingSession records by user and day, summing the duration and pages read, then upserts the result into the UserReadingStats table.

#### 8.3.6.3.0 Configuration

*No data available*

### 8.3.7.0.0 Frequency

batch

### 8.3.8.0.0 Criticality

medium

### 8.3.9.0.0 Dependencies

- REQ-PERF-002

### 8.3.10.0.0 Validation

#### 8.3.10.1.0 Pre Transformation

*No items available*

#### 8.3.10.2.0 Post Transformation

- Ensure aggregated values are non-negative.

### 8.3.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | High |
| Latency Requirement | N/A (background job) |
| Optimization Strategy | Process data in batches per user and use efficient... |

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

MAP-001: Domain Entity to API DTO Mapping

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Medium

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

MAP-002: API DTO to Client Local DB Mapping

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- MAP-001

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

low

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

PST-001: LLM Prompt Generation & Response Parsing

### 9.3.2.0.0 Priority

🔴 high

### 9.3.3.0.0 Dependencies

- MAP-001

### 9.3.4.0.0 Estimated Effort

High

### 9.3.5.0.0 Risk Level

high

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

PST-002: User Data Export to JSON

### 9.4.2.0.0 Priority

🟡 medium

### 9.4.3.0.0 Dependencies

*No items available*

### 9.4.4.0.0 Estimated Effort

Medium

### 9.4.5.0.0 Risk Level

medium

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

OpenAI API response format changes unexpectedly.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Implement robust and defensive parsing logic. Add contract tests against the OpenAI API. Log the raw payload of any failed parsing attempts for debugging.

### 10.1.5.0.0 Contingency Plan

Temporarily disable the recommendation feature and display a user-friendly message.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Data export job consumes excessive memory for users with large amounts of data.

### 10.2.2.0.0 Impact

medium

### 10.2.3.0.0 Probability

medium

### 10.2.4.0.0 Mitigation

Design the transformation to stream data from the database directly to the output file without loading the entire dataset into memory.

### 10.2.5.0.0 Contingency Plan

Set a timeout for the job and notify the user to contact support if it fails.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Implementation

### 11.1.2.0.0 Recommendation

Use a dedicated library like AutoMapper in the .NET backend for handling the boilerplate of DTO-to-Entity transformations.

### 11.1.3.0.0 Justification

Reduces manual mapping code, improves maintainability, and decreases the likelihood of errors when models change.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Configure AutoMapper profiles during application startup.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Testing

### 11.2.2.0.0 Recommendation

Create a suite of transformation-specific unit tests for all complex mappings, especially for LLM response parsing and data export formatting.

### 11.2.3.0.0 Justification

Ensures that transformations are correct and resilient to edge cases in the source data, preventing data corruption or application crashes.

### 11.2.4.0.0 Priority

🔴 high

### 11.2.5.0.0 Implementation Notes

Tests should cover valid inputs, null/empty values, and malformed data.

## 11.3.0.0.0 Category

### 11.3.1.0.0 Category

🔹 Resilience

### 11.3.2.0.0 Recommendation

Ensure the LLM response parser is designed as a 'tolerant reader', ignoring unknown fields in the response.

### 11.3.3.0.0 Justification

Prevents the entire transformation from failing if the external API adds new, non-critical information to its response, improving forward compatibility.

### 11.3.4.0.0 Priority

🟡 medium

### 11.3.5.0.0 Implementation Notes

Catch and log exceptions for unexpected data structures but attempt to process the known parts of the payload.

