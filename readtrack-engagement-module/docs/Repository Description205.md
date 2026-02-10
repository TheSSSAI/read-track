# 1 Id

REPO-BE-MOD-ENGAGEMENT

# 2 Name

readtrack-engagement-module

# 3 Description

This repository contains features designed to drive user engagement and habit formation. Extracted from `readtrack-backend-api`, its scope includes flexible goal setting and monitoring (REQ-GOL-001), daily reading tasks (REQ-TSK-001), the curated library of reading tips (REQ-TIP-001), and interactive vocabulary-building games (REQ-VOC-001). This module is highly reactive; it primarily listens to events from the Reading module (e.g., 'ReadingSessionLogged') to automatically update goal progress. Separating engagement features allows for rapid iteration and experimentation with new habit-forming mechanics without impacting the core stability of the reading tracking or monetization systems.

# 4 Type

🔹 Business Logic

# 5 Namespace

ReadTrack.Engagement

# 6 Output Path

solution/backend/modules/engagement

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

ASP.NET Core 8, Entity Framework Core 8

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- application
- domain
- infrastructure

# 12 Dependencies

- REPO-BE-LIB-CONTRACTS
- REPO-BE-LIB-INFRA
- REPO-BE-MOD-MONETIZATION

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-GOL-001

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-TSK-001

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-TIP-001

## 13.4.0 Requirement Id

### 13.4.1 Requirement Id

REQ-VOC-001

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Clean Architecture Slice

# 17.0.0 Architecture Map

- application-layer-010

# 18.0.0 Components Map

- backend-goals-controller-002

# 19.0.0 Requirements Map

- REQ-GOL-001

# 20.0.0 Decomposition Rationale

## 20.1.0 Operation Type

NEW_DECOMPOSED

## 20.2.0 Source Repository

REPO-BE-API

## 20.3.0 Decomposition Reasoning

Engagement features (goals, tasks, games) often have a different lifecycle than core utilities. They are subject to more frequent changes, A/B testing, and iteration. Isolating them in their own module allows the product team to experiment with these features without creating risk for the core reading tracking or payment systems.

## 20.4.0 Extracted Responsibilities

- Reading Goal Management
- Daily Task Creation and Tracking
- Vocabulary List and Game Logic
- Fetching and Caching Reading Tips from a CMS

## 20.5.0 Reusability Scope

- The goal-setting engine could be generalized for other habit-tracking applications.

## 20.6.0 Development Benefits

- Enables rapid iteration on user engagement mechanics.
- Decouples feature logic from core data management.
- Contains dependencies like the Headless CMS client to a single module.

# 21.0.0 Dependency Contracts

*No data available*

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'IGoalService', 'methods': ['GetUserGoalsAsync(Guid userId) : List<GoalProgressDto>', 'CreateGoalAsync(Guid userId, CreateGoalRequest request) : Result<GoalDto>'], 'events': ['GoalAchieved(Guid userId, Guid goalId)'], 'properties': [], 'consumers': []}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Exposes services like `IGoalService` and `IVocabul... |
| Event Communication | Crucially, it acts as a primary consumer of the `R... |
| Data Flow | Owns the `Goal` and `VocabularyWord` tables. Reads... |
| Error Handling | Gracefully handles unavailability of the external ... |
| Async Patterns | Event handlers that update goals must be asynchron... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Implement event handlers for `ReadingSessionLogged... |
| Performance Considerations | Goal progress calculation should be efficient. Cac... |
| Security Considerations | Authorization rules must ensure users can only man... |
| Testing Approach | Unit test the logic for goal progress updates base... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- All business logic for goals, tasks, tips, and vocabulary games.
- Event handlers to update state based on user reading activity.

## 25.2.0 Must Not Implement

- Logging of the reading sessions themselves.
- Management of user subscriptions.
- User profile management.

## 25.3.0 Extension Points

- Adding new types of goals (e.g., 'reading streak' goal).
- Adding new vocabulary games.

## 25.4.0 Validation Rules

- Enforce limits on active goals for Free Users.
- Validate vocabulary word entries.

