using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
namespace OShop.Dao
{
    public class CategoryDao
    {
        ShopDBContext db = null;
        public CategoryDao()
        {
            db = new ShopDBContext();
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }

        public IEnumerable<Category> ListAllPaging(int page, int pagesize)
        {
            return db.Categories.OrderByDescending(x => x.DisplayOrder).ToPagedList(page, pagesize);
        }

        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                if (!String.IsNullOrEmpty(entity.Name))
                {
                    category.Name = entity.Name;
                }
                if (!String.IsNullOrEmpty(entity.MetaTitle))
                {
                    category.MetaTitle = entity.MetaTitle;
                }
                if (entity.DisplayOrder!=null)
                {
                    category.DisplayOrder = entity.DisplayOrder;
                }
                category.Status = entity.Status;
                category.ModifiedBy = entity.ModifiedBy;
                category.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }

        public List<Category> ListAllTrue()
        {
            return db.Categories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}