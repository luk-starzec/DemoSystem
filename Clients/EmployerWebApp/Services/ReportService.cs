using EmployerWebApp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public class ReportService : IReportService
    {
        private readonly HttpClient client;
        private readonly ILogger<IssueService> logger;

        public ReportService(HttpClient client, IOptions<ServicesSettings> options, ILogger<IssueService> logger)
        {
            client.BaseAddress = new Uri(options.Value.ReportServiceUrl);
            this.client = client;
            this.logger = logger;
        }

        public async Task<List<IssueModel>> GetLastIssuesAsync()
        {
            var response = await client.GetFromJsonAsync<List<IssueModel>>("/api/issues", new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response;
        }
    }
}
