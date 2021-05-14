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
    public class JobTitleSecondPartService : IJobTitleSecondPartService
    {
        private readonly HttpClient client;

        private readonly string defaultValue;

        public JobTitleSecondPartService(HttpClient client, IOptions<JobTitleSettings> options)
        {
            var settings = options.Value;

            defaultValue = settings.SecondPartDefaultValue;

            client.BaseAddress = new Uri(settings.SecondPartApiUrl);
            this.client = client;
        }

        public async Task<string> GetJobTitleSecondPartAsync()
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
