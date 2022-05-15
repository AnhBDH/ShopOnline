using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OShop.Common;
using OShop.Dao;
using OShop.EF;
namespace OShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (Session[CommonConstants.USER_CLIENT_SESSION] == null)
            {
                return Redirect("/dang-nhap");
            }
            var account = (UserLogin)Session[CommonConstants.USER_CLIENT_SESSION];
            var model = new CartDetailDao().ShowCart(account.UserID);
            return View(model);
        }

        public ActionResult AddCart(long productID, int quantity = 1)
        {
            if(Session[CommonConstants.USER_CLIENT_SESSION]== null)
            {
                return Redirect("/dang-nhap");
            }
            var account = (UserLogin)Session[CommonConstants.USER_CLIENT_SESSION];
            var dao = new CartDetailDao();
            var exist = dao.Exist(account.UserID, productID);
            if (exist)
            {
                var addone = dao.AddOne(account.UserID, productID);
            }
            else
            {
                var CartDetail = new CartDetail();
                CartDetail.IDCustomer = account.UserID;
                CartDetail.IDProduct = productID;
                CartDetail.Quantity = 1;
                var productdao = new ProductDao();
                var product = productdao.ViewDetail((int)productID);
                if (product.PromotionPrice > 0)
                {
                    CartDetail.Price = product.PromotionPrice;
                }
                else
                {
                    CartDetail.Price = product.Price;
                }                
                var insert = dao.Insert(CartDetail);
            }
            return Redirect("/gio-hang");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            if (Session[CommonConstants.USER_CLIENT_SESSION] == null)
            {
                return Redirect("/dang-nhap");
            }
            var account = (UserLogin)Session[CommonConstants.USER_CLIENT_SESSION];
            var model = new CartDetailDao().ShowCart(account.UserID);
            var user = new UserDao().ViewDetail((int)account.UserID);
            ViewBag.UserDetail = user;
            return View(model);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email, string note)
        {
            if (Session[CommonConstants.USER_CLIENT_SESSION] == null)
            {
                return Redirect("/dang-nhap");
            }
            var account = (UserLogin)Session[CommonConstants.USER_CLIENT_SESSION];
            var order = new Order();
            order.CustomerID = account.UserID;
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;
            order.Note = note;
            order.Status = 1;
            try
            {
                var id = new OrderDao().Insert(order);
                var listDetailOrder = new CartDetailDao().ListAllByUserID(account.UserID);
                foreach(var item in listDetailOrder)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = id ;
                    orderDetail.ProductID = item.IDProduct;
                    orderDetail.Price = item.Price ;
                    orderDetail.Quantity = item.Quantity ;
                    var create = new OrderDetailDao().Insert(orderDetail);
                }
                var deleteCard = new CartDetailDao().Delete(account.UserID);
                return Redirect("/");
            }
            catch
            {
                return Redirect("/");
            }
            
        }
    }
}