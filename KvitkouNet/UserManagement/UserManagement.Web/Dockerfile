FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["UserManagement/UserManagement.Web/UserManagement.Web.csproj", "UserManagement/UserManagement.Web/"]
COPY ["UserManagement/UserManagement.Logic/UserManagement.Logic.csproj", "UserManagement/UserManagement.Logic/"]
COPY ["UserManagement/UserManagement.Data/UserManagement.Data.csproj", "UserManagement/UserManagement.Data/"]
COPY ["Gateway/KvitkouNet.Messages/KvitkouNet.Messages.csproj", "Gateway/KvitkouNet.Messages/"]
RUN dotnet restore "UserManagement/UserManagement.Web/UserManagement.Web.csproj"
COPY UserManagement UserManagement
COPY Gateway/KvitkouNet.Messages Gateway/KvitkouNet.Messages
WORKDIR "/src/UserManagement/UserManagement.Web"
RUN dotnet build "UserManagement.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserManagement.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserManagement.Web.dll"]