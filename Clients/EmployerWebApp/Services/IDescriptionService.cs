using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public interface IDescriptionService
    {
        Task<List<(string name, int id)>> GetTextSourcesAsync();
    }
}