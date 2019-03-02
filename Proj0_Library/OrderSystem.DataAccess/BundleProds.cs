using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class BundleProds
    {
        public int BundleId { get; set; }
        public int ProdId { get; set; }

        public virtual Bundle Bundle { get; set; }
        public virtual Product Prod { get; set; }
    }
}
