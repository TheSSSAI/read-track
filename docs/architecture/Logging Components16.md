# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- Serilog
- Hangfire
- Polly
- Flutter

## 1.3 Monitoring Requirements

- REQ-PERF-001
- REQ-REL-002
- REQ-FUNC-009
- REQ-DATA-001

## 1.4 System Architecture

ModularMonolith

## 1.5 Environment

production

# 2.0 Log Level And Category Strategy

## 2.1 Default Log Level

Information

## 2.2 Environment Specific Levels

### 2.2.1 Environment

#### 2.2.1.1 Environment

Production

#### 2.2.1.2 Log Level

Information

#### 2.2.1.3 Justification

Captures essential operational events, warnings, and errors without excessive noise.

### 2.2.2.0 Environment

#### 2.2.2.1 Environment

Staging/Development

#### 2.2.2.2 Log Level

Debug

#### 2.2.2.3 Justification

Enables verbose logging, including EF Core database commands, for detailed troubleshooting during development and testing.

## 2.3.0.0 Component Categories

### 2.3.1.0 Component

#### 2.3.1.1 Component

Backend.API

#### 2.3.1.2 Category

🔹 Microsoft.AspNetCore

#### 2.3.1.3 Log Level

Warning

#### 2.3.1.4 Verbose Logging

❌ No

#### 2.3.1.5 Justification

Reduces noise from the web server pipeline, focusing on warnings and errors.

### 2.3.2.0 Component

#### 2.3.2.1 Component

Backend.Infrastructure.Persistence

#### 2.3.2.2 Category

🔹 Microsoft.EntityFrameworkCore.Database.Command

#### 2.3.2.3 Log Level

Warning

#### 2.3.2.4 Verbose Logging

❌ No

#### 2.3.2.5 Justification

In production, SQL queries are only logged if the execution time exceeds a threshold (via performance monitoring), to support REQ-PERF-001.

### 2.3.3.0 Component

#### 2.3.3.1 Component

Backend.Infrastructure.Clients

#### 2.3.3.2 Category

🔹 ReadTrack.ExternalClients

#### 2.3.3.3 Log Level

Information

#### 2.3.3.4 Verbose Logging

✅ Yes

#### 2.3.3.5 Justification

Logs initiation of external calls. Warnings are logged for retries and circuit breaker state changes to meet REQ-REL-002.

### 2.3.4.0 Component

#### 2.3.4.1 Component

Backend.Infrastructure.Jobs

#### 2.3.4.2 Category

🔹 Hangfire

#### 2.3.4.3 Log Level

Information

#### 2.3.4.4 Verbose Logging

❌ No

#### 2.3.4.5 Justification

Logs the start, completion, and failure of background jobs like Data Export, which is essential for auditing REQ-FUNC-009.

## 2.4.0.0 Sampling Strategies

*No items available*

## 2.5.0.0 Logging Approach

### 2.5.1.0 Structured

✅ Yes

### 2.5.2.0 Format

JSON

### 2.5.3.0 Standard Fields

- Timestamp
- Level
- MessageTemplate
- Exception

### 2.5.4.0 Custom Fields

- CorrelationId
- TraceId
- SpanId
- UserId
- RequestPath
- RequestMethod
- DurationMs
- JobId

# 3.0.0.0 Log Aggregation Architecture

## 3.1.0.0 Collection Mechanism

### 3.1.1.0 Type

🔹 library

### 3.1.2.0 Technology

Serilog with AWS CloudWatch Sink

### 3.1.3.0 Configuration

*No data available*

### 3.1.4.0 Justification

Directly integrated into the .NET application for rich contextual logging, aligning with the monitoring plan's specification of a CloudWatch sink.

## 3.2.0.0 Strategy

| Property | Value |
|----------|-------|
| Approach | centralized |
| Reasoning | All backend logs are sent to a single AWS CloudWat... |
| Local Retention | none |

## 3.3.0.0 Shipping Methods

- {'protocol': 'HTTP', 'destination': 'AWS CloudWatch Logs', 'reliability': 'at-least-once', 'compression': True}

## 3.4.0.0 Buffering And Batching

| Property | Value |
|----------|-------|
| Buffer Size | 10000 events |
| Batch Size | 100 |
| Flush Interval | 10s |
| Backpressure Handling | Handled by Serilog sink library. |

## 3.5.0.0 Transformation And Enrichment

- {'transformation': 'Contextual Property Enrichment', 'purpose': 'Add CorrelationId, TraceId, and UserId to all log entries via Serilog enrichers to enable effective tracing and user-specific debugging.', 'stage': 'collection'}

## 3.6.0.0 High Availability

| Property | Value |
|----------|-------|
| Required | ✅ |
| Redundancy | Provided by AWS CloudWatch Logs service across mul... |
| Failover Strategy | Managed by AWS. |

# 4.0.0.0 Retention Policy Design

## 4.1.0.0 Retention Periods

### 4.1.1.0 Log Type

#### 4.1.1.1 Log Type

OperationalLogs

#### 4.1.1.2 Retention Period

30 days

#### 4.1.1.3 Justification

Sufficient for troubleshooting most operational issues and performance anomalies (REQ-PERF-001, REQ-REL-002).

#### 4.1.1.4 Compliance Requirement

None

### 4.1.2.0 Log Type

#### 4.1.2.1 Log Type

AuditEvents

#### 4.1.2.2 Retention Period

90 days

#### 4.1.2.3 Justification

Retains logs related to critical business events like subscription termination (REQ-FUNC-002) and data export job status (REQ-FUNC-009) for a longer period.

#### 4.1.2.4 Compliance Requirement

GDPR (Right to Data Portability)

## 4.2.0.0 Compliance Requirements

*No items available*

## 4.3.0.0 Volume Impact Analysis

| Property | Value |
|----------|-------|
| Estimated Daily Volume | 5-10 GB |
| Storage Cost Projection | Based on AWS CloudWatch pricing. |
| Compression Ratio | Varies |

## 4.4.0.0 Storage Tiering

*No data available*

## 4.5.0.0 Compression Strategy

| Property | Value |
|----------|-------|
| Algorithm | Gzip |
| Compression Level | default |
| Expected Ratio | 5:1 - 10:1 |

## 4.6.0.0 Anonymization Requirements

- {'dataType': 'PII', 'method': 'exclude', 'timeline': 'at-source', 'compliance': 'REQ-DATA-001'}

# 5.0.0.0 Search Capability Requirements

## 5.1.0.0 Essential Capabilities

### 5.1.1.0 Capability

#### 5.1.1.1 Capability

Trace requests via CorrelationId/TraceId

#### 5.1.1.2 Performance Requirement

< 5s

#### 5.1.1.3 Justification

Essential for debugging end-to-end request flows from the API gateway to the database.

### 5.1.2.0 Capability

#### 5.1.2.1 Capability

Isolate user activity via UserId

#### 5.1.2.2 Performance Requirement

< 10s

#### 5.1.2.3 Justification

Required for troubleshooting issues reported by a specific user.

### 5.1.3.0 Capability

#### 5.1.3.1 Capability

Filter logs by level, component, and time

#### 5.1.3.2 Performance Requirement

< 5s

#### 5.1.3.3 Justification

Basic requirement for operational monitoring and incident response.

## 5.2.0.0 Performance Characteristics

| Property | Value |
|----------|-------|
| Search Latency | seconds |
| Concurrent Users | 10 |
| Query Complexity | simple |
| Indexing Strategy | Automatic indexing by AWS CloudWatch Logs Insights... |

## 5.3.0.0 Indexed Fields

### 5.3.1.0 Field

#### 5.3.1.1 Field

CorrelationId

#### 5.3.1.2 Index Type

JSON Property

#### 5.3.1.3 Search Pattern

exact match

#### 5.3.1.4 Frequency

high

### 5.3.2.0 Field

#### 5.3.2.1 Field

UserId

#### 5.3.2.2 Index Type

JSON Property

#### 5.3.2.3 Search Pattern

exact match

#### 5.3.2.4 Frequency

medium

### 5.3.3.0 Field

#### 5.3.3.1 Field

Level

#### 5.3.3.2 Index Type

JSON Property

#### 5.3.3.3 Search Pattern

exact match

#### 5.3.3.4 Frequency

high

## 5.4.0.0 Full Text Search

### 5.4.1.0 Required

✅ Yes

### 5.4.2.0 Fields

- MessageTemplate
- Exception

### 5.4.3.0 Search Engine

AWS CloudWatch Logs Insights

### 5.4.4.0 Relevance Scoring

❌ No

## 5.5.0.0 Correlation And Tracing

### 5.5.1.0 Correlation Ids

- CorrelationId

### 5.5.2.0 Trace Id Propagation

OpenTelemetry SDK adds TraceId/SpanId, which is included in the log context and propagated via HTTP headers.

### 5.5.3.0 Span Correlation

✅ Yes

### 5.5.4.0 Cross Service Tracing

✅ Yes

## 5.6.0.0 Dashboard Requirements

*No items available*

# 6.0.0.0 Storage Solution Selection

## 6.1.0.0 Selected Technology

### 6.1.1.0 Primary

AWS CloudWatch Logs

### 6.1.2.0 Reasoning

Fully managed, scalable, and directly aligns with the specified monitoring plan. Natively integrates with other AWS services for alerting and metrics.

### 6.1.3.0 Alternatives

- ELK Stack
- Datadog

## 6.2.0.0 Scalability Requirements

| Property | Value |
|----------|-------|
| Expected Growth Rate | 20% MoM |
| Peak Load Handling | Handled automatically by the managed service. |
| Horizontal Scaling | ✅ |

## 6.3.0.0 Cost Performance Analysis

*No items available*

## 6.4.0.0 Backup And Recovery

| Property | Value |
|----------|-------|
| Backup Frequency | Managed by AWS. |
| Recovery Time Objective | N/A |
| Recovery Point Objective | N/A |
| Testing Frequency | N/A |

## 6.5.0.0 Geo Distribution

### 6.5.1.0 Required

❌ No

## 6.6.0.0 Data Sovereignty

*No items available*

# 7.0.0.0 Access Control And Compliance

## 7.1.0.0 Access Control Requirements

### 7.1.1.0 Role

#### 7.1.1.1 Role

Developer

#### 7.1.1.2 Permissions

- read
- query

#### 7.1.1.3 Log Types

- *

#### 7.1.1.4 Justification

Allows developers to troubleshoot issues in staging and production environments without modification rights.

### 7.1.2.0 Role

#### 7.1.2.1 Role

Operator

#### 7.1.2.2 Permissions

- read
- query
- manage

#### 7.1.2.3 Log Types

- *

#### 7.1.2.4 Justification

Allows operations team to manage log groups, retention policies, and configure alerts.

## 7.2.0.0 Sensitive Data Handling

- {'dataType': 'PII', 'handlingStrategy': 'exclude', 'fields': ['User.email', 'User.passwordHash'], 'complianceRequirement': 'REQ-DATA-001'}

## 7.3.0.0 Encryption Requirements

### 7.3.1.0 In Transit

| Property | Value |
|----------|-------|
| Required | ✅ |
| Protocol | HTTPS/TLS 1.2+ |
| Certificate Management | Managed by AWS SDK. |

### 7.3.2.0 At Rest

| Property | Value |
|----------|-------|
| Required | ✅ |
| Algorithm | AES-256 |
| Key Management | AWS KMS (default AWS-managed key). |

## 7.4.0.0 Audit Trail

| Property | Value |
|----------|-------|
| Log Access | ✅ |
| Retention Period | 1 year |
| Audit Log Location | AWS CloudTrail |
| Compliance Reporting | ❌ |

## 7.5.0.0 Regulatory Compliance

- {'regulation': 'GDPR', 'applicableComponents': ['Backend.Application', 'Backend.Infrastructure'], 'specificRequirements': ['REQ-DATA-001: Do not log PII.'], 'evidenceCollection': 'Log access is audited via AWS CloudTrail.'}

## 7.6.0.0 Data Protection Measures

*No items available*

## 7.7.0.0 Project Specific Logging Config

### 7.7.1.0 Logging Config

#### 7.7.1.1 Level

🔹 Information

#### 7.7.1.2 Retention

30 days

#### 7.7.1.3 Aggregation

Centralized to AWS CloudWatch

#### 7.7.1.4 Storage

AWS CloudWatch Logs

#### 7.7.1.5 Configuration

*No data available*

### 7.7.2.0 Component Configurations

*No items available*

### 7.7.3.0 Metrics

#### 7.7.3.1 Custom Metrics

*No data available*

### 7.7.4.0 Alert Rules

#### 7.7.4.1 High P95 API Latency

##### 7.7.4.1.1 Name

High P95 API Latency

##### 7.7.4.1.2 Condition

A custom metric derived from request log `DurationMs` exceeds 200 for a sustained period of 5 minutes.

##### 7.7.4.1.3 Severity

Critical

##### 7.7.4.1.4 Actions

- {'type': 'CloudWatch Alarm', 'target': 'OnCall-SNS-Topic', 'configuration': {}}

##### 7.7.4.1.5 Suppression Rules

*No items available*

##### 7.7.4.1.6 Escalation Path

*No items available*

#### 7.7.4.2.0 External API Circuit Breaker Open

##### 7.7.4.2.1 Name

External API Circuit Breaker Open

##### 7.7.4.2.2 Condition

Log search for message template containing 'Circuit breaker is now open' with level 'Warning'.

##### 7.7.4.2.3 Severity

High

##### 7.7.4.2.4 Actions

- {'type': 'CloudWatch Alarm', 'target': 'DevTeam-SNS-Topic', 'configuration': {}}

##### 7.7.4.2.5 Suppression Rules

*No items available*

##### 7.7.4.2.6 Escalation Path

*No items available*

#### 7.7.4.3.0 Data Export Job Failed Permanently

##### 7.7.4.3.1 Name

Data Export Job Failed Permanently

##### 7.7.4.3.2 Condition

Log search for message template containing 'Job * failed after retries' from Hangfire with level 'Error'.

##### 7.7.4.3.3 Severity

High

##### 7.7.4.3.4 Actions

- {'type': 'CloudWatch Alarm', 'target': 'DevTeam-SNS-Topic', 'configuration': {}}

##### 7.7.4.3.5 Suppression Rules

*No items available*

##### 7.7.4.3.6 Escalation Path

*No items available*

## 7.8.0.0.0 Implementation Priority

*No items available*

## 7.9.0.0.0 Risk Assessment

*No items available*

## 7.10.0.0.0 Recommendations

*No items available*

