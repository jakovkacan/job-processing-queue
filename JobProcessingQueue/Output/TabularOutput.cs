namespace JobProcessingQueue.Output;

public class TabularOutput : IJobOutput
{
    public List<Dictionary<string, object>> Rows { get; set; }
}