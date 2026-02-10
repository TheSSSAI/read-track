# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-070 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User views current and longest reading str... |
| As A User Story | As a Premium User, I want to see my current readin... |
| User Persona | Premium User: A subscribed user with full access t... |
| Business Value | Increases daily user engagement and long-term rete... |
| Functional Area | Statistics and Insights |
| Story Theme | Premium Feature Engagement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Premium User views their active streak

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am an authenticated Premium User with reading sessions logged on each of the last 5 consecutive days (based on my local calendar date)

### 3.1.5 And

the system displays a 'Longest Streak' value of '10'.

### 3.1.6 When

I navigate to the 'Insights' screen

### 3.1.7 Then

the system displays a 'Current Streak' value of '5'

### 3.1.8 Validation Notes

Verify the UI in the 'Insights' section shows two distinct, correctly calculated streak values. The calculation must correctly identify consecutive calendar days.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Premium User extends their current streak

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a Premium User with a current streak of 5 days, with my last session logged yesterday

### 3.2.5 When

I log a new reading session for today

### 3.2.6 And

I navigate to or refresh the 'Insights' screen

### 3.2.7 Then

my 'Current Streak' value updates to '6'.

### 3.2.8 Validation Notes

Test that logging a new session for the current day correctly increments the streak count.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Premium User's current streak surpasses their longest streak

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am a Premium User with a current streak of 10 days

### 3.3.5 And

my 'Longest Streak' value also updates to '11'.

### 3.3.6 When

I log a new reading session for today

### 3.3.7 Then

my 'Current Streak' value updates to '11'

### 3.3.8 Validation Notes

Ensure that when the current streak exceeds the historical longest streak, both values are updated correctly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User's streak is broken due to a missed day

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a Premium User whose last reading session was logged two calendar days ago

### 3.4.5 And

I had a current streak of 7 days prior to the missed day

### 3.4.6 When

I navigate to the 'Insights' screen today

### 3.4.7 Then

my 'Current Streak' value is displayed as '0'.

### 3.4.8 Validation Notes

Verify that a gap of one full calendar day with no reading sessions correctly resets the current streak counter to zero.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

New Premium User with no reading history views streaks

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a new Premium User who has not logged any reading sessions yet

### 3.5.5 When

I navigate to the 'Insights' screen for the first time

### 3.5.6 Then

the 'Current Streak' value is '0'

### 3.5.7 And

the 'Longest Streak' value is '0'.

### 3.5.8 Validation Notes

Confirm that the default state for a user with no data is zero for both metrics.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Free User attempts to access the Insights screen

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am an authenticated Free User

### 3.6.5 When

I attempt to access the feature containing reading streaks (the 'Insights' screen)

### 3.6.6 Then

I am prevented from viewing the streak data

### 3.6.7 And

the system displays a prompt to upgrade to a Premium subscription.

### 3.6.8 Validation Notes

Check that the feature is correctly gated by subscription status. This can be tested by attempting to navigate to the screen or by checking for the absence of the navigation element.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Streak calculation correctly handles multiple sessions on the same day

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a Premium User with a current streak of 3 days

### 3.7.5 When

I log three separate reading sessions today

### 3.7.6 And

I refresh the 'Insights' screen

### 3.7.7 Then

my 'Current Streak' value is '4'.

### 3.7.8 Validation Notes

The streak calculation must count unique calendar days with sessions, not the number of sessions. Multiple sessions on one day count as one day in the streak.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated card or section within the 'Insights' screen.
- A clear label for 'Current Streak'.
- A clear label for 'Longest Streak'.
- Large, legible numeric displays for both streak values.
- A subtle, motivational icon (e.g., a flame) next to the streak numbers.

## 4.2.0 User Interactions

- The user passively views the streak information; no direct interaction with the streak component is required.

## 4.3.0 Display Requirements

- The streak values must be updated automatically upon visiting or refreshing the 'Insights' screen.
- The component must be visually distinct and easy to locate on the screen.

## 4.4.0 Accessibility Needs

- All labels and numeric values must be compatible with screen readers (e.g., TalkBack, VoiceOver).
- Color contrast for text and icons must meet WCAG 2.1 AA standards, as per REQ-UIF-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A 'reading day' is defined as any calendar day (00:00 to 23:59) in the user's local timezone that contains at least one logged reading session.

### 5.1.3 Enforcement Point

Backend streak calculation service.

### 5.1.4 Violation Handling

N/A - This is a definitional rule for the calculation logic.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The current streak is broken if there is at least one full calendar day between the last reading day and the current date.

### 5.2.3 Enforcement Point

Backend streak calculation service.

### 5.2.4 Violation Handling

The current streak value is reset to 0.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Access to the reading streaks feature is restricted to users with an active 'Premium User' status.

### 5.3.3 Enforcement Point

API Gateway and/or mobile client UI.

### 5.3.4 Violation Handling

API requests from non-premium users are rejected with a 403 Forbidden status. The UI hides the feature and shows an upgrade prompt.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The system must be able to identify a user's subscription status (Premium vs. Free) to control access to this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-046

#### 6.1.2.2 Dependency Reason

The system must allow users to log reading sessions with accurate timestamps, as this data is the foundation for all streak calculations.

## 6.2.0.0 Technical Dependencies

- A backend service/endpoint to calculate and return streak data (e.g., GET /api/v1/stats/streaks).
- The `ReadingSession` data model must be defined and include a reliable timestamp for each session.
- The Role-Based Access Control (RBAC) mechanism, based on JWT claims, must be in place to protect the endpoint.

## 6.3.0.0 Data Dependencies

- Requires access to the complete history of a user's `ReadingSession` records.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for fetching streak data must respond within the P95 latency target of < 200ms (NFR-PERF-001).
- The calculation should be optimized to handle users with thousands of reading sessions without degrading performance. Consider pre-calculation or caching strategies.

## 7.2.0.0 Security

- The API endpoint must be protected, requiring an authenticated session (valid JWT).
- The endpoint must enforce authorization, ensuring only users with a 'premium_user' role can access the data (NFR-SEC-002).

## 7.3.0.0 Usability

- The streak information should be presented in a clear, unambiguous, and motivational manner.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards as specified in REQ-UIF-001.

## 7.5.0.0 Compatibility

- The feature must display correctly on all supported iOS and Android devices and OS versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The backend logic for calculating streaks is non-trivial, especially when handling timezones correctly.
- Performance optimization is critical. A naive calculation on every request could be slow for highly active users. An event-driven approach to update stored streak values upon new session creation is recommended.
- Requires robust handling of user-specific timezones to accurately define a 'calendar day'.

## 8.3.0.0 Technical Risks

- Bugs in the timezone conversion logic could lead to inaccurate streak calculations, frustrating users.
- A poorly optimized algorithm could lead to high database load and slow API response times.

## 8.4.0.0 Integration Points

- Mobile Client: The 'Insights' screen in the Flutter app.
- Backend: A new API endpoint in the statistics service.
- Database: Requires efficient querying of the `ReadingSession` table.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify streak calculation with various data sets: no sessions, single session, sessions on consecutive days, sessions with a one-day gap, sessions with multi-day gaps.
- Test with session timestamps that cross UTC day boundaries but not local day boundaries.
- Test API endpoint security: access by Free User, Premium User, and unauthenticated user.
- E2E test: Log a session in the app and verify the streak number updates correctly on the Insights screen.

## 9.3.0.0 Test Data Needs

- Test accounts for both Free and Premium users.
- Ability to seed specific reading session histories for test users to validate complex streak scenarios.

## 9.4.0.0 Testing Tools

- Jest for backend unit tests.
- Postman or similar for API integration tests.
- Flutter's `integration_test` package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Backend streak calculation logic is covered by unit tests with at least 80% code coverage, including timezone-specific cases.
- API endpoint is tested for functionality, performance, and security.
- Flutter UI components are implemented and reviewed against design mockups.
- E2E tests confirm the flow from logging a session to viewing the updated streak.
- Code has been peer-reviewed and merged into the main branch.
- Feature is verified to be accessible only to Premium users.
- Documentation for the new API endpoint is created/updated in OpenAPI format.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a key motivational feature for premium users and a strong selling point for the subscription.
- Backend work on the calculation logic should precede frontend work, as the UI is dependent on the data from the API.

## 11.4.0.0 Release Impact

- This feature will be highlighted in release notes and marketing materials as a new benefit for Premium subscribers.

