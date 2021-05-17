using EventBus;
using IssueGenerator.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.IntegrationEvents.EventHandlers
{
    public class IssueCreatedIntegrationEventHandler : IIntegrationEventHandler<IssueCreatedIntegrationEvent>
    {
        public Task Handle(IssueCreatedIntegrationEvent @event)
        {
            Console.WriteLine($"Recived: {@event.Title}");
            return Task.CompletedTask;
        }
    }
}
