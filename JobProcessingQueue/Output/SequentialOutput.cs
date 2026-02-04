namespace JobProcessingQueue.Output;

public class SequentialOutput : IJobOutput
{
    public List<object> Values { get; set; }
}