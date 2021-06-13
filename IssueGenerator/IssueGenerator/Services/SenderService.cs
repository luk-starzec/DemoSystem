using IssueGenerator.Models;
using IssueGenerator.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IssueGenerator.Services
{
    public class SenderService : ISenderService
    {
        private readonly HttpClient client;

        public SenderService(HttpClient client, IOptions<ServicesSettings> options)
        {
            client.BaseAddress = new Uri(options.Value.SenderProviderUrl);
            this.client = client;
        }

        public async Task<string> GetSender()
        {
            var model = await client.GetFromJsonAsync<SenderModel>("/api/sender");
            return $"{model.Name}{Environment.NewLine}{model.JobTitle}";
        }
    }
}
