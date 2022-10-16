using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePOSAPI.Enumerators
{
    public class Enumerator
    {
        public enum OrderStatus:byte
        {
            Cancelled=0,
            Placed=1,
            Confirmed,
            Delivered,
            Refund
        }
    }
}
