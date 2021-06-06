#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["IssueGenerator/IssueGenerator/IssueGenerator.csproj", "IssueGenerator/IssueGenerator/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/EventBus/"]
COPY ["EventBus/BasicIntegrationEventService/BasicIntegrationEventService.csproj", "EventBus/BasicIntegrationEventService/"]
COPY ["Helpers/HealthChecksHelper/HealthChecksHelper.csproj", "Helpers/HealthChecksHelper/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "IssueGenerator/IssueGenerator/IssueGenerator.csproj"
COPY . .
WORKDIR "/src/IssueGenerator/IssueGenerator"
RUN dotnet build "IssueGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IssueGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IssueGenerator.dll"]
