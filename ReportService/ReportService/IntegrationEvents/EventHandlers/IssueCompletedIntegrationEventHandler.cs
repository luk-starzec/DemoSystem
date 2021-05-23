using EventBus;
using ReportService.IntegrationEvents.Events;
using System;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssueCompletedIntegrationEventHandler : IIntegrationEventHandler<IssueCompletedIntegrationEvent>
    {
        public Task Handle(IssueCompletedIntegrationEvent @event)
        {
            var issue = @event.Issue;
            Console.WriteLine($"Completed by {issue.AssignedUser} - app: {issue.App}, title: {issue.Title}, priority: {issue.Priority}");
            return Task.CompletedTask;
        }
    }
}
