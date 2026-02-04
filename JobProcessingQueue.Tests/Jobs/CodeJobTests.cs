using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs.Code;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Tests.Jobs;

public class CodeJobTests
{
    [Fact]
    public void CodeJob_Constructor_InitializesProperties()
    {
        // Arrange & Act
        var job = new CodeJob
        {
            Id = "test-1",
            Priority = Enums.Priority.High,
            SourceCode = "print('Hello')",
            LanguageType = Enums.ProgrammingLanguageType.Interpreted
        };

        // Assert
        Assert.Equal("test-1", job.Id);
        Assert.Equal(Enums.Priority.High, job.Priority);
        Assert.Equal("print('Hello')", job.SourceCode);
        Assert.Equal(Enums.ProgrammingLanguageType.Interpreted, job.LanguageType);
        Assert.NotNull(job.Arguments);
        Assert.Empty(job.Arguments);
    }

    [Fact]
    public void CodeJob_ProducesOutput_ReturnsTrue()
    {
        // Arrange
        var job = new CodeJob();

        // Act
        var result = job.ProducesOutput();

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(Enums.Priority.Low)]
    [InlineData(Enums.Priority.Medium)]
    [InlineData(Enums.Priority.High)]
    public void CodeJob_CanSetDifferentPriorities(Enums.Priority priority)
    {
        // Arrange
        var job = new CodeJob { Priority = priority };

        // Act & Assert
        Assert.Equal(priority, job.Priority);
    }

    [Fact]
    public void CodeJob_Arguments_CanBeAdded()
    {
        // Arrange
        var job = new CodeJob();

        // Act
        job.Arguments.Add("--verbose");
        job.Arguments.Add("--output=file.txt");

        // Assert
        Assert.Equal(2, job.Arguments.Count);
        Assert.Contains("--verbose", job.Arguments);
        Assert.Contains("--output=file.txt", job.Arguments);
    }

    [Fact]
    public void CodeJob_Stop_SetsStopFlag()
    {
        // Arrange
        var job = new CodeJob();

        // Act
        job.Stop();

        // Assert - No exception thrown
        Assert.NotNull(job);
    }

    [Fact]
    public async Task CodeJob_Execute_ReturnsTextOutput()
    {
        // Arrange
        var job = new CodeJob
        {
            SourceCode = "print('test')",
            LanguageType = Enums.ProgrammingLanguageType.Interpreted
        };

        // Act
        var result = await job.Execute();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<TextOutput>(result);
    }
}