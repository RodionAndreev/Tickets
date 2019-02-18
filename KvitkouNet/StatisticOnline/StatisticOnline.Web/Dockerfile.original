FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["StatisticOnline.Web/StatisticOnline.Web.csproj", "StatisticOnline.Web/"]
RUN dotnet restore "StatisticOnline.Web/StatisticOnline.Web.csproj"
COPY . .
WORKDIR "/src/StatisticOnline.Web"
RUN dotnet build "StatisticOnline.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StatisticOnline.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StatisticOnline.Web.dll"]