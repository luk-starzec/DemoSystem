using EventBus;

namespace EmployeeConsoleApp.IntegrationEvents.Events
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
