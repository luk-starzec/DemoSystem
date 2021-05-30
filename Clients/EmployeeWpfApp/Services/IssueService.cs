using BasicIntegrationEventService;
using EmployeeWpfApp.IntegrationEvents.EventHandlers;
using EmployeeWpfApp.IntegrationEvents.Events;
using EmployeeWpfApp.Models;
using EmployeeWpfApp.ViewModels;
using EventBus;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWpfApp.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIntegrationEventService integrationEventService;
        private readonly IEventBus eventBus;
        private readonly IUserService userService;

        private readonly int wordsPerSecond;

        private readonly ObservableCollection<IssueViewModel> issues;

        public IssueService(IIntegrationEventService integrationEventService, IEventBus eventBus, IUserService userService)
        {
            this.integrationEventService = integrationEventService;
            this.eventBus = eventBus;
            this.userService = userService;

            wordsPerSecond = 2;
            issues =  new();
        }

        public ObservableCollection<IssueViewModel> GetIssuesCollection()
        {
            return issues;
        }

        public void AddIssue(IssueModel model)
        {
            var vm = new IssueViewModel(this)
            {
                Id = model.Id,
                Created = model.Created,
                App = model.App,
                Title = model.Title,
                Description = model.Description,
                Sender = model.Sender,
                Priority = model.Priority,
            };
            issues.Add(vm);
        }

        public async Task CompleteIssueAsync(IssueModel issue)
        {
            AssignEmployee(issue);

            int processingTime = CalculateProcessingTime(issue);

            var startedEvent = new IssueStartedIntegrationEvent(issue);
            integrationEventService.PublishThroughEventBus(startedEvent);

            await DoWorkAsync(processingTime);

            var completedEvent = new IssueCompletedIntegrationEvent(issue);
            integrationEventService.PublishThroughEventBus(completedEvent);

        }

        public void Subscribe()
        {
            eventBus.Subscribe<IssuePrioritySetIntegrationEvent, IssuePrioritySetIntegrationEventHandler>();
        }

        public int CalculateProcessingTime(IssueModel issue)
        {
            return CalculateProcessingTime(issue.Description);
        }

        private int CalculateProcessingTime(string description)
        {
            var wordsCount = GetWordsCount(description);
            return wordsCount / wordsPerSecond * 1000;
        }

        private int GetWordsCount(string text)
        {
            return text.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private void AssignEmployee(IssueModel issue)
        {
            issue.Employee = userService.UserName;
        }

        private async Task DoWorkAsync(int processingTime)
        {
            await Task.Delay(processingTime);
        }
    }
}
