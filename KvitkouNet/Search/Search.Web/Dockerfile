FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Gateway/KvitkouNet.Messages", "Gateway/KvitkouNet.Messages"]
COPY ["Search/Search.Web/Search.Web.csproj", "Search/Search.Web/"]
RUN dotnet restore "Search/Search.Web/Search.Web.csproj"
COPY ["Search", "Search"]
WORKDIR "/src/Search/Search.Web"
RUN dotnet build "Search.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Search.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
VOLUME /app/Database
ENTRYPOINT ["dotnet", "Search.Web.dll"]