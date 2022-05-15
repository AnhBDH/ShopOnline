using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
using OShop.Models;
namespace OShop.Dao
{
    public class CartDetailDao
    {
        ShopDBContext db = null;
        public CartDetailDao()
        {
            db = new ShopDBContext();
        }

        public bool Exist (long UserId, long ProductId)
        {
            var entity = db.CartDetails.SingleOrDefault(x => x.IDCustomer == UserId && x.IDProduct == ProductId);
            if(entity == null)
            {
                return false;
            }
            return true;
        }

        public bool Insert (CartDetail entity)
        {
            db.CartDetails.Add(entity);
            db.SaveChanges();
            return true;
        }

        public bool AddOne (long UserId, long ProductId)
        {
            var entity = db.CartDetails.Where(x => x.IDCustomer == UserId && x.IDProduct == ProductId).SingleOrDefault();
            if(entity!= null)
            {                
                entity.Quantity = entity.Quantity + 1;
                db.SaveChanges();
            }
            return true;
        }

        public List<CartDetailViewModel> ShowCart(long UserId)
        {
            List<CartDetail> listCart = db.CartDetails.Where(x => x.IDCustomer == UserId).ToList();
            var result = new List<CartDetailViewModel>();
            foreach (var item in listCart) {
                var productDetail = db.Products.Find(item.IDProduct);
                var cartdetail = new CartDetailViewModel();
                cartdetail.IDProduct = item.IDProduct;
                cartdetail.Quantity = item.Quantity;
                cartdetail.Price = item.Price;
                cartdetail.Name = productDetail.Name;
                cartdetail.Code = productDetail.Code;
                cartdetail.MetaTitle = productDetail.Image;
                cartdetail.Image = productDetail.Image;
                cartdetail.ProductPrice = productDetail.Price;
                cartdetail.ProductPromotionPrice = productDetail.PromotionPrice;
                result.Add(cartdetail);
            }
            return result;
        }

        public List<CartDetail> ListAllByUserID(long UserId)
        {
            return db.CartDetails.Where(x => x.IDCustomer == UserId).ToList();
        }

        public bool Delete (long userID)
        {
            var entity = db.CartDetails.Where(x => x.IDCustomer == userID);
            foreach(var item in entity)
            {
                db.CartDetails.Remove(item);
            }
            db.SaveChanges();
            return true;
        }
    }
}