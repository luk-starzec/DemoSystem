using DescriptionProvider;
using EmployerWebApp.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployerWebApp.Services
{
    public class DescriptionService : IDescriptionService
    {
        private readonly string descriptionProviderUrl;

        public DescriptionService(IOptions<ServiceUrls> options)
        {
            descriptionProviderUrl = options.Value.DescriptionProviderUrl;
        }

        public async Task<List<(string name, int id)>> GetDescriptionSourceNamesAsync()
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            var reply = await client.GetTextSourceNamesAsync(new Google.Protobuf.WellKnownTypes.Empty());

            var list = reply.TextSourceNames
                .Select(r => (r.Name, r.Id))
                .ToList();
            list.Insert(0, ("random", 0));

            return list;
        }

        public async Task<List<DescriptionSource>> GetDescriptionSourcesAsync()
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            var reply = await client.GetTextSourcesAsync(new Google.Protobuf.WellKnownTypes.Empty());

            return reply.TextSources
                .Select(r => TextSourceReply2DescriptionSource(r))
                .ToList();
        }

        public async Task<DescriptionSource> AddDescriptionSourceAsync(DescriptionSource descriptionSource)
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            var request = new AddTextSourceRequest
            {
                Name = descriptionSource.Name,
                Text = descriptionSource.Text,
            };
            var reply = await client.AddTextSourceAsync(request);

            return TextSourceReply2DescriptionSource(reply);
        }

        public async Task DeleteDescriptionSourceAsync(int id)
        {
            using var chanel = GrpcChannel.ForAddress(descriptionProviderUrl);
            var client = new Description.DescriptionClient(chanel);

            var request = new DeleteTextSourceRequest { TextSourceId = id };
            var reply = await client.DeleteTextSourceAsync(request);
        }

        private DescriptionSource TextSourceReply2DescriptionSource(TextSourceReply textSourceReply)
            => new()
            {
                Id = textSourceReply.Id,
                Name = textSourceReply.Name,
                Text = textSourceReply.Text,
            };
    }
}
