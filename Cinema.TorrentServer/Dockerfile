FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cinema.TorrentServer/Cinema.TorrentServer.csproj", "Cinema.TorrentServer/"]
RUN dotnet restore "Cinema.TorrentServer/Cinema.TorrentServer.csproj"
COPY . .
WORKDIR "/src/Cinema.TorrentServer"
RUN dotnet build "Cinema.TorrentServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cinema.TorrentServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinema.TorrentServer.dll"]
