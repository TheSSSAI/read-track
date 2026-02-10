# 1 Dependency Levels

## 1.1 Level

### 1.1.1 Level

🔹 0

### 1.1.2 Files

- src/ReadTrack.Recommendations.Domain/Enums/RecommendationJobStatus.cs
- src/ReadTrack.Recommendations.Application/DTOs/RecommendationJobStatusDto.cs
- src/ReadTrack.Recommendations.Infrastructure/Configuration/OpenAIOptions.cs
- src/ReadTrack.Recommendations.Infrastructure/Configuration/OpenSearchOptions.cs
- src/ReadTrack.Recommendations.Application/Features/Recommendations/GenerateRecommendationsCommand.cs
- src/ReadTrack.Recommendations.Application/Features/Recommendations/GetRecommendationJobStatusQuery.cs
- src/ReadTrack.Recommendations.Application/Features/Recommendations/SubmitFeedbackCommand.cs

## 1.2.0 Level

### 1.2.1 Level

🔹 1

### 1.2.2 Files

- src/ReadTrack.Recommendations.Domain/Entities/Recommendation.cs
- src/ReadTrack.Recommendations.Domain/Entities/RecommendationFeedback.cs
- src/ReadTrack.Recommendations.Domain/Entities/UserContextEmbedding.cs
- src/ReadTrack.Recommendations.Domain/Entities/RecommendationJob.cs
- src/ReadTrack.Recommendations.Application/Interfaces/ILLMClient.cs
- src/ReadTrack.Recommendations.Application/Interfaces/IVectorStoreClient.cs
- src/ReadTrack.Recommendations.Application/Interfaces/IRecommendationRepository.cs
- src/ReadTrack.Recommendations.Application/Interfaces/IEmbeddingGenerator.cs
- src/ReadTrack.Recommendations.Application/Interfaces/IReadingHistoryProvider.cs
- src/ReadTrack.Recommendations.Application/Interfaces/Infrastructure/IPromptBuilder.cs
- src/ReadTrack.Recommendations.Application/Services/IRecommendationProcessService.cs

## 1.3.0 Level

### 1.3.1 Level

🔹 2

### 1.3.2 Files

- src/ReadTrack.Recommendations.Infrastructure/Persistence/RecommendationConfiguration.cs
- src/ReadTrack.Recommendations.Infrastructure/Persistence/RecommendationJobConfiguration.cs
- src/ReadTrack.Recommendations.Infrastructure/Persistence/RecommendationConfiguration.cs
- src/ReadTrack.Recommendations.Infrastructure/AI/PromptBuilder.cs
- src/ReadTrack.Recommendations.Infrastructure/AI/LlmResponseParser.cs
- src/ReadTrack.Recommendations.Infrastructure/AI/TokenBudgetManager.cs

## 1.4.0 Level

### 1.4.1 Level

🔹 3

### 1.4.2 Files

- src/ReadTrack.Recommendations.Infrastructure/Persistence/RecommendationsDbContext.cs
- src/ReadTrack.Recommendations.Infrastructure/AI/OpenAIClientAdapter.cs
- src/ReadTrack.Recommendations.Infrastructure/Persistence/OpenSearchClientAdapter.cs

## 1.5.0 Level

### 1.5.1 Level

🔹 4

### 1.5.2 Files

- src/ReadTrack.Recommendations.Infrastructure/Persistence/RecommendationRepository.cs
- src/ReadTrack.Recommendations.Application/Services/RecommendationProcessService.cs

## 1.6.0 Level

### 1.6.1 Level

🔹 5

### 1.6.2 Files

- src/ReadTrack.Recommendations.Application/Features/Recommendations/GenerateRecommendationsHandler.cs
- src/ReadTrack.Recommendations.Application/Features/Recommendations/GetRecommendationJobStatusHandler.cs

## 1.7.0 Level

### 1.7.1 Level

🔹 6

### 1.7.2 Files

- src/ReadTrack.Recommendations.Web/Controllers/RecommendationsController.cs
- src/ReadTrack.Recommendations.Infrastructure/DependencyInjection.cs
- src/ReadTrack.Recommendations.csproj

# 2.0.0 Total Files

34

# 3.0.0 Generation Order

- 0
- 1
- 2
- 3
- 4
- 5
- 6

