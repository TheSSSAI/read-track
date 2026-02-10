# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-013 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User is prevented from adding more than 20 li... |
| As A User Story | As a Free User who has already saved 20 items in m... |
| User Persona | Free User: An authenticated user who has not purch... |
| Business Value | This is a primary monetization gate that enforces ... |
| Functional Area | Library Management & Monetization |
| Story Theme | Freemium Subscription Model |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Free User with 20 items attempts to add a 21st item

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in 'Free User' and my personal library contains exactly 20 items (across all shelves)

### 3.1.5 When

I attempt to add a new item to my library from any source (e.g., book search, article URL, AI recommendation)

### 3.1.6 Then

The system must prevent the new item from being saved to my library, my library count must remain 20, and a modal dialog must be displayed.

### 3.1.7 Validation Notes

Verify via UI that the modal appears. Check the database or subsequent API calls to confirm the user's library item count is still 20.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Upgrade prompt content and actions

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The upgrade prompt modal is displayed because I have reached my 20-item limit

### 3.2.5 When

I view the modal

### 3.2.6 Then

The modal must contain: a clear title like 'Free Limit Reached', text explaining the 20-item limit, a primary Call-to-Action button like 'Upgrade to Premium', and a secondary dismissal option like a 'Maybe Later' button or an 'X' icon.

### 3.2.7 Validation Notes

Visual inspection of the UI modal to ensure all required elements and text are present.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User interacts with the upgrade prompt's primary action

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The upgrade prompt modal is displayed

### 3.3.5 When

I tap the 'Upgrade to Premium' button

### 3.3.6 Then

I must be navigated to the in-app subscription screen where I can view Premium benefits and make a purchase.

### 3.3.7 Validation Notes

E2E test: trigger the modal, tap the upgrade button, and verify the correct screen is presented.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Free User with fewer than 20 items adds an item

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am a logged-in 'Free User' and my personal library contains 19 items

### 3.4.5 When

I attempt to add a new item to my library

### 3.4.6 Then

The item must be successfully added to my library, my library count must become 20, and no upgrade prompt shall be displayed.

### 3.4.7 Validation Notes

Verify the item is added and the library count is updated correctly without any modal appearing.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Premium User adds an item when they have more than 20

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am a logged-in 'Premium User' and my personal library contains 50 items

### 3.5.5 When

I attempt to add a new item to my library

### 3.5.6 Then

The item must be successfully added to my library, my library count must become 51, and no limit or prompt shall be applied.

### 3.5.7 Validation Notes

Test with a premium-flagged account to ensure the limit logic is correctly bypassed.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Former Premium User with more than 20 items attempts to add another

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a 'Free User' whose Premium subscription has expired, and my library contains 25 items

### 3.6.5 When

I attempt to add a new item to my library

### 3.6.6 Then

The system must prevent the new item from being saved, my library count must remain 25, and the upgrade prompt modal must be displayed.

### 3.6.7 Validation Notes

This tests the scenario described in REQ-BUS-001. The user's existing data is preserved but they cannot add more until they are within the free limit or re-subscribe.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Backend authoritatively enforces the limit

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am a 'Free User' with 20 items

### 3.7.5 When

I attempt to bypass the client-side check and send an 'add item' request directly to the API

### 3.7.6 Then

The backend must reject the request with an appropriate status code (e.g., 403 Forbidden or 422 Unprocessable Entity) and an error message indicating the limit has been reached.

### 3.7.7 Validation Notes

This requires an integration or API-level test. The server must be the single source of truth for this business rule.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Modal Dialog / Bottom Sheet: To display the upgrade prompt.
- Primary Button: 'Upgrade to Premium'
- Secondary Button / Icon: 'Maybe Later' or 'X' to dismiss.

## 4.2.0 User Interactions

- The attempt to add an item is interrupted by the modal.
- Tapping 'Upgrade' navigates to the subscription screen.
- Tapping 'Maybe Later' or 'X' dismisses the modal and returns the user to their previous context (e.g., search results).

## 4.3.0 Display Requirements

- The modal must clearly state the 20-item limit for the free plan.
- The modal must present the upgrade as the solution to the limit.

## 4.4.0 Accessibility Needs

- The modal must be focus-trapped, so screen reader users cannot interact with the content behind it.
- All text must have sufficient color contrast.
- Buttons must have accessible names and be large enough to tap easily.

# 5.0.0 Business Rules

- {'rule_id': 'BR-FRE-001', 'rule_description': "A user with the 'Free User' role cannot have more than 20 items in their library. This check applies only to the action of adding new items.", 'enforcement_point': 'Backend: Before creating a new LibraryItem record in the database. Frontend: As a preliminary check to provide immediate user feedback.', 'violation_handling': 'The add-item transaction is rejected. The user interface displays an upgrade prompt.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

Requires user authentication to determine the user's ID and subscription status.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-037

#### 6.1.2.2 Dependency Reason

Requires the core functionality of adding a book to the library, which this story modifies.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-018

#### 6.1.3.2 Dependency Reason

Requires the existence of a subscription/upgrade screen to navigate the user to from the prompt.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for adding a library item.
- Backend logic to check user's role/subscription tier from their authenticated session (e.g., JWT).
- Frontend state management (Riverpod) to hold user's subscription status and library count.

## 6.3.0.0 Data Dependencies

- User record in the database must have a field indicating their subscription tier ('Free' or 'Premium').

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The backend check for item count must complete within the API's P95 latency target of <200ms (NFR-PERF-001).

## 7.2.0.0 Security

- The user's subscription status must be determined authoritatively by the backend based on a validated session token (JWT), not a client-provided value (NFR-SEC-002).

## 7.3.0.0 Usability

- The upgrade prompt must be clear, concise, and non-punitive. It should feel like an opportunity, not a penalty.

## 7.4.0.0 Accessibility

- The modal must adhere to WCAG 2.1 Level AA standards, as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The modal dialog must render correctly on all supported iOS and Android screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The core logic is a simple database count and a conditional check.
- Requires coordination between frontend and backend to handle the specific 'limit reached' error response.
- The check must be implemented at every point a user can add an item to ensure consistency.

## 8.3.0.0 Technical Risks

- Potential for a mismatch between client-side cached count and the authoritative server-side count, leading to a confusing UX. The server must always be the source of truth.

## 8.4.0.0 Integration Points

- Backend API: The 'add item' endpoint(s).
- Frontend: Any UI flow that triggers adding an item (Book Search, Article Add, AI Recommendations).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify a Free User with 19 items can add one more.
- Verify a Free User with 20 items is blocked and sees the prompt.
- Verify a Premium User with 100 items can add one more.
- Verify a lapsed Premium User with 25 items is blocked and sees the prompt.
- Verify tapping the 'Upgrade' button navigates correctly.
- Verify dismissing the modal works as expected.

## 9.3.0.0 Test Data Needs

- A test account with 'Free User' status and exactly 20 library items.
- A test account with 'Free User' status and 19 library items.
- A test account with 'Premium User' status.
- A test account simulating a lapsed Premium user with >20 items.

## 9.4.0.0 Testing Tools

- Backend: Jest for unit/integration tests.
- Frontend: `flutter_test` for widget tests, `integration_test` for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Backend code implementing the limit check is peer-reviewed and merged.
- Frontend code for displaying the modal and handling the API response is peer-reviewed and merged.
- Unit tests for both backend service logic and frontend components achieve >=80% coverage.
- Integration test confirming the API correctly rejects requests over the limit is implemented and passing.
- E2E test simulating the full user flow is implemented and passing.
- UI/UX of the upgrade modal has been reviewed and approved by the design team.
- Story has been deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical part of the monetization strategy.
- It is dependent on the completion of core 'add item' and 'subscription status' stories.

## 11.4.0.0 Release Impact

- Essential for the initial launch to support the freemium business model.

