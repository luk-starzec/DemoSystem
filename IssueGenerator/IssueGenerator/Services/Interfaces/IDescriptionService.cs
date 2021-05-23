using System.Threading.Tasks;

namespace IssueGenerator.Services.Interfaces
{
    public interface IDescriptionService
    {
        Task<string> GetDescriptionAsync(int? textSetId, int? wordsLimit, bool randomWordsCount);
        Task Test();
    }
}