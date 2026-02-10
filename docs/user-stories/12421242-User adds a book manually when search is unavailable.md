# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-055 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User adds a book manually when search is unavailab... |
| As A User Story | As a user, I want to be able to manually add a boo... |
| User Persona | Any authenticated user (Free or Premium) attemptin... |
| Business Value | Improves application resilience and user experienc... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | System Resilience and Fallbacks |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Fallback option appears on search API failure

### 3.1.3 Scenario Type

Alternative_Flow

### 3.1.4 Given

A user is on the 'Add Book' screen and initiates a search

### 3.1.5 When

The external book search API (Google Books API) returns a server error (e.g., 5xx status) or times out

### 3.1.6 Then

The UI displays a user-friendly error message (e.g., 'Book search is currently unavailable') and a clearly visible 'Add Manually' button.

### 3.1.7 Validation Notes

Can be tested by mocking the API call to return an error or timeout. Verify the error message and button appear.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User successfully adds a book manually (Happy Path)

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The user has tapped the 'Add Manually' button and is presented with the manual entry form

### 3.2.5 When

The user enters a valid Title and Author, selects a shelf (e.g., 'Want to Read'), and submits the form

### 3.2.6 Then

A new book item is created in the user's library with the provided details, a placeholder cover image is assigned, and the user is navigated to their library where the new book is visible.

### 3.2.7 Validation Notes

Verify the book record is created correctly in the database with null values for ISBN, cover URL, etc. Check the UI for the new book with its placeholder cover.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Form validation for required fields

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

The user is on the manual entry form

### 3.3.5 When

The user attempts to submit the form without entering a Title

### 3.3.6 Then

A validation error message is displayed next to the Title field, the form is not submitted, and no book is added to the library.

### 3.3.7 Validation Notes

Repeat the test for a missing Author field. Ensure validation messages are clear and accessible.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Free User hits library limit when adding manually

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

A 'Free User' has the maximum of 20 items in their library

### 3.4.5 When

The user attempts to save a new book via the manual entry form

### 3.4.6 Then

The system prevents the book from being added and displays the standard 'Upgrade to Premium' prompt.

### 3.4.7 Validation Notes

Requires a test user account in the 'Free User' state with a full library. Verify the correct prompt is shown and the item count does not exceed 20.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Manually added book handles missing data gracefully

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

A book has been added manually

### 3.5.5 When

The user views the book's details page or its entry in the library list

### 3.5.6 Then

The UI displays the user-provided Title and Author, uses a generic placeholder for the cover image, and gracefully hides or shows 'N/A' for fields that were not provided (e.g., Page Count, ISBN).

### 3.5.7 Validation Notes

Check all UI locations where book details are displayed to ensure no crashes or visual bugs occur due to null data.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Error message component for API failures
- Button: 'Add Manually'
- Modal or Screen: 'Add Book Manually'
- Text Input Field: 'Title' (required)
- Text Input Field: 'Author' (required)
- Text Input Field: 'Page Count' (optional)
- Button: 'Save Book'
- Inline validation error messages

## 4.2.0 User Interactions

- On search API failure, the 'Add Manually' button becomes visible.
- Tapping 'Add Manually' opens the manual entry form.
- Tapping 'Save Book' with invalid data displays validation errors.
- Tapping 'Save Book' with valid data adds the book and closes the form.

## 4.3.0 Display Requirements

- The error message must be non-technical and user-friendly.
- Manually added books must display a consistent, high-quality placeholder cover image.

## 4.4.0 Accessibility Needs

- All form fields must have proper labels for screen readers.
- Validation errors must be programmatically associated with their respective input fields.
- All UI elements must adhere to WCAG 2.1 AA contrast ratios and support dynamic type.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The manual add functionality is only presented as a fallback when the primary book search API is confirmed to be unavailable (e.g., via timeout or 5xx error code).

### 5.1.3 Enforcement Point

Client-side application, within the book search feature's error handling logic.

### 5.1.4 Violation Handling

The 'Add Manually' option remains hidden.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Adding a book manually is subject to the same library item limits as adding via search, specifically the 20-item limit for Free Users.

### 5.2.3 Enforcement Point

Backend API, before creating the new LibraryItem record.

### 5.2.4 Violation Handling

The API returns an error indicating the limit has been reached, and the client displays the upgrade prompt.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-037

#### 6.1.1.2 Dependency Reason

The primary book search functionality must exist to have a failure case to handle.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-041

#### 6.1.2.2 Dependency Reason

The core backend and client logic for adding a LibraryItem to a user's library must be implemented first.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-013

#### 6.1.3.2 Dependency Reason

The business logic for enforcing the Free User library limit must be available to be checked during manual addition.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-015

#### 6.1.4.2 Dependency Reason

The UI component for the upgrade prompt must be available to be displayed when a Free User hits their limit.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint capable of creating a `LibraryItem` with nullable fields (ISBN, coverUrl, pageCount).
- Client-side HTTP client configuration for handling network timeouts and errors.
- A generic placeholder book cover asset in the application's asset bundle.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The manual entry form must load in under 200ms after the 'Add Manually' button is tapped.
- Submitting the manual form should have a P95 latency of < 200ms, consistent with other API calls.

## 7.2.0.0 Security

- All data submitted from the manual entry form must undergo the same input validation and sanitization on the backend as any other API endpoint to prevent injection attacks (NFR-SEC-006).

## 7.3.0.0 Usability

- The fallback path must be intuitive and require no user training.
- Error messages must be clear and constructive.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- The form and its components must render correctly on all supported iOS and Android versions and screen sizes (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires careful state management on the client to handle the search API's error state.
- Backend logic must be robust to handle records with missing metadata.
- Requires a new UI component (the manual entry form).

## 8.3.0.0 Technical Risks

- Inconsistently defining what constitutes an API 'failure' could lead to the fallback option showing up too often or not often enough.

## 8.4.0.0 Integration Points

- Client-side Google Books API service.
- Backend `LibraryItem` creation endpoint.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Simulate a 503 error from the Google Books API and verify the fallback UI appears.
- Simulate a network timeout when calling the Google Books API.
- Submit the manual form with all fields empty, one field empty, and all fields filled.
- As a Free User with 20 items, attempt to add a 21st book manually.
- As a Premium User, add a book manually.
- Verify a manually added book displays correctly in the library list and on its detail page.

## 9.3.0.0 Test Data Needs

- A test user account for a 'Free User' with 20 items in their library.
- A test user account for a 'Premium User'.

## 9.4.0.0 Testing Tools

- Flutter's `flutter_test` and `integration_test` packages.
- A mocking library (e.g., `mocktail`) to simulate API responses.
- Jest for backend unit tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing with a mocked API failure completed successfully
- User interface reviewed and approved for all supported screen sizes and themes (light/dark)
- Performance requirements verified
- Security requirements validated
- Documentation updated appropriately
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story should be scheduled in a sprint after the primary 'add book via search' stories are completed and stable.
- Requires coordination between frontend and backend to ensure the API handles nullable fields correctly.

## 11.4.0.0 Release Impact

Improves the robustness of the application for the public release. While not a core feature itself, it supports a core feature, making the overall product more reliable.

