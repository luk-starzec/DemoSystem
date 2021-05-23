using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DescriptionProvider.Services
{
    public partial class DescriptionService
    {
        public override async Task<TextReply> GetText(TextRequest request, ServerCallContext context)
        {
            var text = request.TextSourceId.HasValue
                ? await GetTextSourceText(request.TextSourceId.Value)
                : await GetRandomText();

            if (request.WordsLimit.HasValue)
                text = LimitWords(text, request.WordsLimit.Value);
            if (request.RandomWordsCount)
                text = RandomizeWordsCount(text);

            return new TextReply { Text = text };
        }


        private async Task<string> GetRandomText()
        {
            var count = await dbContext.TextSources.CountAsync();
            var i = random.Next(count);
            var textSet = await dbContext.TextSources.OrderBy(r => r.Id).Skip(i).FirstAsync();
            return textSet.Text;
        }

        private async Task<string> GetTextSourceText(int textSourcetId)
        {
            var textSource = await dbContext.TextSources.SingleAsync(r => r.Id == textSourcetId);
            return textSource.Text;
        }

        private string LimitWords(string text, int limit)
        {
            var words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(limit));
        }

        private string RandomizeWordsCount(string text)
        {
            var words = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var min = (int)(words.Length / 10m);
            var max = words.Length;
            var limit = random.Next(min, max);

            return string.Join(" ", words.Take(limit));
        }
    }
}
