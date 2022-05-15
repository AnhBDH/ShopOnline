using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
namespace OShop.Dao
{
    public class UserDao
    {
        ShopDBContext db = null;
        public UserDao()
        {
            db = new ShopDBContext();
        }

        public long Insert(User entity)
        { 
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User GetByEmail(string email)
        {
            return db.Users.SingleOrDefault(x => x.Email == email);
        }

        public IEnumerable<User> ListAllPaging(int page, int pagesize)
        {
            return db.Users.OrderBy(x=>x.CreatedDate).ToPagedList(page, pagesize);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                if (!String.IsNullOrEmpty(entity.Password))
                {
                    var encrytedMd5Pas = Encryptor.MD5Hash(entity.Password);
                    user.Password = encrytedMd5Pas;
                }
                if (!String.IsNullOrEmpty(entity.Name))
                {
                    user.Name = entity.Name;
                }
                if (!String.IsNullOrEmpty(entity.Avartar))
                {
                    user.Avartar = entity.Avartar;
                }
                if (!String.IsNullOrEmpty(entity.Email))
                {
                    user.Email = entity.Email;
                }
                if (!String.IsNullOrEmpty(entity.Phone))
                {
                    user.Phone = entity.Phone;
                }
                user.GroupID = entity.GroupID;
                user.Status = entity.Status;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                            {
                                return 1;
                            }
                            else return -2;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (result.GroupID == CommonConstants.MEMBER_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                            {
                                return 1;
                            }
                            else return -2;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

            }
        }

        public bool Delete(int id)
        {
            try {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}