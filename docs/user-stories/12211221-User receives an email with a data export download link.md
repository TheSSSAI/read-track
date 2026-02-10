# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-034 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User receives an email with a data export download... |
| As A User Story | As a user who has requested an export of my person... |
| User Persona | Any registered user (Free or Premium) who has init... |
| Business Value | Fulfills the 'Right to Data Portability' under GDP... |
| Functional Area | User Management & Data Privacy |
| Story Theme | Account & Data Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful data export and email notification

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has successfully requested a data export via the 'Export My Data' feature

### 3.1.5 When

the asynchronous backend job successfully completes the data export and generates the file

### 3.1.6 Then

an email is sent to the user's registered email address using Amazon SES

### 3.1.7 And

the email body explicitly states the link's expiration period (e.g., 'This link will expire in 24 hours.')

### 3.1.8 Validation Notes

Verify by checking the mock email inbox in the test environment for an email with the correct content and a valid, clickable link.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User attempts to use an expired download link

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

a user has received a data export email

### 3.2.5 And

the data file is not downloaded

### 3.2.6 When

the user clicks the download link

### 3.2.7 Then

the user is directed to a static web page explaining that the link has expired

### 3.2.8 Validation Notes

Automated test should attempt to access the pre-signed URL after its expiration time and assert that it receives an access denied error (which should be handled by a redirect to the error page).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Data export job fails to complete

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a user has requested a data export

### 3.3.5 When

the asynchronous backend job fails for any reason

### 3.3.6 Then

an email is sent to the user's registered email address informing them of the failure

### 3.3.7 And

the email does not contain a download link

### 3.3.8 Validation Notes

Simulate a job failure in the test environment and verify that the correct failure notification email is sent.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Security of the generated file and link

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a data export file has been generated and stored in Amazon S3

### 3.4.5 When

an attempt is made to access the file's S3 URL directly without the pre-signed parameters

### 3.4.6 Then

access is denied

### 3.4.7 And

the file is only accessible via the complete, valid, and unexpired pre-signed URL

### 3.4.8 Validation Notes

Confirm S3 bucket policies are set to private. Attempt to access the object URL directly and verify a 403 Forbidden error. Then, access with the pre-signed URL and verify a 200 OK.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- HTML Email Template (Success)
- HTML Email Template (Failure)
- Static Web Page (Link Expired)

## 4.2.0 User Interactions

- User clicks a link within an email.

## 4.3.0 Display Requirements

- Email must be responsive and render correctly on major mobile and web clients.
- Email must contain the app's branding.
- 'Link Expired' page must be branded and provide clear instructions.

## 4.4.0 Accessibility Needs

- Email templates should include a plain-text version for accessibility.
- HTML in emails and on the error page must use semantic markup.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Data export download links must expire 24 hours after creation.

### 5.1.3 Enforcement Point

Amazon S3 (via pre-signed URL policy) and the backend service that generates the link.

### 5.1.4 Violation Handling

Access to the S3 object is denied by AWS. The system should present a user-friendly error page.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Generated data export files must be permanently deleted after 24 hours.

### 5.2.3 Enforcement Point

An automated S3 lifecycle policy on the export bucket.

### 5.2.4 Violation Handling

N/A. This is a system cleanup rule to enforce data retention policies.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-033', 'dependency_reason': 'This story implements the notification part of the data export process, which is triggered by the completion of the export request initiated in US-033.'}

## 6.2.0 Technical Dependencies

- Amazon SQS: For receiving the 'export complete' or 'export failed' message from the worker.
- Amazon S3: For storing the generated data file and serving it via a pre-signed URL.
- Amazon SES: For sending the notification email.
- Backend Notification Service (e.g., AWS Lambda): To orchestrate the process of generating the link and sending the email.

## 6.3.0 Data Dependencies

- User's verified email address.
- Reference to the generated file's location in S3.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The notification email should be sent within 10 seconds of the export job's completion.

## 7.2.0 Security

- The S3 bucket for data exports must be private and not publicly accessible.
- Pre-signed URLs must be used for download links and must have the shortest practical expiration time (24 hours).
- The process must comply with GDPR/CCPA requirements for secure data handling.

## 7.3.0 Usability

- Email content must be clear, concise, and unambiguous.
- The process should feel seamless to the user, despite its asynchronous nature.

## 7.4.0 Accessibility

- WCAG 2.1 Level AA standards should be applied to the 'Link Expired' web page.

## 7.5.0 Compatibility

*No items available*

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Requires orchestration of multiple asynchronous AWS services (SQS, S3, SES, Lambda/Fargate).
- Correct implementation of security policies (IAM roles for Lambda, S3 bucket policies) is critical and non-trivial.
- Requires creating and managing responsive HTML email templates.
- End-to-end testing of the asynchronous flow is more complex than for a synchronous API.

## 8.3.0 Technical Risks

- Misconfiguration of IAM or S3 policies could lead to a data breach.
- Issues with email deliverability (e.g., being marked as spam) could prevent users from receiving their link.
- Failure to properly handle errors in the asynchronous workflow could leave the process in an inconsistent state.

## 8.4.0 Integration Points

- SQS queue for job completion messages.
- Amazon S3 API for generating pre-signed URLs.
- Amazon SES API for sending templated emails.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0 Test Scenarios

- Successful email delivery on job completion.
- Failure email delivery on job failure.
- Accessing a valid link successfully downloads the file.
- Accessing an expired link shows the correct error page.
- Attempting to access the S3 object directly (without pre-signed URL) fails.

## 9.3.0 Test Data Needs

- A test user account with a valid, accessible email address (e.g., using a service like Mailinator or a test inbox).
- Sample exported data file (JSON).

## 9.4.0 Testing Tools

- Jest for backend unit tests.
- AWS SDK mocks.
- A tool for testing email delivery in CI/CD, such as MailHog or a similar mock SMTP server.
- Integration test suite capable of interacting with AWS services in a test environment.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for the notification service and passing with >80% coverage
- Integration testing between the SQS queue, notification service, and SES is completed successfully
- End-to-end test scenario for requesting an export and receiving the email is automated and passing
- Email templates (success, failure) and the 'Link Expired' page are reviewed and approved
- Security requirements, including S3 bucket policies and IAM roles, are validated by a peer
- Logging and monitoring for the notification service are in place
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This story is blocked by US-033.
- Requires AWS credentials and permissions for SQS, S3, and SES in development/testing environments.
- The team should allocate time for creating and testing the email templates.

## 11.4.0 Release Impact

This is a critical feature for achieving GDPR/CCPA compliance. The data export feature cannot be released without it.

