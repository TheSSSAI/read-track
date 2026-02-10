# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-032 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User disables daily task reminder push notificatio... |
| As A User Story | As a registered user, I want to disable push notif... |
| User Persona | Any registered user (Free or Premium) who wishes t... |
| Business Value | Increases user satisfaction and retention by givin... |
| Functional Area | User Management & Settings |
| Story Theme | User Preferences & Notifications |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User successfully disables daily task notifications with an active internet connection

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user on the 'Settings' screen, and the 'Daily Task Reminders' toggle is currently in the 'On' state

### 3.1.5 When

I tap the 'Daily Task Reminders' toggle switch

### 3.1.6 Then

The toggle switch immediately updates its visual state to 'Off'

### 3.1.7 And

I will no longer receive push notifications for daily tasks

### 3.1.8 Validation Notes

Verify via manual testing that a test push notification is not received. Verify in the backend database that the user's preference flag is set to false.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

The disabled setting for daily task notifications is persisted across sessions

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I have previously disabled 'Daily Task Reminders' notifications

### 3.2.5 When

I close and reopen the application and navigate back to the 'Settings' screen

### 3.2.6 Then

The 'Daily Task Reminders' toggle is displayed in the 'Off' state, reflecting my saved preference

### 3.2.7 Validation Notes

Can be tested by restarting the app or logging out and logging back in.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User disables daily task notifications while offline

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am a logged-in user on the 'Settings' screen, my device is offline, and the 'Daily Task Reminders' toggle is 'On'

### 3.3.5 When

I tap the 'Daily Task Reminders' toggle switch

### 3.3.6 Then

The toggle switch immediately updates its visual state to 'Off'

### 3.3.7 And

When my device's network connectivity is restored, the setting is automatically synchronized with the server in the background

### 3.3.8 Validation Notes

Test by enabling airplane mode, changing the setting, then disabling airplane mode. Verify the change is reflected on the server after a short delay.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

System handles failure to save the notification preference to the backend

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a logged-in user on the 'Settings' screen with an active internet connection

### 3.4.5 When

I tap the 'Daily Task Reminders' toggle to 'Off', but the backend API returns an error

### 3.4.6 Then

The toggle switch reverts to its original 'On' state

### 3.4.7 And

A non-intrusive error message, such as a toast or snackbar, is displayed to me (e.g., 'Failed to update settings. Please try again.')

### 3.4.8 Validation Notes

This can be tested by mocking a 5xx server response from the user preferences API endpoint.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Disabling daily task reminders does not affect other notification types

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I have enabled both 'Goal Reminders' and 'Daily Task Reminders'

### 3.5.5 When

I disable only the 'Daily Task Reminders'

### 3.5.6 Then

I no longer receive notifications for daily tasks

### 3.5.7 And

I continue to receive notifications for 'Goal Reminders' as expected

### 3.5.8 Validation Notes

Requires both notification types to be implemented. Test by triggering both types of notifications and verifying only the correct one is received.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Settings' or 'Profile' screen.
- A 'Notifications' section within the settings.
- A labeled row for 'Daily Task Reminders'.
- A standard platform-native toggle switch (e.g., Flutter's `SwitchListTile`).

## 4.2.0 User Interactions

- User must be able to tap the toggle or the entire row to change the setting.
- The toggle must provide immediate visual feedback upon interaction.

## 4.3.0 Display Requirements

- The current state (On/Off) of the notification setting must be clearly visible upon entering the screen.

## 4.4.0 Accessibility Needs

- The toggle switch must have a proper content description for screen readers (e.g., 'Daily Task Reminders, On').
- The touch target for the toggle control must meet WCAG 2.1 standards (minimum 44x44 CSS pixels).

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "A user's choice to disable a specific notification type must be respected for all future sends of that notification type until the user re-enables it.", 'enforcement_point': 'Backend push notification service (before calling Amazon SNS).', 'violation_handling': 'N/A. This is a system design rule.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to access their settings.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-031

#### 6.1.2.2 Dependency Reason

The functionality to enable notifications, including the UI toggle and backend logic, must exist before it can be disabled.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-075

#### 6.1.3.2 Dependency Reason

The system for creating daily tasks and triggering their corresponding notifications must be in place for this toggle to have a meaningful effect.

## 6.2.0.0 Technical Dependencies

- A defined user preferences/settings data model in the backend database.
- An authenticated API endpoint to update user preferences.
- Integration with Amazon SNS for sending push notifications.
- The client-side offline storage mechanism (Isar) and synchronization logic.

## 6.3.0.0 Data Dependencies

- Requires a user record in the database with a specific field to store this preference (e.g., `dailyTaskNotificationsEnabled: boolean`).

## 6.4.0.0 External Dependencies

- Amazon Simple Notification Service (SNS) for the actual delivery of push notifications.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update the user's preference should have a P95 latency of less than 500ms.
- The UI change on the client should feel instantaneous (<100ms).

## 7.2.0.0 Security

- The API endpoint for updating settings must be secured and require user authentication.
- The endpoint must authorize the request to ensure a user can only modify their own settings.

## 7.3.0.0 Usability

- The setting must be easy to find within a logically named 'Settings' or 'Notifications' menu.
- The label for the setting must be clear and unambiguous.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards, particularly for interactive elements and labels.

## 7.5.0.0 Compatibility

- The feature must function identically on all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Standard UI component (toggle switch).
- Simple backend CRUD operation.
- Requires coordination between the user preferences service and the notification-sending service to ensure the flag is checked.

## 8.3.0.0 Technical Risks

- Potential for a race condition if the user changes the setting while a notification job is already running. The check must be performed as close to the send action as possible.

## 8.4.0.0 Integration Points

- Client App -> Backend API (to update preference)
- Backend Notification Service -> Backend User Database (to check preference)
- Backend Notification Service -> Amazon SNS (to send/not send notification)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify a user can disable notifications.
- Verify the setting persists after app restart.
- Verify the setting syncs correctly after being changed offline.
- Verify that disabling this setting does not affect other notification types.
- Verify that a test notification is NOT received after disabling.
- Verify UI gracefully handles API errors during the save operation.

## 9.3.0.0 Test Data Needs

- A test user account with push notifications enabled.
- A mechanism to trigger a test 'Daily Task Reminder' push notification on demand for a specific user.

## 9.4.0.0 Testing Tools

- Flutter's `flutter_test` and `integration_test` packages.
- Jest for backend unit/integration tests.
- A tool like Postman or an automated script to trigger test notifications via a debug endpoint.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing for both frontend and backend logic, with >80% coverage
- Integration testing completed successfully, verifying the notification is correctly suppressed
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified
- Security requirements validated
- Documentation updated appropriately (e.g., API documentation for the preferences endpoint)
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core user experience feature and should be prioritized highly to prevent user frustration.
- Requires both frontend and backend development effort, which should be coordinated.

## 11.4.0.0 Release Impact

Improves user satisfaction and is considered a baseline feature for a modern mobile application. Its absence could be noted in user reviews.

