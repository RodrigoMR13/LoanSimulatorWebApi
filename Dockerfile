# Etapa de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
# EXPOSE 8081

# Etapa de Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LoanSimulatorWebAPI/LoanSimulatorWebAPI.csproj", "LoanSimulatorWebAPI/"] 
RUN dotnet restore "./LoanSimulatorWebAPI/LoanSimulatorWebAPI.csproj"
COPY . .
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
ENTRYPOINT ["dotnet", "LoanSimulatorWebAPI.dll"]