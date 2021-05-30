using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeeWpfApp.Views
{
    /// <summary>
    /// Interaction logic for ucProcessing.xaml
    /// </summary>
    public partial class ucProcessing : UserControl
    {
        public ucProcessing()
        {
            InitializeComponent();
        }

       // public static readonly RoutedEvent AnimateEvent = EventManager.RegisterRoutedEvent(
       //"Animate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucProcessing));

       // public event RoutedEventHandler Animate
       // {
       //     add { AddHandler(AnimateEvent, value); }
       //     remove { RemoveHandler(AnimateEvent, value); }
       // }

       // void RaiseAnimateEvent()
       // {
       //     RoutedEventArgs newEventArgs = new RoutedEventArgs(ucProcessing.AnimateEvent);
       //     RaiseEvent(newEventArgs);
       // }
    }
}
