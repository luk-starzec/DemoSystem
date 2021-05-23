using DescriptionProvider.Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DescriptionProvider.Services
{
    public partial class DescriptionService
    {
        public override async Task<TextSourcesReply> GetTextSources(Empty request, ServerCallContext context)
        {
            var textSources = await dbContext.TextSources
                .Select(r => new TextSourceReply
                {
                    Id = r.Id,
                    Name = r.Name,
                    Text = r.Text,
                }).ToArrayAsync();

            var reply = new TextSourcesReply();
            reply.TextSources.AddRange(textSources);

            return reply;
        }

        public override async Task GetTextSourcesStream(Empty request, IServerStreamWriter<TextSourceReply> responseStream, ServerCallContext context)
        {
            var textSources = dbContext.TextSources
                .Select(r => new TextSourceReply
                {
                    Id = r.Id,
                    Name = r.Name,
                    Text = r.Text,
                });

            await foreach (var textSource in textSources.AsAsyncEnumerable())
            {
                await responseStream.WriteAsync(textSource);

                await Task.Delay(1000);
            }
        }

        public override async Task<TextSourceReply> AddTextSource(AddTextSourceRequest request, ServerCallContext context)
        {
            var entity = new TextSource
            {
                Name = request.Name,
                Text = request.Text,
            };
            await dbContext.TextSources.AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return new TextSourceReply
            {
                Id = entity.Id,
                Name = entity.Name,
                Text = entity.Text,
            };
        }

        public override async Task<DeleteTextSourceResponse> DeleteTextSource(DeleteTextSourceRequest request, ServerCallContext context)
        {
            var entity = await dbContext.TextSources.SingleOrDefaultAsync(r => r.Id == request.TextSourceId);

            if (entity is null)
                return new DeleteTextSourceResponse { Error = "Item not found" };

            try
            {
                dbContext.TextSources.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new DeleteTextSourceResponse { Error = ex.Message };
            }

            return new DeleteTextSourceResponse();
        }
    }
}
