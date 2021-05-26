using EmployerWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IDescriptionService
    {
        Task<List<(string name, int id)>> GetDescriptionSourceNamesAsync();
        Task<List<DescriptionSource>> GetDescriptionSourcesAsync();
        Task<DescriptionSource> AddDescriptionSourceAsync(DescriptionSource descriptionSource);
        Task DeleteDescriptionSourceAsync(int id);
    }
}