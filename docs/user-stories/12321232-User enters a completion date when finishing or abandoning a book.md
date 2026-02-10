# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-045 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User enters a completion date when finishing or ab... |
| As A User Story | As a diligent reader, I want to be prompted to ent... |
| User Persona | Any registered user (Free or Premium) who wants to... |
| Business Value | Enhances the core tracking functionality by captur... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | Core User Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User marks a book as 'Read' and selects a completion date

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has a book on their 'Currently Reading' shelf

### 3.1.5 When

the user initiates an action to move the book to the 'Read' shelf

### 3.1.6 Then

a date picker dialog is presented, defaulted to the current date, and the user selects a valid date (today or in the past) and confirms

### 3.1.7 Validation Notes

Verify the book's shelf status is updated to 'Read' and the selected completion date is persisted in both the local (Isar) and backend (Aurora) databases.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: User marks a book as 'DNF' and selects a completion date

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a user has a book on their 'Currently Reading' shelf

### 3.2.5 When

the user initiates an action to move the book to the 'Did Not Finish (DNF)' shelf

### 3.2.6 Then

a date picker dialog is presented, and the user selects a valid date and confirms

### 3.2.7 Validation Notes

Verify the book's shelf status is updated to 'DNF' and the selected completion date is saved correctly.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Edge Case: User cancels or skips entering a completion date

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is presented with the completion date picker dialog after moving a book

### 3.3.5 When

the user taps 'Cancel', 'Skip', or dismisses the dialog without confirming a date

### 3.3.6 Then

the book's shelf is still updated to the target shelf ('Read' or 'DNF')

### 3.3.7 Validation Notes

Verify that the completion date field for the book remains null in the database.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: User attempts to select a future date

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

the user is interacting with the completion date picker

### 3.4.5 When

the user attempts to select a date in the future

### 3.4.6 Then

the UI prevents the selection of the future date, and the 'Confirm' button is disabled until a valid date is chosen

### 3.4.7 Validation Notes

Automated UI tests should confirm that future dates are unselectable or greyed out.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Alternative Flow: User reverts a finished book's status

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

a user has a book on their 'Read' or 'DNF' shelf with a saved completion date

### 3.5.5 When

the user moves that book back to the 'Currently Reading' or 'Want to Read' shelf

### 3.5.6 Then

the book's shelf is updated, and the associated completion date for that book is cleared (set to null)

### 3.5.7 Validation Notes

Verify in the database that the completionDate field is nullified after the move.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Offline Behavior: User sets completion date while offline

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

the user's device is offline and they have a book on their 'Currently Reading' shelf

### 3.6.5 When

the user moves the book to the 'Read' shelf and selects a completion date

### 3.6.6 Then

the shelf change and completion date are saved to the local Isar database immediately, and the UI reflects the change

### 3.6.7 Validation Notes

After the device reconnects to the internet, verify that the data is automatically synced to the backend without user intervention.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Modal dialog or bottom sheet for date selection
- Native-style date picker component
- A 'Confirm' or 'Save' button
- A 'Cancel' or 'Skip' button

## 4.2.0 User Interactions

- The date picker dialog must appear automatically after the user confirms a move to the 'Read' or 'DNF' shelf.
- The date picker should default to the current system date.
- Users can scroll back in time to select a past date.
- Future dates must be disabled and unselectable.

## 4.3.0 Display Requirements

- The dialog should have a clear title, e.g., 'When did you finish?'.

## 4.4.0 Accessibility Needs

- The date picker component must be fully accessible, supporting screen readers (e.g., VoiceOver, TalkBack) and dynamic type scaling as per REQ-UIF-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A completion date can only be set for a LibraryItem with a shelf status of 'Read' or 'Did Not Finish'.

### 5.1.3 Enforcement Point

Client-side logic and Backend API validation.

### 5.1.4 Violation Handling

If a request is made to set a completion date for a book on another shelf, the API should return a 400 Bad Request error. The client UI should prevent this state from occurring.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The completion date cannot be in the future.

### 5.2.3 Enforcement Point

Client-side UI validation and Backend API validation.

### 5.2.4 Violation Handling

The client UI will prevent selection. The API will reject any request with a future date and return a 400 Bad Request error.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Moving a book from a 'finished' state ('Read' or 'DNF') to an 'unfinished' state ('Currently Reading', 'Want to Read') must nullify the completion date.

### 5.3.3 Enforcement Point

Client-side logic and Backend API logic.

### 5.3.4 Violation Handling

The system automatically clears the date upon this state transition to maintain data integrity.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-043

#### 6.1.1.2 Dependency Reason

This story extends the functionality of moving a book to the 'Read' shelf.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-044

#### 6.1.2.2 Dependency Reason

This story extends the functionality of moving a book to the 'DNF' shelf.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-099

#### 6.1.3.2 Dependency Reason

The offline behavior of this story relies on the core offline shelf-moving capability being implemented.

## 6.2.0.0 Technical Dependencies

- The `LibraryItem` data model in the client (Isar) and backend (Aurora/Prisma) must be updated to include a nullable `completionDate` field (DateTime/Timestamp).
- The API endpoint for updating a `LibraryItem` must be modified to accept and validate the `completionDate` field.

## 6.3.0.0 Data Dependencies

- Requires an existing `LibraryItem` in the user's library to perform the action on.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The date picker dialog must appear in under 200ms after the user's action.

## 7.2.0.0 Security

- The backend API must validate that the user making the request is the owner of the `LibraryItem` being modified.

## 7.3.0.0 Usability

- The flow should be seamless and intuitive. Defaulting the date picker to the current date is critical for a good user experience.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards, particularly for the date picker component.

## 7.5.0.0 Compatibility

- The date picker UI should render correctly on all supported iOS and Android versions and screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium-Low

## 8.2.0.0 Complexity Factors

- Requires coordinated changes across frontend, backend, and local/remote databases.
- Offline synchronization logic for the new `completionDate` field needs careful implementation and testing.
- Schema migration for both Isar (local) and Aurora (remote) databases must be handled.

## 8.3.0.0 Technical Risks

- Potential for data sync conflicts if not handled correctly, though the 'last write wins' strategy (REQ-OFF-001) should mitigate this.
- Improper handling of the schema migration could lead to data loss for existing users.

## 8.4.0.0 Integration Points

- Client State Management (Riverpod)
- Local Database (Isar)
- Backend API (Fastify/Node.js)
- Remote Database (Aurora/Prisma)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify date selection and saving for both 'Read' and 'DNF' shelves.
- Verify skipping/canceling the dialog correctly updates the shelf but leaves the date null.
- Verify moving a book back from 'Read' to 'Currently Reading' clears the date.
- Perform the entire flow while offline, then reconnect and verify successful data synchronization.
- Attempt to select a future date and confirm it is not possible.

## 9.3.0.0 Test Data Needs

- A test user account with books on the 'Currently Reading' shelf.

## 9.4.0.0 Testing Tools

- flutter_test
- integration_test
- Jest

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage for new logic
- Integration testing between client and backend completed successfully
- User interface reviewed and approved for both light/dark themes
- Performance requirements verified
- Backend API endpoint is secured and validated
- Database migration scripts are written and tested
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be scheduled in a sprint immediately following the implementation of the basic shelf management features (US-043, US-044).
- Backend and frontend tasks can be worked on in parallel once the API contract for the `completionDate` field is agreed upon.

## 11.4.0.0 Release Impact

This is a core feature enhancement that significantly improves the value of the tracking functionality. It is a prerequisite for future features like 'Year in Review' summaries.

