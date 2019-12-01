FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY *.sln .
WORKDIR /src
COPY IC6.WeatherClient/IC6.WeatherClient.csproj ./IC6.WeatherClient/
RUN dotnet restore IC6.WeatherClient/IC6.WeatherClient.csproj

# copy everything else and build app
COPY IC6.WeatherClient/. ./IC6.WeatherClient/
WORKDIR /src/IC6.WeatherClient
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /src/IC6.WeatherClient/out ./
ENTRYPOINT ["dotnet", "IC6.WeatherClient.dll"]

# How to build the image:
# from the `src` folder, exec:
# docker build -f .\IC6.WeatherClient\Dockerfile -t phenixita/ic6weatherclient .

# how to use the image:
# docker run -d -p 8080:80 --rm phenixita/ic6weatherclient:latest
# docker run -d -p 8080:80 --rm -e "WEATHER_SERVICE_PORT=80" -e "WEATHER_SERVICE_URL=172.17.0.4" phenixita/ic6weatherclient:latest

# http://localhost:8080/
  











