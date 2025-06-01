# Stage 1: Build the application
# Using the .NET 9.0 SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:c5b188baf837b0180a14f988815b9cc7a55b836dd6cbe1b6e6523cf3098faaa8 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers to leverage Docker cache
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Create the runtime image
# Using the ASP.NET Core 9.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:d5507d8292fb2469ec0b9913f3cd4bb8d5a014bd6dc00c33fd4c4e7310229f07
WORKDIR /src
COPY --from=build-env /src/out .

# Expose port 8080 (standard for Kestrel in containers, can be mapped to any host port)
# ASP.NET Core apps default to port 8080 in containers from .NET 8 onwards.
# For .NET 7 and earlier, it was port 80.
# If your app is configured for a different port, update this.
EXPOSE 8080

# If you are using HTTPS, you might also need to expose port 8081 (or your configured HTTPS port)
# EXPOSE 8081

# Define the entry point for the container.
# Replace 'YourApiProjectName.dll' with the actual name of your API's DLL file.
ENTRYPOINT ["dotnet", "InventoryAPI.dll"]

# Optional: Health check (requires ASP.NET Core Health Checks configured in your app)
# HEALTHCHECK --interval=30s --timeout=30s --start-period=5s --retries=3 \
#   CMD curl -f http://localhost:8080/healthz || exit 1
