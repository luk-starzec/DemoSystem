using System.ComponentModel;

namespace EmployeeWpfApp.ViewModels
{
    public  class IssuesListViewModel
    {
        public string Title { get; set; }
        public ICollectionView IssuesList { get; set; }
    }
}
