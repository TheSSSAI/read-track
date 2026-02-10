# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-078 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User marks a daily task as complete |
| As A User Story | As a user focused on building a consistent reading... |
| User Persona | Any authenticated user (Free or Premium) who has c... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Daily Tasks & Goal Management |
| Story Theme | Habit Formation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Marking an incomplete task as complete

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A logged-in user is viewing their list of daily tasks for the current day

### 3.1.5 When

The user taps the interactive element (e.g., checkbox) for an incomplete task

### 3.1.6 Then



```
The UI immediately updates to show the task in a 'completed' state (e.g., checkbox is filled, text has a strikethrough).
AND The task's completion status for the current date is persisted to the backend.
AND If the user navigates away and returns, the task remains marked as complete.
```

### 3.1.7 Validation Notes

Verify UI change is instant. Check the API call is successful (200 OK) and the database reflects the new completion record for the correct user, task, and date.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Unmarking a completed task

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A logged-in user is viewing their list of daily tasks for the current day

### 3.2.5 And

A task is already marked as complete

### 3.2.6 When

The user taps the interactive element for the completed task

### 3.2.7 Then



```
The UI immediately updates to show the task in an 'incomplete' state.
AND The task's completion status for the current date is updated on the backend.
```

### 3.2.8 Validation Notes

Verify the UI reverts to the original state. Check the API call is successful and the database record for the completion is removed or marked inactive.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Marking a task complete while offline

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

A logged-in user is viewing their daily tasks while the device is offline

### 3.3.5 When

The user taps to mark an incomplete task as complete

### 3.3.6 Then



```
The UI immediately updates to show the task as complete.
AND The change is saved to the local device database (Isar).
AND When network connectivity is restored, the change is automatically synchronized with the backend without further user interaction.
```

### 3.3.7 Validation Notes

Test by enabling airplane mode, performing the action, closing and reopening the app to see if state persists locally. Then, disable airplane mode and verify the backend data is updated.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

API call fails when updating task status

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

A logged-in user is online and viewing their daily tasks

### 3.4.5 When

The user taps to change a task's completion status, but the backend API returns an error (e.g., 5xx)

### 3.4.6 Then



```
The UI reverts the task to its original state before the tap.
AND A non-intrusive error message (e.g., a toast notification) is displayed to the user, such as 'Could not update task. Please try again.'
```

### 3.4.7 Validation Notes

Use a tool like Charles Proxy or mock the API response to simulate a server error and verify the UI rollback and error message.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User cannot complete tasks for other days

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A user is viewing the daily tasks list

### 3.5.5 When

The date changes to the next day (e.g., past midnight)

### 3.5.6 Then



```
The list of tasks refreshes to show the tasks for the new day.
AND The completion status of the previous day's tasks is preserved but no longer directly editable from the main view.
```

### 3.5.7 Validation Notes

Manually change the device's date/time to test this transition. The UI should only present interactive completion controls for the current day's tasks.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A list of daily tasks for the current day.
- An interactive control (e.g., a checkbox or tappable circle) next to each task.

## 4.2.0 User Interactions

- Tapping the control toggles the task's completion state.
- The UI provides immediate visual feedback upon tap (optimistic update).

## 4.3.0 Display Requirements

- Completed tasks must be visually distinct from incomplete tasks (e.g., filled checkbox, strikethrough text, faded color).
- The state must persist across app sessions.

## 4.4.0 Accessibility Needs

- The tappable area for the control must meet WCAG 2.1 standards (minimum 44x44 points).
- A screen reader must announce the task's name and its current state (e.g., 'Read one chapter, checkbox, checked' or '...not checked').
- State changes must not be conveyed by color alone.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A task completion is unique to a specific task, user, and calendar date.', 'enforcement_point': 'Backend API and Database Schema', 'violation_handling': 'The system should prevent duplicate completion entries for the same task on the same day. An attempt to mark a completed task as complete again should be an idempotent operation, returning a success status without creating a new record.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-075

#### 6.1.1.2 Dependency Reason

Users must be able to create tasks before they can mark them as complete.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-076

#### 6.1.2.2 Dependency Reason

The system needs to know which recurring tasks are scheduled for the current day to display them.

## 6.2.0.0 Technical Dependencies

- REQ-OFF-001: Offline support functionality, including the local Isar database and background synchronization service.
- REQ-USR-001: User authentication system to ensure a user can only modify their own tasks.

## 6.3.0.0 Data Dependencies

- Requires a data model that can store task completion status on a per-day basis. A simple boolean on the task entity is insufficient. A `TaskCompletion` table with `(taskId, userId, completionDate)` is recommended.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for updating task status must have a P95 latency of less than 200ms (NFR-PERF-001).
- The UI update on the client must be instantaneous (<50ms) and not block the UI thread.

## 7.2.0.0 Security

- The API endpoint must be protected and require a valid JWT.
- The backend must perform an authorization check to ensure the authenticated user owns the task they are attempting to modify (NFR-SEC-002).

## 7.3.0.0 Usability

- The interaction should be simple, intuitive, and provide clear, immediate feedback.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires careful implementation of optimistic UI updates with rollback on API failure.
- Integration with the offline storage (Isar) and background sync mechanism adds complexity.
- The backend data model must correctly handle daily completions for recurring tasks, which is more complex than a simple status flag.

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if a task status is updated on two different devices while one is offline. The 'last write wins' strategy (REQ-OFF-001) must be correctly implemented.
- Race conditions if the user taps the control very rapidly. The client-side logic should debounce the action.

## 8.4.0.0 Integration Points

- Client State Management (Riverpod)
- Local Database (Isar)
- Backend API Gateway
- Backend Tasks Service
- Primary Database (Aurora)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify marking and unmarking a task online.
- Verify marking and unmarking a task offline and successful sync upon reconnection.
- Verify UI rollback and error messaging on API failure.
- Verify that a task for a recurring schedule can be completed independently each day.
- Verify screen reader functionality for announcing task state.

## 9.3.0.0 Test Data Needs

- User accounts (Free and Premium).
- At least one single and one recurring daily task per user.

## 9.4.0.0 Testing Tools

- flutter_test for unit/widget tests.
- integration_test package for E2E tests.
- Jest for backend unit tests.
- A proxy tool (e.g., Charles) to simulate network failures.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage
- Backend integration testing completed successfully
- User interface reviewed and approved for both iOS and Android
- Performance requirements (API latency) verified
- Security requirements (authorization) validated
- Accessibility audit passed for the task list component
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational feature for the habit-building loop and is a prerequisite for features like streaks (US-070) and dashboard reminders (US-059).
- Requires both frontend and backend development effort.

## 11.4.0.0 Release Impact

This is a core feature for the initial release, essential for demonstrating the app's value in habit formation.

