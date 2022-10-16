using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Models.DTO
{
    public class PromotionDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ValidateNever]
        public int Id { get; set; }

        [Required]
        public int DiscountValue { get; set; }
        
        [Required]
        [StringLength(10)]
        public string DiscountUnit { get; set; }

        [Required]
        public string ValidFrom { get; set ; }

        [Required]
        public string ValidUntil { get; set; }
        public string CouponCode { get; set; }
        public double? MinimumOrderValue { get; set; }
        public double? MaximumDiscountValue { get; set; }
        public bool? IsRedeemAllowed { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
