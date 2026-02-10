# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-073 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User views most-read authors and genres |
| As A User Story | As a Premium User, I want to view a ranked list of... |
| User Persona | Premium User: A paying subscriber who has access t... |
| Business Value | Enhances the value proposition of the Premium subs... |
| Functional Area | Statistics and Insights |
| Story Theme | Premium Feature Enhancement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Premium User with reading history views their top authors and genres

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is authenticated as a 'Premium User' and has multiple books on their 'Read' shelf with complete author and genre metadata

### 3.1.5 When

the user navigates to the 'Insights' screen

### 3.1.6 Then

the system displays two distinct sections: 'Most-Read Authors' and 'Most-Read Genres'

### 3.1.7 And

the calculation only includes books from the user's 'Read' shelf.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Free User attempts to access the feature

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

the user is authenticated as a 'Free User'

### 3.2.5 When

the user navigates to the screen where advanced statistics are displayed

### 3.2.6 Then

the 'Most-Read Authors' and 'Most-Read Genres' sections are either not visible or are displayed as a locked feature

### 3.2.7 And

if displayed as locked, an unobtrusive prompt to upgrade to Premium is present.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Premium User has no completed books

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is authenticated as a 'Premium User'

### 3.3.5 And

their 'Read' shelf is empty

### 3.3.6 When

the user navigates to the 'Insights' screen

### 3.3.7 Then

the system displays a user-friendly placeholder message within the 'Most-Read Authors' and 'Most-Read Genres' sections, such as 'Finish some books to discover your favorites here!'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Calculation handles books with multiple authors and genres

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

a Premium User has a book on their 'Read' shelf with two authors (Author A, Author B) and two genres (Genre X, Genre Y)

### 3.4.5 When

the system calculates the statistics

### 3.4.6 Then

the count for Author A is incremented by one, and the count for Author B is also incremented by one

### 3.4.7 And

the count for Genre X is incremented by one, and the count for Genre Y is also incremented by one.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Calculation handles books with missing metadata

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a Premium User has a book on their 'Read' shelf that is missing genre information but has an author

### 3.5.5 When

the system calculates the statistics

### 3.5.6 Then

the book is correctly included in the 'Most-Read Authors' calculation but is excluded from the 'Most-Read Genres' calculation.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Lists handle ties in counts

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a Premium User has read 3 books by 'Author A' and 3 books by 'Author C'

### 3.6.5 When

the 'Most-Read Authors' list is displayed

### 3.6.6 Then

the primary sorting is by count (descending), and the secondary sorting for items with the same count is alphabetical (ascending). 'Author A' will appear before 'Author C'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated section within the 'Insights' screen for 'Most-Read Authors'.
- A dedicated section within the 'Insights' screen for 'Most-Read Genres'.
- Scrollable lists for both authors and genres.
- A placeholder view/message for when no data is available.

## 4.2.0 User Interactions

- The lists should initially display the top 5 items, with a 'See All' button to expand and view the complete list.
- Tapping 'See All' should navigate the user to a new screen or expand the list in place to show all items.

## 4.3.0 Display Requirements

- Each list item must clearly show the name (author/genre) and the count.
- The UI must be consistent with the app's overall design language, including light and dark themes.

## 4.4.0 Accessibility Needs

- All text must support dynamic type scaling based on OS settings.
- UI elements must adhere to WCAG 2.1 Level AA contrast ratios.
- Lists must be navigable using screen readers, with each item announcing the name and count (e.g., 'Stephen King, 5 books').

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

This feature is exclusively available to users with an active 'Premium User' subscription status.

### 5.1.3 Enforcement Point

Backend API Gateway and Frontend UI rendering.

### 5.1.4 Violation Handling

API requests from non-premium users will receive a 403 Forbidden response. The UI will hide the feature or show an upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only books with the status 'Read' are included in the calculations.

### 5.2.3 Enforcement Point

Backend data aggregation query.

### 5.2.4 Violation Handling

Books on 'Currently Reading', 'Want to Read', or 'DNF' shelves are filtered out and not included in the counts.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

A user must be able to become a Premium User to access this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-043

#### 6.1.2.2 Dependency Reason

The core data for this feature is generated when a user moves a book to the 'Read' shelf.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-037

#### 6.1.3.2 Dependency Reason

Book metadata, including authors and genres, is fetched when a book is added to the library via search.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to compute and serve the aggregated author and genre data.
- The primary database (Amazon Aurora) must contain the user's library and reading history.
- The `LibraryItem` data model must support storing arrays of authors and genres.

## 6.3.0.0 Data Dependencies

- User-generated reading history data.
- Author and genre metadata fetched from the Google Books API upon adding a book.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint providing this data must have a P95 latency of less than 200ms as per REQ-PER-001.
- The data should be cached (e.g., in Redis) to avoid re-computation on every view, with cache invalidation occurring when a user moves a new book to the 'Read' shelf.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible by authenticated users with a valid JWT containing a 'premium_user' role, as per REQ-SEC-001 and NFR-SEC-002.

## 7.3.0.0 Usability

- The information must be presented in a clear, easily scannable format.
- The feature should be discoverable within the dedicated 'Insights' section for Premium users.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards, as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The feature must render correctly on all supported iOS and Android versions and screen sizes as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The backend aggregation logic requires an efficient database query to handle potentially large reading histories and unnesting of author/genre arrays.
- Implementing a robust caching strategy to meet performance requirements.
- Handling inconsistencies or missing data from the external Google Books API.
- Potential need for data normalization for genres (e.g., 'Sci-Fi' vs 'Science Fiction') in a future iteration.

## 8.3.0.0 Technical Risks

- A naive database query could be slow for users with thousands of completed books, impacting user experience and system load.
- Inconsistent genre data from the Google Books API may lead to fragmented statistics.

## 8.4.0.0 Integration Points

- Backend: Amazon Aurora for data retrieval.
- Backend: Amazon ElastiCache for Redis for caching results.
- Frontend: Flutter UI component within the 'Insights' screen.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance

## 9.2.0.0 Test Scenarios

- Verify correct data and sorting for a Premium user with a populated 'Read' shelf.
- Verify the placeholder state for a Premium user with an empty 'Read' shelf.
- Verify access is denied for a Free user at both the API and UI levels.
- Verify correct counting for books with multiple authors/genres.
- Verify correct handling of books with missing metadata.
- Verify secondary alphabetical sorting for items with tied counts.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' roles.
- A Premium user account with an empty 'Read' shelf.
- A Premium user account with a populated 'Read' shelf containing a mix of books: single author/genre, multiple authors/genres, missing metadata, and data that will result in count ties.

## 9.4.0.0 Testing Tools

- Jest for backend unit tests.
- flutter_test for Flutter widget tests.
- integration_test package for Flutter E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend aggregation logic and frontend components, achieving >80% coverage
- Integration testing completed successfully, verifying API access control and data correctness
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified via load testing the API endpoint
- Security requirements validated (endpoint is protected)
- Documentation for the new API endpoint is generated in OpenAPI format
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story should be prioritized after the core subscription and reading tracking functionalities are stable.
- The backend and frontend work can be done in parallel once the API contract is defined.

## 11.4.0.0 Release Impact

- This feature will be a key marketing point for the Premium subscription tier in release notes and promotional materials.

