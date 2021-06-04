using EmployerWebApp.Helpers;
using EmployerWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly IHttpClientFactory clientFactory;

        public PriorityService(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public async Task<List<PriorityModel>> GetTitlePrioritiesAsync()
        {
            var client = GetPrioritySetterClient();
            var response = await client.GetFromJsonAsync<IEnumerable<TitlePriorityModel>>("/api/title");
            return response.Select(r => r.ToPriorityModel()).ToList();
        }

        public async Task<List<PriorityModel>> GetAppPrioritiesAsync()
        {
            var client = GetPrioritySetterClient();
            var response = await client.GetFromJsonAsync<IEnumerable<AppPriorityModel>>("/api/app");
            return response.Select(r => r.ToPriorityModel()).ToList();
        }

        public async Task<List<string>> GetTitlesAsync()
        {
            var client = GetHeaderProviderClient();
            var response = await client.GetFromJsonAsync<IEnumerable<string>>("/api/header/titles");
            return response.ToList();
        }

        public async Task<List<string>> GetAppsAsync()
        {
            var client = GetHeaderProviderClient();
            var response = await client.GetFromJsonAsync<IEnumerable<string>>("/api/header/apps");
            return response.ToList();
        }

        public async Task AddTitlePriorityAsync(PriorityModel priorityModel)
        {
            var model = new TitlePriorityModel
            {
                Title = priorityModel.Name,
                PriorityLevelId = (int)priorityModel.Priority,
            };

            var client = GetPrioritySetterClient();
            await client.PostAsJsonAsync("/api/title", model);
        }

        public async Task SaveTitlePriorityAsync(PriorityModel priorityModel)
        {
            var client = GetPrioritySetterClient();
            await client.PutAsJsonAsync($"/api/title/{priorityModel.Name}", priorityModel.Priority);
        }

        public async Task DeleteTitlePriorityAsync(PriorityModel priorityModel)
        {
            var client = GetPrioritySetterClient();
            await client.DeleteAsync($"/api/title/{priorityModel.Name}");
        }

        public async Task AddAppPriorityAsync(PriorityModel priorityModel)
        {
            var model = new AppPriorityModel
            {
                App = priorityModel.Name,
                PriorityLevelId = (int)priorityModel.Priority,
            };

            var client = GetPrioritySetterClient();
            await client.PostAsJsonAsync("/api/app", model);
        }

        public async Task SaveAppPriorityAsync(PriorityModel priorityModel)
        {
            var client = GetPrioritySetterClient();
            await client.PutAsJsonAsync($"/api/app/{priorityModel.Name}", priorityModel.Priority);
        }

        public async Task DeleteAppPriorityAsync(PriorityModel priorityModel)
        {
            var client = GetPrioritySetterClient();
            await client.DeleteAsync($"/api/app/{priorityModel.Name}");
        }

        private HttpClient GetPrioritySetterClient() 
            => clientFactory.CreateClient(HttpClientNames.PrioritySetterClient);

        private HttpClient GetHeaderProviderClient() 
            => clientFactory.CreateClient(HttpClientNames.HeaderProviderClient);
    }
}
