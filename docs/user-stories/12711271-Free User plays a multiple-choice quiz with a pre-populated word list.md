# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-084 |
| Elaboration Date | 2025-01-18 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Free User plays a multiple-choice quiz with a pre-... |
| As A User Story | As a Free User, I want to play a multiple-choice q... |
| User Persona | Free User (unsubscribed, subject to feature limita... |
| Business Value | Increases engagement and retention for Free Users ... |
| Functional Area | Vocabulary Building |
| Story Theme | Gamified Learning Experience |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: User starts, plays, and successfully completes a quiz

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Free User and I am on the 'Vocabulary Games' screen

### 3.1.5 When

I tap the 'Multiple-Choice Quiz' option

### 3.1.6 Then



```
A new quiz session begins, fetching words from the centrally managed pre-populated list from the Headless CMS.
The first question is displayed, showing one word and four unique definition options, one of which is correct.
My current progress (e.g., 'Question 1 of 10') is visible.
```

### 3.1.7 Validation Notes

Verify the quiz starts and the first question is rendered correctly. The word list must be from the Free Tier source in Contentful.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User selects the correct answer

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am playing a multiple-choice quiz and a question is displayed

### 3.2.5 When

I tap on the correct definition option

### 3.2.6 Then



```
The selected option is immediately highlighted in green to indicate it is correct.
All other options become un-tappable.
A 'Next' button appears to proceed to the next question.
```

### 3.2.7 Validation Notes

Test that visual feedback is immediate and correct, and that the state correctly transitions to allow moving to the next question.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

User selects an incorrect answer

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am playing a multiple-choice quiz and a question is displayed

### 3.3.5 When

I tap on an incorrect definition option

### 3.3.6 Then



```
The selected option is immediately highlighted in red to indicate it is incorrect.
The correct definition option is simultaneously highlighted in green.
All options become un-tappable.
A 'Next' button appears to proceed to the next question.
```

### 3.3.7 Validation Notes

Verify that both the incorrect selection and the correct answer are highlighted simultaneously to provide a learning opportunity.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User completes the final question of the quiz

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am on the final question of a quiz (e.g., 10 of 10)

### 3.4.5 When

I select an answer and tap the 'Finish' button (which replaces 'Next')

### 3.4.6 Then



```
I am navigated to a quiz summary screen.
The summary screen displays my final score (e.g., '8/10 Correct').
The summary screen provides a 'Play Again' button and a 'Return to Games' button.
```

### 3.4.7 Validation Notes

Ensure the transition to the summary screen is smooth and the score calculation is accurate.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

User's quiz performance is saved

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I have just completed a quiz and viewed the summary screen

### 3.5.5 When

I navigate away from the summary screen

### 3.5.6 Then

The result of the quiz (score, date/time) is saved to my performance history, as per REQ-VOC-001.

### 3.5.7 Validation Notes

Check the local Isar database or the 'Performance History' screen (covered in US-088) to confirm the new record was created.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Error Condition: Word list fails to load from CMS

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am a Free User on the 'Vocabulary Games' screen

### 3.6.5 When

I tap 'Multiple-Choice Quiz' and the app cannot fetch the word list from Contentful (e.g., no network connection)

### 3.6.6 Then



```
A user-friendly error message is displayed, such as 'Could not load quiz. Please check your connection and try again.'
The quiz does not start.
```

### 3.6.7 Validation Notes

Simulate a network failure or an empty response from the CMS API to test this error handling.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Edge Case: User attempts to leave a quiz mid-session

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am in the middle of a quiz session (e.g., on question 4 of 10)

### 3.7.5 When

I tap the system back button or navigate to another tab

### 3.7.6 Then



```
A confirmation dialog appears with the message 'Are you sure you want to quit? Your progress will be lost.' and options 'Quit' and 'Cancel'.
If I tap 'Cancel', the dialog closes and I remain in the quiz.
If I tap 'Quit', I am returned to the 'Vocabulary Games' screen and the current quiz session data is discarded.
```

### 3.7.7 Validation Notes

Test both confirmation options to ensure correct navigation and state handling.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Button to start 'Multiple-Choice Quiz'
- Question display area (for the word)
- Four tappable buttons for definition options
- Progress indicator (e.g., 'Question X of Y')
- 'Next' / 'Finish' button
- Quiz summary screen with score display
- 'Play Again' button
- 'Return to Games' button
- Confirmation dialog for quitting

## 4.2.0 User Interactions

- User taps a game mode to start.
- User taps one of four options to answer a question.
- The UI provides immediate visual feedback (color change) for correct/incorrect answers.
- User taps 'Next' to advance or 'Finish' to complete the quiz.

## 4.3.0 Display Requirements

- The word being quizzed must be clearly displayed.
- The definition options must be legible and distinct.
- The final score must be prominently displayed on the summary screen.

## 4.4.0 Accessibility Needs

- Feedback colors for correct (green) and incorrect (red) answers must have sufficient contrast and be supplemented with icons (e.g., checkmark, 'X') for color-blind users (WCAG 2.1).
- All interactive elements must have adequate tap target sizes.
- Text must respect OS-level dynamic type settings (REQ-UIF-001).

# 5.0.0 Business Rules

- {'rule_id': 'BR-VOC-001', 'rule_description': 'Free Users must only use the pre-populated word list for vocabulary games.', 'enforcement_point': 'When the quiz session is initiated.', 'violation_handling': 'The system will only ever fetch data from the designated Free Tier word list in the Headless CMS. There is no path for a Free User to access a personal list.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-105

#### 6.1.1.2 Dependency Reason

This story cannot be implemented until the pre-populated word lists are created and structured in the Headless CMS (Contentful) by a Content Manager.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-088

#### 6.1.2.2 Dependency Reason

This story generates performance data that will be displayed by US-088 ('User views their vocabulary game performance history'). The data model for history should be coordinated between these stories.

## 6.2.0.0 Technical Dependencies

- A functional integration with the Contentful API to fetch content (REQ-SIF-001).
- The Isar local database must be set up in the project to persist performance history (REQ-OFF-001).

## 6.3.0.0 Data Dependencies

- A defined and approved data schema for vocabulary words in Contentful, including the word, the correct definition, and any metadata.
- A defined strategy for generating the three incorrect 'distractor' definitions for each question.

## 6.4.0.0 External Dependencies

- Availability of the Contentful Delivery API.

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The transition between questions, including feedback animation, should complete in under 300ms.
- The initial loading of the quiz, including the API call to Contentful, should complete in under 2 seconds on a standard 4G connection.

## 7.2.0.0 Security

- All communication with the Contentful API must be over HTTPS.

## 7.3.0.0 Usability

- The quiz should be intuitive, requiring no external instructions to play.
- Feedback must be clear and immediate to facilitate learning.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards, particularly regarding color contrast and non-color-based indicators.

## 7.5.0.0 Compatibility

- The feature must function correctly on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The logic for generating plausible but incorrect 'distractor' definitions is a key challenge. The simplest approach is randomly selecting definitions from other words in the fetched list, but this may produce obvious non-answers.
- State management for the quiz session (current question index, running score, user answers) needs to be robust, especially handling app lifecycle events (e.g., app going to background).
- Requires careful parsing and error handling for the data fetched from the external CMS.

## 8.3.0.0 Technical Risks

- The Contentful API may be slow or unavailable, requiring robust error handling and user feedback.
- Poorly chosen distractor definitions could make the quiz too easy or nonsensical, degrading the user experience.

## 8.4.0.0 Integration Points

- Headless CMS (Contentful) for fetching the word list.
- Local Database (Isar) for writing performance history.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Widget (Flutter)
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Complete a full quiz with a perfect score.
- Complete a full quiz with a zero score.
- Answer a mix of correct and incorrect questions.
- Start a quiz with network disabled to test error handling.
- Quit a quiz midway and confirm progress is not saved.
- Verify that the words presented are randomized and not in the same order each time.

## 9.3.0.0 Test Data Needs

- Access to a staging environment in Contentful with a populated list of at least 20 vocabulary words to ensure sufficient data for a quiz and for generating distractors.

## 9.4.0.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for E2E tests.
- A tool for mocking HTTP responses from the Contentful API.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >= 80% code coverage for new logic
- Integration testing with a mocked CMS endpoint completed successfully
- User interface reviewed and approved by the design team for both light and dark modes
- Performance requirements verified on target devices
- Accessibility checks (voice-over, dynamic type) have been performed
- Documentation for the quiz state management logic is created
- Story deployed and verified in the staging environment using data from the staging CMS

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a core part of the free user experience and a key engagement feature.
- Requires coordination with the content team to ensure the prerequisite data (US-105) is available in Contentful before the sprint begins.

## 11.4.0.0 Release Impact

- This feature is part of the initial MVP launch and is critical for demonstrating the value of the application's learning tools to new users.

