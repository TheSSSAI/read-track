# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-009 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | New user adds a 'Currently Reading' book during on... |
| As A User Story | As a new user going through the onboarding process... |
| User Persona | A new user who has just authenticated for the firs... |
| Business Value | Increases user activation and retention by immedia... |
| Functional Area | User Onboarding |
| Story Theme | First-Time User Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User successfully finds and adds a book

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a new user is on the 'Add Currently Reading Book' step of the onboarding flow

### 3.1.5 When

the user enters a valid book title into the search field and initiates a search

### 3.1.6 Then

a loading indicator is displayed while the search is in progress, and a list of relevant book results from the Google Books API is displayed, showing at least the cover image, title, and author for each result. The user then selects a book from the list. The selected book is added to the user's library with the shelf status set to 'Currently Reading', and the user is automatically advanced to the next step in the onboarding flow.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Alternative Flow: User skips adding a book

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

a new user is on the 'Add Currently Reading Book' step of the onboarding flow

### 3.2.5 When

the user taps the 'Skip for now' button

### 3.2.6 Then

no book is added to the user's library, and the user is advanced to the next step in the onboarding flow.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Edge Case: Search yields no results

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

a new user is on the 'Add Currently Reading Book' step

### 3.3.5 When

the user enters a search query that returns no results from the external API

### 3.3.6 Then

the UI displays a clear message such as 'No books found. Please try a different search.' and the user can either perform a new search or skip the step.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: External book API fails

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a new user is on the 'Add Currently Reading Book' step

### 3.4.5 When

the user initiates a search, and the backend service fails to get a response from the Google Books API

### 3.4.6 Then

the UI displays a user-friendly error message, such as 'Sorry, we couldn't search for books right now. You can skip this step and add a book later.' and the 'Skip' button remains enabled.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: User has no network connectivity

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

a new user is on the 'Add Currently Reading Book' step and their device is offline

### 3.5.5 When

the user attempts to initiate a search

### 3.5.6 Then

the app displays an 'Offline' or 'No Internet Connection' message, the search action is prevented, and the 'Skip' button remains enabled.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Verification: Book appears on dashboard post-onboarding

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

a new user has successfully added a 'Currently Reading' book during onboarding

### 3.6.5 When

the user completes the entire onboarding flow and lands on the main dashboard

### 3.6.6 Then

the book they added is displayed prominently in the 'Currently Reading' section of the dashboard.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clear screen title, e.g., 'What are you currently reading?'
- A text input field with a placeholder, e.g., 'Search by title, author, or ISBN'
- A search button or icon
- A loading indicator (e.g., spinner) to show during API calls
- A scrollable list to display search results
- A 'Skip for now' or 'I'll do this later' button

## 4.2.0 User Interactions

- The search should be triggered after the user stops typing for a brief period (debouncing) or taps a search button.
- Tapping a search result selects that book.
- The UI must provide clear feedback for loading, success, no results, and error states.

## 4.3.0 Display Requirements

- Each search result item must display the book's cover image, title, and author(s).
- Error messages must be user-friendly and non-technical.

## 4.4.0 Accessibility Needs

- The search input field must have a proper label for screen readers.
- All interactive elements (buttons, list items) must have a minimum tap target size of 44x44 points.
- The UI must adhere to color contrast ratios as per WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A book added during this onboarding step must automatically be assigned to the 'Currently Reading' shelf.", 'enforcement_point': 'Backend service, when creating the LibraryItem record for the user.', 'violation_handling': 'N/A - System logic must enforce this rule.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

User must be able to sign up with Google to enter the onboarding flow.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-003

#### 6.1.2.2 Dependency Reason

User must be able to sign up with Apple to enter the onboarding flow.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-006

#### 6.1.3.2 Dependency Reason

The overall onboarding flow framework must exist for this step to be integrated into it.

## 6.2.0.0 Technical Dependencies

- Backend endpoint to proxy search requests to the Google Books API (SI-005).
- Backend endpoint to create a new LibraryItem for a user.
- Flutter application state management (Riverpod) for the onboarding flow.
- Client-side HTTP client (Dio) for API communication.

## 6.3.0.0 Data Dependencies

- The user's authenticated session (JWT) is required to associate the new book with their account.

## 6.4.0.0 External Dependencies

- Availability of the Google Books API.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- P95 latency for the book search API endpoint must be under 500ms (excluding the external API's latency).
- The UI for search results must render within 2 seconds on a standard 4G network connection.

## 7.2.0.0 Security

- All API calls between the client and backend must be over HTTPS.
- The backend must not expose the Google Books API key to the client application.

## 7.3.0.0 Usability

- The search functionality should be intuitive and provide immediate feedback (loading, results, errors).
- The option to skip this step must be clearly visible and accessible at all times.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must render correctly on all supported iOS (14.0+) and Android (7.0+) screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated frontend and backend development.
- Integration with a third-party API (Google Books) introduces external dependency.
- UI must handle multiple states: initial, loading, results, no results, and error.
- Implementing search input debouncing on the client to prevent excessive API calls.

## 8.3.0.0 Technical Risks

- The Google Books API may have rate limits that need to be handled by the backend.
- Inconsistent data quality or missing fields from the Google Books API response must be handled gracefully.

## 8.4.0.0 Integration Points

- Backend: Google Books API (SI-005)
- Backend: User's personal library data store (Amazon Aurora)
- Frontend: Onboarding flow state management

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify successful book search and addition.
- Verify the 'skip' functionality.
- Verify UI behavior for 'no results' found.
- Verify UI behavior when the backend API returns an error.
- Verify UI behavior when the device is offline.
- Verify that after completing onboarding, the added book is correctly displayed on the dashboard.

## 9.3.0.0 Test Data Needs

- A set of known book titles, authors, and ISBNs that produce predictable results.
- A search term known to produce zero results.
- A user account in a 'new user' state.

## 9.4.0.0 Testing Tools

- Backend: Jest
- Frontend: flutter_test, integration_test

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in the staging environment.
- Code for both frontend and backend is peer-reviewed and merged into the main branch.
- Unit and widget tests are implemented with >= 80% code coverage.
- Backend integration tests for the Google Books API proxy and library item creation are passing.
- E2E automated tests for the happy path and skip flow are passing.
- UI has been reviewed by a designer and approved.
- Performance requirements for API latency and UI load time are verified.
- The feature is verified on a representative range of supported iOS and Android devices.
- No P1/P2 accessibility issues are found.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a critical path story for the initial user experience and should be prioritized for an early sprint.
- Backend API development may need to be completed before or in parallel with the frontend UI work to avoid blocking.

## 11.4.0.0 Release Impact

This feature is essential for the Minimum Viable Product (MVP) release as it is part of the core user activation flow.

