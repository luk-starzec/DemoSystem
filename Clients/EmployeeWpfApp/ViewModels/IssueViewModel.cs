using EmployeeWpfApp.Commands;
using EmployeeWpfApp.Services;
using System;

namespace EmployeeWpfApp.ViewModels
{
    public class IssueViewModel : BaseViewModel
    {
        private bool isCompleted;
        private bool isProcessing;

        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string App { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sender { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted
        {
            get => isCompleted;
            set { isCompleted = value; OnPropertyChanged(); }
        }
        public bool IsProcessing
        {
            get => isProcessing;
            set { isProcessing = value; OnPropertyChanged(); }
        }
        public int ProcessingTime { get; set; }

        public bool ShowCompleteButton => !isCompleted;
        public int WordsCount => Description?.Split(" ", StringSplitOptions.RemoveEmptyEntries).Length ?? 0;
        public string Status => isCompleted ? "DONE" : isProcessing ? "PROCESSING..." : "";

        public CompleteIssueCommand CompleteIssueCommand { get; private set; }


        public IssueViewModel(IIssueService issueService)
        {
            CompleteIssueCommand = new CompleteIssueCommand(issueService);
        }
    }
}
