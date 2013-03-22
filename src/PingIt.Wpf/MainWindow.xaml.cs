using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PingIt.Domain.Services;
using PingIt.Wpf.ViewModels;

namespace PingIt.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly HostService _service = new HostService();
        
        readonly ObservableCollection<DisplayHostViewModel> _listModels = new ObservableCollection<DisplayHostViewModel>();
        readonly HostViewModel _host;
        

        public MainWindow()
        {
            InitializeComponent();
            _host = new HostViewModel(_service);

            DataContext = _host;
            HostList.DataContext = _listModels;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var host = _host.Save();
            _listModels.Add(new DisplayHostViewModel(host));
        }
    }
}
