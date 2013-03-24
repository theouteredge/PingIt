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
using PingIt.Domain.Respository;
using PingIt.Domain.Services;
using PingIt.Wpf.ViewModels;

namespace PingIt.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly HostService _service;
        readonly MainWindowViewModel _model;
        

        public MainWindow()
        {
            InitializeComponent();
            
            _service = new HostService(new HostRepository());
            _model = new MainWindowViewModel(Dispatcher, _service, new HostRepository());

            DataContext = _model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _model.Save();
        }
    }
}
