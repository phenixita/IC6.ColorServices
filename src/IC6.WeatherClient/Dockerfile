FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["IC6.WeatherClient/IC6.WeatherClient.csproj", "IC6.WeatherClient/"]
RUN dotnet restore "IC6.WeatherClient/IC6.WeatherClient.csproj"
COPY . .
WORKDIR "/src/IC6.WeatherClient"
RUN dotnet build "IC6.WeatherClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IC6.WeatherClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IC6.WeatherClient.dll"]