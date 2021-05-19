
using EventBus;
using ReportService.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssuePrioritySetIntegrationEventHandler : IIntegrationEventHandler<IssuePrioritySetIntegrationEvent>
    {
        public Task Handle(IssuePrioritySetIntegrationEvent @event)
        {
            var issue = @event.Issue;
            Console.WriteLine($"Raport - app: {issue.App}, title: {issue.Title}, priority: {issue.Priority}");
            return Task.CompletedTask;
        }
    }
}
