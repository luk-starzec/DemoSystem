using EmployeeWpfApp.Models;
using EmployeeWpfApp.Services;
using EmployeeWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace EmployeeWpfApp.Commands
{
    public class CompleteIssueCommand : ICommand
    {

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var vm = parameter as IssueViewModel;
            return vm is null ? false : !vm.IsCompleted && !vm.IsProcessing;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as IssueViewModel;
            DoWork(vm);
        }

        private void DoWork(IssueViewModel issue)
        {
            var dispatcher = Dispatcher.CurrentDispatcher;

            var model = new IssueModel
            {
                Id = issue.Id,
                Created = issue.Created,
                App = issue.App,
                Title = issue.Title,
                Description = issue.Description,
                Sender = issue.Sender,
                Priority = issue.Priority,
            };

            var processingTime = issueService.CalculateProcessingTime(model);

            issue.ProcessingTime = processingTime;
            issue.IsProcessing = true;

            Task.Run(async () => await issueService.CompleteIssueAsync(model))
                .ContinueWith(r => dispatcher.Invoke(() =>
                     {
                         issue.IsProcessing = false;
                         issue.IsCompleted = true;
                     })
                );
        }


        private readonly IIssueService issueService;
        public CompleteIssueCommand(IIssueService issueService)
        {
            this.issueService = issueService;
        }
    }
}
