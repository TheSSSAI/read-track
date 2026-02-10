# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-076 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User configures a recurring daily task |
| As A User Story | As a user trying to build a consistent reading hab... |
| User Persona | Any authenticated user (Free or Premium) who wants... |
| Business Value | Increases user engagement and retention by embeddi... |
| Functional Area | Daily Tasks |
| Story Theme | Goal Management and Habit Formation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Creating a new task with weekly recurrence

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The user is on the 'Create New Task' screen

### 3.1.5 When

The user enters a task title, and selects 'Monday', 'Wednesday', and 'Friday' from the recurrence options

### 3.1.6 And

The system will display this task on the user's task list on all future Mondays, Wednesdays, and Fridays.

### 3.1.7 Then

The task is successfully saved with the specified recurrence rule

### 3.1.8 Validation Notes

Verify via API response that the task is created with the correct recurrence data. Manually check the UI on different days of the week (or by mocking the system date) to ensure the task appears only on the selected days.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Editing an existing task's recurrence schedule

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A user has an existing task that recurs on 'Tuesday' and 'Thursday'

### 3.2.5 When

The user navigates to the 'Edit Task' screen for that task

### 3.2.6 And

The task will no longer appear on Tuesdays and Thursdays but will appear on Saturdays and Sundays going forward.

### 3.2.7 Then

The task's recurrence rule is updated

### 3.2.8 Validation Notes

Confirm the UI correctly pre-populates the current recurrence settings. After saving, verify the task's visibility on different days of the week reflects the new schedule.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Attempting to save a recurring task with no days selected

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The user is on the 'Create New Task' screen

### 3.3.5 When

The user enters a task title but does not select any days of the week for recurrence

### 3.3.6 And

A user-friendly validation message is displayed, such as 'Please select at least one day for the task to repeat'.

### 3.3.7 Then

The system prevents the task from being saved

### 3.3.8 Validation Notes

This should be client-side validation to provide immediate feedback. The backend API should also enforce this rule and return a 400 Bad Request error if the client-side validation is bypassed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Recurrence settings are persisted and displayed correctly upon editing

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

A user has created a task that recurs on 'Monday' and 'Friday'

### 3.4.5 When

The user closes and reopens the app, then navigates to edit that task

### 3.4.6 Then

The recurrence UI should accurately show 'Monday' and 'Friday' as selected.

### 3.4.7 Validation Notes

This verifies that the recurrence state is correctly fetched from the backend and mapped to the UI state upon loading the edit screen.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A set of 7 selectable UI elements (e.g., chips, toggle buttons) representing each day of the week (M, T, W, T, F, S, S).
- A label for the section, such as 'Repeat on'.

## 4.2.0 User Interactions

- Tapping a day element toggles its state between 'selected' and 'unselected'.
- The selected state must be visually distinct from the unselected state (e.g., different background color, border, or icon).

## 4.3.0 Display Requirements

- The day selection controls must be present on both the 'Create Task' and 'Edit Task' screens.

## 4.4.0 Accessibility Needs

- Each day selector must have a proper content label for screen readers (e.g., 'Monday, not selected').
- The contrast ratio between the selected/unselected states and their background must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A recurring task must be set to recur on at least one day of the week.', 'enforcement_point': 'Client-side form validation and Backend API validation upon task creation or update.', 'violation_handling': 'The client displays an error message to the user. The backend rejects the request with a 400 status code and an error message.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-075', 'dependency_reason': 'This story adds recurrence functionality to the task creation process. The basic ability to create a single, non-recurring task must be implemented first.'}

## 6.2.0 Technical Dependencies

- Backend API endpoints for creating and updating tasks must be able to accept and store recurrence data.
- The database schema for the 'Task' entity must be updated to include a field for recurrence rules (e.g., an array of day identifiers).

## 6.3.0 Data Dependencies

*No items available*

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The UI interaction for selecting/deselecting days must be instantaneous with no perceivable lag.
- Saving the task with recurrence settings should complete within 500ms on a standard 4G connection.

## 7.2.0 Security

- Standard API security measures (authentication, authorization) must be applied to the task creation/update endpoints.

## 7.3.0 Usability

- The day selection interface should be intuitive and require minimal cognitive load from the user.

## 7.4.0 Accessibility

- All UI elements must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0 Compatibility

- The UI component must render correctly on all supported iOS and Android versions and screen sizes as defined in REQ-OPE-001.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Requires coordinated changes across the stack: Flutter frontend (UI component, state management), backend API (validation, service logic), and database (schema migration).
- Logic must be implemented to determine which tasks are active for the current day based on their recurrence rules and the user's local timezone.

## 8.3.0 Technical Risks

- Handling timezones correctly can be complex. The logic for determining the 'current day' must reliably use the user's device timezone to display the correct tasks.

## 8.4.0 Integration Points

- Frontend task creation/editing form.
- Backend `POST /api/v1/tasks` and `PUT /api/v1/tasks/{id}` endpoints.
- Database `Task` table/collection.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E
- Accessibility

## 9.2.0 Test Scenarios

- Create a task recurring on one day.
- Create a task recurring on multiple days.
- Create a task recurring on all days.
- Edit a task to change its recurrence from one day to another.
- Edit a task to add/remove days from its recurrence.
- Verify task visibility across a full week after creation/editing.
- Test form validation for selecting no days.

## 9.3.0 Test Data Needs

- User accounts (Free and Premium) to ensure functionality is available to both.
- Pre-existing tasks to test the editing flow.

## 9.4.0 Testing Tools

- Flutter: `flutter_test` for unit/widget tests, `integration_test` for E2E tests.
- Backend: Jest for unit/integration tests.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented and passing with >= 80% coverage
- Backend integration testing completed successfully
- User interface reviewed and approved by Product Owner/Design
- Accessibility requirements validated with screen readers and contrast checkers
- Database migration script is written and tested
- Documentation for the API endpoint changes is updated in OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- The prerequisite story US-075 must be completed or scheduled in the same sprint.
- Frontend and backend developers need to agree on the data structure for the recurrence rule (e.g., `['MON', 'WED', 'FRI']`) before implementation begins.

## 11.4.0 Release Impact

This is a key feature for the Daily Tasks functional area and significantly enhances the app's habit-building capabilities.

