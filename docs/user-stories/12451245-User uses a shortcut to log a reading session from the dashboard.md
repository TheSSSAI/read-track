# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-058 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User uses a shortcut to log a reading session from... |
| As A User Story | As a frequent user, I want a prominent shortcut on... |
| User Persona | Any active user (Free or Premium) who is tracking ... |
| Business Value | Increases user engagement and retention by streaml... |
| Functional Area | Dashboard & Reading Tracking |
| Story Theme | Core User Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Shortcut with a single 'Currently Reading' book

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

the user is logged in and on the Dashboard screen

### 3.1.5 When

the user has exactly one book on their 'Currently Reading' shelf and taps the 'Log Session' shortcut

### 3.1.6 Then

the application navigates directly to the 'Log Session' screen for that specific book.

### 3.1.7 Validation Notes

Verify that no intermediate selection screen is shown and the correct book is pre-selected on the logging screen.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Shortcut with multiple 'Currently Reading' books

### 3.2.3 Scenario Type

Alternative_Flow

### 3.2.4 Given

the user is logged in and on the Dashboard screen

### 3.2.5 When

the user has two or more books on their 'Currently Reading' shelf and taps the 'Log Session' shortcut

### 3.2.6 Then

the application must display a modal or a selection screen listing all books from the 'Currently Reading' shelf.

### 3.2.7 Validation Notes

Verify the modal appears, lists all correct books, and is dismissible. After selecting a book, verify navigation to the correct 'Log Session' screen.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Shortcut with no 'Currently Reading' books

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the user is logged in and on the Dashboard screen

### 3.3.5 When

the user has zero books on their 'Currently Reading' shelf and taps the 'Log Session' shortcut

### 3.3.6 Then

the application must display a non-intrusive prompt or dialog explaining that a book must be added to 'Currently Reading' first.

### 3.3.7 Validation Notes

Verify the prompt appears and contains a clear call-to-action button that navigates the user to the book search or library screen.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

UI prominence and accessibility of the shortcut

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the user is on the Dashboard screen

### 3.4.5 When

the screen is rendered

### 3.4.6 Then

a prominent, easily tappable shortcut (e.g., a Floating Action Button) is visible.

### 3.4.7 Validation Notes

Verify the shortcut has a sufficient tap target size (min 44x44dp), good color contrast, and an intuitive icon, meeting WCAG 2.1 AA standards.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A Floating Action Button (FAB) or similarly prominent button on the Dashboard.
- A modal/dialog for selecting a book when multiple items are in 'Currently Reading'.
- A prompt/dialog for the case where no books are in 'Currently Reading'.

## 4.2.0 User Interactions

- Tapping the shortcut initiates the logging flow.
- If the selection modal appears, tapping a book proceeds to the logging screen for that book.
- Tapping outside the selection modal or on a close button dismisses it.
- Tapping the call-to-action in the 'no books' prompt navigates the user away from the dashboard.

## 4.3.0 Display Requirements

- The shortcut's icon should clearly represent adding a log entry (e.g., a plus icon, a book with a plus).
- The book selection modal must display the cover and title of each 'Currently Reading' book.

## 4.4.0 Accessibility Needs

- The shortcut button must have a content description for screen readers (e.g., 'Log reading session').
- The book selection modal must be navigable using accessibility services.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "The log session shortcut flow is determined by the number of items on the 'Currently Reading' shelf.", 'enforcement_point': 'Client-side, upon tapping the shortcut button on the Dashboard.', 'violation_handling': 'N/A - This is a flow-control rule.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-056

#### 6.1.1.2 Dependency Reason

The dashboard must be able to display 'Currently Reading' books to determine the shortcut's behavior.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

The core functionality of having a 'Currently Reading' shelf must exist.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-046

#### 6.1.3.2 Dependency Reason

The destination 'Log Session' screen, which this shortcut navigates to, must be implemented.

## 6.2.0.0 Technical Dependencies

- State management solution (Riverpod) to provide the list of 'Currently Reading' books.
- Application navigation/routing system.
- Local database (Isar) to query the user's library.

## 6.3.0.0 Data Dependencies

- Access to the user's local library data, specifically the 'Currently Reading' shelf.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The UI response to tapping the shortcut (navigation or modal display) must be under 200ms.

## 7.2.0.0 Security

*No items available*

## 7.3.0.0 Usability

- The shortcut's placement should follow platform-specific UI/UX conventions (e.g., bottom-right for a FAB).
- The flow should require the minimum number of taps to achieve the goal.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards as per REQ-UIF-001.

## 7.5.0.0 Compatibility

- The shortcut and subsequent modals must function correctly on all supported iOS and Android versions (REQ-OPE-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Conditional logic is required to handle three distinct user states (0, 1, or >1 'Currently Reading' books).
- A new, reusable UI component for book selection (modal/dialog) may need to be created.

## 8.3.0.0 Technical Risks

- Potential for state management complexity if the 'Currently Reading' list is not efficiently provided to the dashboard widget.

## 8.4.0.0 Integration Points

- Dashboard screen UI.
- State management provider for the user's library.
- Application's navigation service.
- Log Session screen.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget
- Integration
- Accessibility

## 9.2.0.0 Test Scenarios

- Tap shortcut with 0 books in 'Currently Reading'.
- Tap shortcut with 1 book in 'Currently Reading'.
- Tap shortcut with 3 books in 'Currently Reading', select the second book, and verify navigation.
- Verify UI with screen reader enabled.
- Verify UI with largest dynamic font size.

## 9.3.0.0 Test Data Needs

- Test user accounts with 0, 1, and >1 books on the 'Currently Reading' shelf.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test for end-to-end tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented and passing with >= 80% coverage for new logic
- Integration testing completed successfully for all three scenarios (0, 1, >1 books)
- User interface reviewed and approved by UX/UI designer
- Performance requirements verified on mid-range test devices
- Accessibility requirements validated using platform tools (e.g., VoiceOver, TalkBack)
- Documentation updated appropriately
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

2

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a high-value UX improvement for the core application loop and should be prioritized early.
- Depends on the completion of the basic dashboard and logging screens.

## 11.4.0.0 Release Impact

- Significantly improves the usability of the application's primary feature.

