# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-085 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User adds a word to their personal vocabul... |
| As A User Story | As a Premium User, I want to manually add a word, ... |
| User Persona | Premium User: A subscribed user who is actively en... |
| Business Value | This is a core premium feature that provides direc... |
| Functional Area | Vocabulary Building |
| Story Theme | Premium Features |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Premium User successfully adds a new word

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Premium User on the vocabulary screen with an active internet connection

### 3.1.5 When

I tap the 'Add Word' action, enter valid text for 'Word', 'Definition', and 'Example Sentence', and tap 'Save'

### 3.1.6 Then

the new word is saved to my personal vocabulary list, the UI updates to show the new word, and a success confirmation message is briefly displayed.

### 3.1.7 Validation Notes

Verify the new word is persisted in the backend database and is available for vocabulary games.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Error Condition: Free User attempts to add a word

### 3.2.3 Scenario Type

Error_Condition

### 3.2.4 Given

I am a logged-in Free User

### 3.2.5 When

I attempt to access the 'Add Word' functionality

### 3.2.6 Then

the action is blocked, and the system displays a non-intrusive prompt to upgrade to the Premium tier.

### 3.2.7 Validation Notes

The 'Add Word' button should either be disabled or, if tapped, trigger the upgrade modal. The API endpoint must reject the request with a 403 Forbidden status.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Error Condition: User attempts to save with empty required fields

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a Premium User in the 'Add Word' form

### 3.3.5 When

I leave the 'Word' or 'Definition' field empty and tap 'Save'

### 3.3.6 Then

the word is not saved, and a clear validation error message is displayed next to the corresponding empty field.

### 3.3.7 Validation Notes

The 'Save' button should be disabled until all required fields are filled. Test with each required field being empty individually.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: User attempts to add a duplicate word

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a Premium User and the word 'Ephemeral' already exists in my vocabulary list

### 3.4.5 When

I attempt to add 'Ephemeral' again

### 3.4.6 Then

the word is not saved, and I see an error message stating 'This word is already in your list'.

### 3.4.7 Validation Notes

The check for duplicates should be case-insensitive to prevent variations like 'ephemeral' and 'Ephemeral'.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Alternative Flow: User cancels adding a word

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am a Premium User in the 'Add Word' form with some text entered

### 3.5.5 When

I tap the 'Cancel' button or use the back gesture

### 3.5.6 Then

the form is dismissed, and no changes are saved to my vocabulary list.

### 3.5.7 Validation Notes

Verify that no new entry is created in the local or remote database.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Edge Case: User adds a word while offline

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a Premium User and my device is offline

### 3.6.5 When

I successfully fill out the 'Add Word' form and tap 'Save'

### 3.6.6 Then

the word is saved to the local device database (Isar), the UI updates to show the new word, and the data is queued for synchronization.

### 3.6.7 Validation Notes

After the device reconnects to the internet, verify the word is automatically synced to the backend without further user interaction.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly visible 'Add Word' button (e.g., Floating Action Button) on the main vocabulary screen.
- A modal or new screen for adding a word.
- Text input field for 'Word' with a label.
- Text area input field for 'Definition' with a label.
- Text area input field for 'Example Sentence' with a label (can be optional).
- A 'Save' button.
- A 'Cancel' or 'Close' button/icon.

## 4.2.0 User Interactions

- Tapping 'Add Word' opens the entry form.
- The 'Save' button is disabled until all required fields contain text.
- Input fields show inline validation errors upon attempting to save with invalid data.
- Successful save dismisses the form and shows a toast/snackbar notification (e.g., 'Word added successfully').

## 4.3.0 Display Requirements

- The vocabulary list must immediately reflect the newly added word after a successful save.

## 4.4.0 Accessibility Needs

- All input fields must have semantic labels for screen readers.
- Buttons must have accessible names.
- Sufficient color contrast for text and UI elements must be maintained per WCAG 2.1 AA.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-VOC-001

### 5.1.2 Rule Description

Only users with an active 'Premium User' status can add words to a personal vocabulary list.

### 5.1.3 Enforcement Point

Client-side (UI disable/hiding) and Server-side (API endpoint authorization).

### 5.1.4 Violation Handling

Client displays an upgrade prompt. Server returns a 403 Forbidden error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-VOC-002

### 5.2.2 Rule Description

The 'Word' and 'Definition' fields are mandatory. The 'Example Sentence' is optional but recommended.

### 5.2.3 Enforcement Point

Client-side form validation and Server-side API validation.

### 5.2.4 Violation Handling

Client shows inline error messages. Server returns a 400 Bad Request error with details.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-VOC-003

### 5.3.2 Rule Description

A user cannot add the same word (case-insensitive) to their list more than once.

### 5.3.3 Enforcement Point

Server-side API validation.

### 5.3.4 Violation Handling

Server returns a 409 Conflict error. Client displays a user-friendly message.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must support a Premium subscription tier to identify eligible users for this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-004

#### 6.1.2.2 Dependency Reason

User must be able to log in to have an account to which the vocabulary list is associated.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-101

#### 6.1.3.2 Dependency Reason

The offline data synchronization mechanism must be implemented to handle offline word additions.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint for adding a word (`POST /api/v1/vocabulary`).
- Definition of the `VocabularyWord` entity in the backend (Aurora) and client (Isar) databases.
- JWT-based authentication and authorization middleware to check for 'Premium User' role.

## 6.3.0.0 Data Dependencies

- User's subscription status must be available and verifiable by the backend.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for saving a new word should be under 200ms (P95).
- The UI transition to and from the 'Add Word' form should be smooth and without jank.

## 7.2.0.0 Security

- The API endpoint must be protected and require an authenticated session of a Premium User.
- All user-provided text must be sanitized on the backend before being stored in the database to prevent XSS and other injection attacks.

## 7.3.0.0 Usability

- The process of adding a word should be quick and intuitive, requiring minimal taps.
- Error messages must be clear, concise, and helpful.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires coordinated changes across frontend, backend, and local database.
- Implementation of robust offline support and synchronization logic.
- Requires strict server-side authorization logic to protect a paid feature.

## 8.3.0.0 Technical Risks

- A flaw in the authorization check could allow Free Users to access a Premium feature, impacting monetization.
- Potential for data loss or sync conflicts if the offline mechanism is not implemented correctly.

## 8.4.0.0 Integration Points

- Local Isar database for offline storage.
- Backend REST API for data persistence.
- User authentication service to verify subscription status.
- Vocabulary games feature, which will consume the data created by this story.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security

## 9.2.0.0 Test Scenarios

- Verify a Premium User can add, see, and use a new word in games.
- Verify a Free User is correctly blocked from adding a word at both the UI and API level.
- Verify form validation for all fields.
- Verify offline addition and successful synchronization upon reconnection.
- Verify that attempting to call the API endpoint with a Free User's token results in a 403 error.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' roles.
- A pre-populated vocabulary list to test the duplicate word scenario.

## 9.4.0.0 Testing Tools

- Flutter: `flutter_test`, `integration_test`
- Backend: Jest
- API Testing: Postman or an equivalent tool for direct API calls.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented for new logic, achieving >80% code coverage
- Automated E2E test for the happy path is created and passing in the CI/CD pipeline
- UI/UX for the 'Add Word' flow has been reviewed and approved by the design team
- Security check confirms the API endpoint is properly secured
- Backend API endpoint is documented in the OpenAPI specification
- Story has been successfully deployed and verified in the staging environment by QA

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational feature for the Premium vocabulary track and is a prerequisite for the personalized vocabulary games (US-086, US-087).
- Requires both frontend and backend development effort, which can be parallelized.

## 11.4.0.0 Release Impact

This is a key marketable feature for the Premium subscription tier and should be highlighted in release notes and marketing materials.

