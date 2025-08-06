# ----------------------------
# Stage 1: Base Runtime Image
# ----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
USER app

# ----------------------------
# Stage 2: Build & Publish
# ----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Node.js (LTS)
RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get update && \
    apt-get install -y nodejs

WORKDIR /src

# Copy and restore .csproj
COPY ["Smart_OrdreMission/Smart_OrdreMission.csproj", "Smart_OrdreMission/"]
RUN dotnet restore "Smart_OrdreMission/Smart_OrdreMission.csproj"

# Copy package files and install frontend deps (before rest of code for caching)
COPY Smart_OrdreMission/package*.json Smart_OrdreMission/
WORKDIR /src/Smart_OrdreMission
RUN npm install

# Copy the rest of the app source code
COPY . .

# Build Tailwind CSS assets
RUN npm run build

# Build and publish the .NET project
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Smart_OrdreMission.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ----------------------------
# Stage 3: Final Runtime Image
# ----------------------------
FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

# Optional: Healthcheck (for Render or other platforms)
HEALTHCHECK --interval=30s --timeout=5s CMD curl -f http://localhost:8080/ || exit 1

ENTRYPOINT ["dotnet", "Smart_OrdreMission.dll"]
