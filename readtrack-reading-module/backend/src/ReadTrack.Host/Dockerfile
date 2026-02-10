# Base stage for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution config files
COPY ["backend/Directory.Build.props", "backend/"]
COPY ["backend/Directory.Packages.props", "backend/"]
COPY ["backend/global.json", "backend/"]

# Copy project files
COPY ["backend/src/ReadTrack.Host/ReadTrack.Host.csproj", "backend/src/ReadTrack.Host/"]
COPY ["backend/src/ReadTrack.Reading.Presentation/ReadTrack.Reading.Presentation.csproj", "backend/src/ReadTrack.Reading.Presentation/"]
COPY ["backend/src/ReadTrack.Reading.Infrastructure/ReadTrack.Reading.Infrastructure.csproj", "backend/src/ReadTrack.Reading.Infrastructure/"]
COPY ["backend/src/ReadTrack.Reading.Application/ReadTrack.Reading.Application.csproj", "backend/src/ReadTrack.Reading.Application/"]
COPY ["backend/src/ReadTrack.Reading.Domain/ReadTrack.Reading.Domain.csproj", "backend/src/ReadTrack.Reading.Domain/"]

# Restore dependencies
WORKDIR /src/backend
RUN dotnet restore "src/ReadTrack.Host/ReadTrack.Host.csproj"

# Copy source code
COPY ["backend/", "."]

# Build the application
WORKDIR "/src/backend/src/ReadTrack.Host"
RUN dotnet build "ReadTrack.Host.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ReadTrack.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadTrack.Host.dll"]