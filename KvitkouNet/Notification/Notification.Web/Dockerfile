FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Notification/Notification.Web/Notification.Web.csproj Notification/Notification.Web/
COPY Notification/Notification.Logic/Notification.Logic.csproj Notification/Notification.Logic/
COPY Notification/Notification.Data/Notification.Data.csproj Notification/Notification.Data/
COPY Gateway/KvitkouNet.Messages Gateway/KvitkouNet.Messages
RUN dotnet restore Notification/Notification.Web/Notification.Web.csproj
COPY ["Notification", "Notification"]
WORKDIR /src/Notification/Notification.Web
RUN dotnet build Notification.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Notification.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Notification.Web.dll"]
