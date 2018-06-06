using SignalManager.Adapters;
using SignalManager.Data;
using SignalManager.Forms;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalManager
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DataForm dataForm = new DataForm())
            {
                dataForm.WindowState = FormWindowState.Maximized;
                dataForm.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForms = new SettingsForm())
            {
                //settingsForms.TopMost = true;
                settingsForms.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SignalForm signalForm = new SignalForm())
            {
                signalForm.WindowState = FormWindowState.Maximized;
                signalForm.FormBorderStyle = FormBorderStyle.None;
                signalForm.TopMost = true;
                signalForm.ShowDialog();
            }
        }
    }
}
