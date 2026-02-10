# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- ASP.NET Core 8
- PostgreSQL 16
- Flutter 3.22+
- Hangfire
- Redis

## 1.3 Metrics Configuration

- Backend APM via OpenTelemetry (P95 Latency, Error Rates)
- Client-side Monitoring via Sentry (Crash Rates, Screen Load Times)
- Infrastructure Monitoring via AWS CloudWatch (CPU, Memory, DB Connections)
- Background Job Monitoring via Hangfire Dashboard

## 1.4 Monitoring Needs

- Ensure P95 API latency remains below 200ms (REQ-PERF-001).
- Detect failures in critical external API integrations (REQ-REL-002).
- Monitor success/failure of critical background jobs (REQ-FUNC-002, REQ-FUNC-009).
- Track client-side application stability and performance (REQ-PERF-002).

## 1.5 Environment

production

# 2.0 Alert Condition And Threshold Design

## 2.1 Critical Metrics Alerts

### 2.1.1 Metric

#### 2.1.1.1 Metric

api.latency.p95

#### 2.1.1.2 Condition

is greater than

#### 2.1.1.3 Threshold Type

static

#### 2.1.1.4 Value

200ms for a sustained period of 5 minutes

#### 2.1.1.5 Justification

Directly supports the non-functional requirement REQ-PERF-001. A sustained breach indicates a systemic performance issue affecting user experience.

#### 2.1.1.6 Business Impact

Degraded user experience, potential for user churn, SLA violation.

### 2.1.2.0 Metric

#### 2.1.2.1 Metric

external_api.circuit_breaker.state

#### 2.1.2.2 Condition

equals 'Open'

#### 2.1.2.3 Threshold Type

static

#### 2.1.2.4 Value

1 (Open State)

#### 2.1.2.5 Justification

Directly supports REQ-REL-002. An open circuit breaker means a critical dependency (e.g., OpenAI recommendations, Google Books) is unavailable, leading to feature degradation.

#### 2.1.2.6 Business Impact

Core features (e.g., AI suggestions, adding books) are non-functional.

### 2.1.3.0 Metric

#### 2.1.3.1 Metric

hangfire.jobs.failed.count

#### 2.1.3.2 Condition

is greater than or equal to

#### 2.1.3.3 Threshold Type

static

#### 2.1.3.4 Value

1

#### 2.1.3.5 Justification

Monitors critical background tasks like subscription termination (REQ-FUNC-002) and data exports (REQ-FUNC-009). A single failure requires investigation.

#### 2.1.3.6 Business Impact

Failure to comply with user requests (data export) or incorrect user account state (subscription downgrade).

### 2.1.4.0 Metric

#### 2.1.4.1 Metric

sentry.crash_free_sessions.rate

#### 2.1.4.2 Condition

is less than

#### 2.1.4.3 Threshold Type

static

#### 2.1.4.4 Value

99.5%

#### 2.1.4.5 Justification

Monitors the overall stability of the Flutter mobile application. A drop in this rate indicates a widespread issue impacting many users.

#### 2.1.4.6 Business Impact

Poor user experience, negative app store reviews, user attrition.

## 2.2.0.0 Threshold Strategies

*No items available*

## 2.3.0.0 Baseline Deviation Alerts

*No items available*

## 2.4.0.0 Predictive Alerts

*No items available*

## 2.5.0.0 Compound Conditions

*No items available*

# 3.0.0.0 Severity Level Classification

## 3.1.0.0 Severity Definitions

### 3.1.1.0 Level

#### 3.1.1.1 Level

🚨 Critical

#### 3.1.1.2 Criteria

A complete service outage or a severe degradation impacting all users. Immediate action is required to restore service. Represents a direct violation of a critical SLO/SLA.

#### 3.1.1.3 Business Impact

High (e.g., revenue loss, major user dissatisfaction)

#### 3.1.1.4 Customer Impact

Severe

#### 3.1.1.5 Response Time

< 15 minutes (acknowledge)

#### 3.1.1.6 Escalation Required

✅ Yes

### 3.1.2.0 Level

#### 3.1.2.1 Level

🔴 High

#### 3.1.2.2 Criteria

A significant degradation of a core feature or a partial service outage. Requires urgent attention but the system is still partially functional.

#### 3.1.2.3 Business Impact

Medium (e.g., core feature unavailable, reputational risk)

#### 3.1.2.4 Customer Impact

Significant

#### 3.1.2.5 Response Time

< 30 minutes (acknowledge)

#### 3.1.2.6 Escalation Required

✅ Yes

### 3.1.3.0 Level

#### 3.1.3.1 Level

🟡 Medium

#### 3.1.3.2 Criteria

A non-critical service is degraded, or a potential problem has been detected. Does not require immediate action but should be investigated within business hours.

#### 3.1.3.3 Business Impact

Low (e.g., non-critical feature impacted, delayed processing)

#### 3.1.3.4 Customer Impact

Moderate

#### 3.1.3.5 Response Time

< 4 hours (acknowledge)

#### 3.1.3.6 Escalation Required

❌ No

## 3.2.0.0 Business Impact Matrix

*No items available*

## 3.3.0.0 Customer Impact Criteria

*No items available*

## 3.4.0.0 Sla Violation Severity

*No items available*

## 3.5.0.0 System Health Severity

*No items available*

# 4.0.0.0 Notification Channel Strategy

## 4.1.0.0 Channel Configuration

### 4.1.1.0 Channel

#### 4.1.1.1 Channel

pagerduty

#### 4.1.1.2 Purpose

Primary notification for on-call engineers for urgent, actionable alerts.

#### 4.1.1.3 Applicable Severities

- Critical

#### 4.1.1.4 Time Constraints

24/7

#### 4.1.1.5 Configuration

*No data available*

### 4.1.2.0 Channel

#### 4.1.2.1 Channel

slack

#### 4.1.2.2 Purpose

Real-time, team-wide communication for all alert severities and incident coordination.

#### 4.1.2.3 Applicable Severities

- Critical
- High
- Medium

#### 4.1.2.4 Time Constraints

24/7

#### 4.1.2.5 Configuration

*No data available*

### 4.1.3.0 Channel

#### 4.1.3.1 Channel

email

#### 4.1.3.2 Purpose

Secondary notification and for generating records for non-urgent issues.

#### 4.1.3.3 Applicable Severities

- High
- Medium

#### 4.1.3.4 Time Constraints

24/7

#### 4.1.3.5 Configuration

*No data available*

## 4.2.0.0 Routing Rules

### 4.2.1.0 Condition

#### 4.2.1.1 Condition

Severity is Critical

#### 4.2.1.2 Severity

Critical

#### 4.2.1.3 Alert Type

API Latency Violation

#### 4.2.1.4 Channels

- pagerduty
- slack

#### 4.2.1.5 Priority

🔹 1

### 4.2.2.0 Condition

#### 4.2.2.1 Condition

Severity is High

#### 4.2.2.2 Severity

High

#### 4.2.2.3 Alert Type

Circuit Breaker Open OR Crash Rate Increase OR Background Job Failure

#### 4.2.2.4 Channels

- slack
- email

#### 4.2.2.5 Priority

🔹 2

## 4.3.0.0 Time Based Routing

*No items available*

## 4.4.0.0 Ticketing Integration

- {'system': 'jira', 'triggerConditions': ['Severity is Critical', 'Severity is High'], 'ticketPriority': 'Highest for Critical, High for High', 'autoAssignment': True}

## 4.5.0.0 Emergency Notifications

*No items available*

## 4.6.0.0 Chat Platform Integration

*No items available*

# 5.0.0.0 Alert Correlation Implementation

## 5.1.0.0 Grouping Requirements

- {'groupingCriteria': 'component (e.g., Backend-API, OpenAI-Client)', 'timeWindow': '5m', 'maxGroupSize': 10, 'suppressionStrategy': 'Group related alerts into a single incident to reduce notification noise.'}

## 5.2.0.0 Parent Child Relationships

*No items available*

## 5.3.0.0 Topology Based Correlation

*No items available*

## 5.4.0.0 Time Window Correlation

*No items available*

## 5.5.0.0 Causal Relationship Detection

*No items available*

## 5.6.0.0 Maintenance Window Suppression

- {'maintenanceType': 'Scheduled Deployment', 'suppressionScope': ['All'], 'automaticDetection': False, 'manualOverride': True}

# 6.0.0.0 False Positive Mitigation

## 6.1.0.0 Noise Reduction Strategies

- {'strategy': 'Sustained Threshold Breach', 'implementation': 'Alert condition must be met for a continuous period (e.g., 5 minutes) before an alert is fired.', 'applicableAlerts': ['api.latency.p95'], 'effectiveness': 'High'}

## 6.2.0.0 Confirmation Counts

*No items available*

## 6.3.0.0 Dampening And Flapping

*No items available*

## 6.4.0.0 Alert Validation

*No items available*

## 6.5.0.0 Smart Filtering

*No items available*

## 6.6.0.0 Quorum Based Alerting

*No items available*

# 7.0.0.0 On Call Management Integration

## 7.1.0.0 Escalation Paths

- {'severity': 'Critical', 'escalationLevels': [{'level': 1, 'recipients': ['Primary On-Call Engineer'], 'escalationTime': '15m', 'requiresAcknowledgment': True}, {'level': 2, 'recipients': ['Secondary On-Call Engineer'], 'escalationTime': '15m', 'requiresAcknowledgment': True}, {'level': 3, 'recipients': ['Engineering Lead'], 'escalationTime': '30m', 'requiresAcknowledgment': False}], 'ultimateEscalation': 'Head of Engineering'}

## 7.2.0.0 Escalation Timeframes

*No items available*

## 7.3.0.0 On Call Rotation

*No items available*

## 7.4.0.0 Acknowledgment Requirements

- {'severity': 'Critical', 'acknowledgmentTimeout': '15m', 'autoEscalation': True, 'requiresComment': False}

## 7.5.0.0 Incident Ownership

*No items available*

## 7.6.0.0 Follow The Sun Support

*No items available*

# 8.0.0.0 Project Specific Alerts Config

## 8.1.0.0 Alerts

### 8.1.1.0 API P95 Latency Violation

#### 8.1.1.1 Name

API P95 Latency Violation

#### 8.1.1.2 Description

The 95th percentile latency for core backend API endpoints has exceeded 200ms for a sustained period of 5 minutes.

#### 8.1.1.3 Condition

avg(api.latency.p95) > 200

#### 8.1.1.4 Threshold

For 5 minutes

#### 8.1.1.5 Severity

Critical

#### 8.1.1.6 Channels

- pagerduty
- slack

#### 8.1.1.7 Correlation

##### 8.1.1.7.1 Group Id

backend-performance

##### 8.1.1.7.2 Suppression Rules

*No items available*

#### 8.1.1.8.0 Escalation

##### 8.1.1.8.1 Enabled

✅ Yes

##### 8.1.1.8.2 Escalation Time

15m

##### 8.1.1.8.3 Escalation Path

- Primary On-Call
- Secondary On-Call

#### 8.1.1.9.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.1.10.0 Validation

##### 8.1.1.10.1 Confirmation Count

1

##### 8.1.1.10.2 Confirmation Window

5m

#### 8.1.1.11.0 Remediation

##### 8.1.1.11.1 Automated Actions

*No items available*

##### 8.1.1.11.2 Runbook Url

🔗 [http://runbooks.example.com/api-latency](http://runbooks.example.com/api-latency)

##### 8.1.1.11.3 Troubleshooting Steps

- Check database CPU and active connections.
- Review recent deployments for performance regressions.
- Analyze APM traces for slow transactions or queries.

### 8.1.2.0.0 External API Circuit Breaker Open

#### 8.1.2.1.0 Name

External API Circuit Breaker Open

#### 8.1.2.2.0 Description

The circuit breaker for a critical external API (e.g., OpenAI, Google Books) has opened, indicating the service is unavailable.

#### 8.1.2.3.0 Condition

sum(external_api.circuit_breaker.state) >= 1

#### 8.1.2.4.0 Threshold

For 1 minute

#### 8.1.2.5.0 Severity

High

#### 8.1.2.6.0 Channels

- slack
- email

#### 8.1.2.7.0 Correlation

##### 8.1.2.7.1 Group Id

external-dependencies

##### 8.1.2.7.2 Suppression Rules

*No items available*

#### 8.1.2.8.0 Escalation

##### 8.1.2.8.1 Enabled

❌ No

##### 8.1.2.8.2 Escalation Time



##### 8.1.2.8.3 Escalation Path

*No items available*

#### 8.1.2.9.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.2.10.0 Validation

##### 8.1.2.10.1 Confirmation Count

0

##### 8.1.2.10.2 Confirmation Window



#### 8.1.2.11.0 Remediation

##### 8.1.2.11.1 Automated Actions

*No items available*

##### 8.1.2.11.2 Runbook Url

🔗 [http://runbooks.example.com/circuit-breaker](http://runbooks.example.com/circuit-breaker)

##### 8.1.2.11.3 Troubleshooting Steps

- Verify the status page of the external provider.
- Check for API key or authentication issues.
- Confirm network connectivity from the production environment.

### 8.1.3.0.0 Critical Background Job Failed

#### 8.1.3.1.0 Name

Critical Background Job Failed

#### 8.1.3.2.0 Description

A background job for a critical process (e.g., data export, subscription termination) has failed after all retries.

#### 8.1.3.3.0 Condition

count(hangfire.jobs.failed) >= 1

#### 8.1.3.4.0 Threshold

Over 1 minute

#### 8.1.3.5.0 Severity

High

#### 8.1.3.6.0 Channels

- slack
- email

#### 8.1.3.7.0 Correlation

##### 8.1.3.7.1 Group Id

backend-processing

##### 8.1.3.7.2 Suppression Rules

*No items available*

#### 8.1.3.8.0 Escalation

##### 8.1.3.8.1 Enabled

❌ No

##### 8.1.3.8.2 Escalation Time



##### 8.1.3.8.3 Escalation Path

*No items available*

#### 8.1.3.9.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.3.10.0 Validation

##### 8.1.3.10.1 Confirmation Count

0

##### 8.1.3.10.2 Confirmation Window



#### 8.1.3.11.0 Remediation

##### 8.1.3.11.1 Automated Actions

*No items available*

##### 8.1.3.11.2 Runbook Url

🔗 [http://runbooks.example.com/hangfire-jobs](http://runbooks.example.com/hangfire-jobs)

##### 8.1.3.11.3 Troubleshooting Steps

- Inspect the exception details in the Hangfire Dashboard.
- Review logs for the specific job execution.
- Manually re-queue the job after investigating the root cause.

### 8.1.4.0.0 Client App Crash Rate High

#### 8.1.4.1.0 Name

Client App Crash Rate High

#### 8.1.4.2.0 Description

The crash-free session rate for the mobile application has dropped below the 99.5% threshold.

#### 8.1.4.3.0 Condition

avg(sentry.crash_free_sessions.rate) < 99.5

#### 8.1.4.4.0 Threshold

Over 1 hour

#### 8.1.4.5.0 Severity

High

#### 8.1.4.6.0 Channels

- slack
- email

#### 8.1.4.7.0 Correlation

##### 8.1.4.7.1 Group Id

client-stability

##### 8.1.4.7.2 Suppression Rules

*No items available*

#### 8.1.4.8.0 Escalation

##### 8.1.4.8.1 Enabled

❌ No

##### 8.1.4.8.2 Escalation Time



##### 8.1.4.8.3 Escalation Path

*No items available*

#### 8.1.4.9.0 Suppression

| Property | Value |
|----------|-------|
| Maintenance Window | ✅ |
| Dependency Failure | ❌ |
| Manual Override | ✅ |

#### 8.1.4.10.0 Validation

##### 8.1.4.10.1 Confirmation Count

0

##### 8.1.4.10.2 Confirmation Window



#### 8.1.4.11.0 Remediation

##### 8.1.4.11.1 Automated Actions

*No items available*

##### 8.1.4.11.2 Runbook Url

🔗 [http://runbooks.example.com/client-crashes](http://runbooks.example.com/client-crashes)

##### 8.1.4.11.3 Troubleshooting Steps

- Analyze the new/regressed crash reports in Sentry.
- Correlate crashes with recent app releases or OS versions.
- Prioritize a hotfix release if the issue is severe.

## 8.2.0.0.0 Alert Groups

*No items available*

## 8.3.0.0.0 Notification Templates

*No items available*

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

API Latency Violation Alert

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

- Backend APM Setup

### 9.1.4.0.0 Estimated Effort

Low

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

External API Circuit Breaker Alert

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- Backend APM Setup

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

low

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Client App Crash Rate Alert

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- Sentry SDK Integration

### 9.3.4.0.0 Estimated Effort

Low

### 9.3.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Alert Fatigue

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Only implement essential, actionable alerts. Use sustained threshold conditions to avoid flapping. Regularly review and tune alert thresholds.

### 10.1.5.0.0 Contingency Plan

Establish a process for silencing noisy alerts and performing a root cause analysis on why they were not actionable.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Missed Critical Incidents

### 10.2.2.0.0 Impact

high

### 10.2.3.0.0 Probability

low

### 10.2.4.0.0 Mitigation

Ensure all critical user journeys and system components have corresponding alert coverage. Regularly conduct fire drills and review alert effectiveness after any incident.

### 10.2.5.0.0 Contingency Plan

Perform a post-mortem to identify gaps in monitoring and alerting, and prioritize implementing the missing coverage.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Operational Readiness

### 11.1.2.0.0 Recommendation

Develop a runbook for each configured alert before it is enabled in production.

### 11.1.3.0.0 Justification

Ensures that on-call engineers have clear, documented steps to follow when an alert fires, reducing Mean Time To Resolution (MTTR).

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Each runbook should contain: alert meaning, potential causes, investigation steps, and escalation contacts.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Continuous Improvement

### 11.2.2.0.0 Recommendation

Schedule quarterly reviews of all active alerts to assess their effectiveness, signal-to-noise ratio, and threshold accuracy.

### 11.2.3.0.0 Justification

Systems evolve, and alerts can become stale or noisy. Regular reviews prevent alert fatigue and ensure the alerting system remains effective.

### 11.2.4.0.0 Priority

🟡 medium

### 11.2.5.0.0 Implementation Notes

Use incident data and on-call feedback as primary inputs for the review process.

