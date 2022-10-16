using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Models.DTO
{
    public class OrderLineItemDTO
    {
        [ValidateNever]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        [Required]
        public int ProductPricingDetailId { get; set; }
        
        public int OrderId { get; set; }

        [Required]
        [Range(1,byte.MaxValue)]
        public byte Quantity { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double UnitPrice { get; set; }

        [ValidateNever]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double SubTotal { get; set; }

        [ValidateNever]
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
