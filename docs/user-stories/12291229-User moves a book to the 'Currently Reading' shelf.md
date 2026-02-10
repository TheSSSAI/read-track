# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-042 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User moves a book to the 'Currently Reading' shelf |
| As A User Story | As a user, I want to change a book's status to 'Cu... |
| User Persona | Any authenticated user (Free or Premium) who wants... |
| Business Value | Enables the core reading tracking loop, which is c... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | Core Reading Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Move a book from 'Want to Read' to 'Currently Reading'

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user viewing my 'Want to Read' shelf, which contains the book 'The Martian'

### 3.1.5 When

I perform the action to move 'The Martian' to the 'Currently Reading' shelf

### 3.1.6 Then

The book 'The Martian' is removed from the 'Want to Read' shelf view

### 3.1.7 And

The book 'The Martian' is displayed in the 'Currently Reading' section of my Dashboard.

### 3.1.8 Validation Notes

Verify UI updates on the library screen (both filtered views) and the dashboard screen. Check the backend database to confirm the item's shelf status has been updated.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Alternative Flow: Move a book from 'Read' to 'Currently Reading' (Re-reading)

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am an authenticated user viewing my 'Read' shelf, which contains the book 'Dune'

### 3.2.5 When

I perform the action to move 'Dune' to the 'Currently Reading' shelf

### 3.2.6 Then

The book 'Dune' is removed from the 'Read' shelf view

### 3.2.7 And

The book 'Dune' now appears on my 'Currently Reading' shelf view.

### 3.2.8 Validation Notes

Verify that any previous completion date associated with the book is either cleared or archived, allowing for new progress tracking.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Alternative Flow: Move a book from 'Did Not Finish' to 'Currently Reading'

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am an authenticated user viewing my 'Did Not Finish' shelf, which contains the book 'Infinite Jest'

### 3.3.5 When

I perform the action to move 'Infinite Jest' to the 'Currently Reading' shelf

### 3.3.6 Then

The book 'Infinite Jest' is removed from the 'Did Not Finish' shelf view

### 3.3.7 And

The book 'Infinite Jest' now appears on my 'Currently Reading' shelf view.

### 3.3.8 Validation Notes

Verify the UI updates correctly and the book is ready for new reading sessions to be logged.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edge Case: Action is unavailable for a book already on the target shelf

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am viewing a book that is already on my 'Currently Reading' shelf

### 3.4.5 When

I view the available actions for this book

### 3.4.6 Then

The option to move it to 'Currently Reading' is disabled or hidden.

### 3.4.7 Validation Notes

Check the context menu or action list for a book on the 'Currently Reading' shelf to ensure the redundant action is not presented to the user.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Perform move action while offline

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am an authenticated user and my device is offline

### 3.5.5 And

When my device reconnects to the internet, the change is automatically synchronized with the backend server without further user interaction.

### 3.5.6 When

I move a book to the 'Currently Reading' shelf

### 3.5.7 Then

The UI updates immediately to reflect the change locally

### 3.5.8 Validation Notes

Use network throttling tools to simulate offline mode. Verify the local database (Isar) is updated. Restore connection and verify the backend database reflects the change.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An interactive control on each library item (e.g., ellipsis menu, long-press context menu) to change shelf status.
- A toast, snackbar, or other non-blocking notification to confirm the action was successful (e.g., 'Moved to Currently Reading').

## 4.2.0 User Interactions

- User taps a control to reveal a list of shelves to move the item to.
- Selecting 'Currently Reading' from the list triggers the state change.
- The UI should provide immediate feedback that the action has been processed.

## 4.3.0 Display Requirements

- The library view must dynamically update to reflect the item's new location.
- The dashboard must update to show the newly added 'Currently Reading' item.

## 4.4.0 Accessibility Needs

- The control for moving the book must be clearly labeled for screen readers (e.g., 'More options for [Book Title]').
- The tap target for the control must meet WCAG 2.1 size requirements.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user can have multiple items on their 'Currently Reading' shelf simultaneously.

### 5.1.3 Enforcement Point

Application Logic

### 5.1.4 Violation Handling

N/A. This is a permissive rule.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Moving an item does not count towards the Free User's 20-item limit, as it modifies an existing item, not adding a new one.

### 5.2.3 Enforcement Point

Application Logic

### 5.2.4 Violation Handling

N/A. This action should never trigger the upgrade prompt.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-041

#### 6.1.1.2 Dependency Reason

A book must be added to a shelf before it can be moved.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-054

#### 6.1.2.2 Dependency Reason

Required to view and verify the book's movement between filtered shelf lists.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-056

#### 6.1.3.2 Dependency Reason

The dashboard must exist to verify that the book appears there after being moved.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-099

#### 6.1.4.2 Dependency Reason

This story is a specific implementation of the generic offline move capability defined in US-099.

## 6.2.0.0 Technical Dependencies

- The `LibraryItem` data model must be defined in both the local Isar database and the backend database, including a `shelfStatus` field (REQ-TRK-001).
- The offline synchronization mechanism (REQ-OFF-001) must be implemented.
- A secure backend API endpoint (e.g., `PATCH /api/v1/library-items/{id}`) must exist to update an item's status.

## 6.3.0.0 Data Dependencies

- Requires the user to have at least one book in their library on a shelf other than 'Currently Reading'.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI update following the move action must complete in under 200ms.
- The background synchronization process must not block or degrade the performance of the UI thread.

## 7.2.0.0 Security

- The API endpoint must validate that the authenticated user owns the library item they are attempting to modify (NFR-SEC-002).

## 7.3.0.0 Usability

- The action to move a book should be easily discoverable and require a minimal number of taps.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards, as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- State management is moderately complex, requiring updates across multiple screens (Library, Dashboard).
- The primary complexity driver is the implementation and testing of the offline-first behavior, ensuring data is saved locally and synced reliably upon reconnection.

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if the same item is modified on two different devices before a sync occurs. The 'last write wins' strategy (REQ-OFF-001) mitigates this, but must be implemented correctly.
- Ensuring the local state remains the source of truth until a successful sync confirmation is received from the backend.

## 8.4.0.0 Integration Points

- Frontend State Management (Riverpod)
- Local Database (Isar)
- Backend API (Node.js/Fastify)
- Backend Database (Aurora)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify moving a book from each possible source shelf ('Want to Read', 'Read', 'DNF') to 'Currently Reading'.
- Verify the UI updates correctly on all relevant screens.
- Verify the offline-to-online synchronization flow.
- Verify that attempting to move a book already on the 'Currently Reading' shelf has no effect.

## 9.3.0.0 Test Data Needs

- A test user account with books on 'Want to Read', 'Read', and 'DNF' shelves.

## 9.4.0.0 Testing Tools

- flutter_test (for unit and widget tests)
- integration_test (for E2E tests)
- Jest (for backend unit/integration tests)
- Network simulation tools for testing offline capabilities.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% code coverage
- Backend API unit and integration tests implemented and passing
- E2E integration test for the full user flow (including offline sync) is implemented and passing
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on target devices
- Security requirements validated via code review and endpoint testing
- Documentation for the API endpoint is updated in the OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the reading tracking epic. Its completion unblocks development on logging reading sessions (US-046, US-048, etc.).
- Requires both frontend and backend development, which can be parallelized.

## 11.4.0.0 Release Impact

- This feature is critical for the Minimum Viable Product (MVP) as it enables the core user journey of tracking a book.

