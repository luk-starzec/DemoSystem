using EventBus;
using System;

namespace BasicIntegrationEventService
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IEventBus _eventBus;

        public IntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public void PublishThroughEventBus(IntegrationEvent evt)
        {
            try
            {
                _eventBus.Publish(evt);
            }
            catch (Exception)
            { }
        }
    }
}
