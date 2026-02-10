# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-030 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User disables goal reminder push notifications |
| As A User Story | As a registered user, I want to disable push notif... |
| User Persona | Any registered user (Free or Premium) who has prev... |
| Business Value | Improves user satisfaction and retention by giving... |
| Functional Area | User Management & Settings |
| Story Theme | User Profile and Preferences |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successfully disabling goal reminders

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user on the 'Settings' screen

### 3.1.5 And

the system must not send any further push notifications related to my goals to any of my registered devices.

### 3.1.6 When

I tap the 'Goal Reminders' toggle

### 3.1.7 Then

the toggle's visual state must immediately change to 'disabled'

### 3.1.8 Validation Notes

Manual test: Tap the toggle and observe the UI change. Backend test: Verify the user's preference flag is updated in the database. E2E test: Trigger a condition that would normally send a goal reminder and confirm no notification is received.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Disabled setting persists across sessions

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I have previously disabled the 'Goal Reminders' push notification toggle

### 3.2.5 And

I have closed and reopened the application

### 3.2.6 When

I navigate back to the 'Settings' screen

### 3.2.7 Then

the 'Goal Reminders' toggle must be displayed in the 'disabled' state.

### 3.2.8 Validation Notes

Manual test: Disable the toggle, force-quit the app, relaunch, and navigate to settings to check the toggle's state.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Disabling notifications while offline

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am a logged-in user on the 'Settings' screen and my device is offline

### 3.3.5 And

when my device's network connectivity is restored, the setting change must be automatically synchronized with the backend without further user interaction.

### 3.3.6 When

I tap the 'Goal Reminders' toggle to disable it

### 3.3.7 Then

the toggle's visual state must immediately change to 'disabled' in the UI

### 3.3.8 Validation Notes

Manual test: Enable airplane mode, change the setting, disable airplane mode. Backend test: Check logs or database to confirm the update was received after the device came back online.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Setting applies across multiple devices

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am logged into my account on two separate devices (Device A and Device B)

### 3.4.5 When

I disable 'Goal Reminders' on Device A

### 3.4.6 Then

the system must stop sending goal reminders to both Device A and Device B

### 3.4.7 And

the 'Goal Reminders' toggle on Device B must reflect the 'disabled' state upon the next data sync or app launch.

### 3.4.8 Validation Notes

Requires testing with two physical or emulated devices logged into the same account.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A toggle switch control, clearly labeled 'Goal Reminders'.

## 4.2.0 User Interactions

- Tapping the toggle switch changes its state between enabled and disabled.
- The control must provide immediate visual feedback upon interaction.

## 4.3.0 Display Requirements

- The toggle must be located within a 'Settings' or 'Notifications' section of the app.
- The current state of the setting (enabled/disabled) must be clearly visible.

## 4.4.0 Accessibility Needs

- The toggle switch must have a proper accessibility label (e.g., 'Goal Reminders, On/Off') for screen readers.
- The touch target for the toggle and its label must meet WCAG 2.1 standards for size.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A user's notification preference for a specific category is global to their account.", 'enforcement_point': 'Backend notification service (e.g., Lambda function triggering SNS).', 'violation_handling': "N/A. The service must check the user's preference before sending any notification."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

The UI element and backend logic to enable notifications must exist to provide the toggle that this story will disable. Both stories are part of the same feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-027

#### 6.1.2.2 Dependency Reason

A user settings/profile screen must exist as a location for the notification toggles.

## 6.2.0.0 Technical Dependencies

- An authenticated API endpoint to update user preferences.
- A user data model/table in the database with a field to store this preference.
- The backend push notification service (Amazon SNS) and the logic that triggers it.

## 6.3.0.0 Data Dependencies

- Requires access to the current user's profile and preference data.

## 6.4.0.0 External Dependencies

- Amazon Simple Notification Service (SNS) for push notification delivery.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI response to tapping the toggle must be under 100ms.
- The backend API call to update the preference should have a P95 latency of less than 200ms.

## 7.2.0.0 Security

- The API endpoint for updating user preferences must be authenticated and authorized, ensuring a user can only modify their own settings.
- The user's device token for push notifications must be handled securely.

## 7.3.0.0 Usability

- The setting must be easy to find within the app's settings menu.
- The purpose of the toggle must be clear from its label.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards for interactive elements.

## 7.5.0.0 Compatibility

- The functionality must work consistently on all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Standard UI component (toggle switch).
- Simple backend CRUD operation (updating a boolean flag).
- Requires modification of an existing service (notification sender) to check the flag.

## 8.3.0.0 Technical Risks

- Potential for race conditions if the user rapidly toggles the setting. The client should debounce requests.
- Ensuring the offline sync logic is robust and correctly handles queued updates.

## 8.4.0.0 Integration Points

- Client State Management (Riverpod) -> Backend User Preferences API -> Aurora Database
- Backend Notification Service -> Aurora Database (to check preference) -> Amazon SNS

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a user can disable notifications.
- Verify the setting persists after app restart.
- Verify the setting syncs correctly after being changed offline.
- Verify that after disabling, a goal reminder event does NOT trigger a push notification.
- Verify the toggle is correctly labeled for screen readers.

## 9.3.0.0 Test Data Needs

- A test user account with at least one active goal.
- A test user account with push notifications enabled at the OS level.

## 9.4.0.0 Testing Tools

- Flutter Test (`flutter_test`, `integration_test`) for frontend testing.
- Jest for backend unit/integration tests.
- A mock or instrumented push notification service to verify that notifications are not sent.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >= 80% coverage
- Integration testing between client, API, and notification service completed successfully
- User interface reviewed and approved for both iOS and Android
- Performance requirements verified
- Security requirements validated
- Documentation for the user preferences API endpoint is updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

1

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story should be developed in conjunction with US-029 (Enable goal reminders) as they are part of the same feature set and share components.
- Requires coordination between frontend and backend developers.

## 11.4.0.0 Release Impact

This is a core user experience feature required for the initial release to provide essential notification controls.

