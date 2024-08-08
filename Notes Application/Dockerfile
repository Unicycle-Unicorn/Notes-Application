# Use the .NET 8.0 SDK image as base
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy the .NET project file and restore dependencies
COPY *.csproj ./

ARG ADD_NUGET_SOURCE=false
ARG GITHUB_TOKEN=not_provided
ARG GITHUB_USER=not_provided

# Conditional GitHub NuGet source configuration
RUN if [ "$ADD_NUGET_SOURCE" = "true" ]; then \
        dotnet nuget add source https://nuget.pkg.github.com/Unicycle-Unicorn/index.json -n UniyclceUnicorn --valid-authentication-types basic -u "${GITHUB_USER}" -p "${GITHUB_TOKEN}" --store-password-in-clear-text; \
    fi

RUN dotnet restore

# Copy the remaining source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

# Set the working directory for the runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build-env /app/out .

# Command to run the application
ENTRYPOINT ["dotnet", "Notes Application.dll"]
