﻿using IssueGenerator.Models;
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
    public class HeaderService : IHeaderService
    {
        private readonly HttpClient client;

        public HeaderService(HttpClient client, IOptions<ServicesSettings> options)
        {
            client.BaseAddress = new Uri(options.Value.HeaderProviderUrl);
            this.client = client;
        }

        public async Task<HeaderModel> GetHeader()
        {
            var result = await client.GetStringAsync("/api/header");
            var model = JsonSerializer.Deserialize<HeaderModel>(result,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return model;
        }
    }
}
