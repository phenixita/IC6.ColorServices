FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY *.sln .
WORKDIR /src
COPY IC6.Weather/IC6.Weather.Server.csproj ./IC6.Weather/
RUN dotnet restore IC6.Weather/IC6.Weather.Server.csproj

# copy everything else and build app
COPY IC6.Weather/. ./IC6.Weather/
WORKDIR /src/IC6.Weather
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /src/IC6.Weather/out ./
ENTRYPOINT ["dotnet", "IC6.Weather.Server.dll"]

# How to build the image:
# from the `src` folder, exec:
# docker build -f .\IC6.Weather\Dockerfile -t phenixita/ic6weather .

# how to use the image:
# docker run -d -p 8001:80 -e "WEATHER_DATALAYER_SERVICE_PORT=80" -e "WEATHER_DATALAYER_SERVICE_URL=172.17.0.2" --rm phenixita/ic6weather:latest
# curl http://localhost:8001/WeatherForecast




