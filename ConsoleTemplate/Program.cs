using ConsoleTemplate;
using ConsoleTemplate.Exceptions;

try
{
    // Load environment variables from .env file
    DotEnv.Load();

    // Build configuration from environment variables
    var config = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .Build();

    // Set up dependency injection
    var serviceProvider = new ServiceCollection()
        .AddDependencyInjection()
        .AddSingleton<IConfiguration>(config)
        .BuildServiceProvider();

    // Run the application
    var app = serviceProvider.GetRequiredService<IApplication>();
    return await app.Run();
}
catch (ConfigurationException ex)
{
    AnsiConsole.Markup($"[red]Configuration Error:[/] Missing required configuration key: [underline]{ex.Key}[/]");
    return await Task.FromResult(1);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return await Task.FromResult(1);
}
