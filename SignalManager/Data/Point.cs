using SignalManager.Context;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Data
{
    public class Point
    {
        [Key()]
        public int PointId { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Int32 Argb { get; set; }

        [NotMapped]
        public Color Color { get; set; }

        public int PointListId { get; set; }

        public PointList PointList { get; set; }

        public PointProxy ToProxy()
        {
            PointProxy pointProxy = new PointProxy()
            {
                Id = this.PointId,
                X = this.X,
                Y = this.Y,
                Width = this.Width,
                Height=this.Height,
                PointListId = this.PointListId,
                Argb = this.Argb,
            };
            return pointProxy;
        }
    }
}
