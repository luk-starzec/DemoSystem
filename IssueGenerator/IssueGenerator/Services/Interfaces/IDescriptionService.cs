using System.Threading.Tasks;

namespace IssueGenerator.Services.Interfaces
{
    public interface IDescriptionService
    {
        Task<string> GetDescription(int? textSetId, int? wordsLimit);
    }
}