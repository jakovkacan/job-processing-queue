using JobProcessingQueue.Output;

namespace JobProcessingQueue.Consumers.DataPipelines.Strategies;

public class NoSQLDatabaseStrategy : IDatabaseStrategy
{
    public string ConnectionString { get; set; }

    public async Task Save(IJobOutput data)
    {
        /* ... */
    }
}