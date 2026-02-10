# Base stage for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution config files
COPY ["Directory.Build.props", "./"]
COPY ["global.json", "./"]

# Copy project files for restore
# Note: These paths are relative to the build context which should be the backend/ root folder
COPY ["src/ReadTrack.Host/ReadTrack.Host.csproj", "src/ReadTrack.Host/"]
COPY ["src/ReadTrack.Shared.Contracts/ReadTrack.Shared.Contracts.csproj", "src/ReadTrack.Shared.Contracts/"]
COPY ["src/Modules/ReadTrack.Modules.Users/ReadTrack.Modules.Users.csproj", "src/Modules/ReadTrack.Modules.Users/"]
COPY ["src/Modules/ReadTrack.Modules.Reading/ReadTrack.Modules.Reading.csproj", "src/Modules/ReadTrack.Modules.Reading/"]
COPY ["src/Modules/ReadTrack.Modules.Engagement/ReadTrack.Modules.Engagement.csproj", "src/Modules/ReadTrack.Modules.Engagement/"]
COPY ["src/Modules/ReadTrack.Modules.Monetization/ReadTrack.Modules.Monetization.csproj", "src/Modules/ReadTrack.Modules.Monetization/"]
COPY ["src/Modules/ReadTrack.Modules.AI/ReadTrack.Modules.AI.csproj", "src/Modules/ReadTrack.Modules.AI/"]

# Restore dependencies
RUN dotnet restore "src/ReadTrack.Host/ReadTrack.Host.csproj"

# Copy full source code
COPY . .

# Build the project
WORKDIR "/src/src/ReadTrack.Host"
RUN dotnet build "ReadTrack.Host.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ReadTrack.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadTrack.Host.dll"]