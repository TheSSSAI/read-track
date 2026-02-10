# Base stage for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution and project files first to leverage Docker layer caching
COPY ["backend/ReadTrack.sln", "backend/"]
COPY ["backend/Directory.Build.props", "backend/"]
COPY ["backend/global.json", "backend/"]
COPY ["backend/src/ReadTrack.Host/ReadTrack.Host.csproj", "backend/src/ReadTrack.Host/"]
COPY ["backend/src/ReadTrack.Shared.Infrastructure/ReadTrack.Shared.Infrastructure.csproj", "backend/src/ReadTrack.Shared.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "backend/src/ReadTrack.Host/ReadTrack.Host.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/backend/src/ReadTrack.Host"
RUN dotnet build "ReadTrack.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReadTrack.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadTrack.Host.dll"]