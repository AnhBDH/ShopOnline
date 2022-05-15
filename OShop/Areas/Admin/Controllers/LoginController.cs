using OShop.Areas.Admin.Models;
using OShop.Common;
using OShop.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {            
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password),true);
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = user.ID;
                    userSession.UserName = user.UserName;
                    userSession.Avatar = user.Avartar;
                    userSession.GroupID = user.GroupID;
                    Session.Add(CommonConstants.USER_ADMIN_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại"); 
                }              
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_ADMIN_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}