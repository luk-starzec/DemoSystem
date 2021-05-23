using EventBus;

namespace EmployeeConsoleApp.IntegrationEvents.Events
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
