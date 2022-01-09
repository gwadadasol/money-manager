#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["CategoryService/CategoryService.csproj", "CategoryService/"]
#RUN dotnet restore "CategoryService/CategoryService.csproj"
#COPY . .
#WORKDIR "/src/CategoryService"
#RUN dotnet build "CategoryService.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "CategoryService.csproj" -c Release -o /app/publish
#
#
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "CategoryService.dll"]
#####################################################

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "CategoryService.dll" ]