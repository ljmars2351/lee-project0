using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Location
    {
        public Location()
        {
            Cart = new HashSet<Cart>();
            Customer = new HashSet<Customer>();
            Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
