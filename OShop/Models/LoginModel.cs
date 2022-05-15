using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OShop.EF;
using PagedList;
using OShop.Common;
using System.ComponentModel.DataAnnotations;

namespace OShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời bạn nhập User Name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời bạn nhập Password")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }

    }
}