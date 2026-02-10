# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-095 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User dismisses an uninteresting book recommendatio... |
| As A User Story | As an App User viewing my book recommendations, I ... |
| User Persona | Any authenticated user (Free or Premium) who is vi... |
| Business Value | Improves user experience by giving users control o... |
| Functional Area | AI Suggestions |
| Story Theme | Recommendation Interaction and Feedback |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User dismisses a recommendation from the list

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is on the recommendations screen and a list of book recommendations is displayed

### 3.1.5 When

the user taps the 'dismiss' (e.g., 'X') icon on a recommendation card

### 3.1.6 Then

the selected recommendation card is smoothly animated out of the view

### 3.1.7 And

the backend records that the user has dismissed this specific recommendation

### 3.1.8 Validation Notes

Verify UI removal is immediate. Check the database to confirm a feedback entry of type 'dismissed' is created for the user and the specific book/recommendation.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Persistence: Dismissed recommendation does not reappear

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the user has previously dismissed a specific book recommendation

### 3.2.5 When

the user refreshes the recommendation list or navigates away and returns to the screen

### 3.2.6 Then

the previously dismissed book recommendation is not present in the new list of suggestions

### 3.2.7 Validation Notes

This requires backend logic to filter out items the user has dismissed. Test by dismissing, leaving the screen, returning, and checking the list.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Edge Case: User dismisses the last recommendation

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is viewing the last remaining recommendation on the screen

### 3.3.5 When

the user dismisses that recommendation

### 3.3.6 Then

the card is removed and an empty state message is displayed, such as 'All caught up! New recommendations will appear here.'

### 3.3.7 Validation Notes

Verify that the empty state UI component is rendered correctly after the last item is removed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Alternative Flow: User dismisses a recommendation while offline

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

the user is offline and viewing cached recommendations

### 3.4.5 When

the user dismisses a recommendation

### 3.4.6 Then

the recommendation is immediately removed from the local UI

### 3.4.7 And

when network connectivity is restored, the queued action is automatically synced with the backend without user intervention

### 3.4.8 Validation Notes

Test by enabling airplane mode, dismissing an item, then disabling airplane mode. Verify the backend database reflects the dismissal after sync.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dismiss icon (e.g., 'X') on each recommendation card.
- An empty state view with informative text to be displayed when no recommendations are left.

## 4.2.0 User Interactions

- Tapping the dismiss icon removes the card.
- The removal of the card must be accompanied by a smooth animation (e.g., fade out or slide away).

## 4.3.0 Display Requirements

- The UI must update instantly upon dismissal, without requiring a manual refresh.

## 4.4.0 Accessibility Needs

- The dismiss icon must have a minimum tap target size of 44x44dp.
- The icon should have a proper content description for screen readers (e.g., 'Dismiss recommendation').

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A dismissed recommendation for a user should be treated as strong negative feedback.

### 5.1.3 Enforcement Point

Backend AI Suggestion Service (REQ-AIS-001)

### 5.1.4 Violation Handling

This feedback must be stored and included in future prompts to the LLM to refine its output and avoid suggesting similar items.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A dismissed recommendation should not be shown to the same user again for a significant period (e.g., 6 months).

### 5.2.3 Enforcement Point

Backend recommendation generation logic.

### 5.2.4 Violation Handling

The system must filter out previously dismissed items before presenting the final list to the user.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-089

#### 6.1.1.2 Dependency Reason

The system must be able to generate and display AI recommendations before a user can dismiss one.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-091

#### 6.1.2.2 Dependency Reason

The system must be able to generate and display AI recommendations before a user can dismiss one.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to receive and process feedback (e.g., POST /api/v1/recommendations/{id}/feedback).
- The client-side state management (Riverpod) must be able to handle removing an item from a list.
- The offline synchronization mechanism (REQ-OFF-001) must be implemented to queue and sync the dismissal action.

## 6.3.0.0 Data Dependencies

- Each recommendation object must have a unique, persistent identifier.
- A backend data model is required to store user feedback, linking a user ID, the recommendation ID, and the feedback type ('dismissed').

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI action of dismissing a card must complete in under 200ms.
- The background API call to the backend must not block the UI thread.

## 7.2.0.0 Security

- The API endpoint for submitting feedback must be authenticated and authorized to ensure a user can only dismiss their own recommendations.

## 7.3.0.0 Usability

- The dismiss action should be intuitive and provide immediate visual feedback.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards for tap targets and screen reader support.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing the offline queuing and synchronization logic adds complexity.
- Backend logic to ensure this feedback correctly influences future LLM prompts is non-trivial and core to the feature's value.
- Requires coordination between frontend (UI, state, offline sync) and backend (API, database, AI service integration).

## 8.3.0.0 Technical Risks

- Potential for race conditions during offline synchronization if not handled carefully.
- The feedback loop to the LLM could be complex to implement effectively, requiring careful prompt engineering.

## 8.4.0.0 Integration Points

- Client State Management (Riverpod)
- Local Database (Isar)
- Backend Feedback API
- Backend AI Recommendation Service (for incorporating feedback)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Dismiss a single item and verify UI and backend state.
- Dismiss the last item and verify the empty state.
- Dismiss an item while offline, reconnect, and verify backend sync.
- Refresh the list after dismissing and verify the item does not reappear.
- Rapidly dismiss multiple items to check for UI performance issues.

## 9.3.0.0 Test Data Needs

- A test user account with a pre-populated list of recommendations.
- Ability to inspect the backend database to verify feedback has been recorded.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test`, `integration_test`
- Backend: Jest

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing for the client-server interaction completed successfully
- E2E test case for dismissing an item is automated and passing
- User interface animation and empty state reviewed and approved by UX/Design
- Backend logic for storing feedback and filtering future results is verified
- Offline dismissal and sync functionality is tested and confirmed working
- Documentation for the new API endpoint is updated in the OpenAPI specification
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be prioritized alongside other feedback mechanisms (US-092, US-093) to create a cohesive user experience.
- Requires both frontend and backend development effort that can be parallelized.

## 11.4.0.0 Release Impact

This is a key interaction for the recommendation feature, significantly improving its usability and long-term effectiveness. It is critical for the feature's success.

