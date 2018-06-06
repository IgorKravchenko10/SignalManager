using SignalManager.Adapters;
using SignalManager.Context;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Properties.DataSources
{
    public class PointEditorViewModel: EditorViewModel
    {

        public new PointProxy Card
        {
            get { return (PointProxy)base.Card; }
            set { base.Card = value; }
        }

        public void SaveItem()
        {
            PointAdapter.SaveItem(this.Card);
        }

        public void GetItem(int pointId)
        {
            this.Card = PointAdapter.GetItem(pointId).ToProxy();
        }

        protected override ProxyObject CreateCard()
        {
            return new PointProxy();
        }
    }
}
