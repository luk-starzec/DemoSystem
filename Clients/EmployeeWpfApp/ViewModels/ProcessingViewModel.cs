using System;
using System.Windows;

namespace EmployeeWpfApp.ViewModels
{
    public class ProcessingViewModel : BaseViewModel
    {
        private bool isProcessing;
        private Duration processingDuration;

        public bool IsProcessing
        {
            get => isProcessing;
            set { isProcessing = value; OnPropertyChanged(); }
        }

        public Duration ProcessingDuration
        {
            get => processingDuration;
            set { processingDuration = value; OnPropertyChanged(); }
        }


        public void Animate(int duration)
        {
            ProcessingDuration = new Duration(TimeSpan.FromMilliseconds(duration));
            OnPropertyChanged("Animate");
        }
    }
}
