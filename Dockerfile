FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PatientREST/PatientREST.csproj", "PatientREST/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["SeedPatientsApp/SeedPatientsApp.csproj", "SeedPatientsApp/"]
COPY ["PatientREST/PatientREST.sln", "PatientREST/"]

RUN dotnet restore "PatientREST/PatientREST.sln"
COPY . .

FROM build AS publish
RUN dotnet publish PatientREST/PatientREST.sln -c Release -o /app 

FROM base AS final
WORKDIR /app

COPY --from=publish /app .

ENTRYPOINT ["dotnet", "PatientREST.dll"]