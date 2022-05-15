using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.Dao;
namespace OShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(int id,int page = 1, int pagesize = 16)
        {
            var category = new CategoryDao().ViewDetail(id);
            if(category== null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Category = category;
            var model = new ProductDao().ListCategoryPaging(id, page, pagesize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Detail(long id)
        {
            var model = new ProductDao().ViewDetail((int)id);
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult PartialViewCategory()
        {
            var model = new CategoryDao().ListAllTrue();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult PartialViewCategoryMinMenu()
        {
            var model = new CategoryDao().ListAllTrue();
            return PartialView(model);
        }
    }
}