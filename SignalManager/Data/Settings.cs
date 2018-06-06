using SignalManager.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Data
{
    public enum ControlType
    {
        Manual=0,
        Automatic=1
    }

    public enum PointsSource
    {
        FromDatabase=0,
        FromRandom=1
    }

    public class Settings
    {
        [Key()]
        public int SettingsId { get; set; }

        public int Interval { get; set; }

        public int DisplayTime { get; set; }

        public int ControlType { get; set; } 

        public int PointsSource { get; set; }

        public int PointsCount { get; set; }

        public int BackgroundColorArgb { get; set; }

        [NotMapped]
        public Color BackgroundColor { get; set; }

        public int SelectedListId { get; set; }

        [ForeignKey("SelectedListId")]
        public PointList SelectedList { get; set; }

        public Settings GetSettings()
        {
            return LocalContext.Instance.Settings.FirstOrDefault();
        }

        public void Save()
        {
            LocalContext.Instance.Settings.Add(this);
            LocalContext.Instance.SaveChanges();
        }
    }
}
