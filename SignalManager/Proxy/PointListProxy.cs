using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Proxy
{
    public class PointListProxy:ProxyObject
    {
        public string Name { get; set; }

        public ICollection<PointProxy> Points { get; set; }
    }
}
