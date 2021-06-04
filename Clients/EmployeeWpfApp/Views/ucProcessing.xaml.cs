using EmployeeWpfApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeWpfApp.Views
{
    public partial class ucProcessing : UserControl
    {
        public ucProcessing()
        {
            InitializeComponent();
            DataContextChanged += UcProcessing_DataContextChanged;
        }

        private void UcProcessing_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var data = DataContext as ProcessingViewModel;
            data.PropertyChanged += Data_PropertyChanged;
        }

        private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Animate")
                animatedRect.RaiseEvent(new RoutedEventArgs(LoadedEvent));
        }
    }
}
