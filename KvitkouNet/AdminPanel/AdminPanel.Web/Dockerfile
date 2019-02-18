FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["AdminPanel/AdminPanel.Web/AdminPanel.Web.csproj", "AdminPanel/AdminPanel.Web/"]
COPY ["AdminPanel/AdminPanel.Logic/AdminPanel.Logic.csproj", "AdminPanel/AdminPanel.Logic/"]
COPY ["Gateway/KvitkouNet.Messages/KvitkouNet.Messages.csproj", "Gateway/KvitkouNet.Messages/"]
RUN dotnet restore "AdminPanel/AdminPanel.Web/AdminPanel.Web.csproj"
COPY AdminPanel AdminPanel
COPY Gateway/KvitkouNet.Messages Gateway/KvitkouNet.Messages
WORKDIR "/src/AdminPanel/AdminPanel.Web"
RUN dotnet build "AdminPanel.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AdminPanel.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AdminPanel.Web.dll"]