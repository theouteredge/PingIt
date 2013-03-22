using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingIt.Domain.Services
{
    public class HostService
    {
        public Host CreateHost(string uri, int port, string name, int pingEvery)
        {
            var host = new Host(uri, port, name, pingEvery);

            // save the host
            //_repository.Save(host);

            return host;
        }


        public List<Host> GetAllHosts()
        {
            return new List<Host>();
        }
    }
}
