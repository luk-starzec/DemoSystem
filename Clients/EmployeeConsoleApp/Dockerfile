#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Clients/EmployeeConsoleApp/EmployeeConsoleApp.csproj", "Clients/EmployeeConsoleApp/"]
COPY ["EventBus/EventBus/EventBus.csproj", "EventBus/EventBus/"]
COPY ["EventBus/BasicIntegrationEventService/BasicIntegrationEventService.csproj", "EventBus/BasicIntegrationEventService/"]
COPY ["EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "Clients/EmployeeConsoleApp/EmployeeConsoleApp.csproj"
COPY . .
WORKDIR "/src/Clients/EmployeeConsoleApp"
RUN dotnet build "EmployeeConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeConsoleApp.dll"]
