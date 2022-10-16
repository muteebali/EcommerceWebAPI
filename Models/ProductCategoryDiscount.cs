using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class ProductCategoryDiscount
    {
        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public double DiscountValue { get; set; }
        public string DiscountUnit { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CouponCode { get; set; }
        public double MinimumOrderValue { get; set; }
        public double MaximumDiscountAmount { get; set; }

        public virtual Category ProductCategory { get; set; }
    }
}
