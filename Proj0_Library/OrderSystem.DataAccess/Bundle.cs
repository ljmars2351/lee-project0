using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Bundle
    {
        public Bundle()
        {
            BundleProds = new HashSet<BundleProds>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<BundleProds> BundleProds { get; set; }
    }
}
