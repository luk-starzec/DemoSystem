using PrioritySetter.Models;
using System.Threading.Tasks;

namespace PrioritySetter.Services
{
    public interface IPriorityService
    {
        Task SetIssuePriority(IssueModel issue);
    }
}