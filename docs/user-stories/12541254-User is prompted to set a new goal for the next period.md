# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-067 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User is prompted to set a new goal for the next pe... |
| As A User Story | As a goal-oriented reader, I want to be prompted t... |
| User Persona | Any user (Free or Premium) who has an active goal ... |
| Business Value | Increases user retention and long-term engagement ... |
| Functional Area | Goal Management |
| Story Theme | User Engagement and Retention |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User with an expired yearly goal is prompted to set a new one

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user had an active yearly goal that ended yesterday

### 3.1.5 When

the user launches the app for the first time today and after they have dismissed the 'Year in Review' summary

### 3.1.6 Then

a modal dialog is displayed with an encouraging title like 'Ready for Your Next Challenge?'

### 3.1.7 Validation Notes

Verify the modal appears only after the 'Year in Review' flow (US-066) is complete. The trigger should be the first app session after the goal's end date.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User accepts the prompt and navigates to goal creation

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the 'Set New Goal' prompt modal is displayed

### 3.2.5 When

the user taps the primary Call-To-Action button (e.g., 'Set New Goal')

### 3.2.6 Then

the modal is dismissed and the user is navigated directly to the goal creation screen.

### 3.2.7 Validation Notes

The navigation target should be the starting point of the goal creation flow (defined in US-060, US-061, US-062).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User dismisses the prompt for later

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

the 'Set New Goal' prompt modal is displayed

### 3.3.5 When

the user taps the secondary dismiss action (e.g., 'Maybe Later')

### 3.3.6 Then

the modal is dismissed and the user remains on the current screen (e.g., Dashboard).

### 3.3.7 Validation Notes

Verify the modal closes and no navigation occurs.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Prompt does not reappear after being dismissed

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a user has previously dismissed the 'Set New Goal' prompt for a specific expired goal

### 3.4.5 When

the user closes and relaunches the application

### 3.4.6 Then

the modal prompt for that same expired goal does not appear again.

### 3.4.7 Validation Notes

A state flag must be persisted locally to prevent re-prompting for the same goal completion event.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User has multiple goals ending simultaneously

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user had both a weekly and a monthly goal that ended on the same day

### 3.5.5 When

the user launches the app after the goals have ended

### 3.5.6 Then

only a single, generic 'Set New Goal' prompt is displayed, not one for each expired goal.

### 3.5.7 Validation Notes

The trigger logic should check for any expired goals and fire a single event, rather than one event per goal.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Modal Dialog
- Title Text (e.g., 'Ready for Your Next Challenge?')
- Body Text (e.g., 'You've completed your last goal. Keep the momentum going by setting a new one!')
- Primary CTA Button (e.g., 'Set New Goal')
- Secondary Text Button/Link (e.g., 'Maybe Later')

## 4.2.0 User Interactions

- Tapping the primary button navigates to the goal creation flow.
- Tapping the secondary action dismisses the modal.

## 4.3.0 Display Requirements

- The prompt must be displayed after the 'Year in Review' summary (US-066) if applicable.
- The prompt should appear on the first app launch after a goal's period has concluded.

## 4.4.0 Accessibility Needs

- The modal dialog must be fully accessible via screen readers (e.g., VoiceOver, TalkBack).
- All buttons must have clear, descriptive labels for accessibility services.
- Text must adhere to WCAG 2.1 AA contrast ratios.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user shall be prompted to create a new goal only once per goal-period conclusion event.', 'enforcement_point': 'Client-side logic upon application launch.', 'violation_handling': 'If the prompt is dismissed, it should not be shown again for the same event. A persistent flag should be set to prevent this.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-066

#### 6.1.1.2 Dependency Reason

The 'Year in Review' summary is the primary celebration of a goal's completion. This prompt is the logical next step and should appear only after the summary has been viewed or dismissed to create a smooth user flow.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-060

#### 6.1.2.2 Dependency Reason

The primary CTA of this story navigates to the 'Set a goal for number of books' screen. That screen must exist.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-061

#### 6.1.3.2 Dependency Reason

The goal creation flow, which this story links to, includes setting page-based goals.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-062

#### 6.1.4.2 Dependency Reason

The goal creation flow, which this story links to, includes setting time-based goals.

## 6.2.0.0 Technical Dependencies

- Local database (Isar) to store goal data and the 'prompt_shown' flag.
- State management (Riverpod) to handle the logic for displaying the prompt.

## 6.3.0.0 Data Dependencies

- Requires access to the user's list of goals, specifically their end dates and types.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The check for expired goals on app startup must not add any noticeable delay (<50ms) to the initial screen load time.

## 7.2.0.0 Security

*No items available*

## 7.3.0.0 Usability

- The prompt should be encouraging and not feel like a demand.
- The flow should be seamless, taking the user directly to the relevant screen with one tap.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The modal and its trigger logic must function correctly on all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires careful state management to ensure the prompt is shown only once.
- Coordination with the 'Year in Review' (US-066) flow is critical and requires a clear sequence of events.
- Logic must handle multiple goals ending at the same time gracefully.

## 8.3.0.0 Technical Risks

- Potential for a race condition if the check for expired goals runs at the same time as other startup logic. The trigger should be tied to a specific point in the app's lifecycle, like the Dashboard's `initState`.

## 8.4.0.0 Integration Points

- App launch/initialization sequence.
- User's goal data stored in the local Isar database.
- Navigation service to route the user to the goal creation screen.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration

## 9.2.0.0 Test Scenarios

- Verify prompt appears for a goal that ended yesterday.
- Verify prompt does NOT appear for a goal ending tomorrow.
- Verify prompt appears after 'Year in Review' is dismissed.
- Verify tapping 'Set New Goal' navigates correctly.
- Verify tapping 'Maybe Later' dismisses the modal and sets the flag to not show again.
- Verify the prompt does not reappear on subsequent app launches after dismissal.
- Verify a single prompt appears when multiple goals end on the same day.

## 9.3.0.0 Test Data Needs

- Test user accounts with various goal configurations: no goals, one active goal, one recently expired goal, multiple expired goals.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end flow validation.
- A time-mocking library to control the device's current date for testing date-based triggers.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage for new logic
- Widget tests for the modal UI are implemented and passing
- Integration testing for the full user flow (launch -> summary -> prompt -> navigate/dismiss) completed successfully
- User interface reviewed and approved by UX/UI designer
- Accessibility requirements (screen reader, labels) validated
- Documentation for the triggering logic updated in the developer wiki
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story must be scheduled in a sprint after its prerequisite stories (especially US-066) are completed and merged.
- Requires coordination with the developer working on the goal creation flow to ensure the navigation target is correct.

## 11.4.0.0 Release Impact

- Enhances the user journey for goal completion, contributing to the overall polish and engagement loop of the feature.

