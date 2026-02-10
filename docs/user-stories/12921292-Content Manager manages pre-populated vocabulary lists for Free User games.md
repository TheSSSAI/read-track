# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-105 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Content Manager manages pre-populated vocabulary l... |
| As A User Story | As a Content Manager, I want to create, update, an... |
| User Persona | Content Manager: A non-technical or semi-technical... |
| Business Value | Enables the vocabulary game feature for Free Users... |
| Functional Area | Content Management & Game Content |
| Story Theme | Freemium Feature Enablement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Content Manager defines the content structure for vocabulary words in the CMS

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

The Content Manager has access to the Contentful space

### 3.1.5 When

they navigate to the Content Model section

### 3.1.6 Then

they can create a new content type named 'Vocabulary Word' with the following required fields: 'word' (Short Text), 'definition' (Long Text), and an optional field 'exampleSentence' (Long Text).

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Content Manager defines the content structure for grouping words into lists

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The 'Vocabulary Word' content model exists in Contentful

### 3.2.5 When

the Content Manager creates a new content type named 'Vocabulary List'

### 3.2.6 Then

they can define the model with a required 'listName' (Short Text) field and a required 'words' field that is a many-to-many reference to 'Vocabulary Word' entries.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Content Manager creates and publishes a new vocabulary list

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

The 'Vocabulary List' and 'Vocabulary Word' content models exist

### 3.3.5 When

the Content Manager creates several 'Vocabulary Word' entries, creates a new 'Vocabulary List' entry, links the words to the list, and publishes the list

### 3.3.6 Then

the list and its associated words become available via the Contentful Delivery API.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Backend provides an API endpoint for fetching vocabulary lists

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

There is at least one published 'Vocabulary List' in Contentful

### 3.4.5 When

a client makes a GET request to the backend endpoint (e.g., `/api/v1/vocabulary/lists`)

### 3.4.6 Then

the backend service fetches the data from Contentful, transforms it into a clean JSON format, and returns a 200 OK response with an array of vocabulary lists and their words.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Mobile app fetches and displays vocabulary content in games

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

A Free User is on a screen that contains a vocabulary game

### 3.5.5 When

the screen loads

### 3.5.6 Then

the app successfully calls the backend API, receives the vocabulary data, and populates the game (e.g., Flashcards, Quiz) with the words from the pre-populated list.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Mobile app caches vocabulary content for offline use

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The app has successfully fetched the vocabulary lists online

### 3.6.5 When

the user goes offline and navigates to the vocabulary games

### 3.6.6 Then

the games are still populated using the data stored in the local Isar database, as per REQ-OFF-001.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

System handles no published vocabulary lists

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

There are no 'Vocabulary List' entries published in Contentful

### 3.7.5 When

the mobile app requests the vocabulary lists from the backend

### 3.7.6 Then

the backend API returns a 200 OK with an empty array, and the app displays a user-friendly message (e.g., 'No vocabulary games available right now.') instead of crashing.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

System handles Contentful API unavailability

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

The Contentful API is down or unreachable by the backend service

### 3.8.5 When

the mobile app requests the vocabulary lists

### 3.8.6 Then

the backend API returns a 503 Service Unavailable error, and the mobile app gracefully handles the error, relying on its cached data if available, as per REQ-REL-001.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- This story does not directly create user-facing mobile UI. It defines the content model within the Contentful web interface that will be used by the Content Manager.

## 4.2.0 User Interactions

- The Content Manager will use the standard Contentful web UI to create, edit, link, and publish content entries.

## 4.3.0 Display Requirements

- The mobile app's vocabulary game screens (defined in US-083, US-084) must display the 'word', 'definition', and 'exampleSentence' as fetched from the API.

## 4.4.0 Accessibility Needs

- Not applicable for the CMS portion. Accessibility for the game UI is covered in its respective user stories.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Only published 'Vocabulary List' entries and their constituent published 'Vocabulary Word' entries shall be exposed via the backend API.", 'enforcement_point': 'Backend service logic when querying the Contentful Delivery API.', 'violation_handling': 'Draft or archived content is ignored and not included in the API response.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-083

#### 6.1.1.2 Dependency Reason

The flashcard game UI must exist to consume and display the vocabulary data.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-084

#### 6.1.2.2 Dependency Reason

The multiple-choice quiz UI must exist to consume and display the vocabulary data.

## 6.2.0.0 Technical Dependencies

- A provisioned Contentful space and API keys.
- Backend service architecture capable of integrating with a third-party REST API.
- Mobile app local database (Isar) setup for caching.

## 6.3.0.0 Data Dependencies

- Initial population of vocabulary words and lists in the CMS is required for testing and launch, as per REQ-TRN-001.

## 6.4.0.0 External Dependencies

- Contentful API availability and performance.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The backend API endpoint for vocabulary lists must adhere to the P95 latency of < 200ms (NFR-PERF-001).
- The backend service should implement caching for the Contentful response to reduce latency and API calls.

## 7.2.0.0 Security

- Contentful API keys must be stored securely in AWS Secrets Manager and not exposed in code or to the client (NFR-SEC-005).

## 7.3.0.0 Usability

- The Contentful content model should be intuitive for a non-technical Content Manager to use.

## 7.4.0.0 Accessibility

- Not applicable.

## 7.5.0.0 Compatibility

- Not applicable.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated work across three components: Contentful (schema setup), Backend (API endpoint), and Mobile App (data fetching/caching).
- Defining a robust and scalable content model in Contentful.
- Implementing a multi-layer caching strategy (backend and client-side).

## 8.3.0.0 Technical Risks

- Changes to the Contentful content model after launch could be breaking changes for older app versions. The API must be versioned.
- Potential for large vocabulary lists to impact mobile app performance if not handled efficiently.

## 8.4.0.0 Integration Points

- Backend Service <-> Contentful Delivery API
- Mobile App <-> Backend Service API (`/api/v1/vocabulary/lists`)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify CRUD operations on vocabulary content in a test Contentful space.
- Test the backend API's response with published, draft, and no content.
- Test the mobile app's behavior when the API succeeds, fails, or returns empty data.
- Test the offline caching and retrieval mechanism on the mobile app.

## 9.3.0.0 Test Data Needs

- A set of sample vocabulary words and lists in a non-production Contentful space.

## 9.4.0.0 Testing Tools

- Jest (Backend unit tests)
- flutter_test (Flutter unit/widget tests)
- Postman or similar for API endpoint testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Content models for 'Vocabulary Word' and 'Vocabulary List' are created in Contentful.
- Backend API endpoint is implemented, documented in OpenAPI format, and returns expected data.
- Mobile app successfully fetches, caches, and uses the data to populate Free User games.
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage for new code
- Integration testing between mobile, backend, and Contentful completed successfully
- Documentation updated for the new API endpoint and Contentful models
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires both backend and frontend developer capacity.
- The API contract between backend and frontend should be defined early in the sprint.
- Contentful setup can be done in parallel with development.

## 11.4.0.0 Release Impact

- This is a foundational story for the Free User vocabulary game feature. It is critical for the initial release.

