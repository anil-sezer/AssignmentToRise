FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["src/Assignment.Domain/Assignment.Domain.csproj", "Assignment.Domain/"]
COPY ["src/Assignment.DataAccess/Assignment.DataAccess.csproj", "Assignment.DataAccess/"]
COPY ["src/Assignment.WorkerService.Report/Assignment.WorkerService.Report.csproj", "Assignment.WorkerService.Report/"]

RUN dotnet restore "Assignment.WorkerService.Report/Assignment.WorkerService.Report.csproj"

COPY src/ .

RUN dotnet build "Assignment.WorkerService.Report/Assignment.WorkerService.Report.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Assignment.WorkerService.Report/Assignment.WorkerService.Report.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Assignment.WorkerService.Report.dll"]
