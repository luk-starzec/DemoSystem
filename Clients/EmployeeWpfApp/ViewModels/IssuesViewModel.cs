﻿using EmployeeWpfApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;

namespace EmployeeWpfApp.ViewModels
{
    public class IssuesViewModel : BaseViewModel
    {
        private Dispatcher dispatcher;

        public ObservableCollection<IssueViewModel> Issues { get; private set; }

        public IssuesListViewModel ToDoList { get; private set; }
        public IssuesListViewModel CompletedList { get; private set; }

        private bool isProcessing;
        public bool IsProcessing
        {
            get => isProcessing;
            set { isProcessing = value; OnPropertyChanged(); }
        }

        public System.Windows.Duration ProcessingDuration { get; set; }

        public string UserName { get; set; }


        public IssuesViewModel(IIssueService issueService)
        {
            dispatcher = Dispatcher.CurrentDispatcher;

            Issues = issueService.GetIssuesCollection();
            Issues.CollectionChanged += Issues_CollectionChanged;
            SetIssuePropertyChangedEvent();

            ToDoList = new IssuesListViewModel
            {
                Title = "To do",
                IssuesList = CollectionViewSource.GetDefaultView(ToDoIssues),
            };

            CompletedList = new IssuesListViewModel
            {
                Title = "Completed",
                IssuesList = CollectionViewSource.GetDefaultView(CompletedIssues),
            };
        }

        private IOrderedEnumerable<IssueViewModel> ToDoIssues =>
            Issues.Where(r => !r.IsCompleted).OrderByDescending(r => r.Created);
        private IOrderedEnumerable<IssueViewModel> CompletedIssues =>
            Issues.Where(r => r.IsCompleted).OrderByDescending(r => r.Created);


        private void Issues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetIssuePropertyChangedEvent();
            RefreshLists();
        }

        private void SetIssuePropertyChangedEvent()
        {
            foreach (var issue in Issues)
            {
                if (!issue.HasPropertyChanged)
                    issue.PropertyChanged += Issue_PropertyChanged;
            }
        }

        private void Issue_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            IsProcessing = ToDoIssues.Any(r => r.IsProcessing);
            
            var processingTime = ToDoIssues.Where(r => r.IsProcessing).Select(r => r.ProcessingTime).Sum();
            ProcessingDuration = new System.Windows.Duration(TimeSpan.FromMilliseconds(processingTime));

            RefreshLists();
        }

        private void RefreshLists()
        {
            dispatcher.Invoke(() =>
            {
                ToDoList.IssuesList.Refresh();
                CompletedList.IssuesList.Refresh();
            });
        }
    }
}
