using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmployeeWpfApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool HasPropertyChanged => PropertyChanged is not null;

        public void ClearPropertyChanged() => PropertyChanged = null;


        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
