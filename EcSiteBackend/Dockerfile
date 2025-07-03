# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln .
COPY src/EcSiteBackend.Domain/*.csproj ./src/EcSiteBackend.Domain/
COPY src/EcSiteBackend.Application/*.csproj ./src/EcSiteBackend.Application/
COPY src/EcSiteBackend.Infrastructure/*.csproj ./src/EcSiteBackend.Infrastructure/
COPY src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI/*.csproj ./src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI/
COPY tests/EcSiteBackend.UnitTests/*.csproj ./tests/EcSiteBackend.UnitTests/
COPY tests/EcSiteBackend.IntegrationTests/*.csproj ./tests/EcSiteBackend.IntegrationTests/

# Restore dependencies
RUN dotnet restore

# Copy everything else
COPY . .

# Build the application
WORKDIR /src/src/EcSiteBackend.Presentation/EcSiteBackend.WebAPI
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcSiteBackend.WebAPI.dll"]