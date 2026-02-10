# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-BE-MOD-RECOMMENDATIONS |
| Extraction Timestamp | 2025-01-27T15:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | Production-Ready |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-AIS-001

#### 1.2.1.2 Requirement Text

The system shall generate and display personalized book recommendations to the user by processing their data via the OpenAI GPT-4 API.

#### 1.2.1.3 Validation Criteria

- Backend must construct prompts including reading history, goals, and vocabulary
- Recommendation generation must be asynchronous, showing a loading state in the UI
- LLM API calls must be rate-limited

#### 1.2.1.4 Implementation Implications

- Implement Retrieval-Augmented Generation (RAG) using Amazon OpenSearch for context
- Use background job processing (Hangfire) to handle long-running LLM inference
- Implement rate limiting logic based on user subscription tier

#### 1.2.1.5 Extraction Reasoning

Core functional requirement driving the module's architecture.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-FRE-001

#### 1.2.2.2 Requirement Text

Enforce freemium model limits (Free users limited to 5 AI suggestions/month).

#### 1.2.2.3 Validation Criteria

- Check user tier before initiating recommendation generation
- Increment usage counter upon successful generation
- Block request if limit exceeded

#### 1.2.2.4 Implementation Implications

- Dependency on Monetization Module to fetch subscription status
- Tracking of usage counts within the Recommendations module

#### 1.2.2.5 Extraction Reasoning

Determines the access control logic for the recommendation feature.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-REL-002

#### 1.2.3.2 Requirement Text

The system must implement resilience patterns to handle failures gracefully when communicating with critical external APIs.

#### 1.2.3.3 Validation Criteria

- Circuit breaker pattern for OpenAI API calls
- Retry policies with backoff for transient failures

#### 1.2.3.4 Implementation Implications

- Configure Polly pipelines in the Infrastructure layer for OpenAIClientAdapter and OpenSearchClientAdapter

#### 1.2.3.5 Extraction Reasoning

Critical for stability given the reliance on volatile external AI services.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

RecommendationProcessService

#### 1.3.1.2 Component Specification

Orchestrates the asynchronous RAG pipeline: context retrieval, prompt engineering, LLM inference, and result parsing.

#### 1.3.1.3 Implementation Requirements

- Aggregate data from Reading Module and OpenSearch
- Execute OpenAI chat completion
- Parse JSON response into domain entities
- Update Job status

#### 1.3.1.4 Architectural Context

Application Layer - Domain Service

#### 1.3.1.5 Extraction Reasoning

The central coordinator for the AI logic.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

OpenAIClientAdapter

#### 1.3.2.2 Component Specification

Infrastructure wrapper for the OpenAI-DotNet library implementing resilience policies.

#### 1.3.2.3 Implementation Requirements

- Inject API Key via Options pattern
- Wrap calls with Polly policies (Retry, Circuit Breaker)
- Log token usage metrics

#### 1.3.2.4 Architectural Context

Infrastructure Layer - External Adapter

#### 1.3.2.5 Extraction Reasoning

Isolates the external LLM dependency.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

OpenSearchClientAdapter

#### 1.3.3.2 Component Specification

Infrastructure wrapper for Amazon OpenSearch Serverless for vector similarity search.

#### 1.3.3.3 Implementation Requirements

- Perform k-NN searches for user context
- Manage connection pooling and authentication (SigV4)

#### 1.3.3.4 Architectural Context

Infrastructure Layer - Vector Store Adapter

#### 1.3.3.5 Extraction Reasoning

Required for the Retrieval phase of RAG.

## 1.4.0.0 Architectural Layers

### 1.4.1.0 Layer Name

#### 1.4.1.1 Layer Name

Application

#### 1.4.1.2 Layer Responsibilities

Defines use cases (CQRS Commands/Queries), interfaces for infrastructure, and orchestrates domain logic.

#### 1.4.1.3 Layer Constraints

- No direct dependency on OpenAI or OpenSearch SDKs
- Must use MediatR for internal decoupling

#### 1.4.1.4 Implementation Patterns

- CQRS
- Mediator

#### 1.4.1.5 Extraction Reasoning

Standard Clean Architecture layer.

### 1.4.2.0 Layer Name

#### 1.4.2.1 Layer Name

Infrastructure

#### 1.4.2.2 Layer Responsibilities

Implements interfaces defined in Application layer, handling external API communication and persistence.

#### 1.4.2.3 Layer Constraints

- Must implement resilience patterns (Polly)
- Must handle configuration injection securely

#### 1.4.2.4 Implementation Patterns

- Adapter
- Repository

#### 1.4.2.5 Extraction Reasoning

Encapsulates heavy dependencies (OpenAI, OpenSearch).

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IReadingHistoryProvider

#### 1.5.1.2 Source Repository

REPO-BE-MOD-READING

#### 1.5.1.3 Method Contracts

- {'method_name': 'GetUserReadingHistoryForEmbeddingsAsync', 'method_signature': 'Task<List<BookData>> GetUserReadingHistoryForEmbeddingsAsync(Guid userId, CancellationToken cancellationToken)', 'method_purpose': 'Retrieves recent reading history to construct the context window for the LLM.', 'integration_context': 'Called by RecommendationProcessService during the context building phase.'}

#### 1.5.1.4 Integration Pattern

In-Process Module Call (DI)

#### 1.5.1.5 Communication Protocol

Direct Method Invocation

#### 1.5.1.6 Extraction Reasoning

The AI needs source data owned by the Reading Module.

### 1.5.2.0 Interface Name

#### 1.5.2.1 Interface Name

IUserSubscriptionService

#### 1.5.2.2 Source Repository

REPO-BE-MOD-MONETIZATION

#### 1.5.2.3 Method Contracts

- {'method_name': 'GetUserSubscriptionStatusAsync', 'method_signature': 'Task<SubscriptionStatusDto> GetUserSubscriptionStatusAsync(Guid userId, CancellationToken cancellationToken)', 'method_purpose': "Retrieves the user's current subscription tier to enforce rate limits (5/month for Free users).", 'integration_context': 'Called by GenerateRecommendationsHandler before accepting the request.'}

#### 1.5.2.4 Integration Pattern

In-Process Module Call (DI)

#### 1.5.2.5 Communication Protocol

Direct Method Invocation

#### 1.5.2.6 Extraction Reasoning

Required to enforce REQ-FRE-001 usage limits.

### 1.5.3.0 Interface Name

#### 1.5.3.1 Interface Name

IBackgroundJobService

#### 1.5.3.2 Source Repository

REPO-BE-LIB-INFRA

#### 1.5.3.3 Method Contracts

- {'method_name': 'Enqueue', 'method_signature': 'string Enqueue(Expression<Action> methodCall)', 'method_purpose': 'Offloads the long-running recommendation generation process to a background worker (Hangfire).', 'integration_context': 'Called by GenerateRecommendationsHandler to schedule the job.'}

#### 1.5.3.4 Integration Pattern

Shared Kernel Abstraction

#### 1.5.3.5 Communication Protocol

In-Process / Persistence

#### 1.5.3.6 Extraction Reasoning

Recommendations take 10-30s; async processing is mandatory to avoid HTTP timeouts.

## 1.6.0.0 Exposed Interfaces

- {'interface_name': 'IRecommendationService', 'consumer_repositories': ['REPO-BE-HOST'], 'method_contracts': [{'method_name': 'GenerateRecommendationsAsync', 'method_signature': 'Task<Result<Guid>> GenerateRecommendationsAsync(Guid userId)', 'method_purpose': 'Initiates a background job to generate recommendations. Returns the Job ID for polling.', 'implementation_requirements': 'Must check subscription limits before queuing.'}, {'method_name': 'GetJobStatusAsync', 'method_signature': 'Task<RecommendationJobStatusDto> GetJobStatusAsync(Guid jobId)', 'method_purpose': 'Allows client to poll for the completion status and results of the generation job.', 'implementation_requirements': 'Must return status (Pending/Processing/Completed/Failed) and results if completed.'}], 'service_level_requirements': ['Job acceptance response < 200ms', 'Job completion typically < 60s'], 'implementation_constraints': ['Rate limited based on user tier'], 'extraction_reasoning': 'The Host (API Gateway) needs these entry points to expose functionality to the mobile client.'}

## 1.7.0.0 Technology Context

### 1.7.1.0 Framework Requirements

.NET 8, ASP.NET Core 8

### 1.7.2.0 Integration Technologies

- OpenAI-DotNet (LLM Client)
- OpenSearch.Client (Vector Store)
- Polly (Resilience)
- MediatR (Internal Messaging)
- Hangfire (Background Jobs)

### 1.7.3.0 Performance Constraints

LLM calls are high latency. System must use async/await and background processing to maintain responsiveness.

### 1.7.4.0 Security Requirements

OpenAI API Keys must be stored in AWS Secrets Manager and accessed via IOptions. No PII should be sent to LLM unless necessary.

## 1.8.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | Verified all dependencies (Reading, Monetization, ... |
| Cross Reference Validation | Validated against REQ-AIS-001 (AI), REQ-FRE-001 (L... |
| Implementation Readiness Assessment | High. Interfaces and patterns are fully specified. |
| Quality Assurance Confirmation | Integration design adheres to Modular Monolith pri... |

