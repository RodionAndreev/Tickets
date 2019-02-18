FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["DashboardMicroservice/DashboardMicroservice.csproj", "DashboardMicroservice/"]
COPY ["Dashboard.Data/Dashboard.Data.csproj", "Dashboard.Data/"]
COPY ["Dashboard.Logic/Dashboard.Logic.csproj", "Dashboard.Logic/"]
COPY ["../DashboardNet.Messages/KvitkouNet.Messages.csproj", "../DashboardNet.Messages/"]
RUN dotnet restore "DashboardMicroservice/DashboardMicroservice.csproj"
COPY . .
WORKDIR "/src/DashboardMicroservice"
RUN dotnet build "DashboardMicroservice.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DashboardMicroservice.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DashboardMicroservice.dll"]