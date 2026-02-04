using JobProcessingQueue.Jobs;

namespace JobProcessingQueue.Factories;

public interface IJobFactory
{
    Job CreateJob();
}