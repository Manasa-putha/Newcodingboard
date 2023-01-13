FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MywebApi.csproj", "."]
RUN dotnet restore "./MywebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MywebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MywebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MywebApi.dll"]