# Etapa de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8443

# Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LoanSimulatorWebAPI/LoanSimulatorWebAPI.csproj", "LoanSimulatorWebAPI/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "./LoanSimulatorWebAPI/LoanSimulatorWebAPI.csproj"
COPY src/ .
WORKDIR "/src/LoanSimulatorWebAPI"
RUN dotnet build "./LoanSimulatorWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação do serviço
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LoanSimulatorWebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY src/LoanSimulatorWebAPI/certs ./certs
ENTRYPOINT ["dotnet", "LoanSimulatorWebAPI.dll"]