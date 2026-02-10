# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-068 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views basic reading statistics |
| As A User Story | As a registered user (Free or Premium), I want to ... |
| User Persona | Any authenticated user (Free or Premium) who has l... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Statistics and Insights |
| Story Theme | User Engagement and Progress Tracking |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User with reading data views their statistics

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has logged two reading sessions (Session 1: 50 pages in 60 minutes; Session 2: 100 pages in 90 minutes) and has marked one book as 'Read'

### 3.1.5 When

the user navigates to the Statistics screen

### 3.1.6 Then

the screen correctly displays 'Total Pages Read: 150', 'Total Time Spent: 2 hours 30 minutes', 'Total Books Completed: 1', and 'Average Reading Speed: 60 pages/hour'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

New user with no reading data views the statistics screen

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

a new user has not logged any reading sessions or completed any books

### 3.2.5 When

the user navigates to the Statistics screen

### 3.2.6 Then

the screen displays 'Total Pages Read: 0', 'Total Time Spent: 0 hours 0 minutes', 'Total Books Completed: 0', and the 'Average Reading Speed' metric is either hidden or displays 'N/A'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Statistics update after logging a new session

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the user is on the Statistics screen displaying their current totals

### 3.3.5 When

the user logs a new reading session and returns to the Statistics screen

### 3.3.6 Then

all relevant statistics (pages, time, speed) are immediately updated to reflect the new session.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User has logged time but no pages

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

a user has only logged reading sessions with time but zero pages (e.g., for articles)

### 3.4.5 When

the user navigates to the Statistics screen

### 3.4.6 Then

the 'Total Time Spent' is displayed correctly, 'Total Pages Read' is 0, and 'Average Reading Speed' is either hidden or displays 'N/A' to prevent a division-by-zero error.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Free User sees a banner ad on the statistics screen

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the user is on the 'Free User' subscription tier

### 3.5.5 When

the user navigates to the Statistics screen

### 3.5.6 Then

a banner ad unit is displayed on the screen, as per REQ-ADS-001.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Premium User does not see ads on the statistics screen

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

the user is on the 'Premium User' subscription tier

### 3.6.5 When

the user navigates to the Statistics screen

### 3.6.6 Then

the screen is displayed without any advertisements.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Statistics screen includes an upsell prompt for advanced insights

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

any user is viewing the basic statistics screen

### 3.7.5 When

they view their stats

### 3.7.6 Then

a non-intrusive UI element (e.g., a button or banner) is present that says 'Unlock Advanced Insights' and links to the Premium subscription page.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated screen titled 'Statistics'.
- Display cards or sections for each of the four key metrics: Total Pages Read, Total Time Spent, Total Books Completed, Average Reading Speed.
- Clear labels for each metric.
- A banner ad container for Free Users.
- A call-to-action button/link to the Premium 'Insights' feature.

## 4.2.0 User Interactions

- The screen is read-only.
- Tapping the 'Unlock Advanced Insights' element navigates the user to the subscription upgrade screen.

## 4.3.0 Display Requirements

- Time spent must be formatted in a human-readable way (e.g., 'Xh Ym').
- Large numbers (e.g., pages > 999) should use comma separators.
- If calculations are not instantaneous, a loading indicator must be shown.

## 4.4.0 Accessibility Needs

- All text must respect the OS-level dynamic type settings (REQ-UIF-001).
- Sufficient color contrast must be used for all text and UI elements to meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-STA-001

### 5.1.2 Rule Description

Average Reading Speed is calculated as Total Pages Read divided by Total Time Spent in hours.

### 5.1.3 Enforcement Point

On calculation of statistics for display.

### 5.1.4 Violation Handling

If Total Time Spent is zero, the Average Reading Speed metric shall not be displayed or shall show a value of 'N/A'.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-STA-002

### 5.2.2 Rule Description

Total Books Completed is a count of all LibraryItems with a status of 'Read'. Items with 'Did Not Finish' status are not included.

### 5.2.3 Enforcement Point

On calculation of statistics for display.

### 5.2.4 Violation Handling

N/A

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-046

#### 6.1.1.2 Dependency Reason

Requires the ability to log reading progress by page number to calculate 'Total Pages Read'.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-049

#### 6.1.2.2 Dependency Reason

Requires the ability to log time spent reading to calculate 'Total Time Spent'.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-043

#### 6.1.3.2 Dependency Reason

Requires the ability to move a book to the 'Read' shelf to calculate 'Total Books Completed'.

## 6.2.0.0 Technical Dependencies

- The local Isar database schema for `ReadingSession` and `LibraryItem` must be finalized.
- The backend data models and synchronization logic must be in place to ensure data consistency.

## 6.3.0.0 Data Dependencies

- This feature is entirely dependent on user-generated `ReadingSession` data.

## 6.4.0.0 External Dependencies

- For Free Users, this story depends on the integration of the Google AdMob SDK (REQ-ADS-001).

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The statistics screen must load and display all metrics in under 1.5 seconds on a standard 4G network (NFR-PERF-002).
- Calculations should not block the UI thread.

## 7.2.0.0 Security

- All data used for statistics must be fetched from the user's own authenticated session data.

## 7.3.0.0 Usability

- The information must be presented clearly and concisely, avoiding clutter.
- The distinction between basic stats and the premium 'Insights' feature should be clear.

## 7.4.0.0 Accessibility

- Must adhere to WCAG 2.1 Level AA standards (REQ-UIF-001).

## 7.5.0.0 Compatibility

- The UI must adapt gracefully to all supported screen sizes on iOS and Android (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The core logic involves simple data aggregation (SUM, COUNT) from the local database.
- Requires careful handling of the division-by-zero edge case for average reading speed.
- UI state management is needed to handle the 'no data' state and the difference between Free/Premium user views (ad vs. no ad).

## 8.3.0.0 Technical Risks

- For users with a very large reading history (thousands of sessions), client-side calculation might introduce a minor delay. This can be mitigated with a loading indicator and considered for future backend optimization if necessary.

## 8.4.0.0 Integration Points

- Local Database (Isar): Reading `ReadingSession` and `LibraryItem` data.
- Ad Platform (Google AdMob): Displaying a banner ad for Free Users.
- Subscription Management System: To determine if the user is Free or Premium.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify calculations with a seeded dataset containing various session types (pages only, time only, both).
- Verify the UI for a brand new user with no data.
- Verify the UI for a Free User (ad is visible).
- Verify the UI for a Premium User (ad is not visible).
- End-to-end test: Log a new session -> navigate to stats -> verify update -> edit session -> verify update -> delete session -> verify update.

## 9.3.0.0 Test Data Needs

- Test accounts for both 'Free User' and 'Premium User' tiers.
- A test account with no reading data.
- A test account with a significant amount of reading data to test performance.

## 9.4.0.0 Testing Tools

- `flutter_test` for unit and widget tests.
- `integration_test` package for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% code coverage for the new logic
- Integration testing completed successfully against a staging environment
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on mid-range test devices
- Accessibility requirements (dynamic type, contrast) validated
- Documentation for the statistics calculation logic is updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a foundational feature for user engagement and should be prioritized early in the development cycle, after the core reading tracking functionality is complete.
- It is a prerequisite for the 'Advanced Statistics' feature (REQ-STA-001).

## 11.4.0.0 Release Impact

- This is a core feature expected in the initial v1.0 release.

