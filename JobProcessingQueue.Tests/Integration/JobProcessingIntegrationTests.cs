using JobProcessingQueue.Consumers;
using JobProcessingQueue.Consumers.DataPipelines;
using JobProcessingQueue.Consumers.DataPipelines.Strategies;
using JobProcessingQueue.Consumers.Notifiers;
using JobProcessingQueue.Consumers.Notifiers.Strategies;
using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs.Code;
using JobProcessingQueue.Jobs.Parsing;
using JobProcessingQueue.Jobs.Parsing.Parsers;
using JobProcessingQueue.Output;
using Moq;

namespace JobProcessingQueue.Tests.Integration;

public class JobProcessingIntegrationTests
{
    [Fact]
    public async Task CompleteWorkflow_CodeJob_WithNotifier()
    {
        // Arrange
        var mockNotificationStrategy = new Mock<INotificationStrategy>();
        mockNotificationStrategy.Setup(s => s.Send(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Returns(Task.CompletedTask);

        var notifier = new Notifier
        {
            Message = "Job completed successfully",
            Receivers = new List<string> { "admin@example.com" },
            NotificationStrategy = mockNotificationStrategy.Object
        };

        var job = new CodeJob
        {
            Id = "integration-test-1",
            Priority = Enums.Priority.High,
            SourceCode = "print('test')",
            LanguageType = Enums.ProgrammingLanguageType.Interpreted,
            Consumers = new List<IConsumer> { notifier }
        };

        // Act
        var output = await job.Execute();
        foreach (var consumer in job.Consumers)
        {
            consumer.Consume(output);
        }

        // Assert
        Assert.NotNull(output);
        mockNotificationStrategy.Verify(
            s => s.Send("Job completed successfully", It.IsAny<List<string>>()),
            Times.Once
        );
    }

    [Fact]
    public async Task CompleteWorkflow_ParsingJob_WithDataPipeline()
    {
        // Arrange
        var mockParser = new Mock<IFileParser>();
        mockParser.Setup(p => p.Parse(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Returns(new TabularOutput
            {
                Rows = new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object> { { "id", 1 } }
                }
            });

        var mockDbStrategy = new Mock<IDatabaseStrategy>();
        mockDbStrategy.Setup(s => s.Save(It.IsAny<IJobOutput>()))
            .Returns(Task.CompletedTask);

        var pipeline = new DataPipeline
        {
            DatabaseStrategy = mockDbStrategy.Object
        };

        var job = new ParsingJob
        {
            Id = "integration-test-2",
            Priority = Enums.Priority.Medium,
            FilePath = "/data/file.csv",
            Parser = mockParser.Object,
            Fields = new List<string> { "id", "name" },
            Consumers = new List<IConsumer> { pipeline }
        };

        // Act
        var output = await job.Execute();
        foreach (var consumer in job.Consumers)
        {
            consumer.Consume(output);
        }

        // Assert
        Assert.NotNull(output);
        Assert.IsType<TabularOutput>(output);
        mockDbStrategy.Verify(s => s.Save(It.IsAny<TabularOutput>()), Times.Once);
    }
}