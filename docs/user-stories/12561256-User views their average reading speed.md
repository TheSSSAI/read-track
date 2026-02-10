# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-069 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User views their average reading speed |
| As A User Story | As a user who tracks my reading, I want the app to... |
| User Persona | All Users (Free and Premium) |
| Business Value | Provides users with a key data-driven insight into... |
| Functional Area | Statistics and Insights |
| Story Theme | Personal Reading Analytics |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Calculation with valid reading sessions

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user has logged two reading sessions for books: (Session 1: 20 pages in 30 minutes) and (Session 2: 40 pages in 60 minutes)

### 3.1.5 When

the user navigates to the statistics screen

### 3.1.6 Then

the system calculates the total pages (60), total time in hours (1.5), and displays the average reading speed as '40 pages/hour'.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Initial state with no reading data

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

a new user has not logged any reading sessions with page numbers

### 3.2.5 When

the user navigates to the statistics screen

### 3.2.6 Then

the average reading speed is displayed as '0 pages/hour' or with a placeholder message like 'Log reading sessions to see your speed'.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Data filtering for sessions without page numbers

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

a user has logged a book session (30 pages in 60 minutes) and an article session (25% progress in 20 minutes)

### 3.3.5 When

the user navigates to the statistics screen

### 3.3.6 Then

the system only uses the book session for the calculation and displays the average reading speed as '30 pages/hour'.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Handling sessions with zero logged time

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a user has logged a session with 50 pages but 0 minutes of duration

### 3.4.5 When

the average reading speed is calculated

### 3.4.6 Then

this session is excluded from the calculation to prevent a division-by-zero error.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Display format and rounding

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the calculated average reading speed is 45.67 pages per hour

### 3.5.5 When

the value is displayed on the statistics screen

### 3.5.6 Then

the value is rounded to the nearest whole number and displayed as '46 pages/hour'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated card or section on the 'Statistics' screen for 'Average Reading Speed'.

## 4.2.0 User Interactions

- The value is display-only; no user interaction is required with the metric itself.

## 4.3.0 Display Requirements

- The metric must be clearly labeled as 'Average Reading Speed'.
- The units 'pages/hour' (or similar abbreviation like 'PPH') must be displayed.
- The UI must gracefully handle the zero-data state as defined in AC-002.

## 4.4.0 Accessibility Needs

- The text for the label and value must support dynamic type scaling (OS-level font size settings).
- The color contrast between the text and its background must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The average reading speed calculation must only include reading sessions that have a non-zero value for both pages read and time spent.

### 5.1.3 Enforcement Point

During the data aggregation and calculation process, both on the client and server.

### 5.1.4 Violation Handling

Sessions not meeting the criteria are filtered out and excluded from the calculation.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The calculation formula is: (Total Pages Read) / (Total Time Spent in Hours).

### 5.2.3 Enforcement Point

Within the statistics calculation service/logic.

### 5.2.4 Violation Handling

N/A - This is a definitional rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-046

#### 6.1.1.2 Dependency Reason

This story requires the ability to log reading progress by page number to have data for the numerator.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-048

#### 6.1.2.2 Dependency Reason

This story requires the ability to log reading time to have data for the denominator.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-049

#### 6.1.3.2 Dependency Reason

This story requires the ability to log reading time to have data for the denominator.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-068

#### 6.1.4.2 Dependency Reason

This story implements a specific metric that will be displayed on the basic statistics screen created by US-068.

## 6.2.0.0 Technical Dependencies

- Availability of the `ReadingSession` data model in the local Isar database.
- A UI component or screen for displaying user statistics.

## 6.3.0.0 Data Dependencies

- User-generated `ReadingSession` data containing `pages_read` and `duration_minutes` fields.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The calculation of the average reading speed on the client device should complete in under 100ms, even with thousands of reading session records.

## 7.2.0.0 Security

- N/A for this specific feature, as it only involves displaying aggregated user-owned data.

## 7.3.0.0 Usability

- The metric should be presented clearly and be easily understandable to the user without requiring additional explanation.

## 7.4.0.0 Accessibility

- Adherence to WCAG 2.1 Level AA standards for text size and color contrast.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- The core calculation is simple arithmetic.
- Requires an efficient database query (likely on the local Isar DB) to aggregate total pages and total time from all relevant sessions.
- Logic must correctly filter out irrelevant sessions (e.g., those logged by percentage or with zero time).

## 8.3.0.0 Technical Risks

- Potential for minor performance issues if the data aggregation query is not optimized for a large number of session records.
- Ensuring the calculation logic is robust and handles all edge cases (like zero time) without crashing.

## 8.4.0.0 Integration Points

- Local Database (Isar): For fetching `ReadingSession` data.
- Statistics UI: For displaying the calculated value.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify calculation with a clean data set.
- Verify calculation with a mix of page-based and percentage-based sessions.
- Verify UI behavior when no valid session data exists.
- Verify UI behavior with sessions that have zero time or zero pages.
- Verify correct rounding and display format.

## 9.3.0.0 Test Data Needs

- Test accounts with no reading sessions.
- Test accounts with multiple valid reading sessions.
- Test accounts with a mix of valid sessions, percentage-based sessions, and sessions with zero duration.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests for the calculation logic implemented with >80% coverage
- Widget tests for the statistics display component are implemented and passing
- Integration testing with the local database is completed successfully
- User interface reviewed and approved for clarity and design consistency
- Performance of the calculation verified on a mid-range device with a large dataset
- Accessibility requirements (dynamic type, contrast) validated
- Story deployed and verified in the staging environment on both iOS and Android

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- This story is part of the initial 'Basic Statistics' feature set and should be prioritized alongside other core metrics.
- Dependent on the completion of reading session logging functionality.

## 11.4.0.0 Release Impact

- Contributes to the core value proposition of the app's tracking and analytics features for the initial release.

