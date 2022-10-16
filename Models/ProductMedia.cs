using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models
{
    public partial class ProductMedia
    {
        public int Id { get; set; }
        public int ProductDetailId { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
        public long? Size { get; set; }
        public bool Thumbnail { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ProductDetail ProductDetail { get; set; }
    }
}
