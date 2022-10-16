using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderLineItem = new HashSet<OrderLineItem>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public byte Status { get; set; }
        public double SubTotal { get; set; }
        public string PaymentMethod { get; set; }
        public double? ShippingAmount { get; set; }
        public double? Discount { get; set; }
        public double? TaxAmount { get; set; }
        public double? GrandTotal { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual User Customer { get; set; }
        public virtual ICollection<OrderLineItem> OrderLineItem { get; set; }
    }
}
