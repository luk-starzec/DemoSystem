﻿
using EventBus;
using ReportService.IntegrationEvents.Events;
using ReportService.Services;
using System;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.EventHandlers
{
    public class IssuePrioritySetIntegrationEventHandler : IIntegrationEventHandler<IssuePrioritySetIntegrationEvent>
    {
        private readonly IIssueService issueService;

        public IssuePrioritySetIntegrationEventHandler(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        public Task Handle(IssuePrioritySetIntegrationEvent @event)
        {
            var issue = @event.Issue;

            Console.WriteLine($"Priority set - app: {issue.App}, title: {issue.Title}, priority: {issue.Priority}");
        
            issueService.SetIssue(issue, @event);

            return Task.CompletedTask;
        }
    }
}
