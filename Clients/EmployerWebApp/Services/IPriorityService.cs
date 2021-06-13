using EmployerWebApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IPriorityService
    {
        Task<List<PriorityListViewModel>> GetListsAsync();
        Task CreateAsync(PriorityViewModel item, PriorityGroupViewModel group);
        Task UpdateAsync(PriorityViewModel item, PriorityGroupViewModel group);
        Task DeleteAsync(PriorityViewModel item, PriorityGroupViewModel group);
    }
}