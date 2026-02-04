using JobProcessingQueue.Jobs.Parsing;
using JobProcessingQueue.Jobs.Parsing.Parsers;
using JobProcessingQueue.Output;
using Moq;

namespace JobProcessingQueue.Tests.Jobs;

public class ParsingJobTests
{
    [Fact]
    public void ParsingJob_Constructor_InitializesFieldsList()
    {
        // Arrange & Act
        var job = new ParsingJob();

        // Assert
        Assert.NotNull(job.Fields);
        Assert.Empty(job.Fields);
    }

    [Fact]
    public void ParsingJob_ProducesOutput_ReturnsTrue()
    {
        // Arrange
        var job = new ParsingJob();

        // Act
        var result = job.ProducesOutput();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ParsingJob_Execute_WithCSVParser_ReturnsTabularOutput()
    {
        // Arrange
        var mockParser = new Mock<IFileParser>();
        mockParser.Setup(p => p.Parse(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Returns(new TabularOutput());

        var job = new ParsingJob
        {
            FilePath = "/path/to/data.csv",
            Parser = mockParser.Object,
            Fields = ["column1", "column2"]
        };

        // Act
        var result = await job.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<TabularOutput>(result);
        mockParser.Verify(p => p.Parse("/path/to/data.csv", job.Fields), Times.Once);
    }

    [Fact]
    public void CSVParser_Parse_ReturnsTabularOutput()
    {
        // Arrange
        var parser = new CSVParser();
        var fields = new List<string> { "name", "age" };

        // Act
        var result = parser.Parse("/path/to/file.csv", fields);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<TabularOutput>(result);
    }

    [Fact]
    public void XMLParser_Parse_ReturnsSequentialOutput()
    {
        // Arrange
        var parser = new XMLParser();
        var fields = new List<string> { "/root/item" };

        // Act
        var result = parser.Parse("/path/to/file.xml", fields);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<SequentialOutput>(result);
    }

    [Fact]
    public void JSONParser_Parse_ReturnsSequentialOutput()
    {
        // Arrange
        var parser = new JSONParser();
        var fields = new List<string> { "$.data[*].id" };

        // Act
        var result = parser.Parse("/path/to/file.json", fields);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<SequentialOutput>(result);
    }
}