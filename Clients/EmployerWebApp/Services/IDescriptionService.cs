using EmployerWebApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IDescriptionService
    {
        Task<List<(string name, int id)>> GetNamesAsync();
        Task<List<DescriptionViewModel>> GetAsync();
        Task<DescriptionViewModel> CreateAsync(DescriptionViewModel descriptionSource);
        Task DeleteAsync(int id);
    }
}