# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AutoFinance.API.csproj", "./"]
RUN dotnet restore "AutoFinance.API.csproj"

COPY . .
RUN dotnet publish "AutoFinance.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AutoFinance.API.dll"]
