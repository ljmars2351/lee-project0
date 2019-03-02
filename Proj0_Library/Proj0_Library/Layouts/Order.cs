using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library.Layouts
{
    public class Order
    {
        public int OrdId { get; set; }

        public int CustId { get; set; }

        public List<Products> Cart { get; set; }

        public int StoreId { get; set; }
    }
}
