# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-006 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | New user completes the onboarding flow |
| As A User Story | As a new user who has just logged in for the first... |
| User Persona | A 'New User' who has successfully created an accou... |
| Business Value | Increases user activation and retention by reducin... |
| Functional Area | User Onboarding & First-Time User Experience |
| Story Theme | User Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Onboarding flow is triggered for a new user

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has successfully authenticated for the very first time

### 3.1.5 When

the application finishes the login process

### 3.1.6 Then

the system must display the first screen of the guided onboarding flow, not the main dashboard.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User completes all steps of the onboarding flow

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a new user is in the onboarding flow

### 3.2.5 When

the user sets a yearly reading goal, adds a 'Currently Reading' book, and dismisses the final feature tour

### 3.2.6 Then

the user is navigated to the main dashboard, the dashboard reflects the newly added goal and book, and the onboarding flow is marked as complete for the user on the backend.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User skips all optional steps in the onboarding flow

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

a new user is in the onboarding flow

### 3.3.5 When

the user selects the 'skip' option on the goal-setting screen and the 'skip' option on the add-book screen, and then dismisses the feature tour

### 3.3.6 Then

the user is navigated to the main dashboard, the dashboard is in its empty default state, and the onboarding flow is marked as complete for the user.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Onboarding state persists if the app is closed

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a new user has completed the goal-setting step but has not yet started the add-book step

### 3.4.5 When

the user closes and reopens the application

### 3.4.6 Then

the application must resume the onboarding flow at the add-book step.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Onboarding flow is not shown to existing users

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

a user has previously completed the onboarding flow

### 3.5.5 When

the user logs out and logs back in

### 3.5.6 Then

the system must navigate the user directly to the main dashboard.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User cannot navigate away from the onboarding flow

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

a new user is in the multi-step onboarding flow

### 3.6.5 When

the user attempts to use the system's back gesture or button

### 3.6.6 Then

the application should not allow navigation to the main app until the flow is completed or the final step is dismissed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Progress indicator (e.g., 'Step 1 of 3')
- Primary action buttons (e.g., 'Set Goal', 'Add Book')
- Secondary 'Skip' buttons or links for optional steps
- Feature tour overlay with highlighted elements and descriptive text
- A final 'Done' or 'Get Started' button to dismiss the tour and exit the flow

## 4.2.0 User Interactions

- User progresses linearly through the steps.
- User can skip optional steps to move to the next step.
- User dismisses the final tour overlay to land on the dashboard.

## 4.3.0 Display Requirements

- Each step of the onboarding process should be presented on a distinct screen to maintain focus.
- The UI should be clean, encouraging, and guide the user's attention to the primary task of each step.

## 4.4.0 Accessibility Needs

- All interactive elements (buttons, links) must have clear labels for screen readers.
- Sufficient color contrast must be used for text and controls.
- The flow must be navigable using accessibility services.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The onboarding flow must be shown to every user exactly once upon their first login.

### 5.1.3 Enforcement Point

Immediately after successful first-time authentication.

### 5.1.4 Violation Handling

If the check fails or is bypassed, the user might have a poor initial experience. The system must ensure this check is robust.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Completion of the onboarding flow (either by finishing all steps or skipping to the end) must be permanently recorded in the user's profile.

### 5.2.3 Enforcement Point

After the user dismisses the final step (feature tour).

### 5.2.4 Violation Handling

If the status is not saved, the user will be forced to see the onboarding flow on every login, which is a critical bug.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

User must be able to sign up with Google to become a 'new user'.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-003

#### 6.1.2.2 Dependency Reason

User must be able to sign up with Apple to become a 'new user'.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-007

#### 6.1.3.2 Dependency Reason

The goal-setting functionality is a required component within this onboarding flow.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-009

#### 6.1.4.2 Dependency Reason

The add 'Currently Reading' book functionality is a required component within this onboarding flow.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-011

#### 6.1.5.2 Dependency Reason

The feature tour is the final required component of this flow.

## 6.2.0.0 Technical Dependencies

- Backend User Service: Must support a flag (e.g., 'onboardingCompleted') on the user model.
- Authentication System (Auth0): To determine if a user session is new.
- Client-side State Management (Riverpod): To manage the state and progression of the multi-step flow.

## 6.3.0.0 Data Dependencies

- A mechanism to distinguish a new user from a returning user based on backend data.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Each step of the onboarding flow must load in under 1 second on a standard 4G connection.

## 7.2.0.0 Security

- The action to mark onboarding as complete must be an authenticated API call.

## 7.3.0.0 Usability

- The flow must be intuitive and require minimal cognitive load from the user.
- Instructions on each screen must be clear and concise.

## 7.4.0.0 Accessibility

- The flow must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The onboarding flow must render correctly on all supported iOS and Android devices and OS versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires careful management of navigation state to prevent users from exiting the flow prematurely.
- State must be managed to handle interruptions like app closures.
- Orchestrates multiple distinct features (goal setting, book search, UI tour) into a single, cohesive flow.

## 8.3.0.0 Technical Risks

- Failure to correctly persist the 'onboardingCompleted' state on the backend could lead to a frustrating user experience where the flow is repeated on every login.

## 8.4.0.0 Integration Points

- Backend User Profile API: To read and write the user's onboarding completion status.
- Goal Management Service: To create the initial goal.
- Library Management Service: To add the initial book.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify onboarding is triggered for a brand new user account.
- Verify onboarding is NOT triggered for an existing user account.
- Test the full 'happy path' where the user completes all steps.
- Test the 'skip all' path.
- Test a mixed path (e.g., complete step 1, skip step 2).
- Test app restart during the flow and verify it resumes correctly.
- Verify data set during onboarding is correctly displayed on the dashboard.

## 9.3.0.0 Test Data Needs

- Ability to provision new user accounts in the test environment that have not completed onboarding.
- Ability to provision existing user accounts that have already completed onboarding.

## 9.4.0.0 Testing Tools

- flutter_test
- integration_test

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and Widget tests implemented with >= 80% coverage for the flow logic
- E2E tests for the primary scenarios (new user, existing user, skip all) are implemented and passing
- User interface reviewed and approved by UX/UI designer
- Backend endpoint for updating user's onboarding status is implemented and tested
- Onboarding flow verified on representative iOS and Android physical devices
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the first-time user experience and should be completed early in the project.
- It has dependencies on several other stories, which must be planned accordingly, potentially within the same sprint.

## 11.4.0.0 Release Impact

- Critical for the initial public release. The application cannot launch without a functional onboarding process.

