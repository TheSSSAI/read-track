# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-007 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | New user sets a yearly reading goal during onboard... |
| As A User Story | As a new user going through the initial setup, I w... |
| User Persona | A 'New User' who has just successfully authenticat... |
| Business Value | Increases user activation and engagement from the ... |
| Functional Area | User Onboarding |
| Story Theme | First-Time User Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User sets a valid yearly goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a new user is on the 'Set Yearly Goal' step of the onboarding flow

### 3.1.5 When

the user enters a valid, positive integer (e.g., '50') into the goal input field and taps the 'Set Goal' button

### 3.1.6 Then

the system saves a goal of type 'BOOKS_PER_YEAR' with the target value '50' for the user, and the user is navigated to the next step in the onboarding flow.

### 3.1.7 Validation Notes

Verify a new record is created in the 'Goals' table for the user with the correct values. The UI should transition smoothly to the next screen.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Alternative Flow: User skips setting a goal

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

a new user is on the 'Set Yearly Goal' step of the onboarding flow

### 3.2.5 When

the user taps the 'Skip for now' button or link

### 3.2.6 Then

the system does not save any goal for the user, and the user is navigated to the next step in the onboarding flow.

### 3.2.7 Validation Notes

Verify no record is created in the 'Goals' table for the user. The UI should transition smoothly.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: User enters an invalid number

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a new user is on the 'Set Yearly Goal' step of the onboarding flow

### 3.3.5 When

the user enters an invalid value (e.g., '0', '-10', 'abc', '12.5') and taps the 'Set Goal' button

### 3.3.6 Then

the system displays an inline validation error message (e.g., 'Please enter a number greater than 0'), the goal is not saved, and the user remains on the current screen.

### 3.3.7 Validation Notes

Test with zero, negative numbers, decimals, and non-numeric text. The 'Set Goal' button might be disabled until valid input is provided.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: User submits without entering a value

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a new user is on the 'Set Yearly Goal' step of the onboarding flow

### 3.4.5 When

the user has not entered any value in the input field and taps the 'Set Goal' button

### 3.4.6 Then

the system displays an inline validation error message (e.g., 'Please enter a goal'), and the user remains on the current screen.

### 3.4.7 Validation Notes

The 'Set Goal' button should ideally be disabled if the input field is empty.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Network failure on goal submission

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

a new user is on the 'Set Yearly Goal' step of the onboarding flow

### 3.5.5 When

the user enters a valid goal and taps 'Set Goal', but the API call to the backend fails

### 3.5.6 Then

the system displays a non-blocking, user-friendly error message (e.g., a toast or snackbar stating 'Could not save goal. Please check your connection and try again.'), and the user remains on the current screen with the ability to retry.

### 3.5.7 Validation Notes

Simulate a network error or a 5xx server response. The UI should show a loading indicator during the API call and hide it upon failure.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clear heading, e.g., 'What's your yearly reading goal?'
- A numeric-only input field for the goal value.
- A primary button labeled 'Set Goal' or 'Continue'.
- A secondary, less prominent button or text link labeled 'Skip for now'.

## 4.2.0 User Interactions

- Tapping into the input field should bring up a numeric keyboard.
- The 'Set Goal' button should show a loading state while the API call is in progress.
- Input validation should appear in real-time or upon submission attempt.

## 4.3.0 Display Requirements

- The screen must clearly indicate the goal is for the number of 'books' per 'year'.
- Any validation or error messages must be clear and easy to understand.

## 4.4.0 Accessibility Needs

- All interactive elements (input field, buttons) must have proper labels for screen readers (e.g., TalkBack, VoiceOver).
- Text and background colors must meet WCAG 2.1 AA contrast ratios.
- The UI must support dynamic type scaling according to OS-level font size settings.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The yearly reading goal target must be a positive integer greater than zero.

### 5.1.3 Enforcement Point

Client-side validation and Backend API validation.

### 5.1.4 Violation Handling

Client displays an inline error message. Backend returns a 400 Bad Request response with a clear error code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A new user can only have one active goal of type 'BOOKS_PER_YEAR' created during onboarding.

### 5.2.3 Enforcement Point

Backend API logic.

### 5.2.4 Violation Handling

If an attempt is made to create a duplicate, the backend should handle it gracefully, possibly by overwriting or returning an error, though this is unlikely in the onboarding flow.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

User must be able to sign up with Google to enter the onboarding flow.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-003

#### 6.1.2.2 Dependency Reason

User must be able to sign up with Apple to enter the onboarding flow.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-006

#### 6.1.3.2 Dependency Reason

This story implements a specific step within the overall onboarding flow defined in US-006.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint must exist to create a goal for a user (e.g., POST /api/v1/goals).
- The 'Goal' data model must be defined in the database schema.
- The Flutter application must have a navigation framework for the multi-step onboarding process.

## 6.3.0.0 Data Dependencies

- Requires a valid, authenticated user session (JWT) to associate the goal with the correct user.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for saving the goal must be < 200ms (P95) as per REQ-PER-001.
- The UI transition to the next onboarding screen must be smooth and complete in under 500ms.

## 7.2.0.0 Security

- The API endpoint for creating a goal must be protected and require a valid JWT.
- All input from the client must be sanitized and validated on the backend to prevent injection attacks (NFR-SEC-006).

## 7.3.0.0 Usability

- The purpose of the screen must be immediately obvious to the user.
- The process of setting or skipping the goal should require minimal effort.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- The UI must render correctly on all supported iOS and Android versions and screen sizes (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The UI is a simple form with one input and two buttons.
- The backend logic is a standard create operation on a single database table.
- Integration into the existing onboarding flow is the main task.

## 8.3.0.0 Technical Risks

- Ensuring robust error handling for network failures to prevent the user from getting stuck in the onboarding flow.

## 8.4.0.0 Integration Points

- Client (Flutter) integrates with the Backend API's goal creation endpoint.
- The saved goal data will be read by the Dashboard (REQ-DSH-001) and Goal Management (REQ-GOL-001) features later.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify successful goal creation.
- Verify successful skipping of the step.
- Test all invalid input cases (0, negative, text, decimal).
- Test UI behavior during network failure and recovery.
- Verify the UI on various screen sizes and in both light/dark modes.

## 9.3.0.0 Test Data Needs

- A newly created user account that has not completed onboarding.

## 9.4.0.0 Testing Tools

- flutter_test for unit/widget tests.
- integration_test package for E2E tests.
- Jest for backend unit/integration tests.
- Postman or similar for manual API endpoint testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and widget tests implemented with >80% code coverage
- Backend integration tests completed successfully
- E2E test for the onboarding goal-setting flow is passing
- User interface reviewed and approved by UX/Product
- Performance requirements (API latency) verified
- Accessibility checks (screen reader, dynamic type) performed
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core part of the initial user experience and should be prioritized early in the development cycle.
- Must be completed after authentication stories (US-002, US-003) are done.

## 11.4.0.0 Release Impact

This feature is required for the initial v1.0 release as it is part of the mandatory onboarding flow.

