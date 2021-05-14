using SenderProvider.Repo;
using SenderProvider.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderProvider.Services
{
    public class NameService : INameService
    {
        private readonly INameRepo repo;

        public NameService(INameRepo repo)
        {
            this.repo = repo;
        }

        public Task<string> GetNameAsync()
        {
            var firstName = repo.GetFirstName();
            var lastName = repo.GetLastName();
            var name = $"{firstName} {lastName}";

            return Task.FromResult(name);
        }
    }
}
