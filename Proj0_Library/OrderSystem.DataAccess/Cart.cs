using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int CustId { get; set; }
        public int ProdId { get; set; }
        public int LocId { get; set; }
        public DateTime CurrentTime { get; set; }
        public int Quantity { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual Location Loc { get; set; }
        public virtual ProdHist Prod { get; set; }
    }
}
