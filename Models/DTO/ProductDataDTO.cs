using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Models.DTO
{
    public class ProductDataDTO
    {
        [Required]
        public ProductDTO BasicInfo { get; set; }

        [Required]
        public ProductDetailDTO ProductDetail { get; set; }

        public ProductMediaDTO[] MediaDetails { get; set; }

        public List<int> Tags { get; set; }
    }
}
