using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class ProductVariant
    {
        public int Id { get; set; }
        public int ProductDetailId { get; set; }
        public byte VariantValueId { get; set; }
        public string Sku { get; set; }
        public byte Stock { get; set; }
        public string Gender { get; set; }

        public virtual ProductDetail ProductDetail { get; set; }
        public virtual VariantValue VariantValue { get; set; }
    }
}
