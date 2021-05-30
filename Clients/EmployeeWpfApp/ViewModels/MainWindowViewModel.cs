using EmployeeWpfApp.Commands;
using EmployeeWpfApp.Services;
using EmployeeWpfApp.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace EmployeeWpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IIssueService issueService;
        private readonly IUserService userService;

        private object currentViewModel;
        public object CurrentViewModel
        {
            get => currentViewModel;
            set { currentViewModel = value; OnPropertyChanged(); }
        }


        public MainWindowViewModel(IIssueService issueService, IUserService userService)
        {
            this.issueService = issueService;
            this.userService = userService;

            userService.LoggedIn += SetIssuesViewModel;

            CurrentViewModel = new LoginViewModel(userService);
        }

        private void SetIssuesViewModel()
        {
            issueService.Subscribe();

            CurrentViewModel = new IssuesViewModel(issueService)
            {
                UserName = userService.UserName,
            };
        }
    }
}
