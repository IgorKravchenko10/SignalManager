using SignalManager.Adapters;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Properties.DataSources
{
    public class PointListEditorViewModel:EditorViewModel
    {
        public new PointListProxy Card
        {
            get { return (PointListProxy)base.Card; }
            set { base.Card = value; }
        }

        protected override ProxyObject CreateCard()
        {
            return new PointListProxy();
        }

        public void GetItem(int pointListId)
        {
            this.Card=PointListAdapter.GetItem(pointListId).ToProxy();
        }

        public void SaveItem()
        {
            PointListAdapter.SaveItem(this.Card);
        }
    }
}
