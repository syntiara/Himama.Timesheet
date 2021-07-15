# Dockerfile
#building our image based on Alpine allows us to build a smaller and more secure container image.
ARG VERSION=3.1-alpine3.10
FROM mcr.microsoft.com/dotnet/core/sdk:$VERSION AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:$VERSION
WORKDIR /app
COPY --from=build-env /app/out .


# Use this for Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Himama.Timesheet.dll