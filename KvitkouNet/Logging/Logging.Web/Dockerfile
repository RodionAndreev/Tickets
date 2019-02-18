FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Logging/Logging.Web/Logging.Web.csproj", "Logging/Logging.Web/"]
COPY ["Logging/Logging.Logic/Logging.Logic.csproj", "Logging/Logging.Logic/"]
COPY ["Logging/Logging.Data/Logging.Data.csproj", "Logging/Logging.Data/"]
COPY ["Gateway/KvitkouNet.Messages/KvitkouNet.Messages.csproj", "Gateway/KvitkouNet.Messages/"]
RUN dotnet restore "Logging/Logging.Web/Logging.Web.csproj"
COPY Logging Logging
COPY Gateway/KvitkouNet.Messages Gateway/KvitkouNet.Messages
WORKDIR "/src/Logging/Logging.Web"
RUN dotnet build "Logging.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Logging.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Logging.Web.dll"]