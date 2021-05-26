using EventBus;
using ReportService.Models;

namespace ReportService.Services
{
    public interface IIssueService
    {
        IssueModel[] GetIssues();
        void SetIssue(IssueModel issue, IntegrationEvent integrationEvent = null);
    }
}