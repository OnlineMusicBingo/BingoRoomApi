#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.3 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0.3 AS build
WORKDIR /src
COPY ["BingoRoomApi/BingoRoomApi.csproj", "BingoRoomApi/"]
RUN dotnet restore "BingoRoomApi/BingoRoomApi.csproj"
COPY . .
WORKDIR "/src/BingoRoomApi"
RUN dotnet build "BingoRoomApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BingoRoomApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BingoRoomApi.dll"]
