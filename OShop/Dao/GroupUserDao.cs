using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
namespace OShop.Dao
{
    public class GroupUserDao
    {
        ShopDBContext db = null;
        public GroupUserDao()
        {
            db = new ShopDBContext();
        }

        public List<GroupUser> ListAll()
        {
            return db.GroupUsers.ToList();
        }
    }
}