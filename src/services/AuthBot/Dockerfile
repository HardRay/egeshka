FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["protos/Proto.Auth", "src/protos/Proto.Auth/"]
COPY ["protos/Proto.Core", "src/protos/Proto.Core/"]
COPY ["services/AuthBot", "src/services/AuthBot/"]

RUN dotnet restore "src/services/AuthBot/AuthBot.csproj"
WORKDIR "src/services/AuthBot"
RUN dotnet build "AuthBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egeshka.AuthBot.dll"]