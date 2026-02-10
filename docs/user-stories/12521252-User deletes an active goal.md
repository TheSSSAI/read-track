# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-065 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User deletes an active goal |
| As A User Story | As a Registered User, I want to permanently delete... |
| User Persona | Any registered user (Free or Premium) who has crea... |
| Business Value | Improves user experience by providing full control... |
| Functional Area | Goal Management |
| Story Theme | Reading Goal Setting and Monitoring |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User successfully deletes a goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a registered user is logged in and is on the 'Goals' screen where an active goal is displayed

### 3.1.5 When

the user taps the 'delete' option for that goal, and then taps 'Confirm' on the confirmation dialog

### 3.1.6 Then

a request is sent to the backend to delete the goal, the goal is removed from the 'Goals' screen, and the Dashboard UI is updated to no longer show progress for the deleted goal.

### 3.1.7 Validation Notes

Verify via UI observation and by checking the backend database that the goal record is removed or marked as deleted for the user.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User cancels the deletion process

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

a registered user is viewing the confirmation dialog for deleting a goal

### 3.2.5 When

the user taps the 'Cancel' button or dismisses the dialog

### 3.2.6 Then

the dialog closes, no deletion request is sent, and the goal remains active and visible in the UI.

### 3.2.7 Validation Notes

Verify that the goal is still present on the 'Goals' screen and the Dashboard, and no API call was made to the delete endpoint.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Confirmation dialog prevents accidental deletion

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

a user taps the 'delete' option for a goal

### 3.3.5 When

the system displays a confirmation dialog

### 3.3.6 Then

the dialog must clearly state that the action is irreversible and contain distinct 'Confirm' and 'Cancel' actions.

### 3.3.7 Validation Notes

UI review to ensure the dialog text is clear and the buttons are unambiguous. The 'Confirm' button should be styled as a destructive action.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Free User can create a new goal after deleting their only one

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a Free User has one active goal (the maximum allowed)

### 3.4.5 When

the user successfully deletes that goal

### 3.4.6 Then

the user is now able to access the flow to create a new goal.

### 3.4.7 Validation Notes

After deletion, navigate to the goal creation screen and verify that the UI elements for creating a goal are enabled.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User deletes a goal while offline

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user is offline and has a locally cached active goal

### 3.5.5 When

the user initiates and confirms the deletion of the goal

### 3.5.6 Then

the goal is immediately removed from the UI, the deletion action is queued locally, and a success feedback message is shown.

### 3.5.7 Validation Notes

With the device in airplane mode, delete a goal and verify the UI updates. Then, restore network connectivity and verify the goal is deleted on the backend.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Backend enforces ownership on deletion

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

User A is authenticated

### 3.6.5 When

an API request is crafted to delete a goal belonging to User B

### 3.6.6 Then

the API must return a '403 Forbidden' or '404 Not Found' error, and the goal belonging to User B must not be deleted.

### 3.6.7 Validation Notes

Requires an integration or API-level test where an authenticated session for User A attempts to call the DELETE endpoint with the goal ID of User B.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'delete' icon (e.g., trash can) or menu option associated with each goal in the goal management list.
- A modal confirmation dialog with a title (e.g., 'Delete Goal?'), descriptive text (e.g., 'This action is permanent and cannot be undone.'), a 'Confirm' button, and a 'Cancel' button.
- A transient, non-blocking success notification (e.g., a toast or snackbar) that appears after successful deletion, saying 'Goal deleted.'

## 4.2.0 User Interactions

- Tapping the delete icon/option triggers the confirmation dialog.
- The confirmation dialog must block interaction with the underlying screen until it is dismissed.
- Tapping 'Confirm' executes the deletion and dismisses the dialog.
- Tapping 'Cancel' dismisses the dialog without any other action.

## 4.3.0 Display Requirements

- Upon successful deletion, the goal must be immediately removed from all lists and dashboards in the app.

## 4.4.0 Accessibility Needs

- The delete icon must have a proper content label for screen readers (e.g., 'Delete goal').
- The confirmation dialog must trap focus, and screen readers must announce its title, message, and button options upon appearing.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-GOL-001

### 5.1.2 Rule Description

A user can only delete goals that they own.

### 5.1.3 Enforcement Point

Backend API (at the service/controller level).

### 5.1.4 Violation Handling

The API will reject the request with a 403 Forbidden or 404 Not Found status code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-GOL-002

### 5.2.2 Rule Description

Goal deletion is a permanent (hard delete) action and is irreversible.

### 5.2.3 Enforcement Point

User confirmation dialog and backend data removal process.

### 5.2.4 Violation Handling

N/A - This rule defines system behavior. The confirmation dialog mitigates user error.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

A user must be able to create a goal before they can delete one.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-063

#### 6.1.2.2 Dependency Reason

Requires the goal list UI where the delete control will be located.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-057

#### 6.1.3.2 Dependency Reason

The dashboard component must exist to be updated upon goal deletion.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-101

#### 6.1.4.2 Dependency Reason

The offline synchronization mechanism must be implemented to handle queued deletion operations.

## 6.2.0.0 Technical Dependencies

- Backend: A secure `DELETE /api/v1/goals/{goalId}` endpoint.
- Mobile: State management (Riverpod) capable of propagating the deletion event to all relevant UI components.
- Mobile: Local database (Isar) and offline sync queue.

## 6.3.0.0 Data Dependencies

- Requires existing goal data for a user to perform the deletion.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response for the DELETE request must have a P95 latency of less than 200ms (as per REQ-PER-001).
- The UI update after deletion must be visually instantaneous (<100ms).

## 7.2.0.0 Security

- The API endpoint must be protected and require a valid JWT.
- The backend must perform an ownership check to ensure the requesting user owns the goal being deleted (as per NFR-SEC-002).

## 7.3.0.0 Usability

- The deletion process must include a confirmation step to prevent accidental data loss.

## 7.4.0.0 Accessibility

- All UI elements involved (buttons, dialogs) must meet WCAG 2.1 Level AA standards (as per REQ-UIF-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (as per REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The core online deletion is 'Low' complexity.
- Implementing robust offline deletion with guaranteed synchronization raises the complexity to 'Medium'.
- Ensuring state is correctly updated across multiple screens (Dashboard, Goals list) requires careful implementation.

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if the offline logic is not robust. The 'last write wins' strategy should be sufficient, as a delete is the final state.
- Risk of leaving orphaned UI elements if the state management does not correctly notify all listeners of the deletion.

## 8.4.0.0 Integration Points

- Client State Management (Riverpod)
- Client Local Database (Isar)
- Client-Backend API (via Dio)
- Backend Goal Service
- Backend Primary Database (Aurora)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify successful online deletion and UI update.
- Verify cancellation of deletion.
- Verify successful offline deletion and subsequent sync upon reconnection.
- Verify a Free User can create a new goal after deleting their only one.
- API Test: Attempt to delete another user's goal and verify failure.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' roles.
- At least one pre-existing goal for each test user.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test` for unit/widget tests.
- Flutter: `integration_test` for E2E tests.
- Backend: `Jest` for unit/integration tests.
- API testing tool like Postman or Insomnia for direct endpoint testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage for new code
- E2E integration test for the deletion flow (including offline) is implemented and passing
- User interface reviewed and approved by UX/UI designer
- API performance confirmed to be within latency targets
- Backend security (ownership check) validated
- Documentation for the API endpoint is updated in the OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core piece of functionality for the Goals feature and should be prioritized alongside goal creation and editing.
- Depends on the completion of the basic goal creation/viewing stories.

## 11.4.0.0 Release Impact

- Essential for a minimum viable product (MVP) release of the Goal Management feature.

