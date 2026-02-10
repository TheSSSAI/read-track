# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-015 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User sees a contextual upgrade prompt when hi... |
| As A User Story | As a Free User, I want to be shown a clear and hel... |
| User Persona | Free User: An authenticated user on the free tier ... |
| Business Value | This is a primary monetization driver. It creates ... |
| Functional Area | User Account & Monetization |
| Story Theme | Freemium Model Enforcement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Free User hits the library item limit

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a 'Free User' and I have exactly 20 items in my library

### 3.1.5 When

I attempt to add a 21st item (book or article)

### 3.1.6 Then

the action to add the item is prevented, and a non-intrusive upgrade prompt is displayed.

### 3.1.7 Validation Notes

Verify the library count remains 20. The prompt can be a modal or a bottom sheet. It must contain specific text about the item limit.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Free User hits the active goal limit

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a 'Free User' and I have one active goal

### 3.2.5 When

I attempt to create a second active goal

### 3.2.6 Then

the goal creation process is blocked, and a non-intrusive upgrade prompt is displayed.

### 3.2.7 Validation Notes

Verify that the user is not navigated to the goal creation screen, or that the final 'save' action is blocked. The prompt must mention the goal limit.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Free User hits the monthly AI suggestion limit

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am a 'Free User' and I have already received 5 AI suggestions in the current calendar month

### 3.3.5 When

I request a 6th AI suggestion

### 3.3.6 Then

the request to the AI service is not made, and a non-intrusive upgrade prompt is displayed.

### 3.3.7 Validation Notes

Verify no API call is made to the AI service. The prompt must mention the monthly suggestion limit.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Upgrade prompt content is clear and actionable

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the upgrade prompt is displayed for any reason

### 3.4.5 When

I view the prompt

### 3.4.6 Then

it must contain a clear message explaining which limit was reached, a primary call-to-action button like 'Upgrade to Premium', and a secondary option to 'Dismiss' or 'Maybe Later'.

### 3.4.7 Validation Notes

Check the text for clarity and ensure both buttons are present and visually distinct.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User taps the upgrade button on the prompt

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the upgrade prompt is displayed

### 3.5.5 When

I tap the 'Upgrade to Premium' button

### 3.5.6 Then

I am navigated to the dedicated subscription screen which details the premium benefits and allows me to initiate an in-app purchase.

### 3.5.7 Validation Notes

Verify correct navigation to the screen implemented in US-017 and US-018.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User dismisses the upgrade prompt

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

the upgrade prompt is displayed

### 3.6.5 When

I tap the 'Dismiss' or 'Maybe Later' button

### 3.6.6 Then

the prompt is closed, and I am returned to my previous state in the app without the action being completed.

### 3.6.7 Validation Notes

Verify the prompt disappears and the UI is responsive.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Hitting a limit while offline

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a 'Free User' with 20 library items and my device is offline

### 3.7.5 When

I attempt to add a 21st item

### 3.7.6 Then

the action is still prevented based on locally cached data, and the upgrade prompt is displayed.

### 3.7.7 Validation Notes

The 'Upgrade' button may be disabled or show a message indicating an internet connection is required to proceed with the purchase.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A reusable modal dialog or bottom sheet component for the prompt.
- Primary call-to-action button (e.g., 'Upgrade to Premium').
- Secondary dismiss button or link (e.g., 'Maybe Later').
- Dynamic text area to display the specific limit that was reached.

## 4.2.0 User Interactions

- The prompt should appear without blocking the entire UI if possible (e.g., a bottom sheet is less intrusive than a full-screen modal).
- Tapping outside a modal prompt should also dismiss it.
- The appearance of the prompt should be animated smoothly.

## 4.3.0 Display Requirements

- The prompt must clearly and concisely state the specific limitation that was encountered (e.g., 'Upgrade to track more than 20 books.').
- The prompt should briefly mention the primary benefit of upgrading related to the limit (e.g., 'Get unlimited tracking with Premium.').

## 4.4.0 Accessibility Needs

- All text in the prompt must respect the user's dynamic type settings.
- Buttons must have sufficient color contrast and touch target size.
- The prompt component must be navigable via screen readers, which should announce the reason for the prompt upon its appearance.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A Free User is limited to 20 library items.

### 5.1.3 Enforcement Point

Client-side before attempting to save a new item; Server-side upon receiving the save request.

### 5.1.4 Violation Handling

The save action is rejected, and the client displays the upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

A Free User is limited to one active goal.

### 5.2.3 Enforcement Point

Client-side before showing the goal creation UI or on save; Server-side upon receiving the creation request.

### 5.2.4 Violation Handling

The creation action is rejected, and the client displays the upgrade prompt.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A Free User is limited to 5 AI suggestions per calendar month.

### 5.3.3 Enforcement Point

Client-side before making the API request; Server-side via rate-limiting logic based on user ID and tier.

### 5.3.4 Violation Handling

The API request is rejected, and the client displays the upgrade prompt.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-013

#### 6.1.1.2 Dependency Reason

Implements the core logic for preventing more than 20 library items.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-014

#### 6.1.2.2 Dependency Reason

Implements the core logic for preventing more than one active goal.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-090

#### 6.1.3.2 Dependency Reason

Implements the core logic for preventing more than 5 monthly AI suggestions.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-018

#### 6.1.4.2 Dependency Reason

The 'Upgrade' button must navigate to the subscription screen built in this story.

## 6.2.0.0 Technical Dependencies

- A reusable UI component library for modals/bottom sheets.
- State management solution (Riverpod) to provide user status and usage counts to the UI.
- A centralized configuration service (backend) to define the specific limits for each tier.

## 6.3.0.0 Data Dependencies

- The application must have access to the user's current subscription tier ('Free User').
- The application must have an accurate, real-time or recently synced count of the user's library items, active goals, and AI suggestions used this month.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The limit check and display of the prompt must be visually instantaneous (<200ms) after the user's action.

## 7.2.0.0 Security

- Limit enforcement must be validated on the backend to prevent client-side bypass.

## 7.3.0.0 Usability

- The prompt must be non-intrusive and not disrupt the user's flow unnecessarily. The messaging must be positive and encouraging, not punitive.

## 7.4.0.0 Accessibility

- The prompt must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The prompt component must render correctly on all supported iOS and Android screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires creating a generic, reusable prompt component that can be configured with different text for various limits.
- Needs to be integrated into three separate feature flows (library, goals, AI).
- The logic for checking limits must work reliably offline using cached data.

## 8.3.0.0 Technical Risks

- Risk of inconsistent implementation if a generic component is not used.
- Potential for poor UX if the prompt is too aggressive or the messaging is unclear.
- Bugs in the limit-checking logic could frustrate users or cause revenue loss.

## 8.4.0.0 Integration Points

- Library management service (when adding a book/article).
- Goal management service (when creating a goal).
- AI suggestion service (when requesting a recommendation).
- Navigation service (to route to the subscription screen).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify prompt appears for book limit.
- Verify prompt appears for goal limit.
- Verify prompt appears for AI suggestion limit.
- Verify tapping 'Upgrade' navigates correctly.
- Verify tapping 'Dismiss' closes the prompt.
- Verify the entire flow works correctly while the device is offline.

## 9.3.0.0 Test Data Needs

- A test account in the 'Free User' state.
- Ability to programmatically set the user's library count to 20.
- Ability to programmatically set the user's active goal count to 1.
- Ability to programmatically set the user's AI suggestion usage for the month to 5.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing for all three limit types.
- The upgrade prompt is implemented as a single, reusable component.
- Code has been peer-reviewed and approved.
- Unit and widget tests achieve >80% code coverage for the new logic and components.
- E2E tests for all three limit scenarios are implemented and passing.
- UI/UX has been reviewed and approved by the design team.
- Functionality has been verified on both iOS and Android physical devices in the staging environment.
- No accessibility violations are reported by automated tools.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a key part of the monetization strategy. It should be prioritized alongside the stories that enforce the limits (US-013, US-014, US-090) and the main subscription screen (US-018).

## 11.4.0.0 Release Impact

- Critical for the initial release as it is the primary mechanism to drive subscriptions.

