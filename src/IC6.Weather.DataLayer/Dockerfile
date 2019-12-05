FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
# COPY *.sln .
WORKDIR /src
COPY IC6.Weather.DataLayer/IC6.Weather.DataLayer.csproj ./IC6.Weather.DataLayer/
RUN dotnet restore IC6.Weather.DataLayer/IC6.Weather.DataLayer.csproj

# copy everything else and build app
COPY IC6.Weather.DataLayer/. ./IC6.Weather.DataLayer/
WORKDIR /src/IC6.Weather.DataLayer
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /src/IC6.Weather.DataLayer/out ./
ENTRYPOINT ["dotnet", "IC6.Weather.DataLayer.dll"]

# How to build the image:
# from the `src` folder, exec:
# docker build -f .\IC6.Weather.DataLayer\Dockerfile -t phenixita/ic6weatherdatalayer .

# how to use the image:
# docker run -d -p 8002:80 --rm phenixita/ic6weatherdatalayer:latest
# curl http://localhost:8002/WeatherData
  











