using System;
using System.Collections.Generic;
using System.Text;

namespace OrderSystem.Library.Layouts
{
    public class Order
    {
        public int OrdId { get; set; }

        public string CustName { get; set; }

        public List<Products> cart = new List<Products>();
    }
}
