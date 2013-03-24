using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingIt.Domain.Respository
{
    public interface IHostRepository
    {
        void Save(Host host);
        void Delete(string id);

        List<Host> GetAll();
    }
}
