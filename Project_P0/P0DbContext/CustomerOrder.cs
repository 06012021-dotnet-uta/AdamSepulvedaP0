using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class CustomerOrder
    {
        public CustomerOrder()
        {
            CustomerOrderQuantities = new HashSet<CustomerOrderQuantity>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<CustomerOrderQuantity> CustomerOrderQuantities { get; set; }
    }
}
