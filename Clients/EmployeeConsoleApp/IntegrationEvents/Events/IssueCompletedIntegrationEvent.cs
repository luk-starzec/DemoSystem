using EventBus;

namespace EmployeeConsoleApp.IntegrationEvents.Events
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
