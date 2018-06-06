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
    public static class PointAdapter
    {
        public static List<PointProxy> GetItems()
        {
            List<PointProxy> result = (from qr in LocalContext.Instance.Points
                                       select new PointProxy()
                                       {
                                           Id=qr.PointId,
                                           X=qr.X,
                                           Y=qr.Y,
                                           Width=qr.Width,
                                           Height=qr.Height,
                                           Argb=qr.Argb,
                                           PointListId=qr.PointListId
                                       }).ToList();
            return result;
        }

        public async static Task<List<PointProxy>> GetItemsAsync()
        {
            return await Task.Factory.StartNew(() => GetItems());
        }

        public static Point GetItem(int id)
        {
            Point point = (from qr in LocalContext.Instance.Points where qr.PointId == id select qr).FirstOrDefault();
            return point;
        }

        public static Point SaveItem(PointProxy pointProxy)
        {
            Point point = CreatePoint(pointProxy);
            LocalContext.Instance.SaveChanges();
            return point;
        }

        public static async Task<Point> SaveItemAsync(PointProxy pointProxy)
        {
            Point point = CreatePoint(pointProxy);
            await LocalContext.Instance.SaveChangesAsync();
            return point;
        }

        public static void RemoveItem(PointProxy pointProxy)
        {
            Point point = GetItem(pointProxy.Id);
            if (point == null)
            {
                return;
            }
            LocalContext.Instance.Points.Remove(point);
            LocalContext.Instance.SaveChanges();
        }

        private static Point CreatePoint(PointProxy pointProxy)
        {
            Point point = GetItem(pointProxy.Id);
            if (point == null)
            {
                point = new Point();
                LocalContext.Instance.Points.Add(point);
            }
            point.PointId = pointProxy.Id;
            point.X = pointProxy.X;
            point.Y = pointProxy.Y;
            point.Width = pointProxy.Width;
            point.Height = pointProxy.Height;
            point.Argb = pointProxy.Argb;
            point.PointListId = pointProxy.PointListId;
            return point;
        }
    }
}
