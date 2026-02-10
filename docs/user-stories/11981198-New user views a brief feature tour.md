# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-011 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | New user views a brief feature tour |
| As A User Story | As a new user who has just completed the onboardin... |
| User Persona | A first-time user who has successfully authenticat... |
| Business Value | Increases user activation and engagement by reduci... |
| Functional Area | User Onboarding & First-Time User Experience |
| Story Theme | User Activation and Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Tour starts automatically after onboarding

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a new user has just completed the final step of the onboarding flow

### 3.1.5 When

the user is navigated to the main Dashboard screen for the first time

### 3.1.6 Then

a feature tour overlay automatically appears, highlighting the first key feature.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Tour highlights key application features in sequence

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the feature tour is active

### 3.2.5 When

the user progresses through the tour

### 3.2.6 Then

the tour must sequentially highlight the following elements: the 'Currently Reading' section on the Dashboard, the primary 'Log a Reading Session' button, and the 'Goals' tab in the main navigation.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User navigates through the tour steps

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the user is viewing a step in the feature tour

### 3.3.5 When

the user taps the 'Next' button

### 3.3.6 Then

the tour advances to the next highlighted feature, and the descriptive text is updated accordingly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User completes the tour

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the user is on the final step of the feature tour

### 3.4.5 When

the user taps the 'Finish' (or 'Done') button

### 3.4.6 Then

the tour overlay is dismissed, the user is given full control of the application, and a flag is set indicating the tour has been completed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User skips the tour

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the user is viewing any step of the feature tour

### 3.5.5 When

the user taps the 'Skip' or 'X' button

### 3.5.6 Then

the tour overlay is immediately dismissed, the user is given full control of the application, and a flag is set indicating the tour has been completed/skipped.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Tour is only shown once

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a user has previously completed or skipped the feature tour

### 3.6.5 When

the user closes and re-opens the app, or logs out and logs back in

### 3.6.6 Then

the feature tour is not displayed again.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Tour state is preserved during app interruption

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

the user is in the middle of the feature tour (e.g., on step 2 of 3)

### 3.7.5 When

the application is sent to the background and then brought back to the foreground

### 3.7.6 Then

the feature tour is still active and displayed on the same step (step 2 of 3).

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A screen overlay that dims the background UI.
- A 'spotlight' effect that highlights a specific widget.
- A tooltip/pop-up box associated with each spotlight, containing descriptive text.
- 'Next' and 'Finish'/'Done' buttons for navigation.
- A 'Skip' or 'X' icon to dismiss the tour at any time.

## 4.2.0 User Interactions

- Tapping 'Next' proceeds to the next step.
- Tapping 'Finish'/'Done' ends the tour.
- Tapping 'Skip'/'X' ends the tour.
- Tapping outside the highlighted area or tooltip should not dismiss the tour or interact with the background UI.

## 4.3.0 Display Requirements

- The text in each tooltip must be concise and clearly explain the purpose of the highlighted feature.
- The tour UI must respect the device's current theme (Light/Dark Mode) as per REQ-UIF-001.

## 4.4.0 Accessibility Needs

- Tooltip text must scale according to the user's OS-level font size settings (Dynamic Type) as per REQ-UIF-001.
- All tour controls ('Next', 'Skip', etc.) must have proper accessibility labels for screen readers.
- The content of each tooltip must be announced by screen readers (e.g., VoiceOver, TalkBack) when it appears.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The feature tour must only be shown once to each new user.', 'enforcement_point': 'Immediately before rendering the Dashboard screen after the onboarding flow.', 'violation_handling': "If the 'tour_completed' flag is true, the tour logic is skipped entirely."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

The feature tour is triggered immediately upon completion of the onboarding flow.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-058

#### 6.1.2.2 Dependency Reason

The dashboard's 'Log a Reading Session' button must exist to be highlighted by the tour.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-057

#### 6.1.3.2 Dependency Reason

The 'Goals' tab/navigation item must exist in the main UI to be highlighted by the tour.

## 6.2.0.0 Technical Dependencies

- A client-side persistence mechanism (Isar database, as per REQ-OFF-001) to store the 'tour_completed' flag for the user.
- A Flutter package or custom component for creating guided tours/showcases.

## 6.3.0.0 Data Dependencies

*No items available*

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The tour overlay and animations must be smooth (60fps) and not cause any noticeable UI lag on target devices (REQ-OPE-001).

## 7.2.0.0 Security

- N/A for this feature.

## 7.3.0.0 Usability

- The tour must be brief, focusing on no more than 3-4 key features to avoid overwhelming the user.
- The option to skip the tour must be clearly visible at all times.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards, particularly for text contrast and control labeling (REQ-UIF-001).

## 7.5.0.0 Compatibility

- The tour must render correctly on all supported iOS and Android screen sizes and orientations.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Ensuring the tour correctly targets widgets that may be built asynchronously.
- Handling device orientation changes and app lifecycle events (background/foreground) gracefully.
- Selecting and integrating a robust third-party library or building a custom solution.
- Ensuring the tour does not interfere with the underlying UI state.

## 8.3.0.0 Technical Risks

- The tour logic could be brittle and break if the UI structure of the highlighted screens changes significantly. Stable GlobalKeys should be used to identify target widgets.
- A third-party library may have unresolved bugs or performance issues.

## 8.4.0.0 Integration Points

- Integrates with the navigation logic following the onboarding flow.
- Reads/writes a user preference flag to the local Isar database.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Widget
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify tour starts for a fresh user install after onboarding.
- Verify tour does NOT start for a user who has already seen it.
- Verify successful step-by-step completion of the tour.
- Verify skipping the tour from the first step and a middle step.
- Verify tour UI adapts correctly to portrait and landscape orientations.
- Verify tour resumes correctly after the app is backgrounded and foregrounded.
- Verify all tour elements with a screen reader (VoiceOver/TalkBack).

## 9.3.0.0 Test Data Needs

- A test account state representing a brand new user who has just finished onboarding.
- A test account state representing a user who has already completed the tour.

## 9.4.0.0 Testing Tools

- flutter_test for widget tests.
- integration_test package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented and passing with >80% coverage for new logic
- E2E integration test for the full tour flow is implemented and passing
- User interface reviewed and approved by UX/UI designer
- Performance verified on mid-range physical devices
- Accessibility requirements validated with screen readers
- The 'tour_completed' flag is correctly persisted and checked
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a direct dependency on the completion of the main dashboard and onboarding user stories.
- Allocate time for testing on multiple physical devices with different form factors.

## 11.4.0.0 Release Impact

This is a core feature for the initial release and is critical for the new user experience.

