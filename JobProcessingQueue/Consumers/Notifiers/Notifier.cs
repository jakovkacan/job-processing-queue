using JobProcessingQueue.Consumers.Notifiers.Strategies;
using JobProcessingQueue.Output;

namespace JobProcessingQueue.Consumers.Notifiers;

public class Notifier : IConsumer
{
    public string Message { get; set; }
    public List<string> Receivers { get; set; }
    public INotificationStrategy NotificationStrategy { get; set; }

    public void Consume(IJobOutput output)
    {
        /* ... */
    }
}