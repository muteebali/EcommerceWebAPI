using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Models.DTO
{
    public class OrderDetailsDTO
    {
        public OrderDTO Order { get; set; }
        public  List<OrderLineItemDTO> OrderLineItems { get; set; }
    }
}
