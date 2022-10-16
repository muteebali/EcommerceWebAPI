using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool Deleted { get; set; }
        public bool? Visible { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? PromotionId { get; set; }

        public virtual Promotion Promotion { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
