# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-048 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User logs reading time with a timer |
| As A User Story | As an active reader, I want to use a start/stop ti... |
| User Persona | Any authenticated user (Free or Premium) who is ac... |
| Business Value | Improves the accuracy of user-generated data (read... |
| Functional Area | Reading Tracking |
| Story Theme | Library and Progress Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User starts, stops, and saves a timed session

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is on the 'Log Reading Session' screen for a book on their 'Currently Reading' shelf

### 3.1.5 When

the user taps the 'Start Timer' button

### 3.1.6 Then

a timer display appears and begins counting up from 00:00:00, and the button's state changes to 'Stop Timer'.

### 3.1.7 Validation Notes

Verify the timer increments every second and the button icon/label changes.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Happy Path: Stopping the timer populates the duration field

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the reading session timer is running

### 3.2.5 When

the user taps the 'Stop Timer' button

### 3.2.6 Then

the timer stops counting, and the total elapsed time is automatically populated into the 'Time Spent' input field.

### 3.2.7 Validation Notes

The format should be consistent with manual entry (e.g., '25 minutes'). The button should revert to its 'Start Timer' state.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Timer continues to run when the app is in the background

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user has started the reading session timer

### 3.3.5 When

the user backgrounds the application or locks the device for 2 minutes

### 3.3.6 Then

upon returning to the 'Log Reading Session' screen, the timer display shows an elapsed time of at least 2 minutes.

### 3.3.7 Validation Notes

This must be tested on physical devices for both iOS and Android. The implementation should rely on storing a start timestamp rather than a simple UI ticker.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User can manually override the timed duration

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

the user has stopped the timer and the 'Time Spent' field is populated

### 3.4.5 When

the user manually edits the value in the 'Time Spent' field

### 3.4.6 Then

the field accepts the new value, overwriting the value from the timer.

### 3.4.7 Validation Notes

Saving the session should persist the manually entered value, not the original timer value.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Timer state is reset if the user navigates away from the screen

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the user has started the reading session timer

### 3.5.5 When

the user navigates back to the previous screen without stopping the timer

### 3.5.6 Then

the timer is discarded, and no session is logged.

### 3.5.7 Validation Notes

A confirmation dialog could be a future enhancement, but for now, navigating away cancels the timer.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Timer state is lost if the OS terminates the app

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

the user has started the reading session timer

### 3.6.5 When

the application process is terminated by the operating system

### 3.6.6 Then

upon re-launching the app and returning to the 'Log Reading Session' screen, the timer is in its initial, stopped state.

### 3.6.7 Validation Notes

This is the accepted behavior for this edge case. The user will have to log their session manually.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A 'Start Timer' button, likely with a 'play' icon.
- A 'Stop Timer' button, likely with a 'stop' icon, which replaces the start button when the timer is active.
- A text display for the running timer, formatted as HH:MM:SS.

## 4.2.0 User Interactions

- Tapping 'Start Timer' begins the countdown and changes the button to 'Stop Timer'.
- Tapping 'Stop Timer' halts the countdown, populates the duration field, and reverts the button to 'Start Timer'.

## 4.3.0 Display Requirements

- The timer must be clearly visible and update every second while active.
- The user must still be able to interact with other fields on the screen (e.g., 'Page Number') while the timer is running.

## 4.4.0 Accessibility Needs

- The start/stop buttons must have accessible labels that announce their current state (e.g., 'Start reading timer', 'Stop reading timer').

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A timed session can only be started for a book on the 'Currently Reading' shelf.", 'enforcement_point': "The 'Log Reading Session' screen is only accessible for items on this shelf.", 'violation_handling': 'N/A - UI flow prevents this.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-042

#### 6.1.1.2 Dependency Reason

A book must be on the 'Currently Reading' shelf to log a session for it.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-049

#### 6.1.2.2 Dependency Reason

This story adds the timer functionality to the existing manual session logging screen. That screen and its manual time entry field must exist first.

## 6.2.0.0 Technical Dependencies

- Local database (Isar) for persisting the timer's start timestamp to handle app backgrounding.
- State management (Riverpod) to manage the timer's state (running, stopped) and update the UI reactively.

## 6.3.0.0 Data Dependencies

- Requires a valid `LibraryItem` object to be passed to the 'Log Reading Session' screen.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The background timer mechanism must be resource-efficient to minimize battery consumption.

## 7.2.0.0 Security

*No items available*

## 7.3.0.0 Usability

- The start/stop interaction must be simple and intuitive, requiring a single tap for each action.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards, particularly for button labels and touch target size.

## 7.5.0.0 Compatibility

- The background timer behavior must be tested and function reliably on all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The primary complexity is ensuring the timer remains accurate when the app is backgrounded or the device is locked. This requires persisting a start timestamp locally rather than relying on an in-memory UI timer.
- Handling state restoration correctly if the user navigates back to the screen while a timer is conceptually 'running'.

## 8.3.0.0 Technical Risks

- Different OS versions (especially on Android) have varying restrictions on background processes, which could affect timer accuracy if not implemented robustly.
- A naive implementation could cause significant battery drain.

## 8.4.0.0 Integration Points

- Integrates with the existing 'Log Reading Session' view.
- Writes to the local Isar database to store the start time.
- Updates the `ReadingSession` data model upon saving.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Full happy path flow: start, wait, stop, save, verify data.
- Backgrounding the app for several minutes and returning to verify elapsed time.
- Locking the device and unlocking to verify elapsed time.
- Manually overriding the timer's value before saving.
- Navigating away from the screen to ensure the timer is cancelled.

## 9.3.0.0 Test Data Needs

- A test user account with a book on the 'Currently Reading' shelf.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end tests.
- Manual testing on physical iOS and Android devices is required to validate background behavior.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage for new logic
- Integration testing completed successfully
- User interface reviewed and approved for both light and dark modes
- Performance requirements verified (no noticeable battery drain)
- Security requirements validated
- Documentation updated appropriately
- Story deployed and verified in staging environment on both iOS and Android

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a key quality-of-life improvement for the core tracking feature.
- Requires dedicated time for manual testing of background states on physical devices, which should be factored into the sprint capacity.

## 11.4.0.0 Release Impact

- Enhances the core value proposition of the app by making reading tracking more accurate and convenient.

