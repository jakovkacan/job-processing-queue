using JobProcessingQueue.Core;
using JobProcessingQueue.Factories;
using JobProcessingQueue.Jobs.Code;
using JobProcessingQueue.Jobs.Parsing;
using JobProcessingQueue.Jobs.Parsing.Parsers;
using JobProcessingQueue.Jobs.Upload;
using JobProcessingQueue.Jobs.Upload.Strategies;
using Moq;

namespace JobProcessingQueue.Tests.Factories;

public class JobFactoryTests
{
    [Fact]
    public void CodeJobFactory_CreateJob_ReturnsCodeJob()
    {
        // Arrange
        var factory = new CodeJobFactory();

        // Act
        var job = factory.CreateJob();

        // Assert
        Assert.NotNull(job);
        Assert.IsType<CodeJob>(job);
        var codeJob = (CodeJob)job;
        Assert.Equal("print('hello')", codeJob.SourceCode);
        Assert.Equal(Enums.ProgrammingLanguageType.Interpreted, codeJob.LanguageType);
        Assert.Equal(Enums.Priority.High, codeJob.Priority);
        Assert.NotNull(codeJob.Id);
        Assert.NotEmpty(codeJob.Id);
    }

    [Fact]
    public void UploadJobFactory_CreateJob_ReturnsUploadJob()
    {
        // Arrange
        var mockStrategy = new Mock<ICloudUploadStrategy>();
        var factory = new UploadJobFactory();

        // Act
        var job = factory.CreateJob();

        // Assert
        Assert.NotNull(job);
        Assert.IsType<UploadJob>(job);
        var uploadJob = (UploadJob)job;
        Assert.Equal("/path/to/file.txt", uploadJob.FilePath);
        Assert.Equal(Enums.Priority.Medium, uploadJob.Priority);
        Assert.NotNull(uploadJob.Id);
    }

    [Fact]
    public void ParsingJobFactory_CreateJob_ReturnsParsingJob()
    {
        // Arrange
        var mockParser = new Mock<IFileParser>();
        var fields = new List<string> { "field1", "field2" };
        var factory = new ParsingJobFactory();

        // Act
        var job = factory.CreateJob();

        // Assert
        Assert.NotNull(job);
        Assert.IsType<ParsingJob>(job);
        var parsingJob = (ParsingJob)job;
        Assert.Equal("/path/to/data.csv", parsingJob.FilePath);
        Assert.Equal(fields, parsingJob.Fields);
        Assert.Equal(Enums.Priority.Low, parsingJob.Priority);
        Assert.NotNull(parsingJob.Id);
    }

    [Fact]
    public void CodeJobFactory_CreateJob_GeneratesUniqueIds()
    {
        // Arrange
        var factory = new CodeJobFactory();

        // Act
        var job1 = factory.CreateJob();
        var job2 = factory.CreateJob();

        // Assert
        Assert.NotEqual(job1.Id, job2.Id);
    }
}