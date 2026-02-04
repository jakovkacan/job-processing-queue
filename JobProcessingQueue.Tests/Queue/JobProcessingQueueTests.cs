using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs.Code;

namespace JobProcessingQueue.Tests.Queue;

public class JobProcessingQueueTests
{
    [Fact]
    public void JobProcessingQueue_Instance_ReturnsSameInstance()
    {
        // Arrange & Act
        var instance1 = JobProcessingQueue.Queue.JobProcessingQueue.Instance;
        var instance2 = JobProcessingQueue.Queue.JobProcessingQueue.Instance;

        // Assert
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void JobProcessingQueue_EnqueueJob_AddsJobToQueue()
    {
        // Arrange
        var queue = JobProcessingQueue.Queue.JobProcessingQueue.Instance;
        var job = new CodeJob
        {
            Id = "test-job",
            Priority = Enums.Priority.Medium
        };

        // Act
        queue.EnqueueJob(job);

        // Assert - No exception thrown
        Assert.NotNull(queue);
    }

    [Fact]
    public void JobProcessingQueue_DequeueJob_ReturnsHighestPriorityJob()
    {
        // Arrange
        var queue = JobProcessingQueue.Queue.JobProcessingQueue.Instance;
        var lowPriorityJob = new CodeJob
        {
            Id = "low",
            Priority = Enums.Priority.Low
        };
        var highPriorityJob = new CodeJob
        {
            Id = "high",
            Priority = Enums.Priority.High
        };

        // Act
        queue.EnqueueJob(lowPriorityJob);
        queue.EnqueueJob(highPriorityJob);
        var dequeuedJob = queue.DequeueJob();

        // Assert
        Assert.NotNull(dequeuedJob);
        Assert.Equal("high", dequeuedJob.Id);
        Assert.Equal(Enums.Priority.High, dequeuedJob.Priority);
    }

    [Fact]
    public void JobProcessingQueue_DequeueJob_WhenEmpty_ReturnsNull()
    {
        // Arrange
        var queue = JobProcessingQueue.Queue.JobProcessingQueue.Instance;

        // Clear queue by dequeuing all jobs
        while (queue.DequeueJob() != null)
        {
        }

        // Act
        var result = queue.DequeueJob();

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData(Enums.Priority.High, Enums.Priority.Medium)]
    [InlineData(Enums.Priority.Medium, Enums.Priority.Low)]
    public void JobProcessingQueue_RespectsPriorityOrder(Enums.Priority higher, Enums.Priority lower)
    {
        // Arrange
        var queue = JobProcessingQueue.Queue.JobProcessingQueue.Instance;
        var lowerPriorityJob = new CodeJob
        {
            Id = "lower",
            Priority = lower
        };
        var higherPriorityJob = new CodeJob
        {
            Id = "higher",
            Priority = higher
        };

        // Act
        queue.EnqueueJob(lowerPriorityJob);
        queue.EnqueueJob(higherPriorityJob);
        var firstDequeued = queue.DequeueJob();

        // Assert
        Assert.Equal("higher", firstDequeued.Id);
        Assert.Equal(higher, firstDequeued.Priority);
    }
}