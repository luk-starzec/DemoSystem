using EmployeeWpfApp.IntegrationEvents.Events;
using EmployeeWpfApp.Services;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWpfApp.IntegrationEvents.EventHandlers
{
    public class IssuePrioritySetIntegrationEventHandler : IIntegrationEventHandler<IssuePrioritySetIntegrationEvent>
    {
        private readonly IIssueService issueService;

        public IssuePrioritySetIntegrationEventHandler(IIssueService issueService)
        {
            this.issueService = issueService ?? throw new ArgumentNullException(nameof(issueService));
        }

        public IssuePrioritySetIntegrationEventHandler()
        { }

        public Task Handle(IssuePrioritySetIntegrationEvent @event)
        {
            var issue = @event.Issue;
            issueService.AddIssue(issue);
            return Task.CompletedTask;
        }
    }
}
