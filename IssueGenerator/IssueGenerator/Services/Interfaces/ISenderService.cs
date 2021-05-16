using System.Threading.Tasks;

namespace IssueGenerator.Services.Interfaces
{
    public interface ISenderService
    {
        Task<string> GetSender();
    }
}