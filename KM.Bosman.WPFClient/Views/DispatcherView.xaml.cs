using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace KM.Bosman.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for DispatcherView.xaml
    /// </summary>
    public partial class DispatcherView : Page
    {
        public DispatcherView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            Task.Run(()=>Send());
        }



        private void Send()
        {
            Status.Content = $"#{Thread.CurrentThread.ManagedThreadId} Sending... ";

            Thread.Sleep(TimeSpan.FromSeconds(5));

            //   Status.Content = "Sent.";

            Panel.Dispatcher.Invoke(() => Status.Content = $"#{Thread.CurrentThread.ManagedThreadId} Sent.");
        }

        private void Send2()
        {
            Status.Content = $"#{Thread.CurrentThread.ManagedThreadId} Sending... ";

            Thread.Sleep(TimeSpan.FromSeconds(5));

            //   Status.Content = "Sent.";

            Panel.Dispatcher.InvokeAsync(() => Status.Content = $"#{Thread.CurrentThread.ManagedThreadId} Sent.");
        }
    }
}
