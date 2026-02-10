# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-064 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User edits the target value of an active goal |
| As A User Story | As an active user, I want to edit the target value... |
| User Persona | Any registered user (Free or Premium) who has set ... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Goal Management |
| Story Theme | Reading Goals and Progress Tracking |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully edit a goal's target value while online

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user on the 'Goals' screen with an active yearly goal of reading 50 books

### 3.1.5 When

I tap the 'Edit' option for that goal, change the target value to '60', and tap 'Save'

### 3.1.6 Then

The system updates the goal's target value to 60, a confirmation message 'Goal updated' is briefly displayed, and the goal progress UI (e.g., progress bar, percentage text) is immediately recalculated and updated to reflect the new target.

### 3.1.7 Validation Notes

Verify the API call `PATCH /api/v1/goals/{goalId}` is successful with a 200 OK response. Check the Dashboard and Goals screen to confirm the UI reflects the new target value and re-calculated progress.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Attempt to save an invalid target value

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am on the 'Edit Goal' screen

### 3.2.5 When

I clear the target value field or enter a non-numeric value (e.g., 'abc') or '0' and tap 'Save'

### 3.2.6 Then

The system prevents the save action and displays a clear, inline validation error message such as 'Please enter a number greater than 0'.

### 3.2.7 Validation Notes

Test with empty, zero, negative, and non-numeric inputs. Verify no API call is made and the error message is displayed and accessible.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Cancel the edit operation

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am on the 'Edit Goal' screen and have changed the target value from 50 to 60

### 3.3.5 When

I tap the 'Cancel' button or dismiss the modal

### 3.3.6 Then

The changes are discarded, the 'Edit Goal' screen closes, and the goal's target value remains unchanged at 50.

### 3.3.7 Validation Notes

Verify that no API call is made and the UI on the previous screen shows the original goal value.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edit a goal's target value while offline

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a logged-in user with an active goal and my device is offline

### 3.4.5 When

I edit the goal's target value and tap 'Save'

### 3.4.6 Then

The goal is updated in the local database (Isar), the UI immediately reflects the change, and the update is queued for synchronization.

### 3.4.7 Validation Notes

Use device settings to disable network. Perform the edit. Verify the UI updates. Re-enable network and verify the background sync process successfully updates the backend.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edit a goal's target to be lower than the current progress

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I have an active goal to read 50 books and my current progress is 40 books read

### 3.5.5 When

I edit the goal's target value to 30 and tap 'Save'

### 3.5.6 Then

The system saves the new target, and the UI updates to show the goal as 100% complete, displaying a 'Goal Achieved' state.

### 3.5.7 Validation Notes

Verify the progress bar shows as full and any associated text indicates completion (e.g., '40/30 books read').

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Edit' icon button (e.g., pencil icon) associated with each active goal in the goal list.
- A modal dialog or dedicated screen for editing a goal.
- A numeric input field, pre-populated with the current target value.
- A 'Save' button to confirm changes.
- A 'Cancel' button or close icon (X) to discard changes.
- A toast or snackbar for success/failure feedback.

## 4.2.0 User Interactions

- Tapping 'Edit' opens the editing interface.
- The numeric keyboard should be displayed when the target value field is focused.
- Tapping 'Save' triggers validation and, if successful, the update process.
- Tapping 'Cancel' closes the editing interface without saving.

## 4.3.0 Display Requirements

- The editing interface must clearly label the goal being edited (e.g., 'Edit Yearly Book Goal').
- Validation errors must be displayed close to the input field they relate to.
- All goal progress indicators across the app (Dashboard, Goals screen) must update immediately after a successful edit.

## 4.4.0 Accessibility Needs

- All buttons and input fields must have accessible labels for screen readers.
- Error messages must be programmatically associated with their respective input fields.
- UI must adhere to WCAG 2.1 Level AA for color contrast and touch target size.

# 5.0.0 Business Rules

- {'rule_id': 'BR-GOL-001', 'rule_description': 'The target value for any goal must be a positive integer greater than zero.', 'enforcement_point': "Client-side validation on the 'Edit Goal' screen and server-side validation on the API endpoint.", 'violation_handling': 'The client shows a user-friendly error message and blocks the save action. The server rejects the request with a 400 Bad Request status code and an error message.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

A user must be able to create a goal before they can edit it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-061

#### 6.1.2.2 Dependency Reason

A user must be able to create a goal before they can edit it.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-062

#### 6.1.3.2 Dependency Reason

A user must be able to create a goal before they can edit it.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-063

#### 6.1.4.2 Dependency Reason

The UI for viewing goal progress must exist to provide an entry point for editing.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-101

#### 6.1.5.2 Dependency Reason

The offline editing functionality relies on the core background data synchronization mechanism.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., `PATCH /api/v1/goals/{goalId}`) for updating a goal.
- The local `Goal` entity schema in the Isar database.
- The Riverpod state management provider for goals, which must support updating a single goal entity.

## 6.3.0.0 Data Dependencies

- Requires an existing, active goal record for the authenticated user in the database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI update after saving a goal must feel instantaneous (less than 500ms).
- The backend API endpoint must conform to the system-wide P95 latency requirement of < 200ms (NFR-PERF-001).

## 7.2.0.0 Security

- The backend API must ensure a user can only edit their own goals (authorization).
- All input must be sanitized on the backend to prevent injection attacks (NFR-SEC-006).

## 7.3.0.0 Usability

- The process of editing a goal should be discoverable and require minimal steps.
- Feedback on success or failure of the action must be clear and immediate.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the offline editing and synchronization logic adds complexity.
- Ensuring reactive UI updates across multiple screens (Dashboard, Goals List) requires careful state management.
- The core UI and API logic is straightforward CRUD, but the integration with the offline system elevates the complexity.

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if the 'last write wins' strategy is not implemented correctly. This is a low risk given the nature of the data.
- State management complexity could lead to bugs where one part of the UI does not update correctly after an edit.

## 8.4.0.0 Integration Points

- Frontend state management (Riverpod).
- Local database (Isar).
- Backend API Gateway and Goals service.
- Background synchronization service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify successful online edit and UI refresh.
- Verify cancellation discards changes.
- Test all invalid input cases (zero, negative, non-numeric, empty).
- Perform an edit while offline, then go online and verify sync.
- Edit a goal to have a target lower than current progress and verify the 'completed' state.
- Attempt to edit a goal belonging to another user via API manipulation (security test).

## 9.3.0.0 Test Data Needs

- A test user account with multiple active goals of different types (books, pages, time).
- A test user account with a goal where progress is close to the target.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.
- Jest for backend unit/integration tests.
- Postman or similar for API security testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% code coverage
- Backend integration testing completed successfully
- E2E automated test for the happy path (online and offline) is implemented and passing
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on target devices
- Security requirements validated (API authorization)
- Documentation for the API endpoint is updated in the OpenAPI specification
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is critical for user satisfaction with the goals feature.
- Confirm that prerequisite stories, especially the offline sync framework (US-101), are completed or will be completed in the same sprint.
- Requires coordinated effort between frontend and backend developers.

## 11.4.0.0 Release Impact

Enhances the core goal management feature, making it more robust and user-friendly. This is a significant improvement for user retention.

