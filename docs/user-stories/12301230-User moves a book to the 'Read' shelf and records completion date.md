# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-043 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User moves a book to the 'Read' shelf and records ... |
| As A User Story | As a dedicated reader, I want to move a book I've ... |
| User Persona | Any active user (Free or Premium) who tracks their... |
| Business Value | Provides a key moment of user satisfaction, genera... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | Core Reading Journey |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Move a book from 'Currently Reading' to 'Read'

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is logged in and has a book on their 'Currently Reading' shelf

### 3.1.5 When

the user initiates the action to move the book to the 'Read' shelf, selects today's date from the completion date prompt, and confirms

### 3.1.6 Then

the system shall display a confirmation message (e.g., toast), the book shall be removed from the 'Currently Reading' shelf, the book shall appear on the 'Read' shelf with today's date as the completion date, and any relevant 'books read' goals shall be updated.

### 3.1.7 Validation Notes

Verify the book's shelf status is 'Read' in both the local Isar DB and the backend Aurora DB after sync. Check the goal progress UI for an increment.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User cancels the completion date selection

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

a user has initiated the action to move a book to the 'Read' shelf

### 3.2.5 When

the completion date prompt is displayed and the user cancels or dismisses it without confirming a date

### 3.2.6 Then

the book's shelf status shall not change, and it shall remain on its original shelf.

### 3.2.7 Validation Notes

Confirm the book is still visible on its original shelf and its status in the database is unchanged.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Date picker validation

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

the completion date prompt is displayed

### 3.3.5 When

the user interacts with the date picker

### 3.3.6 Then

the date picker must default to the current date, and the user must be prevented from selecting a future date.

### 3.3.7 Validation Notes

Attempt to select a future date in the date picker UI; it should be disabled or non-selectable.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Offline functionality

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a user is offline and has a book on their 'Currently Reading' shelf

### 3.4.5 When

the user moves the book to the 'Read' shelf and confirms a completion date

### 3.4.6 Then

the UI must update immediately to reflect the change, the update must be saved to the local Isar database, and the change must be queued for synchronization.

### 3.4.7 Validation Notes

Perform the action with network disabled. Verify the UI updates. Close and reopen the app (still offline) to confirm the change persists locally. Re-enable network and verify the data syncs to the backend.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Statistics update after moving a book to 'Read'

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

a user has a 'Total Books Completed' statistic of 'N'

### 3.5.5 When

the user successfully moves a book to the 'Read' shelf for the first time

### 3.5.6 Then

the 'Total Books Completed' statistic shall be updated to 'N+1'.

### 3.5.7 Validation Notes

Check the statistics screen (REQ-STA-000) before and after the action to confirm the value has incremented.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An interactive element (e.g., context menu, button) on a library item to change its shelf.
- A native-style date picker modal/dialog to select the completion date.
- A 'Confirm' button within the date picker dialog.
- A 'Cancel' button or dismiss action for the date picker dialog.
- A non-intrusive success indicator (e.g., toast, snackbar) upon completion.

## 4.2.0 User Interactions

- User can trigger the 'move shelf' action from their library or the book details screen.
- The date picker defaults to the current date upon opening.
- User can scroll back to select a past date.
- User cannot select a future date.

## 4.3.0 Display Requirements

- The 'Read' shelf view must display the completion date alongside each book's details.

## 4.4.0 Accessibility Needs

- All interactive elements must have proper labels for screen readers.
- The date picker must be fully navigable using accessibility services.
- UI must respect the user's OS-level font size settings (Dynamic Type) as per REQ-UIF-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-TRK-001

### 5.1.2 Rule Description

A completion date must be recorded when an item is moved to the 'Read' shelf.

### 5.1.3 Enforcement Point

Client-side, before sending the update request to the backend.

### 5.1.4 Violation Handling

The move action cannot be completed without a valid date confirmation.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-TRK-002

### 5.2.2 Rule Description

The completion date cannot be in the future.

### 5.2.3 Enforcement Point

Client-side UI (disabling dates) and backend API (validation).

### 5.2.4 Violation Handling

The UI prevents selection. The API will reject any request with a future date with a 400 Bad Request error.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-037

#### 6.1.1.2 Dependency Reason

User must be able to add a book to their library before they can change its status.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

The most common workflow starts with a book on the 'Currently Reading' shelf.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-054

#### 6.1.3.2 Dependency Reason

A 'Read' shelf view must exist to visually verify the successful completion of this story.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-060

#### 6.1.4.2 Dependency Reason

A goal tracking system must be in place to verify that goal progress is updated correctly.

## 6.2.0.0 Technical Dependencies

- Local Database (Isar) schema for 'LibraryItem' must be defined with 'shelf_status' and 'completion_date' fields.
- Backend API endpoint (e.g., PATCH /api/v1/library-items/{id}) for updating an item's status.
- Offline synchronization mechanism (REQ-OFF-001).

## 6.3.0.0 Data Dependencies

- User must have an authenticated session.
- User must have at least one book in their library that is not already on the 'Read' shelf.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI update for moving the book must feel instantaneous (<200ms).
- The backend API call must adhere to the P95 latency of <200ms (NFR-PERF-001).

## 7.2.0.0 Security

- The API endpoint must be protected and require a valid JWT.
- The backend must perform authorization checks to ensure a user can only modify their own library items (NFR-SEC-002).

## 7.3.0.0 Usability

- The process of moving a book and setting the date should be achievable in 3 taps or fewer from the book details screen.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Handling client-side state changes across multiple views (Dashboard, Library).
- Implementing the offline-first logic with Isar and the sync queue.
- Backend logic to trigger side-effects (updating goals and statistics) upon a successful status change.
- Ensuring the date picker component is robust and accessible on both platforms.

## 8.3.0.0 Technical Risks

- Potential race conditions or conflicts during offline synchronization if not handled carefully by the 'last write wins' strategy.
- Ensuring that recalculating statistics or goal progress is performant and does not cause noticeable delay.

## 8.4.0.0 Integration Points

- Local State Management (Riverpod)
- Local Database (Isar)
- Backend API (Fastify/Node.js)
- Goal Management Service (Backend)
- Statistics Service (Backend)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify moving a book from 'Currently Reading' to 'Read'.
- Verify moving a book from 'Want to Read' directly to 'Read'.
- Verify cancelling the date selection prompt leaves the book unchanged.
- Verify the entire flow works correctly while the device is offline.
- Verify data syncs correctly when the device comes back online.
- Verify goal progress and user statistics are updated correctly after the action.

## 9.3.0.0 Test Data Needs

- A test user account with books on various shelves.
- A test user account with an active 'books per year' goal.

## 9.4.0.0 Testing Tools

- flutter_test
- integration_test
- Jest (for backend)

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and widget tests implemented for frontend logic with >80% coverage
- Backend unit and integration tests implemented with >80% coverage
- E2E tests for the primary success and offline scenarios are passing
- User interface reviewed and approved by UX/UI designer
- Performance requirements (UI responsiveness, API latency) verified
- Security requirements (API authorization) validated
- Functionality verified on representative iOS and Android devices
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core piece of functionality for the reading tracking loop.
- It is a dependency for accurate goal tracking and statistics, so it should be prioritized before advanced dashboard or insights stories.

## 11.4.0.0 Release Impact

Essential for the Minimum Viable Product (MVP) release.

