namespace ConsoleTemplate;

internal static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services) =>
        services.AddSingleton<IAnsiConsole>((sp) =>
        {
#pragma warning disable CS0618 // 'AnsiConsoleFactory' is obsolete
            AnsiConsoleFactory factory = new();
#pragma warning restore CS0618

            AnsiConsoleSettings settings = new()
            {
                Ansi = AnsiSupport.Detect,
                ColorSystem = ColorSystemSupport.Detect,
                Out = new AnsiConsoleOutput(Console.Out)
            };
            return factory.Create(settings);
        })
        .AddSingleton<IApplication, Application>();
}
