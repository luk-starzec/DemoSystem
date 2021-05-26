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

        public async Task CompleteIssueAsync(IssueModel issue)
        {
            AssignEmployee(issue);

            logger.LogInformation("Employee assigned");

            int processingTime = GetProcessingTime(issue);

            logger.LogInformation("Started: {title} in {app}. Processing time: {time}s", issue.Title, issue.App, processingTime / 1000);

            var startedEvent = new IssueStartedIntegrationEvent(issue);
            integrationEventService.PublishThroughEventBus(startedEvent);

            await DoWorkAsync(processingTime);

            logger.LogInformation($"Issue completed");

            var completedEvent = new IssueCompletedIntegrationEvent(issue);
            integrationEventService.PublishThroughEventBus(completedEvent);

            logger.LogInformation($"Work done");
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

        private void AssignEmployee(IssueModel issue)
        {
            issue.Employee = userName;
        }

        private async Task DoWorkAsync(int processingTime)
        {
            await Task.Delay(processingTime);
        }
    }
}
