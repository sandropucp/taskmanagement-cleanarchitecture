#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# From solution root:
# docker build -t front-desk --progress=plain --no-cache -f .\src\TaskManagement.Api\Dockerfile .
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
# run this from repository root
COPY ./ ./ 
#RUN ls -lha .

RUN echo 'Building TaskManagement container'

WORKDIR /app/src/TaskManagement.Api
#RUN ls -lha .
RUN dotnet restore

RUN dotnet build "TaskManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManagement.Api.dll"]
