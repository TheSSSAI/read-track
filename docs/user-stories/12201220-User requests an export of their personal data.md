# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-033 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User requests an export of their personal data |
| As A User Story | As a registered user, I want to request and downlo... |
| User Persona | Any registered user (Free or Premium). This is a c... |
| Business Value | Ensures compliance with data privacy regulations l... |
| Functional Area | User Management & Settings |
| Story Theme | Regulatory Compliance & User Trust |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User successfully requests and receives data export

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user on the 'Account Settings' screen

### 3.1.5 When

I tap the 'Export My Data' option

### 3.1.6 Then

The system displays a confirmation message like 'Your data export is being prepared. We will email you a download link shortly.' and the backend queues an asynchronous job via Amazon SQS.

### 3.1.7 Validation Notes

Verify a message is added to the SQS queue with the correct user ID. The UI should provide immediate feedback and not block.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: User receives email and downloads data

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the asynchronous export job has completed successfully

### 3.2.5 When

the system sends an email to my registered address via Amazon SES

### 3.2.6 Then

the email contains a secure, time-limited (24-hour) link to download my data from Amazon S3, and clicking the link successfully downloads a JSON file.

### 3.2.7 Validation Notes

Verify the email is sent and the pre-signed S3 URL works. The downloaded file must be valid JSON.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Happy Path: Exported data format and content is correct

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have downloaded the data export file

### 3.3.5 When

I inspect the contents of the JSON file

### 3.3.6 Then

it contains my complete library list (all shelves) and a full history of all my reading sessions, matching the data model defined in REQ-TRK-001.

### 3.3.7 Validation Notes

Validate the JSON schema and content against the user's actual data in the database.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edge Case: User requests a new export while one is already in progress

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I have already requested a data export and the job is still processing

### 3.4.5 When

I tap the 'Export My Data' option again

### 3.4.6 Then

the system displays a message like 'An export is already in progress. Please wait for the current request to complete.' and does not queue a new job.

### 3.4.7 Validation Notes

The backend should check for an existing 'pending' export job for the user before creating a new one.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: User clicks an expired download link

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I have received a data export email

### 3.5.5 When

I click the download link more than 24 hours after it was generated

### 3.5.6 Then

I am shown an error page or message indicating the link has expired and I must request a new export.

### 3.5.7 Validation Notes

This is handled by the S3 pre-signed URL's expiry policy. Test by generating a link with a short expiry and attempting to access it after the time has passed.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Error Condition: Backend export job fails

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I have requested a data export

### 3.6.5 When

the backend job fails due to an unexpected error

### 3.6.6 Then

the system sends me an email notifying me of the failure and advising me to try again later, and the failure is logged for developer review.

### 3.6.7 Validation Notes

Use a dead-letter queue (DLQ) for the SQS queue to capture failed jobs. The notification logic should be triggered by the failure event.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Edge Case: User has no data to export

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a new user with no library items or reading sessions

### 3.7.5 When

I request and download my data export

### 3.7.6 Then

the system provides a valid JSON file with empty arrays for 'libraryItems' and 'readingSessions'.

### 3.7.7 Validation Notes

The process should complete successfully without errors, generating a structurally correct but empty file.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled 'Export My Data' button or list item within the 'Account Settings' screen.
- A non-blocking toast notification or confirmation dialog to provide feedback after the request is initiated.

## 4.2.0 User Interactions

- User taps the 'Export My Data' option.
- User dismisses the confirmation message.

## 4.3.0 Display Requirements

- The confirmation message must clearly state that the export has started and will be delivered via email.
- The email must clearly identify the app, state the purpose of the email, and specify the 24-hour validity of the download link.

## 4.4.0 Accessibility Needs

- The 'Export My Data' button must have a proper accessibility label for screen readers.
- The confirmation message must be accessible to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user can only have one pending data export request at a time.

### 5.1.3 Enforcement Point

Backend API, before queuing a new export job.

### 5.1.4 Violation Handling

Return a specific error code (e.g., 429 Too Many Requests) with a user-friendly message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Data export files must be automatically and permanently deleted after 24 hours.

### 5.2.3 Enforcement Point

Amazon S3, via a bucket lifecycle policy.

### 5.2.4 Violation Handling

N/A (System-enforced policy). Misconfiguration should trigger a monitoring alert.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

User must be able to sign up with Google to have an account and email.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-003

#### 6.1.2.2 Dependency Reason

User must be able to sign up with Apple to have an account and email.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-037

#### 6.1.3.2 Dependency Reason

Core functionality for adding books to a library must exist to have data to export.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-046

#### 6.1.4.2 Dependency Reason

Core functionality for logging reading sessions must exist to have data to export.

## 6.2.0.0 Technical Dependencies

- Amazon SQS for asynchronous job queuing.
- Amazon S3 for secure, temporary file storage.
- Amazon SES for sending email notifications.
- A serverless function (AWS Lambda) or container (AWS Fargate) to process the export job.
- Backend authentication to identify the user making the request.

## 6.3.0.0 Data Dependencies

- Access to the user's registered email address.
- Read access to the User, LibraryItem, and ReadingSession tables in the primary database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The initial API request to trigger the export must respond in under 500ms.
- The asynchronous job for a user with a large library (e.g., 1000 books, 10,000 sessions) should complete within 5 minutes.

## 7.2.0.0 Security

- The S3 bucket for exports must be private, with access granted only via secure, short-lived pre-signed URLs.
- The export generation process must be strictly scoped to the requesting user's data.
- The system must enforce the 24-hour data retention policy for export files as per REQ-DAT-001.

## 7.3.0.0 Usability

- The process should be simple and require minimal user effort (ideally a single tap).
- Feedback to the user must be clear and immediate.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must be accessible on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Coordination of multiple AWS services (API Gateway, SQS, Lambda/Fargate, S3, SES).
- Implementing a robust asynchronous processing flow with proper error handling and notifications.
- Ensuring secure access and automatic cleanup of sensitive user data.
- Defining and implementing an efficient database query to gather all user data without impacting system performance.

## 8.3.0.0 Technical Risks

- Misconfiguration of IAM roles or S3 bucket policies could lead to a data breach.
- Failure in the asynchronous job could leave the user without their data; a robust retry and notification mechanism is critical.
- Potential for long-running jobs for power users, requiring optimization of the data gathering process.

## 8.4.0.0 Integration Points

- Frontend App -> Backend API
- Backend API -> Amazon SQS
- SQS Consumer (Lambda/Fargate) -> Amazon Aurora DB
- SQS Consumer (Lambda/Fargate) -> Amazon S3
- SQS Consumer (Lambda/Fargate) -> Amazon SES

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify successful end-to-end flow for a user with data.
- Verify flow for a user with no data.
- Test the 'export in progress' logic by making concurrent requests.
- Test the expired link scenario by setting a short expiry on a pre-signed URL.
- Simulate a job failure and verify the user is notified correctly and the event is logged.
- Security scan of S3 bucket configuration and IAM policies.

## 9.3.0.0 Test Data Needs

- A test user account with a small amount of data (e.g., 5 books, 20 sessions).
- A test user account with a large amount of data to test performance.
- A test user account with no data.

## 9.4.0.0 Testing Tools

- Jest for backend unit tests.
- A tool to inspect SQS queues and S3 buckets in a test environment.
- An email testing service (e.g., MailHog, Mailtrap) to intercept and validate emails without sending them externally.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Code for all components (API endpoint, SQS consumer) reviewed and approved.
- Unit and integration tests implemented with >= 80% code coverage.
- End-to-end automated test for the happy path is created and passing.
- S3 bucket lifecycle policy for 24-hour deletion is configured and verified.
- Security review of IAM roles and S3 policies is complete.
- Email template is approved by the product owner.
- The JSON schema for the data export is documented.
- Story deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a compliance-critical feature.
- Requires backend-heavy work involving multiple cloud services. Ensure team has necessary AWS expertise or allocates time for it.
- Frontend work is minimal and can be completed quickly.

## 11.4.0.0 Release Impact

- This feature is required for the initial public release to meet legal compliance standards.

