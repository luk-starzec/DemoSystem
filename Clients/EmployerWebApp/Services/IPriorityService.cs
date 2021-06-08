using EmployerWebApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IPriorityService
    {
        Task<List<PriorityListViewModel>> GetPriorityTypesAsync();
        Task AddPriorityAsync(PriorityViewModel item, PriorityGroupViewModel group);
        Task SavePriorityAsync(PriorityViewModel item, PriorityGroupViewModel group);
        Task DeletePriorityAsync(PriorityViewModel item, PriorityGroupViewModel group);
    }
}