using ConsoleTemplate.Exceptions;
using ConsoleTemplate.Extensions;
using Microsoft.Extensions.Configuration;

namespace ConsoleTemplate.Tests.Extensions;

public class ConfigurationExtensionTests
{
    [Test]
    public void GetRequiredValue_ReturnsValue_WhenKeyExists()
    {
        // Arrange
        var values = new Dictionary<string, string?>
        {
            ["TEMP_FOLDER"] = "/tmp"
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(values!)
            .Build();

        // Act
        var result = config.GetRequiredValue("TEMP_FOLDER");

        // Assert
        result.ShouldBe("/tmp");
    }

    [Test]
    public void GetRequiredValue_ThrowsConfigurationException_WhenKeyMissing()
    {
        // Arrange
        var config = new ConfigurationBuilder().Build();
        var key = "MISSING_KEY";

        // Act & Assert
        var ex = Should.Throw<ConfigurationException>(() => config.GetRequiredValue(key));
        ex.Key.ShouldBe(key);
    }

    [Test]
    public void GetRequiredValue_AllowsEmptyStringValue()
    {
        // Arrange
        var values = new Dictionary<string, string?>
        {
            ["TEMP_FOLDER"] = string.Empty
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(values!)
            .Build();

        // Act
        var result = config.GetRequiredValue("TEMP_FOLDER");

        // Assert
        result.ShouldBe(string.Empty);
    }
}
