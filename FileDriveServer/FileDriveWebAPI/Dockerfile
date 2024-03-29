#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update && apt-get install -y libgdiplus

WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://*:5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FileDriveWebAPI/FileDriveWebAPI.csproj", "FileDriveWebAPI/"]
RUN dotnet restore "FileDriveWebAPI/FileDriveWebAPI.csproj"
COPY . .
WORKDIR "/src/FileDriveWebAPI"
RUN dotnet build "FileDriveWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileDriveWebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileDriveWebAPI.dll"]