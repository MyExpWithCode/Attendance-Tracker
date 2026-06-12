FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["AttendanceService/AttendanceService.csproj", "AttendanceService/"]
COPY ["AttendanceService.Business/AttendanceService.Business.csproj", "AttendanceService.Business/"]
COPY ["AttendanceService.DAL/AttendanceService.DAL.csproj", "AttendanceService.DAL/"]
RUN dotnet restore "AttendanceService/AttendanceService.csproj"
COPY . .
WORKDIR "/src/AttendanceService"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AttendanceService.dll"]
