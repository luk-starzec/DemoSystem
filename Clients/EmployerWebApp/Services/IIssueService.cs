using EmployerWebApp.Models;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IIssueService
    {
        Task GenerateIssuesAsync(IssueGenerationModel issueGeneration);
    }
}