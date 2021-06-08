using EmployerWebApp.ApiModels;
using EmployerWebApp.Helpers;
using EmployerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private HttpClient GetPrioritySetterClient()
            => clientFactory.CreateClient(HttpClientNames.PrioritySetterClient);

        private HttpClient GetHeaderProviderClient()
            => clientFactory.CreateClient(HttpClientNames.HeaderProviderClient);


        public async Task<List<PriorityListViewModel>> GetPriorityTypesAsync()
        {
            var priorityCient = GetPrioritySetterClient();
            var headerClient = GetHeaderProviderClient();

            Activity.Current = null;
            return new List<PriorityListViewModel>
            {
                new(){
                    Group = new ()
                    {
                        Header = "Title",
                        Keys = await GetKeysAsync(headerClient,"title"),
                        ApiPath = "title",
                        ApiModelType = typeof(TitlePriorityApiModel),
                    },
                    Items = await GetItemsAsync<TitlePriorityApiModel>(priorityCient,"title"),
                },
                new(){
                    Group = new ()
                    {
                        Header = "App",
                        Keys = await GetKeysAsync(headerClient,"app"),
                        ApiPath = "app",
                        ApiModelType = typeof(AppPriorityApiModel),
                    },
                    Items = await GetItemsAsync<AppPriorityApiModel>(priorityCient,"app"),
                },
            };
        }

        private async Task<List<PriorityViewModel>> GetItemsAsync<T>(HttpClient client, string path) where T : IApiModel
        {
            var response = await client.GetFromJsonAsync<IEnumerable<T>>($"/api/{path}");
            return response.Select(r => r.ToViewModel()).ToList();
        }

        private async Task<List<string>> GetKeysAsync(HttpClient client, string path)
        {
            var response = await client.GetFromJsonAsync<IEnumerable<string>>($"/api/header/{path}");
            return response.ToList();
        }


        public async Task AddPriorityAsync(PriorityViewModel item, PriorityGroupViewModel group)
        {
            var model = Activator.CreateInstance(group.ApiModelType, item);
            var client = GetPrioritySetterClient();
            Activity.Current = null;
            await client.PostAsJsonAsync($"/api/{group.ApiPath}", model);
        }

        public async Task SavePriorityAsync(PriorityViewModel item, PriorityGroupViewModel group)
        {
            var client = GetPrioritySetterClient();
            Activity.Current = null;
            await client.PutAsJsonAsync($"/api/{group.ApiPath}/{item.Name}", item.PriorityLevel);
        }

        public async Task DeletePriorityAsync(PriorityViewModel item, PriorityGroupViewModel group)
        {
            var client = GetPrioritySetterClient();
            Activity.Current = null;
            await client.DeleteAsync($"/api/{group.ApiPath}/{item.Name}");
        }

    }
}
