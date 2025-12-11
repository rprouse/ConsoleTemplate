# .NET Console Application Template

[![🏗️ .NET Build and Test](https://github.com/rprouse/ConsoleTemplate/actions/workflows/dotnet-build-test.yml/badge.svg)](https://github.com/rprouse/ConsoleTemplate/actions/workflows/dotnet-build-test.yml) [![🐋 Build and Push Docker Image](https://github.com/rprouse/ConsoleTemplate/actions/workflows/docker-build-push.yml/badge.svg)](https://github.com/rprouse/ConsoleTemplate/actions/workflows/docker-build-push.yml) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)

A small starter template for building modern .NET console applications.

This repository provides a minimal, well-structured console application that demonstrates common patterns used in production-ready tools such as dependency injection, configuration handling, environment variable loading, and console output formatting.

## Purpose

Use this project as a starting point when creating new .NET console applications. It includes examples of wiring up services, reading required configuration values, and writing formatted output to the console.

## Key Features

- Target framework: `.NET 10`
- Dependency injection with `Microsoft.Extensions.DependencyInjection`
- Configuration support with `Microsoft.Extensions.Configuration`
- Helper extension `ConfigurationExtensions.GetRequiredValue` that throws a typed `ConfigurationException` when a required configuration key is missing
- Environment variable loader (`DotEnv.Load`) that reads a `.env` file
- Console output using `Spectre.Console` for rich formatting
- Unit tests using `NUnit` and `Shouldly`

## Requirements

- .NET 10 SDK

## Quickstart

1. Clone the repository.
2. Set any required configuration values as environment variables or in a `.env` file at the project root. Example `.env` contents:

    ```
    TEMP_FOLDER=/tmp
    ```

3. Build and run the application from the `ConsoleTemplate` project directory:

    ```
    dotnet build
    dotnet run --project ConsoleTemplate/ConsoleTemplate.csproj
    ```

    If a required configuration key is missing, the application will display a helpful error indicating which key is not set.

## Running Tests

Run the unit tests from the solution root:

```
dotnet test
```

## Contributing

This project is intended as a simple template. Feel free to open issues or submit pull requests to improve examples or add features.