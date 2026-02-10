# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-044 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User moves a book to the 'Did Not Finish' shelf |
| As A User Story | As a reader managing my personal library, I want t... |
| User Persona | Any authenticated user (Free or Premium) who is ma... |
| Business Value | Improves the accuracy and organization of the user... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | Library Organization |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Move a book from 'Currently Reading' to 'Did Not Finish' shelf

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is logged in and has a book on their 'Currently Reading' shelf

### 3.1.5 When

the user selects the option to change the book's shelf status and chooses 'Did Not Finish'

### 3.1.6 Then

the book is immediately removed from the 'Currently Reading' shelf in the UI

### 3.1.7 And

the book is no longer counted as an active book for goal progress calculations.

### 3.1.8 Validation Notes

Verify the UI updates instantly. Check the local database (Isar) for the updated shelf status and completion date. After sync, verify the change in the backend database.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User cancels the move to 'Did Not Finish'

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

the user has initiated moving a book to the 'Did Not Finish' shelf

### 3.2.5 When

the 'Stopped Reading' date selection dialog is displayed and the user taps 'Cancel' or dismisses the dialog

### 3.2.6 Then

the move operation is aborted

### 3.2.7 And

no changes are saved locally or sent to the backend.

### 3.2.8 Validation Notes

Confirm that the book's status in the UI and local database remains unchanged.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Move a book to 'Did Not Finish' while offline

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user's device is offline

### 3.3.5 When

the user moves a book to the 'Did Not Finish' shelf and confirms a date

### 3.3.6 Then

the change is saved to the local device database (Isar)

### 3.3.7 And

the change is queued for synchronization once network connectivity is restored.

### 3.3.8 Validation Notes

Perform the action in airplane mode. Verify the UI change. Reconnect to the internet and verify the data syncs successfully to the backend without further user interaction.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

API fails during synchronization of the shelf change

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a user moves a book to 'Did Not Finish' and the app attempts to sync with the backend

### 3.4.5 When

the backend API returns an error (e.g., 5xx server error)

### 3.4.6 Then

the UI change remains (optimistic update), showing the book on the 'Did Not Finish' shelf

### 3.4.7 And

the background sync mechanism is responsible for retrying the update.

### 3.4.8 Validation Notes

Use a tool like Charles Proxy to simulate a network failure for the specific API endpoint and verify the app's graceful handling and retry behavior.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A menu option (e.g., in a dropdown or context menu) labeled 'Move to Shelf'
- A sub-option labeled 'Did Not Finish'
- A native date picker dialog to select the 'Stopped Reading' date
- Confirmation and Cancel buttons within the date picker dialog
- A toast or snackbar notification for user feedback (e.g., 'Book moved successfully').

## 4.2.0 User Interactions

- User can access the move option from a book's detail page or a context menu in the library list.
- The date picker should default to today's date but allow selection of past dates.
- The UI should update optimistically without waiting for the backend response to feel instantaneous.

## 4.3.0 Display Requirements

- The 'Did Not Finish' shelf view must display the books that have been moved there.
- The book's detail page, when on the DNF shelf, should display the 'Stopped Reading' date.

## 4.4.0 Accessibility Needs

- All interactive elements (buttons, menus) must have proper labels for screen readers.
- The date picker must be fully accessible and navigable using accessibility services.
- UI must respect the user's OS-level font size settings (Dynamic Type).

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Moving an item to 'Read' or 'DNF' must prompt for a completion date.", 'enforcement_point': "Client-side, immediately after the user selects the 'Did Not Finish' option.", 'violation_handling': 'The move operation cannot be completed without a date being provided and confirmed.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-037

#### 6.1.1.2 Dependency Reason

User must be able to add a book to the library before they can manage its shelf status.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

A book must be on a shelf (e.g., 'Currently Reading') to be moved from it.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-054

#### 6.1.3.2 Dependency Reason

A filterable library view is required to verify that the book has been successfully moved to the 'Did Not Finish' shelf.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-101

#### 6.1.4.2 Dependency Reason

The core offline synchronization logic must be implemented for this feature to work reliably offline.

## 6.2.0.0 Technical Dependencies

- The `LibraryItem` data model in both the local Isar database and the backend Aurora database must include a `shelfStatus` enum and a nullable `completionDate` field.
- A backend API endpoint (e.g., `PATCH /api/v1/library-items/{id}`) must exist to update a book's status and completion date.

## 6.3.0.0 Data Dependencies

- Requires an existing library item associated with the authenticated user.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI update after confirming the move must be perceived as instant (<200ms).
- The background sync operation should not block the UI thread or degrade app performance.

## 7.2.0.0 Security

- The API endpoint for updating the library item must be protected and ensure a user can only modify their own items.

## 7.3.0.0 Usability

- The action should be discoverable and require a minimal number of taps to complete.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- This is a standard CRUD operation with state change.
- Leverages existing UI components for library display and menus.
- The main complexity lies in correctly handling the offline state, which is part of a separate, broader story (US-101).

## 8.3.0.0 Technical Risks

- Potential for sync conflicts if the same item is modified on multiple devices while one is offline. The 'last write wins' strategy (REQ-OFF-001) should be applied.

## 8.4.0.0 Integration Points

- Local Database (Isar): Writing the updated status and date.
- Backend API: Sending the update to the server.
- State Management (Riverpod): Notifying the UI to rebuild relevant widgets.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify moving a book from 'Currently Reading' to 'DNF'.
- Verify moving a book from 'Want to Read' to 'DNF'.
- Verify canceling the move operation.
- Verify the entire flow works correctly while the device is in airplane mode.
- Verify successful data synchronization after reconnecting to the network.
- Verify graceful failure handling when the backend API is unavailable.

## 9.3.0.0 Test Data Needs

- A test user account with books on the 'Currently Reading' and 'Want to Read' shelves.

## 9.4.0.0 Testing Tools

- flutter_test
- integration_test
- A mock API server or network proxy for testing error conditions.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented and passing with >= 80% coverage for new code
- Integration testing completed successfully for both online and offline scenarios
- User interface reviewed and approved for both light and dark themes
- Performance requirements verified
- Security requirements validated via code review of the API endpoint
- Documentation for the API endpoint is updated in the OpenAPI specification
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core library management feature. It can be developed in conjunction with US-043 ('Move to Read shelf') as they share significant implementation overlap.
- Dependent on the completion of the basic library view and data synchronization framework.

## 11.4.0.0 Release Impact

- Essential for the initial release (MVP) as it completes the basic lifecycle of a library item.

