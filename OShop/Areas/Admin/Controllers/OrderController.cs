using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.Dao;
using PagedList;

namespace OShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        [HttpGet]
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(1,10);
            return View(model);
        }
    }
}