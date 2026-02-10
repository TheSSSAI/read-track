# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-026 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User experiences an ad-free app |
| As A User Story | As a Premium User, I want the application to be co... |
| User Persona | Premium User: A user with an active, paid subscrip... |
| Business Value | This is a primary feature justifying the Premium s... |
| Functional Area | User Experience & Monetization |
| Story Theme | Freemium Subscription Model |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Premium User does not see banner ads

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user with an active Premium subscription is logged into the application

### 3.1.5 When

the user navigates to a secondary screen, such as the statistics page, where a banner ad would normally appear for a Free User

### 3.1.6 Then

no banner ad is initialized or displayed, and the UI layout adjusts gracefully without leaving an empty placeholder.

### 3.1.7 Validation Notes

Verify by logging in with a premium test account and navigating to all screens that show banner ads for free users. Check the widget tree to confirm the ad widget is not present.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Premium User does not see interstitial ads

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

a user with an active Premium subscription is logged into the application

### 3.2.5 When

the user completes and logs three separate reading sessions

### 3.2.6 Then

no full-screen interstitial ad is triggered or displayed.

### 3.2.7 Validation Notes

Verify by logging in with a premium test account and logging 3+ reading sessions. Confirm no interstitial ad is shown.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User upgrades from Free to Premium in-app

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

a Free User is actively using the app and is seeing advertisements

### 3.3.5 When

the user successfully completes the in-app purchase to upgrade to a Premium subscription

### 3.3.6 Then

the user's subscription status is immediately updated in the app's state, and all ad placements are removed for the remainder of the session and all future sessions.

### 3.3.7 Validation Notes

Using a sandbox test account, perform an upgrade. Before upgrading, confirm ads are visible. After the purchase confirmation, navigate to a screen with an ad and confirm it is gone.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Premium User's subscription expires

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a user's Premium subscription has expired, and their account has reverted to the 'Free User' class on the backend

### 3.4.5 When

the user opens the app and their session is refreshed, or they log out and log back in

### 3.4.6 Then

the app correctly identifies the user as a Free User and begins displaying ads according to the free tier rules.

### 3.4.7 Validation Notes

Use a test account and manually expire the subscription on the backend or via a test environment tool. Relaunch the app and verify that ads are now being displayed.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Premium User uses the app offline

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a Premium User has a locally cached valid subscription status from a previous online session

### 3.5.5 When

the user opens and uses the app while their device is offline

### 3.5.6 Then

the application remains ad-free based on the cached status.

### 3.5.7 Validation Notes

Log in with a premium account, then turn on airplane mode. Close and reopen the app, then navigate to ad-bearing screens and log sessions to confirm no ads are shown.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- This story primarily concerns the *absence* of UI elements (ad banners, interstitial overlays).

## 4.2.0 User Interactions

- The user's interaction with the app should be seamless and uninterrupted by ad-related logic.

## 4.3.0 Display Requirements

- Screen layouts must be designed to gracefully collapse or adapt when ad containers are not present, preventing awkward empty spaces.

## 4.4.0 Accessibility Needs

- Not applicable, as this story removes UI elements.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Only users with a currently active and valid 'Premium' subscription status shall have advertisements suppressed.", 'enforcement_point': 'Client-side ad initialization logic, controlled by an authoritative state derived from the backend.', 'violation_handling': "If status is not 'Premium', ad logic proceeds as defined for the 'Free User' tier."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must be able to process subscription purchases to create Premium users.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-020

#### 6.1.2.2 Dependency Reason

The system must handle subscription expiration to correctly revert users to the free tier.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-023

#### 6.1.3.2 Dependency Reason

The banner ad display logic for Free Users must exist before it can be suppressed.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-024

#### 6.1.4.2 Dependency Reason

The interstitial ad display logic for Free Users must exist before it can be suppressed.

## 6.2.0.0 Technical Dependencies

- Backend Authentication Service (must include subscription status in JWT claims)
- Client-side State Management (Riverpod, to manage and react to user subscription status)
- Google AdMob SDK (must be integrated in the client)

## 6.3.0.0 Data Dependencies

- The user record in the primary database must have a clear and accurate subscription status field.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The check for subscription status must not introduce any noticeable latency to screen transitions or app startup. This check should rely on locally cached session data (from JWT) rather than a network call for every decision.

## 7.2.0.0 Security

- The client must not be able to spoof its subscription status. The status must be authoritatively determined by the backend and signed within the user's JWT.

## 7.3.0.0 Usability

- The transition from an ad-supported experience to an ad-free one (upon upgrade) must be immediate and seamless.

## 7.4.0.0 Accessibility

*No items available*

## 7.5.0.0 Compatibility

- The ad suppression logic must function identically on all supported iOS and Android versions.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The core logic is a simple conditional check.
- Requires robust client-side state management to handle status changes reactively.
- Depends on the backend correctly embedding the user's role/tier in the JWT.

## 8.3.0.0 Technical Risks

- Risk of 'flash of ad' if the subscription status is not available synchronously when a screen loads. State must be managed carefully to prevent this.
- Incorrectly handling state transitions (upgrade, downgrade, login/logout) could lead to a premium user seeing ads or a free user not seeing them.

## 8.4.0.0 Integration Points

- User Authentication Flow: The client needs to parse the JWT to extract the user's subscription tier.
- Ad Display Logic: All calls to initialize or show ads via the AdMob SDK must be wrapped in a condition that checks the user's subscription status.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Log in as a Premium user and verify no ads on any screen.
- Log in as a Free user and verify ads are present.
- Perform an in-app purchase upgrade and verify ads disappear immediately.
- Simulate a subscription expiration and verify ads reappear after session refresh.
- Use the app as a Premium user in offline mode and verify it remains ad-free.

## 9.3.0.0 Test Data Needs

- Test accounts provisioned with 'Free User' status.
- Test accounts provisioned with 'Premium User' status.
- Ability to simulate subscription lifecycle events (purchase, expiration) in a sandbox environment.

## 9.4.0.0 Testing Tools

- flutter_test for unit/widget tests.
- integration_test for E2E tests.
- App Store Connect Sandbox / Google Play Billing test environments.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing on both iOS and Android
- Code reviewed and approved by team
- Unit and widget tests implemented for the conditional logic and state management, achieving >80% coverage
- E2E tests for both Premium and Free user ad experiences are implemented and passing
- User interface reviewed on various device sizes to confirm no layout issues
- Performance impact of the subscription check is verified to be negligible
- Security review confirms that subscription status cannot be spoofed on the client
- Documentation for the state management of subscription status is updated
- Story deployed and verified in the staging environment using test accounts

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is blocked by the implementation of the subscription system and the initial ad display logic for free users. It should be scheduled in a sprint after its dependencies are met.

## 11.4.0.0 Release Impact

- This is a critical feature for the launch of the premium offering. The app cannot be released with a paid tier until this story is complete.

