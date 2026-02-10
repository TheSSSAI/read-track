# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-072 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User views reading activity by time and da... |
| As A User Story | As a Premium User, I want to view a visual breakdo... |
| User Persona | A Premium User who is data-driven and wants to gai... |
| Business Value | Enhances the value proposition of the Premium subs... |
| Functional Area | Statistics and Insights |
| Story Theme | Advanced User Analytics |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display of Weekly Reading Habits Heatmap

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Premium User with multiple reading sessions recorded over the past several weeks

### 3.1.5 When

I navigate to the 'Insights' section of the application

### 3.1.6 Then

I should see a visualization, such as a heatmap chart, titled 'Weekly Reading Habits'.

### 3.1.7 Validation Notes

Verify the presence of the chart component on the Insights screen for a premium user account with session data.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Correct Data Aggregation and Visualization

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The 'Weekly Reading Habits' chart is displayed

### 3.2.5 When

The data is rendered in the chart

### 3.2.6 Then

The chart must have days of the week (e.g., Mon-Sun) on one axis and defined time blocks ('Morning', 'Afternoon', 'Evening', 'Night') on the other axis.

### 3.2.7 And

A legend must be visible that clearly explains the color scale (e.g., light color for less time, dark color for more time).

### 3.2.8 Validation Notes

Cross-reference the reading session data in the database with the colors displayed in the chart for a test user. Ensure the aggregation logic correctly sums durations.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Interaction with Chart Cells

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am viewing the 'Weekly Reading Habits' chart with data

### 3.3.5 When

I tap or long-press on a specific cell (e.g., 'Tuesday, Evening')

### 3.3.6 Then

A tooltip or overlay should appear, displaying the precise total reading time for that block (e.g., '4h 15m').

### 3.3.7 Validation Notes

Test the tap interaction on multiple cells, including empty ones (which should show '0m' or no tooltip) and populated ones, to verify the correct data is displayed.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Handling of No Reading Data

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a logged-in Premium User who has not yet logged any reading sessions

### 3.4.5 When

I navigate to the 'Insights' section

### 3.4.6 Then

The chart area should display a user-friendly placeholder message, such as 'Start logging reading sessions to see your habits here!', instead of an empty chart.

### 3.4.7 Validation Notes

Test with a newly created premium account that has zero reading sessions.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Access Control for Free Users

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a logged-in Free User

### 3.5.5 When

I navigate to the 'Insights' section

### 3.5.6 Then

The 'Weekly Reading Habits' chart feature must not be accessible.

### 3.5.7 And

The area where the chart would be should display a prompt to upgrade to the Premium tier.

### 3.5.8 Validation Notes

Log in with a Free User account and confirm the feature is locked and the upgrade prompt is displayed as per REQ-STA-001.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Accessibility Compliance

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The 'Weekly Reading Habits' chart is displayed

### 3.6.5 When

I view the chart

### 3.6.6 Then

The color palette used must be color-blind accessible and meet WCAG 2.1 Level AA contrast ratios.

### 3.6.7 And

When using a screen reader, the chart data must be announced in a logical sequence (e.g., 'Monday, Morning, 30 minutes.').

### 3.6.8 Validation Notes

Use accessibility analysis tools to check color contrast. Perform manual testing with VoiceOver (iOS) and TalkBack (Android).

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A heatmap or matrix-style chart component.
- Axes labels for days of the week and time blocks.
- A chart title (e.g., 'Weekly Reading Habits').
- A color legend to explain data intensity.
- A tooltip/overlay for displaying detailed information on tap.
- A placeholder view for the 'no data' state.

## 4.2.0 User Interactions

- User navigates to the 'Insights' screen to view the chart.
- User can tap/long-press a cell in the chart to view the exact duration.

## 4.3.0 Display Requirements

- The chart must aggregate and display the total reading duration (in hours and minutes).
- The chart must adapt to both light and dark themes.
- The chart must be responsive and render correctly on various supported device screen sizes.

## 4.4.0 Accessibility Needs

- Adherence to WCAG 2.1 Level AA standards, particularly for color contrast as specified in REQ-STA-001.
- Content should be compatible with screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

This feature is exclusively available to users with an active 'Premium User' subscription status.

### 5.1.3 Enforcement Point

Backend API endpoint and Frontend UI rendering.

### 5.1.4 Violation Handling

API will return a 403 Forbidden error. UI will display an upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Time of day is categorized into four blocks: Morning (06:00-11:59), Afternoon (12:00-17:59), Evening (18:00-21:59), and Night (22:00-05:59).

### 5.2.3 Enforcement Point

Backend data aggregation service.

### 5.2.4 Violation Handling

N/A - This is a data processing rule.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

A reading session's entire duration is attributed to the time block in which the session started.

### 5.3.3 Enforcement Point

Backend data aggregation service.

### 5.3.4 Violation Handling

N/A - This is a data processing rule to handle sessions that cross time-block boundaries.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

The user must be able to become a Premium User to access this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-048

#### 6.1.2.2 Dependency Reason

The system must be able to log reading sessions with duration, as this is the core data for the analysis.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-049

#### 6.1.3.2 Dependency Reason

The system must be able to log reading sessions with duration, as this is the core data for the analysis.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-070

#### 6.1.4.2 Dependency Reason

The 'Insights' screen, where this chart will be located, must exist.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to provide aggregated reading session data.
- A Flutter charting library capable of rendering a heatmap.
- The authentication service must provide the user's subscription tier in the JWT claims for access control.

## 6.3.0.0 Data Dependencies

- Requires access to the `ReadingSession` table containing user_id, start_time, and duration_in_minutes.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for fetching chart data must respond with P95 latency under 200ms (NFR-PERF-001).
- The Insights screen, including this chart, must load and become interactive in under 1.5 seconds on a standard 4G network (NFR-PERF-002).

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible to authenticated users with a 'premium_user' role, enforced via JWT validation (NFR-SEC-002).

## 7.3.0.0 Usability

- The chart must be intuitive and easy to understand at a glance.
- Interaction with the chart should be responsive and provide immediate feedback.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards (REQ-UIF-001).

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The backend database query for data aggregation can be complex, requiring grouping by user, day of week, and custom time blocks.
- Performance optimization for the aggregation query will be necessary for users with large amounts of historical data.
- Integrating and styling a third-party charting library in Flutter to match the app's design and accessibility requirements.

## 8.3.0.0 Technical Risks

- The chosen charting library may have limitations in styling or accessibility features.
- A naive data aggregation query could lead to slow API response times for highly active users.

## 8.4.0.0 Integration Points

- Backend: Integrates with the Aurora (PostgreSQL) database to read `ReadingSession` data.
- Frontend: Integrates with the backend API to fetch chart data.
- System: Integrates with the Auth0-based authentication system to check user subscription status.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify chart rendering for a premium user with data.
- Verify placeholder display for a premium user with no data.
- Verify access denial and upgrade prompt for a free user.
- Verify data accuracy by comparing chart values to raw session data.
- Test behavior of sessions that start in one time block and end in another.
- Test on various screen sizes and in both light and dark modes.
- Test with screen readers (VoiceOver/TalkBack).

## 9.3.0.0 Test Data Needs

- A test account with 'Premium User' status and a significant number of reading sessions spread across all days and time blocks.
- A test account with 'Premium User' status and zero reading sessions.
- A test account with 'Free User' status.

## 9.4.0.0 Testing Tools

- Jest (Backend unit tests)
- flutter_test / integration_test (Flutter tests)
- Postman or similar for API endpoint testing.
- Browser dev tools or dedicated accessibility checkers for WCAG compliance.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented for backend aggregation logic and frontend components, achieving >80% coverage
- Integration testing between frontend and backend completed successfully
- User interface reviewed and approved by UX/UI designer, including accessibility checks
- Performance requirements for the API endpoint are verified under load
- Security requirements (role-based access) are validated
- Documentation for the new API endpoint is created in OpenAPI format
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This is a feature-enhancing story for premium users, best scheduled after core functionalities are stable.
- Requires coordinated work between backend (API endpoint) and frontend (UI implementation). The API can be developed first against a contract, unblocking frontend work with mock data.

## 11.4.0.0 Release Impact

- This feature will be a key marketing point for the Premium subscription tier in release notes and promotional materials.

