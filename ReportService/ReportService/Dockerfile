#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["ReportService/ReportService/ReportService.csproj", "ReportService/ReportService/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/EventBus/"]
COPY ["EventBus/BasicIntegrationEventService/BasicIntegrationEventService.csproj", "EventBus/BasicIntegrationEventService/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "ReportService/ReportService/ReportService.csproj"
COPY . .
WORKDIR "/src/ReportService/ReportService"
RUN dotnet build "ReportService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.dll"]
