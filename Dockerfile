# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and all csproj files for restore
COPY *.sln ./
COPY FullStackDemo.WebApi/*.csproj ./FullStackDemo.WebApi/
COPY FullStackDemo.Domain/*.csproj ./FullStackDemo.Domain/
COPY FullStackDemo.Infrastructure/*.csproj ./FullStackDemo.Infrastructure/
COPY FullStackDemo.ApplicationService/*.csproj ./FullStackDemo.ApplicationService/
COPY FullStackDemo.Common/*.csproj ./FullStackDemo.Common/
COPY FullStackDemo.SQL.GundamDb/*.sqlproj ./FullStackDemo.SQL.GundamDb/
COPY FullStackDemo.Frontend/*.esproj ./FullStackDemo.Frontend/

RUN dotnet restore FullStackDemo.WebApi/FullStackDemo.WebApi.csproj

# Copy all source files and publish
COPY . . 
WORKDIR /app/FullStackDemo.WebApi
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/FullStackDemo.WebApi/out ./

# Environment
ENV ASPNETCORE_URLS=http://+:5028

# Expose port
EXPOSE 5028
ENTRYPOINT ["dotnet", "FullStackDemo.WebApi.dll"]