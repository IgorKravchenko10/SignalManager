using SignalManager.Context;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Data
{
    public class PointList
    {
        public int PointListId { get; set; }

        public string PointListName { get; set; }

        public ICollection<Point> Points { get; set; }

        public PointListProxy ToProxy()
        {
            PointListProxy pointListProxy = new PointListProxy()
            {
                Id = this.PointListId,
                Name = this.PointListName,
                Points = (from qr in this.Points
                          select new PointProxy()
                          {
                              Id = qr.PointId,
                              Argb = qr.Argb,
                              X = qr.X,
                              Y = qr.Y
                          }).ToList()
            };
            return pointListProxy;
        }
    }
}
