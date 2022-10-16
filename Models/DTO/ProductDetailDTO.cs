using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models.DTO
{
    public partial class ProductDetailDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        
        [Required]
        public double BasePrice { get; set; }
        
        [Required]
        public double RetailPrice { get; set; }

        [Required]
        public byte Stock { get; set; }

        public double? ProductWeight { get; set; }
        public bool Deleted { get; set; }
        public bool? Visible { get; set; }

        [ValidateNever]
        public DateTime CreatedOn { get; set; }

        public List<byte> ProductVariantIDs { get; set; }

    }
}
