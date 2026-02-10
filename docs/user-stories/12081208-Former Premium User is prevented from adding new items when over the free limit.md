# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-021 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Former Premium User is prevented from adding new i... |
| As A User Story | As a Former Premium User whose library exceeds the... |
| User Persona | A 'Free User' who has downgraded from a 'Premium' ... |
| Business Value | Enforces the freemium business model by creating a... |
| Functional Area | Library Management & Freemium Model Enforcement |
| Story Theme | Freemium Subscription and Monetization |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User over the limit is blocked from adding a new item

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a 'Free User' with a library containing 25 items from my previous Premium subscription

### 3.1.5 When

I attempt to add a new book from the search results screen

### 3.1.6 Then

the system prevents the book from being added to my library

### 3.1.7 And

a modal dialog is displayed.

### 3.1.8 Validation Notes

Verify that no API call to add the book is successfully completed and the user's library count remains 25.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

The blocking modal contains correct information and actions

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the 'Library Limit Reached' modal is displayed

### 3.2.5 When

I view the modal's content

### 3.2.6 Then

it must contain a clear title like 'Library Limit Reached'

### 3.2.7 And

it must have a dismiss option (e.g., 'Cancel' or 'OK').

### 3.2.8 Validation Notes

Check the UI component for all required text and interactive elements.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User at the exact limit is blocked

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am a 'Free User' with a library containing exactly 20 items

### 3.3.5 When

I attempt to add a 21st item

### 3.3.6 Then

the system prevents the item from being added and displays the 'Library Limit Reached' modal.

### 3.3.7 Validation Notes

This confirms the limit logic is inclusive (<= 20 is allowed for having items, >20 is blocked for adding).

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User navigates to the upgrade screen from the modal

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

the 'Library Limit Reached' modal is displayed

### 3.4.5 When

I tap the 'Upgrade to Premium' button

### 3.4.6 Then

I am navigated to the application's subscription/upgrade screen.

### 3.4.7 Validation Notes

Verify the navigation correctly routes to the screen implemented in US-017/US-018.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User navigates to their library from the modal

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the 'Library Limit Reached' modal is displayed

### 3.5.5 When

I tap the 'Manage Library' button

### 3.5.6 Then

I am navigated to my personal library screen where I can view and delete items.

### 3.5.7 Validation Notes

Verify the navigation correctly routes to the main library view.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User below the limit can add an item

### 3.6.3 Scenario Type

Negative_Case

### 3.6.4 Given

I am a 'Free User' with a library containing 19 items

### 3.6.5 When

I attempt to add a new book

### 3.6.6 Then

the book is successfully added to my library

### 3.6.7 And

the 'Library Limit Reached' modal is not displayed.

### 3.6.8 Validation Notes

Ensures the blocking logic does not trigger incorrectly.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Backend API enforces the limit

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am a 'Free User' with 25 items in my library

### 3.7.5 When

a client-side check is bypassed and an API request is sent to add a new item

### 3.7.6 Then

the backend API must reject the request with an appropriate error status (e.g., 403 Forbidden) and a specific error code (e.g., 'USER_LIMIT_EXCEEDED').

### 3.7.7 Validation Notes

This is a critical security and business rule test. It must be tested by mocking an API call directly.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Modal dialog component
- Primary button ('Upgrade to Premium')
- Secondary button ('Manage Library')
- Dismiss button/icon ('OK', 'Cancel')

## 4.2.0 User Interactions

- The attempt to add an item (e.g., tapping an 'Add to Library' button) is intercepted.
- The modal appears, overlaying the current screen.
- Tapping the primary button navigates to the subscription screen.
- Tapping the secondary button navigates to the library screen.

## 4.3.0 Display Requirements

- The modal must clearly state the reason for the block (limit reached).
- The user's current item count could optionally be displayed for clarity (e.g., 'You have 25/20 items').

## 4.4.0 Accessibility Needs

- The modal must be focus-trapped, meaning keyboard and screen reader focus cannot leave the modal until it is dismissed.
- All buttons must have clear, descriptive labels for screen readers (WCAG 2.1 AA).

# 5.0.0 Business Rules

- {'rule_id': 'BR-FRE-001a', 'rule_description': "A user with the 'Free User' role cannot add a new library item if their current item count is equal to or greater than the maximum limit of 20.", 'enforcement_point': 'Client-side (pre-API call for immediate feedback) and Backend API (authoritative enforcement).', 'violation_handling': 'The add action is blocked. The client displays an informational modal with upgrade and management options. The backend returns a 403 Forbidden error.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-020

#### 6.1.1.2 Dependency Reason

This story requires the system to handle the transition of a user from 'Premium' to 'Free' status, creating the user state this story addresses.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-017

#### 6.1.2.2 Dependency Reason

The 'Upgrade to Premium' button must navigate to the screen showing premium benefits.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-037

#### 6.1.3.2 Dependency Reason

The blocking logic must be integrated into the user flow for adding a new item, such as searching for and adding a book.

## 6.2.0.0 Technical Dependencies

- Backend subscription management service to authoritatively determine user's current tier.
- State management (Riverpod) on the client to have access to the user's tier and library count.

## 6.3.0.0 Data Dependencies

- Accurate real-time count of items in the user's library.
- User's current subscription status (Free/Premium) available in their session data (e.g., JWT).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The client-side check of the user's item count and tier must be instantaneous, using locally cached state.
- The backend API validation must not add more than 50ms to the overall request latency.

## 7.2.0.0 Security

- The user's subscription tier and item count must be validated on the backend for every 'add item' request. The client-side check is for UX only and cannot be the sole point of enforcement.

## 7.3.0.0 Usability

- The message in the modal must be clear, concise, and helpful, not accusatory. It should guide the user toward a solution (upgrade or manage).

## 7.4.0.0 Accessibility

- The modal must adhere to WCAG 2.1 Level AA standards for dialogs.

## 7.5.0.0 Compatibility

- The modal and its behavior must be consistent across all supported iOS and Android versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- A reusable modal component may need to be created or modified.
- Logic must be added to every entry point where a user can add a library item.
- Backend API endpoint for adding items must be updated to include this validation logic.

## 8.3.0.0 Technical Risks

- Potential for inconsistent behavior if the check is not implemented at all 'add item' entry points.
- Risk of race conditions if the user's library count is not handled atomically on the backend.

## 8.4.0.0 Integration Points

- Frontend: Book Search, Add Article, AI Recommendations 'Save to Library' feature.
- Backend: API endpoint(s) responsible for creating new LibraryItem records.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify blocking for a user with 20 items.
- Verify blocking for a user with >20 items.
- Verify successful addition for a user with <20 items.
- Verify no blocking for a Premium user regardless of item count.
- Verify navigation from the modal's buttons.
- Verify backend API rejection with a direct API test.

## 9.3.0.0 Test Data Needs

- Test account: 'Free User', 19 library items.
- Test account: 'Free User', 20 library items.
- Test account: 'Free User', 25 library items (downgraded premium).
- Test account: 'Premium User', 50 library items.

## 9.4.0.0 Testing Tools

- Flutter's `integration_test` package for E2E tests.
- Postman or a similar tool for direct backend API testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend logic, achieving >80% coverage
- Integration testing completed successfully, verifying the client-server interaction
- E2E tests for the primary scenarios are automated and passing
- User interface modal reviewed and approved by UX/UI designer
- Performance requirements verified
- Security requirement for backend validation is implemented and tested
- Documentation for the API endpoint change is updated
- Story deployed and verified in the staging environment using the required test accounts

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core monetization feature and should be prioritized highly once the prerequisite stories are complete.
- Requires coordination between frontend and backend developers to ensure the API contract for the rejection is clear.

## 11.4.0.0 Release Impact

- Critical for the launch of the freemium model. The application cannot be released without this enforcement mechanism in place.

