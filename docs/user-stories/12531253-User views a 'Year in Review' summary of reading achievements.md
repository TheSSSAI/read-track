# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-066 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views a 'Year in Review' summary of reading a... |
| As A User Story | As a dedicated reader who has completed a yearly g... |
| User Persona | Any user (Free or Premium) who had an active yearl... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Goal Management & Statistics |
| Story Theme | User Engagement and Motivation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Automatic presentation of the summary at the start of a new year

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

A user had an active yearly goal for the previous calendar year (e.g., 2024)

### 3.1.5 When

The user opens the app for the first time on or after January 1st of the new year (e.g., 2025)

### 3.1.6 Then

The system shall present a modal or full-screen 'Year in Review' summary for the previous year.

### 3.1.7 Validation Notes

Test by setting device clock forward. A flag on the user profile (e.g., `showYearInReview_2024: true`) should be checked by the client on startup.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Content of the summary for a Free User

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A Free User is viewing their 'Year in Review' summary

### 3.2.5 When

They navigate through the summary screens

### 3.2.6 Then

The summary must display at least: Total books completed (vs. goal), Total pages read, Total time spent reading, and the user's calculated average reading speed (pages per hour).

### 3.2.7 Validation Notes

Verify that the data displayed matches the aggregated reading session data for the specified year and only includes metrics available to Free Users per REQ-STA-000.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Enhanced content of the summary for a Premium User

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A Premium User is viewing their 'Year in Review' summary

### 3.3.5 When

They navigate through the summary screens

### 3.3.6 Then

The summary must display all Free User metrics PLUS: Longest reading streak of the year, most-read author(s), and most-read genre(s).

### 3.3.7 Validation Notes

Verify that the additional premium metrics are present and correctly calculated per REQ-STA-001.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User is prompted to set a new goal after viewing the summary

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

A user has navigated to the final screen of the 'Year in Review' summary

### 3.4.5 When

They view the final screen

### 3.4.6 Then

A prominent Call-To-Action button labeled 'Set New Goal for [Current Year]' shall be displayed, which navigates them to the goal creation screen.

### 3.4.7 Validation Notes

Confirm the button is present and correctly navigates to the goal setting flow, pre-filled for the current year if possible.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Summary is not shown again after being dismissed

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

A user has viewed and dismissed their 'Year in Review' summary

### 3.5.5 When

They close and reopen the application

### 3.5.6 Then

The summary shall not be presented automatically again.

### 3.5.7 Validation Notes

The client should call an endpoint to update the user's profile flag (e.g., `showYearInReview_2024: false`) after the summary is closed.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Summary is not shown to users without a yearly goal

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

A user did not have an active yearly goal for the previous calendar year

### 3.6.5 When

They open the app for the first time in the new year

### 3.6.6 Then

The 'Year in Review' summary shall not be presented.

### 3.6.7 Validation Notes

Verify that the backend logic for setting the trigger flag correctly identifies users who are not eligible.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Summary gracefully handles a user with a goal but no reading data

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

A user had a yearly goal for the previous year but logged zero reading sessions

### 3.7.5 When

They are presented with the 'Year in Review' summary

### 3.7.6 Then

The summary shall display all metrics as zero and show a motivational message like 'You set a goal, which is the first step! Let's track your reading this year.'

### 3.7.7 Validation Notes

Create a test user with a goal but no reading data to verify the UI handles this state without crashing or showing confusing information.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Full-screen modal or multi-page view (e.g., PageView)
- Visually engaging graphics, icons, and charts
- Prominent 'Set New Goal' button on the final screen
- A 'Share' button to generate a shareable image of the summary
- A 'Dismiss' or 'Close' (X) button

## 4.2.0 User Interactions

- User can swipe or tap to navigate between summary pages.
- Tapping 'Set New Goal' navigates to the goal creation flow.
- Tapping 'Share' opens the native OS share sheet with a generated image.
- Tapping 'Dismiss' closes the summary view.

## 4.3.0 Display Requirements

- All statistics must be clearly labeled (e.g., 'Total Books Read').
- If a goal was met, a celebratory visual confirmation should be shown.
- The design should be consistent with the app's overall theme (light/dark).

## 4.4.0 Accessibility Needs

- All text must adhere to WCAG 2.1 AA contrast ratios.
- Charts must use color-blind accessible palettes as per REQ-STA-001.
- Content should be navigable using screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The 'Year in Review' summary is only generated for the preceding calendar year (Jan 1 - Dec 31).

### 5.1.3 Enforcement Point

Backend data aggregation service.

### 5.1.4 Violation Handling

Data from other years must be excluded from the summary calculations.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The summary is only triggered for users who had an active 'books per year' goal.

### 5.2.3 Enforcement Point

Backend job that determines eligibility.

### 5.2.4 Violation Handling

Users with only daily, weekly, or monthly goals are not eligible for the yearly summary.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-060

#### 6.1.1.2 Dependency Reason

The user must be able to set a yearly goal for this story to be triggered.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-046

#### 6.1.2.2 Dependency Reason

The system must be able to log reading progress to have data for the summary.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-068

#### 6.1.3.2 Dependency Reason

Requires the core logic for calculating basic statistics.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-070

#### 6.1.4.2 Dependency Reason

Requires the logic for calculating reading streaks for Premium users.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-073

#### 6.1.5.2 Dependency Reason

Requires the logic for identifying most-read authors/genres for Premium users.

## 6.2.0.0 Technical Dependencies

- Backend data aggregation mechanism (e.g., scheduled Lambda/Cron job).
- API endpoint to serve the pre-calculated summary data.
- Client-side logic to check for and display the summary on app start.

## 6.3.0.0 Data Dependencies

- Access to historical `ReadingSession` and `Goal` data for all users.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The data aggregation must be performed asynchronously by a backend job to avoid impacting user login or app startup performance.
- The API endpoint serving the summary data must respond in under 200ms (P95).

## 7.2.0.0 Security

- The API endpoint must be protected and only return data for the authenticated user.

## 7.3.0.0 Usability

- The summary should be easy to understand, visually appealing, and celebratory in tone.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The UI must render correctly on all supported iOS and Android devices and screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires a scheduled backend job for pre-calculating summaries to ensure performance.
- The UI/UX design needs to be high-quality and engaging, which can be time-consuming.
- Logic to handle different data points for Free vs. Premium users.
- Implementing the 'render widget to image' for the share feature.

## 8.3.0.0 Technical Risks

- The data aggregation job could fail for some users; robust error handling and retries are needed.
- Ensuring the trigger mechanism works reliably across time zones and for all eligible users on Jan 1st.

## 8.4.0.0 Integration Points

- User Profile Service (to read/write the `showYearInReview` flag).
- Reading Data Store (to aggregate session data).
- Goal Service (to determine eligibility).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify summary for a Free user with complete data.
- Verify summary for a Premium user with complete data.
- Verify summary for a user with a goal but no reading data.
- Verify summary is not shown for a user with no yearly goal.
- Verify the 'Set New Goal' CTA navigation.
- Verify the summary is dismissed permanently after viewing.

## 9.3.0.0 Test Data Needs

- Test accounts for both Free and Premium tiers with a significant amount of reading data from the previous calendar year.
- A test account with a yearly goal but zero reading sessions.
- A test account with no yearly goal.

## 9.4.0.0 Testing Tools

- Jest (Backend unit tests)
- flutter_test, integration_test (Flutter tests)
- A tool to manipulate the device's system clock for triggering the summary.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >= 80% coverage for new logic
- E2E test for the main flow is implemented and passing
- Backend scheduled job is deployed and tested in staging
- User interface reviewed and approved by UX/UI designer
- Performance of the summary API endpoint is verified under load
- Accessibility audit passed for the new screens
- Documentation for the new API endpoint and backend job is created
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a time-sensitive feature that must be deployed before the end of the calendar year to be available on January 1st.
- Requires both backend (data aggregation job, API) and frontend (UI) work that can be done in parallel.

## 11.4.0.0 Release Impact

- Key feature for year-end user engagement. Should be highlighted in release notes and marketing communications.

