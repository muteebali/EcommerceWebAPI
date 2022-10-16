using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models.DTO
{
    public partial class ProductDTO
    {

        [ValidateNever]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        public string LongDesc { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string CategoryTitle { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool? Visible { get; set; }
        public bool Deleted { get; set; }
        public int? PromotionId { get; set; }
        public string Brand { get; set; }

    }
}
