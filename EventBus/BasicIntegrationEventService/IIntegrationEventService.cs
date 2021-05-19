using EventBus;

namespace BasicIntegrationEventService
{
    public interface IIntegrationEventService
    {
        void PublishThroughEventBus(IntegrationEvent evt);
    }
}