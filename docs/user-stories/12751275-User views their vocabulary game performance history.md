# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-088 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views their vocabulary game performance histo... |
| As A User Story | As a user (both Free and Premium), I want to view ... |
| User Persona | Any registered user (Free or Premium) who has enga... |
| Business Value | Increases user engagement and retention by providi... |
| Functional Area | Vocabulary Games |
| Story Theme | User Engagement and Progress Tracking |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User with existing game history views the performance screen

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user who has played at least three vocabulary games of mixed types (e.g., two quizzes, one flashcard session)

### 3.1.5 When

I navigate to the Vocabulary Games section and select the 'Performance History' option

### 3.1.6 Then

The system displays a dedicated history screen containing summary statistics (e.g., 'Average Score', 'Total Games Played'), a performance chart, and a list of recent sessions.

### 3.1.7 Validation Notes

Verify that the screen loads and all three components (summary, chart, list) are present and populated with data corresponding to the user's game history.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Performance chart correctly visualizes progress over time

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the 'Performance History' screen with multiple game sessions recorded

### 3.2.5 When

I view the performance chart

### 3.2.6 Then

The chart displays a line graph where the X-axis represents the date of the game sessions and the Y-axis represents the score percentage (0-100%).

### 3.2.7 Validation Notes

Test with a known data set to ensure data points are plotted correctly. The chart should be interactive, showing a tooltip with the exact score and date on tap/hover.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Recent sessions list is correctly displayed and ordered

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am on the 'Performance History' screen

### 3.3.5 When

I view the list of recent game sessions

### 3.3.6 Then

A list of my most recent games is displayed in reverse chronological order (newest first).

### 3.3.7 And

Each list item clearly shows the Game Type (e.g., 'Multiple-Choice Quiz'), the Date, and the Score (e.g., '8/10 - 80%').

### 3.3.8 Validation Notes

Verify the sorting order and the format of the information in each list item. If more than 20 sessions exist, pagination or lazy loading should be implemented.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User filters the performance history by game type

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am on the 'Performance History' screen viewing data for 'All Games'

### 3.4.5 When

I select the 'Quiz' filter option

### 3.4.6 Then

The summary statistics, performance chart, and recent sessions list all update to show data exclusively from 'Multiple-Choice Quiz' games.

### 3.4.7 Validation Notes

Test the filter functionality for each available game type ('All', 'Quiz', 'Flashcards'). The UI should update instantly without a full page reload.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

New user with no game history views the performance screen

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am a new user who has never played a vocabulary game

### 3.5.5 When

I navigate to the 'Performance History' screen

### 3.5.6 Then

The system displays a well-designed empty state message, such as 'No game history yet. Play a game to start tracking your progress!'.

### 3.5.7 And

A clear call-to-action button is present that navigates me to the game selection screen.

### 3.5.8 Validation Notes

Ensure no errors are thrown and the empty state is user-friendly and guides the user on what to do next.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

User views performance history while offline

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I have previously viewed my performance history while online, and my device is now offline

### 3.6.5 When

I navigate to the 'Performance History' screen

### 3.6.6 Then

The screen loads successfully and displays the data that was cached during my last online session.

### 3.6.7 Validation Notes

Test by enabling airplane mode after loading the data once. The screen should still be accessible and display the last known state.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Filter control (e.g., Segmented Control, Dropdown) for game types: 'All', 'Quiz', 'Flashcards'.
- Summary statistics display area.
- Interactive line chart for performance visualization.
- Scrollable list for recent game sessions.
- Empty state view with text and a call-to-action button.

## 4.2.0 User Interactions

- User can tap the filter to change the displayed data.
- User can tap a data point on the chart to see a tooltip with details.
- User can scroll through the list of recent sessions.

## 4.3.0 Display Requirements

- Summary stats must include at least 'Average Score' and 'Total Games Played'.
- Chart must be clearly labeled with axes for 'Date' and 'Score %'.
- Each session in the list must display Game Type, Date, and Score.

## 4.4.0 Accessibility Needs

- The performance chart must use a color-blind accessible palette (as per REQ-STA-001).
- All text and data must be compatible with screen readers.
- All interactive elements must have a minimum touch target size of 44x44dp and provide haptic feedback where appropriate.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Performance history is user-specific and private.

### 5.1.3 Enforcement Point

API Gateway and Backend Service

### 5.1.4 Violation Handling

API requests for another user's data must be rejected with a 403 Forbidden or 404 Not Found status.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Performance data is tracked for both Free and Premium users.

### 5.2.3 Enforcement Point

Backend Service (Game Session Logging)

### 5.2.4 Violation Handling

N/A - This is a rule of inclusion.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-083

#### 6.1.1.2 Dependency Reason

Must be able to play and log results for Flashcard games (Free User) before history can be shown.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-084

#### 6.1.2.2 Dependency Reason

Must be able to play and log results for Quiz games (Free User) before history can be shown.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-086

#### 6.1.3.2 Dependency Reason

Must be able to play and log results for Flashcard games (Premium User) before history can be shown.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-087

#### 6.1.4.2 Dependency Reason

Must be able to play and log results for Quiz games (Premium User) before history can be shown.

## 6.2.0.0 Technical Dependencies

- Backend: A new database table (`VocabularyGameSession`) is required to store game results.
- Backend: A new API endpoint (e.g., `GET /api/v1/vocabulary/history`) must be created to serve the performance data.
- Mobile Frontend: A charting library (e.g., `fl_chart`) must be integrated into the Flutter project.
- Mobile Frontend: Local database (Isar) schema must be updated to cache performance history for offline access.

## 6.3.0.0 Data Dependencies

- Requires access to the user's authenticated session to fetch their specific data.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The performance history screen must load and become interactive in under 1.5 seconds on a standard 4G network (NFR-PERF-002).
- API response time for the history endpoint must have a P95 latency of less than 200ms (NFR-PERF-001).

## 7.2.0.0 Security

- The API endpoint for fetching performance history must be protected and only accessible by an authenticated user.
- The endpoint must enforce that a user can only retrieve their own data.

## 7.3.0.0 Usability

- The chart and data visualizations must be intuitive and easy for a non-technical user to understand.
- The empty state must clearly guide the user on how to generate data for this screen.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards (REQ-UIF-001).

## 7.5.0.0 Compatibility

- The UI must render correctly on all supported iOS and Android screen sizes.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires both backend (new table, new API endpoint with aggregations) and frontend (new screen, chart integration, state management) development.
- Implementation of offline caching and synchronization for game history adds complexity.
- Designing a visually appealing and informative chart requires careful UI/UX consideration.

## 8.3.0.0 Technical Risks

- The chosen charting library might have performance issues with very large datasets. Pagination or data sampling might be needed for long-term users.
- Ensuring data consistency between offline-played games and the synced server state requires robust conflict resolution logic.

## 8.4.0.0 Integration Points

- Backend: Integrates with the primary user database (Aurora) to store and retrieve session data.
- Frontend: Integrates with the local Isar database for caching.
- Frontend: Integrates with the app's state management (Riverpod) to handle data fetching and filtering.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify history screen with no data (empty state).
- Verify history screen with a single game session.
- Verify history screen with a large number of game sessions (>50).
- Verify filter functionality works correctly for all options.
- Verify offline viewing of cached data.
- Verify that a game played offline appears in the history after the device reconnects and syncs.

## 9.3.0.0 Test Data Needs

- Test accounts with zero, one, and many (50+) game sessions of mixed types.

## 9.4.0.0 Testing Tools

- Backend: Jest for unit/integration tests.
- Frontend: `flutter_test` for unit/widget tests, `integration_test` for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% code coverage
- Backend and frontend integration testing completed successfully
- User interface reviewed and approved by UX/Product team
- Performance requirements verified on mid-range devices
- Accessibility checks (screen reader, dynamic type) have been performed
- Documentation for the new API endpoint is created in OpenAPI format
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story should be scheduled in a sprint after the core vocabulary game playing functionality (US-083, US-084, US-086, US-087) is completed and merged.
- Requires coordinated effort between backend and frontend developers.

## 11.4.0.0 Release Impact

- This is a significant feature enhancement for the vocabulary games. It should be highlighted in release notes as a new way for users to track their learning progress.

