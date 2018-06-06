using SignalManager.Adapters;
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
    public partial class PointEditor : Form
    {
        public PointEditor()
        {
            InitializeComponent();

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PointProxy pointProxy = new PointProxy()
                {
                    X = Convert.ToInt32(XTextBox.Text),
                    Y = Convert.ToInt32(YTextBox.Text),
                    Argb = ColorButton.BackColor.ToArgb(),
                    PointListId = (comboBox1.SelectedItem as PointListProxy).Id
                };
                await PointAdapter.SaveItemAsync(pointProxy);
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

        private void button2_Click(object sender, EventArgs e)
        {
            using (PointListEditorForm pointListEditor=new PointListEditorForm())
            {
                pointListEditor.ShowDialog();
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog()== DialogResult.OK)
            {
                ColorButton.BackColor = colorDialog1.Color;
            }
        }

        private void PointEditor_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = PointListAdapter.GetItems();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
