using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Proxy
{
    public class SettingsProxy:ProxyObject
    {
        public int Interval { get; set; }

        public int DisplayTime { get; set; }

        public int ControlType { get; set; }

        public int PointsSource { get; set; }

        public int PointsCount { get; set; }

        public int BackgroundColorArgb
        {
            get
            {
                if (this.BackgroundColor != null)
                {
                    return this.BackgroundColor.ToArgb();
                }
                return -1;
            }
            set
            {
                    this.BackgroundColor = Color.FromArgb(Convert.ToInt32(value));
            }
        }

        public Color BackgroundColor { get; private set; }

        public int SelectedListId { get; set; }

        public PointListProxy SelectedList { get; set; }
    }
}
