using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class ProductDetail
    {
        public ProductDetail()
        {
            OrderLineItem = new HashSet<OrderLineItem>();
            ProductMedia = new HashSet<ProductMedia>();
            ProductMeta = new HashSet<ProductMeta>();
            ProductVariant = new HashSet<ProductVariant>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string LongDesc { get; set; }
        public double BasePrice { get; set; }
        public double RetailPrice { get; set; }
        public double? ProductWeight { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<OrderLineItem> OrderLineItem { get; set; }
        public virtual ICollection<ProductMedia> ProductMedia { get; set; }
        public virtual ICollection<ProductMeta> ProductMeta { get; set; }
        public virtual ICollection<ProductVariant> ProductVariant { get; set; }
    }
}
