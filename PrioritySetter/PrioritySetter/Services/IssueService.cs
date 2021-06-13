using BasicIntegrationEventService;
using Microsoft.EntityFrameworkCore;
using PrioritySetter.Data;
using PrioritySetter.IntegrationEvents.Events;
using PrioritySetter.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PrioritySetter.Services
{
    public class IssueService : IIssueService
    {
        private readonly PrioritySetterContext dbContext;
        private readonly IIntegrationEventService integrationEventService;

        public IssueService(PrioritySetterContext dbContext, IIntegrationEventService integrationEventService)
        {
            this.dbContext = dbContext;
            this.integrationEventService = integrationEventService;
        }

        public async Task SetPriority(IssueModel issue)
        {
            var defaultPriority = await GetDefaultPriorityAsync();
            var errorPriority = (await GetErrorPriority(issue.Title)) ?? defaultPriority;
            var appPriority = (await GetAppPriority(issue.App)) ?? defaultPriority;

            var priority = errorPriority.PriorityLevel == defaultPriority.PriorityLevel
                ? appPriority
                : errorPriority;

            issue.Priority = priority.Name;
            issue.PriorityDescription = priority.Description;

            var prioritySetEvent = new IssuePrioritySetIntegrationEvent(issue);
            integrationEventService.PublishThroughEventBus(prioritySetEvent);
        }

        private async Task<Priority> GetErrorPriority(string error)
            => await dbContext.TitlePriority
                 .Where(r => r.Title.ToLower() == error.ToLower())
                 .Select(r => r.PriorityRelation)
                 .SingleOrDefaultAsync();

        private async Task<Priority> GetAppPriority(string app)
            => await dbContext.AppPriority
                 .Where(r => r.App.ToLower() == app.ToLower())
                 .Select(r => r.PriorityRelation)
                 .SingleOrDefaultAsync();

        private async Task<Priority> GetDefaultPriorityAsync()
            => await dbContext.Priority
                .SingleAsync(r => r.PriorityLevel == EnumPriorityLevel.Normal);
    }
}
