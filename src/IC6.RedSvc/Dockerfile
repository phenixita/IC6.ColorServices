FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["IC6.RedSvc/IC6.RedSvc.csproj", "IC6.RedSvc/"]
RUN dotnet restore "IC6.RedSvc/IC6.RedSvc.csproj"
COPY . .
WORKDIR "/src/IC6.RedSvc"
RUN dotnet build "IC6.RedSvc.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IC6.RedSvc.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IC6.RedSvc.dll"]