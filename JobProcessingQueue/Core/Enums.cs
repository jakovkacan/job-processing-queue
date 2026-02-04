namespace JobProcessingQueue.Core;

public class Enums
{
    public enum AWSStorageType
    {
        S3,
        FileSystem
    }

    public enum CloudProvider
    {
        AWS,
        GCP,
        Azure
    }

    public enum DatabaseType
    {
        SQL,
        NoSQL
    }

    public enum FileFormat
    {
        CSV,
        XML,
        JSON
    }

    public enum NotificationType
    {
        SMS,
        Email,
        PushNotification
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum ProgrammingLanguageType
    {
        Compiled,
        Interpreted
    }
}