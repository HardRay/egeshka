FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Auth", "src/Auth/"]
COPY ["Auth.Domain", "src/Auth.Domain/"]
COPY ["Auth.Application", "src/Auth.Application/"]
COPY ["Auth.Infrastructure", "src/Auth.Infrastructure/"]

RUN dotnet restore "src/Auth/Auth.csproj"
WORKDIR "src/Auth"
RUN dotnet build "Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egeshka.Auth.dll"]