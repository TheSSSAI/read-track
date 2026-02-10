# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-057 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views goal progress on the dashboard |
| As A User Story | As a user with an active reading goal, I want to s... |
| User Persona | Any authenticated user (Free or Premium) who has s... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Dashboard |
| Story Theme | Goal Management & User Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display of a single active goal for a Free or Premium User

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has one active goal, 'Read 20 books this year', and has completed 5 books

### 3.1.5 When

the user navigates to the dashboard screen

### 3.1.6 Then

a goal progress widget is displayed containing the goal title 'Read 20 books this year', the progress text '5 / 20', and a visual progress bar filled to 25%.

### 3.1.7 Validation Notes

Verify the widget appears and all three elements (title, progress text, progress bar) are present and accurate.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Display of multiple active goals for a Premium User

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a Premium User has two active goals: 'Read 50 books this year' (progress: 10) and 'Read 30 minutes per day' (progress: 15)

### 3.2.5 When

the user navigates to the dashboard screen

### 3.2.6 Then

two distinct goal progress widgets are displayed in a list, one for each goal, showing the correct respective progress ('10 / 50' and '15 / 30').

### 3.2.7 Validation Notes

Verify that multiple widgets are rendered correctly in a scrollable or stacked list and that each displays the correct data for its associated goal.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Goal progress updates automatically after logging a reading session

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

a user is on the dashboard and their 'Read 100 pages this week' goal shows progress of '50 / 100'

### 3.3.5 When

the user logs a new reading session of 20 pages

### 3.3.6 Then

the goal progress widget on the dashboard reactively updates to show '70 / 100' and the progress bar adjusts to 70% without requiring a manual refresh.

### 3.3.7 Validation Notes

This must be tested via an end-to-end flow: view dashboard -> log session -> return to dashboard and verify the update.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Dashboard state when a user has no active goals

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a user has not set any active goals or has deleted all previous goals

### 3.4.5 When

the user navigates to the dashboard screen

### 3.4.6 Then

the area for goal progress displays a clear call-to-action message, such as 'Set a reading goal to get started!', with a button that navigates to the goal creation screen.

### 3.4.7 Validation Notes

Verify that no goal widgets are shown and the call-to-action is present and functional.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Display of a completed goal (100% progress)

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user has a goal to 'Read 10 books' and has just logged their 10th book

### 3.5.5 When

the user views the dashboard

### 3.5.6 Then

the goal progress widget shows '10 / 10', the progress bar is 100% full, and a distinct visual indicator of completion (e.g., a checkmark icon, a change in color) is present.

### 3.5.7 Validation Notes

Verify the visual treatment for a completed goal is distinct from an in-progress goal.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Display of a goal where progress exceeds the target

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a user has a goal to 'Read 10 books' and has logged 11 books

### 3.6.5 When

the user views the dashboard

### 3.6.6 Then

the goal progress widget correctly displays the progress text as '11 / 10' and the progress bar remains 100% full.

### 3.6.7 Validation Notes

Ensure the progress text accurately reflects exceeding the goal while the visual bar caps at 100%.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A container/card on the dashboard for 'Goal Progress'.
- Individual goal widgets within the container.
- For each widget: a text label for the goal title, a text label for numeric progress (e.g., 'X / Y'), and a horizontal progress bar.
- A call-to-action element (text and button) for the 'no active goals' state.

## 4.2.0 User Interactions

- The list of goal widgets should be vertically scrollable if it exceeds the allocated space on the dashboard.
- Tapping on a goal widget (or a 'View All' button) should navigate the user to the main Goals screen (Note: this navigation is out of scope for this story but the element should be designed to support it).
- The UI should update reactively without user intervention after a reading session is logged.

## 4.3.0 Display Requirements

- Progress calculation must be accurate for all goal types (books, pages, time) and periods (day, week, month, year).
- The display must clearly differentiate between multiple concurrent goals for Premium users.

## 4.4.0 Accessibility Needs

- All text must respect the user's OS-level font size settings (Dynamic Type).
- Progress bars and text must have sufficient color contrast to meet WCAG 2.1 AA standards.
- Screen readers should announce the full goal status, e.g., 'Goal: Read 20 books this year. Progress: 5 of 20 books.'

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Free Users can only have one active goal displayed.

### 5.1.3 Enforcement Point

Dashboard UI rendering logic.

### 5.1.4 Violation Handling

The UI should never be in a state to display more than one goal for a Free User, as this is prevented by the goal creation logic (US-014).

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Premium Users can have unlimited active goals displayed.

### 5.2.3 Enforcement Point

Dashboard UI rendering logic.

### 5.2.4 Violation Handling

The UI must be able to render a list of all active goals for a Premium User.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

Functionality to create a 'books per year' goal must exist to be displayed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-061

#### 6.1.2.2 Dependency Reason

Functionality to create a 'pages per period' goal must exist to be displayed.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-062

#### 6.1.3.2 Dependency Reason

Functionality to create a 'time per period' goal must exist to be displayed.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-046

#### 6.1.4.2 Dependency Reason

Functionality to log reading progress by page number is required to update goal progress.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-048

#### 6.1.5.2 Dependency Reason

Functionality to log reading time is required to update time-based goals.

## 6.2.0.0 Technical Dependencies

- State management solution (Riverpod) must be implemented to handle reactive UI updates.
- Local database (Isar) schema for Goals and ReadingSessions must be defined and implemented.
- A robust data layer for fetching and calculating goal progress is required.

## 6.3.0.0 Data Dependencies

- Access to the user's list of active goals.
- Access to the user's reading session history to calculate progress against each goal's criteria.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The dashboard, including goal progress calculation and rendering, must load in under 1.5 seconds on a standard 4G network (NFR-PERF-002).
- Goal progress calculations must be performed efficiently and should not block the UI thread, especially on app start.

## 7.2.0.0 Security

- N/A for this specific story, as it only displays user-owned data.

## 7.3.0.0 Usability

- The progress display must be intuitive and immediately understandable to the user.
- The 'no goals' state should effectively guide the user toward setting a goal.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The UI must render correctly on all supported iOS and Android screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The logic for calculating progress for different goal types (books, pages, time) and time periods (day, week, month, year) requires careful implementation, especially regarding date and timezone handling for rolling periods like 'week' or 'month'.
- Ensuring the UI is reactive and performs well, even when a Premium user has many goals and a long reading history.
- Designing a flexible UI component that handles all states: single goal, multiple goals, no goals, and completed goals.

## 8.3.0.0 Technical Risks

- Incorrectly handling timezones for daily/weekly goals could lead to inaccurate progress reporting.
- Inefficient data queries for progress calculation could slow down dashboard load times.

## 8.4.0.0 Integration Points

- Local database (Isar) for fetching goal and session data.
- State management (Riverpod) for propagating data changes to the UI.
- Navigation system for handling taps on the call-to-action button.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify progress calculation for a yearly book goal.
- Verify progress calculation for a daily time-based goal, testing across midnight.
- Verify progress calculation for a weekly page-based goal, testing across the week boundary.
- Test UI rendering for a Free User with one goal.
- Test UI rendering for a Premium User with 5+ goals (to test scrolling).
- Test the E2E flow: User has no goals -> Taps CTA -> Creates a goal -> Returns to dashboard and sees the new goal widget.

## 9.3.0.0 Test Data Needs

- Test accounts for both Free and Premium users.
- Accounts pre-populated with various goal configurations (no goals, one goal, multiple goals).
- Accounts with extensive reading history to test performance.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for all progress calculation logic with >80% coverage
- Widget tests implemented for all UI states of the goal progress component
- Integration testing completed successfully to verify reactive updates
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on target devices
- Accessibility requirements (WCAG 2.1 AA) validated
- Documentation updated for the dashboard components
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature for the dashboard and a key driver of user engagement.
- Must be planned after the foundational stories for goal creation and session logging are completed.
- The complexity of the progress calculation logic should be factored into planning.

## 11.4.0.0 Release Impact

- This feature is critical for the initial release as it is a primary component of the main user-facing screen.

