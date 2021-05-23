using EventBus;
using ReportService.IntegrationEvents.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssueCreatedIntegrationEventHandler : IIntegrationEventHandler<IssueCreatedIntegrationEvent>
    {
        public Task Handle(IssueCreatedIntegrationEvent @event)
        {
            var issue = @event.Issue;
            Console.WriteLine($"Created - app: {issue.App}, title: {issue.Title}, desc: {issue.Description}");
            return Task.CompletedTask;
        }
    }
}
