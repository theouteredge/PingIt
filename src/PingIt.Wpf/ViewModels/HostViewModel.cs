using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PingIt.Domain;
using PingIt.Domain.Services;

namespace PingIt.Wpf.ViewModels
{
    public class HostViewModel
    {
        private readonly HostService _service;


        public string Name { get; set; }
        public string Uri { get; set; }
        public int Port { get; set; }
        public int PingEvery { get; set; }


        public HostViewModel(HostService service)
        {
            _service = service;
        }


        public Host Save()
        {
            return _service.CreateHost(Uri, Port, Name, PingEvery);
        }
    }
}
