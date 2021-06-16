using System;
using System.Collections.Generic;

#nullable disable

namespace P0DbContext
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            StoreInventories = new HashSet<StoreInventory>();
        }

        public int StoreId { get; set; }
        public string StoreStreet { get; set; }
        public string StoreCity { get; set; }
        public string StoreState { get; set; }
        public int StoreZip { get; set; }

        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
