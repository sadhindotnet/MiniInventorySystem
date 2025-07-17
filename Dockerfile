# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# নিচের path এ .csproj ফাইলটা আছে ধরে নিচ্ছি
COPY ["MiniInventorySystem/MiniInventorySystem.csproj", "MiniInventorySystem/"]

WORKDIR /src/MiniInventorySystem
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "MiniInventorySystem.dll"]
