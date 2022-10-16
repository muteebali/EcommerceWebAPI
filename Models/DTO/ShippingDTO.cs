using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnlinePOSAPI.Models.DTO
{
    public partial class ShippingDTO
    {
        public int OrderId { get; set; }
        public byte Status { get; set; }
        public string Agency { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
