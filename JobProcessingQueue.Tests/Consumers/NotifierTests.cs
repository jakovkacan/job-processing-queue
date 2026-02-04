using JobProcessingQueue.Consumers.Notifiers;
using JobProcessingQueue.Consumers.Notifiers.Strategies;
using JobProcessingQueue.Output;
using Moq;

namespace JobProcessingQueue.Tests.Consumers;

public class NotifierTests
{
    [Fact]
    public void Notifier_Constructor_InitializesReceiversList()
    {
        // Arrange & Act
        var notifier = new Notifier();

        // Assert
        Assert.NotNull(notifier.Receivers);
        Assert.Empty(notifier.Receivers);
    }

    [Fact]
    public void Notifier_Consume_CallsNotificationStrategy()
    {
        // Arrange
        var mockStrategy = new Mock<INotificationStrategy>();
        mockStrategy.Setup(s => s.Send(It.IsAny<string>(), It.IsAny<List<string>>()))
            .Returns(Task.CompletedTask);

        var notifier = new Notifier
        {
            Message = "Job completed",
            Receivers = ["user@example.com"],
            NotificationStrategy = mockStrategy.Object
        };

        var output = new TextOutput { Content = "Result" };

        // Act
        notifier.Consume(output);

        // Assert
        mockStrategy.Verify(s => s.Send("Job completed", notifier.Receivers), Times.Once);
    }

    [Theory]
    [InlineData("SMS notification")]
    [InlineData("Email notification")]
    [InlineData("Push notification")]
    public void Notifier_CanSetDifferentMessages(string message)
    {
        // Arrange
        var notifier = new Notifier { Message = message };

        // Act & Assert
        Assert.Equal(message, notifier.Message);
    }
}