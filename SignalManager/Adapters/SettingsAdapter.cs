using SignalManager.Context;
using SignalManager.Data;
using SignalManager.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalManager.Adapters
{
    public static class SettingsAdapter
    {
        public static Settings GetItem(int id)
        {
            return (from qr in LocalContext.Instance.Settings where qr.SettingsId == id select qr).FirstOrDefault();
        }

        public static List<SettingsProxy> GetSettings()
        {
            List<SettingsProxy> settingsProxy = (from qr in LocalContext.Instance.Settings
                                                 select new SettingsProxy
                                                 {
                                                     Id = qr.SettingsId,
                                                     ControlType = qr.ControlType,
                                                     DisplayTime = qr.DisplayTime,
                                                     Interval = qr.Interval,
                                                     PointsSource = qr.PointsSource,
                                                     PointsCount = qr.PointsCount,
                                                     SelectedListId = qr.SelectedListId,
                                                     SelectedList = new PointListProxy()
                                                     {
                                                         Id=qr.SelectedListId,
                                                         Name=qr.SelectedList.PointListName,
                                                     },
                                               BackgroundColorArgb=qr.BackgroundColorArgb,
                                           }).ToList();
            return settingsProxy;
        }

        public static Settings SaveSettings(SettingsProxy settingsProxy)
        {
            Settings settings = GetItem(settingsProxy.Id);
            if (settings == null)
            {
                settings = new Settings();
                LocalContext.Instance.Settings.Add(settings);
            }
            settings.ControlType = settingsProxy.ControlType;
            settings.DisplayTime = settingsProxy.DisplayTime;
            settings.Interval = settingsProxy.Interval;
            settings.PointsCount = settingsProxy.PointsCount;
            settings.PointsSource = settingsProxy.PointsSource;
            settings.SelectedListId = settingsProxy.SelectedListId;
            settings.BackgroundColorArgb = settingsProxy.BackgroundColorArgb;
            LocalContext.Instance.SaveChanges();
            return settings;
        }
    }
}
