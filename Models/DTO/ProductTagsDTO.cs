using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models.DTO
{
    public partial class ProductTagsDTO
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual TagDTO Tag { get; set; }
    }
}
