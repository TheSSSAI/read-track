# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-025 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User provides or denies consent for personalized a... |
| As A User Story | As a user, I want to be presented with a clear cho... |
| User Persona | All Users (with a specific flow for New Users on f... |
| Business Value | Ensures legal compliance with privacy regulations ... |
| Functional Area | User Management & Compliance |
| Story Theme | Onboarding & User Privacy |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

New user on Android consents to personalized ads

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a new user opens the app for the first time on an Android device

### 3.1.5 When

the app's initial loading is complete

### 3.1.6 Then

a modal consent dialog for personalized advertising is displayed, blocking other UI interaction

### 3.1.7 And

the Google AdMob SDK is initialized to request personalized ads for the user's session.

### 3.1.8 Validation Notes

Verify via network proxy that ad requests do not contain the 'npa=1' flag. Verify the consent status is stored against the user profile in the database.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

New user on Android denies consent for personalized ads

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a new user opens the app for the first time on an Android device

### 3.2.5 When

the personalized advertising consent dialog is displayed

### 3.2.6 And

the Google AdMob SDK is initialized to request non-personalized ads.

### 3.2.7 Then

the dialog is dismissed

### 3.2.8 Validation Notes

Verify via network proxy that ad requests contain the 'npa=1' flag. Verify the consent status is stored against the user profile in the database.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

New user on iOS 14.5+ denies App Tracking Transparency (ATT) permission

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

a new user opens the app for the first time on an iOS device with version 14.5 or higher

### 3.3.5 When

the native iOS App Tracking Transparency (ATT) prompt is displayed

### 3.3.6 And

the in-app consent dialog for personalized ads is not shown, as the OS-level setting takes precedence.

### 3.3.7 Then

the user's consent status is automatically set to 'denied' within the application

### 3.3.8 Validation Notes

Verify that denying ATT prevents the in-app consent form from appearing. Verify via network proxy that ad requests contain the 'npa=1' flag.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

New user on iOS 14.5+ allows App Tracking Transparency (ATT) permission

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

a new user opens the app for the first time on an iOS device with version 14.5 or higher

### 3.4.5 When

the native iOS App Tracking Transparency (ATT) prompt is displayed

### 3.4.6 And

the user's choice on this dialog determines whether personalized or non-personalized ads are requested.

### 3.4.7 Then

the app proceeds to display the in-app modal consent dialog for personalized advertising

### 3.4.8 Validation Notes

Verify that allowing ATT correctly triggers the display of the in-app consent form. The subsequent flow should match AC-001 or AC-002.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User closes the app before making a consent choice

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a new user is presented with the consent dialog

### 3.5.5 When

the user closes and re-opens the application without having made a choice

### 3.5.6 Then

the consent dialog is displayed again until a choice is made.

### 3.5.7 Validation Notes

Simulate an app kill while the dialog is visible and verify it reappears on next launch.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Existing user changes their consent preference in settings

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

an existing user has previously made a consent choice

### 3.6.5 When

the user navigates to the 'Settings' -> 'Account' screen

### 3.6.6 And

all subsequent ad requests reflect the updated preference.

### 3.6.7 Then

the new choice is persisted to the backend

### 3.6.8 Validation Notes

After changing the setting, trigger a new ad request (e.g., by navigating to a screen with an ad) and verify the request parameters via network proxy.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Consent Management Platform (CMP) fails to load

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

the app is launched

### 3.7.5 When

the third-party Consent Management Platform SDK fails to initialize or load the form

### 3.7.6 Then

the system defaults to the most restrictive privacy setting ('denied')

### 3.7.7 And

the app does not crash and logs the error.

### 3.7.8 Validation Notes

Use network throttling or mock a failed API response from the CMP to test this fallback behavior.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Modal dialog for initial consent.
- Clearly labeled buttons for 'Accept' and 'Decline'.
- A link to the application's Privacy Policy within the dialog.
- A dedicated entry point in the 'Settings' screen, e.g., 'Privacy & Ad Settings'.
- A toggle or selection control on the settings page to modify the consent choice.

## 4.2.0 User Interactions

- The initial consent dialog must be modal, preventing interaction with the background app content until a choice is made.
- Tapping a choice dismisses the dialog and allows the user to proceed.
- The setting in the user profile should provide immediate visual feedback upon being changed.

## 4.3.0 Display Requirements

- The consent dialog text must clearly explain what data will be used and for what purpose, in simple, user-friendly language. All text must be approved by legal counsel.

## 4.4.0 Accessibility Needs

- The consent dialog and all its elements (text, links, buttons) must be fully accessible, supporting screen readers (VoiceOver/TalkBack) and dynamic type scaling per WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Consent must be obtained from the user before initializing any advertising SDKs or trackers that collect data for personalization.

### 5.1.3 Enforcement Point

Application startup sequence, before the main UI is displayed.

### 5.1.4 Violation Handling

Default to non-personalized mode. Log a critical error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

On iOS 14.5+, the native App Tracking Transparency (ATT) permission must be requested before the in-app consent form is shown.

### 5.2.3 Enforcement Point

Application startup sequence on applicable iOS versions.

### 5.2.4 Violation Handling

A denial at the ATT level automatically results in a 'denied' consent status for the app, overriding any other choice.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-002

#### 6.1.1.2 Dependency Reason

Requires user authentication to be in place to persist the consent choice against a user profile on the backend.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-003

#### 6.1.2.2 Dependency Reason

Requires user authentication to be in place to persist the consent choice against a user profile on the backend.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-023

#### 6.1.3.2 Dependency Reason

This story defines the consent mechanism that governs how ads (implemented in US-023 and US-024) are displayed. It must be completed before ads are shown to users.

## 6.2.0.0 Technical Dependencies

- Google AdMob SDK integration.
- A Consent Management Platform (CMP) SDK, such as Google's User Messaging Platform (UMP), is required to handle the consent flow and TCF v2 strings.
- Apple's AppTrackingTransparency framework for iOS.
- A backend endpoint to store and retrieve the user's consent status.

## 6.3.0.0 Data Dependencies

- User's profile/account to associate the consent status with.

## 6.4.0.0 External Dependencies

- Legal counsel must review and approve all user-facing text in the consent dialog to ensure compliance.
- Apple and Google platform policies regarding user consent and tracking.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The consent dialog must load and be presented to the user within 500ms of the app's readiness to display it, to avoid a perceived delay in startup.

## 7.2.0.0 Security

- The user's consent status must be stored securely on the backend and fetched over HTTPS.
- The client-side state should be considered non-authoritative; the backend's stored value is the source of truth.

## 7.3.0.0 Usability

- The language used must be simple and clear, avoiding legal jargon.
- The options to accept or decline must be equally prominent, with no 'dark patterns' to influence the user's choice.

## 7.4.0.0 Accessibility

- The consent flow must be fully compliant with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The implementation must correctly handle the different consent requirements for supported iOS (14.0+) and Android (7.0+) versions, with special logic for iOS 14.5+.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires platform-specific implementation paths for iOS (ATT) and Android.
- Integration with a third-party Consent Management Platform (CMP) SDK adds complexity and an external dependency.
- The timing of the consent prompt within the app's startup lifecycle is critical and must be carefully managed.
- Requires both frontend (UI, SDK integration) and backend (storage API) development.

## 8.3.0.0 Technical Risks

- Changes in platform policies (Apple/Google) may require future updates to the consent flow.
- Bugs or breaking changes in the third-party CMP SDK could impact the app's startup.
- Incorrect implementation could lead to legal non-compliance or app store rejection.

## 8.4.0.0 Integration Points

- Google AdMob SDK (for initialization).
- Google UMP SDK (or other CMP) for displaying the form.
- Apple AppTrackingTransparency framework.
- Backend User Profile service (for storing consent).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Fresh install on iOS 14.5+ with ATT allowed/denied.
- Fresh install on older iOS version.
- Fresh install on Android.
- Changing consent status in settings for an existing user.
- App launch with no network connection to test CMP failure.
- Verification of ad request parameters using a network proxy tool (e.g., Charles, Fiddler).

## 9.3.0.0 Test Data Needs

- Test accounts for new users.
- Test accounts for existing users with pre-set consent choices.
- Physical devices for both iOS and Android are required for reliable testing of native prompts.

## 9.4.0.0 Testing Tools

- Flutter integration_test package.
- Network proxy tool for inspecting ad requests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing on both iOS and Android
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage for the new logic
- E2E tests for the consent flow are passing
- User interface reviewed and approved by UX/UI designer
- Accessibility audit of the consent dialog passed
- All user-facing text has been approved by legal counsel
- Documentation for the consent flow and its configuration is created
- Story deployed and verified in the staging environment on physical devices

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a blocking story for any ad-related features and is a requirement for app store submission. It must be prioritized in an early sprint.
- Requires dedicated testing time on physical devices for both platforms.

## 11.4.0.0 Release Impact

This is a mandatory requirement for the initial v1.0 launch. The application cannot be released without this functionality.

