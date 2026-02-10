# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-051 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User edits a past reading session to correct data |
| As A User Story | As an active reader, I want to edit the details of... |
| User Persona | Any registered user (Free or Premium) who actively... |
| Business Value | Enhances data integrity and user trust in the appl... |
| Functional Area | Reading Tracking |
| Story Theme | Library and Progress Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User successfully edits the page number of a session

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am viewing the session history for a book in my library, and a session exists showing progress from page 20 to 40.

### 3.1.5 When

I select the 'edit' option for that session, change the end page to 50, and save the changes.

### 3.1.6 Then

The session history list updates to show the session progress as page 20 to 50, and my overall statistics (total pages read, goal progress) are recalculated and updated.

### 3.1.7 Validation Notes

Verify the session record in the local (Isar) and remote (Aurora) database is updated. Verify the UI on the session history screen, dashboard, and statistics screen reflects the new data.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: User successfully edits the duration of a session

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am viewing the session history for a book, and a session exists with a logged duration of '30 minutes'.

### 3.2.5 When

I select the 'edit' option for that session, change the duration to '45 minutes', and save the changes.

### 3.2.6 Then

The session history list updates to show the duration as '45 minutes', and my overall statistics (total time spent, goal progress) are recalculated and updated.

### 3.2.7 Validation Notes

Verify the session record's duration field is updated in the database. Verify UI elements related to time tracking are updated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Happy Path: User successfully edits the date of a session

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have a logged session dated yesterday, and my current reading streak is 5 days.

### 3.3.5 When

I edit the session and change its date to three days ago.

### 3.3.6 Then

The session is now listed under the new date, and my reading streak is correctly recalculated based on the new session date.

### 3.3.7 Validation Notes

Verify the session's timestamp is updated in the database. Verify the reading streak calculation logic is triggered and the value displayed in the 'Insights' section (for Premium users) is correct.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: User enters an invalid page number

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am editing a session for a book that has a previous session ending on page 50.

### 3.4.5 When

I attempt to change the current session's end page to 45 (a value less than the previous session's end page).

### 3.4.6 Then

The system prevents me from saving and displays a clear, user-friendly error message, such as 'Page number must be greater than the previous session.'

### 3.4.7 Validation Notes

Test frontend validation for immediate feedback and backend validation to ensure data integrity.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: User enters a zero or negative duration

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am editing a session's duration.

### 3.5.5 When

I attempt to enter '0' or a negative number for the duration and save.

### 3.5.6 Then

The system prevents the save and displays an error message, such as 'Duration must be greater than zero.'

### 3.5.7 Validation Notes

Verify input validation on the duration field.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Edge Case: Editing a session changes a book's status from 'Read' to 'Currently Reading'

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I have a 300-page book on my 'Read' shelf because I logged a final session ending on page 300.

### 3.6.5 When

I edit that final session and change the end page to 250.

### 3.6.6 Then

The book's shelf status automatically changes from 'Read' to 'Currently Reading'.

### 3.6.7 Validation Notes

Verify that updating a session triggers a check on the parent LibraryItem's status and updates it if necessary.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Alternative Flow: User cancels the edit operation

### 3.7.3 Scenario Type

Alternative_Flow

### 3.7.4 Given

I have opened the edit screen for a reading session and made some changes.

### 3.7.5 When

I tap the 'Cancel' button instead of 'Save'.

### 3.7.6 Then

I am returned to the session history screen, and no changes have been saved to the session data.

### 3.7.7 Validation Notes

Verify that the state is discarded and no API call is made to update the session.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Offline Behavior: User edits a session while offline

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

I am offline and viewing my session history, which is available from the local cache.

### 3.8.5 When

I edit a session and save the changes.

### 3.8.6 Then

The changes are saved to the local Isar database, and the UI updates immediately. When I reconnect to the internet, the changes are automatically synced to the backend.

### 3.8.7 Validation Notes

Test this by enabling airplane mode, performing the edit, verifying the local UI, then disabling airplane mode and verifying the data is updated on the backend.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Edit' icon or button next to each session in the session history list.
- A modal or dedicated screen for editing a session.
- Editable input fields for 'End Page' (or 'Percentage'), 'Duration', and 'Date'.
- A 'Save' or 'Confirm' button to commit changes.
- A 'Cancel' or 'Back' button to discard changes.
- A toast/snackbar notification confirming 'Session updated successfully.'

## 4.2.0 User Interactions

- Tapping 'Edit' opens the editing interface with fields pre-populated with the session's current data.
- Tapping 'Save' triggers validation, saves the data, closes the edit interface, and refreshes the session list.
- Tapping 'Cancel' closes the edit interface without saving.

## 4.3.0 Display Requirements

- The edit form must clearly label each field.
- Validation errors must be displayed inline, next to the relevant field.

## 4.4.0 Accessibility Needs

- All input fields must have proper labels for screen readers.
- Tap targets for 'Edit', 'Save', and 'Cancel' must meet minimum size requirements (WCAG 2.1).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-TRK-001

### 5.1.2 Rule Description

A session's end page/percentage cannot be less than or equal to the end page/percentage of the chronologically preceding session for the same library item.

### 5.1.3 Enforcement Point

Client-side validation on input change; Backend validation before database commit.

### 5.1.4 Violation Handling

Prevent save operation and display a user-friendly error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-TRK-002

### 5.2.2 Rule Description

A session's duration must be a positive value greater than zero.

### 5.2.3 Enforcement Point

Client-side and backend validation.

### 5.2.4 Violation Handling

Prevent save operation and display an error message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-TRK-003

### 5.3.2 Rule Description

Editing a session must trigger a recalculation of all derived data, including book progress, goal progress, user statistics, and reading streaks.

### 5.3.3 Enforcement Point

Backend, upon successful update of a ReadingSession record.

### 5.3.4 Violation Handling

The entire update operation (session + stats) should be atomic. If recalculation fails, the session update should be rolled back.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-046

#### 6.1.1.2 Dependency Reason

Core functionality for logging progress by page number must exist to be editable.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-048

#### 6.1.2.2 Dependency Reason

Core functionality for logging time must exist to be editable.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-050

#### 6.1.3.2 Dependency Reason

The session history view is the entry point for initiating an edit.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-068

#### 6.1.4.2 Dependency Reason

Basic statistics must exist to be recalculated and reflect the edited data.

## 6.2.0.0 Technical Dependencies

- Backend API: A secure `PUT` or `PATCH` endpoint to update a `ReadingSession` entity (e.g., `/api/v1/sessions/{sessionId}`).
- Local Database (Isar): Schema for `ReadingSession` must be defined and support updates.
- Offline Synchronization Service: Must handle updates to existing records, not just creations.

## 6.3.0.0 Data Dependencies

- Requires existing `ReadingSession` data for a user to edit.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The edit screen must load in under 500ms.
- Saving the session and recalculating stats on the client-side should feel instantaneous (< 200ms) to the user.
- Backend API response for the update request must be < 200ms (P95), as per REQ-PER-001.

## 7.2.0.0 Security

- The API endpoint must validate that the user making the request is the owner of the reading session they are trying to edit (RBAC).

## 7.3.0.0 Usability

- Error messages must be clear and actionable.
- The process of editing should require a minimal number of taps.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards, as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity is managing the 'ripple effect' of the edit, requiring robust logic to recalculate and update all dependent data points (goals, stats, streaks).
- Implementing the update logic to be atomic (all-or-nothing) on the backend.
- Handling offline edits and ensuring correct synchronization with the 'last write wins' strategy.

## 8.3.0.0 Technical Risks

- Potential for data inconsistency if the recalculation logic fails or is incomplete.
- Race conditions during offline synchronization if not handled carefully.

## 8.4.0.0 Integration Points

- State Management (Riverpod): The UI must correctly reflect the updated state after an edit.
- Backend Database (Aurora): The primary data store for the session.
- Local Database (Isar): The offline data store.
- Statistics & Goals Services (Backend): These services will be invoked to perform recalculations.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify all happy path scenarios for editing page, percentage, duration, and date.
- Test all validation rules and error conditions.
- Test the edge case where a book's status changes from 'Read' to 'Currently Reading'.
- Perform E2E tests to confirm that changes are reflected correctly on the Dashboard, Goals, and Statistics screens.
- Test the full offline-to-online sync cycle for an edited session.

## 9.3.0.0 Test Data Needs

- A test user account with multiple books and multiple reading sessions.
- A book with a defined page count.
- An article without a page count (to test percentage-based edits).
- A user with active goals and existing reading streaks.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test for E2E tests.
- Jest for backend unit/integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage for new logic
- Backend integration testing completed successfully
- E2E test case for editing a session is implemented and passing
- User interface reviewed and approved by UX/UI designer
- Performance requirements for API and UI response times are met
- Security requirement for ownership validation is implemented and tested
- Documentation for the new API endpoint is generated (OpenAPI)
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a high-priority follow-up to the initial tracking implementation, as it's a core user expectation for data management.
- The team should allocate sufficient time for testing the recalculation logic across all affected features.

## 11.4.0.0 Release Impact

Improves the quality and reliability of the core tracking feature. Essential for user retention.

