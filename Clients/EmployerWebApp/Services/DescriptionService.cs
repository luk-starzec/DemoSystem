using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public class DescriptionService : IDescriptionService
    {
        public Task<List<(string name, int id)>> GetTextSourcesAsync()
        {
            var list = new List<(string name, int id)>
            {
                new ("random", 0),
                new ("Lorem ipsum", 1),
            };
            return Task.FromResult(list);
        }
    }
}
