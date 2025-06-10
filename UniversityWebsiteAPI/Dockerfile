# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY UserService.Api/UserService.Api.csproj ./UserService.Api/
COPY UserService.Application/UserService.Application.csproj ./UserService.Application/
COPY UserService.Infrastructure/UserService.Infrastructure.csproj ./UserService.Infrastructure/
COPY UserService.Core/UserService.Core.csproj ./UserService.Core/

RUN dotnet restore ./UserService.Api/UserService.Api.csproj

COPY . .

WORKDIR /src/UserService.Api
RUN dotnet build "UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикация
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]


