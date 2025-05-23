# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PurrfectTrack.Api/PurrfectTrack.Api.csproj", "PurrfectTrack.Api/"]
COPY ["PurrfectTrack.Application/PurrfectTrack.Application.csproj", "PurrfectTrack.Application/"]
COPY ["PurrfectTrack.Domain/PurrfectTrack.Domain.csproj", "PurrfectTrack.Domain/"]
COPY ["PurrfectTrack.Shared/PurrfectTrack.Shared.csproj", "PurrfectTrack.Shared/"]
COPY ["PurrfectTrack.Infrastructure/PurrfectTrack.Infrastructure.csproj", "PurrfectTrack.Infrastructure/"]
RUN dotnet restore "./PurrfectTrack.Api/PurrfectTrack.Api.csproj"
COPY . .
WORKDIR "/src/PurrfectTrack.Api"
RUN dotnet build "./PurrfectTrack.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PurrfectTrack.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PurrfectTrack.Api.dll"]