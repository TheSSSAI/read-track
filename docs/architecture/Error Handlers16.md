# 1 Strategies

## 1.1 Retry

### 1.1.1 Type

🔹 Retry

### 1.1.2 Configuration

#### 1.1.2.1 Policy Name

ExternalAPIRetryPolicy

#### 1.1.2.2 Description

Applies to transient errors when calling external services like OpenAI, Google Books, and Contentful. Uses exponential backoff with jitter to avoid overwhelming a recovering service.

#### 1.1.2.3 Retry Attempts

3

#### 1.1.2.4 Backoff Strategy

Exponential

#### 1.1.2.5 Initial Delay

1s

#### 1.1.2.6 Jitter

250ms

#### 1.1.2.7 Error Handling Rules

- HttpRequestException
- TaskCanceledException
- HttpStatusCode.ServiceUnavailable (503)
- HttpStatusCode.GatewayTimeout (504)

## 1.2.0.0 Retry

### 1.2.1.0 Type

🔹 Retry

### 1.2.2.0 Configuration

#### 1.2.2.1 Policy Name

BackgroundJobRetryPolicy

#### 1.2.2.2 Description

Applies to Hangfire background jobs (e.g., DataExportJob) to handle transient database or network issues. Uses longer, increasing intervals.

#### 1.2.2.3 Retry Attempts

5

#### 1.2.2.4 Backoff Strategy

Linear

#### 1.2.2.5 Retry Intervals

| Property | Value |
|----------|-------|
| Interval1 | 1m |
| Interval2 | 5m |
| Interval3 | 15m |
| Interval4 | 30m |
| Interval5 | 60m |

#### 1.2.2.6 Error Handling Rules

- DatabaseTransientError
- NetworkConnectivityError

## 1.3.0.0 CircuitBreaker

### 1.3.1.0 Type

🔹 CircuitBreaker

### 1.3.2.0 Configuration

#### 1.3.2.1 Policy Name

ExternalAPICircuitBreaker

#### 1.3.2.2 Description

Protects the system from repeated calls to a failing external service (OpenAI, Google Books, Contentful), as required by REQ-REL-002. Trips after a number of consecutive failures and allows a single test request after a timeout.

#### 1.3.2.3 Failure Threshold

5

#### 1.3.2.4 Break Duration

30s

#### 1.3.2.5 Half Open Actions

1

#### 1.3.2.6 Error Handling Rules

- HttpRequestException
- TaskCanceledException
- HttpStatusCode.InternalServerError (500)
- HttpStatusCode.ServiceUnavailable (503)
- HttpStatusCode.GatewayTimeout (504)

## 1.4.0.0 Fallback

### 1.4.1.0 Type

🔹 Fallback

### 1.4.2.0 Configuration

#### 1.4.2.1 Policy Name

APIFailureFallback

#### 1.4.2.2 Description

Provides graceful degradation paths when a circuit is open or retries are exhausted for critical external APIs, as defined in REQ-REL-002.

#### 1.4.2.3 Triggering Errors

- BrokenCircuitException

#### 1.4.2.4 Fallback Rules

##### 1.4.2.4.1 Service

###### 1.4.2.4.1.1 Service

OpenAIRecommendationService

###### 1.4.2.4.1.2 Response

DisplayFriendlyMessageInUI

##### 1.4.2.4.2.0 Service

###### 1.4.2.4.2.1 Service

GoogleBooksAPI

###### 1.4.2.4.2.2 Response

EnableManualBookEntryInUI

##### 1.4.2.4.3.0 Service

###### 1.4.2.4.3.1 Service

ContentfulCMS

###### 1.4.2.4.3.2 Response

ServeStaleContentFromCache

## 1.5.0.0.0.0 DeadLetter

### 1.5.1.0.0.0 Type

🔹 DeadLetter

### 1.5.2.0.0.0 Configuration

#### 1.5.2.1.0.0 Policy Name

FailedJobProcessor

#### 1.5.2.2.0.0 Description

Handles background jobs (e.g., DataExportJob) that have exhausted all retry attempts. Marks the corresponding database record as 'Failed' for manual review, acting as a logical dead-letter queue.

#### 1.5.2.3.0.0 Dead Letter Action

Update 'DataExportJob' status to 'Failed'

#### 1.5.2.4.0.0 Error Handling Rules

- BackgroundJobPermanentError

# 2.0.0.0.0.0 Monitoring

## 2.1.0.0.0.0 Error Types

- HttpRequestException
- TaskCanceledException
- DatabaseTransientError
- NetworkConnectivityError
- BrokenCircuitException
- BackgroundJobPermanentError
- UnhandledException

## 2.2.0.0.0.0 Alerting

As per REQ-PERF-001, a high-severity alert is triggered via CloudWatch to an on-call channel if P95 API latency exceeds 200ms for a sustained 5-minute period. A medium-severity alert is triggered if a Circuit Breaker opens for any critical external service. A low-severity alert is triggered for any permanently failed background job. All errors are logged to a centralized system (e.g., Serilog sink) with a correlation ID for end-to-end tracing.

