using ConsoleTemplate.Exceptions;

namespace ConsoleTemplate.Tests.Exceptions;

public class ConfigurationExceptionTests
{
    [Test]
    public void Constructor_SetsKeyProperty()
    {
        // Arrange
        var key = "MY_KEY";

        // Act
        var ex = new ConfigurationException(key);

        // Assert
        ex.Key.ShouldBe(key);
    }

    [Test]
    public void Constructor_SetsMessage()
    {
        // Arrange
        var key = "TEMP_FOLDER";

        // Act
        var ex = new ConfigurationException(key);

        // Assert
        ex.Message.ShouldBe($"Configuration for {key} is not set");
    }

    [Test]
    public void Constructor_AllowsNullKey()
    {
        // Act
        var ex = new ConfigurationException(null!);

        // Assert
        ex.Key.ShouldBeNull();
        ex.Message.ShouldBe("Configuration for  is not set");
    }
}
