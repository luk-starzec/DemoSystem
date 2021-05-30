using EmployeeWpfApp.Commands;
using EmployeeWpfApp.Services;

namespace EmployeeWpfApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        public LogInCommand LogInCommand { get; set; }


        public LoginViewModel(IUserService userService)
        {
            LogInCommand = new LogInCommand(userService);
        }
    }
}
