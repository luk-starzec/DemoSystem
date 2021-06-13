using PrioritySetter.Models;
using System.Threading.Tasks;

namespace PrioritySetter.Services
{
    public interface IIssueService
    {
        Task SetPriority(IssueModel issue);
    }
}