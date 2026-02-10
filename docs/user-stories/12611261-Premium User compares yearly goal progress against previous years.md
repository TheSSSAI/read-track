# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-074 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User compares yearly goal progress against... |
| As A User Story | As a long-term Premium User, I want to visually co... |
| User Persona | Premium User who has been using the application fo... |
| Business Value | Increases the perceived value of the Premium subsc... |
| Functional Area | Statistics and Insights |
| Story Theme | Premium Feature Enhancement |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Premium User with multiple years of data views comparison

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Premium User with completed 'books per year' goals for 2023 (40 books) and 2024 (50 books)

### 3.1.5 When

I navigate to the 'Insights' screen

### 3.1.6 Then

I see a 'Yearly Goal Comparison' chart that displays data for 2023, 2024, and my current year's progress.

### 3.1.7 Validation Notes

Verify the chart renders correctly with bars corresponding to the historical data. The current year's bar should be clearly marked as 'in progress'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Edge Case: Premium User in their first year of using the app

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

I am a logged-in Premium User who has been using the app for less than one full year

### 3.2.5 When

I navigate to the 'Insights' screen

### 3.2.6 Then

I see a placeholder message in the 'Yearly Goal Comparison' area explaining that the chart will appear after my first year is complete.

### 3.2.7 Validation Notes

Confirm that no chart is displayed and the placeholder text is user-friendly and informative.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Edge Case: User has a gap year with no goal set

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am a Premium User with a completed 'books per year' goal for 2023, but I did not set a yearly goal for 2024

### 3.3.5 When

I navigate to the 'Insights' screen

### 3.3.6 Then

the 'Yearly Goal Comparison' chart displays data for 2023 and the current year, but correctly omits any data or label for 2024.

### 3.3.7 Validation Notes

Check that the chart's x-axis does not show a bar or placeholder for the year with no goal data.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Error Condition: Backend fails to load historical data

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am a logged-in Premium User with historical goal data

### 3.4.5 When

I navigate to the 'Insights' screen and the API call to fetch comparison data fails

### 3.4.6 Then

the 'Yearly Goal Comparison' area displays a user-friendly error message and a 'Retry' button.

### 3.4.7 Validation Notes

Use a tool like Charles Proxy or mock the API response to simulate a 5xx error and verify the UI state.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Alternative Flow: User interacts with the chart

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the 'Yearly Goal Comparison' chart is displayed with data

### 3.5.5 When

I tap on a bar representing a specific year

### 3.5.6 Then

a tooltip or overlay appears showing the exact number of books read for that year (e.g., '50 books').

### 3.5.7 Validation Notes

Verify that tapping each bar in the chart triggers the display of the correct data in a tooltip.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Security: Free User cannot access the feature

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am a logged-in Free User

### 3.6.5 When

I attempt to navigate to the 'Insights' screen

### 3.6.6 Then

I am blocked from accessing the screen, as it is a Premium feature.

### 3.6.7 Validation Notes

This is covered by REQ-STA-001, but serves as a regression check. Verify the 'Insights' tab/button is either hidden, disabled, or leads to an upgrade prompt for Free Users.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Bar chart component for data visualization.
- Labels for each year on the x-axis.
- Labels for the quantity (e.g., number of books) on the y-axis.
- Tooltip/Overlay for displaying detailed data on interaction.
- Placeholder view for users with no historical data.
- Error state view with a 'Retry' button.

## 4.2.0 User Interactions

- User can view the chart upon entering the 'Insights' screen.
- Tapping a bar on the chart reveals a tooltip with the exact value.

## 4.3.0 Display Requirements

- The chart must clearly distinguish between completed years and the current, in-progress year.
- The chart must be visually clean and easy to interpret at a glance.

## 4.4.0 Accessibility Needs

- The chart must use a color-blind accessible palette as per REQ-STA-001.
- All chart elements (bars, axes) must be properly labeled for screen readers (e.g., '2023, 40 books').
- The feature must respect the user's dynamic type settings.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

This feature is exclusively available to users with an active 'Premium User' subscription status.

### 5.1.3 Enforcement Point

Backend API endpoint and Frontend UI rendering.

### 5.1.4 Violation Handling

API will return a 403 Forbidden error. UI will not render the component or will show an upgrade prompt.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The comparison will only be performed on goals of the same type, prioritizing 'Number of books per year'.

### 5.2.3 Enforcement Point

Backend data aggregation logic.

### 5.2.4 Violation Handling

Years with non-matching goal types will be excluded from the dataset returned to the client for this specific chart.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-019

#### 6.1.1.2 Dependency Reason

User must be able to subscribe to the Premium tier to gain access to this feature.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-060

#### 6.1.2.2 Dependency Reason

The system must allow users to set yearly goals, which is the source data for this comparison feature.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-066

#### 6.1.3.2 Dependency Reason

The system needs to finalize and store yearly goal achievements, which will be queried by this feature.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-070

#### 6.1.4.2 Dependency Reason

The 'Insights' screen, which will host this comparison chart, must be implemented first.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to provide aggregated yearly goal data for a given user.
- A frontend charting library for Flutter (e.g., fl_chart) that supports accessibility and customization.
- Existence of historical data in the 'Goal' and 'ReadingSession' tables in the Aurora database.

## 6.3.0.0 Data Dependencies

- Requires access to a user's historical goal settings and completion data, potentially spanning multiple years.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API endpoint for historical goal data must respond with P95 latency under 300ms.
- The chart on the client-side must render within 500ms of receiving data.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible to the authenticated user who owns the data.
- The endpoint must validate that the user's subscription status is 'Premium'.

## 7.3.0.0 Usability

- The chart should be intuitive and require no explanation for the user to understand their progress over time.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards, particularly for color contrast and screen reader support.

## 7.5.0.0 Compatibility

- The chart must render correctly on all supported iOS and Android devices and screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend: Requires an efficient database query to aggregate data across multiple years. A materialized view or summary table might be needed to ensure performance as user data grows.
- Frontend: Implementing a responsive, interactive, and accessible chart component with multiple states (loading, error, empty, data) requires careful state management.
- Data Logic: Handling edge cases like missing data for certain years or different goal types requires robust logic.

## 8.3.0.0 Technical Risks

- The data aggregation query could become slow over time for users with many years of data. This should be load-tested.
- The chosen charting library may have limitations regarding accessibility or customization.

## 8.4.0.0 Integration Points

- Client (Flutter App) -> Backend API Gateway -> Goal Aggregation Service -> Aurora Database

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify chart rendering for a user with 0, 1, and 3+ years of data.
- Verify the placeholder UI for a new Premium user.
- Verify the error UI when the backend API returns an error.
- Verify chart interaction (tooltips) on both iOS and Android.
- Manual testing with VoiceOver (iOS) and TalkBack (Android) to ensure accessibility.

## 9.3.0.0 Test Data Needs

- Test accounts must be seeded with various historical goal data configurations: no history, single year, multiple consecutive years, and years with gaps.

## 9.4.0.0 Testing Tools

- Flutter's `integration_test` package for E2E tests.
- Postman or similar for API endpoint testing.
- Platform-native accessibility inspectors.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% coverage
- Integration testing between frontend and new backend endpoint completed successfully
- User interface reviewed and approved by UX/UI designer
- Performance requirements for API and client rendering verified
- Accessibility testing passed for screen readers and color contrast
- New API endpoint is documented in the OpenAPI specification
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story is dependent on the completion of the core goal-setting and 'Insights' screen features.
- Requires coordination with QA to ensure appropriate test data is available in the staging environment.

## 11.4.0.0 Release Impact

This is a key feature for demonstrating the value of the Premium subscription in the long term. It should be included in a release focused on enhancing premium features.

