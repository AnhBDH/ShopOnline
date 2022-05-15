using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
using OShop.Models;
namespace OShop.Dao
{
    public class OrderDetailDao
    {
        ShopDBContext db = null;
        public OrderDetailDao()
        {
            db = new ShopDBContext();
        }

        public bool Insert(OrderDetail entity) {
            db.OrderDetails.Add(entity);
            db.SaveChanges();
            return true;
        }

    }
}