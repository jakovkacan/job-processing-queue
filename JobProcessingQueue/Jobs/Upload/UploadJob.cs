using JobProcessingQueue.Jobs.Upload.Strategies;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs.Upload;

public class UploadJob : Job, ICancellable
{
    public string FilePath { get; set; }
    public ICloudUploadStrategy UploadStrategy { get; set; }

    public void Cancel()
    {
        /* ... */
    }

    public override async Task<IJobOutput> Execute()
    {
        /* ... */
        await UploadStrategy.Upload(FilePath);
        return await Task.FromResult<IJobOutput>(new VoidOutput());
    }

    public override bool ProducesOutput()
    {
        return false;
    }
}