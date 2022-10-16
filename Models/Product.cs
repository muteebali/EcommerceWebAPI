using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Brand { get; set; }
        public int? PromotionId { get; set; }
        public bool? Visible { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Category Category { get; set; }
        public virtual Promotion Promotion { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
    }
}
