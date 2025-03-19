FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EnrollmentMS.API/EnrollmentMS.API.csproj", "EnrollmentMS.API/"]
COPY ["EnrollmentMS.Application/EnrollmentMS.Application.csproj", "EnrollmentMS.Application/"]
COPY ["EnrollmentMS.Domain/EnrollmentMS.Domain.csproj", "EnrollmentMS.Domain/"]
COPY ["EnrollmentMS.Persistence/EnrollmentMS.Persistence.csproj", "EnrollmentMS.Persistence/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["EnrollmentMS.Infrastructure/EnrollmentMS.Infrastructure.csproj", "EnrollmentMS.Infrastructure/"]
COPY ["Solution1.Domain/Solution1.Domain.csproj", "Solution1.Domain/"]
RUN dotnet restore "EnrollmentMS.API/EnrollmentMS.API.csproj"
COPY . .
WORKDIR "/src/EnrollmentMS.API"
RUN dotnet build "EnrollmentMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EnrollmentMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnrollmentMS.API.dll"]
