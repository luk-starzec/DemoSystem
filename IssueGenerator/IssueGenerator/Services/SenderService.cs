using IssueGenerator.Models;
using IssueGenerator.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            var result = await client.GetStringAsync("/api/sender");
            var model = JsonSerializer.Deserialize<SenderModel>(result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return $"{model.Name}{Environment.NewLine}{model.JobTitle}";
        }
    }
}
