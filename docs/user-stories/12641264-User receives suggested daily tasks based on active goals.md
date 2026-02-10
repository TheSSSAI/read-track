# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-077 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User receives suggested daily tasks based on activ... |
| As A User Story | As a user with active reading goals, I want the ap... |
| User Persona | Any user (Free or Premium) who has set at least on... |
| Business Value | Increases user engagement and retention by making ... |
| Functional Area | Daily Tasks & Goal Management |
| Story Theme | Intelligent User Guidance |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Suggestion for a yearly book goal

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has an active goal to read 24 books per year and no other active goals

### 3.1.5 When

the user navigates to the 'Daily Tasks' screen on a new day

### 3.1.6 Then

the system displays a suggested task card, visually distinct from user-created tasks, with a title like 'Read for 20 minutes to stay on track with your yearly goal'

### 3.1.7 Validation Notes

Verify the suggestion appears. The logic for converting a yearly goal to a daily suggestion must be implemented as per the defined business rules.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Suggestion for a weekly page goal

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a user has an active goal to read 70 pages per week

### 3.2.5 When

the user navigates to the 'Daily Tasks' screen

### 3.2.6 Then

the system calculates the remaining daily average and suggests a task like 'Read 10 pages to stay on track with your weekly goal'

### 3.2.7 Validation Notes

Verify the calculation is correct based on the day of the week and progress so far.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User accepts a suggested task

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the system is displaying a suggested task

### 3.3.5 When

the user taps the 'Add' or 'Accept' button on the suggestion

### 3.3.6 Then

the suggestion card is removed, and a new, standard daily task is added to the user's list for the current day with the suggested action

### 3.3.7 Validation Notes

Confirm the new task appears in the list and is functionally identical to a manually created task.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User dismisses a suggested task

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

the system is displaying a suggested task

### 3.4.5 When

the user taps the 'Dismiss' or 'X' button on the suggestion

### 3.4.6 Then

the suggestion card is removed for the remainder of the day and does not reappear on subsequent visits to the screen on the same day

### 3.4.7 Validation Notes

Verify the suggestion disappears and does not return after leaving and re-entering the screen.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

No suggestion when user has no active goals

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user has no active goals

### 3.5.5 When

the user navigates to the 'Daily Tasks' screen

### 3.5.6 Then

the system does not display any goal-based suggested tasks

### 3.5.7 Validation Notes

The task suggestion area should be empty or not visible.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

No suggestion when user is ahead of schedule

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

a user has a weekly goal of reading 140 minutes

### 3.6.5 And

it is Thursday, and the user has already logged 150 minutes of reading for the week

### 3.6.6 When

the user navigates to the 'Daily Tasks' screen

### 3.6.7 Then

the system does not display a goal-based suggested task

### 3.6.8 Validation Notes

Verify the progress calculation correctly identifies that the user has met their goal for the period.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Suggestion is adjusted based on partial progress

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

a user has a daily goal to read 30 pages

### 3.7.5 And

the user has already logged a session of 10 pages today

### 3.7.6 When

the user navigates to the 'Daily Tasks' screen

### 3.7.7 Then

the system suggests a task like 'Read 20 more pages to meet your daily goal'

### 3.7.8 Validation Notes

Ensure the suggestion logic accounts for progress made within the current goal period.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Goal prioritization for suggestions

### 3.8.3 Scenario Type

Edge_Case

### 3.8.4 Given

a user has two active goals: 'Read 20 pages per day' and 'Read 30 books per year'

### 3.8.5 When

the system generates a suggestion

### 3.8.6 Then

the suggestion is based on the more specific 'Read 20 pages per day' goal

### 3.8.7 Validation Notes

Verify that the defined goal hierarchy (Daily > Weekly > Monthly > Yearly) is correctly implemented.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A visually distinct 'Suggestion' card or banner on the Daily Tasks screen.
- An 'Add' or 'Accept' button within the suggestion card.
- A 'Dismiss' or 'Close' (X) icon within the suggestion card.

## 4.2.0 User Interactions

- Tapping 'Add' converts the suggestion into a standard task item in the list.
- Tapping 'Dismiss' removes the suggestion card for the current day.

## 4.3.0 Display Requirements

- The suggestion text must clearly state the recommended action and link it to a specific user goal (e.g., '...to meet your weekly goal').

## 4.4.0 Accessibility Needs

- The suggestion card and its action buttons must be fully accessible via screen readers.
- Sufficient color contrast must be used to distinguish the suggestion from the background and other UI elements.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A goal-based task suggestion is only generated if the user has at least one active goal.

### 5.1.3 Enforcement Point

Client-side logic before rendering the Daily Tasks screen.

### 5.1.4 Violation Handling

No suggestion is displayed.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

If multiple goals are active, the suggestion is based on the goal with the shortest time period (Daily > Weekly > Monthly > Yearly).

### 5.2.3 Enforcement Point

Client-side suggestion generation algorithm.

### 5.2.4 Violation Handling

N/A - Algorithmic rule.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

No suggestion is generated for a goal period if the user's progress has already met or exceeded the target.

### 5.3.3 Enforcement Point

Client-side logic, checking user progress against goal target.

### 5.3.4 Violation Handling

No suggestion is displayed.

## 5.4.0 Rule Id

### 5.4.1 Rule Id

BR-004

### 5.4.2 Rule Description

Once a suggestion is dismissed by the user, it will not be shown again for the same calendar day.

### 5.4.3 Enforcement Point

Client-side state management, persisting the dismissal state for the day.

### 5.4.4 Violation Handling

The suggestion UI element is not rendered.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

System needs the ability for users to set yearly book goals to generate suggestions from them.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-061

#### 6.1.2.2 Dependency Reason

System needs the ability for users to set periodic page goals.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-062

#### 6.1.3.2 Dependency Reason

System needs the ability for users to set periodic time goals.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-075

#### 6.1.4.2 Dependency Reason

System needs a daily task list where the accepted suggestion can be added.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-046

#### 6.1.5.2 Dependency Reason

System needs reading session data to track progress against goals and make intelligent suggestions.

## 6.2.0.0 Technical Dependencies

- Local database (Isar) schema for Goals, ReadingSessions, and DailyTasks must be finalized and implemented.
- State management (Riverpod) provider for daily tasks must be available to update.

## 6.3.0.0 Data Dependencies

- Access to the user's complete list of active goals.
- Access to the user's reading session history for the current goal periods.
- Access to the user's list of existing daily tasks for the current day.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The suggestion generation logic must execute in under 50ms on a mid-range device to avoid any noticeable delay when loading the Daily Tasks screen.

## 7.2.0.0 Security

*No items available*

## 7.3.0.0 Usability

- The suggestion should be clearly worded, actionable, and easy to understand.
- The process of accepting or dismissing a suggestion should be intuitive and require a single tap.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Designing a robust and flexible algorithm to convert various goal types (yearly books, weekly pages, etc.) into a single, sensible daily action.
- Handling the prioritization logic when multiple, conflicting goals are active.
- Managing the local state to ensure a dismissed suggestion does not reappear on the same day.
- The logic must accurately account for user progress within the current period (day, week, month).

## 8.3.0.0 Technical Risks

- The suggestion algorithm might produce non-intuitive or repetitive tasks if not designed carefully, leading to poor user experience.
- Poor performance of the client-side calculation could slow down screen load times.

## 8.4.0.0 Integration Points

- Reads data from the Goal, ReadingSession, and DailyTask repositories/services.
- Writes data to the DailyTask repository/service when a suggestion is accepted.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Test the suggestion algorithm with various goal types and progress levels (0%, 50%, 100%, 110%).
- Test the UI for accepting and dismissing a suggestion.
- Test the edge case where a user has multiple active goals.
- Test the edge case where a user has no active goals.
- End-to-end test: Create a goal, navigate to tasks, accept suggestion, verify task is created, complete task.

## 9.3.0.0 Test Data Needs

- User profiles with no goals.
- User profiles with single active goals of each type (books/year, pages/week, time/day).
- User profiles with multiple conflicting active goals.
- User profiles with varying levels of progress towards their goals.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for the suggestion algorithm implemented with >80% coverage
- Widget tests for the suggestion UI component implemented and passing
- Integration testing with local database completed successfully
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on a mid-range test device
- Security requirements validated
- Documentation updated appropriately
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- Requires clear product decisions on the suggestion logic (e.g., 'How many minutes/pages does a yearly book goal translate to daily?').
- Dependent on the completion of core goal and task management features.

## 11.4.0.0 Release Impact

Enhances the core loop of goal-setting and tracking, likely to improve user engagement metrics post-release.

