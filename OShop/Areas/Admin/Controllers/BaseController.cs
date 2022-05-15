using System;
using System.Web.Mvc;
using OShop.Common;
using System.Web.Routing;
namespace OShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_ADMIN_SESSION];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "success";

            }else if(type == "warning")
            {
                TempData["AlertType"] = "warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "danger";
            }
            
        }
    }
}