FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY StatisticUser.Web/StatisticUser.Web.csproj StatisticUser.Web/
COPY StatisticUser.Logic/StatisticUser.Logic.csproj StatisticUser.Logic/
COPY StatisticUser.Data/StatisticUser.Data.csproj StatisticUser.Data/
WORKDIR /src/StatisticUser.Web
RUN dotnet restore StatisticUser.Web.csproj
WORKDIR /src
COPY . .
WORKDIR /src/StatisticUser.Web
RUN dotnet build "StatisticUser.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StatisticUser.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StatisticUser.Web.dll"]