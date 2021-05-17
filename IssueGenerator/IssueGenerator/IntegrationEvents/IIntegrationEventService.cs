using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.IntegrationEvents
{
    public interface IIntegrationEventService
    {
        void PublishThroughEventBus(IntegrationEvent evt);
    }
}
