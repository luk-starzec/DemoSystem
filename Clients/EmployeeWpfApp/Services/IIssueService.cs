using EmployeeWpfApp.Models;
using EmployeeWpfApp.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EmployeeWpfApp.Services
{
    public interface IIssueService
    {
        ObservableCollection<IssueViewModel> GetIssuesCollection();

        void AddIssue(IssueModel issueModel);

        Task CompleteIssueAsync(IssueModel issue);
        int CalculateProcessingTime(IssueModel issue);

        void Subscribe();
    }
}