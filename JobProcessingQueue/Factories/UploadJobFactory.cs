using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs;
using JobProcessingQueue.Jobs.Upload;
using JobProcessingQueue.Jobs.Upload.Strategies;

namespace JobProcessingQueue.Factories;

public class UploadJobFactory : IJobFactory
{
    private readonly string _filePath;
    private readonly Enums.Priority _priority;
    private readonly ICloudUploadStrategy _uploadStrategy;

    public Job CreateJob()
    {
        /* ... */
        return new UploadJob();
    }
}