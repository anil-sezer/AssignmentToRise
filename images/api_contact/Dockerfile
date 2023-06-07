FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base 

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

COPY ["src/Assignment.Domain/Assignment.Domain.csproj", "Assignment.Domain/"]
COPY ["src/Assignment.DataAccess/Assignment.DataAccess.csproj", "Assignment.DataAccess/"]
COPY ["src/Assignment.Application.Shared/Assignment.Application.Shared.csproj", "Assignment.Application.Shared/"]
COPY ["src/Assignment.Web.Core/Assignment.Web.Core.csproj", "Assignment.Web.Core/"]
COPY ["src/Assignment.Application.Contact/Assignment.Application.Contact.csproj", "Assignment.Application.Contact/"]
COPY ["src/Assignment.Web.Api/Assignment.Web.Api.csproj", "Assignment.Web.Api/"]


RUN dotnet restore "Assignment.Web.Api/Assignment.Web.Api.csproj" --verbosity quiet

COPY src/ .

RUN dotnet build "Assignment.Web.Api/Assignment.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "Assignment.Web.Api/Assignment.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Assignment.Web.Api.dll"]
