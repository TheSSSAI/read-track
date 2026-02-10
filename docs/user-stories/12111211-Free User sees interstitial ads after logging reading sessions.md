# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-024 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User sees interstitial ads after logging read... |
| As A User Story | As a Free User, I want to be shown a full-screen a... |
| User Persona | Free User: A user who has not purchased a Premium ... |
| Business Value | Generates advertising revenue from the free user b... |
| Functional Area | Monetization & Advertisements |
| Story Theme | Freemium Business Model |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Ad is displayed on the third logged session

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in 'Free User' and I have a session counter at 2

### 3.1.5 When

I successfully log a new reading session

### 3.1.6 Then

the session counter increments to 3, and a full-screen interstitial ad is displayed after the session logging confirmation UI disappears.

### 3.1.7 Validation Notes

Verify the ad is displayed. The session counter can be checked in local storage or via debug logs.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Ad is not displayed on sessions that are not a multiple of three

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a logged-in 'Free User' and I have a session counter at 0

### 3.2.5 When

I successfully log my first reading session, and then my second reading session

### 3.2.6 Then

no interstitial ad is displayed after either session.

### 3.2.7 Validation Notes

Log two consecutive sessions and confirm no ad is triggered.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Ad counter continues correctly after an ad is shown

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am a logged-in 'Free User' and I have just seen an ad for my 3rd session

### 3.3.5 When

I log my 4th and 5th sessions, and then my 6th session

### 3.3.6 Then

no ad is shown for the 4th and 5th sessions, but an interstitial ad is shown after the 6th session is logged.

### 3.3.7 Validation Notes

Requires logging six sessions in total and verifying ads appear only on the 3rd and 6th.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Premium Users never see interstitial ads

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am a logged-in 'Premium User'

### 3.4.5 When

I log any number of reading sessions (e.g., 1st, 2nd, 3rd, 6th)

### 3.4.6 Then

no interstitial ad is ever displayed.

### 3.4.7 Validation Notes

Log in with a premium account, log multiple sessions, and confirm no ads appear.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Graceful failure when ad network provides no ad

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a 'Free User' and I have just logged my 3rd reading session

### 3.5.5 When

the ad network (Google AdMob) fails to return an ad (no fill)

### 3.5.6 Then

the application does not display an ad, does not crash or hang, and the user flow continues seamlessly.

### 3.5.7 Validation Notes

This can be tested by using a test ad unit ID that is configured for no fill, or by simulating a network error for the ad request.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Session counter persists across app restarts

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a 'Free User' and I have logged 2 reading sessions

### 3.6.5 When

I close and restart the application, and then log one more reading session

### 3.6.6 Then

an interstitial ad is displayed, indicating the counter correctly persisted at 2.

### 3.6.7 Validation Notes

Log 2 sessions, force quit the app, reopen, log 1 more session, and verify the ad appears.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Ad is not triggered for sessions logged while offline

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a 'Free User' with a session counter of 2, and my device is offline

### 3.7.5 When

I log my 3rd reading session

### 3.7.6 Then

the session is saved locally, the local counter increments to 3, but no ad is displayed.

### 3.7.7 Validation Notes

Enable airplane mode, log a session that would normally trigger an ad, and confirm no ad is shown. The next ad should trigger on the 6th session that is logged while online.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

User can dismiss the ad and return to the app

### 3.8.3 Scenario Type

Happy_Path

### 3.8.4 Given

an interstitial ad is being displayed

### 3.8.5 When

I tap the close button on the ad

### 3.8.6 Then

the ad is dismissed and I am returned to the screen I was on before the ad was shown (e.g., the book details screen).

### 3.8.7 Validation Notes

Manually test the ad dismissal flow.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Full-screen (interstitial) ad view provided by the Google AdMob SDK.

## 4.2.0 User Interactions

- The ad appears modally, covering the entire application UI.
- User must be able to dismiss the ad via a standard close control (e.g., 'X' icon).

## 4.3.0 Display Requirements

- The ad should only be displayed *after* the user receives confirmation that their reading session has been successfully logged.

## 4.4.0 Accessibility Needs

- The app must correctly handle the shift in focus to the ad view and back to the app upon dismissal for screen reader users.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-ADS-001

### 5.1.2 Rule Description

An interstitial ad must be shown to Free Users after every 3rd completed reading session.

### 5.1.3 Enforcement Point

Client-side, immediately after a successful 'log reading session' action.

### 5.1.4 Violation Handling

If the rule fails (e.g., no ad fill), the system should fail silently and not disrupt the user experience.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-ADS-002

### 5.2.2 Rule Description

Premium Users must not be shown any in-app advertisements.

### 5.2.3 Enforcement Point

Client-side, before any ad request is made, the user's subscription status must be checked.

### 5.2.4 Violation Handling

N/A. This is a preventative check.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-046

#### 6.1.1.2 Dependency Reason

This story is triggered by the action of logging reading progress by page number. That feature must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-047

#### 6.1.2.2 Dependency Reason

This story is triggered by the action of logging reading progress by percentage. That feature must exist first.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-016

#### 6.1.3.2 Dependency Reason

The system must be able to determine the user's current subscription status (Free/Premium) to decide whether to show an ad.

## 6.2.0.0 Technical Dependencies

- Google AdMob SDK for Flutter must be integrated into the project.
- A local persistence mechanism (e.g., Isar or shared_preferences) is required to store the session counter.

## 6.3.0.0 Data Dependencies

- Requires access to the user's current subscription status from the app's state management.

## 6.4.0.0 External Dependencies

- Google AdMob service for serving ads.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The ad SDK should be initialized on app startup to avoid delays.
- Interstitial ads should be pre-loaded in the background before they are needed to ensure they display instantly when triggered.
- Displaying the ad should not cause a noticeable drop in application performance or responsiveness.

## 7.2.0.0 Security

- AdMob App ID and Ad Unit IDs must be stored securely and not hardcoded in easily reverse-engineered code.

## 7.3.0.0 Usability

- The ad frequency must not be so high as to be overly disruptive to the core user experience.
- The ad display must not interrupt the user in the middle of a task; it must occur after a task is completed.

## 7.4.0.0 Accessibility

- The app must remain stable and usable with accessibility services (like TalkBack/VoiceOver) enabled when an ad is displayed and dismissed.

## 7.5.0.0 Compatibility

- The ad implementation must be compatible with all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires integration with a third-party native SDK (AdMob).
- Involves managing the ad lifecycle (request, load, show, dismiss, error).
- Requires robust local state management for the session counter that persists across app sessions.
- Needs careful handling of edge cases like offline mode and no-ad-fill errors to avoid a poor user experience.

## 8.3.0.0 Technical Risks

- The AdMob SDK could introduce bugs or performance issues into the app.
- Poor ad network performance (high latency, low fill rate) could impact user experience and revenue.
- Platform-specific configuration (e.g., Info.plist for iOS, AndroidManifest.xml for Android) must be done correctly.

## 8.4.0.0 Integration Points

- The 'log reading session' confirmation flow.
- The app's global state management system (to check user subscription status).
- The local database (Isar) for persisting the session counter.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify ad display on 3rd, 6th, 9th sessions for a Free User.
- Verify no ad display for a Premium User.
- Verify no ad display when the ad network returns an error.
- Verify the session counter persists after the app is restarted.
- Verify the ad dismissal flow returns the user to the correct screen.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' roles.
- Google AdMob test ad unit IDs to avoid generating invalid traffic during testing.

## 9.4.0.0 Testing Tools

- flutter_test for unit/widget tests.
- integration_test package for E2E tests.
- A mocking library (like Mockito) to mock the AdMob SDK calls in integration tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for the session counter logic are implemented and passing with >80% coverage
- Integration testing of the ad trigger logic (with a mocked SDK) is completed successfully
- User interface for the ad display has been manually verified on representative iOS and Android devices
- Performance requirements for ad pre-loading have been verified
- Security requirements for key storage have been validated
- Documentation for the ad logic and configuration has been added to the team's knowledge base
- Story deployed and verified in the staging environment using test ad units

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a key monetization feature and should be prioritized after the core reading tracking and subscription features are in place.
- Requires setup of a Google AdMob account and configuration of ad units if not already done.

## 11.4.0.0 Release Impact

- Enables the primary revenue stream for the free tier of the application. Critical for the business model.

