using EventBus;
using ReportService.IntegrationEvents.Events;
using ReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Services
{
    public class IssueService : IIssueService
    {
        private readonly List<IssueModel> issues = new();

        public IssueModel[] GetIssues()
        {
            return issues.ToArray();
        }

        public void SetIssue(IssueModel issue, IntegrationEvent integrationEvent = null)
        {
            var existingIssue = issues.Where(r => r.Id == issue.Id).SingleOrDefault();

            if (existingIssue is null)
            {
                existingIssue = new IssueModel
                {
                    Id = issue.Id,
                    Created = issue.Created
                };
                issues.Add(existingIssue);
            }

            UpdateIssue(existingIssue, issue);

            AddLog(existingIssue, integrationEvent);
        }

        private static void UpdateIssue(IssueModel issue, IssueModel source)
        {
            issue.App = source.App;
            issue.Title = source.Title;
            issue.Description = source.Description;
            issue.Sender = source.Sender;
            issue.Priority = source.Priority;
            issue.Employee = source.Employee;
        }

        private void AddLog(IssueModel issue, IntegrationEvent integrationEvent)
        {
            if (integrationEvent is null)
                return;

            var log = new EventLogModel
            {
                Date = DateTimeOffset.Now,
                Description = GetLogDescription(integrationEvent),
            };
            issue.Logs.Add(log);
        }

        private static string GetLogDescription(IntegrationEvent integrationEvent)
        {
            if (integrationEvent.GetType() == typeof(IssueCreatedIntegrationEvent))
                return "Created";

            if (integrationEvent.GetType() == typeof(IssuePrioritySetIntegrationEvent))
                return $"Priority set to: {((IssuePrioritySetIntegrationEvent)integrationEvent).Issue.Priority}";
            
            if (integrationEvent.GetType() == typeof(IssueStartedIntegrationEvent))
                return $"Started by: {((IssueStartedIntegrationEvent)integrationEvent).Issue.Employee}";
            
            if (integrationEvent.GetType() == typeof(IssueCompletedIntegrationEvent))
                return $"Completed by: {((IssueCompletedIntegrationEvent)integrationEvent).Issue.Employee}";

            return "something happened";
        }
    }
}
