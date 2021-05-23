using IssueGenerator.Models;
using IssueGenerator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueGenerator.Services
{
    public class IssueService : IIssueService
    {
        private readonly IHeaderService headerService;
        private readonly IDescriptionService descriptionService;
        private readonly ISenderService senderService;

        public IssueService(IHeaderService headerService, IDescriptionService descriptionService, ISenderService senderService)
        {
            this.headerService = headerService;
            this.descriptionService = descriptionService;
            this.senderService = senderService;
        }

        public async Task<IssueModel[]> GenerateIssues(GenerationOptions options)
        {
            var result = new List<IssueModel>();
            for (int i = 0; i < options.IssuesCount; i++)
            {
                var header = await headerService.GetHeader();
                var description = await descriptionService.GetDescriptionAsync(options.TextSourceId, options.WordsLimit, options.RandomWordsCount);
                var sender = await senderService.GetSender();

                var issue = new IssueModel
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.UtcNow,
                    App = header.App,
                    Title = header.Title,
                    Description = description,
                    Sender = sender,
                };
                result.Add(issue);
            }
            return result.ToArray();
        }
    }
}
