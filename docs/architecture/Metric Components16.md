# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- Flutter
- Dart
- Isar
- .NET 8
- ASP.NET Core 8
- PostgreSQL
- Redis
- Hangfire

## 1.3 Monitoring Components

- OpenTelemetry SDK for .NET
- Sentry SDK for Flutter
- Serilog with AWS CloudWatch Sink
- AWS CloudWatch
- AspNetCore.HealthChecks

## 1.4 Requirements

- REQ-PERF-001
- REQ-PERF-002
- REQ-REL-002
- REQ-FUNC-002
- REQ-FUNC-007
- REQ-FUNC-009
- REQ-FUNC-010
- REQ-FUNC-011

## 1.5 Environment

production

# 2.0 Standard System Metrics Selection

## 2.1 Hardware Utilization Metrics

### 2.1.1 gauge

#### 2.1.1.1 Name

system.cpu.utilization

#### 2.1.1.2 Type

🔹 gauge

#### 2.1.1.3 Unit

percent

#### 2.1.1.4 Description

Measures the CPU utilization of the backend host.

#### 2.1.1.5 Collection

##### 2.1.1.5.1 Interval

60s

##### 2.1.1.5.2 Method

Agent-based (CloudWatch)

#### 2.1.1.6.0 Thresholds

##### 2.1.1.6.1 Warning

> 70%

##### 2.1.1.6.2 Critical

> 85%

#### 2.1.1.7.0 Justification

Provides fundamental insight into server health and potential performance bottlenecks affecting API latency (REQ-PERF-001).

### 2.1.2.0.0 gauge

#### 2.1.2.1.0 Name

system.memory.utilization

#### 2.1.2.2.0 Type

🔹 gauge

#### 2.1.2.3.0 Unit

percent

#### 2.1.2.4.0 Description

Measures the memory utilization of the backend host.

#### 2.1.2.5.0 Collection

##### 2.1.2.5.1 Interval

60s

##### 2.1.2.5.2 Method

Agent-based (CloudWatch)

#### 2.1.2.6.0 Thresholds

##### 2.1.2.6.1 Warning

> 80%

##### 2.1.2.6.2 Critical

> 90%

#### 2.1.2.7.0 Justification

Essential for detecting memory pressure or leaks that could lead to degraded performance or service unavailability.

## 2.2.0.0.0 Runtime Metrics

### 2.2.1.0.0 gauge

#### 2.2.1.1.0 Name

dotnet.gc.heap.size

#### 2.2.1.2.0 Type

🔹 gauge

#### 2.2.1.3.0 Unit

bytes

#### 2.2.1.4.0 Description

Tracks the total allocated memory in the .NET garbage collector heap.

#### 2.2.1.5.0 Technology

.NET

#### 2.2.1.6.0 Collection

##### 2.2.1.6.1 Interval

30s

##### 2.2.1.6.2 Method

OpenTelemetry .NET Runtime Instrumentation

#### 2.2.1.7.0 Criticality

medium

### 2.2.2.0.0 counter

#### 2.2.2.1.0 Name

dotnet.thread_pool.completed_items.count

#### 2.2.2.2.0 Type

🔹 counter

#### 2.2.2.3.0 Unit

requests

#### 2.2.2.4.0 Description

Monitors the number of completed work items in the .NET thread pool, indicating overall throughput.

#### 2.2.2.5.0 Technology

.NET

#### 2.2.2.6.0 Collection

##### 2.2.2.6.1 Interval

30s

##### 2.2.2.6.2 Method

OpenTelemetry .NET Runtime Instrumentation

#### 2.2.2.7.0 Criticality

low

## 2.3.0.0.0 Request Response Metrics

- {'name': 'http.server.request.duration', 'type': 'histogram', 'unit': 'ms', 'description': 'Measures the duration of incoming HTTP requests to the backend API. This is the primary metric for REQ-PERF-001.', 'dimensions': ['http.request.method', 'url.path', 'http.response.status_code'], 'percentiles': ['p50', 'p90', 'p95', 'p99'], 'collection': {'interval': 'real-time', 'method': 'OpenTelemetry ASP.NET Core Instrumentation'}}

## 2.4.0.0.0 Availability Metrics

- {'name': 'api.availability.ratio', 'type': 'gauge', 'unit': 'percent', 'description': 'Measures the overall availability of the backend API.', 'calculation': '(Count of requests with status < 500) / (Total requests)', 'slaTarget': '99.9%'}

## 2.5.0.0.0 Scalability Metrics

*No items available*

# 3.0.0.0.0 Application Specific Metrics Design

## 3.1.0.0.0 Transaction Metrics

- {'name': 'background_job.execution.duration', 'type': 'histogram', 'unit': 'ms', 'description': 'Measures the duration of Hangfire background jobs.', 'business_context': 'Monitors performance of asynchronous tasks like Data Export (REQ-FUNC-009) and Subscription Status checks (REQ-FUNC-002).', 'dimensions': ['job_name'], 'collection': {'interval': 'on_completion', 'method': 'OpenTelemetry Hangfire Instrumentation'}, 'aggregation': {'functions': ['avg', 'p95'], 'window': '5m'}}

## 3.2.0.0.0 Cache Performance Metrics

- {'name': 'cache.hit_ratio', 'type': 'gauge', 'unit': 'ratio', 'description': 'Measures the Redis cache hit ratio.', 'cacheType': 'Redis', 'hitRatioTarget': '> 0.90'}

## 3.3.0.0.0 External Dependency Metrics

### 3.3.1.0.0 histogram

#### 3.3.1.1.0 Name

http.client.request.duration

#### 3.3.1.2.0 Type

🔹 histogram

#### 3.3.1.3.0 Unit

ms

#### 3.3.1.4.0 Description

Measures latency of outgoing HTTP calls to external services like OpenAI.

#### 3.3.1.5.0 Dependency

OpenAI API

#### 3.3.1.6.0 Circuit Breaker Integration

✅ Yes

#### 3.3.1.7.0 Sla

##### 3.3.1.7.1 Response Time

N/A

##### 3.3.1.7.2 Availability

N/A

### 3.3.2.0.0 gauge

#### 3.3.2.1.0 Name

polly.circuit.state

#### 3.3.2.2.0 Type

🔹 gauge

#### 3.3.2.3.0 Unit

state

#### 3.3.2.4.0 Description

Tracks the state of the Polly circuit breaker for external APIs (0=Closed, 1=HalfOpen, 2=Open). Directly supports REQ-REL-002.

#### 3.3.2.5.0 Dependency

OpenAI API

#### 3.3.2.6.0 Circuit Breaker Integration

✅ Yes

#### 3.3.2.7.0 Sla

##### 3.3.2.7.1 Response Time



##### 3.3.2.7.2 Availability



## 3.4.0.0.0 Error Metrics

### 3.4.1.0.0 counter

#### 3.4.1.1.0 Name

application.errors.total

#### 3.4.1.2.0 Type

🔹 counter

#### 3.4.1.3.0 Unit

errors

#### 3.4.1.4.0 Description

Counts total unhandled exceptions in the backend and failed background jobs.

#### 3.4.1.5.0 Error Types

- unhandled_exception
- job_failure

#### 3.4.1.6.0 Dimensions

- error.type
- job_name

#### 3.4.1.7.0 Alert Threshold

> 5 in 5m

### 3.4.2.0.0 counter

#### 3.4.2.1.0 Name

client.errors.total

#### 3.4.2.2.0 Type

🔹 counter

#### 3.4.2.3.0 Unit

errors

#### 3.4.2.4.0 Description

Counts total unhandled exceptions and crashes in the Flutter client.

#### 3.4.2.5.0 Error Types

- unhandled_dart_exception
- native_crash

#### 3.4.2.6.0 Dimensions

- os.name
- app.version

#### 3.4.2.7.0 Alert Threshold

> 20 in 1h

## 3.5.0.0.0 Throughput And Latency Metrics

### 3.5.1.0.0 summary

#### 3.5.1.1.0 Name

api.core_endpoints.latency.p95

#### 3.5.1.2.0 Type

🔹 summary

#### 3.5.1.3.0 Unit

ms

#### 3.5.1.4.0 Description

95th percentile latency for core API endpoints, as required by REQ-PERF-001.

#### 3.5.1.5.0 Percentiles

- p95

#### 3.5.1.6.0 Buckets

*No items available*

#### 3.5.1.7.0 Sla Targets

##### 3.5.1.7.1 P95

< 200ms

##### 3.5.1.7.2 P99



### 3.5.2.0.0 histogram

#### 3.5.2.1.0 Name

client.dashboard.load_time

#### 3.5.2.2.0 Type

🔹 histogram

#### 3.5.2.3.0 Unit

ms

#### 3.5.2.4.0 Description

Time-to-interactive for the main dashboard screen on the Flutter client, as required by REQ-PERF-002.

#### 3.5.2.5.0 Percentiles

- p95

#### 3.5.2.6.0 Buckets

- 500
- 1000
- 1500
- 2000

#### 3.5.2.7.0 Sla Targets

##### 3.5.2.7.1 P95

< 1500ms

##### 3.5.2.7.2 P99



# 4.0.0.0.0 Business Kpi Identification

## 4.1.0.0.0 Critical Business Metrics

### 4.1.1.0.0 gauge

#### 4.1.1.1.0 Name

subscription.status.count

#### 4.1.1.2.0 Type

🔹 gauge

#### 4.1.1.3.0 Unit

users

#### 4.1.1.4.0 Description

Tracks the number of users in each subscription tier.

#### 4.1.1.5.0 Business Owner

Product Team

#### 4.1.1.6.0 Calculation

```sql
SELECT subscriptionTier, COUNT(userId) FROM User GROUP BY subscriptionTier
```

#### 4.1.1.7.0 Reporting Frequency

1h

#### 4.1.1.8.0 Target



### 4.1.2.0.0 counter

#### 4.1.2.1.0 Name

data_export.jobs.status.count

#### 4.1.2.2.0 Type

🔹 counter

#### 4.1.2.3.0 Unit

jobs

#### 4.1.2.4.0 Description

Counts the number of completed and failed data export jobs.

#### 4.1.2.5.0 Business Owner

Compliance Team

#### 4.1.2.6.0 Calculation

Increment counter on DataExportJob completion/failure, dimensioned by status.

#### 4.1.2.7.0 Reporting Frequency

real-time

#### 4.1.2.8.0 Target

Failure rate < 1%

## 4.2.0.0.0 User Engagement Metrics

- {'name': 'reading_sessions.logged.total', 'type': 'counter', 'unit': 'sessions', 'description': 'Total number of reading sessions logged by users, a core engagement metric.', 'segmentation': ['subscriptionTier'], 'cohortAnalysis': False}

## 4.3.0.0.0 Conversion Metrics

*No items available*

## 4.4.0.0.0 Operational Efficiency Kpis

*No items available*

## 4.5.0.0.0 Revenue And Cost Metrics

### 4.5.1.0.0 counter

#### 4.5.1.1.0 Name

ad.impressions.total

#### 4.5.1.2.0 Type

🔹 counter

#### 4.5.1.3.0 Unit

impressions

#### 4.5.1.4.0 Description

Total number of ads displayed to 'Free User' tier users (REQ-FUNC-010).

#### 4.5.1.5.0 Frequency

daily

#### 4.5.1.6.0 Accuracy



### 4.5.2.0.0 counter

#### 4.5.2.1.0 Name

openai.api.calls.total

#### 4.5.2.2.0 Type

🔹 counter

#### 4.5.2.3.0 Unit

calls

#### 4.5.2.4.0 Description

Total calls made to the OpenAI API for cost monitoring (Constraint from REQ-FUNC-007).

#### 4.5.2.5.0 Frequency

real-time

#### 4.5.2.6.0 Accuracy



## 4.6.0.0.0 Customer Satisfaction Indicators

- {'name': 'recommendation.feedback.ratio', 'type': 'gauge', 'unit': 'ratio', 'description': "Ratio of 'Like' vs 'Dislike' feedback on AI-generated recommendations (REQ-FUNC-007).", 'dataSource': 'Recommendation Table', 'updateFrequency': 'daily'}

# 5.0.0.0.0 Collection Interval Optimization

## 5.1.0.0.0 Sampling Frequencies

### 5.1.1.0.0 Metric Category

#### 5.1.1.1.0 Metric Category

API Performance

#### 5.1.1.2.0 Interval

per-request

#### 5.1.1.3.0 Justification

Required for accurate P95 calculation to meet REQ-PERF-001.

#### 5.1.1.4.0 Resource Impact

medium

### 5.1.2.0.0 Metric Category

#### 5.1.2.1.0 Metric Category

Hardware Utilization

#### 5.1.2.2.0 Interval

60s

#### 5.1.2.3.0 Justification

Sufficient for detecting sustained resource pressure without excessive data volume.

#### 5.1.2.4.0 Resource Impact

low

### 5.1.3.0.0 Metric Category

#### 5.1.3.1.0 Metric Category

Business KPIs

#### 5.1.3.2.0 Interval

1h-24h

#### 5.1.3.3.0 Justification

Business trends do not require real-time data; batch aggregation is more efficient.

#### 5.1.3.4.0 Resource Impact

low

## 5.2.0.0.0 High Frequency Metrics

- {'name': 'http.server.request.duration', 'interval': 'per-request', 'criticality': 'high', 'costJustification': 'Directly required to validate SLA in REQ-PERF-001.'}

## 5.3.0.0.0 Cardinality Considerations

- {'metricName': 'http.server.request.duration', 'estimatedCardinality': 'medium', 'dimensionStrategy': 'Avoid high-cardinality dimensions like user IDs. Use parameterized URL paths instead of raw URLs.', 'mitigationApproach': 'Route templating in ASP.NET Core instrumentation.'}

## 5.4.0.0.0 Aggregation Periods

- {'metricType': 'Performance', 'periods': ['1m', '5m', '1h'], 'retentionStrategy': 'Raw data for 24h, 1m aggregates for 7d, 1h aggregates for 90d.'}

## 5.5.0.0.0 Collection Methods

- {'method': 'real-time', 'applicableMetrics': ['http.server.request.duration', 'openai.api.calls.total'], 'implementation': 'OpenTelemetry SDK', 'performance': 'High'}

# 6.0.0.0.0 Aggregation Method Selection

## 6.1.0.0.0 Statistical Aggregations

- {'metricName': 'background_job.execution.duration', 'aggregationFunctions': ['avg', 'max', 'p95'], 'windows': ['1m', '5m'], 'justification': 'Average and max show typical and worst-case performance, while P95 filters outliers.'}

## 6.2.0.0.0 Histogram Requirements

- {'metricName': 'http.server.request.duration', 'buckets': ['50', '100', '200', '500', '1000'], 'percentiles': ['p95'], 'accuracy': 'High accuracy required for SLA validation (REQ-PERF-001).'}

## 6.3.0.0.0 Percentile Calculations

- {'metricName': 'api.core_endpoints.latency.p95', 'percentiles': ['p95'], 'algorithm': 'hdr', 'accuracy': 'high'}

## 6.4.0.0.0 Metric Types

- {'name': 'application.errors.total', 'implementation': 'counter', 'reasoning': 'Errors are discrete events that only increase over time. A counter is the appropriate monotonically increasing type.', 'resetsHandling': 'Handled by monitoring system (rate calculation).'}

## 6.5.0.0.0 Dimensional Aggregation

- {'metricName': 'http.server.request.duration', 'dimensions': ['url.path', 'http.response.status_code'], 'aggregationStrategy': 'Aggregations can be performed across any combination of dimensions.', 'cardinalityImpact': 'Medium; controlled by using parameterized paths.'}

## 6.6.0.0.0 Derived Metrics

- {'name': 'cache.hit_ratio', 'calculation': 'cache.hits / (cache.hits + cache.misses)', 'sourceMetrics': ['cache.hits', 'cache.misses'], 'updateFrequency': '1m'}

# 7.0.0.0.0 Storage Requirements Planning

## 7.1.0.0.0 Retention Periods

### 7.1.1.0.0 Metric Type

#### 7.1.1.1.0 Metric Type

High-Resolution Performance Metrics

#### 7.1.1.2.0 Retention Period

14 days

#### 7.1.1.3.0 Justification

Allows for detailed performance analysis and debugging of recent incidents.

#### 7.1.1.4.0 Compliance Requirement

None

### 7.1.2.0.0 Metric Type

#### 7.1.2.1.0 Metric Type

Aggregated Business Metrics

#### 7.1.2.2.0 Retention Period

1 year

#### 7.1.2.3.0 Justification

Needed for year-over-year trend analysis.

#### 7.1.2.4.0 Compliance Requirement

None

## 7.2.0.0.0 Data Resolution

### 7.2.1.0.0 Time Range

#### 7.2.1.1.0 Time Range

0-24 hours

#### 7.2.1.2.0 Resolution

10s

#### 7.2.1.3.0 Query Performance

High

#### 7.2.1.4.0 Storage Optimization

Raw data

### 7.2.2.0.0 Time Range

#### 7.2.2.1.0 Time Range

24 hours - 30 days

#### 7.2.2.2.0 Resolution

1m

#### 7.2.2.3.0 Query Performance

Medium

#### 7.2.2.4.0 Storage Optimization

Downsampled

## 7.3.0.0.0 Downsampling Strategies

- {'sourceResolution': '10s', 'targetResolution': '1m', 'aggregationMethod': 'avg for gauges, sum for counters, merged histograms', 'triggerCondition': 'After 24 hours'}

## 7.4.0.0.0 Storage Performance

| Property | Value |
|----------|-------|
| Write Latency | < 100ms |
| Query Latency | < 2s for typical dashboard queries |
| Throughput Requirements | Handle peak API request volume |
| Scalability Needs | Scale with user growth |

## 7.5.0.0.0 Query Optimization

*No items available*

## 7.6.0.0.0 Cost Optimization

- {'strategy': 'Aggressive Downsampling and Retention', 'implementation': 'Configure downsampling rules and retention policies in the time-series database (e.g., CloudWatch).', 'expectedSavings': 'Significant reduction in long-term storage costs.', 'tradeoffs': 'Loss of raw data granularity for older time ranges.'}

# 8.0.0.0.0 Project Specific Metrics Config

## 8.1.0.0.0 Standard Metrics

*No items available*

## 8.2.0.0.0 Custom Metrics

*No items available*

## 8.3.0.0.0 Dashboard Metrics

*No items available*

# 9.0.0.0.0 Implementation Priority

*No items available*

# 10.0.0.0.0 Risk Assessment

*No items available*

# 11.0.0.0.0 Recommendations

*No items available*

