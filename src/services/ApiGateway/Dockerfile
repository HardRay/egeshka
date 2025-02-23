FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["libraries/Core.Domain", "src/libraries/Core.Domain/"]
COPY ["libraries/Core.Models", "src/libraries/Core.Models/"]

COPY ["protos/Proto.Common", "src/protos/Proto.Common/"]
COPY ["protos/Proto.Auth", "src/protos/Proto.Auth/"]
COPY ["protos/Proto.Progress", "src/protos/Proto.Progress/"]
COPY ["protos/Proto.Progress/Enums", "src/protos/Proto.Progress/Enums/"]

COPY ["services/ApiGateway", "src/services/ApiGateway/"]

RUN dotnet restore "src/services/ApiGateway/ApiGateway.csproj"
WORKDIR "src/services/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egeshka.ApiGateway.dll"]