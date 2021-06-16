using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class Product
    {
        public Product()
        {
            CustomerOrderQuantities = new HashSet<CustomerOrderQuantity>();
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductDesc { get; set; }

        public virtual ICollection<CustomerOrderQuantity> CustomerOrderQuantities { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
