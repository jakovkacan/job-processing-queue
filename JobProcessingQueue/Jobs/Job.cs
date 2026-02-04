using JobProcessingQueue.Consumers;
using JobProcessingQueue.Core;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Jobs;

public abstract class Job
{
    public string Id { get; set; }
    public Enums.Priority Priority { get; set; }
    public List<IConsumer> Consumers { get; set; }

    public abstract Task<IJobOutput> Execute();
    public abstract bool ProducesOutput();
}