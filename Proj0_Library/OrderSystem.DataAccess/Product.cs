using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Product
    {
        public Product()
        {
            BundleProds = new HashSet<BundleProds>();
            Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<BundleProds> BundleProds { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
