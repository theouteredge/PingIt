using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServiceStack.Text;

namespace PingIt.Domain.Respository
{
    public class HostRepository : IHostRepository
    {
        private const string FILE_NAME = "hosts.json";
        private readonly List<Host> _store = new List<Host>();


        public HostRepository()
        {
            string json =  null;

            if (File.Exists(FILE_NAME))
            {
                json = File.ReadAllText(FILE_NAME);
                _store = json.FromJson<List<Host>>();
            }
            else _store = new List<Host>();
        }


        public void Save(Host host)
        {
            _store.Add(host);
            WriteToFile();
        }

        public void Delete(string id)
        {
            var host = _store.SingleOrDefault(x => x.Id == id);
            if (host != null)
            {
                _store.Remove(host);
                WriteToFile();
            }
        }

        public List<Host> GetAll()
        {
            return _store;
        }


        private void WriteToFile()
        {
            var json = _store.ToJson();
            File.WriteAllText(FILE_NAME, json);
        }
    }
}