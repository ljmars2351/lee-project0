using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library
{
    public class Order
    {
        public int OrdId { get; set; }

        public int CustId { get; set; }

        public int StoreId { get; set; }

        public int ProdId { get; set; }

        public int Quantity { get; set; }

        public DateTime OrdTIme { get; set; }
    }
}
