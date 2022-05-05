FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebChat.API/WebChat.API.csproj", "WebChat.API/"]
RUN dotnet restore "WebChat.API/WebChat.API.csproj"
COPY . .
WORKDIR "/src/WebChat.API"
RUN dotnet build "WebChat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebChat.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebChat.API.dll"]
