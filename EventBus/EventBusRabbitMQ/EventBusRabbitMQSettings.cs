namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQSettings
    {
        public const string EventBusSettingsKey = "EventBusRabbitMQSettings";

        public string Connection { get; set; }
        public int RetryCount { get; set; }

        public string SubscriptionClientName { get; set; }
    }
}
