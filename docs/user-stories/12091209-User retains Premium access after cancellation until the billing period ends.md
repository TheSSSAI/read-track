# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-022 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User retains Premium access after cancellation unt... |
| As A User Story | As a Premium User who has decided to cancel my sub... |
| User Persona | A 'Premium User' who has initiated the cancellatio... |
| Business Value | Ensures compliance with Apple and Google's subscri... |
| Functional Area | User Management & Billing |
| Story Theme | Subscription Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful cancellation of a subscription with retained access

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A Premium User has an active subscription with a 'current_period_end' date in the future

### 3.1.5 When

the backend receives a server-to-server notification (webhook) from the platform (Apple/Google) indicating the user has turned off auto-renewal

### 3.1.6 Then

the system must update the user's subscription record to flag it as 'cancelled' or 'will_not_renew'

### 3.1.7 And

the user must retain full, unrestricted access to all Premium features until the 'current_period_end' date is reached

### 3.1.8 Validation Notes

Verify in the database that the user's role is still 'premium_user' and a cancellation flag is set. Log in as the user and confirm access to premium features like Advanced Statistics and unlimited library items.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

UI correctly displays the cancelled-but-active subscription state

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A Premium User has cancelled their subscription but it has not yet expired

### 3.2.5 When

the user navigates to the subscription management screen in the app

### 3.2.6 Then

the UI must clearly display a message indicating their Premium status is active until a specific date (e.g., 'Your Premium access will end on YYYY-MM-DD')

### 3.2.7 And

the primary call-to-action should change from 'Upgrade' to an option to 'Re-subscribe' or 'Keep Premium'

### 3.2.8 Validation Notes

Visually inspect the subscription screen on both iOS and Android to confirm the correct messaging and button text is displayed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User re-subscribes before the cancellation period ends

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

A Premium User has cancelled their subscription but it has not yet expired

### 3.3.5 When

the user taps the 'Re-subscribe' button and successfully completes the native in-app purchase flow

### 3.3.6 Then

the system must receive a webhook for the renewal and update the user's subscription record to remove the cancellation flag

### 3.3.7 And

the subscription screen UI must revert to the standard active Premium User state

### 3.3.8 Validation Notes

Perform this flow using a sandbox account. Verify the database record is updated correctly and the UI refreshes to show the active, non-cancelled state.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Subscription period ends and account is downgraded

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A user's cancelled subscription has a 'current_period_end' date that is now in the past

### 3.4.5 When

the user opens the app or a scheduled system job runs

### 3.4.6 Then

the user's account status must be automatically downgraded to 'Free User'

### 3.4.7 And

all Free User limitations (e.g., ad display, feature limits) must be applied immediately

### 3.4.8 Validation Notes

This is a direct dependency on US-020. The test involves verifying the user's role is changed in the database and that upon next login, they see ads and are blocked from premium actions.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Webhook processing failure

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

The backend receives a cancellation webhook from a payment provider

### 3.5.5 When

the webhook processing fails due to a transient error (e.g., database timeout)

### 3.5.6 Then

the system must not permanently fail the request

### 3.5.7 And

an alert must be triggered in CloudWatch to notify the development team after 3 failed retry attempts

### 3.5.8 Validation Notes

Simulate a database failure for the webhook endpoint and verify that the message is correctly routed to the configured SQS dead-letter queue and a CloudWatch alarm is triggered.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A text area on the subscription screen to display the subscription's end date.
- A 'Re-subscribe' or 'Keep Premium' button that replaces the standard 'Upgrade' button.

## 4.2.0 User Interactions

- User can view their subscription end date.
- User can tap the 'Re-subscribe' button to initiate the native purchase flow to undo the cancellation.

## 4.3.0 Display Requirements

- The message must be unambiguous, stating clearly that access is retained and providing the exact expiration date.
- The UI should not imply that the user has lost any features before the expiration date.

## 4.4.0 Accessibility Needs

- The status message must be readable by screen readers.
- The color contrast of the status message must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user who cancels a subscription retains all rights and access of that subscription tier until the end of the current, paid-for billing period.', 'enforcement_point': 'Backend API (access control middleware) and scheduled jobs.', 'violation_handling': 'N/A - This rule defines expected behavior, not a violation.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must be able to process a successful subscription purchase before it can handle a cancellation of that subscription.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-020

#### 6.1.2.2 Dependency Reason

This story handles the state *before* expiration. US-020 handles the state transition *at* expiration. They are two halves of the same lifecycle event and should be developed and tested together.

## 6.2.0.0 Technical Dependencies

- Backend webhook endpoint capable of receiving and validating notifications from both Apple App Store and Google Play Billing.
- Database schema in Aurora (User/Subscription table) must support fields for `subscription_status`, `period_end_date`, and `cancellation_date`.
- Integration with Amazon SQS for reliable, asynchronous processing of incoming webhooks and a DLQ for handling failures.

## 6.3.0.0 Data Dependencies

- Requires a valid subscription record for a user in the database, created by a successful purchase event.

## 6.4.0.0 External Dependencies

- Apple App Store Server Notifications V2.
- Google Play Real-time developer notifications.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Webhook endpoint must acknowledge receipt of the notification within 200ms to prevent the provider from resending.

## 7.2.0.0 Security

- Webhook endpoints must be secured and must validate the authenticity of incoming notifications using cryptographic signatures (e.g., JWT validation for Apple, Base64-decoded message validation for Google) to prevent spoofing.

## 7.3.0.0 Usability

- The in-app messaging about the subscription status must be clear, concise, and free of jargon to avoid user confusion.

## 7.4.0.0 Accessibility

- All UI elements related to this state must be compliant with WCAG 2.1 AA standards.

## 7.5.0.0 Compatibility

- The logic must correctly handle notifications from all supported OS versions (iOS 14+, Android 7+).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires integration with two separate, complex external webhook systems (Apple and Google) with different data formats and validation methods.
- Asynchronous nature of webhooks requires robust error handling, retries, and idempotency.
- Managing the user's state transition accurately over time (cancelled -> expired -> free) requires careful logic and potentially a scheduled job.

## 8.3.0.0 Technical Risks

- Webhook delivery is not guaranteed to be instantaneous or in order. The system must be resilient to delays or out-of-order events.
- Platform-specific edge cases (e.g., subscription pauses, grace periods for billing issues) can complicate the state machine.

## 8.4.0.0 Integration Points

- Auth0 (to get user identity)
- Apple App Store / Google Play Billing (for webhooks)
- Amazon Aurora (to update user subscription state)
- Amazon SQS (for webhook processing queue)
- Amazon CloudWatch (for alerting on failures)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify a monthly subscriber cancels and retains access until the end of the month.
- Verify a yearly subscriber cancels and retains access until the end of the year.
- Verify a user in a cancelled state can successfully re-subscribe.
- Verify the webhook endpoint correctly rejects an invalid/unauthenticated request.
- Verify the scheduled job correctly downgrades an account whose `period_end_date` has passed.

## 9.3.0.0 Test Data Needs

- Sandbox/test accounts for both Apple App Store and Google Play.
- Mock webhook payloads for all relevant event types (e.g., Apple's `DID_CHANGE_RENEWAL_STATUS`, Google's `SUBSCRIPTION_CANCELED`).

## 9.4.0.0 Testing Tools

- Jest (Backend unit/integration tests)
- Postman or Insomnia (for manually sending mock webhooks to a local dev environment)
- Apple StoreKit Test Framework
- Google Play Billing test tracks

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for webhook processing logic with >80% coverage
- Integration testing completed for webhook endpoint with mock data
- E2E testing successfully performed using sandbox accounts for both iOS and Android
- User interface on the subscription screen reviewed and approved by UX/Product
- Security requirements for webhook validation are implemented and verified
- Documentation for the webhook endpoint and subscription state management is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational feature for any subscription app and is required for launch.
- Requires coordination with both frontend and backend developers.
- Setup of sandbox testing environments for Apple and Google can be time-consuming if not already in place.

## 11.4.0.0 Release Impact

- Blocks the ability to launch the app with subscriptions if not completed. This is a critical path item.

