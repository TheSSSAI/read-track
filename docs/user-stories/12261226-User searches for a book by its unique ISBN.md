# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-039 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User searches for a book by its unique ISBN |
| As A User Story | As a reader who wants to accurately log the specif... |
| User Persona | Any user ('Free User' or 'Premium User') who wants... |
| Business Value | Increases the accuracy and reliability of the core... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | Library Population and Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful search using a valid 13-digit ISBN

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is on the 'Add Book' screen

### 3.1.5 When

the user enters a valid 13-digit ISBN (e.g., '9780321765723') and initiates the search

### 3.1.6 Then

the system must display a loading indicator, query the external book API, and present the single matching book result with its title, author, and cover image, allowing the user to add it to a shelf.

### 3.1.7 Validation Notes

Verify that a single, correct book is returned and can be successfully added to the library.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Successful search using a valid 10-digit ISBN

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the user is on the 'Add Book' screen

### 3.2.5 When

the user enters a valid 10-digit ISBN (e.g., '0321765723') and initiates the search

### 3.2.6 Then

the system must display a loading indicator, query the external book API, and present the single matching book result.

### 3.2.7 Validation Notes

Verify that the system correctly handles the ISBN-10 format and returns the correct book.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Search with an ISBN containing hyphens or spaces

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is on the 'Add Book' screen

### 3.3.5 When

the user enters a valid ISBN containing hyphens or spaces (e.g., '978-0-321-76572-3')

### 3.3.6 Then

the system must automatically sanitize the input by removing non-alphanumeric characters before performing the search and return the correct book result.

### 3.3.7 Validation Notes

Test with various formatting styles to ensure the sanitization logic is robust.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Search for a validly formatted but non-existent ISBN

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

the user is on the 'Add Book' screen

### 3.4.5 When

the user enters a validly formatted ISBN that does not correspond to any book in the external API's database

### 3.4.6 Then

the system must display a clear, user-friendly message such as 'No book found for this ISBN. Please check the number or try searching by title.'

### 3.4.7 Validation Notes

Verify that no results are shown and the specified error message is displayed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User enters an invalid ISBN format

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

the user is on the 'Add Book' screen's search field

### 3.5.5 When

the user enters a string that is not a valid 10 or 13-digit ISBN (e.g., '12345' or 'not-an-isbn')

### 3.5.6 Then

the search action must be disabled and inline validation feedback must appear, stating 'Please enter a valid 10 or 13-digit ISBN.'

### 3.5.7 Validation Notes

Test with strings of incorrect length and invalid characters to ensure client-side validation works as expected.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

External book API is unavailable or returns an error

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

the user initiates a search with a valid ISBN

### 3.6.5 When

the external Google Books API is unreachable or returns a server-side error (e.g., 5xx status code)

### 3.6.6 Then

the system must display a non-intrusive error message like 'Search is temporarily unavailable. Please try again later.' and provide the option to add the book manually.

### 3.6.7 Validation Notes

This can be tested by mocking an API failure response. Verify the correct message and the fallback option appear.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

UI displays a loading state during search

### 3.7.3 Scenario Type

Alternative_Flow

### 3.7.4 Given

the user has initiated a search with a valid ISBN

### 3.7.5 When

the system is awaiting a response from the external book API

### 3.7.6 Then

the UI must display a clear loading indicator (e.g., a spinner or shimmer placeholder) to provide feedback that the search is in progress.

### 3.7.7 Validation Notes

Visually confirm that the loading state is present and replaces the input/search button area to prevent duplicate requests.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated input field for ISBN search, possibly as a tab or option alongside Title/Author search.
- A search button or icon to initiate the search.
- A loading indicator (e.g., spinner, shimmer effect).
- A display area for the single search result, showing cover art, title, and author.
- A text area for displaying validation and error messages.

## 4.2.0 User Interactions

- The user can type or paste an ISBN into the input field.
- The system should provide real-time validation feedback on the input format.
- Tapping the search button triggers the API call.
- The user can tap on the search result to proceed with adding the book to their library.

## 4.3.0 Display Requirements

- The input field should have a placeholder text like 'Enter 10 or 13-digit ISBN'.
- Error messages must be clear, concise, and helpful.
- The search result must clearly display the book's essential metadata.

## 4.4.0 Accessibility Needs

- The input field must have a proper label for screen readers.
- Error messages must be associated with the input field and announced by screen readers.
- All interactive elements must have sufficient touch target sizes and contrast ratios per WCAG 2.1 AA.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

ISBN input must be sanitized to remove non-alphanumeric characters (like hyphens and spaces) before being sent to the search API.

### 5.1.3 Enforcement Point

Client-side, before the API request is constructed.

### 5.1.4 Violation Handling

N/A - This is a data normalization rule, not a violation.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A search can only be initiated if the input string is a validly formatted 10-digit or 13-digit ISBN.

### 5.2.3 Enforcement Point

Client-side, by disabling the search button until the input is valid.

### 5.2.4 Violation Handling

The user is prevented from proceeding and is shown an inline validation message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-037

#### 6.1.1.2 Dependency Reason

Establishes the basic 'Add Book' screen UI, search input components, and result display mechanism that this story will extend.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-041

#### 6.1.2.2 Dependency Reason

Provides the functionality to add a found book to a shelf, which is the necessary next step after a successful search.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-055

#### 6.1.3.2 Dependency Reason

Provides the 'add book manually' fallback path required for the API failure scenario (AC-006).

## 6.2.0.0 Technical Dependencies

- A configured HTTP client (Dio) for making API requests.
- State management solution (Riverpod) to handle UI states (loading, data, error).
- Backend endpoint that securely proxies requests to the Google Books API.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

- The Google Books API (SI-005) must be available and accessible for fetching book metadata. Its rate limits and terms of service must be respected.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The P95 latency for the backend proxy endpoint to the Google Books API should be under 500ms.
- The UI should display search results within 2 seconds of initiation on a standard 4G network.

## 7.2.0.0 Security

- The Google Books API key must be stored securely in AWS Secrets Manager and used only by the backend service, never exposed in the client application (NFR-SEC-005).

## 7.3.0.0 Usability

- The search process should be intuitive, and feedback (loading, success, error) must be immediate and clear to the user.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing robust client-side validation for both ISBN-10 and ISBN-13 formats.
- Managing multiple UI states (idle, loading, success with data, error/not found) using Riverpod.
- Creating a backend endpoint to securely proxy requests to the Google Books API to protect the API key.
- Graceful handling of various API error responses and network conditions.

## 8.3.0.0 Technical Risks

- The Google Books API may have rate limits that could be hit under heavy usage, requiring a strategy for throttling or caching.
- Data quality from the API can be inconsistent (e.g., missing page counts or cover images), requiring fallback logic in the UI.

## 8.4.0.0 Integration Points

- Client App -> Backend API (for proxied search)
- Backend API -> Google Books API (SI-005)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test ISBN validation logic with valid ISBN-10, valid ISBN-13, and various invalid strings.
- Test UI rendering for all states: initial, loading, result found, result not found, and API error.
- Test the full end-to-end flow from entering an ISBN to adding the resulting book to a shelf, using a mocked backend.

## 9.3.0.0 Test Data Needs

- A list of known valid ISBN-10s.
- A list of known valid ISBN-13s.
- A list of validly formatted ISBNs that are known not to exist in the Google Books API.
- A list of malformed/invalid strings.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.
- A mock server (like Mockito or a custom solution) to simulate backend and Google Books API responses.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and widget tests implemented with >= 80% code coverage for new logic
- E2E integration test for the happy path is implemented and passing
- User interface reviewed and approved by the design/product owner
- Performance requirements (API latency, UI load time) are verified
- API key is confirmed to be stored securely on the backend
- Documentation for the new backend endpoint is created/updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires both frontend (Flutter) and backend (Node.js) development work for the secure API proxy.
- Availability of a valid Google Books API key is required before development can be completed.

## 11.4.0.0 Release Impact

This is a core feature enhancement for adding books and is expected to be included in the next minor release.

