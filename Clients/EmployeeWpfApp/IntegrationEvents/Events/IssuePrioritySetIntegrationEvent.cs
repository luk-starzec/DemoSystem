using EmployeeWpfApp.Models;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWpfApp.IntegrationEvents.Events
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
