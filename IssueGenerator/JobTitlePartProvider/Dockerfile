#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["IssueGenerator/JobTitlePartProvider/JobTitlePartProvider.csproj", "IssueGenerator/JobTitlePartProvider/"]
RUN dotnet restore "IssueGenerator/JobTitlePartProvider/JobTitlePartProvider.csproj"
COPY . .
WORKDIR "/src/IssueGenerator/JobTitlePartProvider"
RUN dotnet build "JobTitlePartProvider.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobTitlePartProvider.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobTitlePartProvider.dll"]
