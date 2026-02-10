# Use the official ASP.NET Core runtime image for .NET 8.0
# This image contains the runtime needed to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET SDK image for building the application
# This image contains the compiler and tools
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution file and project configuration files
COPY ["ReadTrack.sln", "."]
COPY ["src/ReadTrack.Host/ReadTrack.Host.csproj", "src/ReadTrack.Host/"]
COPY ["Directory.Packages.props", "."]
COPY ["nuget.config", "."]

# Restore dependencies
# This layer is cached unless project files change
RUN dotnet restore "src/ReadTrack.Host/ReadTrack.Host.csproj"

# Copy the remaining source code
COPY . .
WORKDIR "/src/src/ReadTrack.Host"

# Build the application
RUN dotnet build "ReadTrack.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReadTrack.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadTrack.Host.dll"]