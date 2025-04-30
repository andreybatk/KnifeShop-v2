# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KnifeShop.API/KnifeShop.API.csproj", "KnifeShop.API/"]
COPY ["KnifeShop.Contracts/KnifeShop.Contracts.csproj", "KnifeShop.Contracts/"]
COPY ["KnifeShop.BL/KnifeShop.BL.csproj", "KnifeShop.BL/"]
COPY ["KnifeShop.DB/KnifeShop.DB.csproj", "KnifeShop.DB/"]
RUN dotnet restore "./KnifeShop.API/KnifeShop.API.csproj"
COPY . .
WORKDIR "/src/KnifeShop.API"
RUN dotnet build "./KnifeShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KnifeShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnifeShop.API.dll"]