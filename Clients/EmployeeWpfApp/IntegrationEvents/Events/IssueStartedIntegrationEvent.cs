using EmployeeWpfApp.Models;
using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWpfApp.IntegrationEvents.Events
{
    public class IssueStartedIntegrationEvent : IntegrationEvent
    {
        public IssueModel Issue { get; set; }

        public IssueStartedIntegrationEvent()
        { }

        public IssueStartedIntegrationEvent(IssueModel issue)
        {
            Issue = issue;
        }
    }
}
