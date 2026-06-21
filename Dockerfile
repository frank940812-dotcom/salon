FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# 💡 這裡已經完全改成符合你外層結構的 salon/salon.csproj
COPY ["salon/salon.csproj", "salon/"]
RUN dotnet restore "salon/salon.csproj"
COPY . .
WORKDIR "/src/salon"
RUN dotnet build "salon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "salon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "salon.dll"]
