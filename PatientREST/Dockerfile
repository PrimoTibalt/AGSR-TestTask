FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["PatientREST/PatientREST.csproj", "PatientREST/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]

RUN dotnet restore "PatientREST/PatientREST.csproj"
COPY . .
RUN dotnet build "PatientREST/PatientREST.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PatientREST/PatientREST.csproj" -c Release -o /app
# Per image database.
FROM build AS efcore
RUN dotnet tool install --global dotnet-ef --version 8.0.1
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src/PatientREST
RUN dotnet ef database update --project "PatientREST.csproj"
COPY --from=build /src/PatientREST/patients.db ./patients.db

FROM base AS final
WORKDIR /app

COPY --from=publish /app .
COPY --from=efcore /src/PatientREST/patients.db .
ENTRYPOINT ["dotnet", "PatientREST.dll"]