using BasicIntegrationEventService;
using EventBus;
using PrioritySetter.Data;
using PrioritySetter.IntegrationEvents.Events;
using PrioritySetter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.IntegrationEvents.EventHandlers
{
    public class IssueCreatedIntegrationEventHandler : IIntegrationEventHandler<IssueCreatedIntegrationEvent>
    {
        private readonly IIssueService priorityService;

        public IssueCreatedIntegrationEventHandler(IIssueService priorityService)
        {
            this.priorityService = priorityService;
        }

        public async Task Handle(IssueCreatedIntegrationEvent @event)
        {
            var issue = @event.Issue;
            Console.WriteLine($"Recived - app: {issue.App}, title: {issue.Title}");

            await Task.Delay(3000);

            await priorityService.SetPriority(issue);
        }

        //public async Task Handle(IssueCreatedIntegrationEvent @event)
        //{
        //    var issue = @event.Issue;
        //    Console.WriteLine($"Recived - app: {issue.App}, title: {issue.Title}");

        //    await Task.Delay(3000);

        //    //await priorityService.SetIssuePriority(issue);

        //    Console.WriteLine("Set");

        //    //return Task.CompletedTask;
        //}

    }
}
