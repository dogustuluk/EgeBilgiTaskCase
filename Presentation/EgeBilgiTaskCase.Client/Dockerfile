#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Presentation/EgeBilgiTaskCase.Client/EgeBilgiTaskCase.Client.csproj", "Presentation/EgeBilgiTaskCase.Client/"]
COPY ["Core/EgeBilgiTaskCase.Application/EgeBilgiTaskCase.Application.csproj", "Core/EgeBilgiTaskCase.Application/"]
COPY ["Core/EgeBilgiTaskCase.Domain/EgeBilgiTaskCase.Domain.csproj", "Core/EgeBilgiTaskCase.Domain/"]
RUN dotnet restore "Presentation/EgeBilgiTaskCase.Client/EgeBilgiTaskCase.Client.csproj"
COPY . .
WORKDIR "/src/Presentation/EgeBilgiTaskCase.Client"
RUN dotnet build "EgeBilgiTaskCase.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EgeBilgiTaskCase.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EgeBilgiTaskCase.Client.dll"]