# -------- Build Stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# নিচের path এ .csproj ফাইলটা আছে বলে ধরে নিচ্ছি
COPY ["EmbroideryWorkerManagement/EmbroideryWorkerManagement.csproj", "EmbroideryWorkerManagement/"]

WORKDIR /src/EmbroideryWorkerManagement
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# -------- Runtime Stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "EmbroideryWorkerManagement.dll"]
