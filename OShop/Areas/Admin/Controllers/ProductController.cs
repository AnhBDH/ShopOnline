using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.EF;
using OShop.Dao;
using OShop.Common;
namespace OShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pagesize = 6)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(page, pagesize);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var list = new List<Boolean>() { true, false };
            ViewBag.Status = new SelectList(list);
            SetViewBag();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product entity)
        {
            entity.ViewCount = 0;
            if (String.IsNullOrEmpty(entity.Name))
            {
                SetAlert("Yêu cầu nhập tên sản phẩm", "warning");
            }
            else if (String.IsNullOrEmpty(entity.Code)){
                SetAlert("Yêu cầu nhập mã của sản phẩm", "warning");
            }
            else if (String.IsNullOrEmpty(entity.MetaTitle)){
                SetAlert("Yêu cầu nhập đường dẫn SEO", "warning");
            }
            else if (String.IsNullOrEmpty(entity.Description)){
                SetAlert("Yêu cầu nhập phần mô tả cho sản phẩm", "warning");
            }
            else if (String.IsNullOrEmpty(entity.Image)){
                SetAlert("Yêu cầu chọn ảnh cho sản phẩm", "warning");
            }
            else if (String.IsNullOrEmpty(entity.Detail)){
                SetAlert("Yêu cầu nhập chi tiết sản phẩm", "warning");
            }
            else if (entity.Price==null)
            {
                SetAlert("Yêu cầu nhập giá cho sản phẩm", "warning");
            }
            else if (entity.Quantity == null)
            {
                SetAlert("Yêu cầu nhập số lượng sản phẩm", "warning");
            }
            else if (entity.Quantity < 0)
            {
                SetAlert("Số lượng sản phẩm không thể âm", "warning");
            }
            else if (entity.Warranty == null)
            {
                SetAlert("Yêu cầu nhập thời gian bảo hành", "warning");
            }
            else if (entity.Warranty < 0)
            {
                SetAlert("Thời gian bảo hành không hợp lệ", "warning");
            }
            else 
            {
                entity.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_ADMIN_SESSION];
                entity.CreatedBy = session.UserName;
                var dao = new ProductDao();
                long id = dao.Insert(entity);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    SetAlert("Thêm thất bại", "warning");
                    return RedirectToAction("Create", "Product");
                }
            }

            return RedirectToAction("Create", "Product");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var list = new List<Boolean>() { true, false };
            ViewBag.Status = new SelectList(list);
            SetViewBag();
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product entity)
        {
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var quantity = new ProductDao().ViewDetail(id).Quantity;
            if (quantity > 0)
            {
                SetAlert("Không thể xóa do còn hàng", "error");
                return RedirectToAction("Index", "Product");
            }
            var result = new ProductDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Không thành công xóa", "warning");
            }
            return RedirectToAction("Index", "Product");
        }

        public void SetViewBag(string selected = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selected);
        }

    }
}