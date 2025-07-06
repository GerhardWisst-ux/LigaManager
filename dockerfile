# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["LigaManagement.Web/LigaManagement.Web.csproj", "LigaManagement.Web/"]
RUN dotnet restore "LigaManagement.Web/LigaManagement.Web.csproj"

COPY . .
WORKDIR "/src/LigaManagement.Web"
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8081

ENTRYPOINT ["dotnet", "LigaManagement.Web.dll"]
