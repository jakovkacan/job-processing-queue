using JobProcessingQueue.Output;

namespace JobProcessingQueue.Consumers;

public interface IConsumer
{
    void Consume(IJobOutput output);
}