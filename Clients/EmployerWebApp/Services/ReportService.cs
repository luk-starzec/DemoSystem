﻿using EmployerWebApp.Helpers;
using EmployerWebApp.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        public ReportService(IHttpClientFactory clientFactory, ILogger<IssueService> logger)
        {
            this.client = clientFactory.CreateClient(HttpClientNames.ReportServerClient);
            this.logger = logger;
        }

        public async Task<List<IssueViewModel>> GetLastIssuesAsync()
        {
            var response = await client.GetFromJsonAsync<List<IssueViewModel>>("/api/issues", new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response;
        }
    }
}
