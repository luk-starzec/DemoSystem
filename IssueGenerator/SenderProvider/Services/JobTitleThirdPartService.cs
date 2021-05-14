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
    public class JobTitleThirdPartService : IJobTitleThirdPartService
    {
        private readonly HttpClient client;

        private readonly string defaultValue;

        public JobTitleThirdPartService(HttpClient client, IOptions<JobTitleSettings> options)
        {
            var settings = options.Value;

            defaultValue = settings.ThirdPartDefaultValue;

            client.BaseAddress = new Uri(settings.ThirdPartApiUrl);
            this.client = client;
        }

        public async Task<string> GetJobTitleThirdPartAsync()
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
