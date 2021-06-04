using HeaderProvider.Models;

namespace HeaderProvider.Services
{
    public interface IHeaderService
    {
        HeaderModel GetHeader();

        string[] GetApps();
        string[] GetTitles();
    }
}
