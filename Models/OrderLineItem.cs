using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class OrderLineItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductPricingDetailId { get; set; }
        public double UnitPrice { get; set; }
        public byte Quantity { get; set; }
        public double Subtotal { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Order Order { get; set; }
        public virtual ProductDetail ProductPricingDetail { get; set; }
    }
}
