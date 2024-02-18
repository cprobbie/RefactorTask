FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/RefactorThis.Api/RefactorThis.Api.csproj", "src/RefactorThis.Api/"]
COPY ["src/RefactorThis.Core/RefactorThis.Core.csproj", "src/RefactorThis.Core/"]
COPY ["src/RefactorThis.Infrastructure/RefactorThis.Infrastructure.csproj", "src/RefactorThis.Infrastructure/"]
RUN dotnet restore "src/RefactorThis.Api/RefactorThis.Api.csproj"
COPY . .
WORKDIR "/src/src/RefactorThis.Api"
RUN dotnet build "RefactorThis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RefactorThis.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RefactorThis.Api.dll"]


# Docker CMD at root directory
# docker build -t refactorthis_api .
# docker run --rm -p 5000:8080 -e ASPNETCORE_URLS=http://+:8080 refactorthis_api
# curl http://localhost:5000/ping