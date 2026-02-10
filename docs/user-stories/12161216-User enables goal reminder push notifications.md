# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-029 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User enables goal reminder push notifications |
| As A User Story | As a goal-oriented user, I want to enable a toggle... |
| User Persona | Any authenticated user (Free or Premium) who has s... |
| Business Value | Increases user engagement and retention by making ... |
| Functional Area | User Management & Settings |
| Story Theme | Notifications and User Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Enable notifications for the first time and grant permission

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated user on the settings screen, and the 'Goal Reminders' toggle is 'off'

### 3.1.5 And

my preference to receive goal notifications is saved to the backend, associated with my device token.

### 3.1.6 When

I tap the 'Goal Reminders' toggle to enable it

### 3.1.7 Then

the native OS dialog requesting permission to send notifications is displayed

### 3.1.8 Validation Notes

Verify on both iOS and Android. Check backend logs or database to confirm the user's preference and device token are stored.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Enable notifications when permission has already been granted

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am an authenticated user on the settings screen, and the 'Goal Reminders' toggle is 'off'

### 3.2.5 And

my preference is saved to the backend.

### 3.2.6 When

I tap the 'Goal Reminders' toggle to enable it

### 3.2.7 Then

the toggle switches to the 'on' position immediately

### 3.2.8 Validation Notes

This can be tested by first enabling another notification type (e.g., Daily Tasks) and then enabling this one.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Enable notifications for the first time and deny permission

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am on the settings screen and the 'Goal Reminders' toggle is 'off'

### 3.3.5 When

I tap the 'Goal Reminders' toggle and the OS permission dialog is displayed

### 3.3.6 And

a non-intrusive message is displayed (e.g., snackbar) stating 'To receive reminders, please enable notifications in your device settings'.

### 3.3.7 Then

the 'Goal Reminders' toggle reverts to the 'off' position

### 3.3.8 Validation Notes

The message should be polite and guide the user on how to fix the issue if they change their mind.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to enable notifications when permission is permanently denied at the OS level

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I have previously denied notification permissions for the app in my device's system settings

### 3.4.5 And

a dialog is displayed that informs me that permissions are disabled and provides a shortcut button to open the app's system settings.

### 3.4.6 When

I tap the 'Goal Reminders' toggle to enable it

### 3.4.7 Then

the toggle remains in the 'off' position

### 3.4.8 Validation Notes

This requires manual testing by going into iOS/Android settings, disabling notifications for the app, and then trying to use the in-app toggle.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Toggle state persists across sessions

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I have successfully enabled the 'Goal Reminders' toggle

### 3.5.5 When

I close and reopen the application and navigate back to the settings screen

### 3.5.6 Then

the 'Goal Reminders' toggle is still in the 'on' position.

### 3.5.7 Validation Notes

Verify this also works after a full app restart (kill from memory).

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Enable notifications while offline

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I am on the settings screen and my device has no internet connectivity

### 3.6.5 When

I tap the 'Goal Reminders' toggle to the 'on' position

### 3.6.6 Then

the UI updates optimistically, showing the toggle as 'on'

### 3.6.7 And

once network connectivity is restored, the preference is automatically synced to the backend without further user interaction.

### 3.6.8 Validation Notes

Test using airplane mode. After reconnecting, verify the backend database reflects the change.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A toggle switch component, consistent with native iOS and Android design patterns.
- A clear label for the toggle: 'Goal Reminders'.
- A descriptive subtext below the label explaining what the reminders are for (e.g., 'Get notified about your reading goal progress').
- A system dialog to direct users to OS settings if permissions are permanently disabled.

## 4.2.0 User Interactions

- Tapping the toggle changes its state (on/off).
- The toggle should provide immediate visual feedback upon interaction.
- The app must handle the native OS permission dialog flow.

## 4.3.0 Display Requirements

- The toggle's state must accurately reflect the user's saved preference.

## 4.4.0 Accessibility Needs

- The toggle and its label must be compatible with screen readers (e.g., VoiceOver, TalkBack).
- The touch target for the toggle must meet WCAG 2.1 standards (minimum 44x44 CSS pixels).

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'A user must explicitly opt-in to receive any push notifications.', 'enforcement_point': 'Settings Screen', 'violation_handling': 'The system will not send any goal-related push notifications unless this toggle is enabled and OS permissions are granted.'}

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

US-060

#### 6.1.3.2 Dependency Reason

The notification system requires an active goal to exist in order to send a relevant reminder. This story enables the preference, but the feature is only useful if goals can be set.

## 6.2.0.0 Technical Dependencies

- Backend infrastructure for Amazon Simple Notification Service (SNS) must be provisioned (REQ-USR-001).
- A backend endpoint must exist to receive and store the user's device token and notification preference.
- The Flutter application must include a push notification handling library (e.g., firebase_messaging) configured for both iOS and Android.
- CI/CD pipeline must be configured with APNs (Apple) and FCM (Google) credentials.

## 6.3.0.0 Data Dependencies

- Requires access to the authenticated user's ID to associate the device token and preference.

## 6.4.0.0 External Dependencies

- Apple Push Notification service (APNs)
- Firebase Cloud Messaging (FCM)

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI response to tapping the toggle must be instantaneous (<100ms).
- Backend API call to update the preference should complete in under 300ms (P95).

## 7.2.0.0 Security

- Device tokens must be transmitted securely over HTTPS.
- The backend must treat device tokens as sensitive data and store them securely.
- The endpoint for updating preferences must be authenticated and authorized.

## 7.3.0.0 Usability

- The purpose of the toggle must be clear and unambiguous.
- The process for handling permissions (denied or disabled) must be user-friendly and provide clear guidance.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- Must function correctly on all supported OS versions: iOS 14.0+ and Android 7.0+ (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires platform-specific implementation for handling notification permissions on iOS and Android.
- Involves secure management of device tokens on both client and server.
- Requires configuration in external services (Apple Developer Portal, Firebase Console).
- Handling the 'permanently denied' state requires deep-linking into the OS settings, which can be complex.

## 8.3.0.0 Technical Risks

- Misconfiguration of push notification certificates or keys can block the feature entirely on one or both platforms.
- Changes in OS-level permission handling in future iOS/Android versions may require updates.

## 8.4.0.0 Integration Points

- Client App <-> Backend API (to save preference)
- Client App <-> OS (to request permissions)
- Backend API <-> Amazon SNS (to register device token)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify the full flow on a clean install: enable toggle -> see OS prompt -> grant -> verify state.
- Verify the flow for denying permission: enable toggle -> see OS prompt -> deny -> verify state and message.
- Verify the flow for permanently disabled permissions: disable in OS settings -> open app -> tap toggle -> verify dialog and shortcut.
- Verify offline functionality and successful sync upon reconnection.
- Verify on a range of supported iOS and Android devices and OS versions.

## 9.3.0.0 Test Data Needs

- Test accounts for both iOS and Android.
- Physical devices are required for reliable push notification testing.

## 9.4.0.0 Testing Tools

- Flutter's `integration_test` package for E2E tests.
- A tool like Postman to manually verify the backend endpoint if needed.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing on both iOS and Android.
- Code reviewed and approved by at least one other developer.
- Unit and integration tests implemented with >= 80% coverage for new code.
- E2E tests for permission flows are implemented and passing.
- User interface reviewed and approved by UX/UI designer.
- Backend endpoint for saving preference is deployed and verified.
- All security requirements for token handling are met.
- Documentation for the notification preference endpoint is updated.
- Story deployed and verified in the staging environment using real devices.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for all push notification features. It should be prioritized early in the development of the engagement features.
- Requires coordination between frontend and backend developers.
- Time must be allocated for setting up APNs/FCM credentials if not already done.

## 11.4.0.0 Release Impact

Enables a key user engagement and retention feature. Essential for the goal-setting and habit-forming value proposition of the app.

