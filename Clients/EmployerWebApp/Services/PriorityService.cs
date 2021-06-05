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

        public async Task<List<PriorityViewModel>> GetTitlePrioritiesAsync()
        {
            var client = GetPrioritySetterClient();
            var response = await client.GetFromJsonAsync<IEnumerable<TitlePriorityApiModel>>("/api/title");
            return response.Select(r => r.ToPriorityModel()).ToList();
        }

        public async Task<List<PriorityViewModel>> GetAppPrioritiesAsync()
        {
            var client = GetPrioritySetterClient();
            var response = await client.GetFromJsonAsync<IEnumerable<AppPriorityApiModel>>("/api/app");
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

        public async Task AddTitlePriorityAsync(PriorityViewModel priorityModel)
        {
            var model = new TitlePriorityApiModel
            {
                Title = priorityModel.Name,
                PriorityLevelId = (int)priorityModel.PriorityLevel,
            };
            var client = GetPrioritySetterClient();
            await client.PostAsJsonAsync("/api/title", model);
        }

        public async Task SaveTitlePriorityAsync(PriorityViewModel viewModel)
        {
            var client = GetPrioritySetterClient();
            await client.PutAsJsonAsync($"/api/title/{viewModel.Name}", viewModel.PriorityLevel);
        }

        public async Task DeleteTitlePriorityAsync(PriorityViewModel viewModel)
        {
            var client = GetPrioritySetterClient();
            await client.DeleteAsync($"/api/title/{viewModel.Name}");
        }

        public async Task AddAppPriorityAsync(PriorityViewModel viewModel)
        {
            var model = new AppPriorityApiModel
            {
                App = viewModel.Name,
                PriorityLevelId = (int)viewModel.PriorityLevel,
            };
            var client = GetPrioritySetterClient();
            await client.PostAsJsonAsync("/api/app", model);
        }

        public async Task SaveAppPriorityAsync(PriorityViewModel viewModel)
        {
            var client = GetPrioritySetterClient();
            await client.PutAsJsonAsync($"/api/app/{viewModel.Name}", viewModel.PriorityLevel);
        }

        public async Task DeleteAppPriorityAsync(PriorityViewModel viewModel)
        {
            var client = GetPrioritySetterClient();
            await client.DeleteAsync($"/api/app/{viewModel.Name}");
        }

        private HttpClient GetPrioritySetterClient() 
            => clientFactory.CreateClient(HttpClientNames.PrioritySetterClient);

        private HttpClient GetHeaderProviderClient() 
            => clientFactory.CreateClient(HttpClientNames.HeaderProviderClient);
    }
}
