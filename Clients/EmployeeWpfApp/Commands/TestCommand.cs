using EmployeeWpfApp.Services;
using EmployeeWpfApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmployeeWpfApp.Commands
{
    public class TestCommand : ICommand
    {
        private readonly IIssueService issueService;
        private readonly IUserService userService;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var vm = parameter as MainWindowViewModel;

            //vm.issues.Add(
            //    new IssueViewModel(issueService)
            //    {
            //        App = "Glob",
            //        Title = "test test",
            //        Description = "xxx xxxxxxxxxxxx xxxxxxxxxx xxxx xxxxxxxxx xxxx xxxxx",
            //        Created = DateTimeOffset.Now,
            //    });
        }

        public TestCommand(IIssueService issueService)
        {
            this.issueService = issueService;
        }
    }
}
