FROM mcr.microsoft.com/dotnet/sdk:9.0 

WORKDIR /app

COPY Presentation/Solution1.API.csproj ./Presentation/

RUN dotnet restore

COPY . ./
RUN dotnet publish  -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Solution1.API.dll"]
