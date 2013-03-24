using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using PingIt.Domain;
using PingIt.Domain.Services;

namespace PingIt.Wpf.ViewModels
{
    public class HostViewModel : ViewModelBase
    {
        private readonly HostService _service;


        public string Name { get; set; }
        public string Uri { get; set; }
        public int Port { get; set; }
        public int PingEvery { get; set; }

        public HostViewModel(){}
        public HostViewModel(Dispatcher dispatcher, HostService service) : base(dispatcher)
        {
            _service = service;
            PingEvery = 500;
        }


        public Host Save()
        {
            return _service.CreateHost(Uri, Port, Name, PingEvery);
        }
    }
}
