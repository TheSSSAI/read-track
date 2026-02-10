# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-031 |
| Elaboration Date | 2025-01-17 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User enables daily task reminder push notification... |
| As A User Story | As a user who has set daily reading tasks, I want ... |
| User Persona | Any registered user (Free or Premium) who wants to... |
| Business Value | Increases user engagement and retention by helping... |
| Functional Area | User Management & Settings |
| Story Theme | Notifications and User Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User successfully enables notifications when OS permissions are already granted

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is logged in, has previously granted the app OS-level notification permissions, and is on the Settings screen where the 'Daily Task Reminders' toggle is 'off'

### 3.1.5 When

the user taps the 'Daily Task Reminders' toggle

### 3.1.6 Then

the toggle's visual state immediately changes to 'on', and the user's preference to receive these notifications is successfully saved to the backend.

### 3.1.7 Validation Notes

Verify the toggle state change in the UI. Check the user's profile data in the backend database to confirm the preference flag is set to true. The device token should be associated with the relevant SNS topic/endpoint.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User attempts to enable notifications when OS permissions have not been granted

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

the user is logged in, has NOT granted the app OS-level notification permissions, and is on the Settings screen

### 3.2.5 When

the user taps the 'Daily Task Reminders' toggle to turn it 'on'

### 3.2.6 Then

the app must prompt the user to grant notification permissions via the native OS dialog. If the user grants permission, the toggle state changes to 'on' and the preference is saved. If the user denies permission, the toggle remains 'off'.

### 3.2.7 Validation Notes

Test on both iOS and Android. Verify the OS permission prompt appears. Test both 'Allow' and 'Don't Allow' paths and confirm the toggle state and backend preference reflect the user's choice.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User attempts to enable notifications when OS permissions were previously denied

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is logged in, has previously DENIED the app OS-level notification permissions, and is on the Settings screen

### 3.3.5 When

the user taps the 'Daily Task Reminders' toggle to turn it 'on'

### 3.3.6 Then

the app displays a helpful message explaining that notifications are blocked in the device settings and provides a shortcut to open the app's settings page in the OS.

### 3.3.7 Validation Notes

Verify that the app does not re-trigger the native OS prompt. Confirm that a custom in-app dialog/message is shown with a button that deep-links to the OS settings for the app.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Network error occurs when saving the preference

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

the user is on the Settings screen and the 'Daily Task Reminders' toggle is 'off'

### 3.4.5 When

the user taps the toggle and the API call to the backend fails due to a network error

### 3.4.6 Then

the toggle reverts to its original 'off' state, and a non-intrusive error message (e.g., a snackbar) is displayed, informing the user that the setting could not be saved.

### 3.4.7 Validation Notes

Use a network proxy or airplane mode to simulate a network failure. Confirm the UI reverts correctly and the error message is displayed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

The enabled setting persists across sessions

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

a user has successfully enabled 'Daily Task Reminders'

### 3.5.5 When

the user closes and re-opens the application and navigates back to the Settings screen

### 3.5.6 Then

the 'Daily Task Reminders' toggle is displayed in the 'on' state, reflecting the saved preference.

### 3.5.7 Validation Notes

Enable the toggle, fully close the app (terminate the process), re-launch, and check the settings screen.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A standard toggle switch component within the app's main Settings screen.
- A clear, descriptive label next to the toggle, e.g., 'Daily Task Reminders'.
- A snackbar or toast component for displaying error messages.
- A custom dialog to explain why OS permissions are needed if they were previously denied.

## 4.2.0 User Interactions

- Tapping the toggle changes its state.
- The UI should provide immediate visual feedback upon tap.
- If needed, a button in the custom dialog should deep-link the user to the app's OS settings page.

## 4.3.0 Display Requirements

- The toggle's state must always reflect the user's last successfully saved preference.

## 4.4.0 Accessibility Needs

- The toggle and its label must be compatible with screen readers (e.g., VoiceOver, TalkBack).
- The touch target for the toggle must meet WCAG 2.1 standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user can only receive push notifications if they have both enabled the in-app setting and granted OS-level permissions.', 'enforcement_point': 'Client-side (for prompting) and Backend (before sending a notification).', 'violation_handling': "The backend should not attempt to send a notification to a device token if the user's preference is disabled, even if the token is active."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to access the settings screen.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-005

#### 6.1.2.2 Dependency Reason

User must be able to log in to access the settings screen.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-075

#### 6.1.3.2 Dependency Reason

While the toggle can be built independently, the end-to-end feature requires the ability to create daily tasks to trigger notifications.

## 6.2.0.0 Technical Dependencies

- A functional user settings/profile screen in the Flutter application.
- A backend API endpoint for updating user preferences.
- Client-side push notification library (e.g., firebase_messaging for Flutter) must be configured.
- Backend integration with Amazon Simple Notification Service (SNS) for managing device tokens and sending notifications (as per REQ-USR-001).

## 6.3.0.0 Data Dependencies

- Requires a field in the User data model to store the boolean preference for daily task notifications.

## 6.4.0.0 External Dependencies

- Apple Push Notification service (APNs)
- Firebase Cloud Messaging (FCM)

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to update the user's preference should have a P95 latency of less than 500ms.

## 7.2.0.0 Security

- The API endpoint for updating preferences must be authenticated and authorized, ensuring a user can only modify their own settings.
- Device tokens must be stored securely on the backend.

## 7.3.0.0 Usability

- The purpose of the toggle must be immediately clear to the user from its label.
- Error feedback must be clear and tell the user how to resolve the issue (e.g., 'Enable notifications in your device settings').

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards for mobile applications.

## 7.5.0.0 Compatibility

- The notification permission flow must be tested and function correctly on all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Handling platform-specific logic for requesting and checking notification permissions on iOS and Android.
- Implementing the deep-linking to OS settings.
- Requires both frontend and backend development, including integration with the external AWS SNS service.

## 8.3.0.0 Technical Risks

- Changes in future OS versions (iOS/Android) could break the permission handling logic.
- Incorrect configuration of push notification certificates (APNs) or server keys (FCM) can be difficult to debug.

## 8.4.0.0 Integration Points

- Flutter Client <-> Backend API (to update preference)
- Backend API <-> Amazon SNS (to register/update device for notifications)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Manual

## 9.2.0.0 Test Scenarios

- Enable toggle with permissions granted.
- Enable toggle without permissions, then grant them.
- Enable toggle without permissions, then deny them.
- Enable toggle with permissions previously denied.
- Verify persistence of the setting after app restart.
- Verify network failure handling.

## 9.3.0.0 Test Data Needs

- Test user accounts in various states (permissions granted, denied, not yet requested).

## 9.4.0.0 Testing Tools

- Flutter's `integration_test` package for automated UI testing.
- A tool like Postman or Insomnia to test the backend API endpoint directly.
- Physical devices for iOS and Android are required to test real push notification delivery.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing on both iOS and Android.
- Code reviewed and approved by at least one other developer.
- Unit and integration tests implemented with >80% coverage for new code.
- UI/UX for the settings toggle and permission prompts reviewed and approved.
- Manual E2E test confirms that enabling the toggle results in a user receiving a daily task notification on a staging build.
- Performance of the API endpoint is verified to be within limits.
- Security review of the preference update endpoint is complete.
- Documentation for the new API endpoint is created/updated.
- Story deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has a dependency on the existence of a settings screen. If this is the first story involving push notifications, initial setup for APNs/FCM and SNS will add significant overhead and should be accounted for in a separate spike or technical story.
- Should be developed in conjunction with US-032 (disable notifications) to reuse components and logic.

## 11.4.0.0 Release Impact

This is a key feature for user engagement and should be included in any major release that also includes the 'Daily Tasks' feature.

