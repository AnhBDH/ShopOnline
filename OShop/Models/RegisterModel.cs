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
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name="Tên đăng nhập")]
        [Required(ErrorMessage ="Yêu cầu nhập tên đăng nhập")]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage ="Độ dài từ 6 - 20 ký tự")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không trùng khớp")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ tên")]
        public string Name { set; get; }

        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Display(Name = "Email")]
        public string Email { set; get; }

        [Display(Name = "Điện thoại")]
        public string Phone { set; get; }
    }
}