# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-014 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User is prevented from creating more than one... |
| As A User Story | As a Free User who has already set one active goal... |
| User Persona | Free User (An authenticated user who is not on a p... |
| Business Value | Enforces the freemium business model by limiting c... |
| Functional Area | Goal Management & Monetization |
| Story Theme | Freemium Feature Gating |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Free User with one active goal attempts to create a new goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is logged in as a 'Free User' and has exactly one active goal

### 3.1.5 When

the user interacts with the UI element to create a new goal (e.g., taps an 'Add Goal' button)

### 3.1.6 Then

the system must prevent navigation to the goal creation screen

### 3.1.7 And

the prompt must contain a dismiss option (e.g., 'Cancel' or 'Maybe Later')

### 3.1.8 Validation Notes

Verify both the UI behavior on the client and that no API call to create a goal is successfully processed by the backend.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Free User with one active goal is directed to the upgrade screen from the limit prompt

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

the user is a 'Free User' with one active goal

### 3.2.5 And

the upgrade limitation prompt is displayed

### 3.2.6 When

the user taps the 'Upgrade' call-to-action button

### 3.2.7 Then

the application navigates to the Premium subscription screen.

### 3.2.8 Validation Notes

Verify the navigation flow is correct and the subscription screen is displayed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Free User with zero active goals attempts to create a goal

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is logged in as a 'Free User' and has zero active goals

### 3.3.5 When

the user interacts with the UI element to create a new goal

### 3.3.6 Then

the system must allow the user to proceed to the goal creation screen without any limitation prompts.

### 3.3.7 Validation Notes

Verify that a free user can create their first goal successfully.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Premium User with one or more active goals attempts to create a new goal

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

the user is logged in as a 'Premium User' and has one or more active goals

### 3.4.5 When

the user interacts with the UI element to create a new goal

### 3.4.6 Then

the system must allow the user to proceed to the goal creation screen without any limitation prompts.

### 3.4.7 Validation Notes

Verify that premium users are not subject to this limitation.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Backend rejects attempt from a Free User to create a second goal

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

the user is a 'Free User' with one active goal

### 3.5.5 When

a client attempts to make a direct API call to the backend endpoint for creating a new goal

### 3.5.6 Then

the backend must reject the request with an appropriate HTTP status code (e.g., 403 Forbidden or 402 Payment Required)

### 3.5.7 And

no new goal record is created in the database for that user.

### 3.5.8 Validation Notes

This is a critical security and business rule check. Test via API testing tools like Postman or an integration test.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Downgraded user with multiple goals is prevented from adding more

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a user was previously 'Premium' and has two or more active goals

### 3.6.5 And

their subscription has expired, and their role is now 'Free User'

### 3.6.6 When

the user interacts with the UI element to create a new goal

### 3.6.7 Then

the system must prevent the action and display the upgrade limitation prompt, as per AC-001.

### 3.6.8 Validation Notes

Verify that existing data is preserved but new additions are blocked, as per REQ-BUS-001.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A UI element (e.g., button, FAB) for initiating goal creation.
- A reusable modal dialog component for displaying the upgrade prompt.
- The prompt must include: an informative title, descriptive text about the one-goal limit, an 'Upgrade' button, and a 'Cancel' or dismiss button/icon.

## 4.2.0 User Interactions

- Tapping the goal creation element triggers the limit check.
- The UI element for goal creation may be visually disabled (greyed out) if the limit is reached, providing an immediate visual cue.
- The upgrade prompt modal must trap focus for accessibility and be dismissible via its dedicated button or an escape key press where applicable.

## 4.3.0 Display Requirements

- The number of active goals must be readily available to the UI to determine the state of the creation button/action.

## 4.4.0 Accessibility Needs

- If the creation button is disabled, its state must be announced by screen readers (e.g., 'Add Goal, button, disabled').
- The upgrade prompt modal must adhere to WCAG 2.1 AA standards for dialogs, ensuring it is properly announced and keyboard navigable.

# 5.0.0 Business Rules

- {'rule_id': 'BR-FRE-002', 'rule_description': "Users with the 'Free User' role are limited to a maximum of one active goal at any given time.", 'enforcement_point': 'Client-side (UI logic) and Server-side (API endpoint for goal creation).', 'violation_handling': 'The client displays an upgrade prompt. The server rejects the request with a 4xx error code.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

The core functionality to create a goal must exist before a limit can be placed on it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-016

#### 6.1.2.2 Dependency Reason

The system must be able to identify the user's current subscription tier (Free/Premium) to apply the rule.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-018

#### 6.1.3.2 Dependency Reason

The upgrade prompt requires a destination screen to navigate to when the user wants to upgrade.

## 6.2.0.0 Technical Dependencies

- User authentication service (Auth0) that provides the user's role/tier in the JWT.
- Client-side state management (Riverpod) to hold user tier and goal count.
- Backend API endpoint for creating goals.

## 6.3.0.0 Data Dependencies

- Access to the user's current subscription status.
- A queryable count of the user's active goals.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The client-side check for the user's tier and goal count must be instantaneous and not require a network call, using cached state data to prevent UI lag.

## 7.2.0.0 Security

- The business rule must be enforced on the backend API to prevent malicious users from bypassing client-side restrictions.

## 7.3.0.0 Usability

- The upgrade prompt must be clear, concise, and non-disruptive, with an obvious path to both upgrade and dismiss.

## 7.4.0.0 Accessibility

- All UI elements, including disabled states and modal dialogs, must be compliant with WCAG 2.1 Level AA.

## 7.5.0.0 Compatibility

- The feature must function identically on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires coordination between frontend and backend.
- Frontend: Conditional UI logic based on user state.
- Backend: Adding a validation middleware or service-level check to the goal creation endpoint.

## 8.3.0.0 Technical Risks

- Potential for inconsistent state between client and server if state synchronization is not handled correctly after a subscription change.

## 8.4.0.0 Integration Points

- User Profile/Subscription Service: To fetch the user's current tier.
- Goal Service (Backend): To check the current goal count and create a new one.
- Goal Management UI (Frontend): To display the button and the prompt.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify a Free User with 1 goal sees the prompt.
- Verify a Free User with 0 goals can create one.
- Verify a Premium User can create multiple goals.
- Verify the backend API correctly rejects invalid requests.
- Verify a downgraded user with >1 goal is blocked from adding more.

## 9.3.0.0 Test Data Needs

- Test accounts for 'Free User' with 0 goals.
- Test accounts for 'Free User' with 1 goal.
- Test accounts for 'Premium User' with multiple goals.
- Test accounts for a downgraded 'Free User' with multiple goals.

## 9.4.0.0 Testing Tools

- Flutter Test (`flutter_test`, `integration_test`) for frontend testing.
- Jest for backend unit/integration tests.
- Postman or similar for direct API testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for both frontend and backend logic, achieving >80% coverage
- Integration testing completed successfully, verifying the client-server interaction
- User interface reviewed and approved by UX/Product
- Backend API security rule is confirmed to be effective
- Accessibility requirements for the prompt and disabled states are validated
- Documentation for the API endpoint is updated to reflect the business rule
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core monetization feature and should be prioritized high. Ensure prerequisite stories for goal creation and subscription status are completed first.

## 11.4.0.0 Release Impact

- Essential for the initial release to establish the freemium model.

