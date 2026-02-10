FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ReadTrack.Api/ReadTrack.Api.csproj", "src/ReadTrack.Api/"]
COPY ["src/ReadTrack.Shared.Contracts/ReadTrack.Shared.Contracts.csproj", "src/ReadTrack.Shared.Contracts/"]
COPY ["src/ReadTrack.Monetization.Presentation/ReadTrack.Monetization.Presentation.csproj", "src/ReadTrack.Monetization.Presentation/"]
COPY ["src/ReadTrack.Monetization.Application/ReadTrack.Monetization.Application.csproj", "src/ReadTrack.Monetization.Application/"]
COPY ["src/ReadTrack.Monetization.Domain/ReadTrack.Monetization.Domain.csproj", "src/ReadTrack.Monetization.Domain/"]
COPY ["src/ReadTrack.Monetization.Infrastructure/ReadTrack.Monetization.Infrastructure.csproj", "src/ReadTrack.Monetization.Infrastructure/"]
RUN dotnet restore "src/ReadTrack.Api/ReadTrack.Api.csproj"
COPY . .
WORKDIR "/src/src/ReadTrack.Api"
RUN dotnet build "ReadTrack.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ReadTrack.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadTrack.Api.dll"]