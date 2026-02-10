# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-046 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User logs reading progress by entering a page numb... |
| As A User Story | As an active reader, I want to log my reading prog... |
| User Persona | Any registered user (Free or Premium) who has adde... |
| Business Value | This is a core user engagement feature that genera... |
| Functional Area | Reading Tracking |
| Story Theme | Library and Progress Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User logs progress for the first time on a book

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has a book with 350 pages on their 'Currently Reading' shelf with no prior progress logged

### 3.1.5 When

the user navigates to the log progress screen, enters '50' as the page number, and confirms

### 3.1.6 Then

a new ReadingSession record is created for 50 pages, the book's overall progress is updated to page 50, and a success confirmation is displayed to the user.

### 3.1.7 Validation Notes

Verify in the database that a new session is created and the LibraryItem's progress field is updated. The UI should reflect the new progress.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: User logs subsequent progress on a book

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a user has a book with 350 pages on their 'Currently Reading' shelf with last logged progress at page 50

### 3.2.5 When

the user enters '85' as the new page number and confirms

### 3.2.6 Then

a new ReadingSession record is created for 35 pages (85 - 50), the book's overall progress is updated to page 85, and a success confirmation is displayed.

### 3.2.7 Validation Notes

Verify the calculation is correct and the database records are updated. The UI should show the new overall progress.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: User enters a page number lower than current progress

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

a user's last logged progress for a book is at page 85

### 3.3.5 When

the user attempts to log progress by entering '70'

### 3.3.6 Then

the system prevents the submission and displays a clear error message, such as 'Page number must be greater than your last logged page (85).'

### 3.3.7 Validation Notes

The log progress button should be disabled or the API call should be rejected with a 400-level error. No new session should be created.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: User enters a page number exceeding the book's total pages

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a user is logging progress for a book with a total of 350 pages

### 3.4.5 When

the user attempts to log progress by entering '400'

### 3.4.6 Then

the system prevents the submission and displays a clear error message, such as 'Page number cannot exceed the total page count (350).'

### 3.4.7 Validation Notes

Client-side and server-side validation must prevent this. No new session should be created.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Edge Case: User finishes a book by logging the last page

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user is logging progress for a book with a total of 350 pages and is currently at page 340

### 3.5.5 When

the user enters '350' and confirms

### 3.5.6 Then

a new ReadingSession is created for 10 pages, the book's progress is updated to 100%, and the user is prompted to move the book to the 'Read' shelf.

### 3.5.7 Validation Notes

This tests the integration with the 'move to shelf' functionality (US-043).

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Alternative Flow: User logs progress while offline

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

the user's device is offline and they have a book cached locally

### 3.6.5 When

the user logs progress by entering a valid page number

### 3.6.6 Then

the reading session is saved to the local Isar database, the UI updates to show the new progress, and the data is queued for synchronization.

### 3.6.7 Validation Notes

Verify with the device offline. Then, reconnect to the internet and verify the session data is successfully synced to the backend without user intervention (US-101).

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Numeric input field for page number
- A 'Log Progress' or 'Save' button
- Text displaying the book's total page count for context (e.g., 'out of 350 pages')
- Error message display area

## 4.2.0 User Interactions

- Tapping on a 'Log Progress' action for a book on the 'Currently Reading' shelf opens a modal or dedicated screen.
- The numeric keyboard should be displayed automatically when the input field is focused.
- The 'Log Progress' button should be disabled until a valid number is entered.
- Upon successful submission, a confirmation toast/snackbar is shown, and the modal/screen is dismissed.

## 4.3.0 Display Requirements

- The input field should clearly indicate what information is required ('Page number').
- The user's last logged page should be visible for reference.
- Error messages must be specific, user-friendly, and displayed inline with the input field.

## 4.4.0 Accessibility Needs

- All input fields and buttons must have ARIA labels for screen readers.
- Error messages must be programmatically associated with their respective inputs and announced by screen readers.
- The UI must respect the user's OS-level font size settings (Dynamic Type) as per REQ-UIF-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-TRK-01

### 5.1.2 Rule Description

A new page number entry must be strictly greater than the previously recorded page number for the same LibraryItem.

### 5.1.3 Enforcement Point

Client-side validation (UI) and Server-side validation (API).

### 5.1.4 Violation Handling

The client UI displays an inline error message. The server API rejects the request with a 400 Bad Request status code and an error message.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-TRK-02

### 5.2.2 Rule Description

A page number entry cannot exceed the total page count defined for the LibraryItem.

### 5.2.3 Enforcement Point

Client-side validation (UI) and Server-side validation (API).

### 5.2.4 Violation Handling

The client UI displays an inline error message. The server API rejects the request with a 400 Bad Request status code and an error message.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-TRK-03

### 5.3.2 Rule Description

Progress can only be logged for items on the 'Currently Reading' shelf.

### 5.3.3 Enforcement Point

The UI should only present the option to log progress for these items. The server API must validate the item's shelf status before creating a session.

### 5.3.4 Violation Handling

The server API rejects the request with a 403 Forbidden or 400 Bad Request status code.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-037

#### 6.1.1.2 Dependency Reason

User must be able to search for and add a book to their library before they can log progress on it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

A book must be on the 'Currently Reading' shelf to be eligible for progress logging.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-097

#### 6.1.3.2 Dependency Reason

The core offline data storage and queuing mechanism must be in place to support offline progress logging.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for creating a ReadingSession.
- Flutter state management (Riverpod) for handling UI state and API calls.
- Local database (Isar) setup for offline storage.

## 6.3.0.0 Data Dependencies

- Requires access to the user's LibraryItem data, specifically items on the 'Currently Reading' shelf and their total page count.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for logging a session must have a P95 latency of less than 200ms (NFR-PERF-001).
- The UI for logging progress must load and become interactive in under 500ms.

## 7.2.0.0 Security

- All API requests must be authenticated via JWT.
- The backend must perform server-side validation on all input to prevent invalid data and protect against injection attacks (NFR-SEC-006).

## 7.3.0.0 Usability

- The process of logging progress should be quick and require minimal taps from the dashboard or book details screen.
- Feedback (success, error) must be immediate and clear.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards (REQ-UIF-001).

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity comes from implementing the offline-first capability.
- Requires careful state management to handle online/offline states and data synchronization.
- Backend logic must be transactional to ensure data integrity when creating a session and updating the parent library item.

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if the user logs progress on multiple devices while one is offline. The 'last write wins' strategy (REQ-OFF-001) must be implemented correctly.
- Ensuring a seamless user experience when transitioning between online and offline states.

## 8.4.0.0 Integration Points

- Backend Database (Amazon Aurora): Creating `ReadingSession` records.
- Local Database (Isar): Storing sessions created offline.
- Goal Management System: This action will trigger updates to goal progress.
- Statistics Engine: This action provides the raw data for calculating statistics.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Usability

## 9.2.0.0 Test Scenarios

- Verify first-time progress logging.
- Verify subsequent progress logging and correct page calculation.
- Test all validation rules (page too low, page too high, non-numeric input).
- Test finishing a book and the subsequent prompt.
- Test the entire offline workflow: go offline, log progress, go online, verify sync.
- Test on various screen sizes to ensure UI adapts correctly.

## 9.3.0.0 Test Data Needs

- User accounts (Free and Premium).
- Library items with no progress.
- Library items with existing progress.
- Library items with a known total page count.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test` for unit/widget tests.
- Flutter: `integration_test` for E2E tests.
- Backend: `Jest` for unit/integration tests.
- Postman or similar for direct API testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and widget tests implemented for Flutter components with >80% coverage
- Backend unit and integration tests implemented with >80% coverage
- E2E tests for the happy path and offline scenario are passing
- User interface reviewed and approved by the design team
- Performance requirements (API latency) verified
- Security requirements (input validation, auth) validated
- Functionality verified in the staging environment on both iOS and Android physical devices

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the core user loop. It is a prerequisite for statistics and goal tracking features.
- Requires coordinated work between frontend and backend developers.
- The offline component may require dedicated testing time.

## 11.4.0.0 Release Impact

This feature must be included in the initial (v1.0) release as it is fundamental to the application's purpose.

