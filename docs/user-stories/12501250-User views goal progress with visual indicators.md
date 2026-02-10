# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-063 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views goal progress with visual indicators |
| As A User Story | As a goal-oriented reader, I want to see my progre... |
| User Persona | Any user (Free or Premium) who has set an active g... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Goal Management & Dashboard |
| Story Theme | Reading Motivation and Tracking |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Displaying progress for a yearly book goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has a yearly goal to read 20 books AND the user has marked 5 books as 'Read' in the current year

### 3.1.5 When

the user views their active goals on the Dashboard or Goals screen

### 3.1.6 Then

a visual indicator (e.g., progress bar) is displayed showing 25% completion AND the text '5 / 20 books' is displayed alongside the indicator.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Displaying progress for a weekly page goal

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a user has a weekly goal to read 200 pages AND the user has logged reading sessions totaling 150 pages within the current week

### 3.2.5 When

the user views their active goals

### 3.2.6 Then

a visual indicator is displayed showing 75% completion AND the text '150 / 200 pages' is displayed.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Displaying progress for a daily time goal

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

a user has a daily goal to read for 30 minutes AND the user has logged reading sessions totaling 15 minutes for the current day

### 3.3.5 When

the user views their active goals

### 3.3.6 Then

a visual indicator is displayed showing 50% completion AND the text '15 / 30 minutes' is displayed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Displaying a completed goal

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a user has a monthly goal to read 500 pages AND the user has logged sessions totaling exactly 500 pages for the current month

### 3.4.5 When

the user views their active goals

### 3.4.6 Then

the visual indicator is displayed at 100% (full) AND the text '500 / 500 pages' is displayed AND a visual cue for completion (e.g., a checkmark icon) is shown.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Displaying an exceeded goal

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user has a yearly goal to read 10 books AND the user has marked 12 books as 'Read' in the current year

### 3.5.5 When

the user views their active goals

### 3.5.6 Then

the visual indicator is displayed at 100% (full) AND the text '12 / 10 books' is displayed AND a visual cue for completion is shown.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Displaying a goal with no progress

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a user has just set a new goal to read 100 pages this week AND has not logged any reading sessions yet

### 3.6.5 When

the user views their active goals

### 3.6.6 Then

the visual indicator is displayed at 0% (empty) AND the text '0 / 100 pages' is displayed.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Progress indicator updates after logging a new session

### 3.7.3 Scenario Type

Alternative_Flow

### 3.7.4 Given

a user has a daily page goal with current progress of '50 / 100 pages'

### 3.7.5 When

the user logs a new reading session of 25 pages for the current day

### 3.7.6 Then

the goal's visual indicator immediately updates to show 75% completion AND the text updates to '75 / 100 pages'.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Progress indicator updates after editing a goal's target

### 3.8.3 Scenario Type

Alternative_Flow

### 3.8.4 Given

a user has a yearly book goal with current progress of '10 / 20 books' (50%)

### 3.8.5 When

the user edits the goal target to 40 books

### 3.8.6 Then

the goal's visual indicator immediately updates to show 25% completion AND the text updates to '10 / 40 books'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Progress Bar component
- Text label for current progress (e.g., '150 / 200 pages')
- Text label for goal type (e.g., 'Weekly Page Goal')
- Icon/indicator for completed goals (e.g., checkmark)

## 4.2.0 User Interactions

- The progress indicator is a display-only component; no direct user interaction is required.

## 4.3.0 Display Requirements

- The component must be displayed for each active goal on the Dashboard and the dedicated Goals screen.
- The progress bar's fill level must accurately reflect the percentage of goal completion.
- Numerical progress (current vs. target) must be clearly displayed.

## 4.4.0 Accessibility Needs

- The progress bar component must be accessible to screen readers, providing context on the current progress (e.g., 'Progress: 50 percent').
- Colors used for the progress bar and background must have a contrast ratio of at least 4.5:1 to meet WCAG 2.1 AA standards, as per REQ-UIF-001.
- Text must support dynamic type scaling based on OS settings.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Goal progress calculation for a given period (day, week, month, year) must only include reading sessions or completed books that fall within that specific period's start and end dates.", 'enforcement_point': 'Data aggregation logic before rendering the UI component.', 'violation_handling': 'Incorrect data will lead to inaccurate progress display. Unit tests must cover date boundary conditions.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

User must be able to set a book-based goal before its progress can be displayed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-061

#### 6.1.2.2 Dependency Reason

User must be able to set a page-based goal before its progress can be displayed.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-062

#### 6.1.3.2 Dependency Reason

User must be able to set a time-based goal before its progress can be displayed.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-046

#### 6.1.4.2 Dependency Reason

User must be able to log progress by page number, which is the data source for page-based goals.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-049

#### 6.1.5.2 Dependency Reason

User must be able to log reading time, which is the data source for time-based goals.

### 6.1.6.0 Story Id

#### 6.1.6.1 Story Id

US-043

#### 6.1.6.2 Dependency Reason

User must be able to mark a book as 'Read', which is the data source for book-based goals.

## 6.2.0.0 Technical Dependencies

- State management library (Riverpod) for reactive UI updates.
- Local database (Isar) for querying session and library data.
- A defined and consistent logic for calculating the start/end of day, week, and month periods.

## 6.3.0.0 Data Dependencies

- Access to the user's list of active goals.
- Access to the user's reading session history.
- Access to the user's library to check the status of books.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Goal progress calculation and UI update must complete in under 100ms after a relevant user action (e.g., logging a session) to feel instantaneous.

## 7.2.0.0 Security

- N/A for this specific UI component story.

## 7.3.0.0 Usability

- The visual indicator must be intuitive and require no explanation for the user to understand their progress.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The visual component must render correctly on all supported iOS and Android screen sizes and versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The data aggregation logic to calculate progress for different time periods (especially 'this week' and 'this month') can be complex due to date/time and timezone considerations.
- Ensuring the component is reusable and configurable for all goal types.
- Managing state updates efficiently so that all instances of the progress indicator refresh correctly after a single data change.

## 8.3.0.0 Technical Risks

- Potential for off-by-one errors or incorrect date range calculations in the progress aggregation logic.
- Performance issues if the local database query to aggregate session data is not optimized, especially for users with extensive history.

## 8.4.0.0 Integration Points

- Dashboard screen (REQ-DSH-001)
- Dedicated Goal Management screen (REQ-GOL-001)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify calculation for each goal type (books, pages, time) and period (day, week, month, year).
- Test boundary conditions for time periods (e.g., logging a session on the last day of the week).
- Test UI rendering for 0%, 50%, 100%, and >100% progress.
- Test real-time UI updates after logging a session, editing a session, and editing a goal.
- Verify accessibility labels and color contrast.

## 9.3.0.0 Test Data Needs

- User accounts with various active goals.
- User accounts with a history of reading sessions spanning different days, weeks, and months.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for progress calculation logic implemented with >80% coverage, including date boundary cases
- Flutter widget tests for the visual indicator component are implemented and passing
- Integration testing completed to verify UI updates after data changes
- User interface reviewed and approved for visual consistency and clarity
- Performance requirements verified on mid-range devices
- Accessibility requirements validated using automated tools and manual checks (e.g., TalkBack/VoiceOver)
- Documentation for the reusable widget is created
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core motivational feature and should be prioritized soon after the foundational goal-setting and tracking stories are complete.
- Requires clear definition of how 'week' and 'month' periods are calculated (e.g., week starts on Sunday vs. Monday).

## 11.4.0.0 Release Impact

- Significantly enhances the user experience and the perceived value of the goal-setting feature.

