using DescriptionProvider.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DescriptionProvider.Services
{
    public class DescriptionService : Description.DescriptionBase
    {
        private readonly DescriptionDbContext dbContext;
        private readonly ILogger<DescriptionService> _logger;
        private readonly Random random = new();

        public DescriptionService(DescriptionDbContext dbContext, ILogger<DescriptionService> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        public override async Task<TextReply> GetText(TextRequest request, ServerCallContext context)
        {
            var text = request.TextSetId.HasValue
                ? await GetTextSetText(request.TextSetId.Value)
                : await GetRandomText();

            if (request.WordsLimit.HasValue)
                text = LimitWords(text, request.WordsLimit.Value);

            return new TextReply { Text = text };
        }


        private async Task<string> GetRandomText()
        {
            var count = await dbContext.TextSets.CountAsync();
            var i = random.Next(count - 1);
            var textSet = await dbContext.TextSets.OrderBy(r => r.Id).Skip(i).FirstAsync();
            return textSet.Text;
        }

        private async Task<string> GetTextSetText(int textSetId)
        {
            var textSet = await dbContext.TextSets.SingleAsync(r => r.Id == textSetId);
            return textSet.Text;
        }

        private string LimitWords(string text, int limit)
        {
            var words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(limit));
        }
    }
}
