# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-047 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User logs reading progress by percentage for items... |
| As A User Story | As a Registered User, I want to log my reading pro... |
| User Persona | Any registered user (Free or Premium) who reads co... |
| Business Value | Increases the application's versatility and utilit... |
| Functional Area | Reading Tracking |
| Story Theme | Library and Progress Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display percentage input for non-paged items

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is logged in and has a library item without a page count (e.g., an article) on their 'Currently Reading' shelf

### 3.1.5 When

the user initiates the 'Log Session' action for that item

### 3.1.6 Then

the session logging interface must display an input field for 'Progress (%)' instead of 'Page Number'.

### 3.1.7 Validation Notes

Verify that for a book with a page count, the 'Page Number' input is shown. For an article with no page count, the 'Progress (%)' input is shown. The switch must be automatic.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Log initial progress for an article

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the user is on the session logging screen for an article with 0% progress

### 3.2.5 When

the user enters '25' into the 'Progress (%)' field, enters a duration, and saves the session

### 3.2.6 Then

a new ReadingSession record is created for the item

### 3.2.7 And

the overall progress for the LibraryItem is updated to 25%.

### 3.2.8 Validation Notes

Check the local database (Isar) and backend database (Aurora) to confirm the session is saved and the parent item's progress is updated.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Log subsequent progress for an article

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the user is logging a session for an article that already has 25% progress

### 3.3.5 When

the user enters '60' into the 'Progress (%)' field and saves the session

### 3.3.6 Then

the overall progress for the LibraryItem is updated to 60%

### 3.3.7 And

the new ReadingSession record correctly reflects the progress made during this session (35%).

### 3.3.8 Validation Notes

Verify the calculation is correct and both the session and the parent library item are updated accurately.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User marks an article as complete by logging 100%

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

the user is logging a session for an article

### 3.4.5 When

the user enters '100' into the 'Progress (%)' field and saves

### 3.4.6 Then

the system updates the item's progress to 100%

### 3.4.7 And

the user is prompted to move the item to the 'Read' shelf and provide a completion date, consistent with the flow for paged books.

### 3.4.8 Validation Notes

This flow should be identical to finishing a book by entering the last page number.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Attempt to log progress with a value over 100%

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

the user is on the session logging screen for an article

### 3.5.5 When

the user attempts to enter a value greater than 100 (e.g., '101') in the 'Progress (%)' field

### 3.5.6 Then

a validation error message is displayed (e.g., 'Percentage must be between 0 and 100')

### 3.5.7 And

the save action is disabled until a valid value is entered.

### 3.5.8 Validation Notes

Test with integer and decimal values above 100.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to log progress lower than the current progress

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

the user is logging a session for an article that has 50% progress

### 3.6.5 When

the user enters a value of '40' in the 'Progress (%)' field

### 3.6.6 Then

a validation error message is displayed (e.g., 'New progress cannot be less than current progress')

### 3.6.7 And

the save action is disabled.

### 3.6.8 Validation Notes

This prevents accidental regression of progress.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Attempt to log progress with non-numeric input

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

the user is on the session logging screen for an article

### 3.7.5 When

the user attempts to enter non-numeric characters (e.g., 'abc') into the 'Progress (%)' field

### 3.7.6 Then

the input is rejected or a validation error is shown

### 3.7.7 And

the save action is disabled.

### 3.7.8 Validation Notes

The input field should be configured to only accept numeric characters.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A numeric input field labeled 'Progress (%)'.
- Validation error message text elements.

## 4.2.0 User Interactions

- The system must automatically and conditionally display either the 'Page Number' input or the 'Progress (%)' input based on the properties of the selected library item.
- User input in the percentage field should be validated in real-time to enable/disable the save button.

## 4.3.0 Display Requirements

- The current progress of the item should be visible on the logging screen to provide context.
- The dashboard and library views must correctly display the percentage progress for non-paged items.

## 4.4.0 Accessibility Needs

- The input field and its label must be properly associated for screen readers.
- Validation error messages must be announced by screen readers.
- UI must adhere to WCAG 2.1 AA contrast ratios.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-TRK-001

### 5.1.2 Rule Description

A LibraryItem is considered eligible for percentage-based tracking if its 'page_count' attribute is null, 0, or its 'type' is 'article'.

### 5.1.3 Enforcement Point

Frontend: When rendering the 'Log Session' screen. Backend: When validating the incoming session data.

### 5.1.4 Violation Handling

If the frontend fails to show the correct input, it's a bug. If the backend receives page data for an article, it should return a 400 Bad Request error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-TRK-002

### 5.2.2 Rule Description

Logged progress percentage must be a numeric value between the item's current progress and 100, inclusive.

### 5.2.3 Enforcement Point

Frontend: Client-side form validation. Backend: API request validation.

### 5.2.4 Violation Handling

Frontend displays a user-friendly error message. Backend rejects the request with a 400 Bad Request error and a descriptive message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-040

#### 6.1.1.2 Dependency Reason

Must be able to add an article to the library before progress can be logged for it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-046

#### 6.1.2.2 Dependency Reason

The core session logging flow and UI for page-based books should be established first, as this story modifies that existing flow.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-058

#### 6.1.3.2 Dependency Reason

The primary entry point for logging a session from the dashboard must exist.

## 6.2.0.0 Technical Dependencies

- The `LibraryItem` and `ReadingSession` data models in both the local database (Isar) and the backend database (Aurora) must be updated to support nullable percentage fields.
- The backend API endpoint for creating a `ReadingSession` must be updated to accept and validate percentage progress.
- The offline synchronization mechanism (REQ-OFF-001) must be able to handle syncing of percentage-based sessions.

## 6.3.0.0 Data Dependencies

- Requires the existence of a `LibraryItem` in the user's 'Currently Reading' shelf that is identified as a non-paged item.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI transition to the logging screen and the conditional rendering of the input field should be instantaneous.
- Saving the session and updating the UI should complete in under 500ms on a standard 4G connection.

## 7.2.0.0 Security

- All input must be sanitized on the backend to prevent injection attacks, even though the input type is numeric.

## 7.3.0.0 Usability

- The process should be intuitive. The user should not have to manually select between page or percentage tracking.

## 7.4.0.0 Accessibility

- Compliant with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires data model changes on both client and server.
- Involves conditional UI logic in a core feature screen.
- Backend API needs to be updated with new validation logic.
- Offline synchronization logic must be verified to handle the new data fields correctly.

## 8.3.0.0 Technical Risks

- Potential for regression bugs in the existing page-based logging flow.
- Ensuring data consistency between `currentPage` and `currentProgressPercentage` fields across the application.

## 8.4.0.0 Integration Points

- Frontend: Log Session UI, Dashboard progress display, Library item detail view.
- Backend: `POST /api/v1/sessions` endpoint.
- Database: `LibraryItem` and `ReadingSession` tables/collections.
- Offline Sync Service.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify correct input field (page vs. percentage) is shown for different item types.
- Test logging initial, subsequent, and final (100%) progress.
- Test all validation rules (value > 100, value < current, non-numeric).
- Test the offline logging and subsequent synchronization of a percentage-based session.

## 9.3.0.0 Test Data Needs

- A test user account.
- A library item with a defined page count (e.g., a book).
- A library item with no page count (e.g., an article).

## 9.4.0.0 Testing Tools

- Flutter's `flutter_test` for unit/widget tests.
- Flutter's `integration_test` for E2E tests.
- Jest for backend unit/integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented for UI logic and validation with >80% coverage
- Backend unit tests for API logic implemented with >80% coverage
- E2E integration test for the full user flow (log percentage session online and offline) is implemented and passing
- User interface reviewed and approved for both light and dark themes
- Performance requirements verified on target devices
- Backend API changes are documented in the OpenAPI 3.0 specification
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a core part of the tracking functionality. It should be prioritized soon after the basic page-based tracking is complete to deliver on the promise of flexible tracking.
- Requires coordinated work between frontend and backend developers.

## 11.4.0.0 Release Impact

This feature is a key differentiator mentioned in the project scope (REQ-TRK-001) and is essential for the initial product launch.

