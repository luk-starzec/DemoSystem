using EmployeeWpfApp.Services;
using EmployeeWpfApp.ViewModels;
using System;
using System.Windows.Input;

namespace EmployeeWpfApp.Commands
{
    public class LogInCommand : ICommand
    {

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            var vm = parameter as LoginViewModel;
            return !string.IsNullOrWhiteSpace(vm?.UserName);
        }

        public void Execute(object parameter)
        {
            var vm = parameter as LoginViewModel;

            userService.LogIn(vm.UserName);
        }


        private readonly IUserService userService;
        public LogInCommand(IUserService userService)
        {
            this.userService = userService;
        }
    }
}
