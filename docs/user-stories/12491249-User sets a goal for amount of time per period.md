# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-062 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User sets a goal for amount of time per period |
| As A User Story | As a habit-focused reader, I want to set a reading... |
| User Persona | Any registered user (Free or Premium) who wants to... |
| Business Value | Increases user engagement and retention by offerin... |
| Functional Area | Goal Management |
| Story Theme | Flexible Goal Setting |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Premium User successfully creates a daily time-based goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Premium User on the 'Create Goal' screen

### 3.1.5 When

I select the 'Amount of time' goal type, enter '30' in the time field, select 'minutes' as the unit, select 'per day' as the period, and confirm

### 3.1.6 Then

the system saves the new goal and I am navigated to the goals screen where the new 'Read 30 minutes per day' goal is displayed with zero progress.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Free User successfully creates their first and only time-based goal

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a logged-in Free User with no active goals

### 3.2.5 When

I create a new goal to read '2' hours 'per week'

### 3.2.6 Then

the goal is created successfully and displayed on my dashboard.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Free User is blocked from creating a second goal

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in Free User and I already have one active goal

### 3.3.5 When

I attempt to create a new goal

### 3.3.6 Then

the system prevents me from creating the goal and displays a non-intrusive prompt to upgrade to the Premium tier, as defined in REQ-FRE-001.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User enters an invalid (non-numeric) value for the time target

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the 'Create Goal' screen for a time-based goal

### 3.4.5 When

I enter 'thirty' in the time field and attempt to save

### 3.4.6 Then

the system displays an inline validation error message, 'Please enter a valid number', and the goal is not created.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User enters a zero or negative value for the time target

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am on the 'Create Goal' screen for a time-based goal

### 3.5.5 When

I enter '0' in the time field and attempt to save

### 3.5.6 Then

the system displays an inline validation error message, 'Please enter a value greater than 0', and the goal is not created.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User attempts to save a goal without selecting a period

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am on the 'Create Goal' screen for a time-based goal and have entered a valid time value

### 3.6.5 When

I attempt to save without selecting a period (day, week, or month)

### 3.6.6 Then

the system highlights the period selection as a required field and prevents the goal from being saved.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Goal creation fails due to a network error

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am creating a time-based goal and my device is offline

### 3.7.5 When

I tap the 'Set Goal' button

### 3.7.6 Then

the system displays a user-friendly error message, such as 'No internet connection. Please try again later.', and the goal is not created.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A selectable option for 'Amount of time' on the goal type selection screen.
- A numeric input field for the target time value.
- A selector (e.g., dropdown or segmented control) for time units ('minutes', 'hours').
- A selector (e.g., segmented control or radio buttons) for the period ('per day', 'per week', 'per month').
- A primary action button to 'Set Goal'.
- Inline text areas for validation error messages.

## 4.2.0 User Interactions

- Selecting the 'Amount of time' goal type reveals the time and period input fields.
- The system should provide immediate feedback on input validation errors before submission is attempted.
- Upon successful creation, the user should be navigated back to their main goals list or dashboard, where the new goal is visible.

## 4.3.0 Display Requirements

- The created goal must be displayed clearly on the user's dashboard and/or goals screen, showing the target time and period (e.g., 'Read 90 minutes per week').

## 4.4.0 Accessibility Needs

- All input fields, selectors, and buttons must have ARIA labels for screen reader compatibility.
- UI must adhere to WCAG 2.1 Level AA standards for color contrast and touch target size, as per REQ-UIF-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-GOL-001

### 5.1.2 Rule Description

Free Users are limited to one active goal at a time.

### 5.1.3 Enforcement Point

Backend API, before creating a new goal for a user.

### 5.1.4 Violation Handling

The API will reject the request with an appropriate error code (e.g., 403 Forbidden). The client will display an upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-GOL-002

### 5.2.2 Rule Description

Goal target value for time must be a positive integer.

### 5.2.3 Enforcement Point

Client-side form validation and Backend API validation.

### 5.2.4 Violation Handling

Display an error message to the user and prevent goal creation.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to access the goal-setting feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-014

#### 6.1.2.2 Dependency Reason

Defines the core business rule limiting Free Users to one goal, which must be enforced by this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-057

#### 6.1.3.2 Dependency Reason

A mechanism to display goal progress on the dashboard must exist for this story's outcome to be visible to the user.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-048

#### 6.1.4.2 Dependency Reason

The ability to log reading time is required to track progress against the goal created in this story.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., POST /api/v1/goals) to create and persist goal data.
- The `Goal` entity in the database schema must support attributes for `type`, `target_value`, `unit`, and `period`.

## 6.3.0.0 Data Dependencies

- Access to the user's current subscription status (Free/Premium) to enforce business rules.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to create a goal must have a P95 latency of less than 200ms as per REQ-PER-001.

## 7.2.0.0 Security

- The API endpoint for creating goals must be protected and require a valid JWT from an authenticated user.
- All input from the user must be sanitized on the backend to prevent injection attacks (NFR-SEC-006).

## 7.3.0.0 Usability

- The process of setting a time-based goal should be intuitive and require minimal steps.
- Error messages must be clear, user-friendly, and guide the user to correct the issue.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards (REQ-UIF-001).

## 7.5.0.0 Compatibility

- The UI must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires standard UI form development in Flutter.
- Requires a straightforward CRUD endpoint on the backend.
- The primary logic involves checking the user's subscription tier, which should be readily available in the user's session data or JWT.

## 8.3.0.0 Technical Risks

- Ensuring the logic for checking the Free User goal limit is robust and handles edge cases where a user might try to create multiple goals simultaneously.

## 8.4.0.0 Integration Points

- Frontend UI with the backend Goal service API.
- Backend Goal service with the User/Subscription service to verify user tier.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify a Premium user can create a time-based goal.
- Verify a Free user with no goals can create one time-based goal.
- Verify a Free user with an existing goal is blocked from creating another and sees an upgrade prompt.
- Test all input validation rules (zero, negative, non-numeric values).
- Test the flow on both iOS and Android devices.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' tiers.
- A 'Free User' account that already has one active goal.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test` for unit/widget tests.
- Backend: `Jest` for unit/integration tests.
- E2E: `integration_test` package for Flutter.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for form validation and backend business logic, achieving >= 80% coverage
- Integration testing between frontend and backend for goal creation is completed successfully
- User interface reviewed and approved by the design team
- The one-goal limit for Free Users is verified in the staging environment
- Documentation for the new API endpoint is generated and published (OpenAPI)
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature for user engagement and should be prioritized early in the development cycle.
- Depends on the completion of the basic user authentication and subscription status management stories.

## 11.4.0.0 Release Impact

- This feature is a key part of the 'Goal Management' epic and is essential for the initial product launch.

