# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-040 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User adds an article to their library by pasting a... |
| As A User Story | As a diligent reader, I want to add an online arti... |
| User Persona | Any registered user (Free or Premium) who reads co... |
| Business Value | Increases application utility and user engagement ... |
| Functional Area | Reading Tracking & Library Management |
| Story Theme | Library Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Successfully adding an article with metadata fetching

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user on my library screen

### 3.1.5 When

I initiate the 'Add Article' flow, paste a valid and publicly accessible URL, and the system successfully fetches the title

### 3.1.6 Then

The title field is pre-populated with the fetched title, I can confirm or edit it, and upon saving, the article is added to my 'Want to Read' shelf and appears in my library.

### 3.1.7 Validation Notes

Test with a known public blog post URL. Verify the title is fetched correctly and the item is created in the database with type 'article'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Alternative Flow: Adding an article when metadata fetching fails

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

I am on the 'Add Article' screen and provide a valid URL that is inaccessible or has no title

### 3.2.5 When

The system's attempt to fetch the title times out or fails

### 3.2.6 Then

The title field remains empty, I can manually enter a custom title, and successfully save the article to my library.

### 3.2.7 Validation Notes

Test with a URL pointing to a 404 page or a local, non-routable IP. Verify the save functionality is not blocked and the user can proceed by entering a manual title.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: User enters an invalid URL

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the 'Add Article' screen

### 3.3.5 When

I enter a string that is not a valid URL format (e.g., 'my article') into the URL field and attempt to save

### 3.3.6 Then

An inline validation error message is displayed stating 'Please enter a valid URL', and the save action is prevented.

### 3.3.7 Validation Notes

Test with various invalid strings. The 'Save' button should ideally be disabled until the URL format is valid.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Required fields are empty

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the 'Add Article' screen

### 3.4.5 When

I attempt to save with either the URL field or the Title field empty

### 3.4.6 Then

The 'Save' button is disabled, and inline validation messages indicate which fields are required.

### 3.4.7 Validation Notes

Verify the 'Save' button's state changes dynamically based on the form's validity.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Business Rule: Free User hits the library item limit

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a 'Free User' with 20 items in my library (as per REQ-BUS-001)

### 3.5.5 When

I attempt to save a new article

### 3.5.6 Then

The system prevents the item from being saved and displays a non-intrusive prompt to upgrade to a Premium subscription.

### 3.5.7 Validation Notes

Requires a test account in the 'Free User' state with exactly 20 library items. Verify the upgrade prompt is displayed and no new item is created.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An 'Add Article' option, accessible from the main library view.
- A dedicated screen/modal for adding an article.
- A text input field for the article URL, supporting paste functionality.
- A text input field for the article title.
- A 'Save' button.
- A loading indicator displayed while fetching the article title.
- Inline validation messages for form fields.

## 4.2.0 User Interactions

- Pasting a URL automatically triggers the title fetch process.
- The 'Save' button is disabled until both a valid URL and a non-empty title are provided.
- Upon successful save, the user is returned to the library view and a confirmation toast/snackbar is displayed.

## 4.3.0 Display Requirements

- The fetched title should be displayed in the title input field.
- Error messages must be clear and user-friendly.

## 4.4.0 Accessibility Needs

- All input fields must have proper labels for screen readers.
- Loading indicators and validation messages must be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A 'Free User' cannot have more than 20 items in their library.

### 5.1.3 Enforcement Point

Backend API, before creating the new LibraryItem record.

### 5.1.4 Violation Handling

The API returns a specific error code (e.g., 403 Forbidden with a payload indicating the limit was reached), which the client uses to trigger an upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

An article must have a valid URL and a non-empty title.

### 5.2.3 Enforcement Point

Client-side form validation and Backend API validation.

### 5.2.4 Violation Handling

The request is rejected with a 400 Bad Request error and details about the validation failure.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be authenticated to have a personal library.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-005

#### 6.1.2.2 Dependency Reason

User must be authenticated to have a personal library.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-013

#### 6.1.3.2 Dependency Reason

The core logic for enforcing the 20-item limit for Free Users must be implemented.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-015

#### 6.1.4.2 Dependency Reason

The UI component for the upgrade prompt must be available to be displayed when the limit is reached.

## 6.2.0.0 Technical Dependencies

- A backend endpoint (e.g., POST /api/v1/library/articles) to create article items.
- A secure, sandboxed backend service for fetching and parsing metadata from external URLs.
- The client-side data model and local database (Isar) must support a 'LibraryItem' of type 'article'.

## 6.3.0.0 Data Dependencies

- The user's current subscription status and library item count must be available to the backend service for validation.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The backend metadata fetching service must have a strict timeout of 5 seconds to prevent long waits on the client.
- The UI must remain responsive while the title is being fetched in the background.

## 7.2.0.0 Security

- The backend service that fetches content from user-provided URLs MUST be protected against Server-Side Request Forgery (SSRF) attacks. It should validate URLs against a strict allowlist and deny requests to internal/private IP ranges.
- All user input must be sanitized on the backend to prevent XSS or other injection attacks.

## 7.3.0.0 Usability

- The process should be quick and intuitive, with clear feedback at each step (loading, success, failure).

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity is in building a robust and secure backend service to scrape metadata from arbitrary external websites.
- Handling various failure modes (timeouts, HTTP errors, malformed HTML, content behind paywalls) adds significant complexity.
- Implementing effective SSRF protection is a critical and non-trivial security requirement.

## 8.3.0.0 Technical Risks

- The metadata scraping may be unreliable across different websites, necessitating a strong reliance on the user's ability to manually enter a title.
- Improper implementation of the URL fetching service could introduce a major security vulnerability (SSRF).

## 8.4.0.0 Integration Points

- Client App -> Backend API Gateway -> Article Creation Service -> Metadata Fetching Service
- Article Creation Service -> User's Library Data (Database)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Add an article from a popular news site.
- Add an article from a blog with simple HTML.
- Attempt to add an article using a URL that returns a 404 error.
- Attempt to add an article using an invalid URL string.
- As a Free User with 20 items, attempt to add a 21st.
- As a Premium User, add a 21st item successfully.
- Security testing: Attempt SSRF attacks with internal IP addresses (e.g., 127.0.0.1, 10.0.0.1, 192.168.1.1).

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' tiers.
- A 'Free User' account pre-populated with 20 library items.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test`, `integration_test`
- Backend: Jest, Supertest
- Security: Automated security scanning tools that can detect SSRF vulnerabilities.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing between client and backend completed successfully
- E2E test scenario for adding an article is automated and passing
- Backend service has been security reviewed and tested for SSRF vulnerabilities
- Performance requirements (5-second timeout) verified
- Documentation for the new API endpoint is created/updated in OpenAPI format
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- The backend scraping service is the most complex part and should be tackled early. A clear API contract between frontend and backend is needed from the start.
- Allocate specific time for security review and testing of the backend service.

## 11.4.0.0 Release Impact

This is a core feature for enhancing the app's value proposition. It should be included in a major feature release.

