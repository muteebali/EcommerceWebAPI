using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class VariantValue
    {
        public VariantValue()
        {
            ProductVariant = new HashSet<ProductVariant>();
        }

        public byte Id { get; set; }
        public byte VariantId { get; set; }
        public string AttributeValue { get; set; }
        public bool? Visible { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Variant Variant { get; set; }
        public virtual ICollection<ProductVariant> ProductVariant { get; set; }
    }
}
