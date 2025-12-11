namespace ConsoleTemplate.Exceptions;

public class ConfigurationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the ConfigurationException class with a message indicating that a required
    /// configuration is not set.
    /// </summary>
    /// <param name="key">The name of the configuration key or environment variable that is missing. Cannot be null or empty.</param>
    public ConfigurationException(string key) :
        base($"Configuration for {key} is not set")
    {
        Key = key;
    }

    public string Key { get; }
}
