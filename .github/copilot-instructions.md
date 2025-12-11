# Coding Guidelines

## Project Overview

This is a .NET 10 console application that that is used as a template for console applications. The application uses dependency injection, environment-based configuration, and modern C# features.

## General Principles

### Language and Framework
- Target framework: **.NET 10**
- Language version: **latest**
- Enable **implicit usings** and **nullable reference types**
- Use modern C# features (pattern matching, expression-bodied members, null-coalescing operators, etc.)

### Code Style
- Use **file-scoped namespaces** (namespace declarations without braces)
- Prefer **expression-bodied members** for simple properties and methods
- Use **var** for local variables when the type is obvious
- Use **PascalCase** for public members, **camelCase** for private fields with `_` prefix for readonly fields
- Keep methods small and focused on a single responsibility
- Add XML documentation comments for public APIs, especially when behavior isn't immediately obvious

### Comments
- Add XML documentation (`///`) for public classes, methods, properties, and interfaces
- Include `<summary>`, `<param>`, `<returns>`, and `<exception>` tags where appropriate
- Use `<remarks>` to provide usage guidance or important behavioral notes
- Avoid inline comments unless explaining complex logic or non-obvious behavior
- Never add redundant comments that merely restate the code

## Architecture Patterns

### Dependency Injection
- Use **Microsoft.Extensions.DependencyInjection** for all service registrations
- Register services in `DependencyInjection.cs` using extension methods
- Prefer constructor injection over property or method injection
- Register interfaces with their implementations (prefer interfaces for testability)
- Use appropriate service lifetimes:
  - **Singleton** for stateless services and configuration
  - **Scoped** for request-scoped services (if applicable)
  - **Transient** for lightweight stateless services

Example:
```csharp
public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
{
    services.AddSingleton<IGoogleCloudStorageService, GoogleCloudStorageService>();
    services.AddSingleton<IApplication, Application>();
    return services;
}
```

### Configuration Management
- Load environment variables from `.env` file using `DotEnv.Load()`
- Use `IConfiguration` from **Microsoft.Extensions.Configuration**
- Access environment variables via `IConfiguration` indexer or extension methods
- Use `ConfigurationExtensions.GetRequiredValue()` for mandatory configuration
- Throw `ConfigurationException` with the key name when required config is missing
- Never hardcode sensitive values; always use environment variables

Example:
```csharp
var bucketUri = configuration.GetRequiredValue("TEMP_FOLDER");
```

### Exception Handling
- Create custom exception types in the `Exceptions/` folder
- Include meaningful error messages and relevant context properties
- Handle exceptions at the application boundary (`Program.cs`)
- Use `Spectre.Console.AnsiConsole` for user-friendly error output
- Return appropriate exit codes (0 for success, non-zero for errors)

Example:
```csharp
public class ConfigurationException : Exception
{
    public ConfigurationException(string key) : 
        base($"Configuration for {key} is not set")
    {
        Key = key;
    }

    public string Key { get; }
}
```

### Extension Methods
- Place extension methods in the `Extensions/` namespace
- Group related extensions in appropriately named static classes
- Keep extension methods simple and focused
- Add XML documentation for clarity
- Use `this` parameter convention for extension methods

Example:
```csharp
namespace StudyConverter.Extensions;

public static class StringExtensions
{
    public static bool IsDicomFile(this string filename) =>
        Path.GetExtension(filename).ToLowerInvariant() == ".dcm";
}
```

## Third-Party Libraries

### Console UI
- Use **Spectre.Console** for all console output
- Use `AnsiConsole.Markup()` for formatted output with colors
- Follow Spectre.Console markup syntax: `[color]text[/]`
- Inject `IAnsiConsole` for testability instead of using static `AnsiConsole`

### Configuration
- Use **Microsoft.Extensions.Configuration.EnvironmentVariables** for environment variable support

## Testing Guidelines

### Test Framework
- Use **NUnit** as the testing framework
- Use **Shouldly** for assertions (fluent assertion library)
- Use **Moq** for mocking dependencies
- Follow NUnit conventions: `[Test]`, `[TestCase]`, `[SetUp]`, `[TearDown]`

### Test Structure
- Organize tests in the same namespace structure as the code under test
- Name test classes as `{ClassUnderTest}Tests`
- Use descriptive test method names: `MethodName_Scenario_ExpectedBehavior`
- Use `[TestCase]` attribute for parameterized tests
- Define global usings in `GlobalUsings.cs` to reduce repetition

Example:
```csharp
namespace StudyConverterTests.Extensions
{
    public class StringExtensionsTests
    {
        [TestCase("file.dcm", true)]
        [TestCase("file.txt", false)]
        public void IsDicomFile_ReturnsExpected(string filename, bool expected)
        {
            filename.IsDicomFile().ShouldBe(expected);
        }
    }
}
```

### Shouldly Assertions
- Use Shouldly's fluent assertion syntax
- Common assertions:
  - `result.ShouldBe(expected)` - equality
  - `result.ShouldBeTrue()` / `result.ShouldBeFalse()` - boolean checks
  - `result.ShouldBeNull()` / `result.ShouldNotBeNull()` - null checks
  - `Should.Throw<TException>(() => code)` - exception testing
  - `collection.ShouldContain(item)` - collection checks

### Test Coverage
- Write tests for all public methods and extension methods
- Focus on edge cases and error conditions
- Test both success and failure paths
- Keep tests independent and isolated

## Docker Support

### Dockerfile
- Multi-stage build with `base`, `build`, `publish`, and `final` stages
- Base image: `mcr.microsoft.com/dotnet/runtime:10.0`
- Build image: `mcr.microsoft.com/dotnet/sdk:10.0`
- Target OS: Linux
- Copy `.env` and `.env.example` files to output directory

### Environment Variables
- Configure all required environment variables in `.env` file
- Copy `.env.example` to `.env` and populate with actual values
- Never commit `.env` file with sensitive values to source control

## Performance Considerations

- Use `IAsyncEnumerable<T>` for streaming large collections
- Dispose of `IDisposable` resources properly (use `using` statements)
- Use buffered I/O with appropriate buffer sizes (e.g., 1 MB for AIP files)
- Avoid blocking calls in async methods
- Use `CancellationToken` for long-running operations

## Security Best Practices

- Never hardcode credentials or sensitive values
- Use environment variables for all configuration
- Support multiple authentication methods (tokens and default credentials)
- Validate and sanitize file paths
- Use secure cryptographic methods from `System.Security.Cryptography`

## Git and Version Control

- Follow conventional commits for commit messages
- Keep `.env` in `.gitignore`
- Include `.env.example` as a template
- Document all required environment variables in README

---

**Remember**: Write clean, maintainable, and testable code. Prefer simple solutions over clever ones. When in doubt, follow existing patterns in the codebase.
