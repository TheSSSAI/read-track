# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-099 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User moves an item between shelves while offline |
| As A User Story | As a dedicated reader, I want to move books and ar... |
| User Persona | Any registered user (Free or Premium) who wants to... |
| Business Value | Enhances application reliability and user satisfac... |
| Functional Area | Reading Tracking & Offline Functionality |
| Story Theme | Offline Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Move item between shelves while offline

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The user is logged in and the application is in offline mode.

### 3.1.5 When

The user moves a library item from one shelf (e.g., 'Want to Read') to another (e.g., 'Currently Reading').

### 3.1.6 Then

The UI must update instantly to reflect the item on the new shelf.

### 3.1.7 And

The local record for the item must be flagged as 'pending sync'.

### 3.1.8 Validation Notes

Verify by checking the UI and querying the local Isar database directly using a debug tool.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Move item to a 'completed' shelf (Read/DNF) while offline

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The user is logged in and the application is in offline mode.

### 3.2.5 When

The user moves an item from 'Currently Reading' to the 'Read' shelf.

### 3.2.6 Then

The application must prompt the user to enter a completion date.

### 3.2.7 And

The local record must be flagged as 'pending sync'.

### 3.2.8 Validation Notes

This also applies to moving an item to the 'Did Not Finish (DNF)' shelf. Verify the prompt appears and all required data is saved locally.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Seamless user experience during offline action

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

The user is performing a shelf-change action.

### 3.3.5 When

The device's network connectivity is lost or unavailable.

### 3.3.6 Then

The user interface and interaction flow must be identical to the online experience.

### 3.3.7 And

The application must not display any blocking error messages or connectivity warnings for this action.

### 3.3.8 Validation Notes

Test by toggling airplane mode on the device during the operation. The user should not notice a difference in the app's immediate response.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Data is correctly queued for synchronization

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A user has moved an item between shelves while offline.

### 3.4.5 When

The device regains network connectivity.

### 3.4.6 Then

The background synchronization mechanism (defined in US-101) must identify and process the 'pending sync' item.

### 3.4.7 Validation Notes

This criterion validates the hand-off to the sync process. The sync process itself is covered by US-101.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Multiple offline changes to the same item

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

The user is offline.

### 3.5.5 And

Only the final state of the item needs to be synced when the device comes online.

### 3.5.6 When

The user first moves the book to 'Currently Reading', and later moves the same book to 'Read' (with a completion date), all while still offline.

### 3.5.7 Then

The local Isar database must reflect the final state of the item ('Read' shelf with completion date).

### 3.5.8 Validation Notes

Ensures that intermediate states are not queued, only the most recent version of the local object is marked for sync.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Existing library view components (shelves, item cards).
- Existing context menu or drag-and-drop interface for moving items.
- Existing date picker modal for completion dates.

## 4.2.0 User Interactions

- The interaction for moving an item must be identical whether online or offline.
- The application should provide immediate visual feedback that the move was successful.

## 4.3.0 Display Requirements

- The library view must update instantly to reflect the new shelf location of the item.

## 4.4.0 Accessibility Needs

- All interactive elements for moving items must adhere to WCAG 2.1 AA standards, including proper labeling for screen readers and sufficient tap target sizes.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-OFF-001

### 5.1.2 Rule Description

All data modifications made while offline must be stored locally first before any attempt to synchronize with the server.

### 5.1.3 Enforcement Point

Client-side data repository layer.

### 5.1.4 Violation Handling

N/A - This is a core architectural principle for offline mode.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-OFF-002

### 5.2.2 Rule Description

Every offline modification must be timestamped on the client to support the 'last write wins' conflict resolution strategy (as per REQ-OFF-001).

### 5.2.3 Enforcement Point

Client-side data repository when saving a change to a local item.

### 5.2.4 Violation Handling

If a timestamp is missing, the sync process should reject the update to prevent data corruption.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-098

#### 6.1.1.2 Dependency Reason

The user's library must be available and viewable offline before items can be manipulated offline.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

The core UI and online logic for moving an item to 'Currently Reading' should exist to be adapted for offline use.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-043

#### 6.1.3.2 Dependency Reason

The core UI and online logic for moving an item to 'Read' should exist to be adapted for offline use.

## 6.2.0.0 Technical Dependencies

- Isar database (REQ-OFF-001) must be set up and integrated into the application.
- A client-side repository pattern that abstracts data sources (local vs. remote) must be in place.
- The data model for `LibraryItem` in Isar must include fields for `syncStatus` and `lastModifiedTimestamp`.

## 6.3.0.0 Data Dependencies

- Requires the user's library data to be pre-populated in the local Isar database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The local database write operation must complete in under 50ms to avoid blocking the UI thread.
- The UI update reflecting the move must be visually instantaneous (<100ms).

## 7.2.0.0 Security

- While the offline action has no direct security implications, the subsequent sync operation must be authenticated via the user's JWT (as per REQ-CIF-001).

## 7.3.0.0 Usability

- The offline experience must be indistinguishable from the online experience for this specific action.

## 7.4.0.0 Accessibility

- Adherence to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires robust local state management.
- Interaction with the background synchronization service adds complexity.
- Ensuring the client-side timestamp is correctly generated and stored for conflict resolution.
- Requires careful testing of network state transitions.

## 8.3.0.0 Technical Risks

- Potential for race conditions if the sync process triggers while the user is making another offline change.
- Incorrect implementation of the local data flagging could lead to changes being missed by the sync service or synced multiple times.

## 8.4.0.0 Integration Points

- Local Isar Database: Writing the updated `LibraryItem`.
- Background Synchronization Service: The updated item must be discoverable by this service for eventual upload.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Move item between shelves with network disabled, then verify local DB state.
- Move item to 'Read' shelf with network disabled, verify date prompt and local DB state.
- Full E2E Test: Go offline, move item, go online, verify UI remains correct and backend data is updated successfully.
- Conflict Resolution Test: Modify an item online, then sync an older offline change for the same item and verify 'last write wins' is respected on the backend.

## 9.3.0.0 Test Data Needs

- A pre-populated user library with items on various shelves.

## 9.4.0.0 Testing Tools

- Flutter's `flutter_test` for unit/widget tests.
- Flutter's `integration_test` package for E2E tests.
- Network mocking tools to simulate offline conditions reliably.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >80% coverage for the new logic
- Integration testing for local database persistence completed successfully
- E2E tests for the offline-to-online sync scenario are implemented and passing
- User interface reviewed and approved for seamlessness
- Performance requirements for local write speed are verified
- Documentation for the offline data model (sync flags, timestamps) is updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is tightly coupled with US-101 ('User's offline changes are synced automatically'). It is highly recommended to plan them in the same sprint to allow for efficient end-to-end testing.
- Requires a developer with experience in local database management (Isar) and state management in a disconnected environment.

## 11.4.0.0 Release Impact

This is a critical feature for the core value proposition of a robust, offline-capable reading tracker.

