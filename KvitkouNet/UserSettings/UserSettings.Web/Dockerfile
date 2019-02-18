FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Gateway/KvitkouNet.Messages", "Gateway/KvitkouNet.Messages"]
COPY ["UserSettings/UserSettings.Data/UserSettings.Data.csproj", "UserSettings/UserSettings.Data/"]
COPY ["UserSettings/UserSettings.Logic/UserSettings.Logic.csproj", "UserSettings/UserSettings.Logic/"]
COPY ["UserSettings/UserSettings.Web/UserSettings.Web.csproj", "UserSettings/UserSettings.Web/"]
RUN dotnet restore "UserSettings/UserSettings.Web/UserSettings.Web.csproj"
COPY ["UserSettings", "UserSettings"]
WORKDIR "/src/UserSettings/UserSettings.Web"
RUN dotnet build "UserSettings.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserSettings.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserSettings.Web.dll"]