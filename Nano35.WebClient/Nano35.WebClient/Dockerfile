#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Nano35.WebClient/NuGet.config", "Nano35.WebClient/"]
COPY ["Nano35.WebClient/Nano35.WebClient.csproj", "Nano35.WebClient/"]
RUN dotnet restore Nano35.WebClient/Nano35.WebClient.csproj --configuration Nano35.WebClient/NuGet.config
COPY . .
WORKDIR "/src/Nano35.WebClient"
RUN dotnet build "Nano35.WebClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nano35.WebClient.csproj" -c Release -o /app/publish -p:PublishTrimmed=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nano35.WebClient.dll"]