# =========================
# BUILD STAGE
# =========================

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copiar solución completa

COPY . .

# Restore específico

RUN dotnet restore "Servicio/AcreditacionErumApi.csproj"

# Publish

RUN dotnet publish \
    "Servicio/AcreditacionErumApi.csproj" \
    -c Release \
    -o /app/publish

# =========================
# RUNTIME STAGE
# =========================

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AcreditacionErumApi.dll"]