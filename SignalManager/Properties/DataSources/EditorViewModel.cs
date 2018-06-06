using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Properties.DataSources
{
    public abstract class EditorViewModel:ViewModel
    {
        private ProxyObject _card;
        public virtual ProxyObject Card
        {
            get
            {
                if (_card == null)
                {
                    _card = this.CreateCard();
                }
                return _card;
            }
            set
            {
                if (value != null)
                {
                    _card = value;
                    this.OnPropertyChanged("Card");
                }
            }
        }

        protected abstract ProxyObject CreateCard();
    }
}
