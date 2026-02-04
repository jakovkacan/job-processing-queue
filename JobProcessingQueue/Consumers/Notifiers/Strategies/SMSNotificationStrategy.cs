namespace JobProcessingQueue.Consumers.Notifiers.Strategies;

public class SMSNotificationStrategy : INotificationStrategy
{
    public async Task Send(string message, List<string> receivers)
    {
        /* ... */
    }
}