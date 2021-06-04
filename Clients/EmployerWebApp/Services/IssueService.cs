using EmployerWebApp.Helpers;
using EmployerWebApp.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public class IssueService : IIssueService
    {
        private readonly HttpClient client;
        private readonly ILogger<IssueService> logger;

        public IssueService(IHttpClientFactory clientFactory, ILogger<IssueService> logger)
        {
            this.client = clientFactory.CreateClient(HttpClientNames.IssueGeneratorClient);
            this.logger = logger;
        }

        public async Task GenerateIssuesAsync(IssueGenerationModel issueGeneration)
        {
            var model = new
            {
                IssuesCount = issueGeneration.IssuesCount,
                WordsLimit = issueGeneration.LimitWordsCount ? issueGeneration.MaxWordsCount : null,
                RandomWordsCount = issueGeneration.RandomizeWordsCount,
                TextSourceId = issueGeneration.TextSourceId > 0 ? issueGeneration.TextSourceId : (int?)null,
            };
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await client.PostAsync("/api/issue", content);
        }
    }
}
