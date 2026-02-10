# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-094 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User saves an AI-recommended book to their 'Want t... |
| As A User Story | As a user viewing AI-powered book recommendations,... |
| User Persona | Any authenticated user (both 'Free User' and 'Prem... |
| Business Value | Increases user engagement with the AI recommendati... |
| Functional Area | AI Suggestions & Library Management |
| Story Theme | AI-Powered Recommendations |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful save for a user with available library space

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user (either Premium, or a Free User with fewer than 20 items in their library) is viewing an AI book recommendation that is not already in their library

### 3.1.5 When

the user taps the 'Save' or '+' icon on the recommendation card

### 3.1.6 Then

the system initiates an asynchronous request to add the book to the user's library

### 3.1.7 And

the book appears in the 'Want to Read' list when the user navigates to their library.

### 3.1.8 Validation Notes

Verify via UI feedback, toast message, and by checking the library screen. The backend database should show a new LibraryItem with the correct user_id and shelf status.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Free User attempts to save a book when at their library limit

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

a 'Free User' with exactly 20 items in their library is viewing an AI book recommendation

### 3.2.5 When

the user taps the 'Save' icon on the recommendation card

### 3.2.6 Then

the system prevents the book from being added

### 3.2.7 And

the 'Save' icon on the recommendation card does not change its state.

### 3.2.8 Validation Notes

Verify that no API call to add the book is made, or that the backend rejects it with a specific 'limit reached' error code. The upgrade prompt modal must be displayed as specified in REQ-FRE-001.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User views a recommendation for a book already in their library

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

a user is viewing an AI book recommendation

### 3.3.5 And

the indicator is not interactive and has no tap action.

### 3.3.6 When

the recommendation card is rendered

### 3.3.7 Then

the 'Save' icon is replaced with a static 'In Library' indicator (e.g., a checkmark icon)

### 3.3.8 Validation Notes

The client application must check if the recommended book's identifier (e.g., ISBN) exists in the local cache of the user's library before rendering the card.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Network or API error during save attempt

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a user is viewing an AI book recommendation

### 3.4.5 When

the user taps the 'Save' icon and the API call to the backend fails (e.g., no network, 5xx server error)

### 3.4.6 Then

the loading state on the icon reverts back to the initial 'Save' state

### 3.4.7 And

the book is not added to the user's library.

### 3.4.8 Validation Notes

Simulate network failure using device settings or a proxy tool. Verify the UI reverts correctly and the error message is shown.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- An interactive 'Save' icon/button (e.g., bookmark or plus icon) on each AI recommendation card.
- A loading state indicator (e.g., spinner) for the icon/button.
- A 'Saved' or 'In Library' state indicator (e.g., checkmark icon).
- A non-blocking toast notification component for success and error messages.

## 4.2.0 User Interactions

- A single tap on the 'Save' icon initiates the save process.
- The icon should be unresponsive while in the loading state to prevent duplicate requests.
- The 'In Library' state should be non-interactive.

## 4.3.0 Display Requirements

- The state of the save action (idle, loading, saved, error) must be clearly communicated to the user through the icon's visual state.

## 4.4.0 Accessibility Needs

- The save icon/button must have a minimum tap target size of 44x44dp.
- The different states of the icon (Save, Saved) should have accessible labels for screen readers (e.g., 'Save to Want to Read', 'Book is in your library').

# 5.0.0 Business Rules

- {'rule_id': 'BR-FRE-001', 'rule_description': 'Free Users are limited to a maximum of 20 items in their library. This rule is defined in REQ-BUS-001.', 'enforcement_point': 'Backend API endpoint for adding a LibraryItem. The client may perform a pre-check to provide a better UX.', 'violation_handling': "The API should return a specific error response (e.g., 403 Forbidden with an error code 'LIMIT_EXCEEDED'). The client must interpret this response and display the upgrade prompt."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-089

#### 6.1.1.2 Dependency Reason

The system must be able to generate and display AI recommendations before a user can interact with them.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-091

#### 6.1.2.2 Dependency Reason

The system must be able to generate and display AI recommendations before a user can interact with them.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-041

#### 6.1.3.2 Dependency Reason

The core backend logic and API endpoint for adding any book to the 'Want to Read' shelf must exist.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-015

#### 6.1.4.2 Dependency Reason

The UI component for the 'upgrade prompt' must be available to be displayed when a Free User hits their library limit.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint: `POST /api/v1/library`
- Client-side state management (Riverpod) to handle UI updates and data synchronization.
- Access to user's subscription status (Free/Premium) on both client and server.

## 6.3.0.0 Data Dependencies

- The AI recommendation must provide sufficient book metadata (e.g., Title, Author, ISBN, Cover URL) to create a new LibraryItem.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI feedback (icon changing to loading state) must be instantaneous (<100ms) upon user tap.
- The P95 latency for the backend API call to save the book should be under 200ms as per REQ-PER-001.

## 7.2.0.0 Security

- The API endpoint for adding a book must be protected and require a valid JWT.
- The endpoint must validate that the user associated with the JWT is the one for whom the book is being added.

## 7.3.0.0 Usability

- The action should be a simple, one-tap interaction.
- Feedback on the action's status (in-progress, success, failure) must be clear and immediate.

## 7.4.0.0 Accessibility

- Adherence to WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Leverages existing 'add to library' functionality.
- Primary work involves UI integration on the recommendation card and handling different states.
- Requires client-side logic to check if a book is already in the library to determine the initial button state.

## 8.3.0.0 Technical Risks

- Potential for race conditions if the user's library data on the client is stale. The backend must be the source of truth for the library limit check.

## 8.4.0.0 Integration Points

- Frontend: AI Recommendation UI component.
- Backend: User Authentication service (to get user ID and tier), Library Management service (to add the book).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test saving a book as a Premium user.
- Test saving a book as a Free user with 19 items in library.
- Test attempting to save a book as a Free user with 20 items in library.
- Test viewing a recommendation for a book already on the 'Want to Read' shelf.
- Test viewing a recommendation for a book already on the 'Read' shelf.
- Test the save action with network connectivity disabled.

## 9.3.0.0 Test Data Needs

- Test accounts for a 'Free User' with 19 items.
- Test accounts for a 'Free User' with 20 items.
- Test accounts for a 'Premium User'.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test`, `integration_test`
- Backend: `Jest`

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage
- Integration testing between client and backend completed successfully
- User interface reviewed and approved for all states (idle, loading, saved, in-library)
- Performance requirements for API latency and UI responsiveness verified
- Accessibility labels and tap targets implemented and verified
- Documentation for the API endpoint updated in OpenAPI spec
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is critical for making the AI recommendation feature actionable and should be prioritized soon after the recommendation display is complete.

## 11.4.0.0 Release Impact

Significantly improves the user experience and value of the AI suggestions feature.

