namespace JobProcessingQueue.Jobs.Upload.Strategies;

public class AzureUploadStrategy : ICloudUploadStrategy
{
    private CancellationTokenSource _cancellationTokenSource;

    public async Task Upload(string filePath)
    {
        /* ... */
    }

    public void Cancel()
    {
        /* ... */
    }
}