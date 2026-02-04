using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs.Upload;
using JobProcessingQueue.Jobs.Upload.Strategies;
using Moq;

namespace JobProcessingQueue.Tests.Jobs;

public class UploadJobTests
{
    [Fact]
    public void UploadJob_ProducesOutput_ReturnsFalse()
    {
        // Arrange
        var job = new UploadJob();

        // Act
        var result = job.ProducesOutput();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UploadJob_Execute_WithAWSStrategy_CallsUpload()
    {
        // Arrange
        var mockStrategy = new Mock<ICloudUploadStrategy>();
        mockStrategy.Setup(s => s.Upload(It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var job = new UploadJob
        {
            FilePath = "/path/to/file.txt",
            UploadStrategy = mockStrategy.Object
        };

        // Act
        await job.Execute();

        // Assert
        mockStrategy.Verify(s => s.Upload("/path/to/file.txt"), Times.Once);
    }

    [Fact]
    public async Task UploadJob_Execute_ReturnsNull()
    {
        // Arrange
        var mockStrategy = new Mock<ICloudUploadStrategy>();
        var job = new UploadJob
        {
            FilePath = "/path/to/file.txt",
            UploadStrategy = mockStrategy.Object
        };

        // Act
        var result = await job.Execute();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void AWSUploadStrategy_HasStorageTypeProperty()
    {
        // Arrange
        var strategy = new AWSUploadStrategy
        {
            StorageType = Enums.AWSStorageType.S3
        };

        // Act & Assert
        Assert.Equal(Enums.AWSStorageType.S3, strategy.StorageType);
    }

    [Fact]
    public void AzureUploadStrategy_Cancel_DoesNotThrow()
    {
        // Arrange
        var strategy = new AzureUploadStrategy();

        // Act & Assert
        strategy.Cancel();
        Assert.NotNull(strategy);
    }

    [Fact]
    public void UploadJob_WithAzureStrategy_CanBeCancelled()
    {
        // Arrange
        var azureStrategy = new AzureUploadStrategy();
        var job = new UploadJob
        {
            UploadStrategy = azureStrategy
        };

        // Act
        job.Cancel();

        // Assert - No exception thrown
        Assert.NotNull(job);
    }
}