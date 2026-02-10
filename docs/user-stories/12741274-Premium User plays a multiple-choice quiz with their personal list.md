# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-087 |
| Elaboration Date | 2024-10-27 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Premium User plays a multiple-choice quiz with the... |
| As A User Story | As a Premium User, I want to play a multiple-choic... |
| User Persona | Premium User: A subscribed user who is actively tr... |
| Business Value | Increases the value proposition of the Premium sub... |
| Functional Area | Vocabulary Building |
| Story Theme | Interactive Learning Games (Premium) |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Starting a quiz with sufficient words

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a Premium User with 10 or more words in my personal vocabulary list

### 3.1.5 When

I navigate to the 'Vocabulary Games' section and select 'Multiple-Choice Quiz'

### 3.1.6 Then

A new quiz session starts, displaying the first question and a progress indicator of '1/10'.

### 3.1.7 Validation Notes

Verify that the quiz starts and the question is populated from the user's personal list.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Quiz question structure

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

A quiz question is displayed

### 3.2.5 When

I view the question

### 3.2.6 Then

It must show one word from my personal list and four unique, tappable definition options.

### 3.2.7 And

The other three definitions (distractors) must be from other words in my personal list.

### 3.2.8 Validation Notes

Inspect the question data to ensure the correct definition and three unique distractors are present and randomized.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Answering a question correctly

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am on a quiz question

### 3.3.5 When

I tap the correct definition

### 3.3.6 Then

The selected option provides immediate positive feedback (e.g., green highlight and a checkmark icon).

### 3.3.7 And

After a 1-second delay, the quiz automatically advances to the next question.

### 3.3.8 Validation Notes

Verify the visual feedback and the automatic progression to the next question.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Answering a question incorrectly

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am on a quiz question

### 3.4.5 When

I tap an incorrect definition

### 3.4.6 Then

The selected option provides immediate negative feedback (e.g., red highlight and an 'X' icon).

### 3.4.7 And

After a 2-second delay to allow for review, the quiz automatically advances to the next question.

### 3.4.8 Validation Notes

Verify that both the incorrect and correct answers are highlighted and the delay is correct before advancing.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Quiz completion and results screen

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I have just answered the final question of the quiz

### 3.5.5 When

The quiz session ends

### 3.5.6 Then

I am navigated to a results screen that displays my final score (e.g., '8/10 Correct').

### 3.5.7 And

The screen provides options to 'Play Again' or 'Return to Games'.

### 3.5.8 Validation Notes

Verify the score is calculated correctly and the incorrect words are listed. Test the navigation buttons.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempting to play with insufficient words

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am a Premium User with fewer than 4 words in my personal vocabulary list

### 3.6.5 When

I navigate to the 'Vocabulary Games' section

### 3.6.6 Then

The 'Multiple-Choice Quiz' option is disabled or greyed out.

### 3.6.7 And

A message is displayed explaining that at least 4 words are required to play the quiz.

### 3.6.8 Validation Notes

Test with 0, 1, 2, and 3 words in the list. Verify the button is not tappable and the informational message is clear.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Playing a quiz with a small word list

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

I am a Premium User with 6 words in my personal vocabulary list

### 3.7.5 When

I start a 'Multiple-Choice Quiz'

### 3.7.6 Then

The quiz length is adjusted to match my word count, consisting of 6 questions.

### 3.7.7 And

The progress indicator shows 'X/6'.

### 3.7.8 Validation Notes

Test with a word count between 4 and 9. Verify the quiz length adapts correctly.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Playing the quiz offline

### 3.8.3 Scenario Type

Alternative_Flow

### 3.8.4 Given

I am a Premium User with a locally cached vocabulary list

### 3.8.5 And

The results are displayed, and the performance data is queued for synchronization when connectivity is restored.

### 3.8.6 When

I start and complete a 'Multiple-Choice Quiz'

### 3.8.7 Then

The quiz functions correctly using the local data.

### 3.8.8 Validation Notes

Enable airplane mode and perform the full quiz flow. Reconnect and verify data syncs for US-088.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Button to start 'Multiple-Choice Quiz'
- Quiz view with a prominent word display area
- Four tappable answer option buttons
- Progress indicator (e.g., 'Question 3/10')
- Results screen with score, list of missed words, 'Play Again' button, and 'Return' button

## 4.2.0 User Interactions

- Tapping an answer option triggers immediate visual feedback.
- The quiz flow is automatic, advancing to the next question after a short, timed delay.
- The results screen buttons navigate the user to the appropriate next step.

## 4.3.0 Display Requirements

- Text must be clear and legible, respecting the device's font size settings.
- Feedback colors (e.g., green for correct, red for incorrect) must be distinct.

## 4.4.0 Accessibility Needs

- Color-based feedback must be supplemented with icons (e.g., checkmark, 'X') for color-blind users (WCAG 2.1 AA).
- All interactive elements must have a minimum tap target size of 44x44dp.
- The app must support dynamic type scaling for all text.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

A user must have a minimum of 4 words in their personal vocabulary list to play the multiple-choice quiz.

### 5.1.3 Enforcement Point

Client-side, on the 'Vocabulary Games' screen.

### 5.1.4 Violation Handling

The quiz option is disabled, and an informational message is displayed to the user.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The standard quiz length is 10 questions. If the user has fewer than 10 words (but at least 4), the quiz length is equal to the number of words in their list.

### 5.2.3 Enforcement Point

Client-side, when the quiz session is initiated.

### 5.2.4 Violation Handling

N/A - This is a rule for adapting the quiz logic.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

- {'story_id': 'US-085', 'dependency_reason': 'This story is critically dependent on the ability for a Premium User to create and populate a personal vocabulary list. The quiz cannot function without a source of words.'}

## 6.2.0 Technical Dependencies

- Flutter framework with Riverpod for state management (REQ-CON-001).
- Isar local database for offline storage of the vocabulary list (REQ-OFF-001).

## 6.3.0 Data Dependencies

- Requires access to the user's personal `VocabularyWord` entities stored in the local Isar database.

## 6.4.0 External Dependencies

*No items available*

# 7.0.0 Non Functional Requirements

## 7.1.0 Performance

- The quiz screen must load in under 500ms.
- Feedback after answering a question must appear instantly (<100ms).

## 7.2.0 Security

- N/A - All data and logic are client-side and pertain to the user's own content.

## 7.3.0 Usability

- The quiz interaction must be intuitive, requiring no prior instruction.
- The flow should be smooth and engaging to encourage repeated play.

## 7.4.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards, particularly regarding color contrast and non-color-based indicators.

## 7.5.0 Compatibility

- The feature must be fully functional on all supported iOS and Android versions as defined in REQ-OPE-001.

# 8.0.0 Implementation Considerations

## 8.1.0 Complexity Assessment

Medium

## 8.2.0 Complexity Factors

- Developing the quiz generation logic, including the random selection of a word and three unique 'distractor' definitions.
- Managing the quiz session state (current question, score, words already asked) using Riverpod.
- Implementing a polished and responsive UI with animations for user feedback.
- Handling edge cases like small list sizes and offline mode gracefully.

## 8.3.0 Technical Risks

- The algorithm for selecting unique distractors could be inefficient on very large vocabulary lists, though this is a low risk for typical use cases.
- Ensuring smooth animations and transitions across a wide range of supported devices.

## 8.4.0 Integration Points

- Reads data from the local Isar database populated by the feature in US-085.
- Writes performance data to the local database to be consumed by the feature in US-088.

# 9.0.0 Testing Requirements

## 9.1.0 Testing Types

- Unit
- Widget
- Integration
- Accessibility

## 9.2.0 Test Scenarios

- Test quiz generation with exactly 4 words.
- Test quiz generation with 7 words.
- Test quiz generation with 20+ words.
- Complete a full 10-question quiz with a mix of correct and incorrect answers.
- Start a quiz and navigate away mid-session to ensure progress is not saved.
- Test the entire flow in offline mode.

## 9.3.0 Test Data Needs

- Test accounts with vocabulary lists of varying sizes: 0, 3, 4, 7, and 20+ words.

## 9.4.0 Testing Tools

- flutter_test for unit and widget tests.
- integration_test package for end-to-end testing.

# 10.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and widget tests implemented with >80% coverage for the new logic
- Integration testing completed successfully for the full quiz flow
- User interface reviewed and approved for both light and dark themes
- Performance requirements verified on mid-range test devices
- Accessibility audit passed (WCAG 2.1 AA)
- Quiz completion data is correctly structured and stored for use by US-088
- Story deployed and verified in staging environment

# 11.0.0 Planning Information

## 11.1.0 Story Points

5

## 11.2.0 Priority

🔴 High

## 11.3.0 Sprint Considerations

- This is a core premium feature and a high priority for demonstrating subscription value.
- Must be scheduled in a sprint after US-085 is completed.
- Can be developed in parallel with US-086 (Flashcards).

## 11.4.0 Release Impact

- This feature is part of the initial 'Premium' feature set and is required for the v1.0 launch.

