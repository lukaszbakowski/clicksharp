#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

CMD python /app/app.py

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ClickSharp/ClickSharp.csproj", "ClickSharp/"]
COPY ["ClickSharp.DataLayer/ClickSharp.DataLayer.csproj", "ClickSharp.DataLayer/"]
COPY ["ClickSharp.Models/ClickSharp.Models.csproj", "ClickSharp.Models/"]
RUN dotnet restore "ClickSharp/ClickSharp.csproj"
COPY . .
WORKDIR "/src/ClickSharp"
RUN dotnet build "ClickSharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClickSharp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClickSharp.dll", "--launch-profile Docker"]