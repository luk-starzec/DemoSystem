using IssueGenerator.Models;
using System.Threading.Tasks;

namespace IssueGenerator.Services.Interfaces
{
    public interface IIssueService
    {
        Task<IssueModel[]> GenerateIssues(GenerationOptions options);
    }
}