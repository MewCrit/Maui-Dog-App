FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8900
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["dog-service/dog-service.csproj", "dog-service/"]

RUN dotnet restore "dog-service/dog-service.csproj"
COPY . .
WORKDIR "/src/dog-service"
RUN dotnet build "dog-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dog-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dog-service.dll"]