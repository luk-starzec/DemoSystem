using System.Threading.Tasks;

namespace EmployeeConsoleApp
{
    public interface IIssueService
    {
        Task CompleteIssue(IssueModel issue);
    }
}