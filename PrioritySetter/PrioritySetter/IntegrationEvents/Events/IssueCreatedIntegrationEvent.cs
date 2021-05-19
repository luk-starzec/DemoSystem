using EventBus;
using PrioritySetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.IntegrationEvents.Events
{
    public class IssueCreatedIntegrationEvent : IntegrationEvent
    {
        public IssueModel Issue { get; set; }

        public IssueCreatedIntegrationEvent()
        { }

        public IssueCreatedIntegrationEvent(IssueModel issue)
        {
            Issue = issue;
        }
    }
}
