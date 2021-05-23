using EventBus;
using ReportService.Models;

namespace ReportService.IntegrationEvents.Events
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
