using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.EF;
using OShop.Dao;
using OShop.Common;
using PagedList;
namespace OShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pagesize = 10)
        {
            var dao = new UserDao();
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
        public ActionResult Create(User user)
        {            
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var userName = dao.GetByUserName(user.UserName);
                if (userName!= null)
                {
                    SetAlert("Tên đăng nhập đã tồn tại", "warning");
                    return RedirectToAction("Create", "User");
                }
                var email = dao.GetByEmail(user.Email);
                if (email != null)
                {
                    SetAlert("Email này đã đăng ký", "warning");
                    return RedirectToAction("Create", "User");
                }
                user.CreatedDate = DateTime.Now;
                var session = (UserLogin)Session[CommonConstants.USER_ADMIN_SESSION];
                user.CreatedBy = session.UserName;
                var encrytedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encrytedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm thất bại", "warning");
                    return RedirectToAction("Create", "User");
                }
            }
            else{ SetAlert("Yêu cầu nhập đủ thông tin", "warning"); }
            return RedirectToAction("Create", "User");
        }

        [HttpGet]
        public ActionResult Edit (int id)
        {
            var list = new List<Boolean>() { true, false };
            ViewBag.Status = new SelectList(list);
            SetViewBag();
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit (User user)
        {
            var dao = new UserDao();

            if (!String.IsNullOrEmpty(user.Email))
            {
                var duplicateEmail = dao.GetByEmail(user.Email);
                if (user.Name != duplicateEmail.Name)
                {
                    SetAlert("Email này đã đăng ký", "warning");
                    return RedirectToAction("Index", "User");
                }                
            }

            user.ModifiedDate = DateTime.Now;
            var session = (UserLogin)Session[CommonConstants.USER_ADMIN_SESSION];
            user.ModifiedBy = session.UserName;
                        
            var update = dao.Update(user);
            if (update)
            {
                SetAlert("Cập nhật thành công", "success");               
            }
            else
            {
                SetAlert("Cập nhật thất bại", "warning");                
            }
            return RedirectToAction("Index", "User");
        }

        public void SetViewBag(string selected = null)
        {
            var dao = new GroupUserDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selected);
        }
        [HttpGet]
        public ActionResult Delete (int id)
        {
            var result = new UserDao().Delete(id);
            if (result)
            {
                SetAlert("Xóa thành công", "success");                
            }
            else
            {
                SetAlert("Không thành công xóa", "warning");
            }
            return RedirectToAction("Index", "User");
        }

        
    }
}