using SignalManager.Adapters;
using SignalManager.Data;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalManager.Forms
{
    public partial class PointListEditorForm : Form
    {
        public PointListEditorForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PointListProxy pointListProxy = new PointListProxy()
                {
                    Name = textBox1.Text,
                };
                await PointListAdapter.SaveItemAsync(pointListProxy);
                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
