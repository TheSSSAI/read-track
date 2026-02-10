# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-093 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User provides negative feedback on a recommendatio... |
| As A User Story | As a user receiving AI-powered book recommendation... |
| User Persona | Any user (Free or Premium) who is presented with A... |
| Business Value | Improves the relevance and accuracy of the AI reco... |
| Functional Area | AI Suggestions |
| Story Theme | Recommendation Engine Enhancement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User successfully provides negative feedback on a recommendation

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is viewing an AI-generated book recommendation that has 'thumbs up' and 'thumbs down' icons

### 3.1.5 When

the user taps the 'thumbs down' icon

### 3.1.6 Then

the 'thumbs down' icon visually changes to a selected state (e.g., filled icon), the 'thumbs up' icon (if previously selected) reverts to a default state, and the backend successfully records the negative feedback for that user and recommendation.

### 3.1.7 Validation Notes

Verify via UI inspection and by checking the backend database to confirm a new feedback record is created with type 'negative'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User undoes their negative feedback

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

a user has already provided negative feedback on a recommendation, and the 'thumbs down' icon is in a selected state

### 3.2.5 When

the user taps the 'thumbs down' icon again

### 3.2.6 Then

the icon reverts to its default, unselected state, and the backend deletes or invalidates the previously recorded negative feedback.

### 3.2.7 Validation Notes

Verify the UI icon returns to its original state and the corresponding record is removed from the backend database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User switches from positive to negative feedback

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

a user has already provided positive feedback on a recommendation, and the 'thumbs up' icon is in a selected state

### 3.3.5 When

the user taps the 'thumbs down' icon on the same recommendation

### 3.3.6 Then

the 'thumbs up' icon reverts to its default state, the 'thumbs down' icon changes to its selected state, and the backend updates the user's feedback from positive to negative for that recommendation.

### 3.3.7 Validation Notes

Verify UI state changes for both icons and confirm the backend record is updated, not duplicated.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System uses negative feedback to refine future recommendations

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a user has provided negative feedback on a specific book or type of book

### 3.4.5 When

the user requests a new set of AI recommendations

### 3.4.6 Then

the prompt sent to the LLM API includes information about the user's negative feedback to guide the generation of more relevant results.

### 3.4.7 Validation Notes

Requires backend log inspection to verify the LLM prompt contains context like 'User disliked [Book Title]. Avoid similar recommendations.' This is a backend validation.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Providing feedback fails due to network error

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

a user is viewing recommendations and the device is offline

### 3.5.5 When

the user taps the 'thumbs down' icon

### 3.5.6 Then

the UI state does not permanently change, and a non-blocking, user-friendly error message (e.g., a toast notification) is displayed, such as 'Could not save feedback. Please check your connection.'

### 3.5.7 Validation Notes

Test by enabling airplane mode on the device. The UI should show an optimistic change and then revert upon failure, or not change at all, and display the specified error.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Providing feedback fails due to a server error

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

the backend API for submitting feedback will return a 5xx error

### 3.6.5 When

the user taps the 'thumbs down' icon

### 3.6.6 Then

the UI state does not permanently change, and a generic error message is displayed to the user.

### 3.6.7 Validation Notes

Use a tool like Charles Proxy or mock the API response in the client to simulate a server error and verify the app's graceful handling.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'thumbs down' icon button must be present on each recommendation card.
- A toast notification or similar non-modal element for displaying feedback/error messages.

## 4.2.0 User Interactions

- Tapping the 'thumbs down' icon toggles its state between selected and unselected.
- The interaction should provide immediate visual feedback (optimistic UI update).
- Selecting 'thumbs down' should automatically deselect 'thumbs up' for the same item.

## 4.3.0 Display Requirements

- The icon must have distinct visual states for 'selected' and 'unselected' (e.g., filled vs. outline).
- The recommendation card may be visually deemphasized (e.g., greyed out) or hidden after negative feedback is given.

## 4.4.0 Accessibility Needs

- The 'thumbs down' icon button must have a descriptive content label, such as 'Dislike this recommendation', for screen readers.
- The tap target size must comply with WCAG 2.1 guidelines.
- Color changes for state must have sufficient contrast.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user can only provide one type of feedback (positive or negative) per recommendation item at any given time.', 'enforcement_point': 'Client-side UI logic and Backend API.', 'violation_handling': 'The most recent feedback action overwrites any previous feedback for that item.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-089

#### 6.1.1.2 Dependency Reason

The ability for a Free User to request and view AI recommendations must exist before feedback can be provided.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-091

#### 6.1.2.2 Dependency Reason

The ability for a Premium User to request and view AI recommendations must exist before feedback can be provided.

## 6.2.0.0 Technical Dependencies

- The backend recommendation service (REQ-AIS-001) that generates LLM prompts.
- A database schema capable of storing user feedback on recommendations.

## 6.3.0.0 Data Dependencies

- Requires access to the user's ID and the unique identifier for each recommended item.

## 6.4.0.0 External Dependencies

- The backend service's integration with the OpenAI GPT-4 API must be capable of handling refined prompts.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to submit feedback must have a P95 latency of less than 200ms as per REQ-PER-001.
- The UI interaction must be responsive and not block the main thread.

## 7.2.0.0 Security

- The API endpoint for submitting feedback must be authenticated and authorized, ensuring users can only submit feedback for their own account.
- All input must be validated on the backend to prevent malicious data submission.

## 7.3.0.0 Usability

- The feedback mechanism should be intuitive and require minimal user effort.
- The system's response to the feedback should be immediate and clear.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards, particularly for tap target size and color contrast.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend: Requires a new data model and API endpoint for storing feedback.
- Backend: The primary complexity is in modifying the prompt engineering logic to effectively incorporate negative feedback into future LLM calls.
- Frontend: Requires state management for the feedback icons and handling of API success/error states.

## 8.3.0.0 Technical Risks

- The effectiveness of refining LLM prompts with negative feedback can be variable and may require significant tuning.
- Potential for race conditions if a user rapidly toggles feedback; the backend must handle this gracefully (e.g., last write wins).

## 8.4.0.0 Integration Points

- Client UI -> Backend Feedback API
- Backend Feedback API -> Primary Database (Aurora)
- Backend Recommendation Service -> Primary Database (to fetch feedback)
- Backend Recommendation Service -> OpenAI API (to send refined prompt)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Usability
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify providing negative feedback works as expected.
- Verify undoing feedback works.
- Verify switching from positive to negative feedback works.
- Verify behavior during network and server errors.
- Manually verify that after disliking several books of a specific genre, subsequent recommendations are less likely to feature that genre.

## 9.3.0.0 Test Data Needs

- Test user accounts (both Free and Premium).
- A set of pre-generated recommendations to interact with.

## 9.4.0.0 Testing Tools

- Flutter's `flutter_test` and `integration_test` packages.
- Jest for backend unit tests.
- Postman or similar for API endpoint testing.
- Sentry for monitoring errors in production.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing completed successfully between client and backend
- User interface reviewed and approved for both iOS and Android
- Performance requirements verified (API latency < 200ms)
- Security requirements validated (endpoint is authenticated)
- Accessibility labels and contrast checked
- Backend API endpoint is documented in the OpenAPI specification
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be developed in conjunction with or immediately after US-092 (Positive Feedback) to ensure UI and backend consistency.
- Requires close collaboration between frontend and backend developers, especially regarding the API contract for feedback submission.

## 11.4.0.0 Release Impact

Enhances a core feature of the application. Its inclusion significantly improves the user's ability to personalize their experience.

