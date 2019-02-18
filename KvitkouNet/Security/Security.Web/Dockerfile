FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Security/Security.Web/Security.Web.csproj", "Security/Security.Web/"]
COPY ["Security/Security.Logic/Security.Logic.csproj", "Security/Security.Logic/"]
COPY ["Security/Security.Data/Security.Data.csproj", "Security/Security.Data/"]
COPY ["Gateway/KvitkouNet.Messages/KvitkouNet.Messages.csproj", "Gateway/KvitkouNet.Messages/"]
RUN dotnet restore "Security/Security.Web/Security.Web.csproj"
COPY Security Security
COPY Gateway/KvitkouNet.Messages Gateway/KvitkouNet.Messages
WORKDIR "/src/Security/Security.Web"
RUN dotnet build "Security.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Security.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Security.Web.dll"]