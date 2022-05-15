using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
namespace OShop.Dao
{
    public class ProductDao
    {
        ShopDBContext db = null;
        public ProductDao()
        {
            db = new ShopDBContext();
        }

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);                
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IEnumerable<Product> ListCategoryPaging(int IDcategory, int page, int pagesize)
        {
            return db.Products.Where(x => x.CategoryID==IDcategory).OrderBy(x => x.Price).ToPagedList(page, pagesize);
        }

        public IEnumerable<Product> ListAllPaging(int page, int pagesize)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pagesize);
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public List<Product> ListNewProduct(int top = 4)
        {
            return db.Products.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListSaleProduct(int top = 4)
        {
            return db.Products.Where(x=>x.PromotionPrice>0).OrderByDescending(x => x.ModifiedDate).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListFollowProduct(int top = 4)
        {
            return db.Products.OrderByDescending(x => x.ViewCount).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }


    }
}