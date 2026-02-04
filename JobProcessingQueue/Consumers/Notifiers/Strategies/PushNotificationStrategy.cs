namespace JobProcessingQueue.Consumers.Notifiers.Strategies;

public class PushNotificationStrategy : INotificationStrategy
{
    public async Task Send(string message, List<string> receivers)
    {
        /* ... */
    }
}