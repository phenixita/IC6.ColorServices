FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["IC6.Weather/IC6.Weather.csproj", "IC6.Weather/"]
RUN dotnet restore "IC6.Weather/IC6.Weather.csproj"
COPY . .
WORKDIR /src/IC6.Weather
RUN dotnet build "IC6.Weather.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IC6.Weather.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IC6.Weather.dll"]