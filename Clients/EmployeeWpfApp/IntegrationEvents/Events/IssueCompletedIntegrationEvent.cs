using EmployeeWpfApp.Models;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWpfApp.IntegrationEvents.Events
{
    public class IssueCompletedIntegrationEvent : IntegrationEvent
    {
        public IssueModel Issue { get; set; }

        public IssueCompletedIntegrationEvent()
        { }

        public IssueCompletedIntegrationEvent(IssueModel issue)
        {
            Issue = issue;
        }
    }
}
