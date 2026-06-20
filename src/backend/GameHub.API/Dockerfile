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

# 1. Copy solution and package configuration props to fix NU1015 missing version error
COPY ["*.sln", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"] 

# 2. Copy ALL project structure files to satisfy project references during restore
COPY ["GameHub.API/GameHub.API.csproj", "GameHub.API/"]
COPY ["GameHub.Application/GameHub.Application.csproj", "GameHub.Application/"]
COPY ["GameHub.Infrastructure/GameHub.Persistence/GameHub.Persistence.csproj", "GameHub.Infrastructure/GameHub.Persistence/"]

# 3. Safely run the restore step now that all files are visible to the compiler
RUN dotnet restore "./GameHub.API/GameHub.API.csproj"

# 4. Copy the remaining source files and build
COPY . .
WORKDIR "/src/GameHub.API"
RUN dotnet build "./GameHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/build


# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GameHub.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameHub.API.dll"]