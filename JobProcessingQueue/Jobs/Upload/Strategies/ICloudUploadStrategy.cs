namespace JobProcessingQueue.Jobs.Upload.Strategies;

public interface ICloudUploadStrategy
{
    Task Upload(string filePath);
}