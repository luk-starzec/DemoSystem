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
using Grpc.Core;

namespace IssueGenerator.Services
{
    public class DescriptionService : IDescriptionService
    {
        private readonly string descriptionProviderUrl;

        public DescriptionService(IOptions<ServicesSettings> options)
        {
            descriptionProviderUrl = options.Value.DescriptionProviderUrl;
        }

        public async Task<string> GetDescriptionAsync(int? textSourceId, int? wordsLimit, bool randomWordsCount)
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            var request = new TextRequest
            {
                TextSourceId = textSourceId,
                WordsLimit = wordsLimit,
                RandomWordsCount = randomWordsCount,
            };
            var reply = await client.GetTextAsync(request);
            return reply.Text;
        }

        public async Task Test()
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            using var reply = client.GetTextSourcesStream(new Google.Protobuf.WellKnownTypes.Empty());
            while (await reply.ResponseStream.MoveNext())
            {
                var current = reply.ResponseStream.Current;
                Console.WriteLine(current.Name);
            }
        }
    }
}
