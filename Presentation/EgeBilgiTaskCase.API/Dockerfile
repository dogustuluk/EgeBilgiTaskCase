#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Presentation/EgeBilgiTaskCase.API/EgeBilgiTaskCase.API.csproj", "Presentation/EgeBilgiTaskCase.API/"]
COPY ["Core/EgeBilgiTaskCase.Application/EgeBilgiTaskCase.Application.csproj", "Core/EgeBilgiTaskCase.Application/"]
COPY ["Core/EgeBilgiTaskCase.Domain/EgeBilgiTaskCase.Domain.csproj", "Core/EgeBilgiTaskCase.Domain/"]
COPY ["Infrastructure/EgeBilgiTaskCase.Infrastructure/EgeBilgiTaskCase.Infrastructure.csproj", "Infrastructure/EgeBilgiTaskCase.Infrastructure/"]
COPY ["Infrastructure/EgeBilgiTaskCase.Persistence/EgeBilgiTaskCase.Persistence.csproj", "Infrastructure/EgeBilgiTaskCase.Persistence/"]
RUN dotnet restore "Presentation/EgeBilgiTaskCase.API/EgeBilgiTaskCase.API.csproj"
COPY . .
WORKDIR "/src/Presentation/EgeBilgiTaskCase.API"
RUN dotnet build "EgeBilgiTaskCase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EgeBilgiTaskCase.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EgeBilgiTaskCase.API.dll"]