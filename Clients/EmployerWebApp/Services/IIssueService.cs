using EmployerWebApp.ViewModels;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IIssueService
    {
        Task GenerateIssuesAsync(IssueGenerationViewModel issueGeneration);
    }
}