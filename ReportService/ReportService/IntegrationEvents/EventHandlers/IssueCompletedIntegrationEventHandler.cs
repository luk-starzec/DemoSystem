using EventBus;
using ReportService.IntegrationEvents.Events;
using ReportService.Services;
using System;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssueCompletedIntegrationEventHandler : IIntegrationEventHandler<IssueCompletedIntegrationEvent>
    {
        private readonly IIssueService issueService;

        public IssueCompletedIntegrationEventHandler(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        public Task Handle(IssueCompletedIntegrationEvent @event)
        {
            var issue = @event.Issue;

            Console.WriteLine($"Completed by {issue.Employee} - app: {issue.App}, title: {issue.Title}, priority: {issue.Priority}");

            issueService.SetIssue(issue, @event);

            return Task.CompletedTask;
        }
    }
}
