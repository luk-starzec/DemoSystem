using EmployerWebApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IDescriptionService
    {
        Task<List<(string name, int id)>> GetDescriptionSourceNamesAsync();
        Task<List<DescriptionSourceViewModel>> GetDescriptionSourcesAsync();
        Task<DescriptionSourceViewModel> AddDescriptionSourceAsync(DescriptionSourceViewModel descriptionSource);
        Task DeleteDescriptionSourceAsync(int id);
    }
}