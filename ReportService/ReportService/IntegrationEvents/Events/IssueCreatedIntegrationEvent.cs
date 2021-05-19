using EventBus;
using ReportService.Models;

namespace ReportService.IntegrationEvents.Events
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
