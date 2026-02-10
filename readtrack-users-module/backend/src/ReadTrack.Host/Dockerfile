FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["backend/src/ReadTrack.Host/ReadTrack.Host.csproj", "src/ReadTrack.Host/"]
COPY ["backend/src/ReadTrack.Users/ReadTrack.Users.csproj", "src/ReadTrack.Users/"]
RUN dotnet restore "./src/ReadTrack.Host/ReadTrack.Host.csproj"
COPY backend/ .
WORKDIR "/src/src/ReadTrack.Host"
RUN dotnet build "./ReadTrack.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReadTrack.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadTrack.Host.dll"]