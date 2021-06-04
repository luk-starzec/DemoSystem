using EmployerWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IPriorityService
    {
        Task<List<PriorityModel>> GetTitlePrioritiesAsync();
        Task AddTitlePriorityAsync(PriorityModel priorityModel);
        Task SaveTitlePriorityAsync(PriorityModel priorityModel);
        Task DeleteTitlePriorityAsync(PriorityModel priorityModel);

        Task<List<PriorityModel>> GetAppPrioritiesAsync();
        Task AddAppPriorityAsync(PriorityModel priorityModel);
        Task SaveAppPriorityAsync(PriorityModel priorityModel);
        Task DeleteAppPriorityAsync(PriorityModel priorityModel);

        Task<List<string>> GetTitlesAsync();
        Task<List<string>> GetAppsAsync();
    }
}