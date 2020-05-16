#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Evolution.Apis/Evolution.Apis.csproj", "Evolution.Apis/"]
RUN dotnet restore "Evolution.Apis/Evolution.Apis.csproj"
COPY . .
WORKDIR "/src/Evolution.Apis"
RUN dotnet build "Evolution.Apis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Evolution.Apis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evolution.Apis.dll"]