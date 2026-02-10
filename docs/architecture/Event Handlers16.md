# 1 System Overview

## 1.1 Analysis Date

2024-10-27

## 1.2 Architecture Type

Modular Monolith

## 1.3 Technology Stack

- .NET 8
- C# 12
- MediatR
- Hangfire

## 1.4 Bounded Contexts

- User Management
- Subscription Management
- Book & Reading Management
- Goal Management

# 2.0 Project Specific Events

## 2.1 Event Id

### 2.1.1 Event Id

EVT-001

### 2.1.2 Event Name

ReadingSessionLogged

### 2.1.3 Event Type

domain

### 2.1.4 Category

🔹 Book & Reading Management

### 2.1.5 Description

Fired after a user successfully logs a new reading session. This event is used to trigger updates to dependent data like goal progress and aggregated reading statistics.

### 2.1.6 Trigger Condition

Successful creation of a new record in the 'ReadingSession' table.

### 2.1.7 Source Context

Book & Reading Management

### 2.1.8 Target Contexts

- Goal Management
- User Management

### 2.1.9 Payload

#### 2.1.9.1 Schema

| Property | Value |
|----------|-------|
| User Id | Guid |
| Reading Session Id | Guid |
| Pages Read | int |
| Duration In Minutes | int |
| Session Date | DateTime |

#### 2.1.9.2 Required Fields

- userId
- readingSessionId
- sessionDate

#### 2.1.9.3 Optional Fields

- pagesRead
- durationInMinutes

### 2.1.10.0 Frequency

high

### 2.1.11.0 Business Criticality

important

### 2.1.12.0 Data Source

| Property | Value |
|----------|-------|
| Database | PostgreSQL |
| Table | ReadingSession |
| Operation | create |

### 2.1.13.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | ReadingSession.Logged |
| Exchange | In-Process (MediatR) |
| Queue | N/A |

### 2.1.14.0 Consumers

#### 2.1.14.1 Service

##### 2.1.14.1.1 Service

GoalManagementService

##### 2.1.14.1.2 Handler

UpdateGoalProgressHandler

##### 2.1.14.1.3 Processing Type

async

#### 2.1.14.2.0 Service

##### 2.1.14.2.1 Service

UserManagementService

##### 2.1.14.2.2 Handler

UpdateUserReadingStatsHandler

##### 2.1.14.2.3 Processing Type

async

### 2.1.15.0.0 Dependencies

*No items available*

### 2.1.16.0.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | In-process retry via MediatR pipeline behavior |
| Dead Letter Queue | N/A (errors fail the originating request) |
| Timeout Ms | 5000 |

## 2.2.0.0.0 Event Id

### 2.2.1.0.0 Event Id

EVT-002

### 2.2.2.0.0 Event Name

SubscriptionTerminated

### 2.2.3.0.0 Event Type

domain

### 2.2.4.0.0 Category

🔹 Subscription Management

### 2.2.5.0.0 Description

Published when a user's premium subscription ends due to expiration, cancellation, or refund. Triggers the process to revert the user's account to the 'Free' tier.

### 2.2.6.0.0 Trigger Condition

The 'SubscriptionStatusService' background job or a payment provider webhook detects a subscription has ended.

### 2.2.7.0.0 Source Context

Subscription Management

### 2.2.8.0.0 Target Contexts

- User Management

### 2.2.9.0.0 Payload

#### 2.2.9.1.0 Schema

| Property | Value |
|----------|-------|
| User Id | Guid |
| Subscription Id | Guid |
| Termination Reason | string (Expired, Canceled, Refunded) |

#### 2.2.9.2.0 Required Fields

- userId
- subscriptionId
- terminationReason

#### 2.2.9.3.0 Optional Fields

*No items available*

### 2.2.10.0.0 Frequency

medium

### 2.2.11.0.0 Business Criticality

critical

### 2.2.12.0.0 Data Source

| Property | Value |
|----------|-------|
| Database | PostgreSQL |
| Table | Subscription |
| Operation | update |

### 2.2.13.0.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | Subscription.Terminated |
| Exchange | In-Process (MediatR) |
| Queue | N/A |

### 2.2.14.0.0 Consumers

- {'service': 'UserManagementService', 'handler': 'RevertUserToFreeTierHandler', 'processingType': 'async'}

### 2.2.15.0.0 Dependencies

- REQ-FUNC-002

### 2.2.16.0.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | Durable retry via Hangfire job |
| Dead Letter Queue | Hangfire Failed Jobs Dashboard |
| Timeout Ms | 30000 |

## 2.3.0.0.0 Event Id

### 2.3.1.0.0 Event Id

EVT-003

### 2.3.2.0.0 Event Name

DataExportRequested

### 2.3.3.0.0 Event Type

command

### 2.3.4.0.0 Category

🔹 User Management

### 2.3.5.0.0 Description

Fired when a user requests an export of their data. This event enqueues a background job to perform the data aggregation and generation of the export file.

### 2.3.6.0.0 Trigger Condition

API call to the 'Export My Data' endpoint in 'UsersController'.

### 2.3.7.0.0 Source Context

User Management

### 2.3.8.0.0 Target Contexts

- Infrastructure (Background Jobs)

### 2.3.9.0.0 Payload

#### 2.3.9.1.0 Schema

| Property | Value |
|----------|-------|
| User Id | Guid |
| Data Export Job Id | Guid |
| Format | string (JSON) |

#### 2.3.9.2.0 Required Fields

- userId
- dataExportJobId

#### 2.3.9.3.0 Optional Fields

- format

### 2.3.10.0.0 Frequency

low

### 2.3.11.0.0 Business Criticality

important

### 2.3.12.0.0 Data Source

| Property | Value |
|----------|-------|
| Database | PostgreSQL |
| Table | DataExportJob |
| Operation | create |

### 2.3.13.0.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | DataExport.Requested |
| Exchange | N/A (Direct Hangfire Enqueue) |
| Queue | default |

### 2.3.14.0.0 Consumers

- {'service': 'DataExportJobService', 'handler': 'GenerateUserDataExportJob', 'processingType': 'async'}

### 2.3.15.0.0 Dependencies

- REQ-FUNC-009

### 2.3.16.0.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | Built-in Hangfire retry policy |
| Dead Letter Queue | Hangfire Failed Jobs Dashboard |
| Timeout Ms | 600000 |

# 3.0.0.0.0 Event Types And Schema Design

## 3.1.0.0.0 Essential Event Types

### 3.1.1.0.0 Event Name

#### 3.1.1.1.0 Event Name

ReadingSessionLogged

#### 3.1.1.2.0 Category

🔹 domain

#### 3.1.1.3.0 Description

Core event for tracking user activity and updating related features like goals.

#### 3.1.1.4.0 Priority

🔴 high

### 3.1.2.0.0 Event Name

#### 3.1.2.1.0 Event Name

SubscriptionTerminated

#### 3.1.2.2.0 Category

🔹 domain

#### 3.1.2.3.0 Description

Critical business event for managing user account tiers and access rights.

#### 3.1.2.4.0 Priority

🔴 high

### 3.1.3.0.0 Event Name

#### 3.1.3.1.0 Event Name

DataExportRequested

#### 3.1.3.2.0 Category

🔹 command

#### 3.1.3.3.0 Description

Initiates a long-running, user-facing process required for legal compliance (GDPR).

#### 3.1.3.4.0 Priority

🟡 medium

## 3.2.0.0.0 Schema Design

| Property | Value |
|----------|-------|
| Format | JSON |
| Reasoning | JSON is native to the .NET web stack, human-readab... |
| Consistency Approach | Use of shared C# class definitions for event paylo... |

## 3.3.0.0.0 Schema Evolution

| Property | Value |
|----------|-------|
| Backward Compatibility | ✅ |
| Forward Compatibility | ❌ |
| Strategy | Additive changes only. New optional fields can be ... |

## 3.4.0.0.0 Event Structure

### 3.4.1.0.0 Standard Fields

- EventId (Guid)
- Timestamp (DateTimeOffset)
- CorrelationId (Guid)

### 3.4.2.0.0 Metadata Requirements

- CorrelationId to trace the flow of a single user request through synchronous and asynchronous processes.

# 4.0.0.0.0 Event Routing And Processing

## 4.1.0.0.0 Routing Mechanisms

### 4.1.1.0.0 In-process messaging (MediatR)

#### 4.1.1.1.0 Type

🔹 In-process messaging (MediatR)

#### 4.1.1.2.0 Description

Used for publishing and handling domain events within the same process. It's lightweight and ensures that event handling can be part of the same transaction as the originating operation.

#### 4.1.1.3.0 Use Case

Decoupling modules within the monolith, e.g., updating goals after a reading session is logged.

### 4.1.2.0.0 Background job queueing (Hangfire)

#### 4.1.2.1.0 Type

🔹 Background job queueing (Hangfire)

#### 4.1.2.2.0 Description

Used for durable, out-of-process, asynchronous task execution. Provides persistence, retries, and monitoring for long-running or critical background tasks.

#### 4.1.2.3.0 Use Case

Generating user data exports (REQ-FUNC-009) and processing subscription status changes (REQ-FUNC-002).

## 4.2.0.0.0 Processing Patterns

### 4.2.1.0.0 Pattern

#### 4.2.1.1.0 Pattern

sequential

#### 4.2.1.2.0 Applicable Scenarios

- When handlers must run in a specific order or one depends on the outcome of another. Not the primary pattern for this system.

#### 4.2.1.3.0 Implementation

N/A - The current requirements are satisfied by parallel, independent handlers.

### 4.2.2.0.0 Pattern

#### 4.2.2.1.0 Pattern

parallel

#### 4.2.2.2.0 Applicable Scenarios

- Multiple independent handlers subscribing to the same event, e.g., `ReadingSessionLogged` triggering both goal updates and stats aggregation.

#### 4.2.2.3.0 Implementation

MediatR's default behavior dispatches notifications to all registered handlers, which can execute concurrently.

## 4.3.0.0.0 Filtering And Subscription

### 4.3.1.0.0 Filtering Mechanism

Topic-based

### 4.3.2.0.0 Subscription Model

Handler interfaces (e.g., INotificationHandler<T> in MediatR) in the application's IoC container handle subscriptions.

### 4.3.3.0.0 Routing Keys

- N/A for MediatR (based on event type), queue names for Hangfire (e.g., 'default', 'critical').

## 4.4.0.0.0 Handler Isolation

| Property | Value |
|----------|-------|
| Required | ❌ |
| Approach | Handlers run within the same application process. ... |
| Reasoning | The system is not a distributed microservices arch... |

## 4.5.0.0.0 Delivery Guarantees

| Property | Value |
|----------|-------|
| Level | at-least-once |
| Justification | This is the default for Hangfire, which is used fo... |
| Implementation | Hangfire's persistence to the database ensures job... |

# 5.0.0.0.0 Event Storage And Replay

## 5.1.0.0.0 Persistence Requirements

| Property | Value |
|----------|-------|
| Required | ❌ |
| Duration | N/A |
| Reasoning | The system does not use event sourcing. State is s... |

## 5.2.0.0.0 Event Sourcing

### 5.2.1.0.0 Necessary

❌ No

### 5.2.2.0.0 Justification

The architecture is a standard layered design with a relational database as the source of truth. Implementing event sourcing would be a major architectural shift and is not required by any functional or non-functional requirements.

### 5.2.3.0.0 Scope

*No items available*

## 5.3.0.0.0 Technology Options

- {'technology': 'N/A - No event store required', 'suitability': 'low', 'reasoning': 'Introducing an event store like EventStoreDB or Kafka would add significant complexity without providing clear benefits over the current state-based persistence model for this system.'}

## 5.4.0.0.0 Replay Capabilities

### 5.4.1.0.0 Required

❌ No

### 5.4.2.0.0 Scenarios

*No items available*

### 5.4.3.0.0 Implementation

N/A. System recovery is handled via traditional database backups, not event replay.

## 5.5.0.0.0 Retention Policy

| Property | Value |
|----------|-------|
| Strategy | No retention |
| Duration | N/A |
| Archiving Approach | Events are not stored after being processed. |

# 6.0.0.0.0 Dead Letter Queue And Error Handling

## 6.1.0.0.0 Dead Letter Strategy

| Property | Value |
|----------|-------|
| Approach | Utilize Hangfire's built-in 'Failed Jobs' list, wh... |
| Queue Configuration | N/A - Provided out-of-the-box by Hangfire. |
| Processing Logic | Manual inspection and re-queueing of failed jobs v... |

## 6.2.0.0.0 Retry Policies

### 6.2.1.0.0 Error Type

#### 6.2.1.1.0 Error Type

Transient external API failures (e.g., OpenAI)

#### 6.2.1.2.0 Max Retries

3

#### 6.2.1.3.0 Backoff Strategy

exponential

#### 6.2.1.4.0 Delay Configuration

Initial delay of 1 minute, doubling with each attempt (Hangfire default).

### 6.2.2.0.0 Error Type

#### 6.2.2.1.0 Error Type

Database Deadlocks

#### 6.2.2.2.0 Max Retries

2

#### 6.2.2.3.0 Backoff Strategy

fixed

#### 6.2.2.4.0 Delay Configuration

100ms delay.

## 6.3.0.0.0 Poison Message Handling

| Property | Value |
|----------|-------|
| Detection Mechanism | A job that fails all configured retry attempts in ... |
| Handling Strategy | The job is moved to the 'Failed Jobs' list for man... |
| Alerting Required | ✅ |

## 6.4.0.0.0 Error Notification

### 6.4.1.0.0 Channels

- Email
- Logging System (e.g., Serilog sink to CloudWatch)

### 6.4.2.0.0 Severity

critical

### 6.4.3.0.0 Recipients

- Development Team
- On-call Engineer

## 6.5.0.0.0 Recovery Procedures

- {'scenario': 'A `DataExportRequested` job fails due to a persistent bug.', 'procedure': '1. Dev team is alerted. 2. Analyze exception in Hangfire dashboard and logs. 3. Fix the underlying code. 4. Deploy the fix. 5. Manually re-queue the failed job from the Hangfire dashboard.', 'automationLevel': 'semi-automated'}

# 7.0.0.0.0 Event Versioning Strategy

## 7.1.0.0.0 Schema Evolution Approach

| Property | Value |
|----------|-------|
| Strategy | Additive Changes Only |
| Versioning Scheme | N/A - No formal versioning scheme required for in-... |
| Migration Strategy | Coordinated deployment. Both publisher and consume... |

## 7.2.0.0.0 Compatibility Requirements

| Property | Value |
|----------|-------|
| Backward Compatible | ✅ |
| Forward Compatible | ❌ |
| Reasoning | Handlers should be written to tolerate new, unexpe... |

## 7.3.0.0.0 Version Identification

| Property | Value |
|----------|-------|
| Mechanism | None |
| Location | N/A |
| Format | N/A |

## 7.4.0.0.0 Consumer Upgrade Strategy

| Property | Value |
|----------|-------|
| Approach | Atomic Deployment |
| Rollout Strategy | All consumers are part of the same application and... |
| Rollback Procedure | Rollback the entire application deployment to the ... |

## 7.5.0.0.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology | N/A |
| Governance | Schema is governed by the shared C# class definiti... |

# 8.0.0.0.0 Event Monitoring And Observability

## 8.1.0.0.0 Monitoring Capabilities

### 8.1.1.0.0 Capability

#### 8.1.1.1.0 Capability

Background Job Monitoring

#### 8.1.1.2.0 Justification

Essential for tracking the status of critical, long-running tasks like data exports and subscription management.

#### 8.1.1.3.0 Implementation

Hangfire Dashboard.

### 8.1.2.0.0 Capability

#### 8.1.2.1.0 Capability

Structured Event Logging

#### 8.1.2.2.0 Justification

To provide a detailed audit trail of when events are published and processed, and to diagnose issues.

#### 8.1.2.3.0 Implementation

A custom MediatR pipeline behavior that logs every event notification using Serilog.

## 8.2.0.0.0 Tracing And Correlation

| Property | Value |
|----------|-------|
| Tracing Required | ✅ |
| Correlation Strategy | A unique Correlation ID is generated at the start ... |
| Trace Id Propagation | The Correlation ID is stored in the HttpContext an... |

## 8.3.0.0.0 Performance Metrics

### 8.3.1.0.0 Metric

#### 8.3.1.1.0 Metric

Event Handler Execution Time (ms)

#### 8.3.1.2.0 Threshold

> 500ms

#### 8.3.1.3.0 Alerting

✅ Yes

### 8.3.2.0.0 Metric

#### 8.3.2.1.0 Metric

Number of Failed Hangfire Jobs

#### 8.3.2.2.0 Threshold

> 0 in 5 minutes

#### 8.3.2.3.0 Alerting

✅ Yes

## 8.4.0.0.0 Event Flow Visualization

| Property | Value |
|----------|-------|
| Required | ❌ |
| Tooling | N/A |
| Scope | The event flow is simple and contained within the ... |

## 8.5.0.0.0 Alerting Requirements

### 8.5.1.0.0 Condition

#### 8.5.1.1.0 Condition

Any Hangfire job enters the 'Failed' state.

#### 8.5.1.2.0 Severity

critical

#### 8.5.1.3.0 Response Time

15 minutes

#### 8.5.1.4.0 Escalation Path

- On-call Engineer
- Lead Developer

### 8.5.2.0.0 Condition

#### 8.5.2.1.0 Condition

P99 event handler execution time exceeds 500ms for a sustained period of 5 minutes.

#### 8.5.2.2.0 Severity

warning

#### 8.5.2.3.0 Response Time

1 hour

#### 8.5.2.4.0 Escalation Path

- Development Team

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

MediatR-based handler for `ReadingSessionLogged`

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

- Goal Management Module
- User Stats Module

### 9.1.4.0.0 Estimated Effort

Low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Hangfire job for `DataExportRequested`

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- User Data Access Layer

### 9.2.4.0.0 Estimated Effort

Medium

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Hangfire job/MediatR handler for `SubscriptionTerminated`

### 9.3.2.0.0 Priority

🔴 high

### 9.3.3.0.0 Dependencies

- User Management Module
- Subscription Data Model

### 9.3.4.0.0 Estimated Effort

Medium

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

Correlation ID propagation middleware

### 9.4.2.0.0 Priority

🟡 medium

### 9.4.3.0.0 Dependencies

*No items available*

### 9.4.4.0.0 Estimated Effort

Low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Over-engineering with an external message broker

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

low

### 10.1.4.0.0 Mitigation

Strictly adhere to the existing architecture. Use in-process MediatR for domain events and Hangfire for background jobs, as they are sufficient for the defined requirements.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

A failing event handler blocks or impacts main application performance

### 10.2.2.0.0 Impact

medium

### 10.2.3.0.0 Probability

medium

### 10.2.4.0.0 Mitigation

Ensure all MediatR handlers are fully asynchronous (`async`/`await`) and non-blocking. Implement timeouts for handlers that interact with external systems. Monitor handler execution time.

## 10.3.0.0.0 Risk

### 10.3.1.0.0 Risk

Inconsistent state due to partial failure within a transaction that involves event publishing

### 10.3.2.0.0 Impact

high

### 10.3.3.0.0 Probability

low

### 10.3.4.0.0 Mitigation

Wrap the database save operation and the MediatR `Publish` call within the same database transaction (e.g., using a transaction scope). If the handler fails, the entire operation is rolled back.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Implementation

### 11.1.2.0.0 Recommendation

Use MediatR's `INotification` and `INotificationHandler` interfaces as the standard for all in-process domain events to maintain a consistent pattern.

### 11.1.3.0.0 Justification

Provides a clean, decoupled, and testable way to handle side effects of primary operations within the monolith.

### 11.1.4.0.0 Priority

🔴 high

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Observability

### 11.2.2.0.0 Recommendation

Implement a custom MediatR pipeline behavior to automatically log and time every event handler execution, including the Correlation ID.

### 11.2.3.0.0 Justification

This provides critical observability into the asynchronous parts of the system with minimal repetitive code in each handler.

### 11.2.4.0.0 Priority

🔴 high

## 11.3.0.0.0 Category

### 11.3.1.0.0 Category

🔹 Operations

### 11.3.2.0.0 Recommendation

Configure alerts on the number of failed jobs in the Hangfire dashboard to ensure prompt investigation of processing failures.

### 11.3.3.0.0 Justification

Automated alerting is necessary for proactive handling of poison messages and other persistent errors in critical background tasks.

### 11.3.4.0.0 Priority

🟡 medium

