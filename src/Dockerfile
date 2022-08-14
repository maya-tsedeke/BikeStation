FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
# copy all the layers' csproj files into respective folders
COPY ["./Project.Api/Project.Api.csproj", "src/Project.Api/"]
COPY ["./Project.ApplicationCore/Project.ApplicationCore.csproj", "src/Project.ApplicationCore/"]
COPY ["./Project.Infrastructure/Project.Infrastructure.csproj", "src/Project.Infrastructure/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/Project.Api/Project.Api.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/Project.Api/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT ["dotnet", "Project.Api.dll"]
