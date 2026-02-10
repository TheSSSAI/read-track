# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-023 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User sees banner ads on secondary screens |
| As A User Story | As a Free User, I want to see non-intrusive banner... |
| User Persona | Free User: An authenticated user who has not purch... |
| Business Value | Generates advertising revenue from the free user b... |
| Functional Area | Monetization & User Experience |
| Story Theme | Freemium Model Implementation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Free User sees a banner ad on a secondary screen with an internet connection

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is logged in as a 'Free User' and has an active internet connection

### 3.1.5 When

the user navigates to a designated secondary screen (e.g., the 'Statistics' page)

### 3.1.6 Then

a banner ad unit is displayed at the bottom of the screen, and the ad content is successfully loaded and visible, and the rest of the screen's content remains fully usable and is not obscured by the ad.

### 3.1.7 Validation Notes

Verify using a test ad unit ID from Google AdMob. The ad should appear consistently on the specified screens.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Premium User does not see any banner ads

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

the user is logged in as a 'Premium User'

### 3.2.5 When

the user navigates to any screen in the application, including secondary screens like the 'Statistics' page

### 3.2.6 Then

no banner ad unit is displayed, and no space is reserved for an ad.

### 3.2.7 Validation Notes

Log in with a premium-enabled test account and navigate through all major screens to confirm the complete absence of ads.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Ad fails to load due to a network error

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

the user is logged in as a 'Free User' and has an active internet connection

### 3.3.5 When

the user navigates to a secondary screen and the ad network fails to provide an ad

### 3.3.6 Then

the space reserved for the banner ad collapses, no empty placeholder is visible, and the application does not crash or display a user-facing error message.

### 3.3.7 Validation Notes

This can be simulated by using an invalid Ad Unit ID or by using network throttling tools to cause a timeout.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User is offline

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

the user is logged in as a 'Free User' and the device is offline

### 3.4.5 When

the user navigates to a secondary screen

### 3.4.6 Then

the ad unit is not displayed, its space is collapsed, and the application functions correctly without attempting to load an ad.

### 3.4.7 Validation Notes

Enable airplane mode on the test device and navigate to the relevant screens.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Ad is not displayed on primary screens

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the user is logged in as a 'Free User'

### 3.5.5 When

the user views the main Dashboard screen or is in the process of logging a reading session

### 3.5.6 Then

no banner ad is displayed, ensuring the core user experience is not disrupted.

### 3.5.7 Validation Notes

Navigate to the Dashboard and initiate the 'log session' flow to confirm no ads are present.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User interacts with an ad

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

a banner ad is successfully displayed for a 'Free User'

### 3.6.5 When

the user taps on the ad

### 3.6.6 Then

the ad's destination is opened in an appropriate view (e.g., in-app browser or external browser), as handled by the AdMob SDK.

### 3.6.7 Validation Notes

Requires manual testing on a device to confirm the click-through behavior of a test ad.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A container view for the banner ad.

## 4.2.0 User Interactions

- Tapping the ad redirects the user to the ad's content.

## 4.3.0 Display Requirements

- The banner ad must be positioned at the bottom of the screen.
- The ad must use an adaptive banner size to fit various screen widths.
- The ad container must collapse when an ad fails to load or when the user is offline, preventing empty white space.

## 4.4.0 Accessibility Needs

- The ad should not interfere with screen reader navigation of the main app content.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-ADS-001

### 5.1.2 Rule Description

Banner ads are only displayed to users with the 'Free User' role.

### 5.1.3 Enforcement Point

Client-side, before requesting an ad from the ad network.

### 5.1.4 Violation Handling

If a user is not 'Free User', no ad request is made.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-ADS-002

### 5.2.2 Rule Description

Banner ads are only displayed on designated secondary screens as defined in REQ-ADS-001.

### 5.2.3 Enforcement Point

Client-side, within the screen layout logic for specific pages.

### 5.2.4 Violation Handling

Ad components are not included in the widget tree for primary screens.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-004

#### 6.1.1.2 Dependency Reason

User must be able to log in to establish an authenticated session.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-016

#### 6.1.2.2 Dependency Reason

The system must be able to determine the user's subscription tier (Free/Premium) to decide whether to show ads.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-025

#### 6.1.3.2 Dependency Reason

The system must handle user consent for personalized advertising to comply with platform policies (e.g., ATT on iOS) before displaying any ads.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-068

#### 6.1.4.2 Dependency Reason

A secondary screen, such as the 'Statistics' page, must exist to serve as a placement for the banner ad.

## 6.2.0.0 Technical Dependencies

- Google AdMob SDK for Flutter (`google_mobile_ads` package).
- Platform-specific frameworks for advertising consent (e.g., App Tracking Transparency on iOS).

## 6.3.0.0 Data Dependencies

- User's subscription status must be available on the client device.

## 6.4.0.0 External Dependencies

- A configured Google AdMob account with valid App ID and Ad Unit IDs for both iOS and Android.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Ad loading must be an asynchronous process that does not block the UI thread or delay the rendering of screen content.
- The ad component should have a minimal impact on app memory usage.

## 7.2.0.0 Security

- AdMob App ID and Ad Unit IDs should be managed as environment variables and not hardcoded as plain text in the source code.

## 7.3.0.0 Usability

- Ads must not cover or obscure application functionality or content.
- Ad placement must be consistent and predictable across all designated secondary screens.

## 7.4.0.0 Accessibility

- N/A for the ad content itself, but the ad container must not trap screen reader focus.

## 7.5.0.0 Compatibility

- Must function correctly on all supported iOS (14.0+) and Android (7.0+) versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires platform-specific native code for handling advertising consent (App Tracking Transparency on iOS).
- Integration with a third-party SDK (Google AdMob) introduces external dependencies.
- Requires careful state management to show/hide the ad based on user tier, connectivity, and ad load status.
- UI layout must be robust enough to adapt to the presence or absence of the ad banner without breaking.

## 8.3.0.0 Technical Risks

- Potential for app rejection by Apple if the App Tracking Transparency prompt is not implemented according to their guidelines.
- Low ad fill rate from the ad network could result in no ads being shown, impacting revenue.
- The AdMob SDK could introduce performance issues or bugs into the application.

## 8.4.0.0 Integration Points

- Google AdMob SDK
- Backend service that provides user subscription status.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify ad displays for Free User on Statistics page.
- Verify NO ad displays for Premium User on Statistics page.
- Verify ad container collapses on ad load failure.
- Verify NO ad displays when offline.
- Verify NO ad displays on the main Dashboard.

## 9.3.0.0 Test Data Needs

- A test account with 'Free User' status.
- A test account with 'Premium User' status.
- Test Ad Unit IDs provided by Google AdMob for development.

## 9.4.0.0 Testing Tools

- `flutter_test` for unit and widget tests.
- `integration_test` for end-to-end tests on emulators/real devices.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing using test ads
- Code reviewed and approved by team
- Unit and widget tests implemented for ad display logic and passing with >80% coverage
- E2E tests successfully verify ad presence for Free users and absence for Premium users
- User interface reviewed on various screen sizes to ensure proper layout
- Performance impact on screen load times is measured and is negligible
- Advertising consent flow (ATT) is implemented and verified on an iOS device
- Documentation updated with AdMob setup instructions and environment variables
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Requires setup of a Google AdMob account and ad units before development can be completed.
- Requires access to physical iOS and Android devices for testing ad display and consent prompts.
- The dependency on the user consent story (US-025) may affect scheduling.

## 11.4.0.0 Release Impact

This is a critical feature for the freemium monetization strategy. The app cannot be launched without this or an alternative revenue stream for free users.

