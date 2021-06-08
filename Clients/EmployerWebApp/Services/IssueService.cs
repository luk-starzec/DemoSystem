using EmployerWebApp.Helpers;
using EmployerWebApp.ViewModels;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task GenerateIssuesAsync(IssueGenerationViewModel issueGeneration)
        {
            var model = new
            {
                IssuesCount = issueGeneration.IssuesCount,
                WordsLimit = issueGeneration.LimitWordsCount ? issueGeneration.MaxWordsCount : null,
                RandomWordsCount = issueGeneration.RandomizeWordsCount,
                TextSourceId = issueGeneration.TextSourceId > 0 ? issueGeneration.TextSourceId : (int?)null,
            };

            Activity.Current = null;
            await client.PostAsJsonAsync("/api/issue", model);
        }
    }
}
