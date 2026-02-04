using JobProcessingQueue.Core;
using JobProcessingQueue.Jobs;
using JobProcessingQueue.Jobs.Code;

namespace JobProcessingQueue.Queue;

public class JobProcessingQueue
{
    private static JobProcessingQueue _instance;
    private static readonly object _lock = new();
    private readonly SortedDictionary<Enums.Priority, Queue<Job>> _priorityQueues;

    public static JobProcessingQueue Instance { get; }

    public void EnqueueJob(Job job)
    {
        /* ... */
    }

    public Job DequeueJob()
    {
        /* ... */
        return new CodeJob();
    }

    public async Task ProcessJobs()
    {
        /* ... */
    }
}