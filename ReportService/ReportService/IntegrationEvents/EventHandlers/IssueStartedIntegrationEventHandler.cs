using EventBus;
using ReportService.IntegrationEvents.Events;
using ReportService.Services;
using System;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssueStartedIntegrationEventHandler : IIntegrationEventHandler<IssueStartedIntegrationEvent>
    {
        private readonly IIssueService issueService;

        public IssueStartedIntegrationEventHandler(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        public Task Handle(IssueStartedIntegrationEvent @event)
        {
            var issue = @event.Issue;

            Console.WriteLine($"Started by {issue.Employee} - app: {issue.App}, title: {issue.Title}, priority: {issue.Priority}");

            issueService.SetIssue(issue, @event);

            return Task.CompletedTask;
        }
    }
}
