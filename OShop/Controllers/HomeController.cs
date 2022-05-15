using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.Dao;
using OShop.EF;
namespace OShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var dao = new ProductDao();
            ViewBag.ListNewProducts = dao.ListNewProduct(4);
            ViewBag.ListFollowProducts = dao.ListFollowProduct(4);
            ViewBag.ListSaleProducts = dao.ListSaleProduct(4);
            return View();
        }
    }
}