FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["libraries/Core.Domain", "src/libraries/Core.Domain/"]
COPY ["libraries/Core.Models", "src/libraries/Core.Models/"]
COPY ["libraries/Core.Application", "src/libraries/Core.Application/"]
COPY ["libraries/Core.Hosting", "src/libraries/Core.Hosting/"]

COPY ["protos/Proto.Common", "src/protos/Proto.Common/"]
COPY ["protos/Proto.Progress", "src/protos/Proto.Progress/"]

COPY ["services/Progress/Progress.Hosting", "src/services/Progress/Progress.Hosting/"]
COPY ["services/Progress/Progress.Domain", "src/services/Progress/Progress.Domain/"]
COPY ["services/Progress/Progress.Application", "src/services/Progress/Progress.Application/"]
COPY ["services/Progress/Progress.Infrastructure", "src/services/Progress/Progress.Infrastructure/"]

RUN dotnet restore "src/services/Progress/Progress.Hosting/Progress.Hosting.csproj"
WORKDIR "src/services/Progress/Progress.Hosting"
RUN dotnet build "Progress.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Progress.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egeshka.Progress.Hosting.dll"]