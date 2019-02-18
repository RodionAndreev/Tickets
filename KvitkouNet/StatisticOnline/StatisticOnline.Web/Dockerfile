FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY StatisticOnline.Web/StatisticOnline.Web.csproj StatisticOnline.Web/
COPY StatisticOnline.Logic/StatisticOnline.Logic.csproj StatisticOnline.Logic/
COPY StatisticOnline.Data/StatisticOnline.Data.csproj StatisticOnline.Data/
WORKDIR /src/StatisticOnline.Web
RUN dotnet restore StatisticOnline.Web.csproj
WORKDIR /src
COPY . .
WORKDIR /src/StatisticOnline.Web
RUN dotnet build "StatisticOnline.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StatisticOnline.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StatisticOnline.Web.dll"]