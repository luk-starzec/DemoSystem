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


        public async Task<List<PriorityListViewModel>> GetListsAsync()
        {
            var priorityCient = GetPrioritySetterClient();
            var headerClient = GetHeaderProviderClient();

            Activity.Current = null;
            return new List<PriorityListViewModel>
            {
                new(){
                    Group = await GetGroup<TitlePriorityApiModel>("Title", headerClient),
                    Items = await GetItemsAsync<TitlePriorityApiModel>("title",priorityCient),
                },
                new(){
                     Group = await GetGroup<AppPriorityApiModel>("App", headerClient),
                    Items = await GetItemsAsync<AppPriorityApiModel>("app",priorityCient),
                },
            };
        }

        private async Task<PriorityGroupViewModel> GetGroup<T>(string header, HttpClient headerClient) where T : IApiModel
        {
            var keys = await headerClient.GetFromJsonAsync<IEnumerable<string>>($"/api/header/{header}");
            return new PriorityGroupViewModel
            {
                Header = header,
                Keys = keys.ToList(),
                ApiPath = header,
                ApiModelType = typeof(T),
            };
        }

        private async Task<List<PriorityViewModel>> GetItemsAsync<T>(string path, HttpClient client) where T : IApiModel
        {
            var response = await client.GetFromJsonAsync<IEnumerable<T>>($"/api/{path}");
            return response.Select(r => r.ToViewModel()).ToList();
        }

        public async Task CreateAsync(PriorityViewModel item, PriorityGroupViewModel group)
        {
            var model = Activator.CreateInstance(group.ApiModelType, item);
            var client = GetPrioritySetterClient();
            Activity.Current = null;
            await client.PostAsJsonAsync($"/api/{group.ApiPath}", model);
        }

        public async Task UpdateAsync(PriorityViewModel item, PriorityGroupViewModel group)
        {
            var client = GetPrioritySetterClient();
            Activity.Current = null;
            await client.PutAsJsonAsync($"/api/{group.ApiPath}/{item.Name}", item.PriorityLevel);
        }

        public async Task DeleteAsync(PriorityViewModel item, PriorityGroupViewModel group)
        {
            var client = GetPrioritySetterClient();
            Activity.Current = null;
            await client.DeleteAsync($"/api/{group.ApiPath}/{item.Name}");
        }

    }
}
