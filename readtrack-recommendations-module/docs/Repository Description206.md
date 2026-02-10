# 1 Id

REPO-BE-MOD-RECOMMENDATIONS

# 2 Name

readtrack-recommendations-module

# 3 Description

This is a highly specialized module, decomposed from `readtrack-backend-api`, dedicated exclusively to generating AI-powered book and content recommendations (REQ-AIS-001). It encapsulates the complex and resource-intensive logic of interacting with a third-party LLM (OpenAI GPT-4) and managing vector embeddings in a dedicated vector store (Amazon OpenSearch). By isolating this functionality, we contain its unique, heavy dependencies (OpenAI client, OpenSearch client), specialized configuration, and higher operational costs. This separation is critical for independent scaling, performance tuning, and cost management of the AI features without impacting the core application's real-time performance.

# 4 Type

🔹 Business Logic

# 5 Namespace

ReadTrack.Recommendations

# 6 Output Path

solution/backend/modules/recommendations

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

ASP.NET Core 8

# 10 Thirdparty Libraries

- OpenAI-DotNet
- OpenSearch.Client

# 11 Layer Ids

- application
- domain
- infrastructure

# 12 Dependencies

- REPO-BE-LIB-CONTRACTS
- REPO-BE-LIB-INFRA
- REPO-BE-MOD-MONETIZATION

# 13 Requirements

- {'requirementId': 'REQ-AIS-001'}

# 14 Generate Tests

✅ Yes

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Clean Architecture Slice

# 17 Architecture Map

- application-layer-010

# 18 Components Map

- recommendation-generation-service-013
- openai-client-014
- backend-recommendations-controller-003

# 19 Requirements Map

- REQ-AIS-001

# 20 Decomposition Rationale

## 20.1 Operation Type

NEW_DECOMPOSED

## 20.2 Source Repository

REPO-BE-API

## 20.3 Decomposition Reasoning

AI/LLM integration is a fundamentally different type of workload compared to the rest of the application's CRUD operations. It is computationally expensive, relies on volatile external APIs, and has significant cost implications. Isolating it into a separate module is a crucial architectural decision for stability, cost control, and independent evolution. This module could even be deployed as a separate microservice in the future if needed.

## 20.4 Extracted Responsibilities

- Generating vector embeddings from user reading history
- Interacting with Amazon OpenSearch for similarity search
- Constructing detailed prompts for the LLM
- Calling the OpenAI GPT-4 API and parsing its response
- Managing rate limiting and cost controls for AI services

## 20.5 Reusability Scope

- The pattern of using embeddings and LLMs could be reused, but the implementation is highly specific to the reading domain.

## 20.6 Development Benefits

- Isolates heavy and expensive dependencies (OpenAI SDK) from the main application.
- Allows a specialized team (e.g., ML/AI engineers) to own the component.
- Enables independent scaling and optimization of the recommendation engine.

# 21.0 Dependency Contracts

## 21.1 Repo-Be-Mod-Reading

### 21.1.1 Required Interfaces

- {'interface': 'IReadingHistoryProvider', 'methods': ['GetUserReadingHistoryForEmbeddingsAsync(Guid userId) : List<BookData>'], 'events': [], 'properties': []}

### 21.1.2 Integration Pattern

Event-driven data sync or direct service call

### 21.1.3 Communication Protocol

In-process

# 22.0.0 Exposed Contracts

## 22.1.0 Public Interfaces

- {'interface': 'IRecommendationService', 'methods': ['GenerateRecommendationsAsync(Guid userId) : List<RecommendationDto>'], 'events': [], 'properties': [], 'consumers': ['REPO-BE-HOST']}

# 23.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Consumes services from other modules (e.g., to get... |
| Event Communication | Could listen to `ReadingSessionLogged` to trigger ... |
| Data Flow | Reads data from the Reading module, processes it, ... |
| Error Handling | Heavy use of resilience patterns (Retry, Circuit B... |
| Async Patterns | The entire recommendation generation process must ... |

# 24.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Implement a dedicated client for OpenAI with Polly... |
| Performance Considerations | The primary bottleneck will be the latency of the ... |
| Security Considerations | API keys for OpenAI must be stored securely (e.g.,... |
| Testing Approach | Focus on integration tests that mock the OpenAI an... |

# 25.0.0 Scope Boundaries

## 25.1.0 Must Implement

- All logic related to generating, storing, and providing feedback on AI recommendations.

## 25.2.0 Must Not Implement

- Core user data management.
- Reading session logging.
- Any UI logic.

## 25.3.0 Extension Points

- Integrating with different LLM providers.
- Experimenting with different prompt engineering techniques.

## 25.4.0 Validation Rules

- Enforce the monthly suggestion limit for Free Users before making an API call.
- Validate and sanitize the response from the LLM.

