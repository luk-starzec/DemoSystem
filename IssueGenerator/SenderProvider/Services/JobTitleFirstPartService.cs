using Microsoft.Extensions.Options;
using SenderProvider.Models;
using SenderProvider.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SenderProvider.Services
{
    public class JobTitleFirstPartService : IJobTitleFirstPartService
    {
        private readonly HttpClient client;

        private readonly string defaultValue;

        public JobTitleFirstPartService(HttpClient client, IOptions<JobTitleSettings> options)
        {
            var settings = options.Value;

            defaultValue = settings.FirstPartDefaultValue;

            client.BaseAddress = new Uri(settings.FirstPartApiUrl);
            this.client = client;
        }

        public async Task<string> GetJobTitleFirstPartAsync()
        {
            try
            {
                return await client.GetStringAsync("/data");
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
