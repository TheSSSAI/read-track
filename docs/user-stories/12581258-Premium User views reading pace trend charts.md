# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-071 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User views reading pace trend charts |
| As A User Story | As a Premium User, I want to view interactive char... |
| User Persona | A subscribed Premium User who is data-driven and m... |
| Business Value | Provides a key, tangible benefit for the Premium s... |
| Functional Area | Statistics and Insights |
| Story Theme | Advanced Premium Features |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Chart displays correctly for a user with sufficient reading history

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a Premium User with reading sessions logged across multiple weeks

### 3.1.5 When

I navigate to the 'Insights' screen

### 3.1.6 Then

I see a bar chart titled 'Weekly Reading Pace'

### 3.1.7 And

The chart accurately displays bars whose heights correspond to the total pages read in each respective week for the last 12 weeks.

### 3.1.8 Validation Notes

Verify that the sum of pages for a given week in the chart matches the sum of pages from all 'ReadingSession' records within that week's date range.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User interacts with a data point on the chart

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The 'Weekly Reading Pace' chart is displayed with data

### 3.2.5 When

I tap on a specific bar in the chart

### 3.2.6 Then

A tooltip or overlay appears, displaying the exact number of pages read for that week (e.g., '152 Pages').

### 3.2.7 Validation Notes

Test tapping on multiple bars to ensure the correct data is shown for each. The tooltip should disappear after a short delay or when tapping elsewhere.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Chart area for a new user with no reading history

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am a Premium User who has not logged any reading sessions yet

### 3.3.5 When

I navigate to the 'Insights' screen

### 3.3.6 Then

The chart area displays a user-friendly message, such as 'Start logging your reading sessions to see your trends here!'

### 3.3.7 And

No empty chart axes or error messages are shown.

### 3.3.8 Validation Notes

Create a new premium test user and navigate directly to this screen to verify the empty state.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Chart correctly displays weeks with no reading activity

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a Premium User who read 100 pages two weeks ago and 120 pages this week, but logged zero pages last week

### 3.4.5 When

I view the 'Weekly Reading Pace' chart

### 3.4.6 Then

The chart displays a bar representing a value of 100 for two weeks ago, a bar representing a value of 0 for last week, and a bar representing a value of 120 for this week.

### 3.4.7 Validation Notes

Manually create test data with gaps in activity to ensure the aggregation logic and chart rendering handle zero-value periods correctly.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Chart displays a loading state while fetching data

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

I am a Premium User navigating to the 'Insights' screen

### 3.5.5 When

The data for the chart is being fetched and calculated

### 3.5.6 Then

A skeleton loader or shimmer animation is displayed in the place of the chart.

### 3.5.7 Validation Notes

Use network throttling tools to simulate a slow connection and verify the loading state is displayed correctly and replaced by the chart once data is available.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Chart is accessible to users with color vision deficiencies

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The 'Weekly Reading Pace' chart is displayed

### 3.6.5 When

I view the chart

### 3.6.6 Then

The chart uses a color-blind accessible palette as specified in REQ-STA-001.

### 3.6.7 Validation Notes

Use a color blindness simulator tool to verify that the chart elements are distinguishable for different types of color vision deficiency.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Bar chart component
- Chart Title ('Weekly Reading Pace')
- X-axis labels (Weeks)
- Y-axis labels (Pages Read)
- Interactive tooltips on tap
- Empty state message view
- Skeleton loader/shimmer view

## 4.2.0 User Interactions

- User can scroll the 'Insights' page to view the chart.
- User can tap on a bar to reveal a tooltip with the exact value.
- The chart is view-only and not editable.

## 4.3.0 Display Requirements

- The chart must display data for the last 12 weeks by default.
- The chart must adapt to both light and dark themes.
- All text must respect the user's OS-level font size settings (Dynamic Type).

## 4.4.0 Accessibility Needs

- Must use a color palette compliant with WCAG 2.1 Level AA for contrast and color differentiation.
- Chart data points should be accessible via screen readers if the chosen library supports it (e.g., 'Week of January 8th, 152 pages').

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

This feature is exclusively available to users with an active 'Premium User' subscription status.

### 5.1.3 Enforcement Point

Client-side UI (feature is hidden or shows an upgrade prompt) and Backend API (endpoint returns 403 Forbidden for non-premium users).

### 5.1.4 Violation Handling

A Free User attempting to access the endpoint will receive an error. The UI will not show the feature or will display a paywall.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The reading pace calculation for this chart only includes progress logged by page number (typically for books). Progress logged by percentage (for articles) is excluded.

### 5.2.3 Enforcement Point

Backend data aggregation query.

### 5.2.4 Violation Handling

N/A - This is a rule for data inclusion/exclusion.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

A user must be able to subscribe and achieve 'Premium User' status to access this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-046

#### 6.1.2.2 Dependency Reason

The chart relies on data generated by users logging their reading progress by page number.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-070

#### 6.1.3.2 Dependency Reason

This story assumes the existence of the 'Insights' screen, which is likely established by the 'Reading Streaks' feature, another premium insight.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to provide aggregated weekly reading pace data for a given user.
- A Flutter charting library (e.g., fl_chart) integrated into the project.
- The user's role/subscription status must be present in the JWT to allow the backend to authorize the request.

## 6.3.0.0 Data Dependencies

- Requires access to the user's complete 'ReadingSession' history.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for chart data must respond with a P95 latency of < 300ms.
- The 'Insights' screen, including the chart, must render in under 2 seconds on a standard 4G connection.

## 7.2.0.0 Security

- The API endpoint that serves the chart data must be protected and can only be accessed by the authenticated user who owns the data.
- The endpoint must validate the user's JWT and confirm their 'Premium User' status before returning data.

## 7.3.0.0 Usability

- The chart must be simple and intuitive, requiring no explanation for the user to understand their reading trends.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards, particularly regarding color contrast and non-reliance on color alone to convey information.

## 7.5.0.0 Compatibility

- The chart must render correctly on all supported iOS and Android screen sizes and OS versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend: Requires an efficient database query to aggregate potentially large amounts of time-series data into weekly buckets. Handling timezones correctly for 'week' definitions is critical.
- Frontend: Integrating and customizing a charting library to match the app's UI/UX, theme, and accessibility requirements.
- State Management: Handling loading, empty, and data-filled states for the chart requires careful state management on the client.

## 8.3.0.0 Technical Risks

- Performance of the data aggregation query for users with very long reading histories.
- Chosen charting library may have limitations regarding accessibility or customization.

## 8.4.0.0 Integration Points

- Client-side 'Insights' screen integrates with the new backend API endpoint.
- Backend service integrates with the primary database (Amazon Aurora) to fetch ReadingSession data.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify chart accuracy with a known dataset.
- Test with a user who has no reading data.
- Test with a user who has gaps in their reading data.
- Test the API endpoint security by making a request as a Free User (expect 403).
- Verify chart rendering on the smallest and largest supported screen sizes.
- Test light and dark mode rendering.
- Use a color blindness simulator to check accessibility.

## 9.3.0.0 Test Data Needs

- Test account with 'Premium User' status and a varied reading history (e.g., >12 weeks of data with gaps).
- Test account with 'Premium User' status and no reading history.
- Test account with 'Free User' status.

## 9.4.0.0 Testing Tools

- Jest (Backend unit tests)
- flutter_test (Flutter widget tests)
- integration_test (Flutter E2E tests)
- Postman or similar for API endpoint testing.
- Browser/IDE accessibility tools for checking color contrast and simulators.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% code coverage
- Backend API endpoint is deployed and successfully tested
- Frontend chart is integrated and verified against UI mockups
- Performance requirements for API and screen load time are met
- Security requirements for the API endpoint are validated
- Accessibility checks (color contrast, dynamic type) have been performed
- Story deployed and verified in the staging environment on both iOS and Android

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- The backend API endpoint should be prioritized to unblock frontend development. A mock API can be used in the interim.
- Requires both backend (Node.js/TypeScript) and frontend (Flutter) development effort.

## 11.4.0.0 Release Impact

This is a significant feature for the Premium offering and should be highlighted in release notes and marketing materials.

