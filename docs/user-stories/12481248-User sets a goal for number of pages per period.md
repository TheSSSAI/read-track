# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-061 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User sets a goal for number of pages per period |
| As A User Story | As a motivated reader, I want to set a goal to rea... |
| User Persona | Any authenticated user (Free or Premium) looking t... |
| Business Value | Increases user engagement and habit formation by p... |
| Functional Area | Goal Management |
| Story Theme | Flexible Goal Setting and Monitoring |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User successfully creates a daily page goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user with permission to create a new goal (e.g., a Premium user, or a Free user with no active goals)

### 3.1.5 When

I navigate to the goal creation screen, select the 'Pages' goal type, enter '50' as the target value, select 'per Day' as the period, and tap 'Save Goal'

### 3.1.6 Then

the system creates a new active goal for me with a target of 50 pages per day, and I am navigated to the Goals screen where my new goal is displayed.

### 3.1.7 Validation Notes

Verify a new record is created in the 'Goals' table for the user with type='pages', target=50, and period='day'. The UI should reflect this new goal.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User successfully creates a weekly page goal

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an authenticated user with permission to create a new goal

### 3.2.5 When

I navigate to the goal creation screen, select the 'Pages' goal type, enter '250' as the target value, select 'per Week' as the period, and tap 'Save Goal'

### 3.2.6 Then

the system creates a new active goal for me with a target of 250 pages per week.

### 3.2.7 Validation Notes

Verify a new record is created in the 'Goals' table for the user with type='pages', target=250, and period='week'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User successfully creates a monthly page goal

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am an authenticated user with permission to create a new goal

### 3.3.5 When

I navigate to the goal creation screen, select the 'Pages' goal type, enter '1000' as the target value, select 'per Month' as the period, and tap 'Save Goal'

### 3.3.6 Then

the system creates a new active goal for me with a target of 1000 pages per month.

### 3.3.7 Validation Notes

Verify a new record is created in the 'Goals' table for the user with type='pages', target=1000, and period='month'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User attempts to create a goal with an invalid page count

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the goal creation screen for a 'Pages' goal

### 3.4.5 When

I enter '0', a negative number, or a non-numeric value in the target value field

### 3.4.6 Then

the 'Save Goal' button is disabled and a validation message 'Please enter a number greater than 0' is displayed.

### 3.4.7 Validation Notes

Test with input values like 0, -10, and an empty field. The client-side validation should prevent the API call.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Free User is prevented from creating a second goal

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a 'Free User' and I already have one active goal

### 3.5.5 When

I navigate to the goal creation screen and attempt to save a new goal

### 3.5.6 Then

the system prevents the action and displays a non-intrusive prompt to upgrade to the Premium plan.

### 3.5.7 Validation Notes

This requires a backend check against the user's subscription tier and current active goal count before creating the new goal. The API should return a specific error code (e.g., 403 Forbidden with a payload indicating the reason) that the client can use to trigger the upgrade prompt.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User cancels the goal creation process

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am on the goal creation screen and have entered some data

### 3.6.5 When

I tap the 'Cancel' or 'Back' button

### 3.6.6 Then

no goal is created, and I am returned to the previous screen without saving any changes.

### 3.6.7 Validation Notes

Verify that no API call to create a goal is made and the user's active goals remain unchanged.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A screen or modal for goal creation.
- Radio buttons or a segmented control to select the goal period: 'Day', 'Week', 'Month'.
- A numeric input field for the target page count.
- A primary 'Save Goal' button.
- A 'Cancel' or 'Back' button/icon.

## 4.2.0 User Interactions

- Selecting a period updates the UI to reflect the choice.
- The numeric input field should only accept positive integers.
- The 'Save Goal' button is disabled until a valid target value is entered.

## 4.3.0 Display Requirements

- The screen must clearly be titled 'Set a New Goal' or similar.
- Input validation messages must be clear and displayed near the relevant input field.

## 4.4.0 Accessibility Needs

- All form elements must have proper labels for screen readers.
- Tap targets for buttons and controls must meet minimum size requirements.
- Validation messages must be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-GOL-001

### 5.1.2 Rule Description

A Free User can have a maximum of one active goal at any time.

### 5.1.3 Enforcement Point

Backend API, before creating a new goal record.

### 5.1.4 Violation Handling

The API will reject the request with an error indicating the limit has been reached. The client will display an upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-GOL-002

### 5.2.2 Rule Description

The target value for a page goal must be a positive integer greater than zero.

### 5.2.3 Enforcement Point

Client-side for immediate feedback, and server-side for data integrity.

### 5.2.4 Violation Handling

Client-side: Disable save button and show a validation error. Server-side: Reject the request with a 400 Bad Request status.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to have an authenticated session to which a goal can be attached.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-016

#### 6.1.2.2 Dependency Reason

The system must be able to determine the user's subscription tier (Free/Premium) to enforce goal limits.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-063

#### 6.1.3.2 Dependency Reason

A UI component must exist to display the newly created goal's progress to the user.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for creating goals (e.g., POST /api/v1/goals).
- User profile service to fetch subscription status.
- Database schema for the 'Goal' entity as defined in REQ-TRK-001.

## 6.3.0.0 Data Dependencies

- Authenticated user's ID.
- User's current subscription status.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to save the goal must have a P95 latency of less than 200ms as per REQ-PER-001.

## 7.2.0.0 Security

- The API endpoint for creating goals must be protected and only accessible by authenticated users.
- Input from the user (page count) must be sanitized on the backend to prevent injection attacks.

## 7.3.0.0 Usability

- The goal creation process should be intuitive and require minimal steps.
- Error messages should be user-friendly and clearly explain the issue.

## 7.4.0.0 Accessibility

- The goal creation form must be fully navigable and operable using accessibility services (e.g., TalkBack, VoiceOver).

## 7.5.0.0 Compatibility

- The UI must render correctly on all supported iOS and Android screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend UI/form logic and backend API development.
- Backend logic involves a database write conditioned on the user's subscription status and existing data (active goals).
- Coordination between frontend and backend on API contract and error handling is crucial.

## 8.3.0.0 Technical Risks

- Potential for race conditions if a user attempts to create multiple goals simultaneously; the backend logic must handle this atomically.
- Incorrectly handling the free user limit could lead to a poor user experience or bypass of the paywall.

## 8.4.0.0 Integration Points

- User Authentication Service (to get user ID).
- Subscription Management Service (to get user tier).
- Primary Database (to persist the goal).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Create a daily, weekly, and monthly page goal as a Premium User.
- Create a page goal as a Free User with zero existing goals.
- Attempt to create a page goal as a Free User with one existing goal; verify the upgrade prompt appears.
- Test input validation for zero, negative, and non-numeric page counts.
- Verify that after creating a goal, it appears correctly on the dashboard/goals screen.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' tiers.
- A 'Free User' account that already has one active goal.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test` for unit/widget tests.
- Backend: `Jest` for unit/integration tests.
- E2E: `integration_test` package.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing between frontend and backend completed successfully
- User interface reviewed and approved by UX/Product
- Performance requirements verified (API latency < 200ms)
- Security requirements validated (endpoint secured, input sanitized)
- Documentation for the new API endpoint is created/updated in OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story can be developed in parallel with other goal creation stories (US-060, US-062) as they share significant UI and backend logic.
- Ensure prerequisite stories for authentication and subscription status are completed in a prior sprint.

## 11.4.0.0 Release Impact

This is a core feature for user engagement and part of the initial feature set for launch.

