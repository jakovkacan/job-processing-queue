namespace JobProcessingQueue.Consumers.Notifiers.Strategies;

public class EmailNotificationStrategy : INotificationStrategy
{
    public async Task Send(string message, List<string> receivers)
    {
        /* ... */
    }
}