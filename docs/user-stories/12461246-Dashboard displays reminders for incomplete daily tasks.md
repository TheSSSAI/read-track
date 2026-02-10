# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-059 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Dashboard displays reminders for incomplete daily ... |
| As A User Story | As a user who has set daily reading tasks, I want ... |
| User Persona | Any user (Free or Premium) who has created one or ... |
| Business Value | Increases daily user engagement and habit formatio... |
| Functional Area | Dashboard |
| Story Theme | User Engagement and Habit Formation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Reminder appears when incomplete tasks exist for the current day

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has at least one daily task scheduled for the current day of the week, and that task is not marked as complete

### 3.1.5 When

the user navigates to the dashboard screen

### 3.1.6 Then

a reminder component is visible on the dashboard, listing the title of the incomplete task(s).

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Reminder disappears after the last incomplete task is completed

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the dashboard is displaying a reminder for the user's last incomplete task of the day

### 3.2.5 When

the user marks that task as complete and the dashboard view is refreshed

### 3.2.6 Then

the daily task reminder component is no longer visible on the dashboard.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Reminder does not appear if all tasks for the day are complete

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

a user has tasks scheduled for the current day, and all of them have been marked as complete

### 3.3.5 When

the user navigates to the dashboard screen

### 3.3.6 Then

the daily task reminder component is not displayed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Reminder does not appear if no tasks are scheduled for the current day

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a user has created daily tasks, but none are scheduled for the current day of the week

### 3.4.5 When

the user navigates to the dashboard screen

### 3.4.6 Then

the daily task reminder component is not displayed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Reminder does not appear if the user has no tasks at all

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user has not created any daily tasks

### 3.5.5 When

the user navigates to the dashboard screen

### 3.5.6 Then

the daily task reminder component is not displayed.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Reminder state updates correctly when the date changes (midnight rollover)

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

the user is viewing the dashboard with an incomplete task reminder for Monday

### 3.6.5 When

the device's local time passes midnight, changing the date to Tuesday

### 3.6.6 Then

the dashboard state refreshes, hiding the reminder for Monday's task and showing a reminder for Tuesday's tasks if any exist and are incomplete.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated card or section on the dashboard for 'Today's Tasks'.
- A list within the card displaying the title of each incomplete task.
- An interactive element (e.g., a checkbox) next to each task to allow for quick completion directly from the dashboard.

## 4.2.0 User Interactions

- The reminder component should appear automatically when conditions are met, without user action.
- Tapping the interactive element next to a task should mark it as complete.
- If all tasks are completed via the dashboard reminder, the entire component should animate out or disappear upon the next UI refresh.

## 4.3.0 Display Requirements

- The component should only display tasks scheduled for the current calendar day.
- The component should not be displayed if there are no incomplete tasks for the current day.

## 4.4.0 Accessibility Needs

- The reminder component and its text must be compatible with screen readers (e.g., VoiceOver, TalkBack).
- Interactive elements must have a minimum touch target size of 44x44dp.
- Text must adhere to WCAG 2.1 AA contrast ratios against the background.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The daily task reminder is driven by the user's local device date.

### 5.1.3 Enforcement Point

Client-side logic on the dashboard screen.

### 5.1.4 Violation Handling

N/A - This is a display logic rule.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The reminder component is only displayed if there is at least one task that is both scheduled for the current day and has a status of 'incomplete'.

### 5.2.3 Enforcement Point

Client-side logic that determines the visibility of the dashboard component.

### 5.2.4 Violation Handling

N/A - This is a display logic rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-075

#### 6.1.1.2 Dependency Reason

Users must be able to create tasks before they can be reminded about them.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-076

#### 6.1.2.2 Dependency Reason

The reminder logic depends on tasks having a recurring schedule to determine if they are for the 'current day'.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-078

#### 6.1.3.2 Dependency Reason

The system must be able to track the completion status of a task to know whether to display a reminder.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

REQ-DSH-001

#### 6.1.4.2 Dependency Reason

This story adds a component to the main dashboard, which must exist first.

## 6.2.0.0 Technical Dependencies

- Local database (Isar) for storing and querying task data offline.
- State management library (Riverpod) to manage the dashboard's state and react to task completion.

## 6.3.0.0 Data Dependencies

- Requires access to the user's list of created daily tasks, including their schedule and completion status.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The logic to check for incomplete tasks must execute in under 50ms to avoid impacting the dashboard's load time (as per REQ-PER-001).
- The check must be performed using data from the local database to ensure functionality and performance while offline.

## 7.2.0.0 Security

*No items available*

## 7.3.0.0 Usability

- The reminder should be noticeable but not intrusive, providing a gentle nudge rather than a disruptive alert.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Requires creating a new UI component for the dashboard.
- Involves client-side state management to show/hide the component based on task status.
- Date and time logic must correctly handle the device's local timezone and the midnight rollover.

## 8.3.0.0 Technical Risks

- Potential for complexity in state management if the dashboard is already very complex. Ensuring the UI reacts instantly to a task being marked complete is critical.
- Incorrectly handling date changes could lead to reminders being shown on the wrong day or not disappearing at midnight.

## 8.4.0.0 Integration Points

- Integrates with the local data layer for tasks (Isar DB).
- Integrates with the state management system for the dashboard screen.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration

## 9.2.0.0 Test Scenarios

- Verify reminder appears with one incomplete task for today.
- Verify reminder appears with multiple incomplete tasks for today.
- Verify reminder does not appear when tasks exist but are not for today.
- Verify reminder does not appear when today's tasks are all complete.
- Verify reminder disappears immediately after the last task is marked complete.
- Manually change device time past midnight and verify the reminder state updates correctly for the new day.

## 9.3.0.0 Test Data Needs

- A user account with no tasks.
- A user account with tasks, none of which are for the current day.
- A user account with one or more tasks for the current day, all incomplete.
- A user account with one or more tasks for the current day, all complete.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >80% coverage for new logic
- Integration testing completed successfully
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on mid-range devices
- Accessibility requirements validated with screen readers
- Documentation for the new dashboard component is created
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be scheduled in a sprint only after its prerequisite stories (US-075, US-076, US-078) are completed and merged.
- Requires collaboration with a UI/UX designer to finalize the component's appearance and placement.

## 11.4.0.0 Release Impact

- This is a key feature for improving daily engagement and should be included in the next minor or major release after implementation.

