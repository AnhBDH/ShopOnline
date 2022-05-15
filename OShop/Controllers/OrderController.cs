using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.EF;
using OShop.Dao;
using OShop.Common;
using PagedList;

namespace OShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            if (Session[CommonConstants.USER_CLIENT_SESSION] == null)
            {
                return Redirect("/dang-nhap");
            }
            var account = (UserLogin)Session[CommonConstants.USER_CLIENT_SESSION];
            var model = new OrderDao().ListAllPagingByUser(page,pagesize,account.UserID);
            return View(model);
        }
    }
}