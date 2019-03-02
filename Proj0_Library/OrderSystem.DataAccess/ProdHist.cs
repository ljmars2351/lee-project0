using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class ProdHist
    {
        public ProdHist()
        {
            Cart = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
    }
}
