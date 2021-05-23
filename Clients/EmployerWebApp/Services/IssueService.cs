using EmployerWebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IssueService(HttpClient client, IOptions<ServicesSettings> options, ILogger<IssueService> logger)
        {
            client.BaseAddress = new Uri(options.Value.IssueGeneratorUrl);
            this.client = client;
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
