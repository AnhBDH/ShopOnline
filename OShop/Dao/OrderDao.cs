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
    public class OrderDao
    {
        ShopDBContext db = null;
        public OrderDao()
        {
            db = new ShopDBContext();
        }

        public long Insert(Order entity) {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<Order> ListAllPaging(int page, int pagesize)
        {
            return db.Orders.OrderBy(x => x.CreatedDate).ToPagedList(page, pagesize);
        }

        public IEnumerable<Order> ListAllPagingByUser(int page, int pagesize, long ID)
        {
            return db.Orders.Where(x=>x.CustomerID== ID).OrderBy(x => x.CreatedDate).ToPagedList(page, pagesize);
        }
    }
}