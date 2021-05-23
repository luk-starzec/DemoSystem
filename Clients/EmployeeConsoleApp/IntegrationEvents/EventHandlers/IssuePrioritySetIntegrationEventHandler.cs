using EmployeeConsoleApp.IntegrationEvents.Events;
using EventBus;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EmployeeConsoleApp.IntegrationEvents.EventHandlers
{
    public class IssuePrioritySetIntegrationEventHandler : IIntegrationEventHandler<IssuePrioritySetIntegrationEvent>
    {
        private readonly IIssueService issueService;
        private readonly ILogger<IssuePrioritySetIntegrationEventHandler> logger;

        public IssuePrioritySetIntegrationEventHandler(IIssueService issueService, ILogger<IssuePrioritySetIntegrationEventHandler> logger)
        {
            this.issueService = issueService;
            this.logger = logger;
        }

        public async Task Handle(IssuePrioritySetIntegrationEvent @event)
        {
            var issue = @event.Issue;

            logger.LogInformation("Received issue: {title} in {app} [{priority}]", issue.Title, issue.App, issue.Priority);

            await issueService.CompleteIssue(issue);
        }
    }
}
