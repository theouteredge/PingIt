using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using PingIt.Domain.Respository;
using PingIt.Domain.Services;

namespace PingIt.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public HostListViewModel ListViewModel { get; set; }
        public HostViewModel HostViewModel { get; set; }


        public MainWindowViewModel()
        {
            ListViewModel = new HostListViewModel();
            HostViewModel = new HostViewModel();
        }

        public MainWindowViewModel(Dispatcher dispatcher, HostService service, IHostRepository repository) : base(dispatcher)
        {
            ListViewModel =  new HostListViewModel(base.Dispatcher, repository);
            ListViewModel.Initalise();

            HostViewModel = new HostViewModel(dispatcher, service);
        }

        public void Save()
        {
            var host = HostViewModel.Save();
            ListViewModel.AddHost(host);
        }
    }
}
