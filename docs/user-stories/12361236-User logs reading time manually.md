# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-049 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User logs reading time manually |
| As A User Story | As a dedicated reader, I want to manually enter th... |
| User Persona | Any registered user ('Free User' or 'Premium User'... |
| Business Value | Increases data accuracy and user retention by prov... |
| Functional Area | Reading Tracking |
| Story Theme | Core User Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User successfully logs a past reading session

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user with a book titled 'The Great Gatsby' on my 'Currently Reading' shelf

### 3.1.5 When

I navigate to log a session, choose the 'Manual Entry' option, select 'The Great Gatsby', enter a duration of '1 hour and 30 minutes', a progress of 'page 120', and select yesterday's date

### 3.1.6 Then

a new ReadingSession record is created and associated with the book, my total reading time is increased by 90 minutes, and my goal progress is updated accordingly.

### 3.1.7 Validation Notes

Verify in the database that a new ReadingSession record exists with the correct duration, timestamp, and library_item_id. Check the user's statistics and goal progress screens to confirm they reflect the new session.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Error Condition: User attempts to log a session with zero duration

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am on the 'Manual Entry' screen for a reading session

### 3.2.5 When

I enter a duration of '0 hours and 0 minutes' and attempt to save the session

### 3.2.6 Then

the system displays an inline validation error message, such as 'Duration cannot be zero', and the session is not saved.

### 3.2.7 Validation Notes

Confirm that the UI prevents form submission and that no new ReadingSession record is created in the local or remote database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: User attempts to log a session for a future date

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the 'Manual Entry' screen for a reading session

### 3.3.5 When

I use the date picker to select a date in the future and attempt to save the session

### 3.3.6 Then

the system displays an inline validation error message, such as 'Cannot log sessions for a future date', and the session is not saved.

### 3.3.7 Validation Notes

The date picker should be constrained to not allow future date selection. If it does, form validation must catch it. Verify no session is created.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edge Case: User logs a session while offline

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a logged-in user and my device is offline

### 3.4.5 When

I manually log a reading session of '45 minutes' for a book in my library

### 3.4.6 Then

the session is successfully saved to the local Isar database, my locally-viewed statistics are updated, and a confirmation message is displayed.

### 3.4.7 Validation Notes

Use device network tools to simulate offline mode. Verify the session is persisted locally. Then, restore connectivity and verify the session is synced to the backend via the background synchronization mechanism.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Alternative Flow: User logs progress by percentage

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am a logged-in user with an article on my 'Currently Reading' shelf

### 3.5.5 When

I manually log a session for the article with a duration of '25 minutes' and progress of '75%'

### 3.5.6 Then

a new ReadingSession is created with the correct duration and percentage progress, and my statistics and goals are updated.

### 3.5.7 Validation Notes

Verify the system correctly handles percentage-based progress for items without a page count, as defined in REQ-TRK-001.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A toggle or tab to select between 'Timer' and 'Manual Entry'
- A dropdown or selector for choosing a book from the 'Currently Reading' shelf
- A duration picker (e.g., two-column scroll wheel for hours and minutes)
- A date picker, defaulting to the current date
- A numeric input field for 'Page Number' or 'Percentage'
- A 'Save Session' button

## 4.2.0 User Interactions

- Tapping 'Save Session' with valid data closes the screen and shows a success confirmation (e.g., a toast message).
- Tapping 'Save Session' with invalid data displays inline error messages next to the corresponding fields.

## 4.3.0 Display Requirements

- The form must clearly indicate which fields are required.
- The selected book's title and cover image should be displayed for confirmation.

## 4.4.0 Accessibility Needs

- All form inputs (duration picker, date picker, text fields) must have accessible labels for screen readers.
- The UI must adhere to WCAG 2.1 Level AA standards, including color contrast and support for dynamic type scaling (REQ-UIF-001).

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-TRK-001

### 5.1.2 Rule Description

A manually logged reading session must have a duration greater than zero.

### 5.1.3 Enforcement Point

Client-side form validation and server-side API validation.

### 5.1.4 Violation Handling

The client will display a user-friendly error message. The server will reject the request with a 400 Bad Request status code.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-TRK-002

### 5.2.2 Rule Description

A reading session cannot be logged for a future date or time.

### 5.2.3 Enforcement Point

Client-side form validation (constraining the date picker) and server-side API validation.

### 5.2.4 Violation Handling

The client will prevent selection or show an error. The server will reject the request with a 400 Bad Request status code.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-042

#### 6.1.1.2 Dependency Reason

User must be able to add a book to the 'Currently Reading' shelf before they can log a session for it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-097

#### 6.1.2.2 Dependency Reason

The core offline logging mechanism must be implemented for this feature to function correctly without a network connection.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

The background data synchronization mechanism is required to push offline-logged sessions to the server.

## 6.2.0.0 Technical Dependencies

- The `ReadingSession` data model must be defined and implemented (REQ-TRK-001).
- The backend API must have an endpoint to create a `ReadingSession` record.
- The Isar local database schema must be set up for offline storage (REQ-OFF-001).

## 6.3.0.0 Data Dependencies

- Requires access to the user's list of `LibraryItem`s on their 'Currently Reading' shelf.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The manual entry form must load in under 500ms.
- The save operation (API call) must have a P95 latency of less than 200ms as per REQ-PER-001.

## 7.2.0.0 Security

- All user-provided input must be sanitized and validated on the backend to prevent injection attacks (NFR-SEC-006).
- The API endpoint for creating a session must be protected and ensure the user can only log sessions for their own library items.

## 7.3.0.0 Usability

- The duration picker should be intuitive and easy to use, minimizing the number of taps required to enter a common duration.
- Error messages should be clear, concise, and guide the user to correct the input.

## 7.4.0.0 Accessibility

- The feature must be fully usable via screen reader and keyboard navigation equivalents.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the offline-first capability using the Isar database and ensuring robust synchronization logic.
- Building an intuitive and accessible custom UI for duration and date picking.
- Coordinating state updates across multiple features (Goals, Statistics) after a session is logged.

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if the user logs sessions on multiple devices while offline. The 'last write wins' strategy (REQ-OFF-001) must be implemented carefully, though it's less critical for creating new, distinct sessions.

## 8.4.0.0 Integration Points

- Local Database (Isar): For offline storage.
- Backend API: For creating the `ReadingSession` record.
- Goal Management System: To update progress after a successful log.
- Statistics Calculation Service: To update user stats.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E
- Usability

## 9.2.0.0 Test Scenarios

- Successfully log a session online and verify data updates.
- Successfully log a session offline and verify local persistence.
- Verify successful data sync after reconnecting to the network.
- Test all validation rules (zero duration, future date, invalid page number).
- Test logging for both page-based and percentage-based items.

## 9.3.0.0 Test Data Needs

- A test user account with items on the 'Currently Reading' shelf.
- Test items with and without a defined page count.

## 9.4.0.0 Testing Tools

- `flutter_test` for unit and widget tests.
- `integration_test` package for E2E tests.
- Backend testing framework (Jest) for API tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented and passing with >= 80% coverage
- Integration testing for online and offline scenarios completed successfully
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on target devices
- Security requirements validated via code review and backend testing
- Documentation for the API endpoint is updated in OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a core part of the reading tracking feature and a high-value alternative to the timer.
- Should be developed in conjunction with or immediately after the timer-based logging (US-048) to ensure a cohesive user experience.

## 11.4.0.0 Release Impact

This is a fundamental feature for the initial release (MVP). The app would feel incomplete without it.

