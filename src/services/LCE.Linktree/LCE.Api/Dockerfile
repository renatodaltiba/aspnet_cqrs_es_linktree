FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/LCE.Linktree/LCE.Api/LCE.Api.csproj", "src/services/LCE.Linktree/LCE.Api/"]
RUN dotnet restore "src/services/LCE.Linktree/LCE.Api/LCE.Api.csproj"
COPY . .
WORKDIR "/src/src/services/LCE.Linktree/LCE.Api"
RUN dotnet build "LCE.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LCE.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LCE.Api.dll"]
