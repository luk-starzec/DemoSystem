using IssueGenerator.Models;
using System.Threading.Tasks;

namespace IssueGenerator.Services.Interfaces
{
    public interface IHeaderService
    {
        Task<HeaderModel> GetHeader();
    }
}