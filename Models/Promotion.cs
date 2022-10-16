using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            Category = new HashSet<Category>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int DiscountValue { get; set; }
        public string DiscountUnit { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public string CouponCode { get; set; }
        public double? MinimumOrderValue { get; set; }
        public double? MaximumDiscountValue { get; set; }
        public bool? IsRedeemAllowed { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
