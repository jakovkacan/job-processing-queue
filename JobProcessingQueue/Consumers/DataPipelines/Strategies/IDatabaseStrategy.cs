using JobProcessingQueue.Output;

namespace JobProcessingQueue.Consumers.DataPipelines.Strategies;

public interface IDatabaseStrategy
{
    Task Save(IJobOutput data);
}