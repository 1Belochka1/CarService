FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CarService.Api/CarService.Api.csproj", "CarService.Api/"]
COPY ["CarService.App/CarService.App.csproj", "CarService.App/"]
COPY ["CarService.Core/CarService.Core.csproj", "CarService.Core/"]
COPY ["CarService.Persitence/CarService.Persitence.csproj", "CarService.Persitence/"]
RUN dotnet restore "CarService.Api/CarService.Api.csproj"
COPY . .
WORKDIR "/src/CarService.Api"
RUN dotnet build "CarService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CarService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarService.Api.dll"]
