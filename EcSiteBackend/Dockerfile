# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore
COPY EcSiteBackend.sln ./
COPY . ./
RUN dotnet restore

# Build the app
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
