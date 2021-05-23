using BasicIntegrationEventService;
using EmployeeConsoleApp.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EmployeeConsoleApp
{
    public class IssueService : IIssueService
    {
        private readonly IIntegrationEventService integrationEventService;
        private readonly ILogger<IssueService> logger;
        private readonly int wordsPerSecond;
        private readonly string userName;

        public IssueService(IOptions<ApplicationSettings> options, IIntegrationEventService integrationEventService, ILogger<IssueService> logger)
        {
            userName = options.Value.UserName;
            wordsPerSecond = options.Value.WordsPerMinute;
            if (wordsPerSecond <= 0)
                wordsPerSecond = options.Value.DefaultWordsPerMinute;

            this.integrationEventService = integrationEventService;
            this.logger = logger;
        }

        public async Task CompleteIssue(IssueModel issue)
        {
            int processingTime = GetProcessingTime(issue);

            logger.LogInformation("Started: {title} in {app}. Processing time: {time}s", issue.Title, issue.App, processingTime / 1000);
            await DoWork(issue, processingTime);
            logger.LogInformation($"Issue completed");

            var completedEvent = new IssueCompletedIntegrationEvent(issue);
            integrationEventService.PublishThroughEventBus(completedEvent);

            logger.LogInformation($"Event sent");
        }

        private int GetProcessingTime(IssueModel issue)
        {
            var wordsCount = GetWordsCount(issue.Description);
            return wordsCount / wordsPerSecond * 1000;
        }

        private int GetWordsCount(string text)
        {
            return text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private async Task DoWork(IssueModel issue, int processingTime)
        {
            await Task.Delay(processingTime);
            issue.AssignedUser = userName;
        }
    }
}
