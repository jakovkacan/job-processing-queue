using JobProcessingQueue.Output;

namespace JobProcessingQueue.Tests.Output;

public class OutputTypesTests
{
    [Fact]
    public void TextOutput_CanStoreContent()
    {
        // Arrange
        var output = new TextOutput
        {
            Content = "Hello, World!"
        };

        // Act & Assert
        Assert.Equal("Hello, World!", output.Content);
        Assert.IsAssignableFrom<IJobOutput>(output);
    }

    [Fact]
    public void TabularOutput_CanStoreRows()
    {
        // Arrange
        var output = new TabularOutput
        {
            Rows = [new Dictionary<string, object> { { "id", 1 }, { "name", "Test" } }]
        };

        // Act & Assert
        Assert.Single(output.Rows);
        Assert.Equal(1, output.Rows[0]["id"]);
        Assert.IsAssignableFrom<IJobOutput>(output);
    }

    [Fact]
    public void SequentialOutput_CanStoreValues()
    {
        // Arrange
        var output = new SequentialOutput
        {
            Values = [1, "two", 3.0]
        };

        // Act & Assert
        Assert.Equal(3, output.Values.Count);
        Assert.Equal(1, output.Values[0]);
        Assert.Equal("two", output.Values[1]);
        Assert.IsAssignableFrom<IJobOutput>(output);
    }
}