using EventBus;
using ReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.IntegrationEvents.Events
{
    public class IssuePrioritySetIntegrationEvent : IntegrationEvent
    {
        public IssueModel Issue { get; set; }

        public IssuePrioritySetIntegrationEvent()
        { }

        public IssuePrioritySetIntegrationEvent(IssueModel issue)
        {
            Issue = issue;
        }
    }
}
