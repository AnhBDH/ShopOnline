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
    public class CartDetailViewModel
    {
        [Key]
        public long IDProduct { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public decimal? ProductPrice { get; set; }

        public decimal? ProductPromotionPrice { get; set; }
    }
}