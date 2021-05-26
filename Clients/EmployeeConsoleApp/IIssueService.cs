using System.Threading.Tasks;

namespace EmployeeConsoleApp
{
    public interface IIssueService
    {
        Task CompleteIssueAsync(IssueModel issue);
    }
}