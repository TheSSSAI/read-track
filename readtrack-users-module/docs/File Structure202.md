# 1 Dependency Levels

## 1.1 Level

### 1.1.1 Level

🔹 0

### 1.1.2 Files

- src/ReadTrack.Users/Domain/Enums/DataExportStatus.cs
- src/ReadTrack.Users/Application/DTOs/SocialLoginRequest.cs
- src/ReadTrack.Users/Application/DTOs/UserProfileDto.cs
- .gitignore
- .editorconfig
- backend/.editorconfig

## 1.2.0 Level

### 1.2.1 Level

🔹 1

### 1.2.2 Files

- src/ReadTrack.Users/Domain/Entities/User.cs
- src/ReadTrack.Users/Domain/Entities/DataExportJob.cs
- src/ReadTrack.Users/Application/Interfaces/IFileStorageService.cs
- src/ReadTrack.Users/Application/Interfaces/IEmailService.cs
- src/ReadTrack.Users/Infrastructure/Configuration/Auth0Settings.cs

## 1.3.0 Level

### 1.3.1 Level

🔹 2

### 1.3.2 Files

- src/ReadTrack.Users/Application/Features/Auth/Commands/SocialLoginCommand.cs
- src/ReadTrack.Users/Application/Features/Auth/Commands/RefreshTokenCommand.cs
- src/ReadTrack.Users/Application/Features/Users/Commands/UpdateUserProfileCommand.cs
- src/ReadTrack.Users/Application/Features/Users/Commands/DeleteAccountCommand.cs
- src/ReadTrack.Users/Application/Features/Users/Commands/RequestDataExportCommand.cs
- src/ReadTrack.Users/Application/Features/Users/Queries/GetUserProfileQuery.cs

## 1.4.0 Level

### 1.4.1 Level

🔹 3

### 1.4.2 Files

- src/ReadTrack.Users/Application/Features/Auth/Commands/SocialLoginCommandHandler.cs
- src/ReadTrack.Users/Application/Features/Users/Commands/DeleteAccountCommandHandler.cs
- src/ReadTrack.Users/Infrastructure/Persistence/UserConfiguration.cs
- src/ReadTrack.Users/Infrastructure/Persistence/DataExportJobConfiguration.cs

## 1.5.0 Level

### 1.5.1 Level

🔹 4

### 1.5.2 Files

- src/ReadTrack.Users/Infrastructure/Persistence/UsersDbContext.cs
- src/ReadTrack.Users/Infrastructure/Services/S3FileStorageService.cs
- src/ReadTrack.Users/Infrastructure/Services/SesEmailService.cs
- src/ReadTrack.Users/Infrastructure/Services/Auth0TokenValidationService.cs

## 1.6.0 Level

### 1.6.1 Level

🔹 5

### 1.6.2 Files

- src/ReadTrack.Users/Presentation/Controllers/AuthController.cs
- src/ReadTrack.Users/Presentation/Controllers/UsersController.cs

## 1.7.0 Level

### 1.7.1 Level

🔹 6

### 1.7.2 Files

- backend/src/ReadTrack.Host/appsettings.Development.json
- backend/src/ReadTrack.Host/Dockerfile
- backend/Directory.Build.props
- backend/ReadTrack.sln
- backend/docker-compose.yml
- backend/global.json
- backend/xunit.runner.json
- infrastructure/cdk.json
- infrastructure/jest.config.js
- infrastructure/package.json
- mobile/analysis_options.yaml
- mobile/build.yaml
- mobile/pubspec.yaml
- .github/workflows/backend-ci.yml
- .github/workflows/mobile-ci.yml
- .vscode/launch.json

# 2.0.0 Total Files

43

# 3.0.0 Generation Order

- 0
- 1
- 2
- 3
- 4
- 5
- 6

