using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Models.DTO
{
    public class InvoiceDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
        
        [Required]
        [StringLength(15)]
        public string Status { get; set; }
    }
}
