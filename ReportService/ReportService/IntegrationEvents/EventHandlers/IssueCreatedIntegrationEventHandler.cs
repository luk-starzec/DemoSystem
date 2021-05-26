using EventBus;
using ReportService.IntegrationEvents.Events;
using ReportService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssueCreatedIntegrationEventHandler : IIntegrationEventHandler<IssueCreatedIntegrationEvent>
    {
        private readonly IIssueService issueService;

        public IssueCreatedIntegrationEventHandler(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        public Task Handle(IssueCreatedIntegrationEvent @event)
        {
            var issue = @event.Issue;

            Console.WriteLine($"Created - app: {issue.App}, title: {issue.Title}, desc: {issue.Description}");

            issueService.SetIssue(issue, @event);

            return Task.CompletedTask;
        }
    }
}
