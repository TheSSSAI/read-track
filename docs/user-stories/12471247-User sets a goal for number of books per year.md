# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-060 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User sets a goal for number of books per year |
| As A User Story | As a registered user, I want to set a yearly goal ... |
| User Persona | Any registered user (Free or Premium) who does not... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Goal Management |
| Story Theme | Goal Setting and Monitoring |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully setting a yearly book goal as a new user

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a logged-in user is on the 'Goals' screen and has no other active goals

### 3.1.5 When

the user selects 'Create Goal', chooses the 'Number of books per year' type, enters a valid positive integer (e.g., '52'), and confirms

### 3.1.6 Then

the system creates a new goal for the current calendar year with a target of 52 books associated with the user's account

### 3.1.7 Validation Notes

Verify a new record exists in the 'Goals' table for the user. The UI should navigate to the main goals screen or dashboard and display the newly created goal with progress '0/52'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempting to set a goal with an invalid number (zero)

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

a user is on the goal creation screen for a yearly book goal

### 3.2.5 When

the user enters '0' as the target number and attempts to save

### 3.2.6 Then

the system displays an inline validation message, such as 'Goal must be at least 1 book', and the 'Save' button remains disabled

### 3.2.7 Validation Notes

Check that no API call is made and no goal is created in the database. The UI must show the error message.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to set a goal with non-numeric or negative input

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a user is on the goal creation screen for a yearly book goal

### 3.3.5 When

the user enters non-numeric text (e.g., 'abc') or a negative number (e.g., '-10')

### 3.3.6 Then

the system displays an inline validation message, such as 'Please enter a valid number', and the 'Save' button is disabled

### 3.3.7 Validation Notes

The input field should prevent or invalidate non-integer values. No goal should be created.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Free User with an existing active goal attempts to create another goal

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a 'Free User' is logged in and already has one active goal

### 3.4.5 When

the user navigates to the 'Goals' screen

### 3.4.6 Then

the option to create a new goal is disabled or, if accessible, triggers an upgrade prompt upon interaction

### 3.4.7 Validation Notes

Verify the UI element for creating a goal is visually disabled. If it's not disabled, verify that tapping it shows the upgrade screen and does not proceed to the goal creation flow.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User attempts to create a duplicate 'books per year' goal

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user already has an active 'Number of books per year' goal for the current calendar year

### 3.5.5 When

the user attempts to create another goal of the same type ('Number of books per year')

### 3.5.6 Then

the system prevents the creation and displays a message like 'You already have a yearly book goal. Would you like to edit it?'

### 3.5.7 Validation Notes

The backend API should reject the request with a specific error code (e.g., 409 Conflict). The frontend should handle this error gracefully by showing the informative message.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User cancels the goal creation process

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

a user has initiated the goal creation flow and entered a number

### 3.6.5 When

the user taps the 'Cancel' or 'Back' button before confirming

### 3.6.6 Then

the goal creation UI is dismissed, and no goal is saved

### 3.6.7 Validation Notes

Verify that no new goal record is created in the database for the user.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A button or link to 'Create a New Goal'.
- A screen to select the goal type (e.g., Books, Pages, Time).
- A numeric input field for the target number of books.
- A 'Save Goal' or 'Confirm' button.
- A 'Cancel' or 'Back' button/icon.
- Inline validation text for the input field.

## 4.2.0 User Interactions

- The numeric input field should trigger a numeric keyboard on mobile devices.
- The 'Save Goal' button should be disabled by default and only become enabled when a valid, positive integer is entered.
- Upon successful creation, a confirmation toast or snackbar should appear briefly.

## 4.3.0 Display Requirements

- The goal creation screen must clearly state the goal type being created (e.g., 'Set Your Yearly Book Goal').

## 4.4.0 Accessibility Needs

- The input field must have a proper label for screen readers.
- Validation error messages must be programmatically associated with the input field.
- All interactive elements must have sufficient color contrast and touch target size.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-GOL-001

### 5.1.2 Rule Description

A user can only have one active goal of a specific type and period at a time (e.g., only one 'books per year' goal for 2025).

### 5.1.3 Enforcement Point

Backend API during the goal creation request.

### 5.1.4 Violation Handling

The API should return a 409 Conflict error. The client should display a user-friendly message prompting to edit the existing goal.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-FRE-002

### 5.2.2 Rule Description

A 'Free User' is limited to a maximum of one active goal at any time.

### 5.2.3 Enforcement Point

Both Frontend (disabling UI elements) and Backend (validating the request).

### 5.2.4 Violation Handling

The UI should prevent the user from starting the creation flow. The API should reject the request with a 403 Forbidden error if a Free User with an active goal attempts to create another.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

User must be able to sign up to have an account to associate the goal with.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-004

#### 6.1.2.2 Dependency Reason

User must be able to log in to access the goal-setting feature.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-014

#### 6.1.3.2 Dependency Reason

The logic to prevent a Free User from creating more than one goal is defined in US-014 and must be implemented for this story to function correctly.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-057

#### 6.1.4.2 Dependency Reason

The dashboard needs to be able to display goal progress, which requires a goal to be set via this story.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint (`POST /api/v1/goals`) for creating goals.
- Database schema for the `Goal` entity as defined in REQ-TRK-001.
- User authentication service to identify the current user and their subscription tier.

## 6.3.0.0 Data Dependencies

- Access to the user's subscription status ('Free' or 'Premium').
- Access to the user's list of current active goals.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for creating a goal must be under 200ms (P95), as per REQ-PER-001.

## 7.2.0.0 Security

- The API endpoint for creating a goal must be protected and require a valid JWT.
- Input from the user (the target number) must be sanitized and validated on the backend to prevent injection attacks.

## 7.3.0.0 Usability

- The process of setting a goal should be simple and require minimal steps.
- Error messages should be clear and guide the user on how to correct the input.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards, as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Standard CRUD operation with business logic.
- Requires a new UI screen/modal with a simple form.
- Backend logic involves checking user tier and existing goals, which adds a small amount of complexity but is straightforward.

## 8.3.0.0 Technical Risks

- Ensuring the definition of 'current year' is consistent across the client and server, especially concerning time zones. The standard should be to use UTC for the calendar year.

## 8.4.0.0 Integration Points

- User's profile service (to get subscription tier).
- Database `Goals` table.
- Frontend Dashboard and Goals list UI components (to display the new goal).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test goal creation for a new Free User.
- Test goal creation for a Premium User.
- Test that a Free User with an existing goal cannot access the creation flow.
- Test all invalid input scenarios (0, negative, non-numeric).
- Test the duplicate goal prevention logic.
- Test that progress on the goal updates correctly after a book is marked as 'Read' (covered in another story, but integration point should be checked).

## 9.3.0.0 Test Data Needs

- Test accounts for 'Free User' with no goals.
- Test accounts for 'Free User' with one active goal.
- Test accounts for 'Premium User' with and without an existing yearly book goal.

## 9.4.0.0 Testing Tools

- Backend: Jest
- Frontend: flutter_test, integration_test

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing completed successfully
- User interface reviewed and approved by UX/Product
- Performance requirements verified
- Security requirements validated
- Backend API documentation (OpenAPI) is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the 'Goal Management' epic and a prerequisite for goal tracking and dashboard features. It should be prioritized early in the project.
- Backend API endpoint should be developed in parallel or before the frontend UI.

## 11.4.0.0 Release Impact

- This feature is a core component of the initial MVP release.

