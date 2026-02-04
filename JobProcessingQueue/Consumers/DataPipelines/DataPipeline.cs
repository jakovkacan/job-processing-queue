using JobProcessingQueue.Consumers.DataPipelines.Strategies;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Consumers.DataPipelines;

public class DataPipeline : IConsumer
{
    public IDatabaseStrategy DatabaseStrategy { get; set; }

    public void Consume(IJobOutput output)
    {
        /* ... */
    }
}