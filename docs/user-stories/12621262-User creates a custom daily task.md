# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-075 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User creates a custom daily task |
| As A User Story | As a goal-oriented reader, I want to create my own... |
| User Persona | Any authenticated user (Free or Premium) who wants... |
| Business Value | Increases daily user engagement and long-term rete... |
| Functional Area | Daily Tasks |
| Story Theme | Goal Setting and Habit Formation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully create a new daily task

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user viewing the 'Daily Tasks' screen

### 3.1.5 When

I tap the 'Add New Task' button, enter a valid description (e.g., 'Read one chapter'), and tap 'Save'

### 3.1.6 Then

a request is sent to the backend to create the task, the new task is persisted and associated with my user account, and the task creation UI is dismissed, and the new task appears in my list of daily tasks.

### 3.1.7 Validation Notes

Verify the new task is visible on the UI. Check the backend database to confirm the new record exists and is correctly linked to the user ID. Verify the local Isar database is updated with the new task.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to save a task with an empty description

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am on the 'Create Task' screen

### 3.2.5 When

I attempt to save the task without entering any text in the description field

### 3.2.6 Then

the 'Save' button is disabled, or if enabled, a validation message 'Task description cannot be empty' is displayed, and no API call is made to create the task.

### 3.2.7 Validation Notes

Confirm the UI prevents saving an empty task and provides clear user feedback. Monitor network traffic to ensure no API call is triggered.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempt to save a task with a description exceeding the character limit

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am on the 'Create Task' screen

### 3.3.5 When

I enter a description longer than the 100-character limit

### 3.3.6 Then

the UI prevents me from entering more characters and/or disables the 'Save' button, and a character counter (e.g., '101/100') is displayed to provide feedback.

### 3.3.7 Validation Notes

Test the input field to ensure the character limit is strictly enforced.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Cancel the task creation process

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am on the 'Create Task' screen and have entered text into the description field

### 3.4.5 When

I tap the 'Cancel' button or use the system back gesture

### 3.4.6 Then

a confirmation dialog appears asking 'Discard changes?'. If I confirm, the screen is dismissed, and no task is created. If I cancel the dialog, I remain on the 'Create Task' screen with my entered text intact.

### 3.4.7 Validation Notes

Verify the confirmation dialog appears only when there are unsaved changes. Verify both confirmation and cancellation actions in the dialog work as expected.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Network error during task creation

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I have entered a valid task description and my device is offline

### 3.5.5 When

I tap the 'Save' button

### 3.5.6 Then

the app displays a non-blocking error message (e.g., 'No internet connection. Please try again.'), the task is not created, and the text I entered remains in the input field.

### 3.5.7 Validation Notes

Use device settings to disable network connectivity and attempt to save a task. Verify the error message and that the form state is preserved.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An entry point on the 'Tasks' screen, such as a Floating Action Button (FAB) with a '+' icon.
- A modal or new screen for task creation.
- A single-line text input field for the task description with a clear label.
- A character counter displayed near the text input.
- A primary 'Save' button.
- A 'Cancel' or 'Close' (X) button to dismiss the UI.
- A confirmation dialog for discarding unsaved changes.

## 4.2.0 User Interactions

- Tapping the entry point opens the creation UI.
- The 'Save' button should be disabled until a valid description is entered.
- Tapping 'Cancel' or navigating back with unsaved changes triggers a confirmation dialog.

## 4.3.0 Display Requirements

- The newly created task must immediately appear in the user's task list upon successful creation.
- Validation errors must be displayed clearly to the user.

## 4.4.0 Accessibility Needs

- All buttons and input fields must have content labels for screen readers.
- The UI must adhere to WCAG 2.1 Level AA contrast ratios and support dynamic type scaling as per REQ-UIF-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-TSK-001

### 5.1.2 Rule Description

A task description is mandatory and cannot be empty.

### 5.1.3 Enforcement Point

Client-side validation before enabling the 'Save' button and server-side validation upon API request.

### 5.1.4 Violation Handling

Client: Display a validation error message. Server: Return a 400 Bad Request response with an error code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-TSK-002

### 5.2.2 Rule Description

A task description must not exceed 100 characters.

### 5.2.3 Enforcement Point

Client-side validation (input restriction) and server-side validation upon API request.

### 5.2.4 Violation Handling

Client: Prevent further input. Server: Return a 400 Bad Request response with an error code.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be authenticated to create a task associated with their account.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

N/A

#### 6.1.2.2 Dependency Reason

A story to implement the main 'Daily Tasks' screen/view is required. This story provides the container and entry point for creating a new task.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint (`POST /api/v1/users/me/tasks`) for creating a task.
- Definition of the `Task` entity in the local Isar database schema.
- State management solution (Riverpod) for handling the creation form's state.

## 6.3.0.0 Data Dependencies

- Requires the authenticated user's ID (from JWT) to associate the task correctly.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to create a task must have a P95 latency of less than 200ms as per NFR-PERF-001.
- The task creation UI must load in under 500ms.

## 7.2.0.0 Security

- The API endpoint for task creation must be protected and require a valid JWT.
- The endpoint must validate that the user ID from the JWT matches the user for whom the task is being created.
- Input validation must be performed on the backend to prevent injection attacks (NFR-SEC-006).

## 7.3.0.0 Usability

- The process of creating a task should be simple and require minimal steps.
- Feedback (success, error, loading) must be immediate and clear.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires standard UI form implementation on the client.
- Requires a simple CRUD 'create' endpoint on the backend.
- Database schema modification is straightforward (adding a 'tasks' table).

## 8.3.0.0 Technical Risks

- Minimal risk. The primary consideration is ensuring the client-side state updates correctly after a successful creation to reflect the new task in the list without a full refresh.

## 8.4.0.0 Integration Points

- Client UI -> Client State Management (Riverpod)
- Client Repository -> Backend API Gateway
- Backend Service -> Aurora Database

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Create a task with a valid description.
- Attempt to create a task with an empty description.
- Attempt to create a task with a description at the exact character limit.
- Attempt to create a task with a description over the character limit.
- Cancel task creation with and without unsaved changes.
- Create a task while offline and verify error handling.

## 9.3.0.0 Test Data Needs

- A test user account (both Free and Premium tiers).

## 9.4.0.0 Testing Tools

- flutter_test for unit/widget tests.
- integration_test for E2E tests.
- Jest for backend unit/integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and widget tests implemented for UI and logic with >80% coverage
- Backend unit and integration tests implemented with >80% coverage
- E2E test scenario for creating a task is implemented and passing
- User interface reviewed and approved by UX/Product
- Performance requirements (API latency) verified
- Security requirements (endpoint protection) validated
- OpenAPI documentation for the new endpoint is created/updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the 'Daily Tasks' epic. It should be completed early in the development of the feature set as it blocks US-076 and US-078.

## 11.4.0.0 Release Impact

- This story is part of the initial release (v1.0) and is a core component of the habit-formation feature.

