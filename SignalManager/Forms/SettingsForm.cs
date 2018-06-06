using SignalManager.Adapters;
using SignalManager.Data;
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

namespace SignalManager.Forms
{
    public partial class SettingsForm : Form
    {
        private SettingsProxy _settingsProxy;

        public SettingsForm()
        {
            InitializeComponent();
            List<PointsSource> pointsSources = new List<PointsSource>()
            {
                PointsSource.FromDatabase,
                PointsSource.FromRandom
            };
            pointsSourceComboBox.DataSource = pointsSources;

            List<ControlType> controlTypes = new List<ControlType>()
            {
                ControlType.Automatic,
                ControlType.Manual
            };
            controlTypeComboBox.DataSource = controlTypes;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _settingsProxy = SettingsAdapter.GetSettings().FirstOrDefault();
                List<PointListProxy> lists = PointListAdapter.GetItems();
                pointListProxyBindingSource.DataSource = lists;
                if (_settingsProxy != null)
                {
                    pointsSourceComboBox.SelectedItem = (PointsSource)_settingsProxy.PointsSource;
                    countNumeric.Value = _settingsProxy.PointsCount;
                    listComboBox.SelectedItem = (from qr in lists where qr.Id == _settingsProxy.SelectedListId select qr).FirstOrDefault();
                    controlTypeComboBox.SelectedItem = (PointsSource)_settingsProxy.ControlType;
                    displayTimeNumeric.Value = _settingsProxy.DisplayTime;
                    intervalNumeric.Value = _settingsProxy.Interval;
                    ColorButton.BackColor = Color.FromArgb(_settingsProxy.BackgroundColorArgb);
                    pathToDbTextBox.Text = Properties.Settings.Default.PathToDatabase;
                }
                else
                {
                    _settingsProxy = new SettingsProxy();
                }
            }
            catch (FileNotFoundException ioEx)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.PathToDatabase = openFileDialog1.FileName;
                    Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                _settingsProxy.PointsSource = (int)pointsSourceComboBox.SelectedItem;
                _settingsProxy.PointsCount = Convert.ToInt32(countNumeric.Value);
                if (listComboBox.SelectedItem != null)
                {
                    _settingsProxy.SelectedListId = (listComboBox.SelectedItem as PointListProxy).Id;
                }
                _settingsProxy.ControlType = (int)controlTypeComboBox.SelectedItem;
                _settingsProxy.DisplayTime = Convert.ToInt32(displayTimeNumeric.Value);
                _settingsProxy.Interval = Convert.ToInt32(intervalNumeric.Value);
                _settingsProxy.BackgroundColorArgb = ColorButton.BackColor.ToArgb();
                SettingsAdapter.SaveSettings(_settingsProxy);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (PointListEditorForm pointListEditor = new PointListEditorForm())
            {
                pointListEditor.ShowDialog();
            }
            pointListProxyBindingSource.DataSource = PointListAdapter.GetItems();
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ColorButton.BackColor = colorDialog1.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathToDbTextBox.Text = openFileDialog1.FileName;
                Properties.Settings.Default.PathToDatabase = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
            }
        }
    }
}
