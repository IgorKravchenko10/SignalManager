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
    public static class PointListAdapter
    {
        public static List<PointListProxy> GetItems()
        {
            List<PointListProxy> result = (from qr in LocalContext.Instance.PointLists
                                           select new PointListProxy()
                                           {
                                               Id = qr.PointListId,
                                               Name = qr.PointListName,
                                               Points = (from point in qr.Points select new PointProxy()
                                               {
                                                   Id=point.PointId,
                                                   X=point.X,
                                                   Y=point.Y,
                                                   Argb=point.Argb,
                                               }).ToList()
                                           }).ToList();
            return result;
        }

        public async static Task<List<PointListProxy>> GetItemsAsync()
        {
            return await Task.Factory.StartNew(() => GetItems());
        }

        public static PointList GetItem(int id)
        {
            PointList pointList = (from qr in LocalContext.Instance.PointLists where qr.PointListId == id select qr).FirstOrDefault();
            return pointList;
        }

        public static PointListProxy GetItemProxy(int id)
        {
            PointListProxy pointList = (from qr in LocalContext.Instance.PointLists where qr.PointListId == id
                                        select new PointListProxy
                                        {
                                            Id=qr.PointListId,
                                            Name=qr.PointListName,
                                            Points = (from point in qr.Points
                                                      select new PointProxy()
                                                      {
                                                          Id = point.PointId,
                                                          X = point.X,
                                                          Y = point.Y,
                                                          Width=point.Width,
                                                          Height=point.Height,
                                                          Argb = point.Argb,
                                                      }).ToList()
                                        }).FirstOrDefault();
             return pointList;
        }

        public static PointList SaveItem(PointListProxy pointListProxy)
        {
            PointList pointList = CreateItem(pointListProxy);
            LocalContext.Instance.SaveChanges();
            return pointList;
        }

        public static async Task<PointList> SaveItemAsync(PointListProxy pointListProxy)
        {
            PointList pointList = CreateItem(pointListProxy);
            await LocalContext.Instance.SaveChangesAsync();
            return pointList;
        }

        private static PointList CreateItem(PointListProxy pointListProxy)
        {
            PointList pointList = GetItem(pointListProxy.Id);
            if (pointList == null)
            {
                pointList = new PointList();
                LocalContext.Instance.PointLists.Add(pointList);
            }
            pointList.PointListId = pointListProxy.Id;
            pointList.PointListName = pointListProxy.Name;

            return pointList;
        }

        public static void RemoveItem(PointListProxy pointListProxy)
        {
            PointList pointList = GetItem(pointListProxy.Id);
            if (pointList == null)
            {
                return;
            }
            LocalContext.Instance.PointLists.Remove(pointList);
            LocalContext.Instance.SaveChanges();
        }
    }
}
