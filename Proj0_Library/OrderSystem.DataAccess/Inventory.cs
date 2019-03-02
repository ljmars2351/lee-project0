using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int ProdId { get; set; }
        public int Quant { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Product Prod { get; set; }
    }
}
