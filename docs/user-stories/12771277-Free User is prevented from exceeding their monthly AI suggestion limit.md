# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-090 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User is prevented from exceeding their monthl... |
| As A User Story | As a Free User who has already used my monthly quo... |
| User Persona | Free User (An authenticated user on the free subsc... |
| Business Value | This is a primary monetization driver, converting ... |
| Functional Area | AI Suggestions & Monetization |
| Story Theme | Freemium Model Enforcement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Free User reaches their monthly limit

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a 'Free User' logged into the application, and my AI suggestion count for the current calendar month is 5

### 3.1.5 When

I perform an action to request a new AI book recommendation

### 3.1.6 Then

The application must prevent a new API call to the external LLM service

### 3.1.7 And

The dialog must contain a secondary, less prominent option to dismiss it, such as 'Maybe Later' or an 'X' icon

### 3.1.8 Validation Notes

Verify backend logs to confirm no call was made to the OpenAI API. Verify the UI displays the correct modal. The API should return a specific error code, e.g., 429 Too Many Requests with a body indicating 'LIMIT_EXCEEDED'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User interacts with the upgrade prompt

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

The 'limit reached' upgrade prompt is displayed

### 3.2.5 When

I tap the 'Upgrade to Premium' button

### 3.2.6 Then

The native in-app purchase flow for the user's platform (Apple App Store or Google Play Store) must be initiated

### 3.2.7 Validation Notes

This action should trigger the functionality defined in US-018.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User dismisses the upgrade prompt

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

The 'limit reached' upgrade prompt is displayed

### 3.3.5 When

I tap the 'Maybe Later' button or the 'X' icon

### 3.3.6 Then

The modal dialog is dismissed, and I am returned to the screen I was on before making the request

### 3.3.7 Validation Notes

The UI should return to its previous state without any errors.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Suggestion count resets at the beginning of a new month

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a 'Free User' and I used 5 AI suggestions last month

### 3.4.5 And

My AI suggestion count for the new month is set to 1

### 3.4.6 When

I perform an action to request a new AI book recommendation

### 3.4.7 Then

The request is successful, and a recommendation is provided

### 3.4.8 Validation Notes

Requires testing with manipulated system dates or a dedicated test environment where the date can be controlled. The backend logic must correctly identify the change in month.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User who just upgraded can immediately get suggestions

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a 'Free User' who was just blocked for exceeding my limit

### 3.5.5 And

I successfully complete the upgrade to a 'Premium User' subscription

### 3.5.6 When

I immediately attempt to request another AI suggestion

### 3.5.7 Then

The request is successful, and a recommendation is provided

### 3.5.8 Validation Notes

The user's subscription status must be updated and recognized by the AI suggestion service in near real-time.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Free User has not yet reached their limit

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I am a 'Free User' and my AI suggestion count for the current month is 4

### 3.6.5 When

I request a new AI book recommendation

### 3.6.6 Then

The request is successful, a recommendation is provided, and my count for the month becomes 5

### 3.6.7 Validation Notes

Verify the user's suggestion count is correctly incremented in the database.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Modal Dialog: A system-consistent modal for displaying the limit message.
- Primary Button: 'Upgrade to Premium' or similar text.
- Secondary Button/Link: 'Maybe Later' or a close 'X' icon.

## 4.2.0 User Interactions

- Tapping the request action triggers either the suggestion or the modal.
- Tapping the primary button initiates the in-app purchase flow.
- Tapping the secondary action dismisses the modal.

## 4.3.0 Display Requirements

- The modal must clearly communicate the reason for the block (monthly limit reached).
- The modal should briefly mention that Premium offers unlimited suggestions.

## 4.4.0 Accessibility Needs

- The modal must be focus-trappable for screen reader users.
- All text must respect the user's OS-level font size settings (Dynamic Type).
- Buttons must have accessible labels and sufficient tap targets.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-AIS-001

### 5.1.2 Rule Description

Free Users are limited to 5 AI-powered suggestions per calendar month.

### 5.1.3 Enforcement Point

Backend API: Before processing a request to the AI recommendation service.

### 5.1.4 Violation Handling

The API request is rejected with a specific error code (e.g., 429) and message. The client application interprets this error to display the upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-AIS-002

### 5.2.2 Rule Description

The monthly AI suggestion count for a user resets to 0 on the first day of each calendar month (00:00 UTC).

### 5.2.3 Enforcement Point

Backend API: When checking the user's current count, the logic must compare the timestamp of the last request with the current date.

### 5.2.4 Violation Handling

N/A - This is a state-resetting rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-089

#### 6.1.1.2 Dependency Reason

The basic functionality for a Free User to request an AI suggestion must exist before limits can be applied to it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-018

#### 6.1.2.2 Dependency Reason

The 'Upgrade' button in the prompt must trigger the in-app purchase flow defined in this story.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-019

#### 6.1.3.2 Dependency Reason

The full upgrade path from this prompt requires the ability to complete a native subscription purchase.

## 6.2.0.0 Technical Dependencies

- Backend User Profile Service: Must be able to store and update a user's monthly suggestion count and the timestamp of their last request.
- Backend API Gateway/Middleware: A mechanism to check user tier and usage before forwarding requests to the AI service.
- Client-side In-App Purchase Module: Must be available to be invoked from the upgrade prompt.

## 6.3.0.0 Data Dependencies

- User's subscription status ('Free User') must be readily available to the backend service.
- A new field in the user data model is required to track `aiSuggestionCount`.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The backend check for the user's suggestion limit must add negligible latency (< 20ms) to the API call.

## 7.2.0.0 Security

- The enforcement of the suggestion limit must be performed exclusively on the backend to prevent client-side tampering or bypass.

## 7.3.0.0 Usability

- The upgrade prompt must be clear, concise, and non-deceptive. The user should immediately understand why they are being blocked and what the path forward is.

## 7.4.0.0 Accessibility

- The upgrade prompt modal must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The display of the modal and its interaction must be consistent across all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated changes across both frontend and backend.
- Backend logic must correctly handle date and time calculations for the monthly reset, preferably in UTC to avoid timezone issues.
- Frontend needs a new, potentially reusable, modal component that can be triggered by a specific API response.
- State management on the client needs to handle the transition to the in-app purchase flow and back.

## 8.3.0.0 Technical Risks

- Incorrectly handling the monthly reset logic could lead to users being unfairly blocked or getting too many free suggestions.
- Failure to properly handle the API error on the client could result in a poor user experience (e.g., an infinite spinner or a generic error message).

## 8.4.0.0 Integration Points

- Backend AI Suggestion Endpoint: This is the primary point of enforcement.
- User Database Model (Aurora): Requires schema modification to add usage tracking fields.
- Client-side API Client (Dio): Interceptor logic is needed to catch the specific 'limit reached' error and trigger the UI flow.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- A Free User successfully makes their 5th request.
- A Free User is blocked on their 6th request and sees the upgrade modal.
- The suggestion count correctly resets on the 1st of the next month.
- A user who upgrades can immediately make a new request.
- The API correctly returns a 429 (or similar) error when the limit is hit.

## 9.3.0.0 Test Data Needs

- Test accounts for 'Free Users'.
- A mechanism for QA to easily set/reset the `aiSuggestionCount` for a test user in the database to avoid having to make 5 real API calls for every test run.

## 9.4.0.0 Testing Tools

- Jest (Backend unit tests)
- flutter_test / integration_test (Flutter tests)
- Postman or similar for direct API integration testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Backend code for limit checking and monthly reset is unit tested
- Frontend modal component is unit and widget tested
- End-to-end automated test for the limit-block-upgrade flow is created and passing
- Code reviewed and approved by at least one other engineer for both frontend and backend
- API returns the correct error code and is documented in the OpenAPI spec
- The feature has been verified on both iOS and Android physical devices
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a key part of the monetization funnel and should be prioritized accordingly.
- Requires both a frontend and a backend developer. Ensure their availability is coordinated for the sprint.

## 11.4.0.0 Release Impact

This feature is critical for the Freemium business model and should be included in any release that also includes the AI suggestions feature.

