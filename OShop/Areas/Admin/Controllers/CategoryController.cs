using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.EF;
using OShop.Common;
using OShop.Dao;
using PagedList;
namespace OShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(page, pagesize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category entity)
        {
            if (String.IsNullOrEmpty(entity.Name))
            {
                SetAlert("Yêu cầu nhập tên danh mục","warning");
            }
            else if (String.IsNullOrEmpty(entity.MetaTitle))
            {
                SetAlert("Yêu cầu nhập tiêu đề SEO", "warning");
            }
            else if(entity.DisplayOrder== null)
            {
                SetAlert("Yêu cầu nhập thứ tự hiện thị", "warning");
            }
            else
            {
                var dao = new CategoryDao();
                entity.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_ADMIN_SESSION];
                entity.CreatedBy = session.UserName;
                long id = dao.Insert(entity);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    SetAlert("Thêm thất bại", "warning");
                    return RedirectToAction("Create", "Category");
                }
            }
            return RedirectToAction("Create", "Category");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = new CategoryDao().ViewDetail(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category entity)
        {
            var dao = new CategoryDao();
            var session = (UserLogin)Session[CommonConstants.USER_ADMIN_SESSION];
            entity.ModifiedBy = session.UserName;

            var update = dao.Update(entity);
            if (update)
            {
                SetAlert("Cập nhật thành công", "success");
            }
            else
            {
                SetAlert("Cập nhật thất bại", "warning");
            }
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = new CategoryDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Không thành công xóa", "warning");
            }
            return RedirectToAction("Index", "Category");
        }
    }
}