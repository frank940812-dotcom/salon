FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# 💡 注意：如果你的專案檔名不叫 SalonSystem.csproj，請把下面兩行的 SalonSystem 改成你的專案名！
COPY ["SalonSystem.csproj", "."]
RUN dotnet restore "SalonSystem.csproj"
COPY . .
RUN dotnet build "SalonSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalonSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# 💡 注意：這裡的 SalonSystem.dll 也要改成你的專案名稱！
ENTRYPOINT ["dotnet", "SalonSystem.dll"]