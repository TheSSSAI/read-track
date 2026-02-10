# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-056 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views 'Currently Reading' books on the dashbo... |
| As A User Story | As a Registered User (Free or Premium), I want to ... |
| User Persona | Any authenticated user (Free or Premium) who has a... |
| Business Value | Increases user engagement and retention by making ... |
| Functional Area | Dashboard |
| Story Theme | Core User Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display of a single 'Currently Reading' book

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is logged in and has exactly one book on their 'Currently Reading' shelf

### 3.1.5 When

they navigate to the Dashboard screen

### 3.1.6 Then

a 'Currently Reading' section is displayed containing one item representing that book, showing its cover image, title, author, and a visual progress indicator (e.g., 'Page 50 of 300' or a progress bar).

### 3.1.7 Validation Notes

Verify the correct book details and progress are fetched and rendered. Progress should reflect the last logged session.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Display of multiple 'Currently Reading' books

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the user is logged in and has more than one book on their 'Currently Reading' shelf

### 3.2.5 When

they navigate to the Dashboard screen

### 3.2.6 Then

the 'Currently Reading' section displays all books in a horizontally scrollable list, with each item showing its respective cover, title, author, and progress.

### 3.2.7 Validation Notes

Confirm that all books from the shelf are present and that the list scrolls horizontally without affecting the vertical layout of the dashboard.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Navigating to log a session

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the user is on the Dashboard and their 'Currently Reading' book(s) are displayed

### 3.3.5 When

they tap on a specific book item in the 'Currently Reading' section

### 3.3.6 Then

the application navigates them to the screen for logging a new reading session for that specific book.

### 3.3.7 Validation Notes

This directly implements US-058. Tapping on Book A should lead to the logging screen pre-filled or contextualized for Book A.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Empty state for 'Currently Reading' shelf

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

the user is logged in and has zero books on their 'Currently Reading' shelf

### 3.4.5 When

they navigate to the Dashboard screen

### 3.4.6 Then

the 'Currently Reading' section displays a user-friendly message (e.g., 'You're not reading anything right now. Add a book to get started!') and a clear call-to-action to add a book.

### 3.4.7 Validation Notes

Verify the message is clear and the CTA navigates to the book search/add functionality.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Offline data display

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the user is logged in, has books on their 'Currently Reading' shelf, and the device is offline

### 3.5.5 When

they open the app and view the Dashboard screen

### 3.5.6 Then

the 'Currently Reading' section is successfully populated with the book data stored in the local database (Isar).

### 3.5.7 Validation Notes

Test by enabling airplane mode after data has been synced once. The dashboard should still load this section correctly.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Graceful handling of missing book cover

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a book on the 'Currently Reading' shelf has a null or invalid cover image URL

### 3.6.5 When

the Dashboard screen is displayed

### 3.6.6 Then

the item for that book displays a default placeholder image instead of a broken image icon or an error.

### 3.6.7 Validation Notes

Manually set a book's cover URL to null in the test data to verify the placeholder is shown.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section/card on the dashboard for 'Currently Reading'.
- A horizontally scrollable list/carousel for displaying multiple book items.
- For each book: cover image, title text, author text, progress bar or text.
- An empty state message with a call-to-action button/link.
- A placeholder image for books without a cover.

## 4.2.0 User Interactions

- The list of books must be horizontally scrollable if it overflows the screen width.
- Each book item in the list must be tappable to initiate the 'log session' flow.

## 4.3.0 Display Requirements

- The component must be placed prominently on the dashboard as per REQ-DSH-001.
- Book titles that are too long should be truncated gracefully with an ellipsis.

## 4.4.0 Accessibility Needs

- All images (book covers, placeholders) must have appropriate content descriptions (e.g., 'Book cover for [Book Title]').
- Tappable elements must have a minimum touch target size of 44x44dp.
- The component must be navigable using screen readers (VoiceOver/TalkBack).

# 5.0.0 Business Rules

*No items available*

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to access the dashboard.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-005

#### 6.1.2.2 Dependency Reason

User must be able to log in to access the dashboard.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-042

#### 6.1.3.2 Dependency Reason

User must be able to add/move a book to the 'Currently Reading' shelf to populate this view.

## 6.2.0.0 Technical Dependencies

- Local database schema for 'LibraryItem' must be defined in Isar (REQ-OFF-001).
- State management solution (Riverpod) must be in place to manage the state of the user's library.
- Backend API endpoint to fetch the user's library must be available for synchronization.

## 6.3.0.0 Data Dependencies

- Requires access to the user's library data, specifically items on the 'Currently Reading' shelf.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 'Currently Reading' data must be loaded from the local cache to ensure the dashboard is interactive in under 1.5 seconds (NFR-PERF-002).

## 7.2.0.0 Security

- Data must be fetched for the authenticated user only. API requests must be authorized using the user's JWT.

## 7.3.0.0 Usability

- The component should be intuitive, providing a clear and immediate overview of what the user is reading.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The UI component must render correctly on all supported iOS and Android screen sizes and versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a robust offline-first data fetching strategy (cache then network).
- Designing a responsive and polished UI for the horizontal carousel.
- Managing the various states (loading, empty, data, error) effectively using Riverpod.

## 8.3.0.0 Technical Risks

- Potential for UI jank in the horizontal scroll list if not implemented efficiently (e.g., using ListView.builder).
- Synchronization logic between local cache and backend could introduce data consistency issues if not handled carefully.

## 8.4.0.0 Integration Points

- Local Isar database for data storage.
- Backend library service API for data synchronization.
- Navigation service to transition to the 'Log Reading Session' screen.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify UI with 0, 1, and 5 books on the shelf.
- Test tap interaction and correct navigation.
- Test offline functionality by disabling network connectivity.
- Test UI with a book that has a very long title.
- Test UI with a book that is missing a cover image.
- Verify screen reader navigation and labels.

## 9.3.0.0 Test Data Needs

- User accounts with varying numbers of books on the 'Currently Reading' shelf.
- A book record with a null value for the cover image URL.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >80% coverage for the new logic
- Integration testing for the dashboard load and navigation completed successfully
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on a mid-range device
- Accessibility requirements validated using VoiceOver and TalkBack
- Documentation for the dashboard state management updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational component for the dashboard and a key part of the core user journey. It should be prioritized early in the development of the main app interface.

## 11.4.0.0 Release Impact

- Critical for the initial application release. The app is not viable without this core dashboard functionality.

