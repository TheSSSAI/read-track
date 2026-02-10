# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-089 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User requests an AI book recommendation withi... |
| As A User Story | As a Free User, I want to request an AI-powered bo... |
| User Persona | Free User: A user on the free tier of the applicat... |
| Business Value | Increases user engagement by providing a high-valu... |
| Functional Area | AI-Powered Features |
| Story Theme | Book Discovery and Recommendations |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User requests a recommendation with suggestions remaining

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in 'Free User' and I have used fewer than 5 AI suggestions in the current calendar month

### 3.1.5 When

I tap the 'Get AI Recommendation' button

### 3.1.6 Then

The system must display a loading indicator while the request is in progress, the UI must remain responsive, an asynchronous request is sent to the backend, my monthly suggestion count is incremented by one in the database, and upon success, the loading indicator is replaced with a formatted book recommendation card.

### 3.1.7 Validation Notes

Verify via UI inspection and by checking the user's `ai_suggestion_count` in the database before and after the request. The recommendation card should be visible.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Edge Case: User uses their last remaining suggestion for the month

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

I am a logged-in 'Free User' and I have used exactly 4 AI suggestions in the current calendar month

### 3.2.5 When

I tap the 'Get AI Recommendation' button and the request succeeds

### 3.2.6 Then

A recommendation is successfully displayed, my monthly suggestion count becomes 5, and the 'Get AI Recommendation' button becomes disabled or visually indicates the limit has been reached.

### 3.2.7 Validation Notes

Verify the UI state of the button changes after the 5th successful request. The database count should be 5.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: Recommendation generation fails on the backend

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in 'Free User' with suggestions remaining

### 3.3.5 When

I request a recommendation, but the backend service or external LLM API returns an error

### 3.3.6 Then

The loading indicator must be removed, a user-friendly error message (as defined in US-096) must be displayed, and my monthly suggestion count must NOT be incremented.

### 3.3.7 Validation Notes

Simulate a 5xx error from the backend API. Verify the UI shows the correct error message and the user's `ai_suggestion_count` in the database remains unchanged.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

UI State: Button state reflects available suggestions

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am a logged-in 'Free User'

### 3.4.5 When

I navigate to the screen with the recommendation feature

### 3.4.6 Then

The 'Get AI Recommendation' button must be enabled if my monthly suggestion count is less than 5, and disabled if the count is 5 or more.

### 3.4.7 Validation Notes

Test with two user accounts: one with `ai_suggestion_count` < 5 and one with `ai_suggestion_count` = 5. Verify the button's initial state on screen load.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Data Model: Monthly suggestion counter resets

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

It is the last day of the month and I am a 'Free User' with a suggestion count of 5

### 3.5.5 When

The system date changes to the first day of the next month

### 3.5.6 Then

My `ai_suggestion_count` must be automatically reset to 0 by a scheduled backend process.

### 3.5.7 Validation Notes

This requires a time-based integration test or manual verification after a scheduled job runs at the start of a new month.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled button, e.g., 'Get AI Recommendation' or 'Discover a New Book'.
- A loading state indicator (e.g., shimmer placeholder, spinner) to show while the recommendation is being fetched.
- A display area for the final recommendation card, including book cover, title, author, and a brief summary/reason.
- A user-friendly error message display area.

## 4.2.0 User Interactions

- Tapping the button initiates the asynchronous request.
- The UI must not be blocked during the request.
- The recommendation card itself will have interactive elements as defined in US-094 and US-095.

## 4.3.0 Display Requirements

- The UI should clearly indicate how many suggestions are left for the month, or that the limit has been reached.

## 4.4.0 Accessibility Needs

- The loading state must be announced by screen readers.
- All buttons and interactive elements must have accessible labels.
- Error messages must be accessible and announced to the user.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-AIS-001

### 5.1.2 Rule Description

Free Users are limited to a maximum of 5 successful AI suggestions per calendar month.

### 5.1.3 Enforcement Point

Backend API endpoint, before calling the recommendation generation service.

### 5.1.4 Violation Handling

The API request is rejected with an appropriate error code. The client displays an upgrade prompt (as defined in US-090).

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-AIS-002

### 5.2.2 Rule Description

The monthly suggestion counter is only incremented upon successful generation of a recommendation.

### 5.2.3 Enforcement Point

Backend API endpoint, after receiving a successful response from the recommendation service.

### 5.2.4 Violation Handling

If the recommendation service fails, the counter is not changed.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to have an identity and associated usage limits.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-094

#### 6.1.2.2 Dependency Reason

The recommendation card requires a 'Save to Want to Read' action.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-095

#### 6.1.3.2 Dependency Reason

The recommendation card requires a 'Dismiss' action.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-096

#### 6.1.4.2 Dependency Reason

Defines the graceful error handling required when the recommendation service is unavailable.

## 6.2.0.0 Technical Dependencies

- Backend recommendation service (REQ-AIS-001) must be implemented and deployed.
- Database schema must be updated to include a counter for monthly AI suggestions on the User model.
- A scheduled job mechanism (e.g., AWS EventBridge Scheduler) for resetting the monthly counter.

## 6.3.0.0 Data Dependencies

- User's reading history data is required to generate personalized recommendations. The system must handle new users with no data gracefully.

## 6.4.0.0 External Dependencies

- OpenAI GPT-4 API for generating the recommendation content.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The recommendation request must be asynchronous and not block the UI thread.
- The client-side UI should update within 500ms of receiving the API response.

## 7.2.0.0 Security

- All API requests to the backend must be authenticated with a valid JWT.
- The backend must validate that the user making the request is the owner of the account whose limit is being checked.

## 7.3.0.0 Usability

- The user must receive clear feedback on the status of their request (loading, success, error).
- The limit on suggestions should be communicated clearly to the user.

## 7.4.0.0 Accessibility

- WCAG 2.1 Level AA standards must be met.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions as per REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both frontend state management for the async flow and backend logic for limit enforcement.
- Implementation of the scheduled job to reset monthly counters adds a separate component to manage and test.
- Coordination between the client and backend on error handling and state management is critical.

## 8.3.0.0 Technical Risks

- The external LLM API could have high latency, requiring careful management of user expectations and timeouts.
- The scheduled job for resetting counters must be robust and handle failures to avoid users being incorrectly blocked or given unlimited suggestions.

## 8.4.0.0 Integration Points

- Client App -> Backend API Gateway
- Backend API -> User Database (Aurora)
- Backend API -> Recommendation Service
- AWS EventBridge Scheduler -> Backend Service/Lambda for counter reset

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- A free user with 0 suggestions requests one.
- A free user with 4 suggestions requests one.
- A free user with 5 suggestions attempts to view the request button (should be disabled).
- The API call fails and the user's count is not incremented.
- Verification of the monthly counter reset job.

## 9.3.0.0 Test Data Needs

- Test accounts for 'Free User' with suggestion counts of 0, 4, and 5.
- Mock API responses for both successful and failed recommendation generation.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test`, `integration_test`
- Backend (Jest): Framework for testing Node.js/TypeScript logic.
- Tools to manually trigger or simulate the monthly scheduled job.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing for both frontend and backend logic with >80% coverage
- Integration testing completed for the client-server interaction and the counter reset job
- User interface reviewed and approved by UX/Product
- Performance requirements verified (UI remains responsive)
- Security requirements validated (endpoint is protected)
- Documentation updated for the new API endpoint and the scheduled job
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story depends on the core recommendation service being available. It should be planned in a sprint after that service is complete.
- The scheduled job for counter reset might be a separate technical task but is essential for this story to be considered complete.

## 11.4.0.0 Release Impact

- This is a key user-facing feature and a primary driver for the freemium upgrade path. It is critical for the initial release.

