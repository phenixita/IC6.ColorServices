FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY IC6.Weather/*.csproj ./IC6.Weather/
RUN dotnet restore

# copy everything else and build app
COPY IC6.Weather/. ./IC6.Weather/
WORKDIR /app/IC6.Weather
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/IC6.Weather/out ./
ENTRYPOINT ["dotnet", "IC6.Weather.dll"]
