FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["TicketManagement/TicketManagement.Web/TicketManagement.Web.csproj", "TicketManagement/TicketManagement.Web/"]
COPY ["TicketManagement/TicketManagement.Data/TicketManagement.Data.csproj", "TicketManagement/TicketManagement.Data/"]
COPY ["TicketManagement/TicketManagement.Logic/TicketManagement.Logic.csproj", "TicketManagement/TicketManagement.Logic/"]
COPY ["Gateway/KvitkouNet.Messages/KvitkouNet.Messages.csproj", "Gateway/KvitkouNet.Messages/"]
RUN dotnet restore "TicketManagement/TicketManagement.Web/TicketManagement.Web.csproj"
COPY . .
WORKDIR "/src/TicketManagement/TicketManagement.Web"
RUN dotnet build "TicketManagement.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TicketManagement.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TicketManagement.Web.dll"]