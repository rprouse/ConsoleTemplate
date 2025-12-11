namespace ConsoleTemplate;

internal static class DotEnv
{
    /// <summary>
    /// Loads environment variables from a .env file located in the current directory.
    /// </summary>
    public static void Load()
    {
        var root = Directory.GetCurrentDirectory();
        var dotenv = Path.Combine(root, ".env");
        Load(dotenv);
    }

    /// <summary>
    /// Loads environment variables from the specified file and sets them for the current process.
    /// </summary>
    /// <remarks>If the file does not exist, the method does nothing. Lines that do not contain a valid
    /// 'key=value' pair are ignored. Environment variables set by this method are available only to the current process
    /// and its child processes.</remarks>
    /// <param name="filePath">The path to the file containing environment variable definitions. Each line should be in the format 'key=value'.</param>
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
        }
    }
}
