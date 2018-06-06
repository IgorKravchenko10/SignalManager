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
    public partial class DataForm : Form
    {
        private bool _isBusy = false;

        public DataForm()
        {
            InitializeComponent();
        }

        private async void DataForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _isBusy = true;
                pointProxyBindingSource.DataSource = await PointAdapter.GetItemsAsync();
                pointListProxyBindingSource.DataSource = await PointListAdapter.GetItemsAsync();
                this.Cursor = Cursors.Arrow;
            }
            catch (FileNotFoundException ioEx)
            {
                MessageBox.Show("Please set database file in settings", ioEx.Message);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }

        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PointProxy pointProxy = (pointProxyBindingSource.DataSource as List<PointProxy>)[e.Row.Index];
                PointAdapter.RemoveItem(pointProxy);
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

        private void dataGridView2_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_isBusy) return;
                this.Cursor = Cursors.WaitCursor;
                dataGridView2.EndEdit();
                List<PointProxy> pointList = (pointProxyBindingSource.DataSource as List<PointProxy>);
                if (pointList.Count <= 0) return;
                PointProxy point = pointList[e.RowIndex];
                if (point.X <= 0 || point.Y <= 0 || point.PointListId <= 0)
                {
                    return;
                }
                point.Id = PointAdapter.SaveItem(point).PointId;
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

        private void DataForm_Shown(object sender, EventArgs e)
        {
            _isBusy = false;
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_isBusy) return;
                this.Cursor = Cursors.WaitCursor;
                dataGridView1.EndEdit();
                List<PointListProxy> pointLists = (pointListProxyBindingSource.DataSource as List<PointListProxy>);
                if (pointLists.Count <= 0) return;
                PointListProxy pointList = pointLists[e.RowIndex];
                if (String.IsNullOrWhiteSpace(pointList.Name))
                {
                    return;
                }
                pointList.Id = PointListAdapter.SaveItem(pointList).PointListId;

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

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                PointListProxy pointListProxy = (pointListProxyBindingSource.DataSource as List<PointListProxy>)[e.Row.Index];
                PointListAdapter.RemoveItem(pointListProxy);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            _isBusy = true;
            this.Cursor = Cursors.WaitCursor;
            pointListProxyBindingSource.DataSource = await PointListAdapter.GetItemsAsync();
            this.Cursor = Cursors.Arrow;
            _isBusy = false;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            _isBusy = true;
            this.Cursor = Cursors.WaitCursor;
            pointProxyBindingSource.DataSource = await PointAdapter.GetItemsAsync();
            this.Cursor = Cursors.Arrow;
            _isBusy = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].HeaderText == "Color")
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    List<PointProxy> pointList = (pointProxyBindingSource.DataSource as List<PointProxy>);
                    if (pointList.Count <= 0) return;
                    PointProxy point = pointList[e.RowIndex];
                    point.Argb = colorDialog1.Color.ToArgb();
                }
            }
        }        
    }
}
