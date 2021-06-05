using EmployerWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IPriorityService
    {
        Task<List<PriorityViewModel>> GetTitlePrioritiesAsync();
        Task AddTitlePriorityAsync(PriorityViewModel viewModel);
        Task SaveTitlePriorityAsync(PriorityViewModel viewModel);
        Task DeleteTitlePriorityAsync(PriorityViewModel viewModel);

        Task<List<PriorityViewModel>> GetAppPrioritiesAsync();
        Task AddAppPriorityAsync(PriorityViewModel viewModel);
        Task SaveAppPriorityAsync(PriorityViewModel viewModel);
        Task DeleteAppPriorityAsync(PriorityViewModel viewModel);

        Task<List<string>> GetTitlesAsync();
        Task<List<string>> GetAppsAsync();
    }
}