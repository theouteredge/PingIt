using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PingIt.Domain.Respository;

namespace PingIt.Domain.Services
{
    public class HostService
    {
        private readonly HostRepository _repository;

        public HostService(HostRepository repository)
        {
            _repository = repository;
        }

        public Host CreateHost(string uri, int port, string name, int pingEvery)
        {
            var host = new Host(uri, port, name, pingEvery);

            // save the host
            _repository.Save(host);

            return host;
        }


        public List<Host> GetAllHosts()
        {
            return _repository.GetAll();
        }
    }
}
