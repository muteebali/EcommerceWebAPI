using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Models.DTO
{
    public class CategoryDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool Deleted { get; set; }
        public bool? Visible { get; set; }

        [ValidateNever]
        public DateTime CreatedOn { get; set; }
        public int? PromotionId { get; set; }

    }
}
