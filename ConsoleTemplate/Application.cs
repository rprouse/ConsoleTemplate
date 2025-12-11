using ConsoleTemplate.Extensions;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace ConsoleTemplate;

public class Application(IAnsiConsole console, IConfiguration configuration) : IApplication
{
    private readonly IAnsiConsole _console = console;
    private readonly IConfiguration _configuration = configuration;

    public async Task<int> Run()
    {
        var temp = _configuration.GetRequiredValue("TEMP_FOLDER");

        _console.MarkupLine($"[green]Hello, World![/] from ConsoleTemplate");
        _console.MarkupLine($"[yellow]Temporary folder is: {temp}[/]");

        return await Task.FromResult(0);
    }
}

public interface IApplication
{
    Task<int> Run();
}
