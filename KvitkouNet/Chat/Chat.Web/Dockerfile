FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Chat/Chat.Web/Chat.Web.csproj Chat/Chat.Web/
COPY Chat/Chat.Logic/Chat.Logic.csproj Chat/Chat.Logic/
COPY Chat/Chat.Data/Chat.Data.csproj Chat/Chat.Data/
COPY Gateway/KvitkouNet.Messages Gateway/KvitkouNet.Messages
RUN dotnet restore Chat/Chat.Web/Chat.Web.csproj
COPY ["Chat", "Chat"]
WORKDIR /src/Chat/Chat.Web
RUN dotnet build Chat.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Chat.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Chat.Web.dll"]
