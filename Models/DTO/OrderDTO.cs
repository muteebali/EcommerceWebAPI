using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using OnlinePOSAPI.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models.DTO
{
    public partial class OrderDTO
    {

        [ValidateNever]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public byte Status { get; set; } = (byte)Enumerator.OrderStatus.Placed;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double SubTotal { get; set; }

        [StringLength(20)]
        [Required]
        public string PaymentMethod { get; set; }

        [Range(0,double.MaxValue)]
        public double? ShippingAmount { get; set; } 

        [Range(0,double.MaxValue)]
        public double? Discount { get; set; }

        [Range(0,double.MaxValue)]
        public double? TaxAmount { get; set; } 

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double? GrandTotal { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string Mobile { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }

        [Required]
        [StringLength(100)]
        public string Address2 { get; set; }
        
        [Required]
        public string PostalCode { get; set; }
        
        [Required]
        public string City { get; set; }
        public DateTime? ModifiedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ValidateNever]
        public DateTime CreatedOn { get; set; }

    }
}
