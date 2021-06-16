using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class CustomerOrderQuantity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }

        public virtual CustomerOrder Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
