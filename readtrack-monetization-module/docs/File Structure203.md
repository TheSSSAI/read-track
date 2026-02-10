# 1 Dependency Levels

## 1.1 Level

### 1.1.1 Level

🔹 0

### 1.1.2 Files

- src/ReadTrack.Monetization.Domain/ValueObjects/SubscriptionTier.cs
- src/ReadTrack.Monetization.Domain/ValueObjects/SubscriptionStatus.cs
- src/ReadTrack.Monetization.Domain/Events/SubscriptionRenewedEvent.cs
- src/ReadTrack.Monetization.Domain/Events/SubscriptionTerminatedEvent.cs
- src/ReadTrack.Monetization.Domain/Exceptions/PaymentProviderException.cs
- src/ReadTrack.Monetization.Domain/Entities/WebhookAuditLog.cs
- src/ReadTrack.Monetization.Application/DTOs/SubscriptionStatusDto.cs
- src/ReadTrack.Monetization.Presentation/DTOs/AppleWebhookRequest.cs
- src/ReadTrack.Monetization.Presentation/DTOs/GoogleWebhookRequest.cs
- src/ReadTrack.Monetization.Infrastructure/Configuration/AppleStoreSettings.cs
- src/ReadTrack.Monetization.Infrastructure/Configuration/GooglePlaySettings.cs

## 1.2.0 Level

### 1.2.1 Level

🔹 1

### 1.2.2 Files

- src/ReadTrack.Monetization.Domain/Entities/Subscription.cs
- src/ReadTrack.Monetization.Domain/Entities/PaymentTransaction.cs
- src/ReadTrack.Monetization.Domain/Interfaces/ISubscriptionRepository.cs
- src/ReadTrack.Monetization.Application/Interfaces/IPaymentProviderService.cs
- src/ReadTrack.Monetization.Application/Interfaces/IPaymentProviderFactory.cs
- src/ReadTrack.Monetization.Application/Services/ISubscriptionService.cs
- src/ReadTrack.Monetization.Application/Commands/ProcessWebhook/ProcessWebhookCommand.cs
- src/ReadTrack.Monetization.Application/Queries/GetSubscriptionStatus/GetSubscriptionStatusQuery.cs

## 1.3.0 Level

### 1.3.1 Level

🔹 2

### 1.3.2 Files

- src/ReadTrack.Monetization.Infrastructure/Persistence/Configurations/SubscriptionConfiguration.cs
- src/ReadTrack.Monetization.Infrastructure/Persistence/MonetizationDbContext.cs

## 1.4.0 Level

### 1.4.1 Level

🔹 3

### 1.4.2 Files

- src/ReadTrack.Monetization.Infrastructure/Persistence/Repositories/SubscriptionRepository.cs
- src/ReadTrack.Monetization.Infrastructure/Services/AppleStoreClient.cs
- src/ReadTrack.Monetization.Infrastructure/Services/GooglePlayClient.cs
- src/ReadTrack.Monetization.Infrastructure/Services/PaymentProviderFactory.cs

## 1.5.0 Level

### 1.5.1 Level

🔹 4

### 1.5.2 Files

- src/ReadTrack.Monetization.Application/Commands/ProcessWebhook/ProcessWebhookCommandHandler.cs
- src/ReadTrack.Monetization.Application/Queries/GetSubscriptionStatus/GetSubscriptionStatusQueryHandler.cs
- src/ReadTrack.Monetization.Infrastructure/Jobs/SubscriptionExpirationJob.cs

## 1.6.0 Level

### 1.6.1 Level

🔹 5

### 1.6.2 Files

- src/ReadTrack.Monetization.Presentation/Controllers/WebhooksController.cs
- src/ReadTrack.Monetization.Presentation/Controllers/SubscriptionController.cs

## 1.7.0 Level

### 1.7.1 Level

🔹 6

### 1.7.2 Files

- .editorconfig
- .gitattributes
- .gitignore
- codecov.yml
- docker-compose.yml
- backend/Directory.Build.props
- backend/global.json
- backend/nuget.config
- backend/ReadTrack.Backend.sln
- backend/src/ReadTrack.Api/Dockerfile
- backend/src/ReadTrack.Api/appsettings.Testing.json
- backend/src/ReadTrack.Api/ReadTrack.Api.csproj
- backend/src/ReadTrack.Api/Properties/launchSettings.json
- backend/src/ReadTrack.Shared.Contracts/ReadTrack.Shared.Contracts.csproj
- .github/workflows/backend-ci.yml
- .github/workflows/mobile-ci.yml
- .github/workflows/infrastructure-cd.yml
- infrastructure/cdk.json
- infrastructure/package.json
- infrastructure/tsconfig.json
- mobile/analysis_options.yaml
- mobile/pubspec.yaml

# 2.0.0 Total Files

52

# 3.0.0 Generation Order

- 0
- 1
- 2
- 3
- 4
- 5
- 6

