using Microsoft.Extensions.Configuration;
using ConsoleTemplate.Exceptions;

namespace ConsoleTemplate.Extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Retrieves the value associated with the specified configuration key, throwing an exception if the key is missing
    /// or its value is null.
    /// </summary>
    /// <remarks>Use this method when a configuration value is required and missing values should be treated
    /// as errors. This method simplifies error handling by throwing an exception if the value is not present.</remarks>
    /// <param name="configuration">The configuration source from which to retrieve the value. Cannot be null.</param>
    /// <param name="key">The key of the configuration value to retrieve. Cannot be null.</param>
    /// <returns>The non-null value associated with the specified key.</returns>
    /// <exception cref="ConfigurationException">Thrown if the specified key does not exist or its value is null in the configuration.</exception>
    public static string GetRequiredValue(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new ConfigurationException(key);
}
