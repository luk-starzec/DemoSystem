using EmployeeWpfApp.Services;
using EmployeeWpfApp.ViewModels;
using System.Windows;

namespace EmployeeWpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow(IIssueService issueService, IUserService userService)
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel(issueService, userService);
        }
    }
}
