using JobProcessingQueue.Core;

namespace JobProcessingQueue.Jobs.Upload.Strategies;

public class AWSUploadStrategy : ICloudUploadStrategy
{
    public Enums.AWSStorageType StorageType { get; set; }

    public async Task Upload(string filePath)
    {
        /* ... */
    }
}