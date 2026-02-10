# 1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-ENGAGEMENT |
| Extraction Timestamp | 2025-01-27T12:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

# 2 Relevant Requirements

## 2.1 Requirement Id

### 2.1.1 Requirement Id

REQ-GOL-001

### 2.1.2 Requirement Text

The system shall enable users to create, edit, and delete personalized reading goals based on type (books, pages, time), target, and period (day, week, month, year).

### 2.1.3 Validation Criteria

- Users must select goal type: number of books, number of pages, or amount of time.
- System must automatically update goal progress after each reading session is logged via event consumption.
- Free Users must be limited to one active goal (REQ-BR-FRE-002).

### 2.1.4 Implementation Implications

- Implement Goal entity with fields for TargetValue, CurrentValue, Type, Frequency, and Start/End dates.
- Create UpdateGoalProgressOnReadingSessionLoggedHandler to consume ReadingSessionLogged events and increment progress.
- Implement validation logic in CreateGoalCommand to check User Role and existing active goal count.

### 2.1.5 Extraction Reasoning

Core engagement feature mapped directly to US-060 through US-065 and Sequence Diagram 451.

## 2.2.0 Requirement Id

### 2.2.1 Requirement Id

REQ-TSK-001

### 2.2.2 Requirement Text

The system shall allow users to configure recurring daily tasks and receive suggestions based on active goals.

### 2.2.3 Validation Criteria

- Users must be able to create custom daily tasks with recurrence (e.g., every Mon/Wed).
- System must suggest tasks based on goal gaps (e.g., 'Read 20 mins' to meet weekly goal).
- Task completion must reset or regenerate daily.

### 2.2.4 Implementation Implications

- Implement DailyTask entity with RecurrencePattern (e.g., bitmask or cron-like) and TaskCompletion history table.
- Implement logic to generate 'Suggested Tasks' by analyzing Goal progress vs. time remaining.
- Implement a daily background job or lazy-loading logic to instantiation daily task instances.

### 2.2.5 Extraction Reasoning

Mapped to US-075, US-076, US-077, and US-078; drives daily user retention.

## 2.3.0 Requirement Id

### 2.3.1 Requirement Id

REQ-TIP-001

### 2.3.2 Requirement Text

The system shall serve curated reading tips from a Headless CMS with offline support and resilience.

### 2.3.3 Validation Criteria

- Content must be fetched from Contentful API.
- Circuit breaker must be implemented for CMS availability (REQ-REL-002).
- Content must be cached (Redis/In-Memory) to serve stale data during outages.

### 2.3.4 Implementation Implications

- Implement ICmsClient using HttpClient wrapped with Polly policies (Retry, Circuit Breaker).
- Implement Cache-Aside pattern using Redis for retrieved Tips.
- Map CMS JSON response to internal ReadingTip DTOs.

### 2.3.5 Extraction Reasoning

Mapped to US-079 through US-082 and Sequence Diagram 467; ensures content delivery reliability.

## 2.4.0 Requirement Id

### 2.4.1 Requirement Id

REQ-VOC-001

### 2.4.2 Requirement Text

The system shall provide vocabulary building features, differentiating between Free (pre-populated) and Premium (personal) lists.

### 2.4.3 Validation Criteria

- Premium users can add custom words (REQ-BR-VOC-001).
- Free users access only system-provided word lists.
- Duplicate words must be prevented (REQ-BR-VOC-003).

### 2.4.4 Implementation Implications

- Implement VocabularyItem entity linked to UserId.
- Implement permission check in AddVocabularyWordCommand to verify Premium role.
- Implement IVocabularyService that switches data source (CMS vs. DB) based on user tier.

### 2.4.5 Extraction Reasoning

Mapped to US-083 through US-088; supports monetization strategy via feature gating.

# 3.0.0 Relevant Components

## 3.1.0 Component Name

### 3.1.1 Component Name

GoalsController

### 3.1.2 Component Specification

ASP.NET Core Controller exposing Goal management endpoints.

### 3.1.3 Implementation Requirements

- Endpoints: GET /goals, POST /goals, PUT /goals/{id}, DELETE /goals/{id}
- Use MediatR to dispatch CreateGoalCommand, UpdateGoalCommand, etc.
- Authorize requests based on User ownership.

### 3.1.4 Architectural Context

Presentation Layer - Application Entry Point

### 3.1.5 Extraction Reasoning

Primary entry point for Goal features.

## 3.2.0 Component Name

### 3.2.1 Component Name

UpdateGoalProgressHandler

### 3.2.2 Component Specification

Domain Event Handler responsible for reactive goal updates.

### 3.2.3 Implementation Requirements

- Implement INotificationHandler<ReadingSessionLoggedEvent>.
- Logic: Find active goals matching the session timeframe -> Calculate delta -> Update CurrentValue -> Save.
- Trigger GoalAchievedEvent if target reached.

### 3.2.4 Architectural Context

Application Layer - Event Processing

### 3.2.5 Extraction Reasoning

Critical component for the reactive architecture defined in Sequence Diagram 451.

## 3.3.0 Component Name

### 3.3.1 Component Name

ContentfulCmsClient

### 3.3.2 Component Specification

Infrastructure service for fetching content from Contentful.

### 3.3.3 Implementation Requirements

- Implement ITipRepository.
- Use Polly PolicyWrap (Retry + Circuit Breaker).
- Integrate with IDistributedCache (Redis) for response caching.

### 3.3.4 Architectural Context

Infrastructure Layer - External Integration

### 3.3.5 Extraction Reasoning

Required to satisfy REQ-TIP-001 and REQ-REL-002.

# 4.0.0 Architectural Layers

## 4.1.0 Layer Name

### 4.1.1 Layer Name

Domain

### 4.1.2 Layer Responsibilities

Defines entities (Goal, Task, Vocabulary), value objects, and domain events.

### 4.1.3 Layer Constraints

- Must be independent of EF Core and HTTP context.
- Must encapsulate business rules (e.g., Goal validity).

### 4.1.4 Implementation Patterns

- Rich Domain Models
- Domain Events

### 4.1.5 Extraction Reasoning

Core business logic center.

## 4.2.0 Layer Name

### 4.2.1 Layer Name

Application

### 4.2.2 Layer Responsibilities

Orchestrates use cases (CQRS Commands/Queries) and event handling.

### 4.2.3 Layer Constraints

- Depends only on Domain and Abstractions.
- Validation logic using FluentValidation.

### 4.2.4 Implementation Patterns

- CQRS (MediatR)
- Mediator Pattern

### 4.2.5 Extraction Reasoning

Execution context for business use cases.

## 4.3.0 Layer Name

### 4.3.1 Layer Name

Infrastructure

### 4.3.2 Layer Responsibilities

Implements interfaces for Database, Caching, and External APIs.

### 4.3.3 Layer Constraints

- Specific dependency on EF Core 8 and Contentful SDK.
- Handles database migrations for Engagement tables.

### 4.3.4 Implementation Patterns

- Repository Pattern
- Adapter Pattern

### 4.3.5 Extraction Reasoning

Physical implementation of data access and integrations.

# 5.0.0 Dependency Interfaces

## 5.1.0 Interface Name

### 5.1.1 Interface Name

ReadingSessionLoggedEvent

### 5.1.2 Source Repository

REPO-BE-MOD-READING

### 5.1.3 Method Contracts

- {'method_name': 'Handle', 'method_signature': 'Task Handle(ReadingSessionLoggedEvent notification, CancellationToken cancellationToken)', 'method_purpose': 'Updates goal progress based on the logged session details (pages, duration).', 'integration_context': 'Asynchronous In-Process Event (MediatR)'}

### 5.1.4 Integration Pattern

Event-Driven Architecture

### 5.1.5 Communication Protocol

In-Process (MediatR)

### 5.1.6 Extraction Reasoning

Crucial dependency for automating goal tracking as per Sequence Diagram 451.

## 5.2.0 Interface Name

### 5.2.1 Interface Name

IUserSubscriptionService

### 5.2.2 Source Repository

REPO-BE-MOD-MONETIZATION

### 5.2.3 Method Contracts

- {'method_name': 'GetUserSubscriptionTierAsync', 'method_signature': 'Task<SubscriptionTier> GetUserSubscriptionTierAsync(Guid userId)', 'method_purpose': 'Retrieves user tier to enforce limits (e.g., max 1 goal for Free users).', 'integration_context': 'Called during CreateGoal and AddVocabularyWord command handling.'}

### 5.2.4 Integration Pattern

Service/Module Interface Call

### 5.2.5 Communication Protocol

In-Process Method Call

### 5.2.6 Extraction Reasoning

Required to enforce REQ-BR-FRE-002 and REQ-BR-VOC-001.

# 6.0.0 Exposed Interfaces

## 6.1.0 Interface Name

### 6.1.1 Interface Name

IGoalService

### 6.1.2 Consumer Repositories

- REPO-BE-API

### 6.1.3 Method Contracts

#### 6.1.3.1 Method Name

##### 6.1.3.1.1 Method Name

GetUserGoalsAsync

##### 6.1.3.1.2 Method Signature

Task<List<GoalDto>> GetUserGoalsAsync(Guid userId)

##### 6.1.3.1.3 Method Purpose

Returns active goals and their progress for the dashboard.

##### 6.1.3.1.4 Implementation Requirements

Optimize for read performance (potential caching).

#### 6.1.3.2.0 Method Name

##### 6.1.3.2.1 Method Name

CreateGoalAsync

##### 6.1.3.2.2 Method Signature

Task<Result<GoalDto>> CreateGoalAsync(Guid userId, CreateGoalRequest request)

##### 6.1.3.2.3 Method Purpose

Creates a new goal with validation.

##### 6.1.3.2.4 Implementation Requirements

Must validate User Tier limits.

### 6.1.4.0.0 Service Level Requirements

- API response time < 200ms (P95)
- High availability for Dashboard rendering

### 6.1.5.0.0 Implementation Constraints

- Must return structured DTOs
- Must handle database exceptions gracefully

### 6.1.6.0.0 Extraction Reasoning

Primary interface for the Client application to interact with Goals.

## 6.2.0.0.0 Interface Name

### 6.2.1.0.0 Interface Name

GoalAchievedEvent

### 6.2.2.0.0 Consumer Repositories

- REPO-BE-MOD-NOTIFICATIONS

### 6.2.3.0.0 Method Contracts

- {'method_name': 'Publish', 'method_signature': 'Task Publish(GoalAchievedEvent notification)', 'method_purpose': 'Notifies other modules when a goal is completed.', 'implementation_requirements': 'Publish via MediatR'}

### 6.2.4.0.0 Service Level Requirements

- Reliable delivery

### 6.2.5.0.0 Implementation Constraints

- Event payload must contain UserID and GoalID

### 6.2.6.0.0 Extraction Reasoning

Enables gamification and notification features.

# 7.0.0.0.0 Technology Context

## 7.1.0.0.0 Framework Requirements

.NET 8, ASP.NET Core 8

## 7.2.0.0.0 Integration Technologies

- MediatR (Event Bus)
- Entity Framework Core 8 (Persistence)
- Polly (Resilience)
- StackExchange.Redis (Caching)

## 7.3.0.0.0 Performance Constraints

Goal calculation logic must be optimized to not degrade 'Log Session' performance. P95 < 200ms.

## 7.4.0.0.0 Security Requirements

OAuth2/JWT for API access. User ID validation in all Command Handlers (Ownership check).

# 8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All user stories (Goals, Tasks, Tips, Vocabulary) ... |
| Cross Reference Validation | Validated against Sequence 451 (Goal Updates), 467... |
| Implementation Readiness Assessment | Architectural patterns (CQRS, Repository), Technol... |
| Quality Assurance Confirmation | Systematic analysis confirms all 4 feature areas (... |

