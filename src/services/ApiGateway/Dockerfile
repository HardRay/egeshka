FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["libraries/Core.Models", "src/libraries/Core.Models/"]
COPY ["libraries/Core.Proto", "src/libraries/Core.Proto/"]
COPY ["protos/auth/auth.proto", "src/protos/auth/auth.proto/"]
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