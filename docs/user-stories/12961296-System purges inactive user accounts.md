# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-109 |
| Elaboration Date | 2025-01-27 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | System purges inactive user accounts |
| As A User Story | As a System Administrator responsible for data pri... |
| User Persona | System Administrator / Data Privacy Officer (on be... |
| Business Value | Ensures compliance with data protection regulation... |
| Functional Area | Data Management & Compliance |
| Story Theme | User Data Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

A user account inactive for more than two years is successfully purged

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A user account exists in the database with a 'last_login_timestamp' more than 730 days in the past

### 3.1.5 When

The scheduled daily data purge job is executed

### 3.1.6 Then

The user's record and all associated PII and user-generated content (library, sessions, goals, vocabulary) are permanently deleted from the primary database (Aurora).

### 3.1.7 And

An attempt to authenticate with the deleted user's credentials fails.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

An active user account is not affected by the purge job

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

A user account exists in the database with a 'last_login_timestamp' less than 730 days in the past

### 3.2.5 When

The scheduled daily data purge job is executed

### 3.2.6 Then

The user's account and all associated data remain unchanged in the database.

### 3.2.7 And

No deletion entry is created in the audit log for this user.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

A user account exactly on the two-year inactivity threshold is not purged

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

A user account exists in the database with a 'last_login_timestamp' exactly 730 days in the past

### 3.3.5 When

The scheduled daily data purge job is executed

### 3.3.6 Then

The user's account and all associated data remain unchanged, as the policy is for accounts inactive for *more than* two years.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

The purge job handles failures gracefully

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

The purge job is processing a batch of 10 inactive accounts and has successfully purged 5

### 3.4.5 When

The job encounters a critical, unrecoverable error (e.g., database connection lost)

### 3.4.6 Then

The transaction for the user being processed at the time of failure is rolled back.

### 3.4.7 And

The 5 successfully purged accounts remain purged, and the remaining 4 unprocessed accounts are not affected.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The purge job is idempotent

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

The purge job failed previously and is being re-run

### 3.5.5 When

The scheduled daily data purge job is executed again

### 3.5.6 Then

The job correctly identifies only the remaining inactive users who were not successfully purged in the previous run.

### 3.5.7 And

The job does not produce errors when attempting to process users who have already been deleted.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- N/A - This is a backend, automated process with no user interface.

## 4.2.0 User Interactions

- N/A

## 4.3.0 Display Requirements

- N/A

## 4.4.0 Accessibility Needs

- N/A

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

An account is defined as 'inactive' if the user has not successfully logged in for a continuous period of two years (730 days).

### 5.1.3 Enforcement Point

During the execution of the scheduled data purge job.

### 5.1.4 Violation Handling

N/A - This is a definition, not a rule that can be violated by a user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Data purging must be a permanent, hard delete. Soft deletes are not compliant with the 'right to erasure' principle.

### 5.2.3 Enforcement Point

Within the data deletion logic of the purge job.

### 5.2.4 Violation Handling

N/A - System must be designed to comply.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

Requires the 'Sign in with Google' flow to reliably update a 'last_login_timestamp' for the user.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-005

#### 6.1.2.2 Dependency Reason

Requires the 'Sign in with Apple' flow to reliably update a 'last_login_timestamp' for the user.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-111

#### 6.1.3.2 Dependency Reason

Requires the immutable audit trail system to be in place to log the deletion event.

## 6.2.0.0 Technical Dependencies

- AWS EventBridge Scheduler (or similar cron mechanism) for scheduling the job.
- AWS Lambda or AWS Fargate for executing the job logic.
- A robust database transaction management strategy to handle failures.
- A defined mechanism for searching and anonymizing data within CloudWatch Logs.

## 6.3.0.0 Data Dependencies

- Accurate 'last_login_timestamp' field on the User data model, which must be indexed for performance.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The job must be designed to run efficiently without impacting the performance of the live application. It should be scheduled for off-peak hours.
- Database queries to identify inactive users must complete within a reasonable timeframe (e.g., < 5 minutes) even with millions of user records.
- The process should operate in batches to avoid overwhelming database resources.

## 7.2.0.0 Security

- The job must run with the minimum necessary IAM permissions to read user data, delete it, and write to the audit log.
- The process of anonymizing logs is security-critical and must not leak PII into other log streams.
- The audit log of deletions must be immutable and protected from tampering.

## 7.3.0.0 Usability

- N/A

## 7.4.0.0 Accessibility

- N/A

## 7.5.0.0 Compatibility

- N/A

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

High

## 8.2.0.0 Complexity Factors

- The destructive and irreversible nature of the operation requires extreme care and thorough testing.
- Implementing a reliable and performant log anonymization process is technically challenging and may require a separate, dedicated service.
- Ensuring the process is performant at scale and does not cause database contention.
- Managing cascading deletes across multiple related tables (Library, Sessions, etc.) must be handled flawlessly to prevent orphaned data.

## 8.3.0.0 Technical Risks

- Accidental deletion of active user data due to a bug in the inactivity detection logic.
- Performance degradation of the main application if the purge job is too resource-intensive.
- Incomplete deletion of user data, leaving orphaned records or failing to comply fully with GDPR.
- High cost or technical infeasibility of the log anonymization requirement as specified.

## 8.4.0.0 Integration Points

- Primary Database (Amazon Aurora)
- Logging System (Amazon CloudWatch Logs)
- Scheduling Service (Amazon EventBridge)
- Notification Service (Amazon SNS for alerts)
- Audit Log System

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- Security

## 9.2.0.0 Test Scenarios

- Verify purge of a user inactive for > 2 years.
- Verify non-purge of a user inactive for < 2 years.
- Verify job failure and rollback behavior.
- Verify audit log creation.
- Verify performance of the user identification query against a large, seeded dataset.

## 9.3.0.0 Test Data Needs

- A test database seeded with user accounts having a wide range of 'last_login_timestamp' values, including just under, exactly at, and just over the 2-year threshold.

## 9.4.0.0 Testing Tools

- Jest for backend unit tests.
- A dedicated integration testing suite that can provision AWS resources and seed a database.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment
- Code reviewed and approved by at least two backend engineers
- Unit tests implemented with >80% coverage for the purge logic
- Integration testing completed successfully against a staging database
- A 'dry run' mode is implemented and verified to correctly identify candidates for deletion without performing the action
- Performance of the identification query is benchmarked and meets requirements
- Security review of the IAM roles and data handling process is complete
- Technical documentation (runbook) for the job, including manual trigger and failure recovery steps, is created
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

13

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical compliance feature. It is purely backend work and requires a senior engineer with experience in data management and AWS.
- A technical spike may be required to investigate the most effective approach for log anonymization before full implementation.

## 11.4.0.0 Release Impact

This feature is not user-facing but is critical for the product's legal and operational readiness. It must be implemented before the system holds a significant amount of user data.

