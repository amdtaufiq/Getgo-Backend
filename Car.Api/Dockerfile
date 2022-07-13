#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7005

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Car.Api/Car.Api.csproj", "Car.Api/"]
RUN dotnet restore "Car.Api/Car.Api.csproj"
COPY . .
WORKDIR "/src/Car.Api"
RUN dotnet build "Car.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Car.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Car.Api.dll"]