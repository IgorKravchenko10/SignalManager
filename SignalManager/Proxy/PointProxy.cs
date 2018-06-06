using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Proxy
{
    public class PointProxy:ProxyObject
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Color Color { get; private set; }

        public int Argb
        {
            get
            {
                if (this.Color != null)
                {
                    return this.Color.ToArgb();
                }
                else
                {
                    return -1;
                }
            }
            set
            {
                this.Color = Color.FromArgb(value);
            }
        }

        public int PointListId { get; set; }

        public Data.Point ToData()
        {
            Data.Point point = new Data.Point()
            {
                X = this.X,
                Y = this.Y,
                Width=this.Width,
                Height=this.Height,
                PointId = this.Id,
                PointListId = this.PointListId,
                Argb = this.Argb
            };
            return point;
        }
    }
}
