# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/Apps/SpotifyClone.Api/SpotifyClone.Api.csproj", "src/Apps/SpotifyClone.Api/"]
COPY ["src/Modules/Accounts/SpotifyClone.Accounts.Application/SpotifyClone.Accounts.Application.csproj", "src/Modules/Accounts/SpotifyClone.Accounts.Application/"]
COPY ["src/Shared/BuildingBlocks/SpotifyClone.Shared.BuildingBlocks.Application/SpotifyClone.Shared.BuildingBlocks.Application.csproj", "src/Shared/BuildingBlocks/SpotifyClone.Shared.BuildingBlocks.Application/"]
COPY ["src/Shared/BuildingBlocks/SpotifyClone.Shared.BuildingBlocks.Domain/SpotifyClone.Shared.BuildingBlocks.Domain.csproj", "src/Shared/BuildingBlocks/SpotifyClone.Shared.BuildingBlocks.Domain/"]
COPY ["src/Shared/Kernel/SpotifyClone.Shared.Kernel/SpotifyClone.Shared.Kernel.csproj", "src/Shared/Kernel/SpotifyClone.Shared.Kernel/"]
COPY ["src/Modules/Accounts/SpotifyClone.Accounts.Domain/SpotifyClone.Accounts.Domain.csproj", "src/Modules/Accounts/SpotifyClone.Accounts.Domain/"]
COPY ["src/Modules/Accounts/SpotifyClone.Accounts.Infrastructure/SpotifyClone.Accounts.Infrastructure.csproj", "src/Modules/Accounts/SpotifyClone.Accounts.Infrastructure/"]
COPY ["src/Shared/BuildingBlocks/SpotifyClone.Shared.BuildingBlocks.Infrastructure/SpotifyClone.Shared.BuildingBlocks.Infrastructure.csproj", "src/Shared/BuildingBlocks/SpotifyClone.Shared.BuildingBlocks.Infrastructure/"]
RUN dotnet restore "./src/Apps/SpotifyClone.Api/SpotifyClone.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/SpotifyClone.Api"
RUN dotnet build "./SpotifyClone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotifyClone.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotifyClone.Api.dll"]