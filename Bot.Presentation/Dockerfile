FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bot.Presentation/Bot.Presentation.csproj", "Bot.Presentation/"]
RUN dotnet restore "Bot.Presentation/Bot.Presentation.csproj"
COPY . .
WORKDIR "/src/Bot.Presentation"
RUN dotnet build "Bot.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bot.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bot.Presentation.dll"]
