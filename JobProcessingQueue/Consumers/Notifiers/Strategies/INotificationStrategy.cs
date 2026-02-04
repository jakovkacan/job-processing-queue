namespace JobProcessingQueue.Consumers.Notifiers.Strategies;

public interface INotificationStrategy
{
    Task Send(string message, List<string> receivers);
}