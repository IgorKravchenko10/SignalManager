using SignalManager.Adapters;
using SignalManager.Context;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalManager.Forms
{
    public partial class SignalForm : Form
    {
        SettingsProxy _settings;
        PointListProxy _points;
        Graphics _graphics;
        Random _random;
        bool _isBusy;

        public SignalForm()
        {
            InitializeComponent();
            _random = new Random();
        }

        private async void SignalForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this._isBusy = true;
                //this.WindowState = FormWindowState.Maximized;
                //this.FormBorderStyle = FormBorderStyle.None;
                //this.TopMost = true;

                _settings = SettingsAdapter.GetSettings()?.FirstOrDefault();
                _graphics = this.CreateGraphics();
                this.BackColor = _settings.BackgroundColor;
                _graphics.Clear(_settings.BackgroundColor);
                if (_settings.PointsSource == (int)PointsSource.FromDatabase)
                {
                    _points = PointListAdapter.GetItemProxy(_settings.SelectedListId);
                }
                else
                {
                    _points = await this.CreatePoints();
                }
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
            finally
            {
                this.Cursor = Cursors.Arrow;
                this._isBusy = false;
            }
        }

        private async Task<PointListProxy> CreatePoints()
        {
            PointListProxy pointListProxy = new PointListProxy()
            {
                Name = "Random" + DateTime.Now.ToString(),
                Points = new List<PointProxy>()
            };
            PointList pointList = await PointListAdapter.SaveItemAsync(pointListProxy);
            pointListProxy.Id = pointList.PointListId;
            for (int i = 0; i < _settings.PointsCount; i++)
            {
                PointProxy pointProxy = new PointProxy()
                {
                    X = _random.Next(1, 100),
                    Y = _random.Next(1, 100),
                    Width=20,
                    Height=20,
                    PointListId = pointList.PointListId,
                    Argb = Color.Yellow.ToArgb()
                };
                pointListProxy.Points.Add(pointProxy);
                await PointAdapter.SaveItemAsync(pointProxy);
            }
            await PointListAdapter.SaveItemAsync(pointListProxy);
            return pointListProxy;
        }

        private async Task Start()
        {
            foreach (PointProxy point in _points.Points)
            {
                await this.ShowPoint(point);
            }
        }

        private async Task ShowPoint(PointProxy point)
        {
            await Task.Factory.StartNew(() =>
            {
                if (point.Color.Name == "0")
                {
                    point.Argb = Color.Yellow.ToArgb();
                }
                _graphics.FillEllipse(new SolidBrush(point.Color), new Rectangle(point.X * this.Width / 100, point.Y * this.Height / 100, point.Width, point.Height));
                Thread.Sleep(_settings.DisplayTime);
                _graphics.Clear(_settings.BackgroundColor);
                Thread.Sleep(_settings.Interval);
            });
        }

        private async void SignalForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_isBusy)
                {
                    return;
                }
                if (e.KeyCode == Keys.Escape)
                {
                    _graphics.Clear(Color.Black);
                    this.Close();
                }

                else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                {
                    _isBusy = true;
                    await this.Start();
                }
                else if (e.KeyCode == Keys.F11)
                {
                    this.TopMost = true;
                    this.WindowState = FormWindowState.Maximized;
                    this.FormBorderStyle = FormBorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }
            finally
            {
                _isBusy = false;
            }
        }
    }
}
