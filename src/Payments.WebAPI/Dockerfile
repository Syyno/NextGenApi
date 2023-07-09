FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Payments.WebAPI/Payments.WebAPI.csproj", "src/Payments.WebAPI/"]
COPY ["src/Payments.Application/Payments.Application.csproj", "src/Payments.Application/"]
COPY ["src/Payments.Domain/Payments.Domain.csproj", "src/Payments.Domain/"]
COPY ["src/Payments.Infrastructure/Payments.Infrastructure.csproj", "src/Payments.Infrastructure/"]
COPY ["src/Payments.Protocol/Payments.Protocol.csproj", "src/Payments.Protocol/"]
COPY ["src/Payments.Presentation/Payments.Presentation.csproj", "src/Payments.Presentation/"]
RUN dotnet restore "src/Payments.WebAPI/Payments.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Payments.WebAPI"
RUN dotnet build "Payments.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payments.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payments.WebAPI.dll"]
