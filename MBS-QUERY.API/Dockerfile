FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MBS-QUERY.API/MBS-QUERY.API.csproj", "MBS-QUERY.API/"]
COPY ["MBS_QUERY.Application/MBS_QUERY.Application.csproj", "MBS_QUERY.Application/"]
COPY ["MBS_QUERY.Contract/MBS_QUERY.Contract.csproj", "MBS_QUERY.Contract/"]
COPY ["MBS_QUERY.Domain/MBS_QUERY.Domain.csproj", "MBS_QUERY.Domain/"]
COPY ["MBS_QUERY.Persistence/MBS_QUERY.Persistence.csproj", "MBS_QUERY.Persistence/"]
COPY ["MBS_QUERY.Infrastructure/MBS_QUERY.Infrastructure.csproj", "MBS_QUERY.Infrastructure/"]
COPY ["MBS_QUERY.Presentation/MBS_QUERY.Presentation.csproj", "MBS_QUERY.Presentation/"]
RUN dotnet restore "MBS-QUERY.API/MBS-QUERY.API.csproj"
COPY . .
WORKDIR "/src/MBS-QUERY.API"
RUN dotnet build "MBS-QUERY.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MBS-QUERY.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MBS-QUERY.API.dll"]
