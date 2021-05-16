using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DescriptionProvider;
using Microsoft.Extensions.Options;
using IssueGenerator.Models;
using IssueGenerator.Services.Interfaces;

namespace IssueGenerator.Services
{
    public class DescriptionService : IDescriptionService
    {
        private readonly string descriptionProviderUrl;

        public DescriptionService(IOptions<ServicesSettings> options)
        {
            descriptionProviderUrl = options.Value.DescriptionProviderUrl;
        }

        public async Task<string> GetDescription(int? textSetId, int? wordsLimit)
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            var request = new TextRequest
            {
                TextSetId = textSetId,
                WordsLimit = wordsLimit,
            };
            var reply = await client.GetTextAsync(request);
            return reply.Text;
        }
    }
}
