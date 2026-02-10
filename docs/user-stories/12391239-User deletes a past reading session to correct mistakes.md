# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-052 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User deletes a past reading session to correct mis... |
| As A User Story | As an authenticated user, I want to permanently de... |
| User Persona | Any authenticated user (Free or Premium) who track... |
| Business Value | Improves data integrity and user trust by giving u... |
| Functional Area | Reading Tracking |
| Story Theme | Library and History Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successfully delete a session

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user viewing the session history for a book I have read

### 3.1.5 When

I tap the delete icon for a specific session and confirm the action in the confirmation dialog

### 3.1.6 Then

the session is permanently removed from the backend, and the session history list on my screen updates immediately to no longer show the deleted session.

### 3.1.7 Validation Notes

Verify via UI observation and by checking the API response. The database should no longer contain the session record.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User cancels the deletion process

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am viewing the session history for a book and have opened the confirmation dialog to delete a session

### 3.2.5 When

I tap the 'Cancel' button

### 3.2.6 Then

the confirmation dialog closes, the session is not deleted, and the session history list remains unchanged.

### 3.2.7 Validation Notes

Verify that no API call is made to the delete endpoint and the UI remains static.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Data Recalculation: Goal progress is updated after deletion

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have an active goal (e.g., 'Read 100 pages this week') and I delete a session that contributed 20 pages to it

### 3.3.5 When

the session deletion is successfully processed

### 3.3.6 Then

my goal progress is immediately recalculated to reflect the removal of those 20 pages, and all relevant UI elements (e.g., progress bars on the Dashboard and Goals screen) are updated.

### 3.3.7 Validation Notes

Before deletion, note the goal progress. After deletion, navigate to the Goals screen and Dashboard to verify the progress has decreased by the correct amount.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Data Recalculation: Overall statistics are updated after deletion

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I have existing reading statistics (total pages, total time, average speed) and I delete a session

### 3.4.5 When

the session deletion is successfully processed

### 3.4.6 Then

my overall statistics are recalculated to exclude the data from the deleted session, and the statistics screen is updated.

### 3.4.7 Validation Notes

Check the statistics screen before and after deletion to confirm that total pages, total time, and average reading speed have been correctly adjusted.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edge Case: Deleting a session breaks a reading streak

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a Premium User with a 5-day reading streak, and the session I am deleting is the only one I logged yesterday

### 3.5.5 When

I successfully delete that session

### 3.5.6 Then

my reading streak is recalculated and correctly displayed as broken or reduced.

### 3.5.7 Validation Notes

Requires setting up a Premium user with a specific streak. Verify the streak value in the 'Insights' section before and after deletion.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Error Handling: Network failure during deletion

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am attempting to delete a reading session while online

### 3.6.5 When

a network error prevents the request from reaching the server

### 3.6.6 Then

a non-blocking, user-friendly error message (e.g., a toast or snackbar) is displayed, and the session remains in the list on my device.

### 3.6.7 Validation Notes

Use a network proxy tool to simulate a network failure for the DELETE API call and verify the app's response.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Offline Support: Deleting a session while offline

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

I am offline and viewing the session history for a book

### 3.7.5 When

I delete a session and confirm the action

### 3.7.6 Then

the session is immediately removed from the local UI, and the delete action is queued for synchronization.

### 3.7.7 Validation Notes

Enable airplane mode. Perform the deletion and verify the UI updates. Check the local Isar database to confirm the item is marked for deletion.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Offline Support: Syncing a deletion after reconnecting

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

I have queued a session for deletion while offline

### 3.8.5 When

I regain network connectivity

### 3.8.6 Then

the app automatically syncs the deletion with the backend, and all server-side data (goals, stats) is updated accordingly.

### 3.8.7 Validation Notes

After performing AC-007, re-enable network. Verify the sync completes and then check on another device or after a fresh login to confirm the session is gone from the server.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly identifiable delete control (e.g., trash can icon) next to each session in the history list.
- A confirmation dialog/modal.

## 4.2.0 User Interactions

- Tapping the delete control triggers the confirmation dialog.
- The confirmation dialog must be modal, preventing interaction with the underlying screen until an action ('Delete' or 'Cancel') is taken.

## 4.3.0 Display Requirements

- The confirmation dialog must contain clear, concise text explaining that the action is permanent (e.g., 'Delete this session? This action cannot be undone.').
- A non-blocking error message (e.g., toast/snackbar) should be displayed on network failure.

## 4.4.0 Accessibility Needs

- The delete icon button must have a proper content description/accessibility label, e.g., 'Delete session from October 25th, 2024'.
- The confirmation dialog must be fully keyboard and screen-reader accessible.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user can only delete their own reading sessions.

### 5.1.3 Enforcement Point

Backend API (API Gateway authorizer and service logic).

### 5.1.4 Violation Handling

The API will return a 403 Forbidden or 404 Not Found status code if a user attempts to delete a session they do not own.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Deleting a reading session must trigger a recalculation of all dependent data.

### 5.2.3 Enforcement Point

Backend service logic, post-deletion.

### 5.2.4 Violation Handling

The deletion should occur within a transaction or trigger an event for a durable processing system (like SQS) to ensure recalculations are reliably executed. Failure should be logged for monitoring.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-050

#### 6.1.1.2 Dependency Reason

The user must be able to view the session history list before they can select a session to delete.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-101

#### 6.1.2.2 Dependency Reason

The offline deletion functionality relies on the generic background synchronization mechanism being implemented.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for `DELETE /api/v1/sessions/{sessionId}` must exist.
- Backend logic for recalculating user statistics and goal progress.
- Client-side local database (Isar) and state management (Riverpod) to handle UI updates and offline queueing.

## 6.3.0.0 Data Dependencies

- Requires existing reading session data for a user to be present for testing.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The client-side UI update after deletion must feel instantaneous (<500ms).
- The backend API call for deletion must adhere to the system-wide P95 latency target of <200ms (NFR-PERF-001).

## 7.2.0.0 Security

- The API endpoint must be secured and verify that the authenticated user is the owner of the session being deleted (NFR-SEC-002).

## 7.3.0.0 Usability

- The delete action must be easily discoverable but require explicit confirmation to prevent accidental data loss.

## 7.4.0.0 Accessibility

- All UI elements and interactions must comply with WCAG 2.1 Level AA standards (REQ-UIF-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity is not the deletion itself, but ensuring the cascading updates to goals and statistics are performed correctly and efficiently.
- Implementing the offline deletion and synchronization logic adds a significant layer of complexity.
- Ensuring data consistency between the client and server, especially after an offline sync, requires careful state management.

## 8.3.0.0 Technical Risks

- Potential for data inconsistency if the session is deleted but a subsequent recalculation fails. An event-driven or transactional approach on the backend is recommended to mitigate this.
- Race conditions during offline sync if the user is modifying related data on another device simultaneously.

## 8.4.0.0 Integration Points

- User Statistics Service/Module
- Goal Management Service/Module
- Offline Synchronization Service

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify deletion and UI update in online mode.
- Verify cancellation of deletion.
- Verify goal progress recalculation for all goal types (pages, time).
- Verify statistics recalculation (total pages, total time, avg. speed).
- Verify reading streak recalculation for Premium users.
- Verify offline deletion and successful sync upon reconnection.
- Verify error handling on network failure.

## 9.3.0.0 Test Data Needs

- Test accounts for both Free and Premium users.
- Accounts with various active goals.
- Accounts with established reading streaks.
- Books with single and multiple reading sessions.

## 9.4.0.0 Testing Tools

- `flutter_test` for unit/widget tests.
- `integration_test` for E2E tests.
- A REST client (e.g., Postman) for API-level testing.
- Network proxy (e.g., Charles) for simulating network errors.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage for new logic
- Integration testing completed successfully for the API endpoint and data recalculation logic
- User interface reviewed and approved for both light and dark modes
- Performance requirements verified on target devices
- Security requirements validated (ownership check on API)
- Documentation for the new API endpoint is generated/updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core user-facing feature required for basic data management. It has dependencies on viewing session history (US-050) and the offline sync mechanism (US-101).
- Should be prioritized after the ability to log and view sessions is complete.

## 11.4.0.0 Release Impact

Essential for the initial release (MVP) as it provides a fundamental error-correction capability.

