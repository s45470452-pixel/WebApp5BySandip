FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["WebApp5BySandip.csproj", "."]
RUN dotnet restore "WebApp5BySandip.csproj"

COPY . .
RUN dotnet publish "WebApp5BySandip.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_USE_POLLING_FILE_WATCHER=1

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApp5BySandip.dll"]