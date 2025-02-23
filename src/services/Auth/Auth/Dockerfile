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
COPY ["protos/Proto.Auth", "src/protos/Proto.Auth/"]

COPY ["services/Auth/Auth", "src/services/Auth/Auth/"]
COPY ["services/Auth/Auth.Domain", "src/services/Auth/Auth.Domain/"]
COPY ["services/Auth/Auth.Application", "src/services/Auth/Auth.Application/"]
COPY ["services/Auth/Auth.Infrastructure", "src/services/Auth/Auth.Infrastructure/"]

RUN dotnet restore "src/services/Auth/Auth/Auth.csproj"
WORKDIR "src/services/Auth/Auth"
RUN dotnet build "Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egeshka.Auth.dll"]