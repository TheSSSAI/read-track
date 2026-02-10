# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-008 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | New user skips setting a goal during onboarding |
| As A User Story | As a new user who is unsure about my reading habit... |
| User Persona | A new user who has just authenticated for the firs... |
| Business Value | Reduces friction in the user onboarding process, i... |
| Functional Area | User Onboarding |
| Story Theme | User Registration and Onboarding |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User successfully skips the goal setting step

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a new user is on the 'Set Your Goal' screen within the onboarding flow

### 3.1.5 When

the user taps the 'Skip' or 'Maybe Later' button

### 3.1.6 Then

the application immediately navigates to the next step in the onboarding flow, which is 'Add Currently Reading Book'

### 3.1.7 Validation Notes

Verify via manual testing or E2E test that the navigation occurs and no goal is created in the application's state or sent to the backend upon onboarding completion.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Dashboard correctly handles the absence of a goal post-onboarding

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

a new user has completed the entire onboarding flow after skipping the goal setting step

### 3.2.5 When

the user lands on the main Dashboard screen (REQ-DSH-001)

### 3.2.6 Then

the area designated for goal progress displays a non-error, empty state with a clear call-to-action, such as 'Set your first reading goal!'

### 3.2.7 Validation Notes

Verify the dashboard UI does not crash or display null/undefined values. The call-to-action should navigate the user to the main goal creation screen.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User navigates back after skipping

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

a new user is on the 'Set Your Goal' screen and taps 'Skip'

### 3.3.5 When

the user is on the subsequent 'Add Currently Reading Book' screen and taps the 'Back' navigation control

### 3.3.6 Then

the user is returned to the 'Set Your Goal' screen, and the input fields are in their default, empty state

### 3.3.7 Validation Notes

Ensure that navigating back and forth does not inadvertently set a goal or cause the application state to become inconsistent.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A clearly labeled button or text link, such as 'Skip for now' or 'Maybe Later', must be present on the goal-setting screen of the onboarding flow.

## 4.2.0 User Interactions

- Tapping the 'Skip' element should trigger an immediate transition to the next screen without requiring a confirmation dialog.

## 4.3.0 Display Requirements

- The 'Skip' option should be less visually prominent than the primary call-to-action (e.g., 'Set Goal') to gently guide users, but must still be easily discoverable.

## 4.4.0 Accessibility Needs

- The 'Skip' control must have a minimum touch target size of 44x44 points to comply with accessibility guidelines.
- The control must have a proper accessibility label for screen readers (e.g., 'Skip goal setting').

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user account can exist without any active goals.', 'enforcement_point': 'Application-wide, particularly on the Dashboard and Goals screens.', 'violation_handling': 'N/A. This rule defines an allowed state. UI components dependent on goals must handle this state gracefully.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

This story implements a specific path within the overall onboarding flow. The main onboarding flow structure must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-007

#### 6.1.2.2 Dependency Reason

This story provides an alternative path on the same screen where a user can set a goal. Both stories should be developed in conjunction.

## 6.2.0.0 Technical Dependencies

- The application's state management solution (Riverpod) must be in place to manage the state of the onboarding process across multiple screens.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The screen transition after tapping 'Skip' must complete in under 200ms on a mid-range device.

## 7.2.0.0 Security

*No items available*

## 7.3.0.0 Usability

- The option to skip must be intuitive and not require user explanation.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA for touch target size and contrast for the 'Skip' control.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Primarily a UI and navigation logic change.
- Requires coordination with the implementation of the Dashboard (US-057) to ensure the 'no goal' state is handled correctly.

## 8.3.0.0 Technical Risks

- Minor risk of introducing a regression where other parts of the app assume a goal always exists. This must be mitigated by testing dependent features.

## 8.4.0.0 Integration Points

- Integrates with the application's navigation router.
- Affects the state passed to the Dashboard and Goals features.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- E2E (End-to-End)

## 9.2.0.0 Test Scenarios

- Verify a user can complete the entire onboarding flow by skipping the goal-setting step.
- Verify the dashboard's appearance and functionality when no goal has been set.
- Verify that a user who skipped can successfully create a goal later from the main app interface.

## 9.3.0.0 Test Data Needs

- A fresh user account that has not completed onboarding.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and widget tests implemented with sufficient coverage for the onboarding state logic
- E2E test for the 'skip goal' onboarding path is implemented and passing
- User interface reviewed and approved by UX/UI designer
- Accessibility requirements (touch target, labels) have been verified
- No regressions are introduced on the Dashboard or Goals screens
- Documentation for the onboarding flow is updated to reflect this path
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

1

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Should be planned in the same sprint as US-007 ('New user sets a yearly reading goal during onboarding') as they are two sides of the same screen.
- Must be completed before the Dashboard story (US-057) can be considered fully done, as the dashboard depends on handling the 'no goal' state.

## 11.4.0.0 Release Impact

This is a core part of the initial user experience and is required for the first release.

