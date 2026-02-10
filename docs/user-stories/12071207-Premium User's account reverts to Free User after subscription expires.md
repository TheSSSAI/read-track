# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-020 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User's account reverts to Free User after ... |
| As A User Story | As a former Premium User whose subscription has ex... |
| User Persona | A user whose paid Premium subscription has reached... |
| Business Value | Automates the subscription lifecycle, correctly en... |
| Functional Area | User Account & Subscription Management |
| Story Theme | Freemium Business Model |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful downgrade after subscription expiration

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A user has an active Premium subscription with a defined expiration date

### 3.1.5 When

The backend receives a server-to-server notification (webhook) from the app store (Apple/Google) that the subscription has expired (e.g., `EXPIRED` or `DID_FAIL_TO_RENEW`)

### 3.1.6 Then

The system validates the webhook and updates the user's account status from 'Premium User' to 'Free User' in the primary database.

### 3.1.7 And

The next time the user's session JWT is refreshed, it contains the 'free_user' role/claim.

### 3.1.8 Validation Notes

Test by simulating a webhook from the app store's sandbox environment and verifying the user's record in the database and the claims in a newly issued JWT.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

UI reflects Free User status after downgrade

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A user's account has been reverted to the 'Free User' tier

### 3.2.5 When

The user opens the app or their session is refreshed

### 3.2.6 Then

The UI correctly displays their status as 'Free User' on the account/settings screen.

### 3.2.7 And

Premium-only features, such as the 'Advanced Insights' section, are visibly disabled or locked.

### 3.2.8 Validation Notes

Log in as the downgraded test user and visually inspect the account screen, ad placement, and the state of premium features.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Existing data exceeding free limits is preserved

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

A user's account is reverted to 'Free User' and they have 50 books in their library (exceeding the free limit of 20)

### 3.3.5 When

The user navigates to their library

### 3.3.6 Then

The user can view all 50 of their existing books.

### 3.3.7 And

No user-generated data (library items, goals, reading sessions) has been deleted as a result of the downgrade.

### 3.3.8 Validation Notes

Set up a premium user with data exceeding free limits, trigger the downgrade, and then verify that all data is still present and accessible via the API and UI.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User over the limit is prevented from adding new items

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A downgraded 'Free User' has 50 books in their library

### 3.4.5 When

The user attempts to add a 51st book

### 3.4.6 Then

The system prevents the item from being added.

### 3.4.7 And

The UI displays a message explaining the limit has been reached and presents an option to upgrade.

### 3.4.8 Validation Notes

Using the test user from AC-003, attempt to add a new book via the UI and API, and assert that the action is blocked with the correct error message/response.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User over the limit is prevented from adding new goals

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

A downgraded 'Free User' has 2 active goals (exceeding the free limit of 1)

### 3.5.5 When

The user attempts to create a new goal

### 3.5.6 Then

The system prevents the goal from being created.

### 3.5.7 And

The UI element to create a new goal is disabled or, when tapped, leads to an upgrade prompt.

### 3.5.8 Validation Notes

Set up a premium user with multiple goals, trigger the downgrade, and then attempt to create another goal, verifying the block.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Fallback mechanism handles missed webhooks

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A user's subscription has expired, but the backend missed the webhook notification

### 3.6.5 When

A periodic system job runs to check the status of all active subscriptions

### 3.6.6 Then

The job identifies the expired subscription by querying the app store's status API or its own records.

### 3.6.7 And

The user's account is correctly downgraded to 'Free User'.

### 3.6.8 Validation Notes

Manually set a subscription as expired in the database without triggering the webhook handler. Run the scheduled job and verify the user's status is corrected.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Lock icons or overlays on premium features (e.g., 'Advanced Insights' tab).
- Upgrade prompts/modals that appear when a user tries to access a locked feature or exceed a limit.
- Text on the Account/Settings screen clearly stating the current subscription tier is 'Free'.

## 4.2.0 User Interactions

- Tapping on a locked feature should trigger an upgrade prompt.
- The transition from a premium to a free experience should be clear upon the next app launch after the downgrade.

## 4.3.0 Display Requirements

- The app must cease displaying premium-only UI components.
- The app must begin displaying advertisements as defined in REQ-ADS-001.

## 4.4.0 Accessibility Needs

- All upgrade prompts and status changes must be clearly communicated to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Upon subscription expiration, a user's role must be changed from 'Premium' to 'Free'.

### 5.1.3 Enforcement Point

Backend, triggered by app store webhook or a scheduled job.

### 5.1.4 Violation Handling

The system must have a fallback/reconciliation job to correct any accounts that were not downgraded correctly.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A downgraded user who is over the free tier limits for any feature (e.g., library items, goals) cannot add new items of that type.

### 5.2.3 Enforcement Point

Backend API, when processing POST/CREATE requests for limited resources.

### 5.2.4 Violation Handling

The API must return a specific error code (e.g., 403 Forbidden or 402 Payment Required) with a message indicating the limit has been reached.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

User-generated content exceeding free limits must not be deleted upon downgrade.

### 5.3.3 Enforcement Point

Backend downgrade logic.

### 5.3.4 Violation Handling

The downgrade process must be designed to only change the user's role and not trigger any data deletion logic.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must be able to process a subscription purchase before it can handle a subscription expiration.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-013

#### 6.1.2.2 Dependency Reason

The logic to enforce the 20-item library limit for Free Users must exist to be re-applied.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-014

#### 6.1.3.2 Dependency Reason

The logic to enforce the 1-goal limit for Free Users must exist to be re-applied.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-023

#### 6.1.4.2 Dependency Reason

The ad-serving mechanism must be in place to be activated for the newly downgraded user.

## 6.2.0.0 Technical Dependencies

- Backend integration with Apple App Store Server Notifications V2.
- Backend integration with Google Play Real-time developer notifications.
- A user role/permission system, managed via the user's JWT, to control API access.
- A scheduled task runner (e.g., AWS Lambda with EventBridge Scheduler) for the fallback mechanism.

## 6.3.0.0 Data Dependencies

- The user's record in the database must contain their current subscription status and the expiration date.

## 6.4.0.0 External Dependencies

- Apple App Store and Google Play Store sandbox environments for testing subscription lifecycle events.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The webhook processing time should be under 500ms to promptly acknowledge the notification.
- The fallback scheduled job must be optimized to not cause significant load on the database.

## 7.2.0.0 Security

- Webhook endpoints must be protected and must validate the authenticity of incoming requests from Apple/Google (e.g., using JWT validation for Apple S2S, or Pub/Sub authentication for Google).

## 7.3.0.0 Usability

- The user should not be logged out. The change in status should be reflected seamlessly within their existing session.

## 7.4.0.0 Accessibility

- N/A for this primarily backend story, but UI changes must adhere to REQ-UIF-001.

## 7.5.0.0 Compatibility

- The webhook handling logic must be compatible with the latest API versions specified by Apple and Google.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires handling two different, complex external webhook systems (Apple and Google).
- Logic must be idempotent to handle potential duplicate webhooks.
- Requires a reliable fallback mechanism (scheduled job) to ensure 100% accuracy in subscription status, adding an extra component to build and maintain.

## 8.3.0.0 Technical Risks

- App store webhooks can be delayed or missed, making the fallback mechanism critical.
- Changes in the app store server-to-server notification APIs could require future maintenance.

## 8.4.0.0 Integration Points

- Apple App Store Server API
- Google Play Developer API
- Internal User Authentication Service (for updating JWT claims)
- Primary Database (for updating user role)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Simulate an `EXPIRED` webhook from Apple Sandbox.
- Simulate a `SUBSCRIPTION_EXPIRED` notification from Google Play.
- Test the over-limit blocking for every limited feature (books, goals, AI suggestions).
- Verify that a user can successfully re-subscribe after being downgraded.
- Manually trigger the fallback job to test its effectiveness on a deliberately 'stuck' premium account.

## 9.3.0.0 Test Data Needs

- Test accounts in App Store Connect and Google Play Console with active subscriptions that can be managed (e.g., cancelled, expired) for testing purposes.

## 9.4.0.0 Testing Tools

- A tool like ngrok to expose local webhook handlers to the public internet for testing.
- Jest for backend unit/integration tests.
- Flutter `integration_test` package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for webhook parsing and downgrade logic, with >80% coverage
- Integration tests successfully validate the end-to-end flow for both Apple and Google webhooks
- Fallback scheduled job is implemented, tested, and deployed
- UI changes for locked features are implemented and reviewed
- Security review of webhook endpoints completed
- Documentation for webhook handling and the downgrade process is created/updated
- Story deployed and verified in the staging environment using sandbox accounts

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the subscription model and must be completed before launch.
- Requires coordination with setting up App Store Connect and Google Play Console for server-to-server notifications.
- The developer will need dedicated time to understand the specifics of both Apple's and Google's notification systems.

## 11.4.0.0 Release Impact

- Critical for launch. Without this, the freemium model is unenforceable and will lead to revenue loss.

