using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class Variant
    {
        public Variant()
        {
            VariantValue = new HashSet<VariantValue>();
        }

        public byte Id { get; set; }
        public string Attribute { get; set; }
        public bool? Visible { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<VariantValue> VariantValue { get; set; }
    }
}
